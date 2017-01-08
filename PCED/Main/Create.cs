using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PCED.Main
{
    public class Create
    {
        public static string[] file_contents { get; set; }
        public static string exitcode { get; set; }

        public static void CreatePlaylist(string[] items, string filepath, string _name) 
        {
            //creating original copies of variables
            string filepath_orig = filepath;
            string _name_orig = _name;
            //creating file
            try
            {
                //Check if file exists, if file already exists; then delete it
                if (File.Exists(filepath)) { File.Delete(filepath); }

                using (FileStream fs = File.Create(filepath)) {
                    //Creating the file & Writing the information to te file
                    fs.Write(null, 0, items.Length);
                }
            }
            catch (Exception e)
            {
                // Print Exception 'e'
                Console.WriteLine(e.ToString());
            }

            //writing text to file
            try
            {
                //Making every 'items' item start on a new line
                for (int x = 0; x < items.Length; x++) {
                    items[x] = items[x] + Environment.NewLine;
                }
                //Adding 'Environment.NewLine' to misc variables
                _name = _name + Environment.NewLine;
                filepath = filepath + Environment.NewLine;
                string items_header = "items=" + Environment.NewLine;
                //Generating whole file content
                string[] fc1 = { "name="+_name, items_header};
                string[] fc2 = { };
                //making 'fc2' Length propety the same as 'items' Length propety
                try
                {
                    if (fc2.Length != items.Length)
                    {
                        if (fc2.Length > items.Length) {
                            for (int x = items.Length + 1; x < fc2.Length; x++)
                            {
                                items[x] = "";
                            }
                        }
                        if (fc2.Length < items.Length)
                        {
                            for (int x = fc2.Length + 1; x < items.Length; x++)
                            {
                                fc2[x] = "";
                            }
                        }
                    }
                }
                catch (Exception e) {
                    exitcode = "Failure : " + e;
                }
                try
                {
                    for (int x = 0; x < fc2.Length; x++)
                    {
                        fc2[x] = items[x];
                    }
                }
                catch (Exception e) {
                    exitcode = "Failure : " + e;
                }

                string[] fc = { fc1[0], fc1[1] };
                //joining content arrays together
                for (int x = 0; x < fc2.Length; x++) {
                    for (int y = 2; y < items.Length; y++) {
                        fc[y] = fc2[x];
                    }
                }
                //making 'file_contents' Length propety the same as 'fc' Length propety
                try
                {
                    if (file_contents.Length != fc.Length)
                    {
                        if (file_contents.Length < fc.Length)
                        {
                            for (int x = file_contents.Length + 1; x < fc.Length; x++)
                            {
                                file_contents[x] = "";
                            }
                        }
                    }
                }
                catch (Exception e) {
                    exitcode = "Failure : " + e;
                }
                //making file content public
                try
                {
                    for (int x = 0; x < fc.Length; x++)
                    {
                        file_contents[x] = fc[x];
                    }
                }
                catch (Exception e) {
                    exitcode = "Failure : " + e;
                }
                //Creating final content string
                string fc_string = ("name="+_name+items_header);
                string fc_string_content = "";
                try
                {
                    for (int x = 0; x < items.Length; x++)
                    {
                        fc_string_content += items[x];
                    }
                }
                catch (Exception e) {
                    exitcode = "Failure : " + e;
                }
                fc_string_content = fc_string_content;
                //writing to file
                try
                {
                    File.WriteAllText(filepath_orig, fc_string);
                    File.AppendAllText(filepath_orig, fc_string_content);
                }
                catch (Exception e) {
                    exitcode = "Failure : " + e;
                }
                exitcode = "Success";
            }
            catch (Exception e)
            {
                exitcode = "Failure : " + e;
                Console.WriteLine(e.ToString());
            }
        }
    }
}
