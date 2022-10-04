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
                int yymaxdepth = 1024;
                int yyflag = 0;
                int yyfnone   = 0;
                int[] yys = new int[1024];
                string[] yyv = new string[1024];

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
                int t_Char123 = 264;
                int t_Char125 = 265;
                int t_Char58 = 266;
                int t_Char40 = 267;
                int t_Char41 = 268;
                int t_Char37 = 269;
                int t_Char38 = 270;
                int t_Char42 = 271;
                int t_Char47 = 272;
                int t_Char94 = 273;
                int t_Char124 = 274;
                int t_Char43 = 275;
                int t_Char45 = 276;
                int t_Char42Char61 = 277;
                int t_Char43Char61 = 278;
                int t_Char45Char61 = 279;
                int t_Char47Char61 = 280;
                int t_Char61 = 281;
                int t_ABS = 282;
                int t_ACOS = 283;
                int t_ASIN = 284;
                int t_COS = 285;
                int t_EXP = 286;
                int t_INT = 287;
                int t_LOG = 288;
                int t_LOG10 = 289;
                int t_LOG2 = 290;
                int t_RANDOM = 291;
                int t_SIGN = 292;
                int t_SIN = 293;
                int t_SQRT = 294;
                int t_TAN = 295;
                int t_IF = 296;
                int t_ELSE = 297;
                int t_WHILE = 298;
                int t_Char33Char61 = 299;
                int t_Char38Char38 = 300;
                int t_Char60 = 301;
                int t_Char60Char61 = 302;
                int t_Char61Char61 = 303;
                int t_Char62 = 304;
                int t_Char62Char61 = 305;
                int t_Char124Char124 = 306;
                int t_Char46 = 307;
                int t_identifier = 308;
                int t_number = 309;
                int t_file = 310;
                int t_string = 311;
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
         yyval = yyv[yysp-0];
         
       break;
							case    4 : 
         yyval = yyv[yysp-0];
         
       break;
							case    5 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case    6 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case    7 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case    8 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case    9 : 
         yyval = yyv[yysp-0];
         
       break;
							case   10 : 
         yyval = yyv[yysp-0];
         
       break;
							case   11 : 
         yyval = yyv[yysp-0];
         
       break;
							case   12 : 
         yyval = yyv[yysp-0];
         
       break;
							case   13 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   14 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   15 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   16 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   17 : 
         yyval = yyv[yysp-0];
         
       break;
							case   18 : 
         yyval = yyv[yysp-0];
         
       break;
							case   19 : 
         yyval = yyv[yysp-0];
         
       break;
							case   20 : 
         yyval = yyv[yysp-0];
         
       break;
							case   21 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   22 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   23 : 
         yyval = yyv[yysp-8] + yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   24 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   25 : 
         yyval = yyv[yysp-0];
         
       break;
							case   26 : 
         yyval = yyv[yysp-0];
         
       break;
							case   27 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   28 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   29 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   30 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   31 : 
         yyval = "";
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   39 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   44 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   45 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   46 : 
         yyval = yyv[yysp-0];
         
       break;
							case   47 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   48 : 
         yyval = yyv[yysp-0];
         
       break;
							case   49 : 
         yyval = yyv[yysp-0];
         
       break;
							case   50 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   51 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   52 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   53 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   54 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   55 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   56 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   57 : 
         yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   58 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   59 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   60 : 
         yyval = yyv[yysp-0];
         
       break;
							case   61 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   62 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   63 : 
         yyval = yyv[yysp-0];
         
       break;
							case   64 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   65 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   70 : 
         yyval = yyv[yysp-0];
         
       break;
							case   71 : 
         yyval = yyv[yysp-0];
         
       break;
							case   72 : 
         yyval = yyv[yysp-0];
         
       break;
							case   73 : 
         yyval = yyv[yysp-0];
         
       break;
							case   74 : 
         yyval = yyv[yysp-0];
         
       break;
							case   75 : 
         yyval = yyv[yysp-0];
         
       break;
							case   76 : 
         yyval = yyv[yysp-0];
         
       break;
							case   77 : 
         yyval = yyv[yysp-0];
         
       break;
							case   78 : 
         yyval = yyv[yysp-0];
         
       break;
							case   79 : 
         yyval = yyv[yysp-0];
         
       break;
							case   80 : 
         yyval = yyv[yysp-0];
         
       break;
							case   81 : 
         yyval = yyv[yysp-0];
         
       break;
							case   82 : 
         yyval = yyv[yysp-0];
         
       break;
							case   83 : 
         yyval = yyv[yysp-0];
         
       break;
							case   84 : 
         yyval = yyv[yysp-0];
         
       break;
							case   85 : 
         yyval = yyv[yysp-0];
         
       break;
							case   86 : 
         yyval = yyv[yysp-0];
         
       break;
							case   87 : 
         yyval = yyv[yysp-0];
         
       break;
							case   88 : 
         yyval = yyv[yysp-0];
         
       break;
							case   89 : 
         yyval = yyv[yysp-0];
         
       break;
							case   90 : 
         yyval = yyv[yysp-0];
         
       break;
							case   91 : 
         yyval = yyv[yysp-0];
         
       break;
							case   92 : 
         yyval = yyv[yysp-0];
         
       break;
							case   93 : 
         yyval = yyv[yysp-0];
         
       break;
							case   94 : 
         yyval = yyv[yysp-0];
         
       break;
							case   95 : 
         yyval = yyv[yysp-10] + yyv[yysp-9] + yyv[yysp-8] + yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   96 : 
         yyval = yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   97 : 
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   98 : 
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   99 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  100 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  101 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  102 : 
         yyval = yyv[yysp-0];
         
       break;
							case  103 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  104 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  107 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  108 : 
         yyval = yyv[yysp-0];
         
       break;
							case  109 : 
         yyval = yyv[yysp-0];
         
       break;
							case  110 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = "";
         
       break;
							case  122 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  128 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
                                default : return;
                        }
                }

                public void InitTables()
                {
                        ////////////////////////////////////////////////////////////////
                        /// Init Table code:
                        ////////////////////////////////////////////////////////////////

					int yynacts   = 1226;
					int yyngotos  = 438;
					int yynstates = 222;
					int yynrules  = 128;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,11);yyac++; 
					yya[yyac] = new YYARec(259,12);yyac++; 
					yya[yyac] = new YYARec(262,13);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(310,39);yyac++; 
					yya[yyac] = new YYARec(311,40);yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(257,11);yyac++; 
					yya[yyac] = new YYARec(259,12);yyac++; 
					yya[yyac] = new YYARec(262,13);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(0,-1 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(309,45);yyac++; 
					yya[yyac] = new YYARec(258,46);yyac++; 
					yya[yyac] = new YYARec(263,50);yyac++; 
					yya[yyac] = new YYARec(264,51);yyac++; 
					yya[yyac] = new YYARec(307,52);yyac++; 
					yya[yyac] = new YYARec(310,39);yyac++; 
					yya[yyac] = new YYARec(311,40);yyac++; 
					yya[yyac] = new YYARec(258,-117 );yyac++; 
					yya[yyac] = new YYARec(258,53);yyac++; 
					yya[yyac] = new YYARec(258,54);yyac++; 
					yya[yyac] = new YYARec(258,55);yyac++; 
					yya[yyac] = new YYARec(263,56);yyac++; 
					yya[yyac] = new YYARec(258,57);yyac++; 
					yya[yyac] = new YYARec(263,58);yyac++; 
					yya[yyac] = new YYARec(258,-26 );yyac++; 
					yya[yyac] = new YYARec(310,39);yyac++; 
					yya[yyac] = new YYARec(311,40);yyac++; 
					yya[yyac] = new YYARec(267,75);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(296,76);yyac++; 
					yya[yyac] = new YYARec(298,77);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(262,13);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(262,13);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(258,88);yyac++; 
					yya[yyac] = new YYARec(264,89);yyac++; 
					yya[yyac] = new YYARec(267,75);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(296,76);yyac++; 
					yya[yyac] = new YYARec(298,77);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(258,91);yyac++; 
					yya[yyac] = new YYARec(265,92);yyac++; 
					yya[yyac] = new YYARec(263,99);yyac++; 
					yya[yyac] = new YYARec(266,100);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(310,39);yyac++; 
					yya[yyac] = new YYARec(311,40);yyac++; 
					yya[yyac] = new YYARec(258,-39 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(267,106);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,-121 );yyac++; 
					yya[yyac] = new YYARec(283,-121 );yyac++; 
					yya[yyac] = new YYARec(284,-121 );yyac++; 
					yya[yyac] = new YYARec(285,-121 );yyac++; 
					yya[yyac] = new YYARec(286,-121 );yyac++; 
					yya[yyac] = new YYARec(287,-121 );yyac++; 
					yya[yyac] = new YYARec(288,-121 );yyac++; 
					yya[yyac] = new YYARec(289,-121 );yyac++; 
					yya[yyac] = new YYARec(290,-121 );yyac++; 
					yya[yyac] = new YYARec(291,-121 );yyac++; 
					yya[yyac] = new YYARec(292,-121 );yyac++; 
					yya[yyac] = new YYARec(293,-121 );yyac++; 
					yya[yyac] = new YYARec(294,-121 );yyac++; 
					yya[yyac] = new YYARec(295,-121 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(267,108);yyac++; 
					yya[yyac] = new YYARec(267,110);yyac++; 
					yya[yyac] = new YYARec(260,111);yyac++; 
					yya[yyac] = new YYARec(261,112);yyac++; 
					yya[yyac] = new YYARec(258,113);yyac++; 
					yya[yyac] = new YYARec(258,114);yyac++; 
					yya[yyac] = new YYARec(307,52);yyac++; 
					yya[yyac] = new YYARec(258,-117 );yyac++; 
					yya[yyac] = new YYARec(263,-117 );yyac++; 
					yya[yyac] = new YYARec(268,-117 );yyac++; 
					yya[yyac] = new YYARec(269,-117 );yyac++; 
					yya[yyac] = new YYARec(270,-117 );yyac++; 
					yya[yyac] = new YYARec(271,-117 );yyac++; 
					yya[yyac] = new YYARec(272,-117 );yyac++; 
					yya[yyac] = new YYARec(273,-117 );yyac++; 
					yya[yyac] = new YYARec(274,-117 );yyac++; 
					yya[yyac] = new YYARec(275,-117 );yyac++; 
					yya[yyac] = new YYARec(276,-117 );yyac++; 
					yya[yyac] = new YYARec(277,-117 );yyac++; 
					yya[yyac] = new YYARec(278,-117 );yyac++; 
					yya[yyac] = new YYARec(279,-117 );yyac++; 
					yya[yyac] = new YYARec(280,-117 );yyac++; 
					yya[yyac] = new YYARec(281,-117 );yyac++; 
					yya[yyac] = new YYARec(282,-117 );yyac++; 
					yya[yyac] = new YYARec(283,-117 );yyac++; 
					yya[yyac] = new YYARec(284,-117 );yyac++; 
					yya[yyac] = new YYARec(285,-117 );yyac++; 
					yya[yyac] = new YYARec(286,-117 );yyac++; 
					yya[yyac] = new YYARec(287,-117 );yyac++; 
					yya[yyac] = new YYARec(288,-117 );yyac++; 
					yya[yyac] = new YYARec(289,-117 );yyac++; 
					yya[yyac] = new YYARec(290,-117 );yyac++; 
					yya[yyac] = new YYARec(291,-117 );yyac++; 
					yya[yyac] = new YYARec(292,-117 );yyac++; 
					yya[yyac] = new YYARec(293,-117 );yyac++; 
					yya[yyac] = new YYARec(294,-117 );yyac++; 
					yya[yyac] = new YYARec(295,-117 );yyac++; 
					yya[yyac] = new YYARec(299,-117 );yyac++; 
					yya[yyac] = new YYARec(300,-117 );yyac++; 
					yya[yyac] = new YYARec(301,-117 );yyac++; 
					yya[yyac] = new YYARec(302,-117 );yyac++; 
					yya[yyac] = new YYARec(303,-117 );yyac++; 
					yya[yyac] = new YYARec(304,-117 );yyac++; 
					yya[yyac] = new YYARec(305,-117 );yyac++; 
					yya[yyac] = new YYARec(306,-117 );yyac++; 
					yya[yyac] = new YYARec(308,-117 );yyac++; 
					yya[yyac] = new YYARec(309,-117 );yyac++; 
					yya[yyac] = new YYARec(263,115);yyac++; 
					yya[yyac] = new YYARec(267,75);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(296,76);yyac++; 
					yya[yyac] = new YYARec(298,77);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(267,75);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(296,76);yyac++; 
					yya[yyac] = new YYARec(298,77);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(263,120);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(258,-46 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(263,121);yyac++; 
					yya[yyac] = new YYARec(258,-50 );yyac++; 
					yya[yyac] = new YYARec(263,122);yyac++; 
					yya[yyac] = new YYARec(258,-51 );yyac++; 
					yya[yyac] = new YYARec(277,124);yyac++; 
					yya[yyac] = new YYARec(278,125);yyac++; 
					yya[yyac] = new YYARec(279,126);yyac++; 
					yya[yyac] = new YYARec(280,127);yyac++; 
					yya[yyac] = new YYARec(281,128);yyac++; 
					yya[yyac] = new YYARec(258,-48 );yyac++; 
					yya[yyac] = new YYARec(263,-48 );yyac++; 
					yya[yyac] = new YYARec(275,-48 );yyac++; 
					yya[yyac] = new YYARec(276,-48 );yyac++; 
					yya[yyac] = new YYARec(282,-48 );yyac++; 
					yya[yyac] = new YYARec(283,-48 );yyac++; 
					yya[yyac] = new YYARec(284,-48 );yyac++; 
					yya[yyac] = new YYARec(285,-48 );yyac++; 
					yya[yyac] = new YYARec(286,-48 );yyac++; 
					yya[yyac] = new YYARec(287,-48 );yyac++; 
					yya[yyac] = new YYARec(288,-48 );yyac++; 
					yya[yyac] = new YYARec(289,-48 );yyac++; 
					yya[yyac] = new YYARec(290,-48 );yyac++; 
					yya[yyac] = new YYARec(291,-48 );yyac++; 
					yya[yyac] = new YYARec(292,-48 );yyac++; 
					yya[yyac] = new YYARec(293,-48 );yyac++; 
					yya[yyac] = new YYARec(294,-48 );yyac++; 
					yya[yyac] = new YYARec(295,-48 );yyac++; 
					yya[yyac] = new YYARec(308,-48 );yyac++; 
					yya[yyac] = new YYARec(309,-48 );yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(267,75);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(296,76);yyac++; 
					yya[yyac] = new YYARec(298,77);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(268,132);yyac++; 
					yya[yyac] = new YYARec(299,133);yyac++; 
					yya[yyac] = new YYARec(300,134);yyac++; 
					yya[yyac] = new YYARec(301,135);yyac++; 
					yya[yyac] = new YYARec(302,136);yyac++; 
					yya[yyac] = new YYARec(303,137);yyac++; 
					yya[yyac] = new YYARec(304,138);yyac++; 
					yya[yyac] = new YYARec(305,139);yyac++; 
					yya[yyac] = new YYARec(306,140);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(309,45);yyac++; 
					yya[yyac] = new YYARec(269,145);yyac++; 
					yya[yyac] = new YYARec(270,146);yyac++; 
					yya[yyac] = new YYARec(271,147);yyac++; 
					yya[yyac] = new YYARec(272,148);yyac++; 
					yya[yyac] = new YYARec(273,149);yyac++; 
					yya[yyac] = new YYARec(274,150);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(268,-102 );yyac++; 
					yya[yyac] = new YYARec(299,-102 );yyac++; 
					yya[yyac] = new YYARec(300,-102 );yyac++; 
					yya[yyac] = new YYARec(301,-102 );yyac++; 
					yya[yyac] = new YYARec(302,-102 );yyac++; 
					yya[yyac] = new YYARec(303,-102 );yyac++; 
					yya[yyac] = new YYARec(304,-102 );yyac++; 
					yya[yyac] = new YYARec(305,-102 );yyac++; 
					yya[yyac] = new YYARec(306,-102 );yyac++; 
					yya[yyac] = new YYARec(267,154);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,-121 );yyac++; 
					yya[yyac] = new YYARec(283,-121 );yyac++; 
					yya[yyac] = new YYARec(284,-121 );yyac++; 
					yya[yyac] = new YYARec(285,-121 );yyac++; 
					yya[yyac] = new YYARec(286,-121 );yyac++; 
					yya[yyac] = new YYARec(287,-121 );yyac++; 
					yya[yyac] = new YYARec(288,-121 );yyac++; 
					yya[yyac] = new YYARec(289,-121 );yyac++; 
					yya[yyac] = new YYARec(290,-121 );yyac++; 
					yya[yyac] = new YYARec(291,-121 );yyac++; 
					yya[yyac] = new YYARec(292,-121 );yyac++; 
					yya[yyac] = new YYARec(293,-121 );yyac++; 
					yya[yyac] = new YYARec(294,-121 );yyac++; 
					yya[yyac] = new YYARec(295,-121 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(264,155);yyac++; 
					yya[yyac] = new YYARec(267,106);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,-121 );yyac++; 
					yya[yyac] = new YYARec(283,-121 );yyac++; 
					yya[yyac] = new YYARec(284,-121 );yyac++; 
					yya[yyac] = new YYARec(285,-121 );yyac++; 
					yya[yyac] = new YYARec(286,-121 );yyac++; 
					yya[yyac] = new YYARec(287,-121 );yyac++; 
					yya[yyac] = new YYARec(288,-121 );yyac++; 
					yya[yyac] = new YYARec(289,-121 );yyac++; 
					yya[yyac] = new YYARec(290,-121 );yyac++; 
					yya[yyac] = new YYARec(291,-121 );yyac++; 
					yya[yyac] = new YYARec(292,-121 );yyac++; 
					yya[yyac] = new YYARec(293,-121 );yyac++; 
					yya[yyac] = new YYARec(294,-121 );yyac++; 
					yya[yyac] = new YYARec(295,-121 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(264,157);yyac++; 
					yya[yyac] = new YYARec(267,106);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,-121 );yyac++; 
					yya[yyac] = new YYARec(283,-121 );yyac++; 
					yya[yyac] = new YYARec(284,-121 );yyac++; 
					yya[yyac] = new YYARec(285,-121 );yyac++; 
					yya[yyac] = new YYARec(286,-121 );yyac++; 
					yya[yyac] = new YYARec(287,-121 );yyac++; 
					yya[yyac] = new YYARec(288,-121 );yyac++; 
					yya[yyac] = new YYARec(289,-121 );yyac++; 
					yya[yyac] = new YYARec(290,-121 );yyac++; 
					yya[yyac] = new YYARec(291,-121 );yyac++; 
					yya[yyac] = new YYARec(292,-121 );yyac++; 
					yya[yyac] = new YYARec(293,-121 );yyac++; 
					yya[yyac] = new YYARec(294,-121 );yyac++; 
					yya[yyac] = new YYARec(295,-121 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(262,13);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(265,161);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(311,40);yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(311,40);yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(267,169);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,-121 );yyac++; 
					yya[yyac] = new YYARec(283,-121 );yyac++; 
					yya[yyac] = new YYARec(284,-121 );yyac++; 
					yya[yyac] = new YYARec(285,-121 );yyac++; 
					yya[yyac] = new YYARec(286,-121 );yyac++; 
					yya[yyac] = new YYARec(287,-121 );yyac++; 
					yya[yyac] = new YYARec(288,-121 );yyac++; 
					yya[yyac] = new YYARec(289,-121 );yyac++; 
					yya[yyac] = new YYARec(290,-121 );yyac++; 
					yya[yyac] = new YYARec(291,-121 );yyac++; 
					yya[yyac] = new YYARec(292,-121 );yyac++; 
					yya[yyac] = new YYARec(293,-121 );yyac++; 
					yya[yyac] = new YYARec(294,-121 );yyac++; 
					yya[yyac] = new YYARec(295,-121 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(267,172);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,-121 );yyac++; 
					yya[yyac] = new YYARec(283,-121 );yyac++; 
					yya[yyac] = new YYARec(284,-121 );yyac++; 
					yya[yyac] = new YYARec(285,-121 );yyac++; 
					yya[yyac] = new YYARec(286,-121 );yyac++; 
					yya[yyac] = new YYARec(287,-121 );yyac++; 
					yya[yyac] = new YYARec(288,-121 );yyac++; 
					yya[yyac] = new YYARec(289,-121 );yyac++; 
					yya[yyac] = new YYARec(290,-121 );yyac++; 
					yya[yyac] = new YYARec(291,-121 );yyac++; 
					yya[yyac] = new YYARec(292,-121 );yyac++; 
					yya[yyac] = new YYARec(293,-121 );yyac++; 
					yya[yyac] = new YYARec(294,-121 );yyac++; 
					yya[yyac] = new YYARec(295,-121 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(299,133);yyac++; 
					yya[yyac] = new YYARec(300,134);yyac++; 
					yya[yyac] = new YYARec(301,135);yyac++; 
					yya[yyac] = new YYARec(302,136);yyac++; 
					yya[yyac] = new YYARec(303,137);yyac++; 
					yya[yyac] = new YYARec(304,138);yyac++; 
					yya[yyac] = new YYARec(305,139);yyac++; 
					yya[yyac] = new YYARec(306,140);yyac++; 
					yya[yyac] = new YYARec(267,174);yyac++; 
					yya[yyac] = new YYARec(258,-119 );yyac++; 
					yya[yyac] = new YYARec(268,-119 );yyac++; 
					yya[yyac] = new YYARec(269,-119 );yyac++; 
					yya[yyac] = new YYARec(270,-119 );yyac++; 
					yya[yyac] = new YYARec(271,-119 );yyac++; 
					yya[yyac] = new YYARec(272,-119 );yyac++; 
					yya[yyac] = new YYARec(273,-119 );yyac++; 
					yya[yyac] = new YYARec(274,-119 );yyac++; 
					yya[yyac] = new YYARec(275,-119 );yyac++; 
					yya[yyac] = new YYARec(276,-119 );yyac++; 
					yya[yyac] = new YYARec(299,-119 );yyac++; 
					yya[yyac] = new YYARec(300,-119 );yyac++; 
					yya[yyac] = new YYARec(301,-119 );yyac++; 
					yya[yyac] = new YYARec(302,-119 );yyac++; 
					yya[yyac] = new YYARec(303,-119 );yyac++; 
					yya[yyac] = new YYARec(304,-119 );yyac++; 
					yya[yyac] = new YYARec(305,-119 );yyac++; 
					yya[yyac] = new YYARec(306,-119 );yyac++; 
					yya[yyac] = new YYARec(307,-119 );yyac++; 
					yya[yyac] = new YYARec(267,177);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,-121 );yyac++; 
					yya[yyac] = new YYARec(283,-121 );yyac++; 
					yya[yyac] = new YYARec(284,-121 );yyac++; 
					yya[yyac] = new YYARec(285,-121 );yyac++; 
					yya[yyac] = new YYARec(286,-121 );yyac++; 
					yya[yyac] = new YYARec(287,-121 );yyac++; 
					yya[yyac] = new YYARec(288,-121 );yyac++; 
					yya[yyac] = new YYARec(289,-121 );yyac++; 
					yya[yyac] = new YYARec(290,-121 );yyac++; 
					yya[yyac] = new YYARec(291,-121 );yyac++; 
					yya[yyac] = new YYARec(292,-121 );yyac++; 
					yya[yyac] = new YYARec(293,-121 );yyac++; 
					yya[yyac] = new YYARec(294,-121 );yyac++; 
					yya[yyac] = new YYARec(295,-121 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(268,178);yyac++; 
					yya[yyac] = new YYARec(299,133);yyac++; 
					yya[yyac] = new YYARec(300,134);yyac++; 
					yya[yyac] = new YYARec(301,135);yyac++; 
					yya[yyac] = new YYARec(302,136);yyac++; 
					yya[yyac] = new YYARec(303,137);yyac++; 
					yya[yyac] = new YYARec(304,138);yyac++; 
					yya[yyac] = new YYARec(305,139);yyac++; 
					yya[yyac] = new YYARec(306,140);yyac++; 
					yya[yyac] = new YYARec(268,180);yyac++; 
					yya[yyac] = new YYARec(269,145);yyac++; 
					yya[yyac] = new YYARec(270,146);yyac++; 
					yya[yyac] = new YYARec(271,147);yyac++; 
					yya[yyac] = new YYARec(272,148);yyac++; 
					yya[yyac] = new YYARec(273,149);yyac++; 
					yya[yyac] = new YYARec(274,150);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(299,-102 );yyac++; 
					yya[yyac] = new YYARec(300,-102 );yyac++; 
					yya[yyac] = new YYARec(301,-102 );yyac++; 
					yya[yyac] = new YYARec(302,-102 );yyac++; 
					yya[yyac] = new YYARec(303,-102 );yyac++; 
					yya[yyac] = new YYARec(304,-102 );yyac++; 
					yya[yyac] = new YYARec(305,-102 );yyac++; 
					yya[yyac] = new YYARec(306,-102 );yyac++; 
					yya[yyac] = new YYARec(267,154);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,-121 );yyac++; 
					yya[yyac] = new YYARec(283,-121 );yyac++; 
					yya[yyac] = new YYARec(284,-121 );yyac++; 
					yya[yyac] = new YYARec(285,-121 );yyac++; 
					yya[yyac] = new YYARec(286,-121 );yyac++; 
					yya[yyac] = new YYARec(287,-121 );yyac++; 
					yya[yyac] = new YYARec(288,-121 );yyac++; 
					yya[yyac] = new YYARec(289,-121 );yyac++; 
					yya[yyac] = new YYARec(290,-121 );yyac++; 
					yya[yyac] = new YYARec(291,-121 );yyac++; 
					yya[yyac] = new YYARec(292,-121 );yyac++; 
					yya[yyac] = new YYARec(293,-121 );yyac++; 
					yya[yyac] = new YYARec(294,-121 );yyac++; 
					yya[yyac] = new YYARec(295,-121 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(267,75);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(296,76);yyac++; 
					yya[yyac] = new YYARec(298,77);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(268,183);yyac++; 
					yya[yyac] = new YYARec(299,133);yyac++; 
					yya[yyac] = new YYARec(300,134);yyac++; 
					yya[yyac] = new YYARec(301,135);yyac++; 
					yya[yyac] = new YYARec(302,136);yyac++; 
					yya[yyac] = new YYARec(303,137);yyac++; 
					yya[yyac] = new YYARec(304,138);yyac++; 
					yya[yyac] = new YYARec(305,139);yyac++; 
					yya[yyac] = new YYARec(306,140);yyac++; 
					yya[yyac] = new YYARec(267,75);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(296,76);yyac++; 
					yya[yyac] = new YYARec(298,77);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(268,185);yyac++; 
					yya[yyac] = new YYARec(299,133);yyac++; 
					yya[yyac] = new YYARec(300,134);yyac++; 
					yya[yyac] = new YYARec(301,135);yyac++; 
					yya[yyac] = new YYARec(302,136);yyac++; 
					yya[yyac] = new YYARec(303,137);yyac++; 
					yya[yyac] = new YYARec(304,138);yyac++; 
					yya[yyac] = new YYARec(305,139);yyac++; 
					yya[yyac] = new YYARec(306,140);yyac++; 
					yya[yyac] = new YYARec(261,186);yyac++; 
					yya[yyac] = new YYARec(263,187);yyac++; 
					yya[yyac] = new YYARec(258,-24 );yyac++; 
					yya[yyac] = new YYARec(297,188);yyac++; 
					yya[yyac] = new YYARec(263,189);yyac++; 
					yya[yyac] = new YYARec(258,-56 );yyac++; 
					yya[yyac] = new YYARec(269,145);yyac++; 
					yya[yyac] = new YYARec(270,146);yyac++; 
					yya[yyac] = new YYARec(271,147);yyac++; 
					yya[yyac] = new YYARec(272,148);yyac++; 
					yya[yyac] = new YYARec(273,149);yyac++; 
					yya[yyac] = new YYARec(274,150);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(258,-58 );yyac++; 
					yya[yyac] = new YYARec(267,169);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,-121 );yyac++; 
					yya[yyac] = new YYARec(283,-121 );yyac++; 
					yya[yyac] = new YYARec(284,-121 );yyac++; 
					yya[yyac] = new YYARec(285,-121 );yyac++; 
					yya[yyac] = new YYARec(286,-121 );yyac++; 
					yya[yyac] = new YYARec(287,-121 );yyac++; 
					yya[yyac] = new YYARec(288,-121 );yyac++; 
					yya[yyac] = new YYARec(289,-121 );yyac++; 
					yya[yyac] = new YYARec(290,-121 );yyac++; 
					yya[yyac] = new YYARec(291,-121 );yyac++; 
					yya[yyac] = new YYARec(292,-121 );yyac++; 
					yya[yyac] = new YYARec(293,-121 );yyac++; 
					yya[yyac] = new YYARec(294,-121 );yyac++; 
					yya[yyac] = new YYARec(295,-121 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(269,145);yyac++; 
					yya[yyac] = new YYARec(270,146);yyac++; 
					yya[yyac] = new YYARec(271,147);yyac++; 
					yya[yyac] = new YYARec(272,148);yyac++; 
					yya[yyac] = new YYARec(273,149);yyac++; 
					yya[yyac] = new YYARec(274,150);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(268,-105 );yyac++; 
					yya[yyac] = new YYARec(299,-105 );yyac++; 
					yya[yyac] = new YYARec(300,-105 );yyac++; 
					yya[yyac] = new YYARec(301,-105 );yyac++; 
					yya[yyac] = new YYARec(302,-105 );yyac++; 
					yya[yyac] = new YYARec(303,-105 );yyac++; 
					yya[yyac] = new YYARec(304,-105 );yyac++; 
					yya[yyac] = new YYARec(305,-105 );yyac++; 
					yya[yyac] = new YYARec(306,-105 );yyac++; 
					yya[yyac] = new YYARec(267,154);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,-121 );yyac++; 
					yya[yyac] = new YYARec(283,-121 );yyac++; 
					yya[yyac] = new YYARec(284,-121 );yyac++; 
					yya[yyac] = new YYARec(285,-121 );yyac++; 
					yya[yyac] = new YYARec(286,-121 );yyac++; 
					yya[yyac] = new YYARec(287,-121 );yyac++; 
					yya[yyac] = new YYARec(288,-121 );yyac++; 
					yya[yyac] = new YYARec(289,-121 );yyac++; 
					yya[yyac] = new YYARec(290,-121 );yyac++; 
					yya[yyac] = new YYARec(291,-121 );yyac++; 
					yya[yyac] = new YYARec(292,-121 );yyac++; 
					yya[yyac] = new YYARec(293,-121 );yyac++; 
					yya[yyac] = new YYARec(294,-121 );yyac++; 
					yya[yyac] = new YYARec(295,-121 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(267,193);yyac++; 
					yya[yyac] = new YYARec(267,169);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,-121 );yyac++; 
					yya[yyac] = new YYARec(283,-121 );yyac++; 
					yya[yyac] = new YYARec(284,-121 );yyac++; 
					yya[yyac] = new YYARec(285,-121 );yyac++; 
					yya[yyac] = new YYARec(286,-121 );yyac++; 
					yya[yyac] = new YYARec(287,-121 );yyac++; 
					yya[yyac] = new YYARec(288,-121 );yyac++; 
					yya[yyac] = new YYARec(289,-121 );yyac++; 
					yya[yyac] = new YYARec(290,-121 );yyac++; 
					yya[yyac] = new YYARec(291,-121 );yyac++; 
					yya[yyac] = new YYARec(292,-121 );yyac++; 
					yya[yyac] = new YYARec(293,-121 );yyac++; 
					yya[yyac] = new YYARec(294,-121 );yyac++; 
					yya[yyac] = new YYARec(295,-121 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(267,169);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,-121 );yyac++; 
					yya[yyac] = new YYARec(283,-121 );yyac++; 
					yya[yyac] = new YYARec(284,-121 );yyac++; 
					yya[yyac] = new YYARec(285,-121 );yyac++; 
					yya[yyac] = new YYARec(286,-121 );yyac++; 
					yya[yyac] = new YYARec(287,-121 );yyac++; 
					yya[yyac] = new YYARec(288,-121 );yyac++; 
					yya[yyac] = new YYARec(289,-121 );yyac++; 
					yya[yyac] = new YYARec(290,-121 );yyac++; 
					yya[yyac] = new YYARec(291,-121 );yyac++; 
					yya[yyac] = new YYARec(292,-121 );yyac++; 
					yya[yyac] = new YYARec(293,-121 );yyac++; 
					yya[yyac] = new YYARec(294,-121 );yyac++; 
					yya[yyac] = new YYARec(295,-121 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(267,172);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,-121 );yyac++; 
					yya[yyac] = new YYARec(283,-121 );yyac++; 
					yya[yyac] = new YYARec(284,-121 );yyac++; 
					yya[yyac] = new YYARec(285,-121 );yyac++; 
					yya[yyac] = new YYARec(286,-121 );yyac++; 
					yya[yyac] = new YYARec(287,-121 );yyac++; 
					yya[yyac] = new YYARec(288,-121 );yyac++; 
					yya[yyac] = new YYARec(289,-121 );yyac++; 
					yya[yyac] = new YYARec(290,-121 );yyac++; 
					yya[yyac] = new YYARec(291,-121 );yyac++; 
					yya[yyac] = new YYARec(292,-121 );yyac++; 
					yya[yyac] = new YYARec(293,-121 );yyac++; 
					yya[yyac] = new YYARec(294,-121 );yyac++; 
					yya[yyac] = new YYARec(295,-121 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(268,197);yyac++; 
					yya[yyac] = new YYARec(265,198);yyac++; 
					yya[yyac] = new YYARec(264,199);yyac++; 
					yya[yyac] = new YYARec(299,133);yyac++; 
					yya[yyac] = new YYARec(300,134);yyac++; 
					yya[yyac] = new YYARec(301,135);yyac++; 
					yya[yyac] = new YYARec(302,136);yyac++; 
					yya[yyac] = new YYARec(303,137);yyac++; 
					yya[yyac] = new YYARec(304,138);yyac++; 
					yya[yyac] = new YYARec(305,139);yyac++; 
					yya[yyac] = new YYARec(306,140);yyac++; 
					yya[yyac] = new YYARec(265,200);yyac++; 
					yya[yyac] = new YYARec(264,201);yyac++; 
					yya[yyac] = new YYARec(299,133);yyac++; 
					yya[yyac] = new YYARec(300,134);yyac++; 
					yya[yyac] = new YYARec(301,135);yyac++; 
					yya[yyac] = new YYARec(302,136);yyac++; 
					yya[yyac] = new YYARec(303,137);yyac++; 
					yya[yyac] = new YYARec(304,138);yyac++; 
					yya[yyac] = new YYARec(305,139);yyac++; 
					yya[yyac] = new YYARec(306,140);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(264,203);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(268,180);yyac++; 
					yya[yyac] = new YYARec(269,145);yyac++; 
					yya[yyac] = new YYARec(270,146);yyac++; 
					yya[yyac] = new YYARec(271,147);yyac++; 
					yya[yyac] = new YYARec(272,148);yyac++; 
					yya[yyac] = new YYARec(273,149);yyac++; 
					yya[yyac] = new YYARec(274,150);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(268,205);yyac++; 
					yya[yyac] = new YYARec(267,106);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,-121 );yyac++; 
					yya[yyac] = new YYARec(283,-121 );yyac++; 
					yya[yyac] = new YYARec(284,-121 );yyac++; 
					yya[yyac] = new YYARec(285,-121 );yyac++; 
					yya[yyac] = new YYARec(286,-121 );yyac++; 
					yya[yyac] = new YYARec(287,-121 );yyac++; 
					yya[yyac] = new YYARec(288,-121 );yyac++; 
					yya[yyac] = new YYARec(289,-121 );yyac++; 
					yya[yyac] = new YYARec(290,-121 );yyac++; 
					yya[yyac] = new YYARec(291,-121 );yyac++; 
					yya[yyac] = new YYARec(292,-121 );yyac++; 
					yya[yyac] = new YYARec(293,-121 );yyac++; 
					yya[yyac] = new YYARec(294,-121 );yyac++; 
					yya[yyac] = new YYARec(295,-121 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(268,207);yyac++; 
					yya[yyac] = new YYARec(269,145);yyac++; 
					yya[yyac] = new YYARec(270,146);yyac++; 
					yya[yyac] = new YYARec(271,147);yyac++; 
					yya[yyac] = new YYARec(272,148);yyac++; 
					yya[yyac] = new YYARec(273,149);yyac++; 
					yya[yyac] = new YYARec(274,150);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(268,208);yyac++; 
					yya[yyac] = new YYARec(269,145);yyac++; 
					yya[yyac] = new YYARec(270,146);yyac++; 
					yya[yyac] = new YYARec(271,147);yyac++; 
					yya[yyac] = new YYARec(272,148);yyac++; 
					yya[yyac] = new YYARec(273,149);yyac++; 
					yya[yyac] = new YYARec(274,150);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(299,-103 );yyac++; 
					yya[yyac] = new YYARec(300,-103 );yyac++; 
					yya[yyac] = new YYARec(301,-103 );yyac++; 
					yya[yyac] = new YYARec(302,-103 );yyac++; 
					yya[yyac] = new YYARec(303,-103 );yyac++; 
					yya[yyac] = new YYARec(304,-103 );yyac++; 
					yya[yyac] = new YYARec(305,-103 );yyac++; 
					yya[yyac] = new YYARec(306,-103 );yyac++; 
					yya[yyac] = new YYARec(268,-107 );yyac++; 
					yya[yyac] = new YYARec(299,-101 );yyac++; 
					yya[yyac] = new YYARec(300,-101 );yyac++; 
					yya[yyac] = new YYARec(301,-101 );yyac++; 
					yya[yyac] = new YYARec(302,-101 );yyac++; 
					yya[yyac] = new YYARec(303,-101 );yyac++; 
					yya[yyac] = new YYARec(304,-101 );yyac++; 
					yya[yyac] = new YYARec(305,-101 );yyac++; 
					yya[yyac] = new YYARec(306,-101 );yyac++; 
					yya[yyac] = new YYARec(268,-106 );yyac++; 
					yya[yyac] = new YYARec(267,75);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(296,76);yyac++; 
					yya[yyac] = new YYARec(298,77);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(267,75);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(296,76);yyac++; 
					yya[yyac] = new YYARec(298,77);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(263,211);yyac++; 
					yya[yyac] = new YYARec(267,75);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(296,76);yyac++; 
					yya[yyac] = new YYARec(298,77);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(268,213);yyac++; 
					yya[yyac] = new YYARec(299,133);yyac++; 
					yya[yyac] = new YYARec(300,134);yyac++; 
					yya[yyac] = new YYARec(301,135);yyac++; 
					yya[yyac] = new YYARec(302,136);yyac++; 
					yya[yyac] = new YYARec(303,137);yyac++; 
					yya[yyac] = new YYARec(304,138);yyac++; 
					yya[yyac] = new YYARec(305,139);yyac++; 
					yya[yyac] = new YYARec(306,140);yyac++; 
					yya[yyac] = new YYARec(265,214);yyac++; 
					yya[yyac] = new YYARec(265,215);yyac++; 
					yya[yyac] = new YYARec(275,37);yyac++; 
					yya[yyac] = new YYARec(276,38);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(309,-121 );yyac++; 
					yya[yyac] = new YYARec(265,217);yyac++; 
					yya[yyac] = new YYARec(299,133);yyac++; 
					yya[yyac] = new YYARec(300,134);yyac++; 
					yya[yyac] = new YYARec(301,135);yyac++; 
					yya[yyac] = new YYARec(302,136);yyac++; 
					yya[yyac] = new YYARec(303,137);yyac++; 
					yya[yyac] = new YYARec(304,138);yyac++; 
					yya[yyac] = new YYARec(305,139);yyac++; 
					yya[yyac] = new YYARec(306,140);yyac++; 
					yya[yyac] = new YYARec(264,-127 );yyac++; 
					yya[yyac] = new YYARec(297,218);yyac++; 
					yya[yyac] = new YYARec(265,-97 );yyac++; 
					yya[yyac] = new YYARec(267,-97 );yyac++; 
					yya[yyac] = new YYARec(282,-97 );yyac++; 
					yya[yyac] = new YYARec(283,-97 );yyac++; 
					yya[yyac] = new YYARec(284,-97 );yyac++; 
					yya[yyac] = new YYARec(285,-97 );yyac++; 
					yya[yyac] = new YYARec(286,-97 );yyac++; 
					yya[yyac] = new YYARec(287,-97 );yyac++; 
					yya[yyac] = new YYARec(288,-97 );yyac++; 
					yya[yyac] = new YYARec(289,-97 );yyac++; 
					yya[yyac] = new YYARec(290,-97 );yyac++; 
					yya[yyac] = new YYARec(291,-97 );yyac++; 
					yya[yyac] = new YYARec(292,-97 );yyac++; 
					yya[yyac] = new YYARec(293,-97 );yyac++; 
					yya[yyac] = new YYARec(294,-97 );yyac++; 
					yya[yyac] = new YYARec(295,-97 );yyac++; 
					yya[yyac] = new YYARec(296,-97 );yyac++; 
					yya[yyac] = new YYARec(298,-97 );yyac++; 
					yya[yyac] = new YYARec(308,-97 );yyac++; 
					yya[yyac] = new YYARec(264,219);yyac++; 
					yya[yyac] = new YYARec(267,75);yyac++; 
					yya[yyac] = new YYARec(282,14);yyac++; 
					yya[yyac] = new YYARec(283,15);yyac++; 
					yya[yyac] = new YYARec(284,16);yyac++; 
					yya[yyac] = new YYARec(285,17);yyac++; 
					yya[yyac] = new YYARec(286,18);yyac++; 
					yya[yyac] = new YYARec(287,19);yyac++; 
					yya[yyac] = new YYARec(288,20);yyac++; 
					yya[yyac] = new YYARec(289,21);yyac++; 
					yya[yyac] = new YYARec(290,22);yyac++; 
					yya[yyac] = new YYARec(291,23);yyac++; 
					yya[yyac] = new YYARec(292,24);yyac++; 
					yya[yyac] = new YYARec(293,25);yyac++; 
					yya[yyac] = new YYARec(294,26);yyac++; 
					yya[yyac] = new YYARec(295,27);yyac++; 
					yya[yyac] = new YYARec(296,76);yyac++; 
					yya[yyac] = new YYARec(298,77);yyac++; 
					yya[yyac] = new YYARec(308,28);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(265,221);yyac++;

					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-11,2);yygc++; 
					yyg[yygc] = new YYARec(-10,3);yygc++; 
					yyg[yygc] = new YYARec(-9,4);yygc++; 
					yyg[yygc] = new YYARec(-8,5);yygc++; 
					yyg[yygc] = new YYARec(-6,6);yygc++; 
					yyg[yygc] = new YYARec(-5,7);yygc++; 
					yyg[yygc] = new YYARec(-4,8);yygc++; 
					yyg[yygc] = new YYARec(-3,9);yygc++; 
					yyg[yygc] = new YYARec(-2,10);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-39,30);yygc++; 
					yyg[yygc] = new YYARec(-16,31);yygc++; 
					yyg[yygc] = new YYARec(-15,32);yygc++; 
					yyg[yygc] = new YYARec(-14,33);yygc++; 
					yyg[yygc] = new YYARec(-13,34);yygc++; 
					yyg[yygc] = new YYARec(-12,35);yygc++; 
					yyg[yygc] = new YYARec(-6,36);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-11,2);yygc++; 
					yyg[yygc] = new YYARec(-10,3);yygc++; 
					yyg[yygc] = new YYARec(-9,4);yygc++; 
					yyg[yygc] = new YYARec(-8,5);yygc++; 
					yyg[yygc] = new YYARec(-6,6);yygc++; 
					yyg[yygc] = new YYARec(-5,7);yygc++; 
					yyg[yygc] = new YYARec(-4,8);yygc++; 
					yyg[yygc] = new YYARec(-3,9);yygc++; 
					yyg[yygc] = new YYARec(-2,41);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-6,42);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-6,43);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-6,44);yygc++; 
					yyg[yygc] = new YYARec(-17,47);yygc++; 
					yyg[yygc] = new YYARec(-16,48);yygc++; 
					yyg[yygc] = new YYARec(-15,49);yygc++; 
					yyg[yygc] = new YYARec(-17,59);yygc++; 
					yyg[yygc] = new YYARec(-16,48);yygc++; 
					yyg[yygc] = new YYARec(-15,49);yygc++; 
					yyg[yygc] = new YYARec(-43,60);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-31,61);yygc++; 
					yyg[yygc] = new YYARec(-30,62);yygc++; 
					yyg[yygc] = new YYARec(-29,63);yygc++; 
					yyg[yygc] = new YYARec(-28,64);yygc++; 
					yyg[yygc] = new YYARec(-27,65);yygc++; 
					yyg[yygc] = new YYARec(-26,66);yygc++; 
					yyg[yygc] = new YYARec(-25,67);yygc++; 
					yyg[yygc] = new YYARec(-24,68);yygc++; 
					yyg[yygc] = new YYARec(-23,69);yygc++; 
					yyg[yygc] = new YYARec(-22,70);yygc++; 
					yyg[yygc] = new YYARec(-21,71);yygc++; 
					yyg[yygc] = new YYARec(-20,72);yygc++; 
					yyg[yygc] = new YYARec(-19,73);yygc++; 
					yyg[yygc] = new YYARec(-6,74);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-6,78);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-11,2);yygc++; 
					yyg[yygc] = new YYARec(-10,3);yygc++; 
					yyg[yygc] = new YYARec(-9,4);yygc++; 
					yyg[yygc] = new YYARec(-8,5);yygc++; 
					yyg[yygc] = new YYARec(-7,79);yygc++; 
					yyg[yygc] = new YYARec(-6,6);yygc++; 
					yyg[yygc] = new YYARec(-4,80);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-11,2);yygc++; 
					yyg[yygc] = new YYARec(-10,3);yygc++; 
					yyg[yygc] = new YYARec(-9,4);yygc++; 
					yyg[yygc] = new YYARec(-8,5);yygc++; 
					yyg[yygc] = new YYARec(-7,81);yygc++; 
					yyg[yygc] = new YYARec(-6,6);yygc++; 
					yyg[yygc] = new YYARec(-4,80);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-39,30);yygc++; 
					yyg[yygc] = new YYARec(-13,82);yygc++; 
					yyg[yygc] = new YYARec(-12,83);yygc++; 
					yyg[yygc] = new YYARec(-6,84);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-39,30);yygc++; 
					yyg[yygc] = new YYARec(-18,85);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-6,87);yygc++; 
					yyg[yygc] = new YYARec(-43,60);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-31,61);yygc++; 
					yyg[yygc] = new YYARec(-30,62);yygc++; 
					yyg[yygc] = new YYARec(-29,63);yygc++; 
					yyg[yygc] = new YYARec(-28,64);yygc++; 
					yyg[yygc] = new YYARec(-27,65);yygc++; 
					yyg[yygc] = new YYARec(-26,66);yygc++; 
					yyg[yygc] = new YYARec(-25,67);yygc++; 
					yyg[yygc] = new YYARec(-24,68);yygc++; 
					yyg[yygc] = new YYARec(-23,69);yygc++; 
					yyg[yygc] = new YYARec(-22,70);yygc++; 
					yyg[yygc] = new YYARec(-21,71);yygc++; 
					yyg[yygc] = new YYARec(-20,72);yygc++; 
					yyg[yygc] = new YYARec(-19,90);yygc++; 
					yyg[yygc] = new YYARec(-6,74);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-39,30);yygc++; 
					yyg[yygc] = new YYARec(-33,93);yygc++; 
					yyg[yygc] = new YYARec(-32,94);yygc++; 
					yyg[yygc] = new YYARec(-16,95);yygc++; 
					yyg[yygc] = new YYARec(-15,96);yygc++; 
					yyg[yygc] = new YYARec(-13,97);yygc++; 
					yyg[yygc] = new YYARec(-12,98);yygc++; 
					yyg[yygc] = new YYARec(-6,84);yygc++; 
					yyg[yygc] = new YYARec(-42,101);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-39,102);yygc++; 
					yyg[yygc] = new YYARec(-36,103);yygc++; 
					yyg[yygc] = new YYARec(-35,104);yygc++; 
					yyg[yygc] = new YYARec(-13,105);yygc++; 
					yyg[yygc] = new YYARec(-43,107);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-43,60);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-31,61);yygc++; 
					yyg[yygc] = new YYARec(-30,62);yygc++; 
					yyg[yygc] = new YYARec(-29,63);yygc++; 
					yyg[yygc] = new YYARec(-28,64);yygc++; 
					yyg[yygc] = new YYARec(-27,65);yygc++; 
					yyg[yygc] = new YYARec(-26,66);yygc++; 
					yyg[yygc] = new YYARec(-25,67);yygc++; 
					yyg[yygc] = new YYARec(-24,68);yygc++; 
					yyg[yygc] = new YYARec(-23,69);yygc++; 
					yyg[yygc] = new YYARec(-22,70);yygc++; 
					yyg[yygc] = new YYARec(-21,71);yygc++; 
					yyg[yygc] = new YYARec(-20,72);yygc++; 
					yyg[yygc] = new YYARec(-19,116);yygc++; 
					yyg[yygc] = new YYARec(-6,74);yygc++; 
					yyg[yygc] = new YYARec(-43,60);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-31,61);yygc++; 
					yyg[yygc] = new YYARec(-30,62);yygc++; 
					yyg[yygc] = new YYARec(-29,63);yygc++; 
					yyg[yygc] = new YYARec(-28,64);yygc++; 
					yyg[yygc] = new YYARec(-27,65);yygc++; 
					yyg[yygc] = new YYARec(-26,66);yygc++; 
					yyg[yygc] = new YYARec(-25,67);yygc++; 
					yyg[yygc] = new YYARec(-24,68);yygc++; 
					yyg[yygc] = new YYARec(-23,69);yygc++; 
					yyg[yygc] = new YYARec(-22,70);yygc++; 
					yyg[yygc] = new YYARec(-21,71);yygc++; 
					yyg[yygc] = new YYARec(-20,72);yygc++; 
					yyg[yygc] = new YYARec(-19,117);yygc++; 
					yyg[yygc] = new YYARec(-6,74);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-39,30);yygc++; 
					yyg[yygc] = new YYARec(-33,93);yygc++; 
					yyg[yygc] = new YYARec(-32,118);yygc++; 
					yyg[yygc] = new YYARec(-13,97);yygc++; 
					yyg[yygc] = new YYARec(-12,119);yygc++; 
					yyg[yygc] = new YYARec(-6,84);yygc++; 
					yyg[yygc] = new YYARec(-34,123);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-39,30);yygc++; 
					yyg[yygc] = new YYARec(-33,93);yygc++; 
					yyg[yygc] = new YYARec(-32,129);yygc++; 
					yyg[yygc] = new YYARec(-13,97);yygc++; 
					yyg[yygc] = new YYARec(-12,119);yygc++; 
					yyg[yygc] = new YYARec(-6,84);yygc++; 
					yyg[yygc] = new YYARec(-43,60);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-31,61);yygc++; 
					yyg[yygc] = new YYARec(-30,62);yygc++; 
					yyg[yygc] = new YYARec(-29,63);yygc++; 
					yyg[yygc] = new YYARec(-28,64);yygc++; 
					yyg[yygc] = new YYARec(-27,65);yygc++; 
					yyg[yygc] = new YYARec(-26,66);yygc++; 
					yyg[yygc] = new YYARec(-25,67);yygc++; 
					yyg[yygc] = new YYARec(-24,68);yygc++; 
					yyg[yygc] = new YYARec(-23,69);yygc++; 
					yyg[yygc] = new YYARec(-22,70);yygc++; 
					yyg[yygc] = new YYARec(-21,71);yygc++; 
					yyg[yygc] = new YYARec(-20,72);yygc++; 
					yyg[yygc] = new YYARec(-19,130);yygc++; 
					yyg[yygc] = new YYARec(-6,74);yygc++; 
					yyg[yygc] = new YYARec(-45,131);yygc++; 
					yyg[yygc] = new YYARec(-40,141);yygc++; 
					yyg[yygc] = new YYARec(-12,142);yygc++; 
					yyg[yygc] = new YYARec(-6,84);yygc++; 
					yyg[yygc] = new YYARec(-41,143);yygc++; 
					yyg[yygc] = new YYARec(-37,144);yygc++; 
					yyg[yygc] = new YYARec(-44,151);yygc++; 
					yyg[yygc] = new YYARec(-42,152);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-39,102);yygc++; 
					yyg[yygc] = new YYARec(-36,103);yygc++; 
					yyg[yygc] = new YYARec(-35,153);yygc++; 
					yyg[yygc] = new YYARec(-13,105);yygc++; 
					yyg[yygc] = new YYARec(-42,156);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-39,102);yygc++; 
					yyg[yygc] = new YYARec(-36,103);yygc++; 
					yyg[yygc] = new YYARec(-35,104);yygc++; 
					yyg[yygc] = new YYARec(-13,105);yygc++; 
					yyg[yygc] = new YYARec(-42,158);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-39,102);yygc++; 
					yyg[yygc] = new YYARec(-36,103);yygc++; 
					yyg[yygc] = new YYARec(-35,104);yygc++; 
					yyg[yygc] = new YYARec(-13,105);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-11,2);yygc++; 
					yyg[yygc] = new YYARec(-10,3);yygc++; 
					yyg[yygc] = new YYARec(-9,4);yygc++; 
					yyg[yygc] = new YYARec(-8,5);yygc++; 
					yyg[yygc] = new YYARec(-6,6);yygc++; 
					yyg[yygc] = new YYARec(-4,159);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-39,30);yygc++; 
					yyg[yygc] = new YYARec(-18,160);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-6,87);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-39,30);yygc++; 
					yyg[yygc] = new YYARec(-33,93);yygc++; 
					yyg[yygc] = new YYARec(-32,162);yygc++; 
					yyg[yygc] = new YYARec(-13,97);yygc++; 
					yyg[yygc] = new YYARec(-12,119);yygc++; 
					yyg[yygc] = new YYARec(-6,84);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-39,30);yygc++; 
					yyg[yygc] = new YYARec(-18,163);yygc++; 
					yyg[yygc] = new YYARec(-15,164);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-6,87);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-39,30);yygc++; 
					yyg[yygc] = new YYARec(-15,165);yygc++; 
					yyg[yygc] = new YYARec(-13,166);yygc++; 
					yyg[yygc] = new YYARec(-12,167);yygc++; 
					yyg[yygc] = new YYARec(-6,84);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-39,102);yygc++; 
					yyg[yygc] = new YYARec(-36,103);yygc++; 
					yyg[yygc] = new YYARec(-35,168);yygc++; 
					yyg[yygc] = new YYARec(-13,105);yygc++; 
					yyg[yygc] = new YYARec(-46,170);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-39,102);yygc++; 
					yyg[yygc] = new YYARec(-36,103);yygc++; 
					yyg[yygc] = new YYARec(-35,171);yygc++; 
					yyg[yygc] = new YYARec(-13,105);yygc++; 
					yyg[yygc] = new YYARec(-45,173);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-39,102);yygc++; 
					yyg[yygc] = new YYARec(-38,175);yygc++; 
					yyg[yygc] = new YYARec(-36,176);yygc++; 
					yyg[yygc] = new YYARec(-13,105);yygc++; 
					yyg[yygc] = new YYARec(-45,179);yygc++; 
					yyg[yygc] = new YYARec(-41,143);yygc++; 
					yyg[yygc] = new YYARec(-37,144);yygc++; 
					yyg[yygc] = new YYARec(-44,181);yygc++; 
					yyg[yygc] = new YYARec(-42,152);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-39,102);yygc++; 
					yyg[yygc] = new YYARec(-36,103);yygc++; 
					yyg[yygc] = new YYARec(-35,153);yygc++; 
					yyg[yygc] = new YYARec(-13,105);yygc++; 
					yyg[yygc] = new YYARec(-43,60);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-31,61);yygc++; 
					yyg[yygc] = new YYARec(-30,62);yygc++; 
					yyg[yygc] = new YYARec(-29,63);yygc++; 
					yyg[yygc] = new YYARec(-28,64);yygc++; 
					yyg[yygc] = new YYARec(-27,65);yygc++; 
					yyg[yygc] = new YYARec(-26,66);yygc++; 
					yyg[yygc] = new YYARec(-25,67);yygc++; 
					yyg[yygc] = new YYARec(-24,68);yygc++; 
					yyg[yygc] = new YYARec(-23,69);yygc++; 
					yyg[yygc] = new YYARec(-22,70);yygc++; 
					yyg[yygc] = new YYARec(-21,71);yygc++; 
					yyg[yygc] = new YYARec(-20,72);yygc++; 
					yyg[yygc] = new YYARec(-19,182);yygc++; 
					yyg[yygc] = new YYARec(-6,74);yygc++; 
					yyg[yygc] = new YYARec(-45,131);yygc++; 
					yyg[yygc] = new YYARec(-43,60);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-31,61);yygc++; 
					yyg[yygc] = new YYARec(-30,62);yygc++; 
					yyg[yygc] = new YYARec(-29,63);yygc++; 
					yyg[yygc] = new YYARec(-28,64);yygc++; 
					yyg[yygc] = new YYARec(-27,65);yygc++; 
					yyg[yygc] = new YYARec(-26,66);yygc++; 
					yyg[yygc] = new YYARec(-25,67);yygc++; 
					yyg[yygc] = new YYARec(-24,68);yygc++; 
					yyg[yygc] = new YYARec(-23,69);yygc++; 
					yyg[yygc] = new YYARec(-22,70);yygc++; 
					yyg[yygc] = new YYARec(-21,71);yygc++; 
					yyg[yygc] = new YYARec(-20,72);yygc++; 
					yyg[yygc] = new YYARec(-19,184);yygc++; 
					yyg[yygc] = new YYARec(-6,74);yygc++; 
					yyg[yygc] = new YYARec(-45,131);yygc++; 
					yyg[yygc] = new YYARec(-41,143);yygc++; 
					yyg[yygc] = new YYARec(-37,144);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-39,102);yygc++; 
					yyg[yygc] = new YYARec(-36,103);yygc++; 
					yyg[yygc] = new YYARec(-35,190);yygc++; 
					yyg[yygc] = new YYARec(-13,105);yygc++; 
					yyg[yygc] = new YYARec(-41,143);yygc++; 
					yyg[yygc] = new YYARec(-37,144);yygc++; 
					yyg[yygc] = new YYARec(-44,191);yygc++; 
					yyg[yygc] = new YYARec(-42,152);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-39,102);yygc++; 
					yyg[yygc] = new YYARec(-36,103);yygc++; 
					yyg[yygc] = new YYARec(-35,153);yygc++; 
					yyg[yygc] = new YYARec(-13,105);yygc++; 
					yyg[yygc] = new YYARec(-43,192);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-39,102);yygc++; 
					yyg[yygc] = new YYARec(-36,103);yygc++; 
					yyg[yygc] = new YYARec(-35,194);yygc++; 
					yyg[yygc] = new YYARec(-13,105);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-39,102);yygc++; 
					yyg[yygc] = new YYARec(-36,103);yygc++; 
					yyg[yygc] = new YYARec(-35,195);yygc++; 
					yyg[yygc] = new YYARec(-13,105);yygc++; 
					yyg[yygc] = new YYARec(-46,196);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-39,102);yygc++; 
					yyg[yygc] = new YYARec(-36,103);yygc++; 
					yyg[yygc] = new YYARec(-35,171);yygc++; 
					yyg[yygc] = new YYARec(-13,105);yygc++; 
					yyg[yygc] = new YYARec(-45,173);yygc++; 
					yyg[yygc] = new YYARec(-45,173);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-39,30);yygc++; 
					yyg[yygc] = new YYARec(-18,202);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-6,87);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-39,30);yygc++; 
					yyg[yygc] = new YYARec(-18,204);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-6,87);yygc++; 
					yyg[yygc] = new YYARec(-41,143);yygc++; 
					yyg[yygc] = new YYARec(-37,144);yygc++; 
					yyg[yygc] = new YYARec(-42,206);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-39,102);yygc++; 
					yyg[yygc] = new YYARec(-36,103);yygc++; 
					yyg[yygc] = new YYARec(-35,104);yygc++; 
					yyg[yygc] = new YYARec(-13,105);yygc++; 
					yyg[yygc] = new YYARec(-41,143);yygc++; 
					yyg[yygc] = new YYARec(-37,144);yygc++; 
					yyg[yygc] = new YYARec(-41,143);yygc++; 
					yyg[yygc] = new YYARec(-37,144);yygc++; 
					yyg[yygc] = new YYARec(-43,60);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-31,61);yygc++; 
					yyg[yygc] = new YYARec(-30,62);yygc++; 
					yyg[yygc] = new YYARec(-29,63);yygc++; 
					yyg[yygc] = new YYARec(-28,64);yygc++; 
					yyg[yygc] = new YYARec(-27,65);yygc++; 
					yyg[yygc] = new YYARec(-26,66);yygc++; 
					yyg[yygc] = new YYARec(-25,67);yygc++; 
					yyg[yygc] = new YYARec(-24,68);yygc++; 
					yyg[yygc] = new YYARec(-23,69);yygc++; 
					yyg[yygc] = new YYARec(-22,70);yygc++; 
					yyg[yygc] = new YYARec(-21,71);yygc++; 
					yyg[yygc] = new YYARec(-20,72);yygc++; 
					yyg[yygc] = new YYARec(-19,209);yygc++; 
					yyg[yygc] = new YYARec(-6,74);yygc++; 
					yyg[yygc] = new YYARec(-43,60);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-31,61);yygc++; 
					yyg[yygc] = new YYARec(-30,62);yygc++; 
					yyg[yygc] = new YYARec(-29,63);yygc++; 
					yyg[yygc] = new YYARec(-28,64);yygc++; 
					yyg[yygc] = new YYARec(-27,65);yygc++; 
					yyg[yygc] = new YYARec(-26,66);yygc++; 
					yyg[yygc] = new YYARec(-25,67);yygc++; 
					yyg[yygc] = new YYARec(-24,68);yygc++; 
					yyg[yygc] = new YYARec(-23,69);yygc++; 
					yyg[yygc] = new YYARec(-22,70);yygc++; 
					yyg[yygc] = new YYARec(-21,71);yygc++; 
					yyg[yygc] = new YYARec(-20,72);yygc++; 
					yyg[yygc] = new YYARec(-19,210);yygc++; 
					yyg[yygc] = new YYARec(-6,74);yygc++; 
					yyg[yygc] = new YYARec(-43,60);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-31,61);yygc++; 
					yyg[yygc] = new YYARec(-30,62);yygc++; 
					yyg[yygc] = new YYARec(-29,63);yygc++; 
					yyg[yygc] = new YYARec(-28,64);yygc++; 
					yyg[yygc] = new YYARec(-27,65);yygc++; 
					yyg[yygc] = new YYARec(-26,66);yygc++; 
					yyg[yygc] = new YYARec(-25,67);yygc++; 
					yyg[yygc] = new YYARec(-24,68);yygc++; 
					yyg[yygc] = new YYARec(-23,69);yygc++; 
					yyg[yygc] = new YYARec(-22,70);yygc++; 
					yyg[yygc] = new YYARec(-21,71);yygc++; 
					yyg[yygc] = new YYARec(-20,72);yygc++; 
					yyg[yygc] = new YYARec(-19,212);yygc++; 
					yyg[yygc] = new YYARec(-6,74);yygc++; 
					yyg[yygc] = new YYARec(-45,131);yygc++; 
					yyg[yygc] = new YYARec(-41,29);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-39,30);yygc++; 
					yyg[yygc] = new YYARec(-18,216);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-6,87);yygc++; 
					yyg[yygc] = new YYARec(-45,173);yygc++; 
					yyg[yygc] = new YYARec(-43,60);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-31,61);yygc++; 
					yyg[yygc] = new YYARec(-30,62);yygc++; 
					yyg[yygc] = new YYARec(-29,63);yygc++; 
					yyg[yygc] = new YYARec(-28,64);yygc++; 
					yyg[yygc] = new YYARec(-27,65);yygc++; 
					yyg[yygc] = new YYARec(-26,66);yygc++; 
					yyg[yygc] = new YYARec(-25,67);yygc++; 
					yyg[yygc] = new YYARec(-24,68);yygc++; 
					yyg[yygc] = new YYARec(-23,69);yygc++; 
					yyg[yygc] = new YYARec(-22,70);yygc++; 
					yyg[yygc] = new YYARec(-21,71);yygc++; 
					yyg[yygc] = new YYARec(-20,72);yygc++; 
					yyg[yygc] = new YYARec(-19,220);yygc++; 
					yyg[yygc] = new YYARec(-6,74);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = -119;  
					yyd[2] = -12;  
					yyd[3] = -11;  
					yyd[4] = -10;  
					yyd[5] = -9;  
					yyd[6] = 0;  
					yyd[7] = -4;  
					yyd[8] = -3;  
					yyd[9] = 0;  
					yyd[10] = 0;  
					yyd[11] = 0;  
					yyd[12] = 0;  
					yyd[13] = 0;  
					yyd[14] = -81;  
					yyd[15] = -82;  
					yyd[16] = -83;  
					yyd[17] = -84;  
					yyd[18] = -85;  
					yyd[19] = -86;  
					yyd[20] = -87;  
					yyd[21] = -88;  
					yyd[22] = -89;  
					yyd[23] = -90;  
					yyd[24] = -91;  
					yyd[25] = -92;  
					yyd[26] = -93;  
					yyd[27] = -94;  
					yyd[28] = -118;  
					yyd[29] = -120;  
					yyd[30] = 0;  
					yyd[31] = -20;  
					yyd[32] = -17;  
					yyd[33] = 0;  
					yyd[34] = -19;  
					yyd[35] = -18;  
					yyd[36] = 0;  
					yyd[37] = -74;  
					yyd[38] = -75;  
					yyd[39] = -125;  
					yyd[40] = -126;  
					yyd[41] = -2;  
					yyd[42] = 0;  
					yyd[43] = 0;  
					yyd[44] = 0;  
					yyd[45] = -122;  
					yyd[46] = -16;  
					yyd[47] = 0;  
					yyd[48] = 0;  
					yyd[49] = -25;  
					yyd[50] = 0;  
					yyd[51] = 0;  
					yyd[52] = 0;  
					yyd[53] = 0;  
					yyd[54] = 0;  
					yyd[55] = -15;  
					yyd[56] = 0;  
					yyd[57] = -22;  
					yyd[58] = 0;  
					yyd[59] = 0;  
					yyd[60] = 0;  
					yyd[61] = -42;  
					yyd[62] = -41;  
					yyd[63] = -40;  
					yyd[64] = -38;  
					yyd[65] = -37;  
					yyd[66] = -36;  
					yyd[67] = -35;  
					yyd[68] = -34;  
					yyd[69] = -33;  
					yyd[70] = -32;  
					yyd[71] = 0;  
					yyd[72] = 0;  
					yyd[73] = 0;  
					yyd[74] = 0;  
					yyd[75] = 0;  
					yyd[76] = 0;  
					yyd[77] = 0;  
					yyd[78] = -116;  
					yyd[79] = -5;  
					yyd[80] = 0;  
					yyd[81] = -6;  
					yyd[82] = 0;  
					yyd[83] = 0;  
					yyd[84] = 0;  
					yyd[85] = 0;  
					yyd[86] = -124;  
					yyd[87] = -123;  
					yyd[88] = -21;  
					yyd[89] = 0;  
					yyd[90] = -30;  
					yyd[91] = 0;  
					yyd[92] = -27;  
					yyd[93] = 0;  
					yyd[94] = -44;  
					yyd[95] = 0;  
					yyd[96] = 0;  
					yyd[97] = -49;  
					yyd[98] = 0;  
					yyd[99] = 0;  
					yyd[100] = 0;  
					yyd[101] = 0;  
					yyd[102] = 0;  
					yyd[103] = -60;  
					yyd[104] = 0;  
					yyd[105] = -66;  
					yyd[106] = 0;  
					yyd[107] = 0;  
					yyd[108] = 0;  
					yyd[109] = 0;  
					yyd[110] = 0;  
					yyd[111] = 0;  
					yyd[112] = -8;  
					yyd[113] = -14;  
					yyd[114] = -13;  
					yyd[115] = 0;  
					yyd[116] = 0;  
					yyd[117] = -29;  
					yyd[118] = -47;  
					yyd[119] = -48;  
					yyd[120] = 0;  
					yyd[121] = 0;  
					yyd[122] = 0;  
					yyd[123] = 0;  
					yyd[124] = -76;  
					yyd[125] = -77;  
					yyd[126] = -78;  
					yyd[127] = -79;  
					yyd[128] = -80;  
					yyd[129] = -43;  
					yyd[130] = -28;  
					yyd[131] = 0;  
					yyd[132] = 0;  
					yyd[133] = -108;  
					yyd[134] = -109;  
					yyd[135] = -110;  
					yyd[136] = -111;  
					yyd[137] = -112;  
					yyd[138] = -113;  
					yyd[139] = -114;  
					yyd[140] = -115;  
					yyd[141] = 0;  
					yyd[142] = -65;  
					yyd[143] = -73;  
					yyd[144] = 0;  
					yyd[145] = -67;  
					yyd[146] = -68;  
					yyd[147] = -69;  
					yyd[148] = -70;  
					yyd[149] = -71;  
					yyd[150] = -72;  
					yyd[151] = 0;  
					yyd[152] = 0;  
					yyd[153] = 0;  
					yyd[154] = 0;  
					yyd[155] = 0;  
					yyd[156] = 0;  
					yyd[157] = 0;  
					yyd[158] = 0;  
					yyd[159] = 0;  
					yyd[160] = 0;  
					yyd[161] = 0;  
					yyd[162] = -45;  
					yyd[163] = 0;  
					yyd[164] = -52;  
					yyd[165] = -53;  
					yyd[166] = -54;  
					yyd[167] = -55;  
					yyd[168] = 0;  
					yyd[169] = 0;  
					yyd[170] = -103;  
					yyd[171] = 0;  
					yyd[172] = 0;  
					yyd[173] = 0;  
					yyd[174] = 0;  
					yyd[175] = -61;  
					yyd[176] = -63;  
					yyd[177] = 0;  
					yyd[178] = -101;  
					yyd[179] = 0;  
					yyd[180] = -59;  
					yyd[181] = 0;  
					yyd[182] = 0;  
					yyd[183] = 0;  
					yyd[184] = 0;  
					yyd[185] = 0;  
					yyd[186] = -7;  
					yyd[187] = 0;  
					yyd[188] = 0;  
					yyd[189] = 0;  
					yyd[190] = 0;  
					yyd[191] = 0;  
					yyd[192] = -128;  
					yyd[193] = 0;  
					yyd[194] = 0;  
					yyd[195] = 0;  
					yyd[196] = 0;  
					yyd[197] = 0;  
					yyd[198] = -99;  
					yyd[199] = 0;  
					yyd[200] = -100;  
					yyd[201] = 0;  
					yyd[202] = 0;  
					yyd[203] = 0;  
					yyd[204] = -57;  
					yyd[205] = -104;  
					yyd[206] = 0;  
					yyd[207] = -64;  
					yyd[208] = -62;  
					yyd[209] = 0;  
					yyd[210] = 0;  
					yyd[211] = 0;  
					yyd[212] = 0;  
					yyd[213] = 0;  
					yyd[214] = 0;  
					yyd[215] = -98;  
					yyd[216] = -23;  
					yyd[217] = -96;  
					yyd[218] = 0;  
					yyd[219] = 0;  
					yyd[220] = 0;  
					yyd[221] = -95; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 19;  
					yyal[2] = 19;  
					yyal[3] = 19;  
					yyal[4] = 19;  
					yyal[5] = 19;  
					yyal[6] = 19;  
					yyal[7] = 39;  
					yyal[8] = 39;  
					yyal[9] = 39;  
					yyal[10] = 58;  
					yyal[11] = 59;  
					yyal[12] = 74;  
					yyal[13] = 89;  
					yyal[14] = 104;  
					yyal[15] = 104;  
					yyal[16] = 104;  
					yyal[17] = 104;  
					yyal[18] = 104;  
					yyal[19] = 104;  
					yyal[20] = 104;  
					yyal[21] = 104;  
					yyal[22] = 104;  
					yyal[23] = 104;  
					yyal[24] = 104;  
					yyal[25] = 104;  
					yyal[26] = 104;  
					yyal[27] = 104;  
					yyal[28] = 104;  
					yyal[29] = 104;  
					yyal[30] = 104;  
					yyal[31] = 105;  
					yyal[32] = 105;  
					yyal[33] = 105;  
					yyal[34] = 106;  
					yyal[35] = 106;  
					yyal[36] = 106;  
					yyal[37] = 112;  
					yyal[38] = 112;  
					yyal[39] = 112;  
					yyal[40] = 112;  
					yyal[41] = 112;  
					yyal[42] = 112;  
					yyal[43] = 113;  
					yyal[44] = 114;  
					yyal[45] = 116;  
					yyal[46] = 116;  
					yyal[47] = 116;  
					yyal[48] = 117;  
					yyal[49] = 119;  
					yyal[50] = 119;  
					yyal[51] = 121;  
					yyal[52] = 140;  
					yyal[53] = 155;  
					yyal[54] = 171;  
					yyal[55] = 187;  
					yyal[56] = 187;  
					yyal[57] = 205;  
					yyal[58] = 205;  
					yyal[59] = 223;  
					yyal[60] = 224;  
					yyal[61] = 225;  
					yyal[62] = 225;  
					yyal[63] = 225;  
					yyal[64] = 225;  
					yyal[65] = 225;  
					yyal[66] = 225;  
					yyal[67] = 225;  
					yyal[68] = 225;  
					yyal[69] = 225;  
					yyal[70] = 225;  
					yyal[71] = 225;  
					yyal[72] = 244;  
					yyal[73] = 245;  
					yyal[74] = 246;  
					yyal[75] = 269;  
					yyal[76] = 288;  
					yyal[77] = 289;  
					yyal[78] = 290;  
					yyal[79] = 290;  
					yyal[80] = 290;  
					yyal[81] = 292;  
					yyal[82] = 292;  
					yyal[83] = 293;  
					yyal[84] = 294;  
					yyal[85] = 335;  
					yyal[86] = 336;  
					yyal[87] = 336;  
					yyal[88] = 336;  
					yyal[89] = 336;  
					yyal[90] = 355;  
					yyal[91] = 355;  
					yyal[92] = 374;  
					yyal[93] = 374;  
					yyal[94] = 394;  
					yyal[95] = 394;  
					yyal[96] = 396;  
					yyal[97] = 398;  
					yyal[98] = 398;  
					yyal[99] = 423;  
					yyal[100] = 441;  
					yyal[101] = 460;  
					yyal[102] = 469;  
					yyal[103] = 485;  
					yyal[104] = 485;  
					yyal[105] = 502;  
					yyal[106] = 502;  
					yyal[107] = 521;  
					yyal[108] = 522;  
					yyal[109] = 541;  
					yyal[110] = 542;  
					yyal[111] = 561;  
					yyal[112] = 577;  
					yyal[113] = 577;  
					yyal[114] = 577;  
					yyal[115] = 577;  
					yyal[116] = 595;  
					yyal[117] = 596;  
					yyal[118] = 596;  
					yyal[119] = 596;  
					yyal[120] = 596;  
					yyal[121] = 614;  
					yyal[122] = 633;  
					yyal[123] = 652;  
					yyal[124] = 671;  
					yyal[125] = 671;  
					yyal[126] = 671;  
					yyal[127] = 671;  
					yyal[128] = 671;  
					yyal[129] = 671;  
					yyal[130] = 671;  
					yyal[131] = 671;  
					yyal[132] = 690;  
					yyal[133] = 698;  
					yyal[134] = 698;  
					yyal[135] = 698;  
					yyal[136] = 698;  
					yyal[137] = 698;  
					yyal[138] = 698;  
					yyal[139] = 698;  
					yyal[140] = 698;  
					yyal[141] = 698;  
					yyal[142] = 718;  
					yyal[143] = 718;  
					yyal[144] = 718;  
					yyal[145] = 737;  
					yyal[146] = 737;  
					yyal[147] = 737;  
					yyal[148] = 737;  
					yyal[149] = 737;  
					yyal[150] = 737;  
					yyal[151] = 737;  
					yyal[152] = 738;  
					yyal[153] = 746;  
					yyal[154] = 763;  
					yyal[155] = 782;  
					yyal[156] = 801;  
					yyal[157] = 810;  
					yyal[158] = 829;  
					yyal[159] = 838;  
					yyal[160] = 839;  
					yyal[161] = 841;  
					yyal[162] = 842;  
					yyal[163] = 842;  
					yyal[164] = 844;  
					yyal[165] = 844;  
					yyal[166] = 844;  
					yyal[167] = 844;  
					yyal[168] = 844;  
					yyal[169] = 853;  
					yyal[170] = 872;  
					yyal[171] = 872;  
					yyal[172] = 889;  
					yyal[173] = 908;  
					yyal[174] = 909;  
					yyal[175] = 928;  
					yyal[176] = 928;  
					yyal[177] = 928;  
					yyal[178] = 947;  
					yyal[179] = 947;  
					yyal[180] = 966;  
					yyal[181] = 966;  
					yyal[182] = 967;  
					yyal[183] = 968;  
					yyal[184] = 977;  
					yyal[185] = 978;  
					yyal[186] = 987;  
					yyal[187] = 987;  
					yyal[188] = 1005;  
					yyal[189] = 1006;  
					yyal[190] = 1024;  
					yyal[191] = 1033;  
					yyal[192] = 1034;  
					yyal[193] = 1034;  
					yyal[194] = 1053;  
					yyal[195] = 1062;  
					yyal[196] = 1071;  
					yyal[197] = 1080;  
					yyal[198] = 1089;  
					yyal[199] = 1089;  
					yyal[200] = 1108;  
					yyal[201] = 1108;  
					yyal[202] = 1127;  
					yyal[203] = 1128;  
					yyal[204] = 1147;  
					yyal[205] = 1147;  
					yyal[206] = 1147;  
					yyal[207] = 1156;  
					yyal[208] = 1156;  
					yyal[209] = 1156;  
					yyal[210] = 1157;  
					yyal[211] = 1158;  
					yyal[212] = 1176;  
					yyal[213] = 1177;  
					yyal[214] = 1186;  
					yyal[215] = 1206;  
					yyal[216] = 1206;  
					yyal[217] = 1206;  
					yyal[218] = 1206;  
					yyal[219] = 1207;  
					yyal[220] = 1226;  
					yyal[221] = 1227; 

					yyah = new int[yynstates];
					yyah[0] = 18;  
					yyah[1] = 18;  
					yyah[2] = 18;  
					yyah[3] = 18;  
					yyah[4] = 18;  
					yyah[5] = 18;  
					yyah[6] = 38;  
					yyah[7] = 38;  
					yyah[8] = 38;  
					yyah[9] = 57;  
					yyah[10] = 58;  
					yyah[11] = 73;  
					yyah[12] = 88;  
					yyah[13] = 103;  
					yyah[14] = 103;  
					yyah[15] = 103;  
					yyah[16] = 103;  
					yyah[17] = 103;  
					yyah[18] = 103;  
					yyah[19] = 103;  
					yyah[20] = 103;  
					yyah[21] = 103;  
					yyah[22] = 103;  
					yyah[23] = 103;  
					yyah[24] = 103;  
					yyah[25] = 103;  
					yyah[26] = 103;  
					yyah[27] = 103;  
					yyah[28] = 103;  
					yyah[29] = 103;  
					yyah[30] = 104;  
					yyah[31] = 104;  
					yyah[32] = 104;  
					yyah[33] = 105;  
					yyah[34] = 105;  
					yyah[35] = 105;  
					yyah[36] = 111;  
					yyah[37] = 111;  
					yyah[38] = 111;  
					yyah[39] = 111;  
					yyah[40] = 111;  
					yyah[41] = 111;  
					yyah[42] = 112;  
					yyah[43] = 113;  
					yyah[44] = 115;  
					yyah[45] = 115;  
					yyah[46] = 115;  
					yyah[47] = 116;  
					yyah[48] = 118;  
					yyah[49] = 118;  
					yyah[50] = 120;  
					yyah[51] = 139;  
					yyah[52] = 154;  
					yyah[53] = 170;  
					yyah[54] = 186;  
					yyah[55] = 186;  
					yyah[56] = 204;  
					yyah[57] = 204;  
					yyah[58] = 222;  
					yyah[59] = 223;  
					yyah[60] = 224;  
					yyah[61] = 224;  
					yyah[62] = 224;  
					yyah[63] = 224;  
					yyah[64] = 224;  
					yyah[65] = 224;  
					yyah[66] = 224;  
					yyah[67] = 224;  
					yyah[68] = 224;  
					yyah[69] = 224;  
					yyah[70] = 224;  
					yyah[71] = 243;  
					yyah[72] = 244;  
					yyah[73] = 245;  
					yyah[74] = 268;  
					yyah[75] = 287;  
					yyah[76] = 288;  
					yyah[77] = 289;  
					yyah[78] = 289;  
					yyah[79] = 289;  
					yyah[80] = 291;  
					yyah[81] = 291;  
					yyah[82] = 292;  
					yyah[83] = 293;  
					yyah[84] = 334;  
					yyah[85] = 335;  
					yyah[86] = 335;  
					yyah[87] = 335;  
					yyah[88] = 335;  
					yyah[89] = 354;  
					yyah[90] = 354;  
					yyah[91] = 373;  
					yyah[92] = 373;  
					yyah[93] = 393;  
					yyah[94] = 393;  
					yyah[95] = 395;  
					yyah[96] = 397;  
					yyah[97] = 397;  
					yyah[98] = 422;  
					yyah[99] = 440;  
					yyah[100] = 459;  
					yyah[101] = 468;  
					yyah[102] = 484;  
					yyah[103] = 484;  
					yyah[104] = 501;  
					yyah[105] = 501;  
					yyah[106] = 520;  
					yyah[107] = 521;  
					yyah[108] = 540;  
					yyah[109] = 541;  
					yyah[110] = 560;  
					yyah[111] = 576;  
					yyah[112] = 576;  
					yyah[113] = 576;  
					yyah[114] = 576;  
					yyah[115] = 594;  
					yyah[116] = 595;  
					yyah[117] = 595;  
					yyah[118] = 595;  
					yyah[119] = 595;  
					yyah[120] = 613;  
					yyah[121] = 632;  
					yyah[122] = 651;  
					yyah[123] = 670;  
					yyah[124] = 670;  
					yyah[125] = 670;  
					yyah[126] = 670;  
					yyah[127] = 670;  
					yyah[128] = 670;  
					yyah[129] = 670;  
					yyah[130] = 670;  
					yyah[131] = 689;  
					yyah[132] = 697;  
					yyah[133] = 697;  
					yyah[134] = 697;  
					yyah[135] = 697;  
					yyah[136] = 697;  
					yyah[137] = 697;  
					yyah[138] = 697;  
					yyah[139] = 697;  
					yyah[140] = 697;  
					yyah[141] = 717;  
					yyah[142] = 717;  
					yyah[143] = 717;  
					yyah[144] = 736;  
					yyah[145] = 736;  
					yyah[146] = 736;  
					yyah[147] = 736;  
					yyah[148] = 736;  
					yyah[149] = 736;  
					yyah[150] = 736;  
					yyah[151] = 737;  
					yyah[152] = 745;  
					yyah[153] = 762;  
					yyah[154] = 781;  
					yyah[155] = 800;  
					yyah[156] = 809;  
					yyah[157] = 828;  
					yyah[158] = 837;  
					yyah[159] = 838;  
					yyah[160] = 840;  
					yyah[161] = 841;  
					yyah[162] = 841;  
					yyah[163] = 843;  
					yyah[164] = 843;  
					yyah[165] = 843;  
					yyah[166] = 843;  
					yyah[167] = 843;  
					yyah[168] = 852;  
					yyah[169] = 871;  
					yyah[170] = 871;  
					yyah[171] = 888;  
					yyah[172] = 907;  
					yyah[173] = 908;  
					yyah[174] = 927;  
					yyah[175] = 927;  
					yyah[176] = 927;  
					yyah[177] = 946;  
					yyah[178] = 946;  
					yyah[179] = 965;  
					yyah[180] = 965;  
					yyah[181] = 966;  
					yyah[182] = 967;  
					yyah[183] = 976;  
					yyah[184] = 977;  
					yyah[185] = 986;  
					yyah[186] = 986;  
					yyah[187] = 1004;  
					yyah[188] = 1005;  
					yyah[189] = 1023;  
					yyah[190] = 1032;  
					yyah[191] = 1033;  
					yyah[192] = 1033;  
					yyah[193] = 1052;  
					yyah[194] = 1061;  
					yyah[195] = 1070;  
					yyah[196] = 1079;  
					yyah[197] = 1088;  
					yyah[198] = 1088;  
					yyah[199] = 1107;  
					yyah[200] = 1107;  
					yyah[201] = 1126;  
					yyah[202] = 1127;  
					yyah[203] = 1146;  
					yyah[204] = 1146;  
					yyah[205] = 1146;  
					yyah[206] = 1155;  
					yyah[207] = 1155;  
					yyah[208] = 1155;  
					yyah[209] = 1156;  
					yyah[210] = 1157;  
					yyah[211] = 1175;  
					yyah[212] = 1176;  
					yyah[213] = 1185;  
					yyah[214] = 1205;  
					yyah[215] = 1205;  
					yyah[216] = 1205;  
					yyah[217] = 1205;  
					yyah[218] = 1206;  
					yyah[219] = 1225;  
					yyah[220] = 1226;  
					yyah[221] = 1226; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 11;  
					yygl[2] = 11;  
					yygl[3] = 11;  
					yygl[4] = 11;  
					yygl[5] = 11;  
					yygl[6] = 11;  
					yygl[7] = 20;  
					yygl[8] = 20;  
					yygl[9] = 20;  
					yygl[10] = 30;  
					yygl[11] = 30;  
					yygl[12] = 32;  
					yygl[13] = 34;  
					yygl[14] = 36;  
					yygl[15] = 36;  
					yygl[16] = 36;  
					yygl[17] = 36;  
					yygl[18] = 36;  
					yygl[19] = 36;  
					yygl[20] = 36;  
					yygl[21] = 36;  
					yygl[22] = 36;  
					yygl[23] = 36;  
					yygl[24] = 36;  
					yygl[25] = 36;  
					yygl[26] = 36;  
					yygl[27] = 36;  
					yygl[28] = 36;  
					yygl[29] = 36;  
					yygl[30] = 36;  
					yygl[31] = 36;  
					yygl[32] = 36;  
					yygl[33] = 36;  
					yygl[34] = 36;  
					yygl[35] = 36;  
					yygl[36] = 36;  
					yygl[37] = 39;  
					yygl[38] = 39;  
					yygl[39] = 39;  
					yygl[40] = 39;  
					yygl[41] = 39;  
					yygl[42] = 39;  
					yygl[43] = 39;  
					yygl[44] = 39;  
					yygl[45] = 39;  
					yygl[46] = 39;  
					yygl[47] = 39;  
					yygl[48] = 39;  
					yygl[49] = 39;  
					yygl[50] = 39;  
					yygl[51] = 42;  
					yygl[52] = 58;  
					yygl[53] = 60;  
					yygl[54] = 68;  
					yygl[55] = 76;  
					yygl[56] = 76;  
					yygl[57] = 82;  
					yygl[58] = 82;  
					yygl[59] = 88;  
					yygl[60] = 88;  
					yygl[61] = 88;  
					yygl[62] = 88;  
					yygl[63] = 88;  
					yygl[64] = 88;  
					yygl[65] = 88;  
					yygl[66] = 88;  
					yygl[67] = 88;  
					yygl[68] = 88;  
					yygl[69] = 88;  
					yygl[70] = 88;  
					yygl[71] = 88;  
					yygl[72] = 104;  
					yygl[73] = 104;  
					yygl[74] = 104;  
					yygl[75] = 114;  
					yygl[76] = 120;  
					yygl[77] = 121;  
					yygl[78] = 122;  
					yygl[79] = 122;  
					yygl[80] = 122;  
					yygl[81] = 122;  
					yygl[82] = 122;  
					yygl[83] = 122;  
					yygl[84] = 122;  
					yygl[85] = 122;  
					yygl[86] = 122;  
					yygl[87] = 122;  
					yygl[88] = 122;  
					yygl[89] = 122;  
					yygl[90] = 138;  
					yygl[91] = 138;  
					yygl[92] = 154;  
					yygl[93] = 154;  
					yygl[94] = 162;  
					yygl[95] = 162;  
					yygl[96] = 162;  
					yygl[97] = 162;  
					yygl[98] = 162;  
					yygl[99] = 163;  
					yygl[100] = 171;  
					yygl[101] = 187;  
					yygl[102] = 188;  
					yygl[103] = 191;  
					yygl[104] = 191;  
					yygl[105] = 193;  
					yygl[106] = 193;  
					yygl[107] = 200;  
					yygl[108] = 200;  
					yygl[109] = 206;  
					yygl[110] = 206;  
					yygl[111] = 212;  
					yygl[112] = 219;  
					yygl[113] = 219;  
					yygl[114] = 219;  
					yygl[115] = 219;  
					yygl[116] = 225;  
					yygl[117] = 225;  
					yygl[118] = 225;  
					yygl[119] = 225;  
					yygl[120] = 225;  
					yygl[121] = 233;  
					yygl[122] = 240;  
					yygl[123] = 247;  
					yygl[124] = 252;  
					yygl[125] = 252;  
					yygl[126] = 252;  
					yygl[127] = 252;  
					yygl[128] = 252;  
					yygl[129] = 252;  
					yygl[130] = 252;  
					yygl[131] = 252;  
					yygl[132] = 258;  
					yygl[133] = 259;  
					yygl[134] = 259;  
					yygl[135] = 259;  
					yygl[136] = 259;  
					yygl[137] = 259;  
					yygl[138] = 259;  
					yygl[139] = 259;  
					yygl[140] = 259;  
					yygl[141] = 259;  
					yygl[142] = 259;  
					yygl[143] = 259;  
					yygl[144] = 259;  
					yygl[145] = 264;  
					yygl[146] = 264;  
					yygl[147] = 264;  
					yygl[148] = 264;  
					yygl[149] = 264;  
					yygl[150] = 264;  
					yygl[151] = 264;  
					yygl[152] = 264;  
					yygl[153] = 265;  
					yygl[154] = 267;  
					yygl[155] = 274;  
					yygl[156] = 290;  
					yygl[157] = 291;  
					yygl[158] = 307;  
					yygl[159] = 308;  
					yygl[160] = 308;  
					yygl[161] = 308;  
					yygl[162] = 308;  
					yygl[163] = 308;  
					yygl[164] = 308;  
					yygl[165] = 308;  
					yygl[166] = 308;  
					yygl[167] = 308;  
					yygl[168] = 308;  
					yygl[169] = 310;  
					yygl[170] = 315;  
					yygl[171] = 315;  
					yygl[172] = 317;  
					yygl[173] = 324;  
					yygl[174] = 325;  
					yygl[175] = 330;  
					yygl[176] = 330;  
					yygl[177] = 330;  
					yygl[178] = 335;  
					yygl[179] = 335;  
					yygl[180] = 341;  
					yygl[181] = 341;  
					yygl[182] = 341;  
					yygl[183] = 341;  
					yygl[184] = 342;  
					yygl[185] = 342;  
					yygl[186] = 343;  
					yygl[187] = 343;  
					yygl[188] = 349;  
					yygl[189] = 349;  
					yygl[190] = 355;  
					yygl[191] = 357;  
					yygl[192] = 357;  
					yygl[193] = 357;  
					yygl[194] = 363;  
					yygl[195] = 365;  
					yygl[196] = 367;  
					yygl[197] = 367;  
					yygl[198] = 367;  
					yygl[199] = 367;  
					yygl[200] = 383;  
					yygl[201] = 383;  
					yygl[202] = 399;  
					yygl[203] = 399;  
					yygl[204] = 415;  
					yygl[205] = 415;  
					yygl[206] = 415;  
					yygl[207] = 416;  
					yygl[208] = 416;  
					yygl[209] = 416;  
					yygl[210] = 416;  
					yygl[211] = 416;  
					yygl[212] = 422;  
					yygl[213] = 422;  
					yygl[214] = 423;  
					yygl[215] = 423;  
					yygl[216] = 423;  
					yygl[217] = 423;  
					yygl[218] = 423;  
					yygl[219] = 423;  
					yygl[220] = 439;  
					yygl[221] = 439; 

					yygh = new int[yynstates];
					yygh[0] = 10;  
					yygh[1] = 10;  
					yygh[2] = 10;  
					yygh[3] = 10;  
					yygh[4] = 10;  
					yygh[5] = 10;  
					yygh[6] = 19;  
					yygh[7] = 19;  
					yygh[8] = 19;  
					yygh[9] = 29;  
					yygh[10] = 29;  
					yygh[11] = 31;  
					yygh[12] = 33;  
					yygh[13] = 35;  
					yygh[14] = 35;  
					yygh[15] = 35;  
					yygh[16] = 35;  
					yygh[17] = 35;  
					yygh[18] = 35;  
					yygh[19] = 35;  
					yygh[20] = 35;  
					yygh[21] = 35;  
					yygh[22] = 35;  
					yygh[23] = 35;  
					yygh[24] = 35;  
					yygh[25] = 35;  
					yygh[26] = 35;  
					yygh[27] = 35;  
					yygh[28] = 35;  
					yygh[29] = 35;  
					yygh[30] = 35;  
					yygh[31] = 35;  
					yygh[32] = 35;  
					yygh[33] = 35;  
					yygh[34] = 35;  
					yygh[35] = 35;  
					yygh[36] = 38;  
					yygh[37] = 38;  
					yygh[38] = 38;  
					yygh[39] = 38;  
					yygh[40] = 38;  
					yygh[41] = 38;  
					yygh[42] = 38;  
					yygh[43] = 38;  
					yygh[44] = 38;  
					yygh[45] = 38;  
					yygh[46] = 38;  
					yygh[47] = 38;  
					yygh[48] = 38;  
					yygh[49] = 38;  
					yygh[50] = 41;  
					yygh[51] = 57;  
					yygh[52] = 59;  
					yygh[53] = 67;  
					yygh[54] = 75;  
					yygh[55] = 75;  
					yygh[56] = 81;  
					yygh[57] = 81;  
					yygh[58] = 87;  
					yygh[59] = 87;  
					yygh[60] = 87;  
					yygh[61] = 87;  
					yygh[62] = 87;  
					yygh[63] = 87;  
					yygh[64] = 87;  
					yygh[65] = 87;  
					yygh[66] = 87;  
					yygh[67] = 87;  
					yygh[68] = 87;  
					yygh[69] = 87;  
					yygh[70] = 87;  
					yygh[71] = 103;  
					yygh[72] = 103;  
					yygh[73] = 103;  
					yygh[74] = 113;  
					yygh[75] = 119;  
					yygh[76] = 120;  
					yygh[77] = 121;  
					yygh[78] = 121;  
					yygh[79] = 121;  
					yygh[80] = 121;  
					yygh[81] = 121;  
					yygh[82] = 121;  
					yygh[83] = 121;  
					yygh[84] = 121;  
					yygh[85] = 121;  
					yygh[86] = 121;  
					yygh[87] = 121;  
					yygh[88] = 121;  
					yygh[89] = 137;  
					yygh[90] = 137;  
					yygh[91] = 153;  
					yygh[92] = 153;  
					yygh[93] = 161;  
					yygh[94] = 161;  
					yygh[95] = 161;  
					yygh[96] = 161;  
					yygh[97] = 161;  
					yygh[98] = 162;  
					yygh[99] = 170;  
					yygh[100] = 186;  
					yygh[101] = 187;  
					yygh[102] = 190;  
					yygh[103] = 190;  
					yygh[104] = 192;  
					yygh[105] = 192;  
					yygh[106] = 199;  
					yygh[107] = 199;  
					yygh[108] = 205;  
					yygh[109] = 205;  
					yygh[110] = 211;  
					yygh[111] = 218;  
					yygh[112] = 218;  
					yygh[113] = 218;  
					yygh[114] = 218;  
					yygh[115] = 224;  
					yygh[116] = 224;  
					yygh[117] = 224;  
					yygh[118] = 224;  
					yygh[119] = 224;  
					yygh[120] = 232;  
					yygh[121] = 239;  
					yygh[122] = 246;  
					yygh[123] = 251;  
					yygh[124] = 251;  
					yygh[125] = 251;  
					yygh[126] = 251;  
					yygh[127] = 251;  
					yygh[128] = 251;  
					yygh[129] = 251;  
					yygh[130] = 251;  
					yygh[131] = 257;  
					yygh[132] = 258;  
					yygh[133] = 258;  
					yygh[134] = 258;  
					yygh[135] = 258;  
					yygh[136] = 258;  
					yygh[137] = 258;  
					yygh[138] = 258;  
					yygh[139] = 258;  
					yygh[140] = 258;  
					yygh[141] = 258;  
					yygh[142] = 258;  
					yygh[143] = 258;  
					yygh[144] = 263;  
					yygh[145] = 263;  
					yygh[146] = 263;  
					yygh[147] = 263;  
					yygh[148] = 263;  
					yygh[149] = 263;  
					yygh[150] = 263;  
					yygh[151] = 263;  
					yygh[152] = 264;  
					yygh[153] = 266;  
					yygh[154] = 273;  
					yygh[155] = 289;  
					yygh[156] = 290;  
					yygh[157] = 306;  
					yygh[158] = 307;  
					yygh[159] = 307;  
					yygh[160] = 307;  
					yygh[161] = 307;  
					yygh[162] = 307;  
					yygh[163] = 307;  
					yygh[164] = 307;  
					yygh[165] = 307;  
					yygh[166] = 307;  
					yygh[167] = 307;  
					yygh[168] = 309;  
					yygh[169] = 314;  
					yygh[170] = 314;  
					yygh[171] = 316;  
					yygh[172] = 323;  
					yygh[173] = 324;  
					yygh[174] = 329;  
					yygh[175] = 329;  
					yygh[176] = 329;  
					yygh[177] = 334;  
					yygh[178] = 334;  
					yygh[179] = 340;  
					yygh[180] = 340;  
					yygh[181] = 340;  
					yygh[182] = 340;  
					yygh[183] = 341;  
					yygh[184] = 341;  
					yygh[185] = 342;  
					yygh[186] = 342;  
					yygh[187] = 348;  
					yygh[188] = 348;  
					yygh[189] = 354;  
					yygh[190] = 356;  
					yygh[191] = 356;  
					yygh[192] = 356;  
					yygh[193] = 362;  
					yygh[194] = 364;  
					yygh[195] = 366;  
					yygh[196] = 366;  
					yygh[197] = 366;  
					yygh[198] = 366;  
					yygh[199] = 382;  
					yygh[200] = 382;  
					yygh[201] = 398;  
					yygh[202] = 398;  
					yygh[203] = 414;  
					yygh[204] = 414;  
					yygh[205] = 414;  
					yygh[206] = 415;  
					yygh[207] = 415;  
					yygh[208] = 415;  
					yygh[209] = 415;  
					yygh[210] = 415;  
					yygh[211] = 421;  
					yygh[212] = 421;  
					yygh[213] = 422;  
					yygh[214] = 422;  
					yygh[215] = 422;  
					yygh[216] = 422;  
					yygh[217] = 422;  
					yygh[218] = 422;  
					yygh[219] = 438;  
					yygh[220] = 438;  
					yygh[221] = 438; 

					yyr[yyrc] = new YYRRec(1,-2);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-2);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(9,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(11,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(8,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-43);yyrc++;
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
                        if (yysp>yymaxdepth)
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

			if (Regex.IsMatch(Rest,"^(;)")){
				Results.Add (t_Char59);
				ResultsV.Add(Regex.Match(Rest,"^(;)").Value);}

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

			if (Regex.IsMatch(Rest,"^(\\{)")){
				Results.Add (t_Char123);
				ResultsV.Add(Regex.Match(Rest,"^(\\{)").Value);}

			if (Regex.IsMatch(Rest,"^(\\})")){
				Results.Add (t_Char125);
				ResultsV.Add(Regex.Match(Rest,"^(\\})").Value);}

			if (Regex.IsMatch(Rest,"^(:)")){
				Results.Add (t_Char58);
				ResultsV.Add(Regex.Match(Rest,"^(:)").Value);}

			if (Regex.IsMatch(Rest,"^(\\()")){
				Results.Add (t_Char40);
				ResultsV.Add(Regex.Match(Rest,"^(\\()").Value);}

			if (Regex.IsMatch(Rest,"^(\\))")){
				Results.Add (t_Char41);
				ResultsV.Add(Regex.Match(Rest,"^(\\))").Value);}

			if (Regex.IsMatch(Rest,"^(%)")){
				Results.Add (t_Char37);
				ResultsV.Add(Regex.Match(Rest,"^(%)").Value);}

			if (Regex.IsMatch(Rest,"^(&)")){
				Results.Add (t_Char38);
				ResultsV.Add(Regex.Match(Rest,"^(&)").Value);}

			if (Regex.IsMatch(Rest,"^(\\*)")){
				Results.Add (t_Char42);
				ResultsV.Add(Regex.Match(Rest,"^(\\*)").Value);}

			if (Regex.IsMatch(Rest,"^(/)")){
				Results.Add (t_Char47);
				ResultsV.Add(Regex.Match(Rest,"^(/)").Value);}

			if (Regex.IsMatch(Rest,"^(\\^)")){
				Results.Add (t_Char94);
				ResultsV.Add(Regex.Match(Rest,"^(\\^)").Value);}

			if (Regex.IsMatch(Rest,"^(\\|)")){
				Results.Add (t_Char124);
				ResultsV.Add(Regex.Match(Rest,"^(\\|)").Value);}

			if (Regex.IsMatch(Rest,"^(\\+)")){
				Results.Add (t_Char43);
				ResultsV.Add(Regex.Match(Rest,"^(\\+)").Value);}

			if (Regex.IsMatch(Rest,"^(\\-)")){
				Results.Add (t_Char45);
				ResultsV.Add(Regex.Match(Rest,"^(\\-)").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)IF)")){
				Results.Add (t_IF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)IF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)ELSE)")){
				Results.Add (t_ELSE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)ELSE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)WHILE)")){
				Results.Add (t_WHILE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)WHILE)").Value);}

			if (Regex.IsMatch(Rest,"^(!=)")){
				Results.Add (t_Char33Char61);
				ResultsV.Add(Regex.Match(Rest,"^(!=)").Value);}

			if (Regex.IsMatch(Rest,"^(&&)")){
				Results.Add (t_Char38Char38);
				ResultsV.Add(Regex.Match(Rest,"^(&&)").Value);}

			if (Regex.IsMatch(Rest,"^(<)")){
				Results.Add (t_Char60);
				ResultsV.Add(Regex.Match(Rest,"^(<)").Value);}

			if (Regex.IsMatch(Rest,"^(<=)")){
				Results.Add (t_Char60Char61);
				ResultsV.Add(Regex.Match(Rest,"^(<=)").Value);}

			if (Regex.IsMatch(Rest,"^(==)")){
				Results.Add (t_Char61Char61);
				ResultsV.Add(Regex.Match(Rest,"^(==)").Value);}

			if (Regex.IsMatch(Rest,"^(>)")){
				Results.Add (t_Char62);
				ResultsV.Add(Regex.Match(Rest,"^(>)").Value);}

			if (Regex.IsMatch(Rest,"^(>=)")){
				Results.Add (t_Char62Char61);
				ResultsV.Add(Regex.Match(Rest,"^(>=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\|\\|)")){
				Results.Add (t_Char124Char124);
				ResultsV.Add(Regex.Match(Rest,"^(\\|\\|)").Value);}

			if (Regex.IsMatch(Rest,"^(\\.)")){
				Results.Add (t_Char46);
				ResultsV.Add(Regex.Match(Rest,"^(\\.)").Value);}

			if (Regex.IsMatch(Rest,"^([A-Za-z][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)")){
				Results.Add (t_identifier);
				ResultsV.Add(Regex.Match(Rest,"^([A-Za-z][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)").Value);}

			if (Regex.IsMatch(Rest,"^(([0-9]*[.])?[0-9]+)")){
				Results.Add (t_number);
				ResultsV.Add(Regex.Match(Rest,"^(([0-9]*[.])?[0-9]+)").Value);}

			if (Regex.IsMatch(Rest,"^(<[^<;:\" ]+\\.[^<;:\" ]+>)")){
				Results.Add (t_file);
				ResultsV.Add(Regex.Match(Rest,"^(<[^<;:\" ]+\\.[^<;:\" ]+>)").Value);}

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
