using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace Snake
{
    class Input // klasa Input koja ce da vodi racuna o tasterima koji se stiskaju na tastaturi
    {
        private static Hashtable keyTable = new Hashtable(); // lista stiskanih tastera na tastaturi
        public static bool KeyPresed(Keys key) // da li je stanje na tasteru true(stisnuto) ili false(nije stisnuto), ili do sad nije bilo stiskano (null)
        {
            if (keyTable[key]== null)
            {
                return false;
            }
            return (bool)keyTable[key];
                
        }
        public static void ChangeState(Keys key,bool state) //promena stanja na tastaturi, ako je stisnut taster onda je true, ako nije onda je false
        {
            keyTable[key] = state;
        }
    }
}
