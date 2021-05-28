using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace TestWPFApp
{
    public partial class MainWindow : Window
    {
        private string AssemblyDllPath;
        public List<string> ClassAndMethodsList;
        public List<string> FileList;
        public MainWindow()
        {
            ClassAndMethodsList = new List<string>();
            FileList = new List<string>();
            InitializeComponent();
        }

        private void SearchClassButton_Click(object sender, RoutedEventArgs e)
        {
            FindDllFile();
            FindClassAndMethods();
            ClassListBox.ItemsSource = ClassAndMethodsList;
        }
        private void FindClassAndMethods()
        {
            Assembly FirstAssembly;
            foreach (var file in FileList)
            {
                FirstAssembly = Assembly.LoadFrom(file);

                foreach (Type oType in FirstAssembly.GetTypes())
                {
                    ClassAndMethodsList.Add(oType.Name.ToString());

                    foreach (MethodInfo members in oType.GetMethods())
                    {
                        if (members.IsFamilyOrAssembly || members.IsPublic)
                        {
                            ClassAndMethodsList.Add("    " + members.Name);
                        }
                    }
                }
            }
        }
        private void FindDllFile()
        {
            AssemblyDllPath = DllPath.Text.ToString();
            DirectoryInfo directoryInfo = new DirectoryInfo(AssemblyDllPath);
            foreach (var file in directoryInfo.GetFiles())
            {
                if (System.IO.Path.GetExtension(file.FullName) == ".dll")
                {
                    FileList.Add(file.FullName);
                }
            }
        }

    }
}
