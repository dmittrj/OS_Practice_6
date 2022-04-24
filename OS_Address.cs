namespace OS_Practice_6
{
    public class OS_Address
    {

        public OS_Task Task { get; set; }
        public int Address { get; set; }
        public OS_Address(OS_Task task, int address)
        {
            Task = task;
            Address = address;
        }
    }
}