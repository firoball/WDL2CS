using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace VCCCompiler
{
        /// <summary>
        /// Zusammenfassung für MyCompiler.
        /// </summary>
        class MyCompiler
        {
                YYARec[] yya;
                YYARec[] yyg;
                YYRRec[] yyr;
                int[] yyd;
                int[] yyal;
                int[] yyah;
                int[] yygl;
                int[] yygh;

                int yyn = 0;
                int yystate = 0;
                int yychar = -1;
                int yynerrs = 0;
                int yyerrflag = 0;
                int yysp = 0;
                int yymaxdepth = 10000;
                int yyflag = 0;
                int yyfnone   = 0;
                int[] yys = new int[10000];
                string[] yyv = new string[10000];

                string yyval = "";

                FileStream OutputStream;
                StreamWriter Output;

                class YYARec
                {
                        public int sym;
                        public int act;
                        public YYARec (int s, int a){ sym = s; act = a; }
                }

                class YYRRec
                {
                        public int len;
                        public int sym;
                        public YYRRec (int l, int s){ sym = s; len = l; }
                }

                ////////////////////////////////////////////////////////////////
                /// Constant values / tokens
                ////////////////////////////////////////////////////////////////
                int t_IFDEF = 257;
                int t_Char59 = 258;
                int t_IFNDEF = 259;
                int t_IFELSEChar59 = 260;
                int t_ENDIFChar59 = 261;
                int t_DEFINE = 262;
                int t_Char44 = 263;
                int t_UNDEF = 264;
                int t_INCLUDE = 265;
                int t_FLIC = 266;
                int t_MODEL = 267;
                int t_MUSIC = 268;
                int t_SOUND = 269;
                int t_BMAP = 270;
                int t_OVLY = 271;
                int t_FONT = 272;
                int t_BMAPS = 273;
                int t_OVLYS = 274;
                int t_Char123 = 275;
                int t_Char125 = 276;
                int t_Char58 = 277;
                int t_NULL = 278;
                int t_Char124Char124 = 279;
                int t_Char38Char38 = 280;
                int t_Char124 = 281;
                int t_Char94 = 282;
                int t_Char38 = 283;
                int t_Char40 = 284;
                int t_Char41 = 285;
                int t_Char33Char61 = 286;
                int t_Char61Char61 = 287;
                int t_Char60 = 288;
                int t_Char60Char61 = 289;
                int t_Char62 = 290;
                int t_Char62Char61 = 291;
                int t_Char43 = 292;
                int t_Char45 = 293;
                int t_Char37 = 294;
                int t_Char42 = 295;
                int t_Char47 = 296;
                int t_Char33 = 297;
                int t_RULE = 298;
                int t_Char42Char61 = 299;
                int t_Char43Char61 = 300;
                int t_Char45Char61 = 301;
                int t_Char47Char61 = 302;
                int t_Char61 = 303;
                int t_ABS = 304;
                int t_ACOS = 305;
                int t_ASIN = 306;
                int t_COS = 307;
                int t_EXP = 308;
                int t_INT = 309;
                int t_LOG = 310;
                int t_LOG10 = 311;
                int t_LOG2 = 312;
                int t_RANDOM = 313;
                int t_SIGN = 314;
                int t_SIN = 315;
                int t_SQRT = 316;
                int t_TAN = 317;
                int t_ELSE = 318;
                int t_IF = 319;
                int t_WHILE = 320;
                int t_Char46 = 321;
                int t_integer = 322;
                int t_fixed = 323;
                int t_identifier = 324;
                int t_file = 325;
                int t_string = 326;
                int t_ignore = 256;
///////////////////////////////////////////////////////////
/// Global settings: 
///////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////


                /// <summary>
                /// Der Haupteinstiegspunkt für die Anwendung.
                /// </summary>
                [STAThread]
                static int Main(string[] args)
                {

                        bool ShowTokens = false;
                        string InputFilename = "";
                        string OutputFilename = "";

                        foreach (string s in args)
                        {
                                if (s.ToLower() == "-t")
                                {
                                        ShowTokens = true;
                                }
                                else
                                {
                                        if (InputFilename == "")  InputFilename = s; else
                                        if (OutputFilename == "") OutputFilename = s; else
                                        {
                                                Console.WriteLine("Too many arguments!");
                                                return 1;
                                        }
                                }
                        }
                        if (InputFilename == "")
                        {
                                System.Console.WriteLine("You need to specify input and outputfile: compiler.exe input.txt output.txt");
                                return 1;
                        }

                        StreamReader in_s = File.OpenText(InputFilename);
                        string inputstream = in_s.ReadToEnd();
                        in_s.Close();

                        ////////////////////////////////////////////////////////////////
                        /// Compiler Code:
                        ////////////////////////////////////////////////////////////////
                        MyCompiler compiler = new MyCompiler();
                        compiler.Output = null;
                        if (OutputFilename != "")
                        {
                                File.Delete(OutputFilename);
                                compiler.OutputStream = File.OpenWrite(OutputFilename);
                                compiler.Output = new StreamWriter(compiler.OutputStream,new System.Text.UTF8Encoding(false));
                        }
                        else
                        {
                                compiler.Output = new StreamWriter(Console.OpenStandardOutput(),new System.Text.UTF8Encoding(false));

                        }

                        if (!compiler.Scanner(inputstream)) return 1;
                        if (ShowTokens)
                        {
                                foreach (AToken t in compiler.TokenList)
                                {
                                        Console.WriteLine("TokenID: "+t.token+"  =  "+t.val);
                                }
                        }
                        compiler.InitTables();
                        if (!compiler.yyparse()) return 1;

                        if (compiler.Output != null) compiler.Output.Close();
			return 0;
                }
                public void yyaction (int yyruleno)
                {
                        switch (yyruleno)
                        {
                                ////////////////////////////////////////////////////////////////
                                /// YYAction code:
                                ////////////////////////////////////////////////////////////////
							case    1 : 
         yyval = yyv[yysp-0];
         //Output.WriteLine(yyval);
         
       break;
							case    2 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case    3 : 
         yyval = "";
         
       break;
							case    4 : 
         yyval = yyv[yysp-0];
         
       break;
							case    5 : 
         yyval = yyv[yysp-0];
         
       break;
							case    6 : 
         yyval = yyv[yysp-0];
         
       break;
							case    7 : 
         yyval = yyv[yysp-0];
         
       break;
							case    8 : 
         yyval = yyv[yysp-0];
         
       break;
							case    9 : 
         yyval = yyv[yysp-0];
         
       break;
							case   10 : 
         yyval = yyv[yysp-0];
         
       break;
							case   11 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   12 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   13 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   14 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   15 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   16 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   17 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   18 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   19 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   20 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   21 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   22 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   23 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   24 : 
         yyval = yyv[yysp-0];
         
       break;
							case   25 : 
         yyval = yyv[yysp-0];
         
       break;
							case   26 : 
         yyval = yyv[yysp-0];
         
       break;
							case   27 : 
         yyval = yyv[yysp-0];
         
       break;
							case   28 : 
         yyval = yyv[yysp-12] + yyv[yysp-11] + yyv[yysp-10] + yyv[yysp-9] + yyv[yysp-8] + yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   29 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   30 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   31 : 
         yyval = yyv[yysp-0];
         
       break;
							case   32 : 
         yyval = yyv[yysp-0];
         
       break;
							case   33 : 
         yyval = yyv[yysp-0];
         
       break;
							case   34 : 
         yyval = yyv[yysp-0];
         
       break;
							case   35 : 
         yyval = yyv[yysp-0];
         
       break;
							case   36 : 
         yyval = yyv[yysp-0];
         
       break;
							case   37 : 
         yyval = yyv[yysp-0];
         
       break;
							case   38 : 
         yyval = yyv[yysp-16] + yyv[yysp-15] + yyv[yysp-14] + yyv[yysp-13] + yyv[yysp-12] + yyv[yysp-11] + yyv[yysp-10] + yyv[yysp-9] + yyv[yysp-8] + yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   39 : 
         yyval = yyv[yysp-8] + yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   40 : 
         yyval = yyv[yysp-0];
         
       break;
							case   41 : 
         yyval = yyv[yysp-0];
         
       break;
							case   42 : 
         yyval = yyv[yysp-0];
         
       break;
							case   43 : 
         yyval = yyv[yysp-0];
         
       break;
							case   44 : 
         yyval = yyv[yysp-0];
         
       break;
							case   45 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   46 : 
         yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   47 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   48 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   49 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   50 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   51 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   52 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   53 : 
         yyval = "";
         
       break;
							case   54 : 
         yyval = yyv[yysp-0];
         
       break;
							case   55 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   56 : 
         yyval = yyv[yysp-0];
         
       break;
							case   57 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   58 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   59 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   60 : 
         yyval = yyv[yysp-0];
         
       break;
							case   61 : 
         yyval = yyv[yysp-0];
         
       break;
							case   62 : 
         yyval = yyv[yysp-0];
         
       break;
							case   63 : 
         yyval = yyv[yysp-0];
         
       break;
							case   64 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   65 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   66 : 
         yyval = yyv[yysp-0];
         
       break;
							case   67 : 
         yyval = yyv[yysp-0];
         
       break;
							case   68 : 
         yyval = yyv[yysp-0];
         
       break;
							case   69 : 
         yyval = "";
         
       break;
							case   70 : 
         yyval = yyv[yysp-0];
         
       break;
							case   71 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   72 : 
         yyval = yyv[yysp-0];
         
       break;
							case   73 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   74 : 
         yyval = yyv[yysp-0];
         
       break;
							case   75 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   76 : 
         yyval = yyv[yysp-0];
         
       break;
							case   77 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   78 : 
         yyval = yyv[yysp-0];
         
       break;
							case   79 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   80 : 
         yyval = yyv[yysp-0];
         
       break;
							case   81 : 
         yyval = yyv[yysp-0];
         
       break;
							case   82 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   83 : 
         yyval = yyv[yysp-0];
         
       break;
							case   84 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   85 : 
         yyval = yyv[yysp-0];
         
       break;
							case   86 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   87 : 
         yyval = yyv[yysp-0];
         
       break;
							case   88 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   89 : 
         yyval = yyv[yysp-0];
         
       break;
							case   90 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   91 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   92 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   93 : 
         yyval = yyv[yysp-0];
         
       break;
							case   94 : 
         yyval = yyv[yysp-0];
         
       break;
							case   95 : 
         yyval = yyv[yysp-0];
         
       break;
							case   96 : 
         yyval = yyv[yysp-0];
         
       break;
							case   97 : 
         yyval = yyv[yysp-0];
         
       break;
							case   98 : 
         yyval = yyv[yysp-0];
         
       break;
							case   99 : 
         yyval = yyv[yysp-0];
         
       break;
							case  100 : 
         yyval = yyv[yysp-0];
         
       break;
							case  101 : 
         yyval = yyv[yysp-0];
         
       break;
							case  102 : 
         yyval = yyv[yysp-0];
         
       break;
							case  103 : 
         yyval = yyv[yysp-0];
         
       break;
							case  104 : 
         yyval = yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-0];
         
       break;
							case  107 : 
         yyval = yyv[yysp-0];
         
       break;
							case  108 : 
         yyval = yyv[yysp-0];
         
       break;
							case  109 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  110 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  111 : 
         yyval = yyv[yysp-0];
         
       break;
							case  112 : 
         yyval = yyv[yysp-0];
         
       break;
							case  113 : 
         yyval = yyv[yysp-0];
         
       break;
							case  114 : 
         yyval = yyv[yysp-0];
         
       break;
							case  115 : 
         yyval = yyv[yysp-0];
         
       break;
							case  116 : 
         yyval = yyv[yysp-0];
         
       break;
							case  117 : 
         yyval = yyv[yysp-0];
         
       break;
							case  118 : 
         yyval = yyv[yysp-0];
         
       break;
							case  119 : 
         yyval = yyv[yysp-0];
         
       break;
							case  120 : 
         yyval = yyv[yysp-0];
         
       break;
							case  121 : 
         yyval = yyv[yysp-0];
         
       break;
							case  122 : 
         yyval = yyv[yysp-0];
         
       break;
							case  123 : 
         yyval = yyv[yysp-0];
         
       break;
							case  124 : 
         yyval = yyv[yysp-0];
         
       break;
							case  125 : 
         yyval = yyv[yysp-0];
         
       break;
							case  126 : 
         yyval = yyv[yysp-0];
         
       break;
							case  127 : 
         yyval = yyv[yysp-0];
         
       break;
							case  128 : 
         yyval = yyv[yysp-0];
         
       break;
							case  129 : 
         yyval = yyv[yysp-0];
         
       break;
							case  130 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  131 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  132 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  133 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  134 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  135 : 
         yyval = yyv[yysp-0];
         
       break;
							case  136 : 
         yyval = yyv[yysp-0];
         
       break;
							case  137 : 
         yyval = yyv[yysp-0];
         
       break;
							case  138 : 
         yyval = yyv[yysp-0];
         
       break;
							case  139 : 
         yyval = yyv[yysp-0];
         
       break;
							case  140 : 
         yyval = yyv[yysp-0];
         
       break;
							case  141 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  142 : 
         yyval = yyv[yysp-0];
         
       break;
							case  143 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  144 : 
         yyval = yyv[yysp-0];
         
       break;
							case  145 : 
         yyval = yyv[yysp-0];
         
       break;
							case  146 : 
         yyval = yyv[yysp-0];
         
       break;
							case  147 : 
         yyval = yyv[yysp-0];
         
       break;
							case  148 : 
         yyval = yyv[yysp-0];
         
       break;
							case  149 : 
         yyval = yyv[yysp-0];
         
       break;
							case  150 : 
         yyval = yyv[yysp-0];
         
       break;
							case  151 : 
         yyval = yyv[yysp-0];
         
       break;
							case  152 : 
         yyval = yyv[yysp-0];
         
       break;
							case  153 : 
         yyval = yyv[yysp-0];
         
       break;
                                default : return;
                        }
                }

                public void InitTables()
                {
                        ////////////////////////////////////////////////////////////////
                        /// Init Table code:
                        ////////////////////////////////////////////////////////////////

					int yynacts   = 1889;
					int yyngotos  = 608;
					int yynstates = 264;
					int yynrules  = 153;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,16);yyac++; 
					yya[yyac] = new YYARec(259,17);yyac++; 
					yya[yyac] = new YYARec(262,18);yyac++; 
					yya[yyac] = new YYARec(264,19);yyac++; 
					yya[yyac] = new YYARec(265,20);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(322,44);yyac++; 
					yya[yyac] = new YYARec(324,45);yyac++; 
					yya[yyac] = new YYARec(322,44);yyac++; 
					yya[yyac] = new YYARec(324,45);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,67);yyac++; 
					yya[yyac] = new YYARec(323,68);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(325,70);yyac++; 
					yya[yyac] = new YYARec(326,71);yyac++; 
					yya[yyac] = new YYARec(257,16);yyac++; 
					yya[yyac] = new YYARec(259,17);yyac++; 
					yya[yyac] = new YYARec(262,18);yyac++; 
					yya[yyac] = new YYARec(264,19);yyac++; 
					yya[yyac] = new YYARec(265,20);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(325,70);yyac++; 
					yya[yyac] = new YYARec(322,44);yyac++; 
					yya[yyac] = new YYARec(324,45);yyac++; 
					yya[yyac] = new YYARec(263,80);yyac++; 
					yya[yyac] = new YYARec(325,-69 );yyac++; 
					yya[yyac] = new YYARec(263,80);yyac++; 
					yya[yyac] = new YYARec(325,-69 );yyac++; 
					yya[yyac] = new YYARec(322,82);yyac++; 
					yya[yyac] = new YYARec(323,83);yyac++; 
					yya[yyac] = new YYARec(263,80);yyac++; 
					yya[yyac] = new YYARec(321,85);yyac++; 
					yya[yyac] = new YYARec(266,-69 );yyac++; 
					yya[yyac] = new YYARec(267,-69 );yyac++; 
					yya[yyac] = new YYARec(268,-69 );yyac++; 
					yya[yyac] = new YYARec(269,-69 );yyac++; 
					yya[yyac] = new YYARec(270,-69 );yyac++; 
					yya[yyac] = new YYARec(271,-69 );yyac++; 
					yya[yyac] = new YYARec(272,-69 );yyac++; 
					yya[yyac] = new YYARec(273,-69 );yyac++; 
					yya[yyac] = new YYARec(274,-69 );yyac++; 
					yya[yyac] = new YYARec(275,-69 );yyac++; 
					yya[yyac] = new YYARec(278,-69 );yyac++; 
					yya[yyac] = new YYARec(292,-69 );yyac++; 
					yya[yyac] = new YYARec(293,-69 );yyac++; 
					yya[yyac] = new YYARec(297,-69 );yyac++; 
					yya[yyac] = new YYARec(304,-69 );yyac++; 
					yya[yyac] = new YYARec(305,-69 );yyac++; 
					yya[yyac] = new YYARec(306,-69 );yyac++; 
					yya[yyac] = new YYARec(307,-69 );yyac++; 
					yya[yyac] = new YYARec(308,-69 );yyac++; 
					yya[yyac] = new YYARec(309,-69 );yyac++; 
					yya[yyac] = new YYARec(310,-69 );yyac++; 
					yya[yyac] = new YYARec(311,-69 );yyac++; 
					yya[yyac] = new YYARec(312,-69 );yyac++; 
					yya[yyac] = new YYARec(313,-69 );yyac++; 
					yya[yyac] = new YYARec(314,-69 );yyac++; 
					yya[yyac] = new YYARec(315,-69 );yyac++; 
					yya[yyac] = new YYARec(316,-69 );yyac++; 
					yya[yyac] = new YYARec(317,-69 );yyac++; 
					yya[yyac] = new YYARec(322,-69 );yyac++; 
					yya[yyac] = new YYARec(323,-69 );yyac++; 
					yya[yyac] = new YYARec(324,-69 );yyac++; 
					yya[yyac] = new YYARec(325,-69 );yyac++; 
					yya[yyac] = new YYARec(326,-69 );yyac++; 
					yya[yyac] = new YYARec(258,-136 );yyac++; 
					yya[yyac] = new YYARec(258,86);yyac++; 
					yya[yyac] = new YYARec(258,87);yyac++; 
					yya[yyac] = new YYARec(258,88);yyac++; 
					yya[yyac] = new YYARec(258,89);yyac++; 
					yya[yyac] = new YYARec(263,90);yyac++; 
					yya[yyac] = new YYARec(258,91);yyac++; 
					yya[yyac] = new YYARec(258,92);yyac++; 
					yya[yyac] = new YYARec(263,80);yyac++; 
					yya[yyac] = new YYARec(325,-69 );yyac++; 
					yya[yyac] = new YYARec(325,70);yyac++; 
					yya[yyac] = new YYARec(325,70);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(275,103);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,67);yyac++; 
					yya[yyac] = new YYARec(323,68);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(325,70);yyac++; 
					yya[yyac] = new YYARec(326,71);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(257,16);yyac++; 
					yya[yyac] = new YYARec(259,17);yyac++; 
					yya[yyac] = new YYARec(262,18);yyac++; 
					yya[yyac] = new YYARec(264,19);yyac++; 
					yya[yyac] = new YYARec(265,20);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(257,16);yyac++; 
					yya[yyac] = new YYARec(259,17);yyac++; 
					yya[yyac] = new YYARec(262,18);yyac++; 
					yya[yyac] = new YYARec(264,19);yyac++; 
					yya[yyac] = new YYARec(265,20);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,67);yyac++; 
					yya[yyac] = new YYARec(323,68);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(325,70);yyac++; 
					yya[yyac] = new YYARec(326,71);yyac++; 
					yya[yyac] = new YYARec(325,70);yyac++; 
					yya[yyac] = new YYARec(258,111);yyac++; 
					yya[yyac] = new YYARec(258,113);yyac++; 
					yya[yyac] = new YYARec(263,80);yyac++; 
					yya[yyac] = new YYARec(292,-69 );yyac++; 
					yya[yyac] = new YYARec(293,-69 );yyac++; 
					yya[yyac] = new YYARec(297,-69 );yyac++; 
					yya[yyac] = new YYARec(322,-69 );yyac++; 
					yya[yyac] = new YYARec(258,114);yyac++; 
					yya[yyac] = new YYARec(321,85);yyac++; 
					yya[yyac] = new YYARec(258,-136 );yyac++; 
					yya[yyac] = new YYARec(263,-136 );yyac++; 
					yya[yyac] = new YYARec(266,-136 );yyac++; 
					yya[yyac] = new YYARec(267,-136 );yyac++; 
					yya[yyac] = new YYARec(268,-136 );yyac++; 
					yya[yyac] = new YYARec(269,-136 );yyac++; 
					yya[yyac] = new YYARec(270,-136 );yyac++; 
					yya[yyac] = new YYARec(271,-136 );yyac++; 
					yya[yyac] = new YYARec(272,-136 );yyac++; 
					yya[yyac] = new YYARec(273,-136 );yyac++; 
					yya[yyac] = new YYARec(274,-136 );yyac++; 
					yya[yyac] = new YYARec(275,-136 );yyac++; 
					yya[yyac] = new YYARec(278,-136 );yyac++; 
					yya[yyac] = new YYARec(279,-136 );yyac++; 
					yya[yyac] = new YYARec(280,-136 );yyac++; 
					yya[yyac] = new YYARec(281,-136 );yyac++; 
					yya[yyac] = new YYARec(282,-136 );yyac++; 
					yya[yyac] = new YYARec(283,-136 );yyac++; 
					yya[yyac] = new YYARec(285,-136 );yyac++; 
					yya[yyac] = new YYARec(286,-136 );yyac++; 
					yya[yyac] = new YYARec(287,-136 );yyac++; 
					yya[yyac] = new YYARec(288,-136 );yyac++; 
					yya[yyac] = new YYARec(289,-136 );yyac++; 
					yya[yyac] = new YYARec(290,-136 );yyac++; 
					yya[yyac] = new YYARec(291,-136 );yyac++; 
					yya[yyac] = new YYARec(292,-136 );yyac++; 
					yya[yyac] = new YYARec(293,-136 );yyac++; 
					yya[yyac] = new YYARec(294,-136 );yyac++; 
					yya[yyac] = new YYARec(295,-136 );yyac++; 
					yya[yyac] = new YYARec(296,-136 );yyac++; 
					yya[yyac] = new YYARec(297,-136 );yyac++; 
					yya[yyac] = new YYARec(299,-136 );yyac++; 
					yya[yyac] = new YYARec(300,-136 );yyac++; 
					yya[yyac] = new YYARec(301,-136 );yyac++; 
					yya[yyac] = new YYARec(302,-136 );yyac++; 
					yya[yyac] = new YYARec(303,-136 );yyac++; 
					yya[yyac] = new YYARec(304,-136 );yyac++; 
					yya[yyac] = new YYARec(305,-136 );yyac++; 
					yya[yyac] = new YYARec(306,-136 );yyac++; 
					yya[yyac] = new YYARec(307,-136 );yyac++; 
					yya[yyac] = new YYARec(308,-136 );yyac++; 
					yya[yyac] = new YYARec(309,-136 );yyac++; 
					yya[yyac] = new YYARec(310,-136 );yyac++; 
					yya[yyac] = new YYARec(311,-136 );yyac++; 
					yya[yyac] = new YYARec(312,-136 );yyac++; 
					yya[yyac] = new YYARec(313,-136 );yyac++; 
					yya[yyac] = new YYARec(314,-136 );yyac++; 
					yya[yyac] = new YYARec(315,-136 );yyac++; 
					yya[yyac] = new YYARec(316,-136 );yyac++; 
					yya[yyac] = new YYARec(317,-136 );yyac++; 
					yya[yyac] = new YYARec(322,-136 );yyac++; 
					yya[yyac] = new YYARec(323,-136 );yyac++; 
					yya[yyac] = new YYARec(324,-136 );yyac++; 
					yya[yyac] = new YYARec(325,-136 );yyac++; 
					yya[yyac] = new YYARec(326,-136 );yyac++; 
					yya[yyac] = new YYARec(263,80);yyac++; 
					yya[yyac] = new YYARec(258,-69 );yyac++; 
					yya[yyac] = new YYARec(266,-69 );yyac++; 
					yya[yyac] = new YYARec(267,-69 );yyac++; 
					yya[yyac] = new YYARec(268,-69 );yyac++; 
					yya[yyac] = new YYARec(269,-69 );yyac++; 
					yya[yyac] = new YYARec(270,-69 );yyac++; 
					yya[yyac] = new YYARec(271,-69 );yyac++; 
					yya[yyac] = new YYARec(272,-69 );yyac++; 
					yya[yyac] = new YYARec(273,-69 );yyac++; 
					yya[yyac] = new YYARec(274,-69 );yyac++; 
					yya[yyac] = new YYARec(278,-69 );yyac++; 
					yya[yyac] = new YYARec(292,-69 );yyac++; 
					yya[yyac] = new YYARec(293,-69 );yyac++; 
					yya[yyac] = new YYARec(297,-69 );yyac++; 
					yya[yyac] = new YYARec(304,-69 );yyac++; 
					yya[yyac] = new YYARec(305,-69 );yyac++; 
					yya[yyac] = new YYARec(306,-69 );yyac++; 
					yya[yyac] = new YYARec(307,-69 );yyac++; 
					yya[yyac] = new YYARec(308,-69 );yyac++; 
					yya[yyac] = new YYARec(309,-69 );yyac++; 
					yya[yyac] = new YYARec(310,-69 );yyac++; 
					yya[yyac] = new YYARec(311,-69 );yyac++; 
					yya[yyac] = new YYARec(312,-69 );yyac++; 
					yya[yyac] = new YYARec(313,-69 );yyac++; 
					yya[yyac] = new YYARec(314,-69 );yyac++; 
					yya[yyac] = new YYARec(315,-69 );yyac++; 
					yya[yyac] = new YYARec(316,-69 );yyac++; 
					yya[yyac] = new YYARec(317,-69 );yyac++; 
					yya[yyac] = new YYARec(322,-69 );yyac++; 
					yya[yyac] = new YYARec(323,-69 );yyac++; 
					yya[yyac] = new YYARec(324,-69 );yyac++; 
					yya[yyac] = new YYARec(325,-69 );yyac++; 
					yya[yyac] = new YYARec(326,-69 );yyac++; 
					yya[yyac] = new YYARec(257,123);yyac++; 
					yya[yyac] = new YYARec(259,124);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(275,125);yyac++; 
					yya[yyac] = new YYARec(298,126);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,127);yyac++; 
					yya[yyac] = new YYARec(319,128);yyac++; 
					yya[yyac] = new YYARec(320,129);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(276,-53 );yyac++; 
					yya[yyac] = new YYARec(260,130);yyac++; 
					yya[yyac] = new YYARec(261,131);yyac++; 
					yya[yyac] = new YYARec(258,132);yyac++; 
					yya[yyac] = new YYARec(263,80);yyac++; 
					yya[yyac] = new YYARec(292,-69 );yyac++; 
					yya[yyac] = new YYARec(293,-69 );yyac++; 
					yya[yyac] = new YYARec(297,-69 );yyac++; 
					yya[yyac] = new YYARec(322,-69 );yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(322,67);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,67);yyac++; 
					yya[yyac] = new YYARec(323,68);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(325,70);yyac++; 
					yya[yyac] = new YYARec(326,71);yyac++; 
					yya[yyac] = new YYARec(258,-58 );yyac++; 
					yya[yyac] = new YYARec(257,123);yyac++; 
					yya[yyac] = new YYARec(259,124);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(275,125);yyac++; 
					yya[yyac] = new YYARec(298,126);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,127);yyac++; 
					yya[yyac] = new YYARec(319,128);yyac++; 
					yya[yyac] = new YYARec(320,129);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(276,-53 );yyac++; 
					yya[yyac] = new YYARec(258,138);yyac++; 
					yya[yyac] = new YYARec(263,80);yyac++; 
					yya[yyac] = new YYARec(277,140);yyac++; 
					yya[yyac] = new YYARec(278,-69 );yyac++; 
					yya[yyac] = new YYARec(322,-69 );yyac++; 
					yya[yyac] = new YYARec(324,-69 );yyac++; 
					yya[yyac] = new YYARec(276,141);yyac++; 
					yya[yyac] = new YYARec(257,123);yyac++; 
					yya[yyac] = new YYARec(259,124);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(275,125);yyac++; 
					yya[yyac] = new YYARec(298,126);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,127);yyac++; 
					yya[yyac] = new YYARec(319,128);yyac++; 
					yya[yyac] = new YYARec(320,129);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(276,-53 );yyac++; 
					yya[yyac] = new YYARec(263,80);yyac++; 
					yya[yyac] = new YYARec(277,144);yyac++; 
					yya[yyac] = new YYARec(258,-56 );yyac++; 
					yya[yyac] = new YYARec(266,-69 );yyac++; 
					yya[yyac] = new YYARec(267,-69 );yyac++; 
					yya[yyac] = new YYARec(268,-69 );yyac++; 
					yya[yyac] = new YYARec(269,-69 );yyac++; 
					yya[yyac] = new YYARec(270,-69 );yyac++; 
					yya[yyac] = new YYARec(271,-69 );yyac++; 
					yya[yyac] = new YYARec(272,-69 );yyac++; 
					yya[yyac] = new YYARec(273,-69 );yyac++; 
					yya[yyac] = new YYARec(274,-69 );yyac++; 
					yya[yyac] = new YYARec(278,-69 );yyac++; 
					yya[yyac] = new YYARec(292,-69 );yyac++; 
					yya[yyac] = new YYARec(293,-69 );yyac++; 
					yya[yyac] = new YYARec(297,-69 );yyac++; 
					yya[yyac] = new YYARec(304,-69 );yyac++; 
					yya[yyac] = new YYARec(305,-69 );yyac++; 
					yya[yyac] = new YYARec(306,-69 );yyac++; 
					yya[yyac] = new YYARec(307,-69 );yyac++; 
					yya[yyac] = new YYARec(308,-69 );yyac++; 
					yya[yyac] = new YYARec(309,-69 );yyac++; 
					yya[yyac] = new YYARec(310,-69 );yyac++; 
					yya[yyac] = new YYARec(311,-69 );yyac++; 
					yya[yyac] = new YYARec(312,-69 );yyac++; 
					yya[yyac] = new YYARec(313,-69 );yyac++; 
					yya[yyac] = new YYARec(314,-69 );yyac++; 
					yya[yyac] = new YYARec(315,-69 );yyac++; 
					yya[yyac] = new YYARec(316,-69 );yyac++; 
					yya[yyac] = new YYARec(317,-69 );yyac++; 
					yya[yyac] = new YYARec(322,-69 );yyac++; 
					yya[yyac] = new YYARec(323,-69 );yyac++; 
					yya[yyac] = new YYARec(324,-69 );yyac++; 
					yya[yyac] = new YYARec(325,-69 );yyac++; 
					yya[yyac] = new YYARec(326,-69 );yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(257,123);yyac++; 
					yya[yyac] = new YYARec(259,124);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(275,125);yyac++; 
					yya[yyac] = new YYARec(298,126);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,127);yyac++; 
					yya[yyac] = new YYARec(319,128);yyac++; 
					yya[yyac] = new YYARec(320,129);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(276,-53 );yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(275,167);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(257,16);yyac++; 
					yya[yyac] = new YYARec(259,17);yyac++; 
					yya[yyac] = new YYARec(262,18);yyac++; 
					yya[yyac] = new YYARec(264,19);yyac++; 
					yya[yyac] = new YYARec(265,20);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(322,67);yyac++; 
					yya[yyac] = new YYARec(322,82);yyac++; 
					yya[yyac] = new YYARec(263,173);yyac++; 
					yya[yyac] = new YYARec(257,123);yyac++; 
					yya[yyac] = new YYARec(259,124);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(275,125);yyac++; 
					yya[yyac] = new YYARec(298,126);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,127);yyac++; 
					yya[yyac] = new YYARec(319,128);yyac++; 
					yya[yyac] = new YYARec(320,129);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(276,-53 );yyac++; 
					yya[yyac] = new YYARec(278,178);yyac++; 
					yya[yyac] = new YYARec(322,44);yyac++; 
					yya[yyac] = new YYARec(324,45);yyac++; 
					yya[yyac] = new YYARec(257,123);yyac++; 
					yya[yyac] = new YYARec(259,124);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(275,125);yyac++; 
					yya[yyac] = new YYARec(298,126);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,127);yyac++; 
					yya[yyac] = new YYARec(319,128);yyac++; 
					yya[yyac] = new YYARec(320,129);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(276,-53 );yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,67);yyac++; 
					yya[yyac] = new YYARec(323,68);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(325,70);yyac++; 
					yya[yyac] = new YYARec(326,71);yyac++; 
					yya[yyac] = new YYARec(257,123);yyac++; 
					yya[yyac] = new YYARec(259,124);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(275,125);yyac++; 
					yya[yyac] = new YYARec(298,126);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,127);yyac++; 
					yya[yyac] = new YYARec(319,128);yyac++; 
					yya[yyac] = new YYARec(320,129);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(276,-53 );yyac++; 
					yya[yyac] = new YYARec(258,182);yyac++; 
					yya[yyac] = new YYARec(258,183);yyac++; 
					yya[yyac] = new YYARec(276,184);yyac++; 
					yya[yyac] = new YYARec(284,185);yyac++; 
					yya[yyac] = new YYARec(258,-150 );yyac++; 
					yya[yyac] = new YYARec(275,-150 );yyac++; 
					yya[yyac] = new YYARec(279,-150 );yyac++; 
					yya[yyac] = new YYARec(280,-150 );yyac++; 
					yya[yyac] = new YYARec(281,-150 );yyac++; 
					yya[yyac] = new YYARec(282,-150 );yyac++; 
					yya[yyac] = new YYARec(283,-150 );yyac++; 
					yya[yyac] = new YYARec(285,-150 );yyac++; 
					yya[yyac] = new YYARec(286,-150 );yyac++; 
					yya[yyac] = new YYARec(287,-150 );yyac++; 
					yya[yyac] = new YYARec(288,-150 );yyac++; 
					yya[yyac] = new YYARec(289,-150 );yyac++; 
					yya[yyac] = new YYARec(290,-150 );yyac++; 
					yya[yyac] = new YYARec(291,-150 );yyac++; 
					yya[yyac] = new YYARec(292,-150 );yyac++; 
					yya[yyac] = new YYARec(293,-150 );yyac++; 
					yya[yyac] = new YYARec(294,-150 );yyac++; 
					yya[yyac] = new YYARec(295,-150 );yyac++; 
					yya[yyac] = new YYARec(296,-150 );yyac++; 
					yya[yyac] = new YYARec(299,-150 );yyac++; 
					yya[yyac] = new YYARec(300,-150 );yyac++; 
					yya[yyac] = new YYARec(301,-150 );yyac++; 
					yya[yyac] = new YYARec(302,-150 );yyac++; 
					yya[yyac] = new YYARec(303,-150 );yyac++; 
					yya[yyac] = new YYARec(321,-150 );yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(294,188);yyac++; 
					yya[yyac] = new YYARec(295,189);yyac++; 
					yya[yyac] = new YYARec(296,190);yyac++; 
					yya[yyac] = new YYARec(258,-85 );yyac++; 
					yya[yyac] = new YYARec(275,-85 );yyac++; 
					yya[yyac] = new YYARec(279,-85 );yyac++; 
					yya[yyac] = new YYARec(280,-85 );yyac++; 
					yya[yyac] = new YYARec(281,-85 );yyac++; 
					yya[yyac] = new YYARec(282,-85 );yyac++; 
					yya[yyac] = new YYARec(283,-85 );yyac++; 
					yya[yyac] = new YYARec(285,-85 );yyac++; 
					yya[yyac] = new YYARec(286,-85 );yyac++; 
					yya[yyac] = new YYARec(287,-85 );yyac++; 
					yya[yyac] = new YYARec(288,-85 );yyac++; 
					yya[yyac] = new YYARec(289,-85 );yyac++; 
					yya[yyac] = new YYARec(290,-85 );yyac++; 
					yya[yyac] = new YYARec(291,-85 );yyac++; 
					yya[yyac] = new YYARec(292,-85 );yyac++; 
					yya[yyac] = new YYARec(293,-85 );yyac++; 
					yya[yyac] = new YYARec(292,192);yyac++; 
					yya[yyac] = new YYARec(293,193);yyac++; 
					yya[yyac] = new YYARec(258,-83 );yyac++; 
					yya[yyac] = new YYARec(275,-83 );yyac++; 
					yya[yyac] = new YYARec(279,-83 );yyac++; 
					yya[yyac] = new YYARec(280,-83 );yyac++; 
					yya[yyac] = new YYARec(281,-83 );yyac++; 
					yya[yyac] = new YYARec(282,-83 );yyac++; 
					yya[yyac] = new YYARec(283,-83 );yyac++; 
					yya[yyac] = new YYARec(285,-83 );yyac++; 
					yya[yyac] = new YYARec(286,-83 );yyac++; 
					yya[yyac] = new YYARec(287,-83 );yyac++; 
					yya[yyac] = new YYARec(288,-83 );yyac++; 
					yya[yyac] = new YYARec(289,-83 );yyac++; 
					yya[yyac] = new YYARec(290,-83 );yyac++; 
					yya[yyac] = new YYARec(291,-83 );yyac++; 
					yya[yyac] = new YYARec(288,195);yyac++; 
					yya[yyac] = new YYARec(289,196);yyac++; 
					yya[yyac] = new YYARec(290,197);yyac++; 
					yya[yyac] = new YYARec(291,198);yyac++; 
					yya[yyac] = new YYARec(258,-81 );yyac++; 
					yya[yyac] = new YYARec(275,-81 );yyac++; 
					yya[yyac] = new YYARec(279,-81 );yyac++; 
					yya[yyac] = new YYARec(280,-81 );yyac++; 
					yya[yyac] = new YYARec(281,-81 );yyac++; 
					yya[yyac] = new YYARec(282,-81 );yyac++; 
					yya[yyac] = new YYARec(283,-81 );yyac++; 
					yya[yyac] = new YYARec(285,-81 );yyac++; 
					yya[yyac] = new YYARec(286,-81 );yyac++; 
					yya[yyac] = new YYARec(287,-81 );yyac++; 
					yya[yyac] = new YYARec(286,200);yyac++; 
					yya[yyac] = new YYARec(287,201);yyac++; 
					yya[yyac] = new YYARec(258,-80 );yyac++; 
					yya[yyac] = new YYARec(275,-80 );yyac++; 
					yya[yyac] = new YYARec(279,-80 );yyac++; 
					yya[yyac] = new YYARec(280,-80 );yyac++; 
					yya[yyac] = new YYARec(281,-80 );yyac++; 
					yya[yyac] = new YYARec(282,-80 );yyac++; 
					yya[yyac] = new YYARec(283,-80 );yyac++; 
					yya[yyac] = new YYARec(285,-80 );yyac++; 
					yya[yyac] = new YYARec(283,202);yyac++; 
					yya[yyac] = new YYARec(258,-78 );yyac++; 
					yya[yyac] = new YYARec(275,-78 );yyac++; 
					yya[yyac] = new YYARec(279,-78 );yyac++; 
					yya[yyac] = new YYARec(280,-78 );yyac++; 
					yya[yyac] = new YYARec(281,-78 );yyac++; 
					yya[yyac] = new YYARec(282,-78 );yyac++; 
					yya[yyac] = new YYARec(285,-78 );yyac++; 
					yya[yyac] = new YYARec(282,203);yyac++; 
					yya[yyac] = new YYARec(258,-76 );yyac++; 
					yya[yyac] = new YYARec(275,-76 );yyac++; 
					yya[yyac] = new YYARec(279,-76 );yyac++; 
					yya[yyac] = new YYARec(280,-76 );yyac++; 
					yya[yyac] = new YYARec(281,-76 );yyac++; 
					yya[yyac] = new YYARec(285,-76 );yyac++; 
					yya[yyac] = new YYARec(281,204);yyac++; 
					yya[yyac] = new YYARec(258,-74 );yyac++; 
					yya[yyac] = new YYARec(275,-74 );yyac++; 
					yya[yyac] = new YYARec(279,-74 );yyac++; 
					yya[yyac] = new YYARec(280,-74 );yyac++; 
					yya[yyac] = new YYARec(285,-74 );yyac++; 
					yya[yyac] = new YYARec(280,205);yyac++; 
					yya[yyac] = new YYARec(258,-72 );yyac++; 
					yya[yyac] = new YYARec(275,-72 );yyac++; 
					yya[yyac] = new YYARec(279,-72 );yyac++; 
					yya[yyac] = new YYARec(285,-72 );yyac++; 
					yya[yyac] = new YYARec(279,206);yyac++; 
					yya[yyac] = new YYARec(258,-70 );yyac++; 
					yya[yyac] = new YYARec(275,-70 );yyac++; 
					yya[yyac] = new YYARec(285,-70 );yyac++; 
					yya[yyac] = new YYARec(299,208);yyac++; 
					yya[yyac] = new YYARec(300,209);yyac++; 
					yya[yyac] = new YYARec(301,210);yyac++; 
					yya[yyac] = new YYARec(302,211);yyac++; 
					yya[yyac] = new YYARec(303,212);yyac++; 
					yya[yyac] = new YYARec(258,-94 );yyac++; 
					yya[yyac] = new YYARec(279,-94 );yyac++; 
					yya[yyac] = new YYARec(280,-94 );yyac++; 
					yya[yyac] = new YYARec(281,-94 );yyac++; 
					yya[yyac] = new YYARec(282,-94 );yyac++; 
					yya[yyac] = new YYARec(283,-94 );yyac++; 
					yya[yyac] = new YYARec(286,-94 );yyac++; 
					yya[yyac] = new YYARec(287,-94 );yyac++; 
					yya[yyac] = new YYARec(288,-94 );yyac++; 
					yya[yyac] = new YYARec(289,-94 );yyac++; 
					yya[yyac] = new YYARec(290,-94 );yyac++; 
					yya[yyac] = new YYARec(291,-94 );yyac++; 
					yya[yyac] = new YYARec(292,-94 );yyac++; 
					yya[yyac] = new YYARec(293,-94 );yyac++; 
					yya[yyac] = new YYARec(294,-94 );yyac++; 
					yya[yyac] = new YYARec(295,-94 );yyac++; 
					yya[yyac] = new YYARec(296,-94 );yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(257,123);yyac++; 
					yya[yyac] = new YYARec(259,124);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(275,125);yyac++; 
					yya[yyac] = new YYARec(298,126);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,127);yyac++; 
					yya[yyac] = new YYARec(319,128);yyac++; 
					yya[yyac] = new YYARec(320,129);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(276,-53 );yyac++; 
					yya[yyac] = new YYARec(275,215);yyac++; 
					yya[yyac] = new YYARec(275,216);yyac++; 
					yya[yyac] = new YYARec(261,217);yyac++; 
					yya[yyac] = new YYARec(263,218);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(322,67);yyac++; 
					yya[yyac] = new YYARec(263,80);yyac++; 
					yya[yyac] = new YYARec(258,-69 );yyac++; 
					yya[yyac] = new YYARec(278,-69 );yyac++; 
					yya[yyac] = new YYARec(322,-69 );yyac++; 
					yya[yyac] = new YYARec(324,-69 );yyac++; 
					yya[yyac] = new YYARec(257,123);yyac++; 
					yya[yyac] = new YYARec(259,124);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(275,125);yyac++; 
					yya[yyac] = new YYARec(298,126);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,127);yyac++; 
					yya[yyac] = new YYARec(319,128);yyac++; 
					yya[yyac] = new YYARec(320,129);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(257,123);yyac++; 
					yya[yyac] = new YYARec(259,124);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(275,125);yyac++; 
					yya[yyac] = new YYARec(298,126);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,127);yyac++; 
					yya[yyac] = new YYARec(319,128);yyac++; 
					yya[yyac] = new YYARec(320,129);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(257,123);yyac++; 
					yya[yyac] = new YYARec(259,124);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(275,125);yyac++; 
					yya[yyac] = new YYARec(298,126);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,127);yyac++; 
					yya[yyac] = new YYARec(319,128);yyac++; 
					yya[yyac] = new YYARec(320,129);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(276,-53 );yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(278,63);yyac++; 
					yya[yyac] = new YYARec(284,164);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(322,165);yyac++; 
					yya[yyac] = new YYARec(323,166);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(285,236);yyac++; 
					yya[yyac] = new YYARec(276,237);yyac++; 
					yya[yyac] = new YYARec(257,123);yyac++; 
					yya[yyac] = new YYARec(259,124);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(275,125);yyac++; 
					yya[yyac] = new YYARec(298,126);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,127);yyac++; 
					yya[yyac] = new YYARec(319,128);yyac++; 
					yya[yyac] = new YYARec(320,129);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(276,-53 );yyac++; 
					yya[yyac] = new YYARec(257,123);yyac++; 
					yya[yyac] = new YYARec(259,124);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(275,125);yyac++; 
					yya[yyac] = new YYARec(298,126);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,127);yyac++; 
					yya[yyac] = new YYARec(319,128);yyac++; 
					yya[yyac] = new YYARec(320,129);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(276,-53 );yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(322,67);yyac++; 
					yya[yyac] = new YYARec(263,241);yyac++; 
					yya[yyac] = new YYARec(278,178);yyac++; 
					yya[yyac] = new YYARec(322,44);yyac++; 
					yya[yyac] = new YYARec(324,45);yyac++; 
					yya[yyac] = new YYARec(258,-64 );yyac++; 
					yya[yyac] = new YYARec(260,243);yyac++; 
					yya[yyac] = new YYARec(261,244);yyac++; 
					yya[yyac] = new YYARec(285,245);yyac++; 
					yya[yyac] = new YYARec(294,188);yyac++; 
					yya[yyac] = new YYARec(295,189);yyac++; 
					yya[yyac] = new YYARec(296,190);yyac++; 
					yya[yyac] = new YYARec(258,-86 );yyac++; 
					yya[yyac] = new YYARec(275,-86 );yyac++; 
					yya[yyac] = new YYARec(279,-86 );yyac++; 
					yya[yyac] = new YYARec(280,-86 );yyac++; 
					yya[yyac] = new YYARec(281,-86 );yyac++; 
					yya[yyac] = new YYARec(282,-86 );yyac++; 
					yya[yyac] = new YYARec(283,-86 );yyac++; 
					yya[yyac] = new YYARec(285,-86 );yyac++; 
					yya[yyac] = new YYARec(286,-86 );yyac++; 
					yya[yyac] = new YYARec(287,-86 );yyac++; 
					yya[yyac] = new YYARec(288,-86 );yyac++; 
					yya[yyac] = new YYARec(289,-86 );yyac++; 
					yya[yyac] = new YYARec(290,-86 );yyac++; 
					yya[yyac] = new YYARec(291,-86 );yyac++; 
					yya[yyac] = new YYARec(292,-86 );yyac++; 
					yya[yyac] = new YYARec(293,-86 );yyac++; 
					yya[yyac] = new YYARec(292,192);yyac++; 
					yya[yyac] = new YYARec(293,193);yyac++; 
					yya[yyac] = new YYARec(258,-84 );yyac++; 
					yya[yyac] = new YYARec(275,-84 );yyac++; 
					yya[yyac] = new YYARec(279,-84 );yyac++; 
					yya[yyac] = new YYARec(280,-84 );yyac++; 
					yya[yyac] = new YYARec(281,-84 );yyac++; 
					yya[yyac] = new YYARec(282,-84 );yyac++; 
					yya[yyac] = new YYARec(283,-84 );yyac++; 
					yya[yyac] = new YYARec(285,-84 );yyac++; 
					yya[yyac] = new YYARec(286,-84 );yyac++; 
					yya[yyac] = new YYARec(287,-84 );yyac++; 
					yya[yyac] = new YYARec(288,-84 );yyac++; 
					yya[yyac] = new YYARec(289,-84 );yyac++; 
					yya[yyac] = new YYARec(290,-84 );yyac++; 
					yya[yyac] = new YYARec(291,-84 );yyac++; 
					yya[yyac] = new YYARec(288,195);yyac++; 
					yya[yyac] = new YYARec(289,196);yyac++; 
					yya[yyac] = new YYARec(290,197);yyac++; 
					yya[yyac] = new YYARec(291,198);yyac++; 
					yya[yyac] = new YYARec(258,-82 );yyac++; 
					yya[yyac] = new YYARec(275,-82 );yyac++; 
					yya[yyac] = new YYARec(279,-82 );yyac++; 
					yya[yyac] = new YYARec(280,-82 );yyac++; 
					yya[yyac] = new YYARec(281,-82 );yyac++; 
					yya[yyac] = new YYARec(282,-82 );yyac++; 
					yya[yyac] = new YYARec(283,-82 );yyac++; 
					yya[yyac] = new YYARec(285,-82 );yyac++; 
					yya[yyac] = new YYARec(286,-82 );yyac++; 
					yya[yyac] = new YYARec(287,-82 );yyac++; 
					yya[yyac] = new YYARec(286,200);yyac++; 
					yya[yyac] = new YYARec(287,201);yyac++; 
					yya[yyac] = new YYARec(258,-79 );yyac++; 
					yya[yyac] = new YYARec(275,-79 );yyac++; 
					yya[yyac] = new YYARec(279,-79 );yyac++; 
					yya[yyac] = new YYARec(280,-79 );yyac++; 
					yya[yyac] = new YYARec(281,-79 );yyac++; 
					yya[yyac] = new YYARec(282,-79 );yyac++; 
					yya[yyac] = new YYARec(283,-79 );yyac++; 
					yya[yyac] = new YYARec(285,-79 );yyac++; 
					yya[yyac] = new YYARec(283,202);yyac++; 
					yya[yyac] = new YYARec(258,-77 );yyac++; 
					yya[yyac] = new YYARec(275,-77 );yyac++; 
					yya[yyac] = new YYARec(279,-77 );yyac++; 
					yya[yyac] = new YYARec(280,-77 );yyac++; 
					yya[yyac] = new YYARec(281,-77 );yyac++; 
					yya[yyac] = new YYARec(282,-77 );yyac++; 
					yya[yyac] = new YYARec(285,-77 );yyac++; 
					yya[yyac] = new YYARec(282,203);yyac++; 
					yya[yyac] = new YYARec(258,-75 );yyac++; 
					yya[yyac] = new YYARec(275,-75 );yyac++; 
					yya[yyac] = new YYARec(279,-75 );yyac++; 
					yya[yyac] = new YYARec(280,-75 );yyac++; 
					yya[yyac] = new YYARec(281,-75 );yyac++; 
					yya[yyac] = new YYARec(285,-75 );yyac++; 
					yya[yyac] = new YYARec(281,204);yyac++; 
					yya[yyac] = new YYARec(258,-73 );yyac++; 
					yya[yyac] = new YYARec(275,-73 );yyac++; 
					yya[yyac] = new YYARec(279,-73 );yyac++; 
					yya[yyac] = new YYARec(280,-73 );yyac++; 
					yya[yyac] = new YYARec(285,-73 );yyac++; 
					yya[yyac] = new YYARec(280,205);yyac++; 
					yya[yyac] = new YYARec(258,-71 );yyac++; 
					yya[yyac] = new YYARec(275,-71 );yyac++; 
					yya[yyac] = new YYARec(279,-71 );yyac++; 
					yya[yyac] = new YYARec(285,-71 );yyac++; 
					yya[yyac] = new YYARec(276,246);yyac++; 
					yya[yyac] = new YYARec(276,247);yyac++; 
					yya[yyac] = new YYARec(258,248);yyac++; 
					yya[yyac] = new YYARec(263,249);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(322,67);yyac++; 
					yya[yyac] = new YYARec(257,123);yyac++; 
					yya[yyac] = new YYARec(259,124);yyac++; 
					yya[yyac] = new YYARec(266,21);yyac++; 
					yya[yyac] = new YYARec(267,22);yyac++; 
					yya[yyac] = new YYARec(268,23);yyac++; 
					yya[yyac] = new YYARec(269,24);yyac++; 
					yya[yyac] = new YYARec(270,25);yyac++; 
					yya[yyac] = new YYARec(271,26);yyac++; 
					yya[yyac] = new YYARec(272,60);yyac++; 
					yya[yyac] = new YYARec(273,61);yyac++; 
					yya[yyac] = new YYARec(274,62);yyac++; 
					yya[yyac] = new YYARec(275,125);yyac++; 
					yya[yyac] = new YYARec(298,126);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,31);yyac++; 
					yya[yyac] = new YYARec(308,32);yyac++; 
					yya[yyac] = new YYARec(309,33);yyac++; 
					yya[yyac] = new YYARec(310,34);yyac++; 
					yya[yyac] = new YYARec(311,35);yyac++; 
					yya[yyac] = new YYARec(312,36);yyac++; 
					yya[yyac] = new YYARec(313,37);yyac++; 
					yya[yyac] = new YYARec(314,38);yyac++; 
					yya[yyac] = new YYARec(315,39);yyac++; 
					yya[yyac] = new YYARec(316,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,127);yyac++; 
					yya[yyac] = new YYARec(319,128);yyac++; 
					yya[yyac] = new YYARec(320,129);yyac++; 
					yya[yyac] = new YYARec(324,42);yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(322,67);yyac++; 
					yya[yyac] = new YYARec(263,253);yyac++; 
					yya[yyac] = new YYARec(261,254);yyac++; 
					yya[yyac] = new YYARec(263,255);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(322,67);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(322,67);yyac++; 
					yya[yyac] = new YYARec(258,258);yyac++; 
					yya[yyac] = new YYARec(263,259);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(322,67);yyac++; 
					yya[yyac] = new YYARec(263,261);yyac++; 
					yya[yyac] = new YYARec(292,64);yyac++; 
					yya[yyac] = new YYARec(293,65);yyac++; 
					yya[yyac] = new YYARec(297,66);yyac++; 
					yya[yyac] = new YYARec(322,67);yyac++; 
					yya[yyac] = new YYARec(258,263);yyac++;

					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-28,2);yygc++; 
					yyg[yygc] = new YYARec(-27,3);yygc++; 
					yyg[yygc] = new YYARec(-23,4);yygc++; 
					yyg[yygc] = new YYARec(-12,5);yygc++; 
					yyg[yygc] = new YYARec(-11,6);yygc++; 
					yyg[yygc] = new YYARec(-10,7);yygc++; 
					yyg[yygc] = new YYARec(-9,8);yygc++; 
					yyg[yygc] = new YYARec(-8,9);yygc++; 
					yyg[yygc] = new YYARec(-7,10);yygc++; 
					yyg[yygc] = new YYARec(-6,11);yygc++; 
					yyg[yygc] = new YYARec(-5,12);yygc++; 
					yyg[yygc] = new YYARec(-4,13);yygc++; 
					yyg[yygc] = new YYARec(-3,14);yygc++; 
					yyg[yygc] = new YYARec(-2,15);yygc++; 
					yyg[yygc] = new YYARec(-24,43);yygc++; 
					yyg[yygc] = new YYARec(-24,46);yygc++; 
					yyg[yygc] = new YYARec(-57,47);yygc++; 
					yyg[yygc] = new YYARec(-54,48);yygc++; 
					yyg[yygc] = new YYARec(-53,49);yygc++; 
					yyg[yygc] = new YYARec(-30,50);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-26,53);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-22,55);yygc++; 
					yyg[yygc] = new YYARec(-21,56);yygc++; 
					yyg[yygc] = new YYARec(-20,57);yygc++; 
					yyg[yygc] = new YYARec(-19,58);yygc++; 
					yyg[yygc] = new YYARec(-18,59);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-28,2);yygc++; 
					yyg[yygc] = new YYARec(-27,3);yygc++; 
					yyg[yygc] = new YYARec(-23,4);yygc++; 
					yyg[yygc] = new YYARec(-12,5);yygc++; 
					yyg[yygc] = new YYARec(-11,6);yygc++; 
					yyg[yygc] = new YYARec(-10,7);yygc++; 
					yyg[yygc] = new YYARec(-9,8);yygc++; 
					yyg[yygc] = new YYARec(-8,9);yygc++; 
					yyg[yygc] = new YYARec(-7,10);yygc++; 
					yyg[yygc] = new YYARec(-6,11);yygc++; 
					yyg[yygc] = new YYARec(-5,12);yygc++; 
					yyg[yygc] = new YYARec(-4,13);yygc++; 
					yyg[yygc] = new YYARec(-3,72);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-12,73);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-12,74);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-12,75);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-12,76);yygc++; 
					yyg[yygc] = new YYARec(-18,77);yygc++; 
					yyg[yygc] = new YYARec(-24,78);yygc++; 
					yyg[yygc] = new YYARec(-25,79);yygc++; 
					yyg[yygc] = new YYARec(-25,81);yygc++; 
					yyg[yygc] = new YYARec(-25,84);yygc++; 
					yyg[yygc] = new YYARec(-25,93);yygc++; 
					yyg[yygc] = new YYARec(-18,94);yygc++; 
					yyg[yygc] = new YYARec(-18,95);yygc++; 
					yyg[yygc] = new YYARec(-57,47);yygc++; 
					yyg[yygc] = new YYARec(-54,48);yygc++; 
					yyg[yygc] = new YYARec(-53,49);yygc++; 
					yyg[yygc] = new YYARec(-31,96);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-26,53);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-22,98);yygc++; 
					yyg[yygc] = new YYARec(-21,99);yygc++; 
					yyg[yygc] = new YYARec(-20,100);yygc++; 
					yyg[yygc] = new YYARec(-18,101);yygc++; 
					yyg[yygc] = new YYARec(-17,102);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-29,104);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-12,105);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-28,2);yygc++; 
					yyg[yygc] = new YYARec(-27,3);yygc++; 
					yyg[yygc] = new YYARec(-23,4);yygc++; 
					yyg[yygc] = new YYARec(-13,106);yygc++; 
					yyg[yygc] = new YYARec(-12,5);yygc++; 
					yyg[yygc] = new YYARec(-11,6);yygc++; 
					yyg[yygc] = new YYARec(-10,7);yygc++; 
					yyg[yygc] = new YYARec(-9,8);yygc++; 
					yyg[yygc] = new YYARec(-8,9);yygc++; 
					yyg[yygc] = new YYARec(-7,10);yygc++; 
					yyg[yygc] = new YYARec(-6,11);yygc++; 
					yyg[yygc] = new YYARec(-5,12);yygc++; 
					yyg[yygc] = new YYARec(-4,13);yygc++; 
					yyg[yygc] = new YYARec(-3,107);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-28,2);yygc++; 
					yyg[yygc] = new YYARec(-27,3);yygc++; 
					yyg[yygc] = new YYARec(-23,4);yygc++; 
					yyg[yygc] = new YYARec(-13,108);yygc++; 
					yyg[yygc] = new YYARec(-12,5);yygc++; 
					yyg[yygc] = new YYARec(-11,6);yygc++; 
					yyg[yygc] = new YYARec(-10,7);yygc++; 
					yyg[yygc] = new YYARec(-9,8);yygc++; 
					yyg[yygc] = new YYARec(-8,9);yygc++; 
					yyg[yygc] = new YYARec(-7,10);yygc++; 
					yyg[yygc] = new YYARec(-6,11);yygc++; 
					yyg[yygc] = new YYARec(-5,12);yygc++; 
					yyg[yygc] = new YYARec(-4,13);yygc++; 
					yyg[yygc] = new YYARec(-3,107);yygc++; 
					yyg[yygc] = new YYARec(-57,47);yygc++; 
					yyg[yygc] = new YYARec(-54,48);yygc++; 
					yyg[yygc] = new YYARec(-53,49);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-26,53);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-22,98);yygc++; 
					yyg[yygc] = new YYARec(-21,99);yygc++; 
					yyg[yygc] = new YYARec(-20,100);yygc++; 
					yyg[yygc] = new YYARec(-18,101);yygc++; 
					yyg[yygc] = new YYARec(-17,109);yygc++; 
					yyg[yygc] = new YYARec(-18,110);yygc++; 
					yyg[yygc] = new YYARec(-25,112);yygc++; 
					yyg[yygc] = new YYARec(-25,115);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-34,116);yygc++; 
					yyg[yygc] = new YYARec(-33,117);yygc++; 
					yyg[yygc] = new YYARec(-32,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-16,120);yygc++; 
					yyg[yygc] = new YYARec(-14,121);yygc++; 
					yyg[yygc] = new YYARec(-12,122);yygc++; 
					yyg[yygc] = new YYARec(-25,133);yygc++; 
					yyg[yygc] = new YYARec(-53,134);yygc++; 
					yyg[yygc] = new YYARec(-26,135);yygc++; 
					yyg[yygc] = new YYARec(-57,47);yygc++; 
					yyg[yygc] = new YYARec(-54,48);yygc++; 
					yyg[yygc] = new YYARec(-53,49);yygc++; 
					yyg[yygc] = new YYARec(-31,136);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-26,53);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-22,98);yygc++; 
					yyg[yygc] = new YYARec(-21,99);yygc++; 
					yyg[yygc] = new YYARec(-20,100);yygc++; 
					yyg[yygc] = new YYARec(-18,101);yygc++; 
					yyg[yygc] = new YYARec(-17,102);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-34,116);yygc++; 
					yyg[yygc] = new YYARec(-33,117);yygc++; 
					yyg[yygc] = new YYARec(-32,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-16,137);yygc++; 
					yyg[yygc] = new YYARec(-14,121);yygc++; 
					yyg[yygc] = new YYARec(-12,122);yygc++; 
					yyg[yygc] = new YYARec(-25,139);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-34,116);yygc++; 
					yyg[yygc] = new YYARec(-33,117);yygc++; 
					yyg[yygc] = new YYARec(-32,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-16,142);yygc++; 
					yyg[yygc] = new YYARec(-14,121);yygc++; 
					yyg[yygc] = new YYARec(-12,122);yygc++; 
					yyg[yygc] = new YYARec(-25,143);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-12,145);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-12,146);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-34,116);yygc++; 
					yyg[yygc] = new YYARec(-33,117);yygc++; 
					yyg[yygc] = new YYARec(-32,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-16,147);yygc++; 
					yyg[yygc] = new YYARec(-14,121);yygc++; 
					yyg[yygc] = new YYARec(-12,122);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-44,155);yygc++; 
					yyg[yygc] = new YYARec(-43,156);yygc++; 
					yyg[yygc] = new YYARec(-42,157);yygc++; 
					yyg[yygc] = new YYARec(-41,158);yygc++; 
					yyg[yygc] = new YYARec(-40,159);yygc++; 
					yyg[yygc] = new YYARec(-39,160);yygc++; 
					yyg[yygc] = new YYARec(-38,161);yygc++; 
					yyg[yygc] = new YYARec(-37,162);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,163);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-44,155);yygc++; 
					yyg[yygc] = new YYARec(-43,156);yygc++; 
					yyg[yygc] = new YYARec(-42,157);yygc++; 
					yyg[yygc] = new YYARec(-41,158);yygc++; 
					yyg[yygc] = new YYARec(-40,159);yygc++; 
					yyg[yygc] = new YYARec(-39,160);yygc++; 
					yyg[yygc] = new YYARec(-38,161);yygc++; 
					yyg[yygc] = new YYARec(-37,168);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,169);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-44,155);yygc++; 
					yyg[yygc] = new YYARec(-43,156);yygc++; 
					yyg[yygc] = new YYARec(-42,157);yygc++; 
					yyg[yygc] = new YYARec(-41,158);yygc++; 
					yyg[yygc] = new YYARec(-40,159);yygc++; 
					yyg[yygc] = new YYARec(-39,160);yygc++; 
					yyg[yygc] = new YYARec(-38,161);yygc++; 
					yyg[yygc] = new YYARec(-37,170);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,169);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-28,2);yygc++; 
					yyg[yygc] = new YYARec(-27,3);yygc++; 
					yyg[yygc] = new YYARec(-23,4);yygc++; 
					yyg[yygc] = new YYARec(-12,5);yygc++; 
					yyg[yygc] = new YYARec(-11,6);yygc++; 
					yyg[yygc] = new YYARec(-10,7);yygc++; 
					yyg[yygc] = new YYARec(-9,8);yygc++; 
					yyg[yygc] = new YYARec(-8,9);yygc++; 
					yyg[yygc] = new YYARec(-7,10);yygc++; 
					yyg[yygc] = new YYARec(-6,11);yygc++; 
					yyg[yygc] = new YYARec(-5,12);yygc++; 
					yyg[yygc] = new YYARec(-4,13);yygc++; 
					yyg[yygc] = new YYARec(-3,171);yygc++; 
					yyg[yygc] = new YYARec(-53,134);yygc++; 
					yyg[yygc] = new YYARec(-26,172);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-34,116);yygc++; 
					yyg[yygc] = new YYARec(-33,117);yygc++; 
					yyg[yygc] = new YYARec(-32,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-16,174);yygc++; 
					yyg[yygc] = new YYARec(-14,121);yygc++; 
					yyg[yygc] = new YYARec(-12,122);yygc++; 
					yyg[yygc] = new YYARec(-36,175);yygc++; 
					yyg[yygc] = new YYARec(-35,176);yygc++; 
					yyg[yygc] = new YYARec(-24,177);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-34,116);yygc++; 
					yyg[yygc] = new YYARec(-33,117);yygc++; 
					yyg[yygc] = new YYARec(-32,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-16,179);yygc++; 
					yyg[yygc] = new YYARec(-14,121);yygc++; 
					yyg[yygc] = new YYARec(-12,122);yygc++; 
					yyg[yygc] = new YYARec(-57,47);yygc++; 
					yyg[yygc] = new YYARec(-54,48);yygc++; 
					yyg[yygc] = new YYARec(-53,49);yygc++; 
					yyg[yygc] = new YYARec(-31,180);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-26,53);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-22,98);yygc++; 
					yyg[yygc] = new YYARec(-21,99);yygc++; 
					yyg[yygc] = new YYARec(-20,100);yygc++; 
					yyg[yygc] = new YYARec(-18,101);yygc++; 
					yyg[yygc] = new YYARec(-17,102);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-34,116);yygc++; 
					yyg[yygc] = new YYARec(-33,117);yygc++; 
					yyg[yygc] = new YYARec(-32,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-16,181);yygc++; 
					yyg[yygc] = new YYARec(-14,121);yygc++; 
					yyg[yygc] = new YYARec(-12,122);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,186);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,169);yygc++; 
					yyg[yygc] = new YYARec(-51,187);yygc++; 
					yyg[yygc] = new YYARec(-49,191);yygc++; 
					yyg[yygc] = new YYARec(-47,194);yygc++; 
					yyg[yygc] = new YYARec(-45,199);yygc++; 
					yyg[yygc] = new YYARec(-56,207);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-44,155);yygc++; 
					yyg[yygc] = new YYARec(-43,156);yygc++; 
					yyg[yygc] = new YYARec(-42,157);yygc++; 
					yyg[yygc] = new YYARec(-41,158);yygc++; 
					yyg[yygc] = new YYARec(-40,159);yygc++; 
					yyg[yygc] = new YYARec(-39,160);yygc++; 
					yyg[yygc] = new YYARec(-38,161);yygc++; 
					yyg[yygc] = new YYARec(-37,213);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,169);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-34,116);yygc++; 
					yyg[yygc] = new YYARec(-33,117);yygc++; 
					yyg[yygc] = new YYARec(-32,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-16,214);yygc++; 
					yyg[yygc] = new YYARec(-14,121);yygc++; 
					yyg[yygc] = new YYARec(-12,122);yygc++; 
					yyg[yygc] = new YYARec(-53,134);yygc++; 
					yyg[yygc] = new YYARec(-26,219);yygc++; 
					yyg[yygc] = new YYARec(-25,220);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-34,116);yygc++; 
					yyg[yygc] = new YYARec(-33,117);yygc++; 
					yyg[yygc] = new YYARec(-32,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-16,221);yygc++; 
					yyg[yygc] = new YYARec(-15,222);yygc++; 
					yyg[yygc] = new YYARec(-14,121);yygc++; 
					yyg[yygc] = new YYARec(-12,122);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-34,116);yygc++; 
					yyg[yygc] = new YYARec(-33,117);yygc++; 
					yyg[yygc] = new YYARec(-32,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-16,221);yygc++; 
					yyg[yygc] = new YYARec(-15,223);yygc++; 
					yyg[yygc] = new YYARec(-14,121);yygc++; 
					yyg[yygc] = new YYARec(-12,122);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-34,116);yygc++; 
					yyg[yygc] = new YYARec(-33,117);yygc++; 
					yyg[yygc] = new YYARec(-32,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-16,224);yygc++; 
					yyg[yygc] = new YYARec(-14,121);yygc++; 
					yyg[yygc] = new YYARec(-12,122);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-44,155);yygc++; 
					yyg[yygc] = new YYARec(-43,156);yygc++; 
					yyg[yygc] = new YYARec(-42,157);yygc++; 
					yyg[yygc] = new YYARec(-41,158);yygc++; 
					yyg[yygc] = new YYARec(-40,159);yygc++; 
					yyg[yygc] = new YYARec(-39,160);yygc++; 
					yyg[yygc] = new YYARec(-38,161);yygc++; 
					yyg[yygc] = new YYARec(-37,225);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,169);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,226);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,169);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,152);yygc++; 
					yyg[yygc] = new YYARec(-48,227);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,169);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-46,228);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,169);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-44,229);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,169);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-44,155);yygc++; 
					yyg[yygc] = new YYARec(-43,230);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,169);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-44,155);yygc++; 
					yyg[yygc] = new YYARec(-43,156);yygc++; 
					yyg[yygc] = new YYARec(-42,231);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,169);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-44,155);yygc++; 
					yyg[yygc] = new YYARec(-43,156);yygc++; 
					yyg[yygc] = new YYARec(-42,157);yygc++; 
					yyg[yygc] = new YYARec(-41,232);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,169);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-44,155);yygc++; 
					yyg[yygc] = new YYARec(-43,156);yygc++; 
					yyg[yygc] = new YYARec(-42,157);yygc++; 
					yyg[yygc] = new YYARec(-41,158);yygc++; 
					yyg[yygc] = new YYARec(-40,233);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,169);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-44,155);yygc++; 
					yyg[yygc] = new YYARec(-43,156);yygc++; 
					yyg[yygc] = new YYARec(-42,157);yygc++; 
					yyg[yygc] = new YYARec(-41,158);yygc++; 
					yyg[yygc] = new YYARec(-40,159);yygc++; 
					yyg[yygc] = new YYARec(-39,234);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,169);yygc++; 
					yyg[yygc] = new YYARec(-55,148);yygc++; 
					yyg[yygc] = new YYARec(-54,149);yygc++; 
					yyg[yygc] = new YYARec(-53,150);yygc++; 
					yyg[yygc] = new YYARec(-52,151);yygc++; 
					yyg[yygc] = new YYARec(-50,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-44,155);yygc++; 
					yyg[yygc] = new YYARec(-43,156);yygc++; 
					yyg[yygc] = new YYARec(-42,157);yygc++; 
					yyg[yygc] = new YYARec(-41,158);yygc++; 
					yyg[yygc] = new YYARec(-40,159);yygc++; 
					yyg[yygc] = new YYARec(-39,160);yygc++; 
					yyg[yygc] = new YYARec(-38,161);yygc++; 
					yyg[yygc] = new YYARec(-37,235);yygc++; 
					yyg[yygc] = new YYARec(-30,97);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-21,169);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-34,116);yygc++; 
					yyg[yygc] = new YYARec(-33,117);yygc++; 
					yyg[yygc] = new YYARec(-32,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-16,238);yygc++; 
					yyg[yygc] = new YYARec(-14,121);yygc++; 
					yyg[yygc] = new YYARec(-12,122);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-34,116);yygc++; 
					yyg[yygc] = new YYARec(-33,117);yygc++; 
					yyg[yygc] = new YYARec(-32,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-16,239);yygc++; 
					yyg[yygc] = new YYARec(-14,121);yygc++; 
					yyg[yygc] = new YYARec(-12,122);yygc++; 
					yyg[yygc] = new YYARec(-53,134);yygc++; 
					yyg[yygc] = new YYARec(-26,240);yygc++; 
					yyg[yygc] = new YYARec(-36,175);yygc++; 
					yyg[yygc] = new YYARec(-35,242);yygc++; 
					yyg[yygc] = new YYARec(-24,177);yygc++; 
					yyg[yygc] = new YYARec(-51,187);yygc++; 
					yyg[yygc] = new YYARec(-49,191);yygc++; 
					yyg[yygc] = new YYARec(-47,194);yygc++; 
					yyg[yygc] = new YYARec(-45,199);yygc++; 
					yyg[yygc] = new YYARec(-53,134);yygc++; 
					yyg[yygc] = new YYARec(-26,250);yygc++; 
					yyg[yygc] = new YYARec(-54,1);yygc++; 
					yyg[yygc] = new YYARec(-34,116);yygc++; 
					yyg[yygc] = new YYARec(-33,117);yygc++; 
					yyg[yygc] = new YYARec(-32,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-27,52);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-16,251);yygc++; 
					yyg[yygc] = new YYARec(-14,121);yygc++; 
					yyg[yygc] = new YYARec(-12,122);yygc++; 
					yyg[yygc] = new YYARec(-53,134);yygc++; 
					yyg[yygc] = new YYARec(-26,252);yygc++; 
					yyg[yygc] = new YYARec(-53,134);yygc++; 
					yyg[yygc] = new YYARec(-26,256);yygc++; 
					yyg[yygc] = new YYARec(-53,134);yygc++; 
					yyg[yygc] = new YYARec(-26,257);yygc++; 
					yyg[yygc] = new YYARec(-53,134);yygc++; 
					yyg[yygc] = new YYARec(-26,260);yygc++; 
					yyg[yygc] = new YYARec(-53,134);yygc++; 
					yyg[yygc] = new YYARec(-26,262);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = -146;  
					yyd[2] = -31;  
					yyd[3] = 0;  
					yyd[4] = 0;  
					yyd[5] = 0;  
					yyd[6] = -10;  
					yyd[7] = -9;  
					yyd[8] = -8;  
					yyd[9] = -7;  
					yyd[10] = -6;  
					yyd[11] = -5;  
					yyd[12] = -4;  
					yyd[13] = 0;  
					yyd[14] = -1;  
					yyd[15] = 0;  
					yyd[16] = 0;  
					yyd[17] = 0;  
					yyd[18] = 0;  
					yyd[19] = 0;  
					yyd[20] = 0;  
					yyd[21] = -32;  
					yyd[22] = -33;  
					yyd[23] = -34;  
					yyd[24] = -35;  
					yyd[25] = -36;  
					yyd[26] = -37;  
					yyd[27] = 0;  
					yyd[28] = -116;  
					yyd[29] = -117;  
					yyd[30] = -118;  
					yyd[31] = -119;  
					yyd[32] = -120;  
					yyd[33] = -121;  
					yyd[34] = -122;  
					yyd[35] = -123;  
					yyd[36] = -124;  
					yyd[37] = -125;  
					yyd[38] = -126;  
					yyd[39] = -127;  
					yyd[40] = -128;  
					yyd[41] = -129;  
					yyd[42] = -145;  
					yyd[43] = 0;  
					yyd[44] = -148;  
					yyd[45] = -147;  
					yyd[46] = 0;  
					yyd[47] = -137;  
					yyd[48] = -150;  
					yyd[49] = 0;  
					yyd[50] = 0;  
					yyd[51] = -151;  
					yyd[52] = -44;  
					yyd[53] = -138;  
					yyd[54] = -43;  
					yyd[55] = -26;  
					yyd[56] = -25;  
					yyd[57] = -24;  
					yyd[58] = 0;  
					yyd[59] = -27;  
					yyd[60] = -41;  
					yyd[61] = -40;  
					yyd[62] = -42;  
					yyd[63] = -135;  
					yyd[64] = -107;  
					yyd[65] = -108;  
					yyd[66] = -106;  
					yyd[67] = -142;  
					yyd[68] = -144;  
					yyd[69] = -149;  
					yyd[70] = -152;  
					yyd[71] = -153;  
					yyd[72] = -2;  
					yyd[73] = 0;  
					yyd[74] = 0;  
					yyd[75] = 0;  
					yyd[76] = 0;  
					yyd[77] = 0;  
					yyd[78] = 0;  
					yyd[79] = 0;  
					yyd[80] = -68;  
					yyd[81] = 0;  
					yyd[82] = -141;  
					yyd[83] = -143;  
					yyd[84] = 0;  
					yyd[85] = 0;  
					yyd[86] = -23;  
					yyd[87] = 0;  
					yyd[88] = 0;  
					yyd[89] = -20;  
					yyd[90] = 0;  
					yyd[91] = -21;  
					yyd[92] = -22;  
					yyd[93] = 0;  
					yyd[94] = 0;  
					yyd[95] = 0;  
					yyd[96] = 0;  
					yyd[97] = 0;  
					yyd[98] = -62;  
					yyd[99] = -63;  
					yyd[100] = -61;  
					yyd[101] = -60;  
					yyd[102] = 0;  
					yyd[103] = 0;  
					yyd[104] = -133;  
					yyd[105] = -134;  
					yyd[106] = -11;  
					yyd[107] = 0;  
					yyd[108] = -12;  
					yyd[109] = 0;  
					yyd[110] = 0;  
					yyd[111] = -30;  
					yyd[112] = 0;  
					yyd[113] = -29;  
					yyd[114] = -45;  
					yyd[115] = 0;  
					yyd[116] = -54;  
					yyd[117] = 0;  
					yyd[118] = 0;  
					yyd[119] = 0;  
					yyd[120] = 0;  
					yyd[121] = 0;  
					yyd[122] = 0;  
					yyd[123] = 0;  
					yyd[124] = 0;  
					yyd[125] = 0;  
					yyd[126] = 0;  
					yyd[127] = 0;  
					yyd[128] = 0;  
					yyd[129] = 0;  
					yyd[130] = 0;  
					yyd[131] = -14;  
					yyd[132] = -19;  
					yyd[133] = 0;  
					yyd[134] = 0;  
					yyd[135] = 0;  
					yyd[136] = -59;  
					yyd[137] = -51;  
					yyd[138] = 0;  
					yyd[139] = 0;  
					yyd[140] = 0;  
					yyd[141] = -46;  
					yyd[142] = -52;  
					yyd[143] = 0;  
					yyd[144] = 0;  
					yyd[145] = 0;  
					yyd[146] = 0;  
					yyd[147] = 0;  
					yyd[148] = -93;  
					yyd[149] = 0;  
					yyd[150] = 0;  
					yyd[151] = -89;  
					yyd[152] = -87;  
					yyd[153] = 0;  
					yyd[154] = 0;  
					yyd[155] = 0;  
					yyd[156] = 0;  
					yyd[157] = 0;  
					yyd[158] = 0;  
					yyd[159] = 0;  
					yyd[160] = 0;  
					yyd[161] = 0;  
					yyd[162] = -109;  
					yyd[163] = 0;  
					yyd[164] = 0;  
					yyd[165] = -140;  
					yyd[166] = -139;  
					yyd[167] = 0;  
					yyd[168] = 0;  
					yyd[169] = -94;  
					yyd[170] = 0;  
					yyd[171] = 0;  
					yyd[172] = 0;  
					yyd[173] = 0;  
					yyd[174] = -50;  
					yyd[175] = 0;  
					yyd[176] = -55;  
					yyd[177] = -67;  
					yyd[178] = -66;  
					yyd[179] = -48;  
					yyd[180] = -57;  
					yyd[181] = -49;  
					yyd[182] = 0;  
					yyd[183] = 0;  
					yyd[184] = 0;  
					yyd[185] = 0;  
					yyd[186] = -90;  
					yyd[187] = 0;  
					yyd[188] = -103;  
					yyd[189] = -104;  
					yyd[190] = -105;  
					yyd[191] = 0;  
					yyd[192] = -101;  
					yyd[193] = -102;  
					yyd[194] = 0;  
					yyd[195] = -97;  
					yyd[196] = -98;  
					yyd[197] = -99;  
					yyd[198] = -100;  
					yyd[199] = 0;  
					yyd[200] = -95;  
					yyd[201] = -96;  
					yyd[202] = 0;  
					yyd[203] = 0;  
					yyd[204] = 0;  
					yyd[205] = 0;  
					yyd[206] = 0;  
					yyd[207] = 0;  
					yyd[208] = -111;  
					yyd[209] = -112;  
					yyd[210] = -113;  
					yyd[211] = -114;  
					yyd[212] = -115;  
					yyd[213] = 0;  
					yyd[214] = 0;  
					yyd[215] = 0;  
					yyd[216] = 0;  
					yyd[217] = -13;  
					yyd[218] = 0;  
					yyd[219] = 0;  
					yyd[220] = 0;  
					yyd[221] = 0;  
					yyd[222] = -15;  
					yyd[223] = -16;  
					yyd[224] = -47;  
					yyd[225] = 0;  
					yyd[226] = -88;  
					yyd[227] = 0;  
					yyd[228] = 0;  
					yyd[229] = 0;  
					yyd[230] = 0;  
					yyd[231] = 0;  
					yyd[232] = 0;  
					yyd[233] = 0;  
					yyd[234] = 0;  
					yyd[235] = -110;  
					yyd[236] = -92;  
					yyd[237] = -130;  
					yyd[238] = 0;  
					yyd[239] = 0;  
					yyd[240] = 0;  
					yyd[241] = 0;  
					yyd[242] = -65;  
					yyd[243] = 0;  
					yyd[244] = -18;  
					yyd[245] = -91;  
					yyd[246] = -131;  
					yyd[247] = -132;  
					yyd[248] = -39;  
					yyd[249] = 0;  
					yyd[250] = 0;  
					yyd[251] = 0;  
					yyd[252] = 0;  
					yyd[253] = 0;  
					yyd[254] = -17;  
					yyd[255] = 0;  
					yyd[256] = 0;  
					yyd[257] = 0;  
					yyd[258] = -28;  
					yyd[259] = 0;  
					yyd[260] = 0;  
					yyd[261] = 0;  
					yyd[262] = 0;  
					yyd[263] = -38; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 29;  
					yyal[2] = 29;  
					yyal[3] = 29;  
					yyal[4] = 31;  
					yyal[5] = 33;  
					yyal[6] = 65;  
					yyal[7] = 65;  
					yyal[8] = 65;  
					yyal[9] = 65;  
					yyal[10] = 65;  
					yyal[11] = 65;  
					yyal[12] = 65;  
					yyal[13] = 65;  
					yyal[14] = 95;  
					yyal[15] = 95;  
					yyal[16] = 96;  
					yyal[17] = 111;  
					yyal[18] = 126;  
					yyal[19] = 141;  
					yyal[20] = 156;  
					yyal[21] = 157;  
					yyal[22] = 157;  
					yyal[23] = 157;  
					yyal[24] = 157;  
					yyal[25] = 157;  
					yyal[26] = 157;  
					yyal[27] = 157;  
					yyal[28] = 159;  
					yyal[29] = 159;  
					yyal[30] = 159;  
					yyal[31] = 159;  
					yyal[32] = 159;  
					yyal[33] = 159;  
					yyal[34] = 159;  
					yyal[35] = 159;  
					yyal[36] = 159;  
					yyal[37] = 159;  
					yyal[38] = 159;  
					yyal[39] = 159;  
					yyal[40] = 159;  
					yyal[41] = 159;  
					yyal[42] = 159;  
					yyal[43] = 159;  
					yyal[44] = 161;  
					yyal[45] = 161;  
					yyal[46] = 161;  
					yyal[47] = 163;  
					yyal[48] = 163;  
					yyal[49] = 163;  
					yyal[50] = 165;  
					yyal[51] = 201;  
					yyal[52] = 201;  
					yyal[53] = 201;  
					yyal[54] = 201;  
					yyal[55] = 201;  
					yyal[56] = 201;  
					yyal[57] = 201;  
					yyal[58] = 201;  
					yyal[59] = 202;  
					yyal[60] = 202;  
					yyal[61] = 202;  
					yyal[62] = 202;  
					yyal[63] = 202;  
					yyal[64] = 202;  
					yyal[65] = 202;  
					yyal[66] = 202;  
					yyal[67] = 202;  
					yyal[68] = 202;  
					yyal[69] = 202;  
					yyal[70] = 202;  
					yyal[71] = 202;  
					yyal[72] = 202;  
					yyal[73] = 202;  
					yyal[74] = 203;  
					yyal[75] = 204;  
					yyal[76] = 206;  
					yyal[77] = 207;  
					yyal[78] = 208;  
					yyal[79] = 210;  
					yyal[80] = 211;  
					yyal[81] = 211;  
					yyal[82] = 212;  
					yyal[83] = 212;  
					yyal[84] = 212;  
					yyal[85] = 245;  
					yyal[86] = 269;  
					yyal[87] = 269;  
					yyal[88] = 298;  
					yyal[89] = 327;  
					yyal[90] = 327;  
					yyal[91] = 359;  
					yyal[92] = 359;  
					yyal[93] = 359;  
					yyal[94] = 360;  
					yyal[95] = 361;  
					yyal[96] = 367;  
					yyal[97] = 368;  
					yyal[98] = 424;  
					yyal[99] = 424;  
					yyal[100] = 424;  
					yyal[101] = 424;  
					yyal[102] = 424;  
					yyal[103] = 458;  
					yyal[104] = 490;  
					yyal[105] = 490;  
					yyal[106] = 490;  
					yyal[107] = 490;  
					yyal[108] = 492;  
					yyal[109] = 492;  
					yyal[110] = 493;  
					yyal[111] = 498;  
					yyal[112] = 498;  
					yyal[113] = 502;  
					yyal[114] = 502;  
					yyal[115] = 502;  
					yyal[116] = 535;  
					yyal[117] = 535;  
					yyal[118] = 569;  
					yyal[119] = 570;  
					yyal[120] = 575;  
					yyal[121] = 576;  
					yyal[122] = 610;  
					yyal[123] = 645;  
					yyal[124] = 660;  
					yyal[125] = 675;  
					yyal[126] = 707;  
					yyal[127] = 738;  
					yyal[128] = 739;  
					yyal[129] = 770;  
					yyal[130] = 801;  
					yyal[131] = 829;  
					yyal[132] = 829;  
					yyal[133] = 829;  
					yyal[134] = 833;  
					yyal[135] = 834;  
					yyal[136] = 835;  
					yyal[137] = 835;  
					yyal[138] = 835;  
					yyal[139] = 869;  
					yyal[140] = 872;  
					yyal[141] = 906;  
					yyal[142] = 906;  
					yyal[143] = 906;  
					yyal[144] = 938;  
					yyal[145] = 972;  
					yyal[146] = 973;  
					yyal[147] = 974;  
					yyal[148] = 975;  
					yyal[149] = 975;  
					yyal[150] = 1001;  
					yyal[151] = 1032;  
					yyal[152] = 1032;  
					yyal[153] = 1032;  
					yyal[154] = 1051;  
					yyal[155] = 1067;  
					yyal[156] = 1081;  
					yyal[157] = 1091;  
					yyal[158] = 1099;  
					yyal[159] = 1106;  
					yyal[160] = 1112;  
					yyal[161] = 1117;  
					yyal[162] = 1121;  
					yyal[163] = 1121;  
					yyal[164] = 1143;  
					yyal[165] = 1174;  
					yyal[166] = 1174;  
					yyal[167] = 1174;  
					yyal[168] = 1206;  
					yyal[169] = 1207;  
					yyal[170] = 1207;  
					yyal[171] = 1208;  
					yyal[172] = 1209;  
					yyal[173] = 1210;  
					yyal[174] = 1214;  
					yyal[175] = 1214;  
					yyal[176] = 1219;  
					yyal[177] = 1219;  
					yyal[178] = 1219;  
					yyal[179] = 1219;  
					yyal[180] = 1219;  
					yyal[181] = 1219;  
					yyal[182] = 1219;  
					yyal[183] = 1252;  
					yyal[184] = 1285;  
					yyal[185] = 1319;  
					yyal[186] = 1350;  
					yyal[187] = 1350;  
					yyal[188] = 1381;  
					yyal[189] = 1381;  
					yyal[190] = 1381;  
					yyal[191] = 1381;  
					yyal[192] = 1412;  
					yyal[193] = 1412;  
					yyal[194] = 1412;  
					yyal[195] = 1443;  
					yyal[196] = 1443;  
					yyal[197] = 1443;  
					yyal[198] = 1443;  
					yyal[199] = 1443;  
					yyal[200] = 1474;  
					yyal[201] = 1474;  
					yyal[202] = 1474;  
					yyal[203] = 1505;  
					yyal[204] = 1536;  
					yyal[205] = 1567;  
					yyal[206] = 1598;  
					yyal[207] = 1629;  
					yyal[208] = 1660;  
					yyal[209] = 1660;  
					yyal[210] = 1660;  
					yyal[211] = 1660;  
					yyal[212] = 1660;  
					yyal[213] = 1660;  
					yyal[214] = 1661;  
					yyal[215] = 1662;  
					yyal[216] = 1694;  
					yyal[217] = 1726;  
					yyal[218] = 1726;  
					yyal[219] = 1730;  
					yyal[220] = 1731;  
					yyal[221] = 1735;  
					yyal[222] = 1737;  
					yyal[223] = 1737;  
					yyal[224] = 1737;  
					yyal[225] = 1737;  
					yyal[226] = 1738;  
					yyal[227] = 1738;  
					yyal[228] = 1757;  
					yyal[229] = 1773;  
					yyal[230] = 1787;  
					yyal[231] = 1797;  
					yyal[232] = 1805;  
					yyal[233] = 1812;  
					yyal[234] = 1818;  
					yyal[235] = 1823;  
					yyal[236] = 1823;  
					yyal[237] = 1823;  
					yyal[238] = 1823;  
					yyal[239] = 1824;  
					yyal[240] = 1825;  
					yyal[241] = 1827;  
					yyal[242] = 1831;  
					yyal[243] = 1831;  
					yyal[244] = 1863;  
					yyal[245] = 1863;  
					yyal[246] = 1863;  
					yyal[247] = 1863;  
					yyal[248] = 1863;  
					yyal[249] = 1863;  
					yyal[250] = 1867;  
					yyal[251] = 1868;  
					yyal[252] = 1869;  
					yyal[253] = 1870;  
					yyal[254] = 1874;  
					yyal[255] = 1874;  
					yyal[256] = 1878;  
					yyal[257] = 1879;  
					yyal[258] = 1880;  
					yyal[259] = 1880;  
					yyal[260] = 1884;  
					yyal[261] = 1885;  
					yyal[262] = 1889;  
					yyal[263] = 1890; 

					yyah = new int[yynstates];
					yyah[0] = 28;  
					yyah[1] = 28;  
					yyah[2] = 28;  
					yyah[3] = 30;  
					yyah[4] = 32;  
					yyah[5] = 64;  
					yyah[6] = 64;  
					yyah[7] = 64;  
					yyah[8] = 64;  
					yyah[9] = 64;  
					yyah[10] = 64;  
					yyah[11] = 64;  
					yyah[12] = 64;  
					yyah[13] = 94;  
					yyah[14] = 94;  
					yyah[15] = 95;  
					yyah[16] = 110;  
					yyah[17] = 125;  
					yyah[18] = 140;  
					yyah[19] = 155;  
					yyah[20] = 156;  
					yyah[21] = 156;  
					yyah[22] = 156;  
					yyah[23] = 156;  
					yyah[24] = 156;  
					yyah[25] = 156;  
					yyah[26] = 156;  
					yyah[27] = 158;  
					yyah[28] = 158;  
					yyah[29] = 158;  
					yyah[30] = 158;  
					yyah[31] = 158;  
					yyah[32] = 158;  
					yyah[33] = 158;  
					yyah[34] = 158;  
					yyah[35] = 158;  
					yyah[36] = 158;  
					yyah[37] = 158;  
					yyah[38] = 158;  
					yyah[39] = 158;  
					yyah[40] = 158;  
					yyah[41] = 158;  
					yyah[42] = 158;  
					yyah[43] = 160;  
					yyah[44] = 160;  
					yyah[45] = 160;  
					yyah[46] = 162;  
					yyah[47] = 162;  
					yyah[48] = 162;  
					yyah[49] = 164;  
					yyah[50] = 200;  
					yyah[51] = 200;  
					yyah[52] = 200;  
					yyah[53] = 200;  
					yyah[54] = 200;  
					yyah[55] = 200;  
					yyah[56] = 200;  
					yyah[57] = 200;  
					yyah[58] = 201;  
					yyah[59] = 201;  
					yyah[60] = 201;  
					yyah[61] = 201;  
					yyah[62] = 201;  
					yyah[63] = 201;  
					yyah[64] = 201;  
					yyah[65] = 201;  
					yyah[66] = 201;  
					yyah[67] = 201;  
					yyah[68] = 201;  
					yyah[69] = 201;  
					yyah[70] = 201;  
					yyah[71] = 201;  
					yyah[72] = 201;  
					yyah[73] = 202;  
					yyah[74] = 203;  
					yyah[75] = 205;  
					yyah[76] = 206;  
					yyah[77] = 207;  
					yyah[78] = 209;  
					yyah[79] = 210;  
					yyah[80] = 210;  
					yyah[81] = 211;  
					yyah[82] = 211;  
					yyah[83] = 211;  
					yyah[84] = 244;  
					yyah[85] = 268;  
					yyah[86] = 268;  
					yyah[87] = 297;  
					yyah[88] = 326;  
					yyah[89] = 326;  
					yyah[90] = 358;  
					yyah[91] = 358;  
					yyah[92] = 358;  
					yyah[93] = 359;  
					yyah[94] = 360;  
					yyah[95] = 366;  
					yyah[96] = 367;  
					yyah[97] = 423;  
					yyah[98] = 423;  
					yyah[99] = 423;  
					yyah[100] = 423;  
					yyah[101] = 423;  
					yyah[102] = 457;  
					yyah[103] = 489;  
					yyah[104] = 489;  
					yyah[105] = 489;  
					yyah[106] = 489;  
					yyah[107] = 491;  
					yyah[108] = 491;  
					yyah[109] = 492;  
					yyah[110] = 497;  
					yyah[111] = 497;  
					yyah[112] = 501;  
					yyah[113] = 501;  
					yyah[114] = 501;  
					yyah[115] = 534;  
					yyah[116] = 534;  
					yyah[117] = 568;  
					yyah[118] = 569;  
					yyah[119] = 574;  
					yyah[120] = 575;  
					yyah[121] = 609;  
					yyah[122] = 644;  
					yyah[123] = 659;  
					yyah[124] = 674;  
					yyah[125] = 706;  
					yyah[126] = 737;  
					yyah[127] = 738;  
					yyah[128] = 769;  
					yyah[129] = 800;  
					yyah[130] = 828;  
					yyah[131] = 828;  
					yyah[132] = 828;  
					yyah[133] = 832;  
					yyah[134] = 833;  
					yyah[135] = 834;  
					yyah[136] = 834;  
					yyah[137] = 834;  
					yyah[138] = 868;  
					yyah[139] = 871;  
					yyah[140] = 905;  
					yyah[141] = 905;  
					yyah[142] = 905;  
					yyah[143] = 937;  
					yyah[144] = 971;  
					yyah[145] = 972;  
					yyah[146] = 973;  
					yyah[147] = 974;  
					yyah[148] = 974;  
					yyah[149] = 1000;  
					yyah[150] = 1031;  
					yyah[151] = 1031;  
					yyah[152] = 1031;  
					yyah[153] = 1050;  
					yyah[154] = 1066;  
					yyah[155] = 1080;  
					yyah[156] = 1090;  
					yyah[157] = 1098;  
					yyah[158] = 1105;  
					yyah[159] = 1111;  
					yyah[160] = 1116;  
					yyah[161] = 1120;  
					yyah[162] = 1120;  
					yyah[163] = 1142;  
					yyah[164] = 1173;  
					yyah[165] = 1173;  
					yyah[166] = 1173;  
					yyah[167] = 1205;  
					yyah[168] = 1206;  
					yyah[169] = 1206;  
					yyah[170] = 1207;  
					yyah[171] = 1208;  
					yyah[172] = 1209;  
					yyah[173] = 1213;  
					yyah[174] = 1213;  
					yyah[175] = 1218;  
					yyah[176] = 1218;  
					yyah[177] = 1218;  
					yyah[178] = 1218;  
					yyah[179] = 1218;  
					yyah[180] = 1218;  
					yyah[181] = 1218;  
					yyah[182] = 1251;  
					yyah[183] = 1284;  
					yyah[184] = 1318;  
					yyah[185] = 1349;  
					yyah[186] = 1349;  
					yyah[187] = 1380;  
					yyah[188] = 1380;  
					yyah[189] = 1380;  
					yyah[190] = 1380;  
					yyah[191] = 1411;  
					yyah[192] = 1411;  
					yyah[193] = 1411;  
					yyah[194] = 1442;  
					yyah[195] = 1442;  
					yyah[196] = 1442;  
					yyah[197] = 1442;  
					yyah[198] = 1442;  
					yyah[199] = 1473;  
					yyah[200] = 1473;  
					yyah[201] = 1473;  
					yyah[202] = 1504;  
					yyah[203] = 1535;  
					yyah[204] = 1566;  
					yyah[205] = 1597;  
					yyah[206] = 1628;  
					yyah[207] = 1659;  
					yyah[208] = 1659;  
					yyah[209] = 1659;  
					yyah[210] = 1659;  
					yyah[211] = 1659;  
					yyah[212] = 1659;  
					yyah[213] = 1660;  
					yyah[214] = 1661;  
					yyah[215] = 1693;  
					yyah[216] = 1725;  
					yyah[217] = 1725;  
					yyah[218] = 1729;  
					yyah[219] = 1730;  
					yyah[220] = 1734;  
					yyah[221] = 1736;  
					yyah[222] = 1736;  
					yyah[223] = 1736;  
					yyah[224] = 1736;  
					yyah[225] = 1737;  
					yyah[226] = 1737;  
					yyah[227] = 1756;  
					yyah[228] = 1772;  
					yyah[229] = 1786;  
					yyah[230] = 1796;  
					yyah[231] = 1804;  
					yyah[232] = 1811;  
					yyah[233] = 1817;  
					yyah[234] = 1822;  
					yyah[235] = 1822;  
					yyah[236] = 1822;  
					yyah[237] = 1822;  
					yyah[238] = 1823;  
					yyah[239] = 1824;  
					yyah[240] = 1826;  
					yyah[241] = 1830;  
					yyah[242] = 1830;  
					yyah[243] = 1862;  
					yyah[244] = 1862;  
					yyah[245] = 1862;  
					yyah[246] = 1862;  
					yyah[247] = 1862;  
					yyah[248] = 1862;  
					yyah[249] = 1866;  
					yyah[250] = 1867;  
					yyah[251] = 1868;  
					yyah[252] = 1869;  
					yyah[253] = 1873;  
					yyah[254] = 1873;  
					yyah[255] = 1877;  
					yyah[256] = 1878;  
					yyah[257] = 1879;  
					yyah[258] = 1879;  
					yyah[259] = 1883;  
					yyah[260] = 1884;  
					yyah[261] = 1888;  
					yyah[262] = 1889;  
					yyah[263] = 1889; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 16;  
					yygl[2] = 16;  
					yygl[3] = 16;  
					yygl[4] = 17;  
					yygl[5] = 18;  
					yygl[6] = 31;  
					yygl[7] = 31;  
					yygl[8] = 31;  
					yygl[9] = 31;  
					yygl[10] = 31;  
					yygl[11] = 31;  
					yygl[12] = 31;  
					yygl[13] = 31;  
					yygl[14] = 45;  
					yygl[15] = 45;  
					yygl[16] = 45;  
					yygl[17] = 47;  
					yygl[18] = 49;  
					yygl[19] = 51;  
					yygl[20] = 53;  
					yygl[21] = 54;  
					yygl[22] = 54;  
					yygl[23] = 54;  
					yygl[24] = 54;  
					yygl[25] = 54;  
					yygl[26] = 54;  
					yygl[27] = 54;  
					yygl[28] = 55;  
					yygl[29] = 55;  
					yygl[30] = 55;  
					yygl[31] = 55;  
					yygl[32] = 55;  
					yygl[33] = 55;  
					yygl[34] = 55;  
					yygl[35] = 55;  
					yygl[36] = 55;  
					yygl[37] = 55;  
					yygl[38] = 55;  
					yygl[39] = 55;  
					yygl[40] = 55;  
					yygl[41] = 55;  
					yygl[42] = 55;  
					yygl[43] = 55;  
					yygl[44] = 56;  
					yygl[45] = 56;  
					yygl[46] = 56;  
					yygl[47] = 57;  
					yygl[48] = 57;  
					yygl[49] = 57;  
					yygl[50] = 57;  
					yygl[51] = 58;  
					yygl[52] = 58;  
					yygl[53] = 58;  
					yygl[54] = 58;  
					yygl[55] = 58;  
					yygl[56] = 58;  
					yygl[57] = 58;  
					yygl[58] = 58;  
					yygl[59] = 58;  
					yygl[60] = 58;  
					yygl[61] = 58;  
					yygl[62] = 58;  
					yygl[63] = 58;  
					yygl[64] = 58;  
					yygl[65] = 58;  
					yygl[66] = 58;  
					yygl[67] = 58;  
					yygl[68] = 58;  
					yygl[69] = 58;  
					yygl[70] = 58;  
					yygl[71] = 58;  
					yygl[72] = 58;  
					yygl[73] = 58;  
					yygl[74] = 58;  
					yygl[75] = 58;  
					yygl[76] = 58;  
					yygl[77] = 58;  
					yygl[78] = 58;  
					yygl[79] = 59;  
					yygl[80] = 60;  
					yygl[81] = 60;  
					yygl[82] = 61;  
					yygl[83] = 61;  
					yygl[84] = 61;  
					yygl[85] = 75;  
					yygl[86] = 80;  
					yygl[87] = 80;  
					yygl[88] = 95;  
					yygl[89] = 110;  
					yygl[90] = 110;  
					yygl[91] = 123;  
					yygl[92] = 123;  
					yygl[93] = 123;  
					yygl[94] = 124;  
					yygl[95] = 124;  
					yygl[96] = 125;  
					yygl[97] = 125;  
					yygl[98] = 125;  
					yygl[99] = 125;  
					yygl[100] = 125;  
					yygl[101] = 125;  
					yygl[102] = 125;  
					yygl[103] = 126;  
					yygl[104] = 136;  
					yygl[105] = 136;  
					yygl[106] = 136;  
					yygl[107] = 136;  
					yygl[108] = 136;  
					yygl[109] = 136;  
					yygl[110] = 136;  
					yygl[111] = 137;  
					yygl[112] = 137;  
					yygl[113] = 139;  
					yygl[114] = 139;  
					yygl[115] = 139;  
					yygl[116] = 153;  
					yygl[117] = 153;  
					yygl[118] = 163;  
					yygl[119] = 163;  
					yygl[120] = 164;  
					yygl[121] = 164;  
					yygl[122] = 174;  
					yygl[123] = 175;  
					yygl[124] = 177;  
					yygl[125] = 179;  
					yygl[126] = 189;  
					yygl[127] = 209;  
					yygl[128] = 209;  
					yygl[129] = 229;  
					yygl[130] = 249;  
					yygl[131] = 263;  
					yygl[132] = 263;  
					yygl[133] = 263;  
					yygl[134] = 265;  
					yygl[135] = 265;  
					yygl[136] = 265;  
					yygl[137] = 265;  
					yygl[138] = 265;  
					yygl[139] = 275;  
					yygl[140] = 278;  
					yygl[141] = 288;  
					yygl[142] = 288;  
					yygl[143] = 288;  
					yygl[144] = 302;  
					yygl[145] = 312;  
					yygl[146] = 312;  
					yygl[147] = 312;  
					yygl[148] = 312;  
					yygl[149] = 312;  
					yygl[150] = 312;  
					yygl[151] = 322;  
					yygl[152] = 322;  
					yygl[153] = 322;  
					yygl[154] = 323;  
					yygl[155] = 324;  
					yygl[156] = 325;  
					yygl[157] = 326;  
					yygl[158] = 326;  
					yygl[159] = 326;  
					yygl[160] = 326;  
					yygl[161] = 326;  
					yygl[162] = 326;  
					yygl[163] = 326;  
					yygl[164] = 327;  
					yygl[165] = 347;  
					yygl[166] = 347;  
					yygl[167] = 347;  
					yygl[168] = 357;  
					yygl[169] = 357;  
					yygl[170] = 357;  
					yygl[171] = 357;  
					yygl[172] = 357;  
					yygl[173] = 357;  
					yygl[174] = 359;  
					yygl[175] = 359;  
					yygl[176] = 360;  
					yygl[177] = 360;  
					yygl[178] = 360;  
					yygl[179] = 360;  
					yygl[180] = 360;  
					yygl[181] = 360;  
					yygl[182] = 360;  
					yygl[183] = 371;  
					yygl[184] = 382;  
					yygl[185] = 392;  
					yygl[186] = 412;  
					yygl[187] = 412;  
					yygl[188] = 422;  
					yygl[189] = 422;  
					yygl[190] = 422;  
					yygl[191] = 422;  
					yygl[192] = 433;  
					yygl[193] = 433;  
					yygl[194] = 433;  
					yygl[195] = 445;  
					yygl[196] = 445;  
					yygl[197] = 445;  
					yygl[198] = 445;  
					yygl[199] = 445;  
					yygl[200] = 458;  
					yygl[201] = 458;  
					yygl[202] = 458;  
					yygl[203] = 472;  
					yygl[204] = 487;  
					yygl[205] = 503;  
					yygl[206] = 520;  
					yygl[207] = 538;  
					yygl[208] = 558;  
					yygl[209] = 558;  
					yygl[210] = 558;  
					yygl[211] = 558;  
					yygl[212] = 558;  
					yygl[213] = 558;  
					yygl[214] = 558;  
					yygl[215] = 558;  
					yygl[216] = 568;  
					yygl[217] = 578;  
					yygl[218] = 578;  
					yygl[219] = 580;  
					yygl[220] = 580;  
					yygl[221] = 583;  
					yygl[222] = 583;  
					yygl[223] = 583;  
					yygl[224] = 583;  
					yygl[225] = 583;  
					yygl[226] = 583;  
					yygl[227] = 583;  
					yygl[228] = 584;  
					yygl[229] = 585;  
					yygl[230] = 586;  
					yygl[231] = 587;  
					yygl[232] = 587;  
					yygl[233] = 587;  
					yygl[234] = 587;  
					yygl[235] = 587;  
					yygl[236] = 587;  
					yygl[237] = 587;  
					yygl[238] = 587;  
					yygl[239] = 587;  
					yygl[240] = 587;  
					yygl[241] = 587;  
					yygl[242] = 589;  
					yygl[243] = 589;  
					yygl[244] = 599;  
					yygl[245] = 599;  
					yygl[246] = 599;  
					yygl[247] = 599;  
					yygl[248] = 599;  
					yygl[249] = 599;  
					yygl[250] = 601;  
					yygl[251] = 601;  
					yygl[252] = 601;  
					yygl[253] = 601;  
					yygl[254] = 603;  
					yygl[255] = 603;  
					yygl[256] = 605;  
					yygl[257] = 605;  
					yygl[258] = 605;  
					yygl[259] = 605;  
					yygl[260] = 607;  
					yygl[261] = 607;  
					yygl[262] = 609;  
					yygl[263] = 609; 

					yygh = new int[yynstates];
					yygh[0] = 15;  
					yygh[1] = 15;  
					yygh[2] = 15;  
					yygh[3] = 16;  
					yygh[4] = 17;  
					yygh[5] = 30;  
					yygh[6] = 30;  
					yygh[7] = 30;  
					yygh[8] = 30;  
					yygh[9] = 30;  
					yygh[10] = 30;  
					yygh[11] = 30;  
					yygh[12] = 30;  
					yygh[13] = 44;  
					yygh[14] = 44;  
					yygh[15] = 44;  
					yygh[16] = 46;  
					yygh[17] = 48;  
					yygh[18] = 50;  
					yygh[19] = 52;  
					yygh[20] = 53;  
					yygh[21] = 53;  
					yygh[22] = 53;  
					yygh[23] = 53;  
					yygh[24] = 53;  
					yygh[25] = 53;  
					yygh[26] = 53;  
					yygh[27] = 54;  
					yygh[28] = 54;  
					yygh[29] = 54;  
					yygh[30] = 54;  
					yygh[31] = 54;  
					yygh[32] = 54;  
					yygh[33] = 54;  
					yygh[34] = 54;  
					yygh[35] = 54;  
					yygh[36] = 54;  
					yygh[37] = 54;  
					yygh[38] = 54;  
					yygh[39] = 54;  
					yygh[40] = 54;  
					yygh[41] = 54;  
					yygh[42] = 54;  
					yygh[43] = 55;  
					yygh[44] = 55;  
					yygh[45] = 55;  
					yygh[46] = 56;  
					yygh[47] = 56;  
					yygh[48] = 56;  
					yygh[49] = 56;  
					yygh[50] = 57;  
					yygh[51] = 57;  
					yygh[52] = 57;  
					yygh[53] = 57;  
					yygh[54] = 57;  
					yygh[55] = 57;  
					yygh[56] = 57;  
					yygh[57] = 57;  
					yygh[58] = 57;  
					yygh[59] = 57;  
					yygh[60] = 57;  
					yygh[61] = 57;  
					yygh[62] = 57;  
					yygh[63] = 57;  
					yygh[64] = 57;  
					yygh[65] = 57;  
					yygh[66] = 57;  
					yygh[67] = 57;  
					yygh[68] = 57;  
					yygh[69] = 57;  
					yygh[70] = 57;  
					yygh[71] = 57;  
					yygh[72] = 57;  
					yygh[73] = 57;  
					yygh[74] = 57;  
					yygh[75] = 57;  
					yygh[76] = 57;  
					yygh[77] = 57;  
					yygh[78] = 58;  
					yygh[79] = 59;  
					yygh[80] = 59;  
					yygh[81] = 60;  
					yygh[82] = 60;  
					yygh[83] = 60;  
					yygh[84] = 74;  
					yygh[85] = 79;  
					yygh[86] = 79;  
					yygh[87] = 94;  
					yygh[88] = 109;  
					yygh[89] = 109;  
					yygh[90] = 122;  
					yygh[91] = 122;  
					yygh[92] = 122;  
					yygh[93] = 123;  
					yygh[94] = 123;  
					yygh[95] = 124;  
					yygh[96] = 124;  
					yygh[97] = 124;  
					yygh[98] = 124;  
					yygh[99] = 124;  
					yygh[100] = 124;  
					yygh[101] = 124;  
					yygh[102] = 125;  
					yygh[103] = 135;  
					yygh[104] = 135;  
					yygh[105] = 135;  
					yygh[106] = 135;  
					yygh[107] = 135;  
					yygh[108] = 135;  
					yygh[109] = 135;  
					yygh[110] = 136;  
					yygh[111] = 136;  
					yygh[112] = 138;  
					yygh[113] = 138;  
					yygh[114] = 138;  
					yygh[115] = 152;  
					yygh[116] = 152;  
					yygh[117] = 162;  
					yygh[118] = 162;  
					yygh[119] = 163;  
					yygh[120] = 163;  
					yygh[121] = 173;  
					yygh[122] = 174;  
					yygh[123] = 176;  
					yygh[124] = 178;  
					yygh[125] = 188;  
					yygh[126] = 208;  
					yygh[127] = 208;  
					yygh[128] = 228;  
					yygh[129] = 248;  
					yygh[130] = 262;  
					yygh[131] = 262;  
					yygh[132] = 262;  
					yygh[133] = 264;  
					yygh[134] = 264;  
					yygh[135] = 264;  
					yygh[136] = 264;  
					yygh[137] = 264;  
					yygh[138] = 274;  
					yygh[139] = 277;  
					yygh[140] = 287;  
					yygh[141] = 287;  
					yygh[142] = 287;  
					yygh[143] = 301;  
					yygh[144] = 311;  
					yygh[145] = 311;  
					yygh[146] = 311;  
					yygh[147] = 311;  
					yygh[148] = 311;  
					yygh[149] = 311;  
					yygh[150] = 321;  
					yygh[151] = 321;  
					yygh[152] = 321;  
					yygh[153] = 322;  
					yygh[154] = 323;  
					yygh[155] = 324;  
					yygh[156] = 325;  
					yygh[157] = 325;  
					yygh[158] = 325;  
					yygh[159] = 325;  
					yygh[160] = 325;  
					yygh[161] = 325;  
					yygh[162] = 325;  
					yygh[163] = 326;  
					yygh[164] = 346;  
					yygh[165] = 346;  
					yygh[166] = 346;  
					yygh[167] = 356;  
					yygh[168] = 356;  
					yygh[169] = 356;  
					yygh[170] = 356;  
					yygh[171] = 356;  
					yygh[172] = 356;  
					yygh[173] = 358;  
					yygh[174] = 358;  
					yygh[175] = 359;  
					yygh[176] = 359;  
					yygh[177] = 359;  
					yygh[178] = 359;  
					yygh[179] = 359;  
					yygh[180] = 359;  
					yygh[181] = 359;  
					yygh[182] = 370;  
					yygh[183] = 381;  
					yygh[184] = 391;  
					yygh[185] = 411;  
					yygh[186] = 411;  
					yygh[187] = 421;  
					yygh[188] = 421;  
					yygh[189] = 421;  
					yygh[190] = 421;  
					yygh[191] = 432;  
					yygh[192] = 432;  
					yygh[193] = 432;  
					yygh[194] = 444;  
					yygh[195] = 444;  
					yygh[196] = 444;  
					yygh[197] = 444;  
					yygh[198] = 444;  
					yygh[199] = 457;  
					yygh[200] = 457;  
					yygh[201] = 457;  
					yygh[202] = 471;  
					yygh[203] = 486;  
					yygh[204] = 502;  
					yygh[205] = 519;  
					yygh[206] = 537;  
					yygh[207] = 557;  
					yygh[208] = 557;  
					yygh[209] = 557;  
					yygh[210] = 557;  
					yygh[211] = 557;  
					yygh[212] = 557;  
					yygh[213] = 557;  
					yygh[214] = 557;  
					yygh[215] = 567;  
					yygh[216] = 577;  
					yygh[217] = 577;  
					yygh[218] = 579;  
					yygh[219] = 579;  
					yygh[220] = 582;  
					yygh[221] = 582;  
					yygh[222] = 582;  
					yygh[223] = 582;  
					yygh[224] = 582;  
					yygh[225] = 582;  
					yygh[226] = 582;  
					yygh[227] = 583;  
					yygh[228] = 584;  
					yygh[229] = 585;  
					yygh[230] = 586;  
					yygh[231] = 586;  
					yygh[232] = 586;  
					yygh[233] = 586;  
					yygh[234] = 586;  
					yygh[235] = 586;  
					yygh[236] = 586;  
					yygh[237] = 586;  
					yygh[238] = 586;  
					yygh[239] = 586;  
					yygh[240] = 586;  
					yygh[241] = 588;  
					yygh[242] = 588;  
					yygh[243] = 598;  
					yygh[244] = 598;  
					yygh[245] = 598;  
					yygh[246] = 598;  
					yygh[247] = 598;  
					yygh[248] = 598;  
					yygh[249] = 600;  
					yygh[250] = 600;  
					yygh[251] = 600;  
					yygh[252] = 600;  
					yygh[253] = 602;  
					yygh[254] = 602;  
					yygh[255] = 604;  
					yygh[256] = 604;  
					yygh[257] = 604;  
					yygh[258] = 604;  
					yygh[259] = 606;  
					yygh[260] = 606;  
					yygh[261] = 608;  
					yygh[262] = 608;  
					yygh[263] = 608; 

					yyr[yyrc] = new YYRRec(1,-2);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(13,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(17,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(9,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++;
                }

                public bool yyact(int state, int sym , ref int act)
                {
                        int k = yyal[state];
                        while ( k <= yyah[state] && yya[k].sym != sym) k++;
                        if (k > yyah[state]) return false;
                        act = yya[k].act;
                        return true;
                }
                public bool yygoto(int state, int sym , ref int nstate)
                {
                        int k = yygl[state];
                        while ( k <= yygh[state] && yyg[k].sym != sym) k++;
                        if (k > yygh[state]) return false;
                        nstate = yyg[k].act;
                        return true;
                }

                public void yyerror (string s)
                {
                        System.Console.Write(s);
                }

                int yylexpos = -1;
                string yylval = "";

                public int yylex ()
                {
                        yylexpos++;
                        if(yylexpos >= TokenList.Count)
                        {
                                yylval = "";
                                return 0;
                        }
                        else
                        {
                                yylval = ((AToken)TokenList[yylexpos]).val;
                                return ((AToken)TokenList[yylexpos]).token;
                        }
                }

                public bool yyparse ()
                {

                        parse:

                                yysp++;
                        if (yysp>=yymaxdepth)
                        {
                                yyerror("yyparse stack overflow");
                                goto abort;
                        }

                        yys[yysp] = yystate;
                        yyv[yysp] = yyval;

                        next:

                                if (yyd[yystate]==0 && yychar==-1)
                                {
                                        yychar = yylex();
                                        if (yychar<0) yychar = 0;
                                }

                        yyn = yyd[yystate];
                        if (yyn != 0) goto reduce;


                        if (! yyact(yystate, yychar, ref yyn)) goto error;
                        else if (yyn>0) goto shift;
                        else if (yyn<0) goto reduce;
                        else            goto accept;

                        error:

                                if (yyerrflag==0) yyerror("syntax error");

                        errlab:

                                if (yyerrflag==0) yynerrs++;

                        if (yyerrflag<=2)
                        {
                                yyerrflag = 3;
                                while (yysp>0 && !(yyact(yys[yysp], 255, ref yyn) && yyn > 0)) yysp--;

                                if (yysp==0) goto abort;
                                yystate = yyn;
                                goto parse;
                        }
                        else
                        {
                                if (yychar==0) goto abort;
                                yychar = -1; goto next;
                        }

                        shift:

                        yystate = yyn;
                        yychar = -1;
                        yyval = yylval;
                        if (yyerrflag>0) yyerrflag--;
                        goto parse;

                        reduce:

                        yyflag = yyfnone;
                        yyaction(-yyn);
                        yysp -= yyr[-yyn].len;

                        if (yygoto(yys[yysp], yyr[-yyn].sym, ref yyn)) yystate = yyn;

                        switch (yyflag)
                        {
                                case 1 : goto accept;
                                case 2 : goto abort;
                                case 3 : goto errlab;
                        }

                        goto parse;

                        accept:

                                return true;

                        abort:

                                return false;
                }
		////////////////////////////////////////////////////////////////
		/// Scanner
		////////////////////////////////////////////////////////////////

		public class AToken
		{
			public int token;
			public string val;
		}

		ArrayList TokenList = new ArrayList();

		public bool Scanner (string Input)
		{
		        if (Input.Length == 0) return true;
			TokenList = new ArrayList();
			while (1==1)
			{
				AToken lasttoken = FindToken(Input);
				if (lasttoken.token == 0) break;
				if (lasttoken.token != t_ignore) TokenList.Add(lasttoken);
				if (Input.Length > lasttoken.val.Length)
				Input = Input.Substring(lasttoken.val.Length); else return true;
			}
                      System.Console.WriteLine(Input);
			System.Console.WriteLine();
			System.Console.WriteLine("No matching token found!");
			return false;
		}
		public AToken FindToken (string Rest)
		{
			ArrayList Results  = new ArrayList();
			ArrayList ResultsV = new ArrayList();
                      try{

			if (Regex.IsMatch(Rest,"^((?i)IFDEF)")){
				Results.Add (t_IFDEF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)IFDEF)").Value);}

			if (Regex.IsMatch(Rest,"^((,[\\s\\t]*)?;+)")){
				Results.Add (t_Char59);
				ResultsV.Add(Regex.Match(Rest,"^((,[\\s\\t]*)?;+)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)IFNDEF)")){
				Results.Add (t_IFNDEF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)IFNDEF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)IFELSE;)")){
				Results.Add (t_IFELSEChar59);
				ResultsV.Add(Regex.Match(Rest,"^((?i)IFELSE;)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)ENDIF;)")){
				Results.Add (t_ENDIFChar59);
				ResultsV.Add(Regex.Match(Rest,"^((?i)ENDIF;)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)DEFINE)")){
				Results.Add (t_DEFINE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)DEFINE)").Value);}

			if (Regex.IsMatch(Rest,"^(,)")){
				Results.Add (t_Char44);
				ResultsV.Add(Regex.Match(Rest,"^(,)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)UNDEF)")){
				Results.Add (t_UNDEF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)UNDEF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)INCLUDE)")){
				Results.Add (t_INCLUDE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)INCLUDE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)FLIC)")){
				Results.Add (t_FLIC);
				ResultsV.Add(Regex.Match(Rest,"^((?i)FLIC)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)MODEL)")){
				Results.Add (t_MODEL);
				ResultsV.Add(Regex.Match(Rest,"^((?i)MODEL)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)MUSIC)")){
				Results.Add (t_MUSIC);
				ResultsV.Add(Regex.Match(Rest,"^((?i)MUSIC)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)SOUND)")){
				Results.Add (t_SOUND);
				ResultsV.Add(Regex.Match(Rest,"^((?i)SOUND)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)BMAP)")){
				Results.Add (t_BMAP);
				ResultsV.Add(Regex.Match(Rest,"^((?i)BMAP)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)OVLY)")){
				Results.Add (t_OVLY);
				ResultsV.Add(Regex.Match(Rest,"^((?i)OVLY)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)FONT)")){
				Results.Add (t_FONT);
				ResultsV.Add(Regex.Match(Rest,"^((?i)FONT)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)BMAPS)")){
				Results.Add (t_BMAPS);
				ResultsV.Add(Regex.Match(Rest,"^((?i)BMAPS)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)OVLYS)")){
				Results.Add (t_OVLYS);
				ResultsV.Add(Regex.Match(Rest,"^((?i)OVLYS)").Value);}

			if (Regex.IsMatch(Rest,"^(\\{)")){
				Results.Add (t_Char123);
				ResultsV.Add(Regex.Match(Rest,"^(\\{)").Value);}

			if (Regex.IsMatch(Rest,"^(\\}([\\t\\s]*;+)?)")){
				Results.Add (t_Char125);
				ResultsV.Add(Regex.Match(Rest,"^(\\}([\\t\\s]*;+)?)").Value);}

			if (Regex.IsMatch(Rest,"^(:)")){
				Results.Add (t_Char58);
				ResultsV.Add(Regex.Match(Rest,"^(:)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)NULL)")){
				Results.Add (t_NULL);
				ResultsV.Add(Regex.Match(Rest,"^((?i)NULL)").Value);}

			if (Regex.IsMatch(Rest,"^(\\|\\|)")){
				Results.Add (t_Char124Char124);
				ResultsV.Add(Regex.Match(Rest,"^(\\|\\|)").Value);}

			if (Regex.IsMatch(Rest,"^(&&)")){
				Results.Add (t_Char38Char38);
				ResultsV.Add(Regex.Match(Rest,"^(&&)").Value);}

			if (Regex.IsMatch(Rest,"^(\\|)")){
				Results.Add (t_Char124);
				ResultsV.Add(Regex.Match(Rest,"^(\\|)").Value);}

			if (Regex.IsMatch(Rest,"^(\\^)")){
				Results.Add (t_Char94);
				ResultsV.Add(Regex.Match(Rest,"^(\\^)").Value);}

			if (Regex.IsMatch(Rest,"^(&)")){
				Results.Add (t_Char38);
				ResultsV.Add(Regex.Match(Rest,"^(&)").Value);}

			if (Regex.IsMatch(Rest,"^(\\()")){
				Results.Add (t_Char40);
				ResultsV.Add(Regex.Match(Rest,"^(\\()").Value);}

			if (Regex.IsMatch(Rest,"^(\\))")){
				Results.Add (t_Char41);
				ResultsV.Add(Regex.Match(Rest,"^(\\))").Value);}

			if (Regex.IsMatch(Rest,"^(!=)")){
				Results.Add (t_Char33Char61);
				ResultsV.Add(Regex.Match(Rest,"^(!=)").Value);}

			if (Regex.IsMatch(Rest,"^(==)")){
				Results.Add (t_Char61Char61);
				ResultsV.Add(Regex.Match(Rest,"^(==)").Value);}

			if (Regex.IsMatch(Rest,"^(<)")){
				Results.Add (t_Char60);
				ResultsV.Add(Regex.Match(Rest,"^(<)").Value);}

			if (Regex.IsMatch(Rest,"^(<=)")){
				Results.Add (t_Char60Char61);
				ResultsV.Add(Regex.Match(Rest,"^(<=)").Value);}

			if (Regex.IsMatch(Rest,"^(>)")){
				Results.Add (t_Char62);
				ResultsV.Add(Regex.Match(Rest,"^(>)").Value);}

			if (Regex.IsMatch(Rest,"^(>=)")){
				Results.Add (t_Char62Char61);
				ResultsV.Add(Regex.Match(Rest,"^(>=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\+)")){
				Results.Add (t_Char43);
				ResultsV.Add(Regex.Match(Rest,"^(\\+)").Value);}

			if (Regex.IsMatch(Rest,"^(\\-)")){
				Results.Add (t_Char45);
				ResultsV.Add(Regex.Match(Rest,"^(\\-)").Value);}

			if (Regex.IsMatch(Rest,"^(%)")){
				Results.Add (t_Char37);
				ResultsV.Add(Regex.Match(Rest,"^(%)").Value);}

			if (Regex.IsMatch(Rest,"^(\\*)")){
				Results.Add (t_Char42);
				ResultsV.Add(Regex.Match(Rest,"^(\\*)").Value);}

			if (Regex.IsMatch(Rest,"^(/)")){
				Results.Add (t_Char47);
				ResultsV.Add(Regex.Match(Rest,"^(/)").Value);}

			if (Regex.IsMatch(Rest,"^(!)")){
				Results.Add (t_Char33);
				ResultsV.Add(Regex.Match(Rest,"^(!)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)RULE)")){
				Results.Add (t_RULE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)RULE)").Value);}

			if (Regex.IsMatch(Rest,"^(\\*[\\s\\t]*=)")){
				Results.Add (t_Char42Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\*[\\s\\t]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\+[\\s\\t]*=)")){
				Results.Add (t_Char43Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\+[\\s\\t]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\-[\\s\\t]*=)")){
				Results.Add (t_Char45Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\-[\\s\\t]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(/[\\s\\t]*=)")){
				Results.Add (t_Char47Char61);
				ResultsV.Add(Regex.Match(Rest,"^(/[\\s\\t]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(=)")){
				Results.Add (t_Char61);
				ResultsV.Add(Regex.Match(Rest,"^(=)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)ABS)")){
				Results.Add (t_ABS);
				ResultsV.Add(Regex.Match(Rest,"^((?i)ABS)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)ACOS)")){
				Results.Add (t_ACOS);
				ResultsV.Add(Regex.Match(Rest,"^((?i)ACOS)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)ASIN)")){
				Results.Add (t_ASIN);
				ResultsV.Add(Regex.Match(Rest,"^((?i)ASIN)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)COS)")){
				Results.Add (t_COS);
				ResultsV.Add(Regex.Match(Rest,"^((?i)COS)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)EXP)")){
				Results.Add (t_EXP);
				ResultsV.Add(Regex.Match(Rest,"^((?i)EXP)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)INT)")){
				Results.Add (t_INT);
				ResultsV.Add(Regex.Match(Rest,"^((?i)INT)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)LOG)")){
				Results.Add (t_LOG);
				ResultsV.Add(Regex.Match(Rest,"^((?i)LOG)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)LOG10)")){
				Results.Add (t_LOG10);
				ResultsV.Add(Regex.Match(Rest,"^((?i)LOG10)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)LOG2)")){
				Results.Add (t_LOG2);
				ResultsV.Add(Regex.Match(Rest,"^((?i)LOG2)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)RANDOM)")){
				Results.Add (t_RANDOM);
				ResultsV.Add(Regex.Match(Rest,"^((?i)RANDOM)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)SIGN)")){
				Results.Add (t_SIGN);
				ResultsV.Add(Regex.Match(Rest,"^((?i)SIGN)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)SIN)")){
				Results.Add (t_SIN);
				ResultsV.Add(Regex.Match(Rest,"^((?i)SIN)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)SQRT)")){
				Results.Add (t_SQRT);
				ResultsV.Add(Regex.Match(Rest,"^((?i)SQRT)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)TAN)")){
				Results.Add (t_TAN);
				ResultsV.Add(Regex.Match(Rest,"^((?i)TAN)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)ELSE)")){
				Results.Add (t_ELSE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)ELSE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)IF)")){
				Results.Add (t_IF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)IF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)WHILE)")){
				Results.Add (t_WHILE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)WHILE)").Value);}

			if (Regex.IsMatch(Rest,"^(\\.)")){
				Results.Add (t_Char46);
				ResultsV.Add(Regex.Match(Rest,"^(\\.)").Value);}

			if (Regex.IsMatch(Rest,"^([0-9]+)")){
				Results.Add (t_integer);
				ResultsV.Add(Regex.Match(Rest,"^([0-9]+)").Value);}

			if (Regex.IsMatch(Rest,"^(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))")){
				Results.Add (t_fixed);
				ResultsV.Add(Regex.Match(Rest,"^(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))").Value);}

			if (Regex.IsMatch(Rest,"^([A-Za-z0-9][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)")){
				Results.Add (t_identifier);
				ResultsV.Add(Regex.Match(Rest,"^([A-Za-z0-9][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)").Value);}

			if (Regex.IsMatch(Rest,"^(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)")){
				Results.Add (t_file);
				ResultsV.Add(Regex.Match(Rest,"^(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)").Value);}

			if (Regex.IsMatch(Rest,"^(\"(.|[\\r\\n])*?\")")){
				Results.Add (t_string);
				ResultsV.Add(Regex.Match(Rest,"^(\"(.|[\\r\\n])*?\")").Value);}

			if (Regex.IsMatch(Rest,"^([\\r\\n\\t\\s]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))")){
				Results.Add (t_ignore);
				ResultsV.Add(Regex.Match(Rest,"^([\\r\\n\\t\\s]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))").Value);}

			}catch{}
			int maxlength = 0;
			int besttoken = 0;
			AToken ret = new AToken();
		        ret.token = besttoken;
			for (int i = 0; i < Results.Count; i++){
				if (ResultsV[i].ToString().Length > maxlength)
				{
					maxlength = ResultsV[i].ToString().Length;
					besttoken = (int)Results[i];
		         	        ret.token = besttoken;
		                  	if (besttoken != 0)
			                ret.val   = ResultsV[i].ToString();
				}
			}
			return ret;
		}


	}
}
