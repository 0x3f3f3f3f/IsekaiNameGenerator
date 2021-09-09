using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Megumin.IsekaiNameGenerator
{
    class ToneHelper
    {
        static public List<char> yisheng = new List<char>()
        {
            'ā','ō','ē','ī','ū','ǖ'
        };

        static public List<char> ersheng = new List<char>()
        {
            'á','ó','é','í','ú','ǘ'
        };

        static public List<char> sansheng = new List<char>()
        {
            'ǎ','ǒ','ě','ǐ','ǔ','ǚ'
        };

        static public List<char> sisheng = new List<char>()
        {
            'à','ò','è','ì','ù','ǜ'
        };

        public static int GetTone(string pinyin)
        {
            pinyin = pinyin.ToLower();
            if (pinyin.Intersect(yisheng).Count() > 0)
            {
                return 1;
            }
            else if (pinyin.Intersect(ersheng).Count() > 0)
            {
                return 2;
            }
            else if (pinyin.Intersect(sansheng).Count() > 0)
            {
                return 3;
            }
            else if (pinyin.Intersect(sisheng).Count() > 0)
            {
                return 4;
            }
            else
            {
                return 5;
            }
        }
    }
}
