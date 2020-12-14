using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;
using System.Net.NetworkInformation;
using System.Net;

namespace SolrSetup
{
    public partial class Form1 : Form
    {
        private string jre;
        private object tomcatpath;
        private string solrpath;
        public Form1()
        {
            InitializeComponent();

            toolTip1.BackColor = Color.Red;
            toolTip1.SetToolTip(Install, "Beware: This will uninstall your current Java.");
            
            OCguide();

        }

        private void OCguide()
            {
            int length = richTextBox1.Text.Length;
            string MyLines1 = @"Welcome to the SolrSetup AddOn!
This addOn will help you create a local instance of SolrWayBack and begin indexing files locally!
";
            length = richTextBox1.Text.Length;
            richTextBox1.AppendText((((Convert.ToString(MyLines1)) + "\r") + "\n"));
            length = richTextBox1.Text.Length;
            string strguide = "Installation of local instance of SolrWayback ";
            richTextBox1.AppendText((((Convert.ToString(strguide)) + "\r") + "\n"));
            richTextBox1.Select(length, strguide.Length);
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);

            string MyLines2 = "In order setup your local instance of SolrWayback this will install JAVA, if you don't have it already ";
            length = richTextBox1.Text.Length + MyLines2.Length;
            richTextBox1.AppendText((((Convert.ToString(MyLines2)) + "\r") + "\n"));

            string beaware = "The program will also change some envriomental variables on windows. ";
            richTextBox1.AppendText((((Convert.ToString(beaware)) + "\r") + "\n"));
            richTextBox1.Select(length, beaware.Length);
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);

            string MyLines3 = @"Press the 'Install' button.
";
            length = richTextBox1.Text.Length + MyLines3.Length;
            richTextBox1.AppendText((((Convert.ToString(MyLines3)) + "\r") + "\n"));
            length = richTextBox1.Text.Length;
            string strguide1 = "Setup up your local instance of SolrWayback ";
            richTextBox1.AppendText((((Convert.ToString(strguide1)) + "\r") + "\n"));
            richTextBox1.Select(length, strguide1.Length);
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);

            string MyLines4 = @"In order to setup your local instance of SolrWayback start by pressing the On-Icon.
This will open op two cmd prompts which has to remain open in order for SolrWayback to function.
When you are done using SolrWayBack, you need to press close this application.
You CANNOT index files without having your local instance running.
To view your local instance press “View Solr” which will open a browser window with Solrwayback and NetArchive as a localhost.
";
            length = richTextBox1.Text.Length + MyLines4.Length;
            richTextBox1.AppendText((((Convert.ToString(MyLines4)) + "\r") + "\n"));
            length = richTextBox1.Text.Length;

            string strguide2 = "Indexing WARC files ";
            richTextBox1.AppendText((((Convert.ToString(strguide2)) + "\r") + "\n"));
            richTextBox1.Select(length, strguide2.Length);
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);

            string MyLines5 = @"In order to index a WARC(s) file press “Index Files” and select your file(s). A cmd prompt will now index your files and when the index is done, SolrSetup will automatically restart your local instance for you to view your indexed files. 
*in order to index WARC files your local instance must be running…Meaning you must have pressed “Start SolrWayBack*
If you want to remove your indexed files and start from scratch, press “Clear indexed files” and your indexed files will be removed and SolrWayback will restart. 
";
            richTextBox1.AppendText((((Convert.ToString(MyLines5)) + "\r") + "\n"));
        }

        private void Install_Click(object sender, EventArgs e)
        {
            //Applying waiting curser when clicked
            Cursor.Current = Cursors.WaitCursor;
            //Get current path/location of executed SolrSetup
            var CurrentDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = CurrentDirectory;

            string fileToCopy = Path.GetFullPath(Path.Combine(path, @"../../properties/solrwayback.properties"));
            string destinationDirectory = System.Environment.GetEnvironmentVariable("USERPROFILE");
            //Copying .properties files into userprofile folder
            File.Copy(fileToCopy, destinationDirectory + "/" + Path.GetFileName(fileToCopy), true);
            string fileToCopy1 = Path.GetFullPath(Path.Combine(path, @"../../properties/solrwaybackweb.properties"));
            string destinationDirectory1 = System.Environment.GetEnvironmentVariable("USERPROFILE"); ;
            File.Copy(fileToCopy1, destinationDirectory1 + "/" + Path.GetFileName(fileToCopy1), true);

            //Preparing process to run cmd commands
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "cmd.exe";


            //SETS JAVA_HOME & JRE_HOME ENVIRONMENTVARIALBE
            try

            {
                //FINDS JAVA FOLDER
                string[] dirs1 = Directory.GetDirectories(@"C:/Program Files/Java/", "jre*");
                foreach (string dir in dirs1)
                {
                    Console.WriteLine(dir);
                    jre = dir;
                }
                jre.ToString();

                //FINDS TOMCAT FOLDER
                string newPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\"));
                string[] dirs2 = Directory.GetDirectories(newPath, "apache-tomcat-*");
                foreach (string dir in dirs2)
                {
                    tomcatpath = dir;
                }
                tomcatpath.ToString();

                string setx1 = "setx JAVA_HOME \"" + jre + "\"";
                string setx2 = "setx JRE_HOME \"" + jre + "\"";
                string setx3 = "setx CATALINA_HOME \"" + tomcatpath + '"';
                startInfo.Arguments = "/C start " + setx1 + "&" + setx2 + "&" + setx3;
                process.StartInfo = startInfo;
                process.Start();
                System.Windows.Forms.MessageBox.Show("SolrSetup successfully installed");
                Application.Exit();
            }
            catch (Exception)
            {
                //DUE TO JAVA NOT INSTALLED - GUIDES USER TO JAVA.COM
                System.Windows.Forms.MessageBox.Show("You dont have Java installed - Please do");
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C start https://www.java.com/en/download/";
                process.StartInfo = startInfo;
                process.Start();
                Application.Exit();

            }

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            //Applying waiting curser when clicked
            Cursor.Current = Cursors.WaitCursor;

            //Preparing process to run cmd commands
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";

            //FINDS TOMCAT FOLDER
            string newPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\"));
            string[] dirs2 = Directory.GetDirectories(newPath, "apache-tomcat-*");
            foreach (string dir in dirs2)
            {
                tomcatpath = dir;
            }
            tomcatpath.ToString();

            //FINDS SOLR FOLDER
            string newPath2 = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\"));
            string[] dirs3 = Directory.GetDirectories(newPath2, "solr-*");
            foreach (string dir in dirs3)
            {
                solrpath = dir;
            }
            solrpath.ToString();

            //SETS CATALINA ENVIRONMETVARIABLE
            string setx3 = "setx CATALINA_HOME \"" + tomcatpath + '"';
            startInfo.Arguments = "/C start " + setx3;
            process.StartInfo = startInfo;
            process.Start();

            //STARTS SOLRWAYBACK/NETARCHIVE
            string solrstartpath = solrpath + @"\bin\solr.cmd";
            solrstartpath.ToString();
            startInfo.Arguments = "/C " + "\"" + solrstartpath + "\"" + " start";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            //STARTS TOMCAT
            string path1 = tomcatpath + @"\bin\Startup.bat";
            path1.ToString();
            startInfo.Arguments = "/C call " + "\"" + path1 + "\"";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }



        private void Index_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //Preparing process to run cmd commands
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.FileName = "cmd.exe";
            /* Running indexing.bat with the following code:
            //This is a modifycation of the original batch_warc_folder.bat withing the "indexing" folder.
            //The changes makes it possible for the user the choose the warc files they want to index - instead of having them placed in a specific folder

            <# : chooser.bat
            :: launches a File... Open sort of file chooser and outputs choice(s) to the console
            :: https://stackoverflow.com/a/15885133/1683264

            cd %~dp0\..\indexing

            @echo off
            setlocal

            for /f "delims=" %%I in ('powershell -noprofile "iex (${%~f0} | out-string)"') do java -Xmx2048M -Djava.io.tmpdir=tika_tmp -jar warc-indexer-3.0.1-SNAPSHOT-jar-with-dependencies.jar -c config3.conf -s  "http://localhost:8983/solr/netarchivebuilder"  "%%I"

            goto :EOF

            : end Batch portion / begin PowerShell hybrid chimera #>

            Add-Type -AssemblyName System.Windows.Forms
            $f = new-object Windows.Forms.OpenFileDialog
            $f.InitialDirectory = pwd
            $f.Filter = "WARC Files (*.warc)|*.warc|All Files (*.*)|*.*"
            $f.ShowHelp = $true
            $f.Multiselect = $true
            [void]$f.ShowDialog()
            if ($f.Multiselect) { $f.FileNames } else { $f.FileName 
             */
            string INDEX = "Indexing.bat";
            startInfo.Arguments = "/c " + INDEX;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            //STOPS PORT LISTENING
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.Arguments = "/c netstat -ano | findstr 0.0.0.0.8983";
            startInfo.FileName = "cmd.exe";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            process.StartInfo = startInfo;
            process.Start();

            //SPLITS OUTPUT AND KILLS LISTENER TASK
            string output = process.StandardOutput.ReadToEnd();
            string last = output.Split(' ').LastOrDefault();
            startInfo.Arguments = "/c TaskKill.exe /f /PID " + last;
            startInfo.FileName = "cmd.exe";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            //FINDS TOMCAT FOLDER
            string newPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\"));
            string[] dirs2 = Directory.GetDirectories(newPath, "apache-tomcat-*");
            foreach (string dir in dirs2)
            {
                tomcatpath = dir;
            }
            tomcatpath.ToString();
            //FINDS SOLR FOLDER
            string newPath2 = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\"));
            string[] dirs3 = Directory.GetDirectories(newPath2, "solr-*");
            foreach (string dir in dirs3)
            {
                solrpath = dir;
            }
            solrpath.ToString();

            //SHUTSDOWN TOMCAT
            string tomcatshutdown = tomcatpath + @"\bin\Shutdown.bat";
            tomcatshutdown.ToString();
            startInfo.Arguments = "/C call " + "\"" + tomcatshutdown + "\"";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            //STARTS SOLRWAYBACK & NETARCHIVE
            string solrstartpath = solrpath + @"\bin\solr.cmd";
            solrstartpath.ToString();
            startInfo.Arguments = "/C " + "\"" + solrstartpath + "\"" + " start";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            //string path1 = Path.GetFullPath(Path.Combine(path, @"..\..\apache-tomcat-8.5.60\bin\Startup.bat"));
            //STARTS TOMCAT
            string tomcatstartup = tomcatpath + @"\bin\Startup.bat";
            tomcatstartup.ToString();
            startInfo.Arguments = "/C call " + "\"" + tomcatstartup + "\"";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            //Clearing the index folder that contains all previouse indexed files
            //To lower the risk of miss clicks and deleting all indexed files by mistake - this safty popup message has been made
            DialogResult dialogResult = MessageBox.Show("Are you sure you what to delete all indexed files?", "Clearing data", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                //KILLS SOLR LISTENER
                startInfo.Arguments = "/c netstat -ano | findstr 0.0.0.0.8983";
                startInfo.FileName = "cmd.exe";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;

                process.StartInfo = startInfo;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string last = output.Split(' ').LastOrDefault();
                startInfo.Arguments = "/c TaskKill.exe /f /PID " + last;
                startInfo.FileName = "cmd.exe";
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

                //FINDS TOMCAT FOLDER
                string newPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\"));
                string[] dirs2 = Directory.GetDirectories(newPath, "apache-tomcat-*");
                foreach (string dir in dirs2)
                {
                    tomcatpath = dir;
                }
                tomcatpath.ToString();

                //SHUTSDOWN TOMCAT
                string tomcatshutdown = tomcatpath + @"\bin\Shutdown.bat";
                tomcatshutdown.ToString();
                startInfo.Arguments = "/C call " + "\"" + tomcatshutdown + "\"";
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

                //FINDS SOLR FOLDER
                string newPath2 = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\"));
                string[] dirs3 = Directory.GetDirectories(newPath2, "solr-*");
                foreach (string dir in dirs3)
                {
                    solrpath = dir;
                }
                solrpath.ToString();

                //Removing the directory of folder containing the previous indexed files
                string CLEARINDEX = solrpath + @"\server\solr\configsets\netarchivebuilder\netarchivebuilder_data\";
                CLEARINDEX.ToString();
                startInfo.Arguments = "/c  rmdir /s /q " + "\"" + CLEARINDEX + "\"";
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

                // STARTS SOLR/NETARCHIVE

                string solrstartpath = solrpath + @"\bin\solr.cmd";
                solrstartpath.ToString();
                startInfo.Arguments = "/C " + "\"" + solrstartpath + "\"" + " start";
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

                //STARTS TOMCAT
                string tomcatstartup = tomcatpath + @"\bin\Startup.bat";
                tomcatstartup.ToString();
                startInfo.Arguments = "/C call " + "\"" + tomcatstartup + "\"";
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

            }
            //If user regrets pressing "clear indexing" nothing happens and the user do not delete anything
            else if (dialogResult == DialogResult.No)
            {
                //DOES NOTHING - RETURNING TO
            }
        }

            private void EXIT_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //Preparing process to run cmd commands
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            //STOPS PORT LISTENING
            startInfo.Arguments = "/c netstat -ano | findstr 0.0.0.0.8983";
            startInfo.FileName = "cmd.exe";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
            string last = output.Split(' ').LastOrDefault();
            Console.WriteLine(last);
            startInfo.Arguments = "/c TaskKill.exe /f /PID " + last;
            startInfo.FileName = "cmd.exe";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            //FINDS TOMCAT FOLDER
            string newPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\"));
            string[] dirs2 = Directory.GetDirectories(newPath, "apache-tomcat-*");
            foreach (string dir in dirs2)
            {
                tomcatpath = dir;
            }


            //SHUTSDOWN TOMCAT
            string tomcatshutdown = tomcatpath + @"\bin\Shutdown.bat";
            tomcatshutdown.ToString();
            startInfo.Arguments = "/C call " + "\"" + tomcatshutdown + "\"";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            Application.Exit();


        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            toolTip1.BackColor = Color.Red;

        }

        private int GuideCounter = 0;

        private void TheGuider() 
            {
            
            if (GuideCounter == 1)
            {
                richTextBox1.Text = "";

                int length;
                length = richTextBox1.Text.Length;

                string strguide = "Installer";
                richTextBox1.AppendText((((Convert.ToString(strguide)) + "\r") + "\n"));
                richTextBox1.Select(length, strguide.Length);
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                string InstallerLines = "This button will setup a local version of SolrWayBack on your computer.";
                richTextBox1.AppendText((((Convert.ToString(InstallerLines)) + "\r") + "\n"));
                length = richTextBox1.Text.Length;
                string strguide1 = "BE AWARE: If you move this folder, you might need to press 'Install' again.";
                length = richTextBox1.Text.Length;
                richTextBox1.AppendText((((Convert.ToString(strguide1)) + "\r") + "\n"));
                richTextBox1.Select(length, strguide1.Length);
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);

            }

            else if (GuideCounter == 2)
            {
                richTextBox1.Text = "";

                int length;
                length = richTextBox1.Text.Length;

                string strguide = "View SolrwayBack";
                richTextBox1.AppendText((((Convert.ToString(strguide)) + "\r") + "\n"));
                richTextBox1.Select(length, strguide.Length);
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                string InstallerLines = "This button will open up your local version of SolrWayBack and NetArchive.";
                richTextBox1.AppendText((((Convert.ToString(InstallerLines)) + "\r") + "\n"));
                length = richTextBox1.Text.Length;
                string strguide1 = "BE AWARE: You cannot open SolrwayBack if the local servers aren't running running. ";
                length = richTextBox1.Text.Length;
                richTextBox1.AppendText((((Convert.ToString(strguide1)) + "\r") + "\n"));
                richTextBox1.Select(length, strguide1.Length);
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);

            }
            else if (GuideCounter == 3)
            {
                richTextBox1.Text = "";

                int length;
                length = richTextBox1.Text.Length;

                string strguide = "Index WARC files";
                richTextBox1.AppendText((((Convert.ToString(strguide)) + "\r") + "\n"));
                richTextBox1.Select(length, strguide.Length);
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                string InstallerLines = "This button will let you index WARC-files.";
                richTextBox1.AppendText((((Convert.ToString(InstallerLines)) + "\r") + "\n"));
                length = richTextBox1.Text.Length;
                string strguide1 = "BE AWARE: You cannot index files if the local servers aren't running running. ";
                length = richTextBox1.Text.Length;
                richTextBox1.AppendText((((Convert.ToString(strguide1)) + "\r") + "\n"));
                richTextBox1.Select(length, strguide1.Length);
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            }
            else if (GuideCounter == 4)
            {
                richTextBox1.Text = "";

                int length;
                length = richTextBox1.Text.Length;

                string strguide = "CLEAR Indexed files";
                richTextBox1.AppendText((((Convert.ToString(strguide)) + "\r") + "\n"));
                richTextBox1.Select(length, strguide.Length);
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                string InstallerLines = "This button will clear all indexed files from your local version of SolrwayBack";
                richTextBox1.AppendText((((Convert.ToString(InstallerLines)) + "\r") + "\n"));
                length = richTextBox1.Text.Length;
                string strguide1 = "BE AWARE: This will clear ALL previously indexed file on this instance of SolrwayBack ";
                length = richTextBox1.Text.Length;
                richTextBox1.AppendText((((Convert.ToString(strguide1)) + "\r") + "\n"));
                richTextBox1.Select(length, strguide1.Length);
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            }
            else if (GuideCounter == 5)
            {
                richTextBox1.Text = "";

                int length;
                length = richTextBox1.Text.Length;

                string strguide = "Open Servers";
                richTextBox1.AppendText((((Convert.ToString(strguide)) + "\r") + "\n"));
                richTextBox1.Select(length, strguide.Length);
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                string InstallerLines = "This button will will start the servers and allow you to use SolrwayBack, Index files and clear your indexed files.";
                richTextBox1.AppendText((((Convert.ToString(InstallerLines)) + "\r") + "\n"));
                length = richTextBox1.Text.Length;
                string strguide1 = "BE AWARE: Do not close the servers that pop up, unless you're done using SolrwayBack";
                length = richTextBox1.Text.Length;
                richTextBox1.AppendText((((Convert.ToString(strguide1)) + "\r") + "\n"));
                richTextBox1.Select(length, strguide1.Length);
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            }


            else if (GuideCounter == 6) 
            {
                richTextBox1.Text = "";
                OCguide();
            }

            else if (GuideCounter == 7)
            {
                richTextBox1.Text = "";

                int length;
                length = richTextBox1.Text.Length;

                string strguide = "CLOSE PROGRAM";
                richTextBox1.AppendText((((Convert.ToString(strguide)) + "\r") + "\n"));
                richTextBox1.Select(length, strguide.Length);
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                string InstallerLines = "By closing this program, you will also close the servers used when running SolrwayBack";
                richTextBox1.AppendText((((Convert.ToString(InstallerLines)) + "\r") + "\n"));
                length = richTextBox1.Text.Length;
                string strguide1 = "BE AWARE: You can NOT use SolrWayback, if this application is closed.";
                length = richTextBox1.Text.Length;
                richTextBox1.AppendText((((Convert.ToString(strguide1)) + "\r") + "\n"));
                richTextBox1.Select(length, strguide1.Length);
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            }


        }

        private void Install_MouseHover(object sender, EventArgs e)
        {
            GuideCounter = 1;
            TheGuider();
        }

        private void Form1_MouseHover(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            GuideCounter = 6;
            TheGuider();
        }

        private void Solr_MouseHover(object sender, EventArgs e)
        {
            GuideCounter = 2;
            TheGuider();
        }

        private void Index_MouseHover(object sender, EventArgs e)
        {
            GuideCounter = 3;
            TheGuider();
        }

        private void Clear_MouseHover(object sender, EventArgs e)
        {
            GuideCounter = 4;
            TheGuider();
        }

        private void guna2CircleButton1_MouseHover(object sender, EventArgs e)
        {
            GuideCounter = 5;
            TheGuider();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            GuideCounter = 6;
            TheGuider();
        }

        private void Solr_Click(object sender, EventArgs e)
        {
            //Opens browser localhost of Solrwayback & Netarchives
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C start http://localhost:8080/solrwayback";
            process.StartInfo = startInfo;
            process.Start();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C start http://localhost:8983/solr/#/netarchivebuilder";
            process.StartInfo = startInfo;
            process.Start();
        }

        public string AddQuotesIfRequired(string path)
        {
            return !string.IsNullOrWhiteSpace(path) ?
                path.Contains(" ") && (!path.StartsWith("\"") && !path.EndsWith("\"")) ?
                    "\"" + path + "\"" : path :
                    string.Empty;
        }

        private void EXIT_MouseHover(object sender, EventArgs e)
        {
            GuideCounter = 7;
            TheGuider();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            GuideCounter = 6;
            TheGuider();
        }
    }
}
