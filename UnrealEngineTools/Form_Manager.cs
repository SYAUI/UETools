using System.Data;
using System.Data.SQLite;
using System.Reflection;


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
            if(DBUtility.SQLiteHelper.IsDataExists("blueprintnode", "name", t_name.Text) || (t_name.Text == ""))
            {
                label_add.Text = "����������";
                err_name.SetError(t_name, "��ʹ��Ψһ����������Ϊ��");
                BPNNameValid = false;
            }
            else
            {
                if (BPNDataValid)
                    label_add.Text = "д��׼������";
                else
                    label_add.Text = "��������";
                BPNNameValid = true;
                err_name.Clear();
            }
        }

        private void b_iputbpn_Click(object sender, EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(typeof(string)))//��֤�Ƿ����ַ���
            {
                BPNClipboardData = (string)data.GetData(typeof(string));
                if (BPNClipboardData.StartsWith("Begin Object Class"))
                {
                    BPNDataValid = true;
                    if (BPNNameValid)
                        label_add.Text = "д��׼������";
                    else
                        label_add.Text = "��ͼ�����뻺��";
                    err_BPNdata.Clear();
                }
                else
                {
                    err_BPNdata.SetError(b_iputbpn, "���������ݲ�Ϊ��ͼ�ڵ�");
                    BPNDataValid = false;
                }
            }


        }
        private void b_inputIMG_Click(object sender, EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(typeof(Bitmap)))//��֤�Ƿ���ͼƬ
            {
                BPNBitMap = (Bitmap)data.GetData(typeof(Bitmap));
                label_add.Text = "��ͼ�����뻺��";
            }
            else
            {
                label_add.Text = "����ͼƬ����";
            }
        }

        private void b_add_Click(object sender, EventArgs e)
        {
            if (BPNDataValid && BPNNameValid)
            {
                SQLiteParameter[] parameters = {
                    new SQLiteParameter("@name", DbType.String,32),
                    new SQLiteParameter("@src", DbType.String),
                    new SQLiteParameter("@desc", DbType.String),
                    new SQLiteParameter("@image", DbType.Binary)};
                parameters[0].Value = t_name.Text;
                parameters[1].Value = BPNClipboardData;
                parameters[2].Value = t_desc.Text;
                parameters[3].Value = BPNBitMap;
                DBUtility.SQLiteHelper.ExecuteNonQuery("insert into blueprintnode VALUES (@name,@src,@desc,@image)", parameters);

            }
            else { MessageBox.Show("���������������飡", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        
    }
}
