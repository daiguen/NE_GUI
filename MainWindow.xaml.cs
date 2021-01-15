using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace New_Enforce_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>    
    public partial class MainWindow : Window
    {
        public static RoutedCommand newMenuItemCommand = new RoutedCommand();
        public static RoutedCommand openMenuItemCommand = new RoutedCommand();
        public static RoutedCommand saveMenuItemCommand = new RoutedCommand();
        public static RoutedCommand exitMenuItemCommand = new RoutedCommand();

        string _inpfile = "";
        string _pfile = "";
        int _cw_no = 1;
        int _cyl_no = 0;
        List<CW> _cw_list = new List<CW>();

        public MainWindow()
        {            
            InitializeComponent();
            CWDataGrid.ItemsSource = _cw_list;
            InitVariables();
            
            newMenuItemCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            openMenuItemCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            saveMenuItemCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            exitMenuItemCommand.InputGestures.Add(new KeyGesture(Key.F4, ModifierKeys.Alt));

            CommandBindings.Add(new CommandBinding(newMenuItemCommand, NewMenuItem_Click));
            CommandBindings.Add(new CommandBinding(openMenuItemCommand, OpenMenuItem_Click));
            CommandBindings.Add(new CommandBinding(saveMenuItemCommand, SaveMenuItem_Click));
            CommandBindings.Add(new CommandBinding(exitMenuItemCommand, ExitMenuItem_Click));                   
        }

        private void NewMenuItem_Click(object sender, RoutedEventArgs e)
        {            
            MessageBoxResult result = MessageBox.Show("Can you save this file?", "Enforce", MessageBoxButton.YesNoCancel);
            if ( result == MessageBoxResult.Yes)
            {
                Microsoft.Win32.SaveFileDialog sfdlg = new Microsoft.Win32.SaveFileDialog();
                sfdlg.InitialDirectory = "C:\\";
                sfdlg.Filter = "Enforce Input File|*.ne";
                if (_inpfile == "")
                {
                    if (sfdlg.ShowDialog() == true)
                    {
                        _inpfile = sfdlg.FileName;
                    }
                }
                if (_inpfile == "")
                {

                }
                else
                {
                    WriteInputFile(_inpfile);                    
                    InitVariables();
                }
            }
            else if (result == MessageBoxResult.No)
            {                
                InitVariables();
            }
            
        }

        private void InitVariables()
        {
            _inpfile = "";
            _pfile = "";
            _cw_no = 1;
            _cyl_no = 0;            
            
            Stroke_Type_Val.Text = "";
            CrankRot_Val.Text = "";
            RPM_Val.Text = "";
            NumCyl_Val.Text = "";
            Order_Val.Text = "";
            BankAng_Val.Text = "";

            fCylPos_Val.Text = "";
            CylDis_Val.Text = "";
            CrankMass_Val.Text = "";
            Pmass_Val.Text = "";
            Pbore_Val.Text = "";

            ConMass_Val.Text = "";
            ConIner_Val.Text = "";
            ConLen_Val.Text = "";
            ConCOG_Val.Text = "";
            MB_Val.Text = "";
            
            fWebPos_Val.Text = "";
            WebDis_Val.Text = "";
            WebCOG_Val.Text = "";
            WebMass_Val.Text = "";

            RemoveAll_CW_Data();

            Press_Val.Text = "";
        }

        private void InitVariables_without_inpfile()
        {
            _pfile = "";
            _cw_no = 1;
            _cyl_no = 0;           

            Stroke_Type_Val.Text = "";
            CrankRot_Val.Text = "";
            RPM_Val.Text = "";
            NumCyl_Val.Text = "";
            Order_Val.Text = "";
            BankAng_Val.Text = "";

            fCylPos_Val.Text = "";
            CylDis_Val.Text = "";
            CrankMass_Val.Text = "";
            Pmass_Val.Text = "";
            Pbore_Val.Text = "";

            ConMass_Val.Text = "";
            ConIner_Val.Text = "";
            ConLen_Val.Text = "";
            ConCOG_Val.Text = "";
            MB_Val.Text = "";

            fWebPos_Val.Text = "";
            WebDis_Val.Text = "";
            WebCOG_Val.Text = "";
            WebMass_Val.Text = "";

            RemoveAll_CW_Data();

            Press_Val.Text = "";
        }
        
        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofdlg = new Microsoft.Win32.OpenFileDialog();
            ofdlg.InitialDirectory = "C:\\";
            ofdlg.Filter = "Enforce Input File|*.ne";
            if (ofdlg.ShowDialog() == true)
            {
                _inpfile = ofdlg.FileName;
            }

            // Read
            if (_inpfile != "")
            {
                InitVariables_without_inpfile();
                StatusMessagBox.Text += "[Open] " + _inpfile + "\r\n";
                ReadInputFile(_inpfile);
            }
        }
        
        private void ReadInputFile(string inpfile)
        {            
            string[] text_value = System.IO.File.ReadAllLines(_inpfile);
            if (text_value.Length > 0)
            {
                double cyl_dis1 = 0;
                double cyl_dis2 = 0;
                double cyl_dis = 0;
                double mb_dis1 = 0;
                double mb_dis2 = 0;
                double mb_dis = 0;
                double web_dis1 = 0;
                double web_dis2 = 0;
                double web_dis = 0;
                List<double> fo = new List<double>();

                List<int> cw_web_idx1 = new List<int>();
                List<int> cw_web_idx2 = new List<int>();

                for (int i = 0; i < text_value.Length; i++)
                {
                    string[] sp_text_value = text_value[i].Split('\t');

                    if (sp_text_value[0] == "PARAMETERS")
                    {
                        RPM_Val.Text = sp_text_value[2];
                        double rpm = double.Parse(RPM_Val.Text);
                        if (rpm >= 0)
                        {
                            CrankRot_Val.Text = "CW";
                            RPM_Val.Text = rpm.ToString();
                        }
                        else
                        {
                            CrankRot_Val.Text = "CCW";
                            RPM_Val.Text = (-rpm).ToString();
                        }
                        Stroke_Type_Val.Text = sp_text_value[4];
                    }
                    else if (sp_text_value[0] == "CYLINDER")
                    {
                        if (Stroke_Type_Val.Text == "4" || Stroke_Type_Val.Text == "4-stroke")
                        {
                            if (int.Parse(sp_text_value[1]) == 1)
                            {
                                fCylPos_Val.Text = sp_text_value[4];
                                cyl_dis1 = double.Parse(sp_text_value[4]);
                            }
                            else if (int.Parse(sp_text_value[1]) == 2) cyl_dis2 = double.Parse(sp_text_value[4]);                            
                            cyl_dis = Math.Abs(cyl_dis1 - cyl_dis2);
                        }
                        else if (Stroke_Type_Val.Text == "2" || Stroke_Type_Val.Text == "2-stroke")
                        {
                            cyl_dis = 0;
                        }
                        if (BankAng_Val.Text == "") BankAng_Val.Text = sp_text_value[2];
                        if (CrankMass_Val.Text == "") CrankMass_Val.Text = sp_text_value[5];
                        fo.Add(double.Parse(sp_text_value[3]));
                        _cyl_no++;
                    }
                    else if (sp_text_value[0] == "PISTON")
                    {
                        if (Pmass_Val.Text == "") Pmass_Val.Text = sp_text_value[2];
                        if (Pbore_Val.Text == "") Pbore_Val.Text = sp_text_value[3];                        
                    }
                    else if (sp_text_value[0] == "CONROD")
                    {
                        if (ConMass_Val.Text == "") ConMass_Val.Text = sp_text_value[2];
                        if (ConIner_Val.Text == "") ConIner_Val.Text = sp_text_value[3];
                        if (ConLen_Val.Text == "") ConLen_Val.Text = sp_text_value[4];
                        if (ConCOG_Val.Text == "") ConCOG_Val.Text = sp_text_value[5];
                    }
                    else if (sp_text_value[0] == "MB")
                    {
                        if (int.Parse(sp_text_value[1]) == 1) mb_dis1 = double.Parse(sp_text_value[2]);
                        else if (int.Parse(sp_text_value[1]) == 2) mb_dis2 = double.Parse(sp_text_value[2]);
                        mb_dis = Math.Abs(mb_dis1 - mb_dis2);
                    }
                    else if (sp_text_value[0] == "WEB")
                    {
                        if (int.Parse(sp_text_value[1]) == 1)
                        {
                            fWebPos_Val.Text = sp_text_value[2];
                            web_dis1 = double.Parse(sp_text_value[2]);
                        }
                        else if (int.Parse(sp_text_value[1]) == 2) web_dis2 = double.Parse(sp_text_value[2]);
                        web_dis = Math.Abs(web_dis1 - web_dis2);                        
                        if (WebMass_Val.Text == "") WebMass_Val.Text = sp_text_value[3];
                        if (Stroke_Val.Text == "") Stroke_Val.Text = sp_text_value[4];
                        if (WebCOG_Val.Text == "") WebCOG_Val.Text = sp_text_value[5];                        
                        
                    }
                    else if (sp_text_value[0] == "PRESSURE")
                    {
                        if (Press_Val.Text == "") Press_Val.Text = sp_text_value[2];
                    }
                    else if (sp_text_value[0] == "CW")
                    {
                        int no = int.Parse(sp_text_value[1]);
                        double pos = double.Parse(sp_text_value[2]);
                        double mass = double.Parse(sp_text_value[3]);
                        double cog = double.Parse(sp_text_value[4]);
                        _cw_list.Add(new CW() { No = no, Position = pos, Mass = mass, COG = cog });
                        _cw_no = int.Parse(sp_text_value[1]) + 1;
                    }
                    else if (sp_text_value[0] == "THROW")
                    {
                        if (Order_Val.Text == "") Order_Val.Text += sp_text_value[2];
                        else Order_Val.Text += "-" + sp_text_value[2];
                    }
                    else
                    {

                    }
                }

                NumCyl_Val.Text = _cyl_no.ToString();
                CylDis_Val.Text = cyl_dis.ToString();
                MB_Val.Text = mb_dis.ToString();
                WebDis_Val.Text = web_dis.ToString();
                Order_Val.Text += "/";
                calculate_firing_order1(fo);
                CWDataGrid.Items.Refresh();
            }
        }

        private void calculate_firing_order1(List<double> fo)
        {
            List<int> order = new List<int>();
            string str_order = "";
            int k = 0;
            while (k != fo.Count)
            {
                order.Add(0);
                fo[k] = System.Math.Abs(fo[k]);
                k++;
            }

            double ref_val = 0;
            for (int j = 1; j < 1 + order.Count; j++)
            {                       
                double min_val = 1000;
                int idx = 0;
                int i = 0;

                foreach (double ang in fo)
                {
                    if (ang < min_val && ang > ref_val)
                    {
                        min_val = ang;
                        idx = i;
                    }
                    i++;
                }
                ref_val = min_val;
                //order[idx] = j;                
                if (j != order.Count) str_order += (idx + 1).ToString() + "-";
                else str_order += (idx + 1).ToString();
            }
            Order_Val.Text += str_order;
        }

        private void WriteInputFile(string inpfile)
        {
            System.IO.StreamWriter writer;
            writer = System.IO.File.CreateText(_inpfile);

            // Domain
            string dom = "";
            dom += "DOMAIN\t1\t1\r\n";
            writer.Write(dom);
            dom = "";
            int cyl_num = int.Parse(NumCyl_Val.Text);
            int n = 0;
            string stroke_type = "";
            if (Stroke_Type_Val.Text == "2" || Stroke_Type_Val.Text == "2-stroke")
            {
                n = cyl_num;
                stroke_type = "2";
            }
            else if (Stroke_Type_Val.Text == "4" || Stroke_Type_Val.Text == "4-stroke")
            {
                n = cyl_num / 2;
                stroke_type = "4";
            }

            bool flag = false;
            for (int i = 0; i < n; i++)
            {
                if (i % 8 == 7)
                {
                    dom += "\t" + (i + 1).ToString() + "\r\n";
                    flag = false;
                }
                else
                {
                    dom += "\t" + (i + 1).ToString();
                    flag = true;
                }
                
            }
            writer.Write(dom);
            if (flag) writer.Write("\r\n");

            // Parameter
            string param = "";
            param += "PARAMETERS\t";
            param += "1\t";
            double rpm = double.Parse(RPM_Val.Text);
            if (CrankRot_Val.Text == "CCW") rpm = -rpm;
            param += rpm.ToString() + "\t";
            param += "1\t";
            param += Stroke_Type_Val.Text + "\r\n";
            writer.Write(param);

            // Pressure
            string press = "";
            press += "PRESSURE\t";
            press += "1\t";
            press += Press_Val.Text + "\r\n";

            // ##############################################################################           

            // MB
            string mb = "";
            List<double> total_mb_pos = new List<double>();

            for (int i = 0; i < n + 1; i++)
            {
                mb += "MB\t";
                mb += (i + 1).ToString() + "\t";
                mb += (double.Parse(MB_Val.Text) * i).ToString() + "\r\n";
                total_mb_pos.Add(double.Parse(MB_Val.Text) * i);
            }
            double del_mb = total_mb_pos[1];

            // Web
            string web = "";
            double fweb_pos = double.Parse(fWebPos_Val.Text);
            double web_dis = double.Parse(WebDis_Val.Text);
            List<double> total_web_pos = new List<double>();
            int m = n * 2;
            for (int i = 0; i < m; i++)
            {                
                total_web_pos.Add(del_mb * System.Math.Truncate(i * 0.5) + fweb_pos + web_dis * (i % 2));
            }
            for (int i = 0; i < m; i++)
            {
                web += "WEB\t";
                web += (i + 1).ToString() + "\t";
                web += total_web_pos[i].ToString() + "\t";
                web += WebMass_Val.Text + "\t";
                web += Stroke_Val.Text + "\t";
                web += WebCOG_Val.Text + "\r\n";
            }

            // CW
            string cw = "";
            for (int i = 0; i < _cw_list.Count; i++)
            {
                CW cw_obj = _cw_list[i] as CW;
                cw += "CW\t";
                cw += (i + 1).ToString() + "\t";
                cw += cw_obj.Position.ToString() + "\t";
                cw += cw_obj.Mass.ToString() + "\t";
                cw += cw_obj.COG.ToString() + "\t";
                cw += cw_obj.COG.ToString() + "\r\n";
            }

            // Piston
            string piston = "";
            double pmss = 0;
            double pbore = 0;
            if (Pmass_Val.Text != "") pmss = double.Parse(Pmass_Val.Text);
            if (Pbore_Val.Text != "") pbore = double.Parse(Pbore_Val.Text);
            for (int i = 0; i < cyl_num; i++)
            {
                piston += "PISTON\t";
                piston += (i + 1).ToString() + "\t";
                piston += pmss.ToString() + "\t";
                piston += pbore.ToString() + "\r\n";
            }

            // Conrod
            string conrod = "";
            double conmass = 0;
            double coniner = 0;
            double conlen = 0;
            double concog = 0;
            if (ConMass_Val.Text != "") conmass = double.Parse(ConMass_Val.Text);
            if (ConIner_Val.Text != "") coniner = double.Parse(ConIner_Val.Text);
            if (ConLen_Val.Text != "") conlen = double.Parse(ConLen_Val.Text);
            if (ConCOG_Val.Text != "") concog = double.Parse(ConCOG_Val.Text);
            for (int i = 0; i < cyl_num; i++)
            {
                conrod += "CONROD\t";
                conrod += (i + 1).ToString() + "\t";
                conrod += conmass.ToString() + "\t";
                conrod += coniner.ToString() + "\t";
                conrod += conlen.ToString() + "\t";
                conrod += concog.ToString() + "\r\n";
            }

            // Cylinder
            string cyl = "";
            double bank_ang = double.Parse(BankAng_Val.Text);
            double fcyl_pos = double.Parse(fCylPos_Val.Text);
            double cyl_dis = double.Parse(CylDis_Val.Text);
            List<double> total_cyl_pos = new List<double>();
            
            List<double> fo_ang = new List<double>();
            List<double> total_bank_ang = new List<double>();
            double new_bank_ang = 0;
            for (int i = 0; i < cyl_num; i++)
            {
                fo_ang.Add(0);
                if (i % 2 == 0) new_bank_ang = bank_ang;
                else if (i % 2 == 1) new_bank_ang = -bank_ang;
                total_bank_ang.Add(new_bank_ang);
            }

            string[] str_ord = Order_Val.Text.Split('/');
            string[] str_th = str_ord[0].Split('-');            
            string[] str_fo = str_ord[1].Split('-');
            List<double> total_throw_ang = new List<double>();

            string idx = "";
            for (int i = 0; i < str_fo.Length; i++)
            {
                idx = (System.Math.Truncate(i * 0.5)).ToString();
                total_throw_ang.Add(double.Parse(str_th[int.Parse(idx)]));
            }

            if (stroke_type == "2")
            {
                double step = 360 / n;
                if (CrankRot_Val.Text == "CCW") step = -step;
                int cidx = 0;
                for (int i = 0; i < str_fo.Length; i++)
                {
                    cidx = int.Parse(str_fo[i]) - 1;
                    fo_ang[cidx] = step * i;
                }
            }
            else if (stroke_type == "4")
            {
                double ang = 0;
                double bang = 0;
                double thang = 0;
                int cidx = 0;
                double rev = 0;
                for (int i = 0; i < str_fo.Length; i++)
                {
                    cidx = int.Parse(str_fo[i]) - 1;
                    bang = total_bank_ang[cidx];
                    thang = total_throw_ang[cidx];
                    if (i >= n) rev = 720;
                    else rev = 360;
                    if (thang != 0) ang = rev - thang + bang;
                    else ang = thang + bang;
                    if (CrankRot_Val.Text == "CCW")
                    {
                        if (ang > 0) ang -= 720;
                        if (ang <= -720) ang += 720;
                    }
                    else
                    {
                        if (ang < 0) ang += 720;
                        if (ang >= 720) ang -= 720;
                    }
                    fo_ang[cidx] = ang;
                }
            }

            if (stroke_type == "2")
            {
                for (int i = 0; i < cyl_num; i++)
                {
                    total_cyl_pos.Add(fcyl_pos + cyl_dis * i);
                }
            }
            else if (stroke_type == "4")
            {
                double j = 0;
                for (int i = 0; i < cyl_num; i++)
                {
                    j = System.Math.Truncate(i * 0.5);
                    if (i % 2 == 0)
                    {
                        total_cyl_pos.Add(del_mb * j + fcyl_pos);
                    }
                    else 
                    {
                        total_cyl_pos.Add(del_mb * j + fcyl_pos + cyl_dis);
                    }
                }
            }

            for (int i = 0; i < cyl_num; i++)
            {
                cyl += "CYLINDER\t";
                cyl += (i + 1).ToString() + "\t";
                if (i % 2 == 0) cyl += bank_ang.ToString() + "\t";
                else cyl += (-bank_ang).ToString()+ "\t";

                cyl += fo_ang[i].ToString() + "\t"; // firing angle
                cyl += total_cyl_pos[i].ToString() + "\t"; // axpos
                cyl += CrankMass_Val.Text + "\t";
                cyl += (i + 1).ToString() + "\t";
                cyl += (i + 1).ToString() + "\t";
                cyl += "1\r\n";
            }                       

            // Throw
            string th = "";
            double cw_tol = 10;
            for (int i = 0; i < n; i++)
            {
                th += "THROW\t";
                th += (i + 1).ToString() + "\t";

                th += str_th[i] + "\t";

                th += (i + 1).ToString() + "\t"; // MB1
                th += (i + 2).ToString() + "\t"; // MB2

                th += (2 * i + 1).ToString() + "\t"; // WEB1
                th += (2 * i + 2).ToString() + "\t"; // WEB2

                List<double> web12 = new List<double>();
                web12.Add(total_web_pos[2 * i]);
                web12.Add(total_web_pos[2 * i + 1]);
                double search_cw = 0;                
                for (int j = 0; j < 2; j++)
                {
                    int cw_id = 0;
                    for (int k = 0; k < _cw_list.Count; k++)
                    {
                        search_cw = System.Math.Abs(web12[j] - _cw_list[k].Position);
                        if (search_cw <= cw_tol)
                        {
                            cw_id = k + 1;
                            break;
                        }
                    }
                    if (j == 0)
                    {
                        th += cw_id.ToString() + "\t"; // CW1                        
                    }
                    else if (j == 1)
                    {                        
                        th += cw_id.ToString() + "\r\n"; // CW2
                    }
                }                

                if (stroke_type == "2")
                {
                    th += "\t" + (i + 1).ToString() + "\r\n"; // cyl1
                }
                else if (stroke_type == "4")
                {
                    th += "\t" + (2 * i + 1).ToString() + "\t"; // cyl1
                    th += (2 * i + 2).ToString() + "\r\n"; // cyl2
                }
            }

            // write order
            writer.Write(th);
            writer.Write(cyl);
            writer.Write(piston);
            writer.Write(conrod);
            writer.Write(mb);
            writer.Write(web);
            writer.Write(cw);
            writer.Write(press);
            writer.Close();
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sfdlg = new Microsoft.Win32.SaveFileDialog();
            sfdlg.InitialDirectory = "C:\\";
            sfdlg.Filter = "Enforce Input File|*.ne";
            if (_inpfile == "")
            {
                if (sfdlg.ShowDialog() == true)
                {
                    _inpfile = sfdlg.FileName;                    
                }
            }            

            if (_inpfile != "")
            {
                StatusMessagBox.Text += "[Save] " + _inpfile + "\r\n";
                WriteInputFile(_inpfile);
            }            
        }

        private void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sfdlg = new Microsoft.Win32.SaveFileDialog();
            sfdlg.InitialDirectory = "C:\\";
            sfdlg.Filter = "Enforce Input File|*.ne";

            if (sfdlg.ShowDialog() == true)
            {
                _inpfile = sfdlg.FileName;                
            }
            
            if (_inpfile != "")
            {
                StatusMessagBox.Text += "[Save As] " + _inpfile + "\r\n";
                WriteInputFile(_inpfile);
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenPressureButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofdlg = new Microsoft.Win32.OpenFileDialog();
            ofdlg.InitialDirectory = "C:\\";
            ofdlg.Filter = "Text File|*.txt";
            if (ofdlg.ShowDialog() == true)
            {
                _pfile = ofdlg.FileName;
                Press_Val.Text = _pfile;
            }            
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            if (_inpfile != "")
            {
                StatusMessagBox.Text += "[Save] " + _inpfile + "\r\n";
                WriteInputFile(_inpfile);
            }

            StatusMessagBox.Text += "[Run] ";            
            NE_ProgressBar.Value = 50;
            Delay(200);

            string sol_path = AppDomain.CurrentDomain.BaseDirectory + "Enforce_Solver.exe";
            string sol_arg = " -i " + _inpfile;
            StatusMessagBox.Text += sol_path + sol_arg + "\r\n";

            System.Diagnostics.ProcessStartInfo pri = new System.Diagnostics.ProcessStartInfo();
            System.Diagnostics.Process pro = new System.Diagnostics.Process();
            pri.FileName = sol_path;
            pri.Arguments = sol_arg;
            pri.CreateNoWindow = true;
            pro.StartInfo = pri;
            pro.Start();

            NE_ProgressBar.Value = 100;
            Delay(300);
            NE_ProgressBar.Value = 0;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {           
            _cw_list.Add(new CW() { No = _cw_no, Position = 0, Mass = 0, COG = 0 });
            CWDataGrid.Items.Refresh();
            _cw_no++;
            
        }

        private void RemoveAll_CW_Data()
        {
            _cw_list.Clear();       
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            int n = CWDataGrid.SelectedItems.Count;            
            if (n > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    CW scw = CWDataGrid.SelectedItems[i] as CW;
                    int k = scw.No;
                    int ii = 0;
                    foreach (CW cw in _cw_list)
                    {
                        if (cw.No == k)
                        {
                            _cw_list.RemoveAt(ii);
                            break;
                        }
                        ii++;
                    }
                }
            }

            _cw_no = 1;
            for (int i = 0; i < _cw_list.Count; i++)
            {
                _cw_list[i].No = _cw_no;                
                _cw_no++;
            }
            CWDataGrid.Items.Refresh();
        }

        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                if (System.Windows.Application.Current != null)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new Action(delegate { }));
                }
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

    }
    
    public class CW
    {        
        public void InitializeComponent()
        {
            No = 0;
            Position = 0;
            Mass = 0;
            COG = 0;
        }

        public int No
        {
            get;
            set;
        }

        public double Position
        {
            get;
            set;
        }

        public double Mass
        {
            get;
            set;
        }

        public double COG
        {
            get;
            set;
        }

    }
}
