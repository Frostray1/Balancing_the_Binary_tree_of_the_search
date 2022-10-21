using System.Drawing;
using System.IO;

namespace BalancedBinaryTree
{
    public static class GraphVizEngine
    {
        public static Bitmap RenderImage(string dot, string outputType, RenderEngine engine = RenderEngine.Dot)
        {
            string exe = @".\graphviz\";
            switch (engine)
            {
                case RenderEngine.Dot:
                    exe += "dot.exe";
                    break;
                case RenderEngine.Neato:
                    exe += "neato.exe";
                    break;
                case RenderEngine.Twopi:
                    exe += "twopi.exe";
                    break;
                default:
                    exe += "dot.exe";
                    break;
            }
            string output = @".\graphviz\temp";
            File.Delete(output + "." + outputType);
            File.WriteAllText(output, dot);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            // Stop the process from opening a new window
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = exe;
            process.StartInfo.Arguments = string.Format(@"{0} -T" + outputType + " -O", output);
            process.Start();
            process.WaitForExit();
            process.Close();
            Bitmap bitmap = null; ;
            using (Stream bmpStream = File.Open(output + "." + outputType, System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);
                bitmap = new Bitmap(image);
            }
            File.Delete(output);
            return bitmap;
        }
        public enum RenderEngine
        {
            Dot,
            Neato,
            Twopi
        }
    }
}
