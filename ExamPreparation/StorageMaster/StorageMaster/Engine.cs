using System;
using System.Collections.Generic;
using System.Linq;
using StorageMaster.Core.IO.Contracts;

namespace StorageMaster
{
    public class Engine
    {
        StorageMaster storageMaster;
        bool isRunning;
        public IReader reader;
        public IWriter writer;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            storageMaster = new StorageMaster();
            isRunning = true;
        }
        public void Run()
        {
            while (isRunning)
            {
                var inputStr = reader.ReadLine().Split(' ').ToList();
                RunCommands(inputStr);
            }
            writer.WriteLine(storageMaster.GetSummary());
        }
        private void RunCommands(List<string> input)
        {
            string output = string.Empty;
            var command = input[0];
            input = input.Skip(1).ToList();
            switch (command)
            {

                case "END":
                    isRunning = false;
                    break;
                case "AddProduct":
                    try
                    {
                        output = storageMaster.AddProduct(input[0], double.Parse(input[1]));
                    }
                    catch (Exception e)
                    {
                        output = "Error: " + e.Message;
                    }
                    break;
                case "RegisterStorage":
                    try
                    {
                        output = storageMaster.RegisterStorage(input[0], input[1]);
                    }
                    catch (Exception e)
                    {
                        output = "Error: " + e.Message;
                    }
                    break;
                case "SelectVehicle":
                    try
                    {
                        output = storageMaster.SelectVehicle(input[0], int.Parse(input[1]));
                    }
                    catch (Exception e)
                    {
                        output = "Error: " + e.Message;
                    }
                    break;
                case "LoadVehicle":
                    try
                    {
                        output = storageMaster.LoadVehicle(input);
                    }
                    catch (Exception e)
                    {
                        output = "Error: " + e.Message;
                    }
                    break;
                case "SendVehicleTo":
                    try
                    {
                        output = storageMaster.SendVehicleTo(input[0], int.Parse(input[1]), input[2]);
                    }
                    catch (Exception e)
                    {
                        output = "Error: " + e.Message;
                    }
                    break;
                case "UnloadVehicle":
                    try
                    {
                        output = storageMaster.UnloadVehicle(input[0], int.Parse(input[1]));
                    }
                    catch (Exception e)
                    {
                        output = "Error: " + e.Message;
                    }
                    break;
                case "GetStorageStatus":
                    try
                    {
                        output = storageMaster.GetStorageStatus(input[0]);
                    }
                    catch (Exception e)
                    {
                        output = "Error: " + e.Message;
                    }
                    break;
            }
            writer.WriteLine(output);
        }
    }

}