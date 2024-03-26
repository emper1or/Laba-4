using LogicalLayes;
using System.Windows.Forms;

namespace Laba_4
{
    public partial class Form2 : Form
    {
        public Form Form1 { get; set; }

        private Locgica Logical = new Locgica();

        public Form2()
        {
            InitializeComponent();
        }

        // при инициализации формы отобжаться в текстбоксах текущий словарь

        private void Form2_Load(object sender, System.EventArgs e)
        {

            richTextBox1.Text = Logical.ShowThisDic(1);
            richTextBox2.Text = Logical.ShowThisDic(2);
            richTextBox3.Text = Logical.ShowThisDic(3);
            richTextBox4.Text = Logical.ShowThisDic(4);
        }

        // при нажатии кнопки обновалять словарь и позже обновлять его в файл

        private void button1_Click(object sender, System.EventArgs e)
        {
            Logical.UploadThisDic(1, richTextBox1.Text);
            Logical.UploadThisDic(2, richTextBox2.Text);
            Logical.UploadThisDic(3, richTextBox3.Text);
            Logical.UploadThisDic(4, richTextBox4.Text);
            Logical.UploadAll();
        }
    }
}
