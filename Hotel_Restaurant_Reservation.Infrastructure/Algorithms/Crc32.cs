namespace Hotel_Restaurant_Reservation.Infrastructure.Algorithms;

public static class Crc32
{
    private static readonly uint[] _table = GenerateTable();

    private static uint[] GenerateTable()
    {
        var table = new uint[256];
        const uint poly = 0xedb88320;
        for (uint i = 0; i < table.Length; i++)
        {
            uint crc = i;
            for (int j = 8; j > 0; j--)
            {
                if ((crc & 1) == 1)
                    crc = crc >> 1 ^ poly;
                else
                    crc >>= 1;
            }
            table[i] = crc;
        }
        return table;
    }

    public static uint Compute(byte[] buffer)
    {
        uint crc = 0xffffffff;
        foreach (byte t in buffer)
        {
            byte index = (byte)(crc & 0xff ^ t);
            crc = crc >> 8 ^ _table[index];
        }
        return ~crc;
    }
}