using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Limilabs.Client.IMAP;
using Limilabs.Mail;
using Limilabs.Mail.MIME;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Imap imap = new Imap())
            {
                imap.ConnectSSL("imap.example.com");
                imap.Login("example@example.com","p@assword");
                imap.SelectInbox();
                List<long> uids = imap.SearchFlag(Flag.Unseen);
                foreach (long uid in uids)
                {
                    IMail email= new MailBuilder().CreateFromEml(imap.GetMessageByUID(uid)) ;
                    Console.WriteLine(email.Subject);
                    
                    // save all attachments to disk
		            foreach(MimeData mime in email.Attachments)
		            {
                        Console.WriteLine(mime.FileName);
			            mime.Save("c:\\Your desired path\\" + mime.FileName);
		            }
	            }
	            imap.Close();
                }
            }

        }
    }
