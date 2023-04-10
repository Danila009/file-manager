using System;
using System.Diagnostics;
using System.Management;
using System.Windows;

namespace FileManagerWPF
{
    /// <summary>
    /// Interaction logic for SystemInfoWindow.xaml
    /// </summary>
    public partial class SystemInfoWindow : Window
    {
        public SystemInfoWindow()
        {
            InitializeComponent();
            PercentProcessorTime();

            string osVersion = Environment.OSVersion.VersionString;

            // Определение текущего процесса
            Process currentProcess = Process.GetCurrentProcess();

            // Определение приоритета текущего процесса
            ProcessPriorityClass processPriority = currentProcess.PriorityClass;

            operatingSystemVersion.Content = $"Версия операционной системы: {osVersion}";

            priorityCurrentProcess.Content = $"Приоритет текущего процесса: {processPriority}";

            int threadCount = Environment.ProcessorCount;
            countThreads.Content = $"Количество потоков: {threadCount}";
        }

        private void Button_UpdatePercentProcessorTimeClick(object sender, EventArgs e)
        {
            PercentProcessorTime();
        }

        private void PercentProcessorTime()
        {
            percentProcessorTime.Content = String.Empty;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                var usage = obj["PercentProcessorTime"];
                var name = obj["Name"];
                percentProcessorTime.Content += "\n" + "Name-" + name + " PercentProcessorTime-" + usage;
            }
        }
    }
}
