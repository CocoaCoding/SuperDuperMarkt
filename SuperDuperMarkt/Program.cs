namespace SuperDuperMarkt
{
    class Program
    {
        static void Main(string[] args)
        {
            SuperDuperMarktProgram prog = SuperDuperMarktProgram.GetInstance();
            prog.Run();
        }
    }
}
