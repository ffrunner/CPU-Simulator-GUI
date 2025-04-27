# CPU Scheduling Simulator - CS 3502 Project 2

## Description
This project simulates various CPU Scheduling Algorithms.  
It measures and compares the performance of each algorithm using key metrics like:
- Average Waiting Time (AWT)
- Average Turnaround Time (ATT)
- CPU Utilization
- Throughput
- (Optional) Response Time

It is based on the starter Windows Forms code provided by OwlTech Systems, extended with new scheduling methods and additional performance measurement features.

---

## How to Build and Run
1. Open the project in **Visual Studio 2022** or later.
2. Open the `CpuSchedulingWinForms.sln` solution file.
3. Press **F5** to build and run the application.
4. Enter the number of processes when prompted.
5. Enter arrival times, burst times, and priorities as requested.
6. Select any scheduling algorithm to see results displayed.

---

## Scheduling Algorithms Implemented
- First Come First Served (FCFS)
- Shortest Job First (SJF)
- Priority Scheduling
- Round Robin Scheduling
- Shortest Remaining Time First (SRTF)
- Highest Response Ratio Next (HRRN)

---

## Changes and Extensions
- Added two new algorithms: **SRTF** and **HRRN**.
- Modified the starter code to allow dynamic number of processes (e.g., 3, 8, or 30 processes).
- Measured and displayed key performance metrics for each scheduling method.
- Extended the Round Robin algorithm to accept user-defined **Time Quantum**.
- Fixed array sizing issues to support larger test cases (e.g., 30 processes).
- Improved overall scheduling result displays for easier testing and comparison.

---

## Notes
- Arrival times, burst times, and priorities can be entered manually for flexible testing.
- Time Quantum is user-specified when running Round Robin scheduling.
- Graphical performance visualization (tables and graphs) is provided separately in the LaTeX report.
