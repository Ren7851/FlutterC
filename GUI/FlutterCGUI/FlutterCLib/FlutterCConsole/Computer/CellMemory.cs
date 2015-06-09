using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class CellMemory
    {
        public static int counter = 0;

        public int pointer = 1;

        public static int MEMORY_SIZE = 1000000;

        public bool[] used = new bool[MEMORY_SIZE];
        public int[] sizes = new int[MEMORY_SIZE];
        public Value[] memoryMap = new Value[MEMORY_SIZE];

        private Dictionary<int, Stack<int>> freeList;

        public Random random = new Random();

        public CellMemory()
        {
            freeList = new Dictionary<int, Stack<int>>();
        }

        public bool isUsed(int address)
        {
            return used[address];
        }

        public Value getValue(int address)
        {
            return memoryMap[address];
        }

        public void setValue(int address, Value value)
        {
            memoryMap[address] = value;
            used[address] = true;
            sizes[address] = 1;
        }

        public void free(int address)
        {
            if(address == 0){
                return;
            }
            int size = sizes[address];
            for (int i = address; i < address + size; i++ )
            {
                used[i] = false;
            }
            if(!freeList.ContainsKey(size)){
                freeList[size] = new Stack<int>();
            }
            freeList[size].Push(address);
            return;
        }

        public int getFreeBlock(int size)
        {
            if (freeList.ContainsKey(size) && freeList[size].Count > 0)
            {
                int r = freeList[size].Peek();
                freeList[size].Pop();
                return r;
            }

            int res = pointer;
            pointer += size;


            counter = pointer;

            if (pointer >= MEMORY_SIZE)
            {
                throw new Exceptions.OutOfMemoryException();
            }

            return res;
        }

        public void allocateVariable(Value val)
        {
            int number = getFreeBlock(1);
            val.address = number;
            setValue(number, val);
        }

        public void malloc(Value pointer, int size)
        {
            pointer.size = size;
            int number = getFreeBlock(size);
            for (int i = number; i < number + size; i++)
            {
                Value val = new Value(0, pointer.getValueToken(), i);
            }
            pointer.value = number;
            sizes[number] = size;
        }

        public void allocatePointer(Value pointer, decimal[] vals)
        {
            int size = pointer.size;
            int number = getFreeBlock(size);
            for (int i = number; i < number + size; i++){
                Value val = new Value(vals[i - number], "char", i);
            }
            pointer.value = number;
            sizes[number] = vals.Length;
        }
    }
}
