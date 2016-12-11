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
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// OnOff.xaml 的交互逻辑
    /// </summary>
    public partial class OnOff : Window
    {
        public OnOff()
        {
            InitializeComponent();
            SetData();
        }

        private void SetData()
        {
            List<AAA> list = new List<AAA>();
            list.Add(new AAA("饮水机1","办公室1","关闭", "开启"));
            list.Add(new AAA("饮水机2", "办公室2", "关闭", "开启"));
            list.Add(new AAA("饮水机3", "办公室3", "关闭", "开启"));
            list.Add(new AAA("饮水机4", "办公室4", "关闭", "开启"));
            list.Add(new AAA("饮水机5", "办公室5", "关闭", "开启"));
            list.Add(new AAA("饮水机6", "办公室6", "关闭", "开启"));
            list.Add(new AAA("饮水机7", "办公室7", "关闭", "开启"));
            list.Add(new AAA("饮水机8", "办公室8", "开启", "关闭"));
            dataGrid.ItemsSource = list;
        }
        private class AAA
        {
            public AAA(string name,string adderss,string state,string doo)
            {
                名称 = name;
                安装位置 = adderss;
                状态 = state;
                操作 = doo;
            }
            public string 名称 { get; set; }
            public string 安装位置 { get; set; }
            public string 状态 { get; set; }
            public string 操作 { get; set; }
        }
    }
}
