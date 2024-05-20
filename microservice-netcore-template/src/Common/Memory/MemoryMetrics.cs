using Serilog;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Common.Memory
{

    public class MemoryMetrics
    {
        public double Total;
        public double Used;
        public double Free;
    }

    public class MemoryMetricsClient
    {
        public MemoryMetrics GetMetrics(bool throwIfFails = false)
        {
            try
            {
                if (IsUnix())
                {
                    return GetUnixMetricsUsingFreeMegaBytes();
                }

                return GetWindowsMetrics();
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "memory cannot be accesed");
                if (throwIfFails)
                    throw;
                else
                    return null;
            }
        }

        public bool IsUnix()
        {
            var isUnix = RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                         RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

            return isUnix;
        }

        public MemoryMetrics GetWindowsMetrics()
        {
            try
            {
                var output = "";

                var info = new ProcessStartInfo();
                info.FileName = "wmic";
                info.Arguments = "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value";
                info.RedirectStandardOutput = true;

                using (var process = Process.Start(info))
                {
                    output = process.StandardOutput.ReadToEnd();
                }

                var lines = output.Trim().Split("\n");
                var freeMemoryParts = lines[0].Split("=", StringSplitOptions.RemoveEmptyEntries);
                var totalMemoryParts = lines[1].Split("=", StringSplitOptions.RemoveEmptyEntries);

                var metrics = new MemoryMetrics();
                metrics.Total = Math.Round(double.Parse(totalMemoryParts[1]) / (1024 * 1024), 2);
                metrics.Free = Math.Round(double.Parse(freeMemoryParts[1]) / (1024 * 1024), 2);
                metrics.Used = metrics.Total - metrics.Free;

                return metrics;
            }
            catch (Exception e)
            {
                Log.Error("Failed to get memory metrics." + e.Message);
                var metrics = new MemoryMetrics();
                metrics.Total = 0;
                metrics.Free = 0;
                metrics.Used = 0;

                return metrics;
            }
        }


        /*
         requires a s.o tool level
         apt-get update
         apt-get install procps
         from 218 to 238
        research: /proc/meminfo
         */
        public MemoryMetrics GetUnixMetricsUsingFreeMegaBytes()
        {
            try
            {
                var output = "";

                var info = new ProcessStartInfo("free -m");
                info.FileName = "/bin/bash";
                info.Arguments = "-c \"free --mega\"";
                info.RedirectStandardOutput = true;

                using (var process = Process.Start(info))
                {
                    output = process.StandardOutput.ReadToEnd();
                }

                var lines = output.Split("\n");
                var memory = lines[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var metrics = new MemoryMetrics();
                metrics.Total = double.Parse(memory[1]);
                metrics.Used = double.Parse(memory[2]);
                metrics.Free = double.Parse(memory[3]);
                return metrics;
            }
            catch (Exception e)
            {
                Log.Error("Failed to get memory metrics." + e.Message);
                var metrics = new MemoryMetrics();
                metrics.Total = 0;
                metrics.Free = 0;
                metrics.Used = 0;

                return metrics;
            }
        }

    }
}