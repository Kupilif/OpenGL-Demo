using System.Windows.Forms;
using OpenGL;

namespace OpenGlDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void glControl_ContextCreated(object sender, OpenGL.GlControlEventArgs e)
        {

        }

        private void glControl_Render(object sender, OpenGL.GlControlEventArgs e)
        {
            Control senderControl = (Control)sender;

            Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit);
        }
    }
}
