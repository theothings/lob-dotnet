using Lob.Net.Resouces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lob.Net
{
    public class Lob
    {
        private string version;
        private string apiKey;

        public string ClientVersion
        {
            get
            {
                return "1.0.0";
            }
        }

        public string Version
        {
            get
            {
                return this.version;
            }
        }

        public string ApiKey
        {
            get
            {
                return this.apiKey;
            }
        }

        public Lob(string apiKey = null, string version = null)
        {
            this.apiKey = apiKey;
            this.version = version;
        }

        public Addresses Addresses
        {
            get
            {
                return new Addresses(this);
            }
        }

        public Areas Areas
        {
            get
            {
                return new Areas(this);
            }
        }

        public BankAccounts BankAccounts
        {
            get
            {
                return new BankAccounts(this);
            }
        }

        public Checks Checks
        {
            get
            {
                return new Checks(this);
            }
        }

        public Countries Countries
        {
            get
            {
                return new Countries(this);
            }
        }

        public Letters Letters
        {
            get
            {
                return new Letters(this);
            }
        }

        public Postcards Postcards
        {
            get
            {
                return new Postcards(this);
            }
        }

        public Routes Routes
        {
            get
            {
                return new Routes(this);
            }
        }

        public States States
        {
            get
            {
                return new States(this);
            }
        }
    }
}
