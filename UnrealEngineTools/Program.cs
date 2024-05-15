

namespace UnrealEngineTools
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            
            ApplicationConfiguration.Initialize();
            Application.Run(new Form_Manager());
        }
    }
}