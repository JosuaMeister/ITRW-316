//Josua Wilken 26939908
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticalThree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        } 
    }/////////Edit As Thou Wish

    ////CLass used to simulate Program Address Space
    class Program_Adress_Space
    {
        public int size;
        public int[] program_adress_space;

        public Program_Adress_Space(int size)///Receives size for array
        {
            SetSize(size);
            SetAdress(size);
        }

        public void SetSize(int x)
        {
            this.size = x;
        }

        public int GetSize()
        {
            return size;
        }
        //sets the size of the array containing the adresses of the physical program
        public void SetAdress(int size)
        {
            program_adress_space = new int[size];

            for (int i = 0; i < size - 1; i++)
            {
                program_adress_space[i] = i;
            }
        }

        public int GetAdress(int x)
        {
            return program_adress_space[x];
        }
    }
    //////////////////////////////////////

    /// Class used for Mapping object and receives program adress space array, size of the array as limit register and the base register 
    class Map
    {
        public int size;
        public int basereg; //variable used to save the actual base register to the physical memory's last limit register


        public Map(int[] program_adress_space_array, int x)
        {
            SetSize(x);
            ////checks to see if enough memory is available
            if (PhysicalMemory.EnoughMemory(size))
            {
                //move to ram
                SetBaseReg(PhysicalMemory.GetLimitReg());//sets the base register on MAP to the limit register on memory
                PhysicalMemory.SendPages(program_adress_space_array);
            }
            else
            {
                //move to disk
                VirtualMemory.SendPages(program_adress_space_array);
            }
        }

        public void SetSize(int x)
        {
            this.size = x;
        }

        public int GetSize()
        {
            return size;
        }

        public void SetBaseReg(int x)
        {
            this.basereg = x;
        }
    }
    ////////////////////

    ///Class to simulate physical memory NOTE: Only one object/instance of this class will be made
    class PhysicalMemory
    {
        public bool flagMemFull;
        public int size, basereg;
        public static int emptyslots;
        public static int limitreg;
        public static int[] program_adress_space;
        public int[] physical_memory_adress_space; ///40 is the amount of pages the physical memory can handle

        public PhysicalMemory(int size)
        {
            flagMemFull = false;
            basereg = 0;
            limitreg = 0;
            physical_memory_adress_space = new int[size];
            emptyslots = 0;
        }

        public static bool EnoughMemory(int numpages)
        {
            if (GetEmptySlots() != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void SendPages(int[] program_adress_space_array)
        {
            SetAdressSpace(program_adress_space_array);
        }

        public static void SetAdressSpace(int[] program_adress_space_array)
        {
            program_adress_space = program_adress_space_array;
        }

        public void SetLimitReg(int x)//x is the limit register 
        {
            limitreg = x;
        }

        public static int GetLimitReg()
        {
            return limitreg;
        }

        public static int GetEmptySlots()
        {
            return emptyslots;
        }

    }
    ///////////////
    /// /// Class to simulate virtual memory on the hard disk  NOTE: Only one object/instance of this class will be made
    class VirtualMemory
    {
        public static int[] virtual_mem;

        public VirtualMemory()
        {

        }

        public static void SendPages(int[] program_adress_space_array)
        {
            SetAdressSpace(program_adress_space_array);
        }

        public static void SetAdressSpace(int[] program_adress_space_array)
        {
            virtual_mem = program_adress_space_array;
        }
    }
}
