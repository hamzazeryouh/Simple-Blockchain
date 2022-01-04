using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace SmartContract.Model
{
    public class Block
    {
        private readonly DateTime _timeStamp;
        private long _nonce;
        public string _previousHash { get; set; }
        public List<Transactions> _transactions { get; set; }

    public string Hash { get; private set; }

    public Block(DateTime timeStamp, List<Transactions> transactions, string previousHash = "")
    {
        _timeStamp = timeStamp;
        _nonce = 0;
        _transactions = transactions;
        _previousHash = previousHash;
        Hash = CreateHash();
    }
    public void MineBlock(int proofOfWorkDifficulty)
    {
        string hashValidationTemplate = new String('0', proofOfWorkDifficulty);

        while (Hash.Substring(0, proofOfWorkDifficulty) != hashValidationTemplate)
        {
            _nonce++;
            Hash = CreateHash();
        }
        Console.WriteLine("Blocked with HASH={0} successfully mined!", Hash);
    }
    public string CreateHash()
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            string rawData = _previousHash + _timeStamp + _transactions + _nonce;
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return Encoding.Default.GetString(bytes);
        }
    }
}
}
