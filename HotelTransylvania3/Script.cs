private void SubTitlesExtract()
{
try
	{
		int counter = 1;                  
		int n;
		string path = @"C:\Users\bhagya.sanjeewa\Downloads\Hotel.Transylvania.3.srt";
		string outputPath = @"D:\Output.txt";

		string[] final = new string[0];
		System.IO.StreamReader file = new System.IO.StreamReader(path);

		string[] allLines = File.ReadAllLines(path);
		List<Subs>  lstSubs = new List<Subs>();
		for (int i = 0; i < allLines.Length; i++)
		{
			
			bool isNumeric = int.TryParse(allLines[i], out n);
			if (isNumeric)
			{
				if (n.Equals(counter))
				{
					//MessageBox.Show(allLines[i]);
					string[] filtered = allLines.Slice(Array.IndexOf(allLines, counter.ToString()), Array.IndexOf(allLines, (counter + 1).ToString()));
					string[] filtered2 = filtered.Slice(Array.IndexOf(filtered, counter.ToString()) + 2, filtered.Length);

					for (int k = 0; k < filtered2.Length; k++)
					{
						if(!filtered2[k].Equals(String.Empty))
						{
							if (!Regex.IsMatch(filtered2[k].Trim().ToString().Substring(0, 1).ToString(), "[a-z]", RegexOptions.IgnoreCase))
							{
								//filtered2[k].Trim().ToString().Replace(filtered2[k].Trim().ToString().Substring(0, 1).ToString(), "");
								filtered2[k] = filtered2[k].Trim().ToString().Replace(filtered2[k].Trim().ToString().Substring(0, 1).ToString(), "").Trim().ToString().Trim();
							}
							
							lstSubs.Add(new Subs{ SubID = k+1, SubValue = filtered2[k].ToString()});
						}
					}

					counter++;
				}                        
			}
		}

		int count = lstSubs.Count;

		if (lstSubs != null && lstSubs.Count > 0)
		{
			if (File.Exists(outputPath))
			{
				File.WriteAllText(outputPath, String.Empty);
				using (var tw = new StreamWriter(outputPath, true))
				{
					foreach (Subs objSub in lstSubs)
					{
						tw.WriteLine(objSub.SubValue);
					}
					
				}
			}

			MessageBox.Show("Done");
		}
	}
	catch (Exception ex)
	{
		
		throw ex;
	}
}
	//Create a static class for Extention Methods
public static class ExtentionMethods
{
	//Extention Method for Slice the array 
	public static T[] Slice<T>(this T[] source, int start, int end)
	{
		if (end < 0)
		{
			end = source.Length + end;
		}
		int len = end - start;

		T[] res = new T[len];
		for (int i = 0; i < len; i++)
		{
			res[i] = source[i + start];
		}
		return res;
	}
}

//Class of Subs
public class Subs
{
    public int SubID { get; set; }
    public string SubValue { get; set; }

}