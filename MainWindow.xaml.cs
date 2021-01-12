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
            
            Stroke_Val.Text = "";
            CrankRot_Val.Text = "";
            RPM_Val.Text = "";
            NumCyl_Val.Text = "";
            FO_Val.Text = "";
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

            Stroke_Val.Text = "";
            CrankRot_Val.Text = "";
            RPM_Val.Text = "";
            NumCyl_Val.Text = "";
            FO_Val.Text = "";
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
                        }
                        else
                        {
                            CrankRot_Val.Text = "CCW";
                        }
                        Stroke_Val.Text = sp_text_value[4];
                    }
                    else if (sp_text_value[0] == "CYLINDER")
                    {
                        if (Stroke_Val.Text == "4" || Stroke_Val.Text == "4-stroke")
                        {
                            if (int.Parse(sp_text_value[1]) == 1)
                            {
                                fCylPos_Val.Text = sp_text_value[4];
                                cyl_dis1 = double.Parse(sp_text_value[4]);
                            }
                            else if (int.Parse(sp_text_value[1]) == 2) cyl_dis2 = double.Parse(sp_text_value[4]);                            
                            cyl_dis = Math.Abs(cyl_dis1 - cyl_dis2);
                        }
                        else if (Stroke_Val.Text == "2" || Stroke_Val.Text == "2-stroke")
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
                        
                    }
                    else
                    {

                    }
                }

                NumCyl_Val.Text = _cyl_no.ToString();
                CylDis_Val.Text = cyl_dis.ToString();
                MB_Val.Text = mb_dis.ToString();
                WebDis_Val.Text = web_dis.ToString();
                calculate_firing_order1(fo);
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
                if (j != order.Count) str_order += (idx + 1).ToString() + " - ";
                else str_order += (idx + 1).ToString();
            }
            FO_Val.Text = str_order;
        }

        private void WriteInputFile(string inpfile)
        {
            System.IO.StreamWriter writer;
            writer = System.IO.File.CreateText(_inpfile);

            // Domain
            string dom = "";
            dom += "Domain\t1\t1\r\n";
            writer.Write(dom);
            dom = "";
            int cyl_num = int.Parse(NumCyl_Val.Text);
            int n = 0;
            if (Stroke_Val.Text == "2" || Stroke_Val.Text == "2-stroke")
            {
                n = cyl_num;
            }
            else if (Stroke_Val.Text == "4" || Stroke_Val.Text == "4-stroke")
            {
                n = cyl_num / 2;
            }

            for (int i = 0; i < n; i++)
            {
                if (i % 8 == 7)
                {
                    dom += "\t" + (i + 1).ToString() + "\r\n";
                }
                else
                {
                    dom += "\t" + (i + 1).ToString();
                }
                
            }
            writer.Write(dom);

            // Parameter
            string param = "";
            param += "PARAMETERS\t";
            param += "1\t";
            double rpm = double.Parse(RPM_Val.Text);
            if (CrankRot_Val.Text == "CCW") rpm = -rpm;
            param += rpm.ToString() + "\t";
            param += "1\t";
            param += Stroke_Val.Text + "\r\n";
            writer.Write(param);

            // Throw

            // Cylinder

            // Piston

            // Conrod

            // MB


            // Web

            // CW
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
            StatusMessagBox.Text += "Run\r\n";            
            NE_ProgressBar.Value = 50;
            Delay(200);


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
            while (CWDataGrid.Items.Count > 1)
            {
                CWDataGrid.Items.Remove(CWDataGrid.Items[CWDataGrid.Items.Count-1]);
            }        
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
