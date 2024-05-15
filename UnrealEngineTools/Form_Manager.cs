using System.Data.SQLite;


namespace UnrealEngineTools
{
    public partial class Form_Manager : Form
    {
        public Form_Manager()
        {
            InitializeComponent();
        }
        private string BPNClipboardData = string.Empty;
        private Bitmap? BPNBitMap;
        private bool BPNDataValid = false;
        private bool BPNNameValid = false;

        private void b_Query_Click(object sender, EventArgs e)
        {


        }



        private void t_name_Leave(object sender, EventArgs e)
        {
            if(DBUtility.SQLiteHelper.IsDataExists("blueprintnode", "name", t_name.Text))
            {
                label_add.Text = "命名已存在";
                err_name.SetError(t_name, "请使用唯一命名");
            }
            else
            {
                if (BPNDataValid)
                    label_add.Text = "写入准备就绪";
                else
                    label_add.Text = "命名可用";
                err_name.Clear();
            }
        }

        private void b_iputbpn_Click(object sender, EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(typeof(string)))//验证是否是字符串
            {
                BPNClipboardData = (string)data.GetData(typeof(string));
                if (BPNClipboardData.StartsWith("Begin Object Class"))
                {
                    BPNDataValid = true;
                    if (BPNNameValid)
                        label_add.Text = "写入准备就绪";
                    else
                        label_add.Text = "蓝图已载入缓存";
                    err_BPNdata.Clear();
                }
                else
                {
                    err_BPNdata.SetError(b_iputbpn, "剪贴板内容不为蓝图节点");
                    BPNDataValid = false;
                }
            }


        }
        private void b_inputIMG_Click(object sender, EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(typeof(Bitmap)))//验证是否是图片
            {
                BPNBitMap = (Bitmap)data.GetData(typeof(Bitmap));
                label_add.Text = "截图已载入缓存";
            }
            else
            {
                label_add.Text = "错误图片类型";
            }
        }

        private void b_add_Click(object sender, EventArgs e)
        {
            if (BPNDataValid && BPNNameValid)
            {
                

            }
            else { MessageBox.Show("数据输入有误！请检查！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        
    }
}
