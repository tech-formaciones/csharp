namespace Formacion.CSharp.ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio del MAIN");
            ParallelDemoFor();
            Console.WriteLine("Fin del MAIN");


            Console.ReadKey();
        }

        static void MetodoTest()
        {
            Console.WriteLine("Método ejecutandose ...");
        }
        static int MetodoTest(CancellationToken ct)
        {
            int resultado = 0;
            while (true)
            {
                resultado++;
                if (ct.IsCancellationRequested) return resultado;
            }
        }

        static void CrearTareas()
        {
            Task tarea1 = new Task(new Action(MetodoTest));

            Task tarea2 = new Task(delegate {
                Thread.Sleep(3000);
                Console.WriteLine("Delegado ejecutandose ...");
            });

            Task tarea3 = new Task(() => {
                Console.WriteLine("Método 3 anónimo ejecutandose ...");
            });

            Task tarea4 = new Task(() => MetodoTest());

            Task tarea5 = Task.Run(() => {
                Console.WriteLine("Método 5 anónimo ejecutandose ...");
            });

            Task<string> tarea6 = Task<string>.Run(() => {
                Console.WriteLine("Método 6 anónimo ejecutandose ...");
                Thread.Sleep(5000);
                Console.WriteLine("Método 6 anónimo finalizado ...");
                return "Método 6";
            });

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;
            Task<int> tarea7 = Task<int>.Run(() => {
                return MetodoTest(ct);
            });

            tarea6.Wait();
            tarea6.Wait(2000);
            var resultado = tarea6.Result;
            Console.WriteLine($"{resultado}");

            Console.WriteLine($"Estado tarea1: {tarea1.Status}");
            tarea1.Start();
            tarea2.Start();
            tarea3.Start();

            Task[] tareas = { tarea1, tarea2, tarea3 };
            Task.WaitAll(tareas);
            Task.WaitAll(tareas, 1000);
            Task.WaitAny(tareas);
            Task.WaitAny(tareas, 1000);

            tarea4.Start();

            Console.WriteLine($"Estado tarea1: {tarea1.Status}");
            tarea1.Wait();
            Console.WriteLine($"Estado tarea1: {tarea1.Status}");

            Console.WriteLine("Pulsa una tecla para cancelar tarea7");
            Console.ReadKey();
            cts.Cancel();
            Console.WriteLine(tarea7.Result);

            ////////////////////////////////////////////////////////////////////////
        }
        static void TareasAnidadas()
        {
            // Tareas anidadas, que contiene otras tareas
            // La tarea externa puede finalizar antes que las tereas internas.

            //Task externa = Task.Run(() => {
            //    Console.WriteLine("Inicio tarea Externa");

            //    Task interna = Task.Run(() => {
            //        Console.WriteLine("Inicio tarea Interna");
            //        Thread.Sleep(3000);
            //        Console.WriteLine("Fin tarea Interna");
            //    });
            //});

            //externa.Wait();
            //Console.WriteLine("Fin tarea Externa");

            // Tareas anidadas, donde las tereas internas deben finalizar antes
            // de las tareas externas

            Task padre = Task.Run(() => {
                Console.WriteLine("Inicio tarea Padre");

                Task hija = Task.Factory.StartNew(() => {
                    Console.WriteLine("Inicio tarea Hija");
                    Thread.Sleep(3000);
                    Console.WriteLine("Fin tarea Hija");
                }, TaskCreationOptions.AttachedToParent);
            });
            padre.Wait();
            Console.WriteLine("Fin tarea Padre");
        }

        static void TestCalcular1()
        {
            var calcular = new Calcular();
            bool resultado = calcular.StartAsync().Result;
            Console.WriteLine("El programa continua ...");
        }
        static async void TestCalcular2()
        {
            var calcular = new Calcular();
            bool resultado = await calcular.StartAsync();
            Console.WriteLine("El programa continua ...");
        }
        static async void TestCalcular3()
        {
            var calcular = new Calcular();

            calcular.FinCalculos += (sender, message) => {
                Console.WriteLine(message);
            };

            calcular.StartAsyncWithEvents();
            Console.WriteLine("El programa continua ...");
        }

        static void ParallelDemo()
        {
            // Procesos en paralelo
            Parallel.Invoke(
                () => MetodoTest(),
                () => MetodoTest(),
                () => {
                    var calcular = new Calcular();
                    bool resultado = calcular.StartAsync().Result;
                    Console.WriteLine("El programa continua ...");
                },
                () => {
                    Task<string> tarea6 = Task<string>.Run(() => {
                        Console.WriteLine("Método 6 anónimo ejecutandose ...");
                        Thread.Sleep(5000);
                        Console.WriteLine("Método 6 anónimo finalizado ...");
                        return "Método 6";
                    });
                });
        }
        static void ParallelDemoFor()
        {
            double[] array = new double[50000000];

            var f1 = DateTime.Now;
            for (int i = 1; i<array.Length; i++) array[i] = Math.Sqrt(i);
            var f2 = DateTime.Now;
            Parallel.For(1, 50000000, (i) => array[i] = Math.Sqrt(i));
            var f3 = DateTime.Now;

            Console.WriteLine($"          FOR -> {f2.Subtract(f1).TotalMilliseconds} ms.");
            Console.WriteLine($" PARALLEL FOR -> {f3.Subtract(f2).TotalMilliseconds} ms.");
        }

        static void ParallelDemoForEach()
        { }
        static void ParallelDemoLinq()
        { }

        static void DemoStatic()
        {
            // Static Class
            // No se puede instanciar utilizando new, se utilizand directamente por el 
            // nombre de la clase.
            ClassC.Propiedad = 10;
            ClassC.Metodo();

            // NO Static Class
            // Necesitamos instanciarlas con new.
            ClassA demoA = new ClassA();
            demoA.Propiedad = 10;
            demoA.Metodo();

            // NO Static Class, con miembros Static
            // Para acceder a los miembros estáticos lo hacemos mediante el nombre de la clase
            // Para acceder a los miembros no estáticos tenemos que instanciar
            ClassB.Propiedad2 = 10;
            ClassB.Metodo2();

            ClassB demoB = new ClassB();
            demoB.Propiedad = 10;
            demoB.Metodo();
        }
    }


    public class Calcular
    {
        private double[] array = new double[50000000];
        public event EventHandler<string> FinCalculos;

        public double[] Array {
            get => array; 
            set => array = value;
        }

        public bool Start()
        { 
            for(int i = 1; i < array.Length; i++) array[i] = Math.Sqrt(i);
            Console.WriteLine("FIN DEL CALCULO");

            return true;    
        }

        public Task<bool> StartAsync()
        {
            return Task<bool>.Run(() => {
                for (int i = 1; i < array.Length; i++) array[i] = Math.Sqrt(i);
                Console.WriteLine("FIN DEL CALCULO");

                return true;
            });
        }

        public Task<bool> StartAsyncWithEvents()
        {
            return Task<bool>.Run(() => {
                for (int i = 1; i < array.Length; i++) array[i] = Math.Sqrt(i);                
                FinCalculos?.Invoke(this, $"FIN CALCULOS {DateTime.Now.ToString()}");

                return true;
            });
        }
    }

    public class ClassA
    {
        public int Propiedad { get; set; }
        public void Metodo() { }
    }

    public class ClassB
    {
        public int Propiedad { get; set; }
        public static int Propiedad2 { get; set; }
        public void Metodo() { }
        public static void Metodo2() { }
    }

    public static class ClassC
    {
        public static int Propiedad { get; set; }
        public static void Metodo() { }
    }
}
