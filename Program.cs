using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace tinyBlock
{
    class Program
    {
    	public static string getHashSha256(string text)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(text);
			SHA256Managed hashstring = new SHA256Managed();
			byte[] hash = hashstring.ComputeHash(bytes);
			string hashString = string.Empty;
			foreach (byte x in hash)
			{
				hashString += String.Format("{0:x2}", x);
			}
			return hashString;
		}

		public class Block
		{
			public int Index;
			public string Hash;
			public string Timestamp;
			public string Data;
			public string Previous_hash;

			public Block(int index, string timestamp, string data, string previous_hash){
				Index = index;
				Timestamp = timestamp;
				Data = data;
				Previous_hash = previous_hash;
				Hash = getHashSha256((Index + Timestamp + Data + Previous_hash).ToString());
			}

			public static string getHashSha256(string text)
			{
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				SHA256Managed hashstring = new SHA256Managed();
				byte[] hash = hashstring.ComputeHash(bytes);
				string hashString = string.Empty;
				foreach (byte x in hash)
				{
					hashString += String.Format("{0:x2}", x);
				}
				return hashString;
			}

		}

		public static Block newBlock(Block lastBlock){
			int nextIndex = lastBlock.Index + 1;
			string nextTimestamp = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
			string nextData = "I'm the next block nr. " + nextIndex.ToString();
			string nextPrevHash = lastBlock.Hash;
			return new Block(nextIndex, nextTimestamp, nextData, nextPrevHash);
		}

		public static Block generateGenesis(){
			return new Block(1, DateTime.Now.ToString("yyyyMMddHHmmss.fff"), "whoooooohoooo!", "0");
		}

		static void Main(string[] args)
		{
			List<Block> blockChain = new List<Block>();
			blockChain.Add(generateGenesis());
			Block previousBlock = blockChain[blockChain.Count -1];

			for (int i = 0; i < 10; i = i + 1) {
				Block nextBlock = newBlock(previousBlock);
				blockChain.Add(nextBlock);
				previousBlock = nextBlock;
			}
			
			foreach(var block in blockChain){
				Console.WriteLine("BlockNr: " + block.Index);
				Console.WriteLine("Hash: " + block.Hash);
				Console.WriteLine("Data: " + block.Data);
				Console.WriteLine(" ");
			}
		}
	}
}