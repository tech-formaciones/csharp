namespace Formacion.CSharp.ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TareasAnidadas();
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
                if(ct.IsCancellationRequested) return resultado;
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

            Task externa = Task.Run(() => {
                Console.WriteLine("Inicio tarea Externa");

                Task interna = Task.Run(() => {
                    Console.WriteLine("Inicio tarea Interna");
                    Thread.Sleep(3000);
                    Console.WriteLine("Fin tarea Interna");
                });
            });

            externa.Wait();
            Console.WriteLine("Fin tarea Externa");

            // Tareas anidadas, donde las tereas internas deben finalizar antes
            // de las tareas externas

            Task padre = Task.Run(() => {
                Console.WriteLine("Inicio tarea Padre");

                Task interna2 = Task.Factory.StartNew(() => {
                    Console.WriteLine("Inicio tarea Hija");
                    Thread.Sleep(3000);
                    Console.WriteLine("Fin tarea Hija");
                }, TaskCreationOptions.AttachedToParent);
            });
            padre.Wait();
            Console.WriteLine("Fin tarea Padre");
        }
    }
}
