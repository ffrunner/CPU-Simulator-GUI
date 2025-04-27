using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuSchedulingWinForms
{
    public static class Algorithms
    {
        public static void fcfsAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int npX2 = np * 2;

            double[] bp = new double[np];
            double[] wtp = new double[np];
            string[] output1 = new string[npX2];
            double twt = 0.0, awt; 
            int num;

            DialogResult result = MessageBox.Show("First Come First Serve Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    //MessageBox.Show("Enter Burst time for P" + (num + 1) + ":", "Burst time for Process", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    //Console.WriteLine("\nEnter Burst time for P" + (num + 1) + ":");

                    string input =
                    Microsoft.VisualBasic.Interaction.InputBox("Enter Burst time: ",
                                                       "Burst time for P" + (num + 1),
                                                       "",
                                                       -1, -1);

                    bp[num] = Convert.ToInt64(input);

                    //var input = Console.ReadLine();
                    //bp[num] = Convert.ToInt32(input);
                }

                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        wtp[num] = 0;
                    }
                    else
                    {
                        wtp[num] = wtp[num - 1] + bp[num - 1];
                        MessageBox.Show("Waiting time for P" + (num + 1) + " = " + wtp[num], "Job Queue", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                awt = twt / np;
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + awt + " sec(s)", "Average Awaiting Time", MessageBoxButtons.OK, MessageBoxIcon.None);

                double[] tat = new double[np];
                for (int i = 0; i < np; i++)
                {
                    tat[i] = wtp[i] + bp[i]; // Turnaround = Waiting + Burst
                }
                double totalTurnaroundTime = 0;
                for (int i = 0; i < np; i++)
                {
                    totalTurnaroundTime += tat[i];
                }
                double att = totalTurnaroundTime / np;
                MessageBox.Show("Average Turnaround Time = " + att.ToString("F2") + " sec(s)", "ATT", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Total Time to complete all processes (for throughput and CPU utilization)
                double totalTime = tat[np - 1]; // Completion time of last process

                // Throughput (Processes per second)
                double throughput = np / totalTime;
                MessageBox.Show("Throughput = " + throughput.ToString("F2") + " processes/sec", "Throughput", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // CPU Utilization
                double totalBurstTime = bp.Sum(); // sum of burst times
                double cpuUtilization = (totalBurstTime / totalTime) * 100;
                MessageBox.Show("CPU Utilization = " + cpuUtilization.ToString("F2") + " %", "CPU Utilization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (result == DialogResult.No)
            {
                //this.Hide();
                //Form1 frm = new Form1();
                //frm.ShowDialog();
            }
        }

        public static void sjfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);

            double[] bp = new double[np];
            double[] wtp = new double[np];
            double[] p = new double[np];
            double twt = 0.0, awt; 
            int x, num;
            double temp = 0.0;
            bool found = false;

            DialogResult result = MessageBox.Show("Shortest Job First Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    bp[num] = Convert.ToInt64(input);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    p[num] = bp[num];
                }
                for (x = 0; x <= np - 2; x++)
                {
                    for (num = 0; num <= np - 2; num++)
                    {
                        if (p[num] > p[num + 1])
                        {
                            temp = p[num];
                            p[num] = p[num + 1];
                            p[num + 1] = temp;
                        }
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = 0;
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time:", MessageBoxButtons.OK, MessageBoxIcon.None);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = wtp[num - 1] + p[num - 1];
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK, MessageBoxIcon.None);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + (awt = twt / np) + " sec(s)", "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);

                double[] tat = new double[np];
                for (int i = 0; i < np; i++)
                {
                    tat[i] = wtp[i] + bp[i]; // Turnaround = Waiting + Burst
                }
                double totalTurnaroundTime = 0;
                for (int i = 0; i < np; i++)
                {
                    totalTurnaroundTime += tat[i];
                }
                double att = totalTurnaroundTime / np;
                MessageBox.Show("Average Turnaround Time = " + att.ToString("F2") + " sec(s)", "ATT", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Total Time to complete all processes (for throughput and CPU utilization)
                double totalTime = tat[np - 1]; // Completion time of last process

                // Throughput (Processes per second)
                double throughput = np / totalTime;
                MessageBox.Show("Throughput = " + throughput.ToString("F2") + " processes/sec", "Throughput", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // CPU Utilization
                double totalBurstTime = bp.Sum(); // sum of burst times
                double cpuUtilization = (totalBurstTime / totalTime) * 100;
                MessageBox.Show("CPU Utilization = " + cpuUtilization.ToString("F2") + " %", "CPU Utilization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void priorityAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Priority Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] bp = new double[np];
                double[] wtp = new double[np + 1];
                int[] p = new int[np];
                int[] sp = new int[np];
                int x, num;
                double twt = 0.0;
                double awt;
                int temp = 0;
                bool found = false;
                for (num = 0; num <= np - 1; num++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    bp[num] = Convert.ToInt64(input);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    string input2 =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter priority: ",
                                                           "Priority for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    p[num] = Convert.ToInt16(input2);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    sp[num] = p[num];
                }
                for (x = 0; x <= np - 2; x++)
                {
                    for (num = 0; num <= np - 2; num++)
                    {
                        if (sp[num] > sp[num + 1])
                        {
                            temp = sp[num];
                            sp[num] = sp[num + 1];
                            sp[num + 1] = temp;
                        }
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (sp[num] == p[x] && found == false)
                            {
                                wtp[num] = 0;
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                temp = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (sp[num] == p[x] && found == false)
                            {
                                wtp[num] = wtp[num - 1] + bp[temp];
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                temp = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + (awt = twt / np) + " sec(s)", "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Console.WriteLine("\n\nAverage waiting time: " + (awt = twt / np));
                //Console.ReadLine();

                double[] tat = new double[np];
                for (int i = 0; i < np; i++)
                {
                    tat[i] = wtp[i] + bp[i]; // Turnaround = Waiting + Burst
                }
                double totalTurnaroundTime = 0;
                for (int i = 0; i < np; i++)
                {
                    totalTurnaroundTime += tat[i];
                }
                double att = totalTurnaroundTime / np;
                MessageBox.Show("Average Turnaround Time = " + att.ToString("F2") + " sec(s)", "ATT", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Total Time to complete all processes (for throughput and CPU utilization)
                double totalTime = tat[np - 1]; // Completion time of last process

                // Throughput (Processes per second)
                double throughput = np / totalTime;
                MessageBox.Show("Throughput = " + throughput.ToString("F2") + " processes/sec", "Throughput", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // CPU Utilization
                double totalBurstTime = bp.Sum(); // sum of burst times
                double cpuUtilization = (totalBurstTime / totalTime) * 100;
                MessageBox.Show("CPU Utilization = " + cpuUtilization.ToString("F2") + " %", "CPU Utilization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //this.Hide();
            }
        }

        public static void roundRobinAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int i, counter = 0;
            double total = 0.0;
            double timeQuantum;
            double waitTime = 0, turnaroundTime = 0;
            double averageWaitTime, averageTurnaroundTime;
            double[] arrivalTime = new double[np];
            double[] burstTime = new double[np];
            double[] temp = new double[np];
            int x = np;

            DialogResult result = MessageBox.Show("Round Robin Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (i = 0; i < np; i++)
                {
                    string arrivalInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                               "Arrival time for P" + (i + 1),
                                                               "",
                                                               -1, -1);

                    arrivalTime[i] = Convert.ToInt64(arrivalInput);

                    string burstInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                               "Burst time for P" + (i + 1),
                                                               "",
                                                               -1, -1);

                    burstTime[i] = Convert.ToInt64(burstInput);

                    temp[i] = burstTime[i];
                }
                string timeQuantumInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter time quantum: ", "Time Quantum",
                                                               "",
                                                               -1, -1);

                timeQuantum = Convert.ToInt64(timeQuantumInput);
                Helper.QuantumTime = timeQuantumInput;

                for (total = 0, i = 0; x != 0;)
                {
                    if (temp[i] <= timeQuantum && temp[i] > 0)
                    {
                        total = total + temp[i];
                        temp[i] = 0;
                        counter = 1;
                    }
                    else if (temp[i] > 0)
                    {
                        temp[i] = temp[i] - timeQuantum;
                        total = total + timeQuantum;
                    }
                    if (temp[i] == 0 && counter == 1)
                    {
                        x--;
                        //printf("nProcess[%d]tt%dtt %dttt %d", i + 1, burst_time[i], total - arrival_time[i], total - arrival_time[i] - burst_time[i]);
                        MessageBox.Show("Turnaround time for Process " + (i + 1) + " : " + (total - arrivalTime[i]), "Turnaround time for Process " + (i + 1), MessageBoxButtons.OK);
                        MessageBox.Show("Wait time for Process " + (i + 1) + " : " + (total - arrivalTime[i] - burstTime[i]), "Wait time for Process " + (i + 1), MessageBoxButtons.OK);
                        turnaroundTime = (turnaroundTime + total - arrivalTime[i]);
                        waitTime = (waitTime + total - arrivalTime[i] - burstTime[i]);                        
                        counter = 0;
                    }
                    if (i == np - 1)
                    {
                        i = 0;
                    }
                    else if (arrivalTime[i + 1] <= total)
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }
                }
                averageWaitTime = Convert.ToInt64(waitTime * 1.0 / np);
                averageTurnaroundTime = Convert.ToInt64(turnaroundTime * 1.0 / np);
                MessageBox.Show("Average wait time for " + np + " processes: " + averageWaitTime + " sec(s)", "", MessageBoxButtons.OK);
                MessageBox.Show("Average turnaround time for " + np + " processes: " + averageTurnaroundTime + " sec(s)", "", MessageBoxButtons.OK);

                double throughput = np / total; // np = number of processes, total = total time
                MessageBox.Show("Throughput: " + throughput.ToString("F2") + " processes/sec", "Throughput", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Calculate CPU Utilization
                double totalBurstTime = 0;
                for (int j = 0; j < np; j++)
                {
                    totalBurstTime += burstTime[j];
                }
                double cpuUtilization = (totalBurstTime / total) * 100;
                MessageBox.Show("CPU Utilization: " + cpuUtilization.ToString("F2") + " %", "CPU Utilization", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        
        public static void srtfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] arrival = new double[np];
            double[] burst = new double[np];
            double[] remaining = new double[np];
            double[] completion = new double[np];
            double totalWait = 0, totalTurnaround = 0;
            int complete = 0, currentTime = 0, minIndex;
            double minRemainingTime;
            bool found;

            // Input arrival and burst times
            for (int i = 0; i < np; i++)
            {
                arrival[i] = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox($"Enter arrival time for P{i + 1}:", "Arrival Time", "", -1, -1));
                burst[i] = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox($"Enter burst time for P{i + 1}:", "Burst Time", "", -1, -1));
                remaining[i] = burst[i];
            }

            while (complete != np)
            {
                minIndex = -1;
                minRemainingTime = double.MaxValue;
                found = false;

                // Find process with minimum remaining time at current time
                for (int i = 0; i < np; i++)
                {
                    if (arrival[i] <= currentTime && remaining[i] > 0 && remaining[i] < minRemainingTime)
                    {
                        minRemainingTime = remaining[i];
                        minIndex = i;
                        found = true;
                    }
                }

                if (!found)
                {
                    currentTime++;
                    continue;
                }

                remaining[minIndex]--;
                currentTime++;

                if (remaining[minIndex] == 0)
                {
                    complete++;
                    completion[minIndex] = currentTime;
                }
            }

            // Calculate Turnaround Time and Waiting Time
            for (int i = 0; i < np; i++)
            {
                double turnaround = completion[i] - arrival[i];
                double waiting = turnaround - burst[i];
                totalTurnaround += turnaround;
                totalWait += waiting;
            }

            double averageWait = totalWait / np;
            double averageTurnaround = totalTurnaround / np;

            // Calculate Throughput
            double totalExecutionTime = completion.Max();
            double throughput = np / totalExecutionTime;

            // Calculate CPU Utilization
            double cpuUtilization = (burst.Sum() / totalExecutionTime) * 100;

            // Display metrics
            MessageBox.Show($"Average Wait Time: {averageWait:F2} sec", "AWT", MessageBoxButtons.OK);
            MessageBox.Show($"Average Turnaround Time: {averageTurnaround:F2} sec", "ATT", MessageBoxButtons.OK);
            MessageBox.Show($"Throughput: {throughput:F2} processes/sec", "Throughput", MessageBoxButtons.OK);
            MessageBox.Show($"CPU Utilization: {cpuUtilization:F2} %", "CPU Utilization", MessageBoxButtons.OK);
        }

       
        public static void hrrnAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] arrival = new double[np];
            double[] burst = new double[np];
            bool[] completed = new bool[np];
            double[] completion = new double[np];
            double currentTime = 0;
            int done = 0;
            double totalWait = 0, totalTurnaround = 0;

            // Input arrival and burst times
            for (int i = 0; i < np; i++)
            {
                arrival[i] = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox($"Enter arrival time for P{i + 1}:", "Arrival Time", "", -1, -1));
                burst[i] = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox($"Enter burst time for P{i + 1}:", "Burst Time", "", -1, -1));
                completed[i] = false;
            }

            while (done != np)
            {
                int selected = -1;
                double highestRR = -1;

                for (int i = 0; i < np; i++)
                {
                    if (arrival[i] <= currentTime && !completed[i])
                    {
                        double waitingTime = currentTime - arrival[i];
                        double responseRatio = (waitingTime + burst[i]) / burst[i];

                        if (responseRatio > highestRR)
                        {
                            highestRR = responseRatio;
                            selected = i;
                        }
                    }
                }

                if (selected == -1)
                {
                    currentTime++;
                    continue;
                }

                currentTime += burst[selected];
                completion[selected] = currentTime;
                completed[selected] = true;
                done++;
            }

            // Calculate Turnaround Time and Waiting Time
            for (int i = 0; i < np; i++)
            {
                double turnaround = completion[i] - arrival[i];
                double waiting = turnaround - burst[i];
                totalTurnaround += turnaround;
                totalWait += waiting;
            }

            double averageWait = totalWait / np;
            double averageTurnaround = totalTurnaround / np;

            // Throughput and CPU Utilization
            double totalExecutionTime = completion.Max();
            double throughput = np / totalExecutionTime;
            double cpuUtilization = (burst.Sum() / totalExecutionTime) * 100;

            // Display metrics
            MessageBox.Show($"Average Wait Time: {averageWait:F2} sec", "AWT", MessageBoxButtons.OK);
            MessageBox.Show($"Average Turnaround Time: {averageTurnaround:F2} sec", "ATT", MessageBoxButtons.OK);
            MessageBox.Show($"Throughput: {throughput:F2} processes/sec", "Throughput", MessageBoxButtons.OK);
            MessageBox.Show($"CPU Utilization: {cpuUtilization:F2} %", "CPU Utilization", MessageBoxButtons.OK);
        }
    }
}

