using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingSmart.Models
{
    /// <summary>
    /// a categorie must not share the same keywords as another categorie.
    /// </summary>
    public class Categorie
    {
        //type
        public static Categorie MENS = new Categorie(1, "MENS", new String[] { "mens", "men", "male", "males" });

        public static Categorie WOMANS = new Categorie(2, "WOMANS", new String[] { "womans", "woman", "female", "females" });

        public static Categorie BOYS = new Categorie(3, "BOYS", new String[] { "boy", "boys", });

        public static Categorie GIRLS = new Categorie(4, "GIRLS", new String[] { "girl", "girls" });

        public static Categorie BABIES = new Categorie(5, "BABIES", new String[] { "baby", "babies", "toddler", "toddlers" });

        // tops 
        public static Categorie TOPS = new Categorie(6, "TOPS", new String[] { "tshirt", "singlet", "tee", "shirt", "waistcoat", "top", "tank", "cardigan", "cardi", "bolero","kimono" });

        public static Categorie JACKETS = new Categorie(7, "JACKETS", new String[] { "anorak", "jacket", "coat", "hoodie", "jumper", "blazer", "sweatshirt", "vest","fleece", "knit" });

        //bottom
        public static Categorie PANTS = new Categorie(8, "PANTS", new String[] { "pants", "jeans", "pant", "jean", "trousers" });

        public static Categorie SHORTS = new Categorie(9, "SHORTS", new String[] { "shorts", "short", "boardshorts", "boardshort", "walkshorts", "boardies", });
        //dresses and skirts
        public static Categorie DRESSES = new Categorie(10, "DRESSES", new String[] { "gown", "dress", "tunic", "skirt" });
        //feet
        public static Categorie SOCKS = new Categorie(11, "SOCKS", new String[] { "socks", "sock" });

        public static Categorie TIGHTS = new Categorie(12, "TIGHTS", new String[] { "tights", "tight", "stocking", "stockings", "hoisery", "legging", "leggings" });
        //todo expand this
        public static Categorie SHOES = new Categorie(13, "SHOES", new String[] { "shoes", "boots", "slippers", "wedges", "heels", "flats", "sandals" });
        //other
        public static Categorie HATS = new Categorie(14, "HATS", new String[] { "hat", "hats", "trilby", "cap", "beanie" });
        /// <summary>
        /// bags and wallets
        /// </summary>
        public static Categorie BAGS = new Categorie(15, "BAGS", new String[] { "bags", "bag", "wallets", "handbag", "backpack", "satchel", "sack" });

        public static Categorie SCARFS = new Categorie(16, "SCARFS", new String[] { "scarf", "scarfs", "Poncho" });

        public static Categorie TIES = new Categorie(17, "TIES", new String[] { "tie", "ties", "bowtie", "bowties" });

        public static Categorie BELTS = new Categorie(18, "BELTS", new String[] { "belt","belts" });

        public static Categorie SWIMWEAR = new Categorie(19, "SWIMWEAR", new String[] { "swimwear","bikini", "swimsuit", "tankini", "skirtini" });

        public static Categorie SLEEPWEAR = new Categorie(20, "SLEEPWEAR", new String[] { "sleepwear", "pyjamas", "robe", "pj", "pjs" });

        public static Categorie UNDERWEAR = new Categorie(21, "UNDERWEAR", new String[] { "bra", "underwear", "knickers", "breif", "gstring" });

        //jewelery is large categorie
        public static Categorie WATCHES = new Categorie(22, "WATCHES", new String[] { "watch", "watches" });

        public static Categorie RINGS = new Categorie(23, "RINGS", new String[] { "ring", "rings" });
        //and lockets
        public static Categorie NECKLACES = new Categorie(24, "NECKLACES", new String[] { "necklace", "chain", "pendant", "locket", "necklet" });

        public static Categorie EARRINGS = new Categorie(25, "EARRINGS", new String[] { "earrings", "earring", "stud", "studs", "hoops" });

        public static Categorie BRACELETS = new Categorie(26, "BRACELETS", new String[] { "bracelet", "anklet" });
        //and charms
        public static Categorie OTHERJEWELERY = new Categorie(27, "OTHERJEWELERY", new String[] { "charm" });

        // companies

        //public static  Categorie THE_WAREHOUSE = new Categorie($,"THE_WAREHOUSE", new String[] { "warehouse"});



        public string name;
        public string[] keywords;
        public int id;

        public Categorie(int id, string name, string[] keywords)
        {
            this.id = id;
            this.name = name;
            this.keywords = keywords;

        }
        /// <summary>
        /// order matters same ordering as categorie ids 
        ///  getCategories()[MENS.id-1] should be mens ect
        /// 
        /// </summary>
        /// <returns></returns>
        public static Categorie[] getCategories()
        {
            return new Categorie[] {
                MENS,
                WOMANS,
                BOYS,
                GIRLS,
                BABIES,
                TOPS,
                JACKETS,
                PANTS,
                SHORTS,
                DRESSES,
                SOCKS,
                TIGHTS,
                SHOES,
                HATS,
                BAGS,
                SCARFS,
                TIES,
                BELTS,
                SWIMWEAR,
                SLEEPWEAR ,
                UNDERWEAR,
                WATCHES,
                RINGS,
                NECKLACES,
                EARRINGS,
                BRACELETS,
                OTHERJEWELERY
            };
        }

        public static Categorie[] getJewelryCategories()
        {
            return new Categorie[] {
                WATCHES,
                RINGS,
                NECKLACES,
                EARRINGS,
                BRACELETS,
                OTHERJEWELERY
            };
        }


    }
}