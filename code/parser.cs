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
                int t_number = 308;
                int t_identifier = 309;
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
         Console.WriteLine(yyval);
         
       break;
							case    2 : 
         yyval = yyv[yysp-0];
         
       break;
							case    3 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case    4 : 
         yyval = yyv[yysp-0];
         
       break;
							case    5 : 
         yyval = yyv[yysp-0];
         
       break;
							case    6 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case    7 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case    8 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case    9 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   14 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   15 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   16 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   17 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   22 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   23 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   24 : 
         yyval = yyv[yysp-8] + yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   25 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   26 : 
         yyval = yyv[yysp-0];
         
       break;
							case   27 : 
         yyval = yyv[yysp-0];
         
       break;
							case   28 : 
         yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   29 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   30 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   31 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   32 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   33 : 
         yyval = "";
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   44 : 
         yyval = yyv[yysp-0];
         
       break;
							case   45 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   46 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   47 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   48 : 
         yyval = yyv[yysp-0];
         
       break;
							case   49 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   50 : 
         yyval = yyv[yysp-0];
         
       break;
							case   51 : 
         yyval = yyv[yysp-0];
         
       break;
							case   52 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   53 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   58 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   59 : 
         yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   60 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   61 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   62 : 
         yyval = yyv[yysp-0];
         
       break;
							case   63 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   64 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   65 : 
         yyval = yyv[yysp-0];
         
       break;
							case   66 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   67 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   96 : 
         yyval = yyv[yysp-0];
         
       break;
							case   97 : 
         yyval = yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   98 : 
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   99 : 
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  100 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  101 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  102 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  103 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  104 : 
         yyval = yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  107 : 
         yyval = yyv[yysp-0];
         
       break;
							case  108 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  109 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case  117 : 
         yyval = yyv[yysp-0];
         
       break;
							case  118 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case  128 : 
         yyval = yyv[yysp-0];
         
       break;
							case  129 : 
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  130 : 
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

					int yynacts   = 1239;
					int yyngotos  = 455;
					int yynstates = 226;
					int yynrules  = 130;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,12);yyac++; 
					yya[yyac] = new YYARec(259,13);yyac++; 
					yya[yyac] = new YYARec(262,14);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(257,12);yyac++; 
					yya[yyac] = new YYARec(259,13);yyac++; 
					yya[yyac] = new YYARec(262,14);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(0,-2 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(308,46);yyac++; 
					yya[yyac] = new YYARec(258,47);yyac++; 
					yya[yyac] = new YYARec(263,51);yyac++; 
					yya[yyac] = new YYARec(264,52);yyac++; 
					yya[yyac] = new YYARec(307,53);yyac++; 
					yya[yyac] = new YYARec(310,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(258,-119 );yyac++; 
					yya[yyac] = new YYARec(258,54);yyac++; 
					yya[yyac] = new YYARec(258,55);yyac++; 
					yya[yyac] = new YYARec(258,56);yyac++; 
					yya[yyac] = new YYARec(263,57);yyac++; 
					yya[yyac] = new YYARec(258,58);yyac++; 
					yya[yyac] = new YYARec(263,59);yyac++; 
					yya[yyac] = new YYARec(258,-27 );yyac++; 
					yya[yyac] = new YYARec(264,61);yyac++; 
					yya[yyac] = new YYARec(310,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(267,77);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(296,78);yyac++; 
					yya[yyac] = new YYARec(297,79);yyac++; 
					yya[yyac] = new YYARec(298,80);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-33 );yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(262,14);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(262,14);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(258,91);yyac++; 
					yya[yyac] = new YYARec(267,77);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(296,78);yyac++; 
					yya[yyac] = new YYARec(297,79);yyac++; 
					yya[yyac] = new YYARec(298,80);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-33 );yyac++; 
					yya[yyac] = new YYARec(264,93);yyac++; 
					yya[yyac] = new YYARec(267,77);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(296,78);yyac++; 
					yya[yyac] = new YYARec(297,79);yyac++; 
					yya[yyac] = new YYARec(298,80);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-33 );yyac++; 
					yya[yyac] = new YYARec(258,95);yyac++; 
					yya[yyac] = new YYARec(265,96);yyac++; 
					yya[yyac] = new YYARec(263,103);yyac++; 
					yya[yyac] = new YYARec(266,104);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(258,-41 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(267,110);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
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
					yya[yyac] = new YYARec(267,112);yyac++; 
					yya[yyac] = new YYARec(264,113);yyac++; 
					yya[yyac] = new YYARec(267,115);yyac++; 
					yya[yyac] = new YYARec(260,116);yyac++; 
					yya[yyac] = new YYARec(261,117);yyac++; 
					yya[yyac] = new YYARec(258,118);yyac++; 
					yya[yyac] = new YYARec(258,119);yyac++; 
					yya[yyac] = new YYARec(307,53);yyac++; 
					yya[yyac] = new YYARec(258,-119 );yyac++; 
					yya[yyac] = new YYARec(263,-119 );yyac++; 
					yya[yyac] = new YYARec(268,-119 );yyac++; 
					yya[yyac] = new YYARec(269,-119 );yyac++; 
					yya[yyac] = new YYARec(270,-119 );yyac++; 
					yya[yyac] = new YYARec(271,-119 );yyac++; 
					yya[yyac] = new YYARec(272,-119 );yyac++; 
					yya[yyac] = new YYARec(273,-119 );yyac++; 
					yya[yyac] = new YYARec(274,-119 );yyac++; 
					yya[yyac] = new YYARec(275,-119 );yyac++; 
					yya[yyac] = new YYARec(276,-119 );yyac++; 
					yya[yyac] = new YYARec(277,-119 );yyac++; 
					yya[yyac] = new YYARec(278,-119 );yyac++; 
					yya[yyac] = new YYARec(279,-119 );yyac++; 
					yya[yyac] = new YYARec(280,-119 );yyac++; 
					yya[yyac] = new YYARec(281,-119 );yyac++; 
					yya[yyac] = new YYARec(282,-119 );yyac++; 
					yya[yyac] = new YYARec(283,-119 );yyac++; 
					yya[yyac] = new YYARec(284,-119 );yyac++; 
					yya[yyac] = new YYARec(285,-119 );yyac++; 
					yya[yyac] = new YYARec(286,-119 );yyac++; 
					yya[yyac] = new YYARec(287,-119 );yyac++; 
					yya[yyac] = new YYARec(288,-119 );yyac++; 
					yya[yyac] = new YYARec(289,-119 );yyac++; 
					yya[yyac] = new YYARec(290,-119 );yyac++; 
					yya[yyac] = new YYARec(291,-119 );yyac++; 
					yya[yyac] = new YYARec(292,-119 );yyac++; 
					yya[yyac] = new YYARec(293,-119 );yyac++; 
					yya[yyac] = new YYARec(294,-119 );yyac++; 
					yya[yyac] = new YYARec(295,-119 );yyac++; 
					yya[yyac] = new YYARec(299,-119 );yyac++; 
					yya[yyac] = new YYARec(300,-119 );yyac++; 
					yya[yyac] = new YYARec(301,-119 );yyac++; 
					yya[yyac] = new YYARec(302,-119 );yyac++; 
					yya[yyac] = new YYARec(303,-119 );yyac++; 
					yya[yyac] = new YYARec(304,-119 );yyac++; 
					yya[yyac] = new YYARec(305,-119 );yyac++; 
					yya[yyac] = new YYARec(306,-119 );yyac++; 
					yya[yyac] = new YYARec(308,-119 );yyac++; 
					yya[yyac] = new YYARec(309,-119 );yyac++; 
					yya[yyac] = new YYARec(263,120);yyac++; 
					yya[yyac] = new YYARec(265,121);yyac++; 
					yya[yyac] = new YYARec(267,77);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(296,78);yyac++; 
					yya[yyac] = new YYARec(297,79);yyac++; 
					yya[yyac] = new YYARec(298,80);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-33 );yyac++; 
					yya[yyac] = new YYARec(267,77);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(296,78);yyac++; 
					yya[yyac] = new YYARec(297,79);yyac++; 
					yya[yyac] = new YYARec(298,80);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-33 );yyac++; 
					yya[yyac] = new YYARec(263,126);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(258,-48 );yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(263,127);yyac++; 
					yya[yyac] = new YYARec(258,-52 );yyac++; 
					yya[yyac] = new YYARec(263,128);yyac++; 
					yya[yyac] = new YYARec(258,-53 );yyac++; 
					yya[yyac] = new YYARec(277,130);yyac++; 
					yya[yyac] = new YYARec(278,131);yyac++; 
					yya[yyac] = new YYARec(279,132);yyac++; 
					yya[yyac] = new YYARec(280,133);yyac++; 
					yya[yyac] = new YYARec(281,134);yyac++; 
					yya[yyac] = new YYARec(258,-50 );yyac++; 
					yya[yyac] = new YYARec(263,-50 );yyac++; 
					yya[yyac] = new YYARec(275,-50 );yyac++; 
					yya[yyac] = new YYARec(276,-50 );yyac++; 
					yya[yyac] = new YYARec(282,-50 );yyac++; 
					yya[yyac] = new YYARec(283,-50 );yyac++; 
					yya[yyac] = new YYARec(284,-50 );yyac++; 
					yya[yyac] = new YYARec(285,-50 );yyac++; 
					yya[yyac] = new YYARec(286,-50 );yyac++; 
					yya[yyac] = new YYARec(287,-50 );yyac++; 
					yya[yyac] = new YYARec(288,-50 );yyac++; 
					yya[yyac] = new YYARec(289,-50 );yyac++; 
					yya[yyac] = new YYARec(290,-50 );yyac++; 
					yya[yyac] = new YYARec(291,-50 );yyac++; 
					yya[yyac] = new YYARec(292,-50 );yyac++; 
					yya[yyac] = new YYARec(293,-50 );yyac++; 
					yya[yyac] = new YYARec(294,-50 );yyac++; 
					yya[yyac] = new YYARec(295,-50 );yyac++; 
					yya[yyac] = new YYARec(308,-50 );yyac++; 
					yya[yyac] = new YYARec(309,-50 );yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(267,77);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(296,78);yyac++; 
					yya[yyac] = new YYARec(297,79);yyac++; 
					yya[yyac] = new YYARec(298,80);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-33 );yyac++; 
					yya[yyac] = new YYARec(268,138);yyac++; 
					yya[yyac] = new YYARec(299,139);yyac++; 
					yya[yyac] = new YYARec(300,140);yyac++; 
					yya[yyac] = new YYARec(301,141);yyac++; 
					yya[yyac] = new YYARec(302,142);yyac++; 
					yya[yyac] = new YYARec(303,143);yyac++; 
					yya[yyac] = new YYARec(304,144);yyac++; 
					yya[yyac] = new YYARec(305,145);yyac++; 
					yya[yyac] = new YYARec(306,146);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(308,46);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(269,151);yyac++; 
					yya[yyac] = new YYARec(270,152);yyac++; 
					yya[yyac] = new YYARec(271,153);yyac++; 
					yya[yyac] = new YYARec(272,154);yyac++; 
					yya[yyac] = new YYARec(273,155);yyac++; 
					yya[yyac] = new YYARec(274,156);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(268,-104 );yyac++; 
					yya[yyac] = new YYARec(299,-104 );yyac++; 
					yya[yyac] = new YYARec(300,-104 );yyac++; 
					yya[yyac] = new YYARec(301,-104 );yyac++; 
					yya[yyac] = new YYARec(302,-104 );yyac++; 
					yya[yyac] = new YYARec(303,-104 );yyac++; 
					yya[yyac] = new YYARec(304,-104 );yyac++; 
					yya[yyac] = new YYARec(305,-104 );yyac++; 
					yya[yyac] = new YYARec(306,-104 );yyac++; 
					yya[yyac] = new YYARec(267,160);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
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
					yya[yyac] = new YYARec(264,161);yyac++; 
					yya[yyac] = new YYARec(267,110);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
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
					yya[yyac] = new YYARec(267,77);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(296,78);yyac++; 
					yya[yyac] = new YYARec(297,79);yyac++; 
					yya[yyac] = new YYARec(298,80);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-33 );yyac++; 
					yya[yyac] = new YYARec(264,164);yyac++; 
					yya[yyac] = new YYARec(267,110);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
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
					yya[yyac] = new YYARec(262,14);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(265,168);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(267,176);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
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
					yya[yyac] = new YYARec(267,179);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
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
					yya[yyac] = new YYARec(299,139);yyac++; 
					yya[yyac] = new YYARec(300,140);yyac++; 
					yya[yyac] = new YYARec(301,141);yyac++; 
					yya[yyac] = new YYARec(302,142);yyac++; 
					yya[yyac] = new YYARec(303,143);yyac++; 
					yya[yyac] = new YYARec(304,144);yyac++; 
					yya[yyac] = new YYARec(305,145);yyac++; 
					yya[yyac] = new YYARec(306,146);yyac++; 
					yya[yyac] = new YYARec(267,181);yyac++; 
					yya[yyac] = new YYARec(258,-126 );yyac++; 
					yya[yyac] = new YYARec(268,-126 );yyac++; 
					yya[yyac] = new YYARec(269,-126 );yyac++; 
					yya[yyac] = new YYARec(270,-126 );yyac++; 
					yya[yyac] = new YYARec(271,-126 );yyac++; 
					yya[yyac] = new YYARec(272,-126 );yyac++; 
					yya[yyac] = new YYARec(273,-126 );yyac++; 
					yya[yyac] = new YYARec(274,-126 );yyac++; 
					yya[yyac] = new YYARec(275,-126 );yyac++; 
					yya[yyac] = new YYARec(276,-126 );yyac++; 
					yya[yyac] = new YYARec(299,-126 );yyac++; 
					yya[yyac] = new YYARec(300,-126 );yyac++; 
					yya[yyac] = new YYARec(301,-126 );yyac++; 
					yya[yyac] = new YYARec(302,-126 );yyac++; 
					yya[yyac] = new YYARec(303,-126 );yyac++; 
					yya[yyac] = new YYARec(304,-126 );yyac++; 
					yya[yyac] = new YYARec(305,-126 );yyac++; 
					yya[yyac] = new YYARec(306,-126 );yyac++; 
					yya[yyac] = new YYARec(307,-126 );yyac++; 
					yya[yyac] = new YYARec(267,184);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
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
					yya[yyac] = new YYARec(268,185);yyac++; 
					yya[yyac] = new YYARec(299,139);yyac++; 
					yya[yyac] = new YYARec(300,140);yyac++; 
					yya[yyac] = new YYARec(301,141);yyac++; 
					yya[yyac] = new YYARec(302,142);yyac++; 
					yya[yyac] = new YYARec(303,143);yyac++; 
					yya[yyac] = new YYARec(304,144);yyac++; 
					yya[yyac] = new YYARec(305,145);yyac++; 
					yya[yyac] = new YYARec(306,146);yyac++; 
					yya[yyac] = new YYARec(268,187);yyac++; 
					yya[yyac] = new YYARec(269,151);yyac++; 
					yya[yyac] = new YYARec(270,152);yyac++; 
					yya[yyac] = new YYARec(271,153);yyac++; 
					yya[yyac] = new YYARec(272,154);yyac++; 
					yya[yyac] = new YYARec(273,155);yyac++; 
					yya[yyac] = new YYARec(274,156);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(299,-104 );yyac++; 
					yya[yyac] = new YYARec(300,-104 );yyac++; 
					yya[yyac] = new YYARec(301,-104 );yyac++; 
					yya[yyac] = new YYARec(302,-104 );yyac++; 
					yya[yyac] = new YYARec(303,-104 );yyac++; 
					yya[yyac] = new YYARec(304,-104 );yyac++; 
					yya[yyac] = new YYARec(305,-104 );yyac++; 
					yya[yyac] = new YYARec(306,-104 );yyac++; 
					yya[yyac] = new YYARec(267,160);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
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
					yya[yyac] = new YYARec(267,77);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(296,78);yyac++; 
					yya[yyac] = new YYARec(297,79);yyac++; 
					yya[yyac] = new YYARec(298,80);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-33 );yyac++; 
					yya[yyac] = new YYARec(268,190);yyac++; 
					yya[yyac] = new YYARec(299,139);yyac++; 
					yya[yyac] = new YYARec(300,140);yyac++; 
					yya[yyac] = new YYARec(301,141);yyac++; 
					yya[yyac] = new YYARec(302,142);yyac++; 
					yya[yyac] = new YYARec(303,143);yyac++; 
					yya[yyac] = new YYARec(304,144);yyac++; 
					yya[yyac] = new YYARec(305,145);yyac++; 
					yya[yyac] = new YYARec(306,146);yyac++; 
					yya[yyac] = new YYARec(265,191);yyac++; 
					yya[yyac] = new YYARec(267,77);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(296,78);yyac++; 
					yya[yyac] = new YYARec(297,79);yyac++; 
					yya[yyac] = new YYARec(298,80);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-33 );yyac++; 
					yya[yyac] = new YYARec(268,193);yyac++; 
					yya[yyac] = new YYARec(299,139);yyac++; 
					yya[yyac] = new YYARec(300,140);yyac++; 
					yya[yyac] = new YYARec(301,141);yyac++; 
					yya[yyac] = new YYARec(302,142);yyac++; 
					yya[yyac] = new YYARec(303,143);yyac++; 
					yya[yyac] = new YYARec(304,144);yyac++; 
					yya[yyac] = new YYARec(305,145);yyac++; 
					yya[yyac] = new YYARec(306,146);yyac++; 
					yya[yyac] = new YYARec(261,194);yyac++; 
					yya[yyac] = new YYARec(263,195);yyac++; 
					yya[yyac] = new YYARec(258,-25 );yyac++; 
					yya[yyac] = new YYARec(297,196);yyac++; 
					yya[yyac] = new YYARec(263,197);yyac++; 
					yya[yyac] = new YYARec(258,-58 );yyac++; 
					yya[yyac] = new YYARec(269,151);yyac++; 
					yya[yyac] = new YYARec(270,152);yyac++; 
					yya[yyac] = new YYARec(271,153);yyac++; 
					yya[yyac] = new YYARec(272,154);yyac++; 
					yya[yyac] = new YYARec(273,155);yyac++; 
					yya[yyac] = new YYARec(274,156);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(258,-60 );yyac++; 
					yya[yyac] = new YYARec(267,176);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
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
					yya[yyac] = new YYARec(269,151);yyac++; 
					yya[yyac] = new YYARec(270,152);yyac++; 
					yya[yyac] = new YYARec(271,153);yyac++; 
					yya[yyac] = new YYARec(272,154);yyac++; 
					yya[yyac] = new YYARec(273,155);yyac++; 
					yya[yyac] = new YYARec(274,156);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(268,-107 );yyac++; 
					yya[yyac] = new YYARec(299,-107 );yyac++; 
					yya[yyac] = new YYARec(300,-107 );yyac++; 
					yya[yyac] = new YYARec(301,-107 );yyac++; 
					yya[yyac] = new YYARec(302,-107 );yyac++; 
					yya[yyac] = new YYARec(303,-107 );yyac++; 
					yya[yyac] = new YYARec(304,-107 );yyac++; 
					yya[yyac] = new YYARec(305,-107 );yyac++; 
					yya[yyac] = new YYARec(306,-107 );yyac++; 
					yya[yyac] = new YYARec(267,160);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
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
					yya[yyac] = new YYARec(267,201);yyac++; 
					yya[yyac] = new YYARec(267,176);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
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
					yya[yyac] = new YYARec(267,176);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
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
					yya[yyac] = new YYARec(267,179);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
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
					yya[yyac] = new YYARec(268,205);yyac++; 
					yya[yyac] = new YYARec(265,206);yyac++; 
					yya[yyac] = new YYARec(264,207);yyac++; 
					yya[yyac] = new YYARec(299,139);yyac++; 
					yya[yyac] = new YYARec(300,140);yyac++; 
					yya[yyac] = new YYARec(301,141);yyac++; 
					yya[yyac] = new YYARec(302,142);yyac++; 
					yya[yyac] = new YYARec(303,143);yyac++; 
					yya[yyac] = new YYARec(304,144);yyac++; 
					yya[yyac] = new YYARec(305,145);yyac++; 
					yya[yyac] = new YYARec(306,146);yyac++; 
					yya[yyac] = new YYARec(265,208);yyac++; 
					yya[yyac] = new YYARec(264,209);yyac++; 
					yya[yyac] = new YYARec(299,139);yyac++; 
					yya[yyac] = new YYARec(300,140);yyac++; 
					yya[yyac] = new YYARec(301,141);yyac++; 
					yya[yyac] = new YYARec(302,142);yyac++; 
					yya[yyac] = new YYARec(303,143);yyac++; 
					yya[yyac] = new YYARec(304,144);yyac++; 
					yya[yyac] = new YYARec(305,145);yyac++; 
					yya[yyac] = new YYARec(306,146);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(264,211);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(268,187);yyac++; 
					yya[yyac] = new YYARec(269,151);yyac++; 
					yya[yyac] = new YYARec(270,152);yyac++; 
					yya[yyac] = new YYARec(271,153);yyac++; 
					yya[yyac] = new YYARec(272,154);yyac++; 
					yya[yyac] = new YYARec(273,155);yyac++; 
					yya[yyac] = new YYARec(274,156);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(268,213);yyac++; 
					yya[yyac] = new YYARec(267,110);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
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
					yya[yyac] = new YYARec(268,215);yyac++; 
					yya[yyac] = new YYARec(269,151);yyac++; 
					yya[yyac] = new YYARec(270,152);yyac++; 
					yya[yyac] = new YYARec(271,153);yyac++; 
					yya[yyac] = new YYARec(272,154);yyac++; 
					yya[yyac] = new YYARec(273,155);yyac++; 
					yya[yyac] = new YYARec(274,156);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(268,216);yyac++; 
					yya[yyac] = new YYARec(269,151);yyac++; 
					yya[yyac] = new YYARec(270,152);yyac++; 
					yya[yyac] = new YYARec(271,153);yyac++; 
					yya[yyac] = new YYARec(272,154);yyac++; 
					yya[yyac] = new YYARec(273,155);yyac++; 
					yya[yyac] = new YYARec(274,156);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(299,-105 );yyac++; 
					yya[yyac] = new YYARec(300,-105 );yyac++; 
					yya[yyac] = new YYARec(301,-105 );yyac++; 
					yya[yyac] = new YYARec(302,-105 );yyac++; 
					yya[yyac] = new YYARec(303,-105 );yyac++; 
					yya[yyac] = new YYARec(304,-105 );yyac++; 
					yya[yyac] = new YYARec(305,-105 );yyac++; 
					yya[yyac] = new YYARec(306,-105 );yyac++; 
					yya[yyac] = new YYARec(268,-109 );yyac++; 
					yya[yyac] = new YYARec(299,-103 );yyac++; 
					yya[yyac] = new YYARec(300,-103 );yyac++; 
					yya[yyac] = new YYARec(301,-103 );yyac++; 
					yya[yyac] = new YYARec(302,-103 );yyac++; 
					yya[yyac] = new YYARec(303,-103 );yyac++; 
					yya[yyac] = new YYARec(304,-103 );yyac++; 
					yya[yyac] = new YYARec(305,-103 );yyac++; 
					yya[yyac] = new YYARec(306,-103 );yyac++; 
					yya[yyac] = new YYARec(268,-108 );yyac++; 
					yya[yyac] = new YYARec(267,77);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(296,78);yyac++; 
					yya[yyac] = new YYARec(297,79);yyac++; 
					yya[yyac] = new YYARec(298,80);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-33 );yyac++; 
					yya[yyac] = new YYARec(267,77);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(296,78);yyac++; 
					yya[yyac] = new YYARec(297,79);yyac++; 
					yya[yyac] = new YYARec(298,80);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-33 );yyac++; 
					yya[yyac] = new YYARec(263,219);yyac++; 
					yya[yyac] = new YYARec(267,77);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(296,78);yyac++; 
					yya[yyac] = new YYARec(297,79);yyac++; 
					yya[yyac] = new YYARec(298,80);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-33 );yyac++; 
					yya[yyac] = new YYARec(268,221);yyac++; 
					yya[yyac] = new YYARec(299,139);yyac++; 
					yya[yyac] = new YYARec(300,140);yyac++; 
					yya[yyac] = new YYARec(301,141);yyac++; 
					yya[yyac] = new YYARec(302,142);yyac++; 
					yya[yyac] = new YYARec(303,143);yyac++; 
					yya[yyac] = new YYARec(304,144);yyac++; 
					yya[yyac] = new YYARec(305,145);yyac++; 
					yya[yyac] = new YYARec(306,146);yyac++; 
					yya[yyac] = new YYARec(265,222);yyac++; 
					yya[yyac] = new YYARec(265,223);yyac++; 
					yya[yyac] = new YYARec(275,38);yyac++; 
					yya[yyac] = new YYARec(276,39);yyac++; 
					yya[yyac] = new YYARec(282,15);yyac++; 
					yya[yyac] = new YYARec(283,16);yyac++; 
					yya[yyac] = new YYARec(284,17);yyac++; 
					yya[yyac] = new YYARec(285,18);yyac++; 
					yya[yyac] = new YYARec(286,19);yyac++; 
					yya[yyac] = new YYARec(287,20);yyac++; 
					yya[yyac] = new YYARec(288,21);yyac++; 
					yya[yyac] = new YYARec(289,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(308,-121 );yyac++; 
					yya[yyac] = new YYARec(265,225);yyac++; 
					yya[yyac] = new YYARec(299,139);yyac++; 
					yya[yyac] = new YYARec(300,140);yyac++; 
					yya[yyac] = new YYARec(301,141);yyac++; 
					yya[yyac] = new YYARec(302,142);yyac++; 
					yya[yyac] = new YYARec(303,143);yyac++; 
					yya[yyac] = new YYARec(304,144);yyac++; 
					yya[yyac] = new YYARec(305,145);yyac++; 
					yya[yyac] = new YYARec(306,146);yyac++; 
					yya[yyac] = new YYARec(264,-129 );yyac++;

					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-12,2);yygc++; 
					yyg[yygc] = new YYARec(-11,3);yygc++; 
					yyg[yygc] = new YYARec(-10,4);yygc++; 
					yyg[yygc] = new YYARec(-9,5);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-6,7);yygc++; 
					yyg[yygc] = new YYARec(-5,8);yygc++; 
					yyg[yygc] = new YYARec(-4,9);yygc++; 
					yyg[yygc] = new YYARec(-3,10);yygc++; 
					yyg[yygc] = new YYARec(-2,11);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-40,31);yygc++; 
					yyg[yygc] = new YYARec(-17,32);yygc++; 
					yyg[yygc] = new YYARec(-16,33);yygc++; 
					yyg[yygc] = new YYARec(-15,34);yygc++; 
					yyg[yygc] = new YYARec(-14,35);yygc++; 
					yyg[yygc] = new YYARec(-13,36);yygc++; 
					yyg[yygc] = new YYARec(-7,37);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-12,2);yygc++; 
					yyg[yygc] = new YYARec(-11,3);yygc++; 
					yyg[yygc] = new YYARec(-10,4);yygc++; 
					yyg[yygc] = new YYARec(-9,5);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-6,7);yygc++; 
					yyg[yygc] = new YYARec(-5,8);yygc++; 
					yyg[yygc] = new YYARec(-4,9);yygc++; 
					yyg[yygc] = new YYARec(-3,42);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-7,43);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-7,44);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-7,45);yygc++; 
					yyg[yygc] = new YYARec(-18,48);yygc++; 
					yyg[yygc] = new YYARec(-17,49);yygc++; 
					yyg[yygc] = new YYARec(-16,50);yygc++; 
					yyg[yygc] = new YYARec(-18,60);yygc++; 
					yyg[yygc] = new YYARec(-17,49);yygc++; 
					yyg[yygc] = new YYARec(-16,50);yygc++; 
					yyg[yygc] = new YYARec(-43,62);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-32,63);yygc++; 
					yyg[yygc] = new YYARec(-31,64);yygc++; 
					yyg[yygc] = new YYARec(-30,65);yygc++; 
					yyg[yygc] = new YYARec(-29,66);yygc++; 
					yyg[yygc] = new YYARec(-28,67);yygc++; 
					yyg[yygc] = new YYARec(-27,68);yygc++; 
					yyg[yygc] = new YYARec(-26,69);yygc++; 
					yyg[yygc] = new YYARec(-25,70);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-23,72);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-20,75);yygc++; 
					yyg[yygc] = new YYARec(-7,76);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-7,81);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-12,2);yygc++; 
					yyg[yygc] = new YYARec(-11,3);yygc++; 
					yyg[yygc] = new YYARec(-10,4);yygc++; 
					yyg[yygc] = new YYARec(-9,5);yygc++; 
					yyg[yygc] = new YYARec(-8,82);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-5,83);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-12,2);yygc++; 
					yyg[yygc] = new YYARec(-11,3);yygc++; 
					yyg[yygc] = new YYARec(-10,4);yygc++; 
					yyg[yygc] = new YYARec(-9,5);yygc++; 
					yyg[yygc] = new YYARec(-8,84);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-5,83);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-40,31);yygc++; 
					yyg[yygc] = new YYARec(-14,85);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-7,87);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-40,31);yygc++; 
					yyg[yygc] = new YYARec(-19,88);yygc++; 
					yyg[yygc] = new YYARec(-14,89);yygc++; 
					yyg[yygc] = new YYARec(-7,90);yygc++; 
					yyg[yygc] = new YYARec(-43,62);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-32,63);yygc++; 
					yyg[yygc] = new YYARec(-31,64);yygc++; 
					yyg[yygc] = new YYARec(-30,65);yygc++; 
					yyg[yygc] = new YYARec(-29,66);yygc++; 
					yyg[yygc] = new YYARec(-28,67);yygc++; 
					yyg[yygc] = new YYARec(-27,68);yygc++; 
					yyg[yygc] = new YYARec(-26,69);yygc++; 
					yyg[yygc] = new YYARec(-25,70);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-23,72);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-20,92);yygc++; 
					yyg[yygc] = new YYARec(-7,76);yygc++; 
					yyg[yygc] = new YYARec(-43,62);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-32,63);yygc++; 
					yyg[yygc] = new YYARec(-31,64);yygc++; 
					yyg[yygc] = new YYARec(-30,65);yygc++; 
					yyg[yygc] = new YYARec(-29,66);yygc++; 
					yyg[yygc] = new YYARec(-28,67);yygc++; 
					yyg[yygc] = new YYARec(-27,68);yygc++; 
					yyg[yygc] = new YYARec(-26,69);yygc++; 
					yyg[yygc] = new YYARec(-25,70);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-23,72);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-20,94);yygc++; 
					yyg[yygc] = new YYARec(-7,76);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-40,31);yygc++; 
					yyg[yygc] = new YYARec(-34,97);yygc++; 
					yyg[yygc] = new YYARec(-33,98);yygc++; 
					yyg[yygc] = new YYARec(-17,99);yygc++; 
					yyg[yygc] = new YYARec(-16,100);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-13,102);yygc++; 
					yyg[yygc] = new YYARec(-7,87);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-40,106);yygc++; 
					yyg[yygc] = new YYARec(-37,107);yygc++; 
					yyg[yygc] = new YYARec(-36,108);yygc++; 
					yyg[yygc] = new YYARec(-14,109);yygc++; 
					yyg[yygc] = new YYARec(-43,111);yygc++; 
					yyg[yygc] = new YYARec(-43,114);yygc++; 
					yyg[yygc] = new YYARec(-43,62);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-32,63);yygc++; 
					yyg[yygc] = new YYARec(-31,64);yygc++; 
					yyg[yygc] = new YYARec(-30,65);yygc++; 
					yyg[yygc] = new YYARec(-29,66);yygc++; 
					yyg[yygc] = new YYARec(-28,67);yygc++; 
					yyg[yygc] = new YYARec(-27,68);yygc++; 
					yyg[yygc] = new YYARec(-26,69);yygc++; 
					yyg[yygc] = new YYARec(-25,70);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-23,72);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-20,122);yygc++; 
					yyg[yygc] = new YYARec(-7,76);yygc++; 
					yyg[yygc] = new YYARec(-43,62);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-32,63);yygc++; 
					yyg[yygc] = new YYARec(-31,64);yygc++; 
					yyg[yygc] = new YYARec(-30,65);yygc++; 
					yyg[yygc] = new YYARec(-29,66);yygc++; 
					yyg[yygc] = new YYARec(-28,67);yygc++; 
					yyg[yygc] = new YYARec(-27,68);yygc++; 
					yyg[yygc] = new YYARec(-26,69);yygc++; 
					yyg[yygc] = new YYARec(-25,70);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-23,72);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-20,123);yygc++; 
					yyg[yygc] = new YYARec(-7,76);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-40,31);yygc++; 
					yyg[yygc] = new YYARec(-34,97);yygc++; 
					yyg[yygc] = new YYARec(-33,124);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-13,125);yygc++; 
					yyg[yygc] = new YYARec(-7,87);yygc++; 
					yyg[yygc] = new YYARec(-35,129);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-40,31);yygc++; 
					yyg[yygc] = new YYARec(-34,97);yygc++; 
					yyg[yygc] = new YYARec(-33,135);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-13,125);yygc++; 
					yyg[yygc] = new YYARec(-7,87);yygc++; 
					yyg[yygc] = new YYARec(-43,62);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-32,63);yygc++; 
					yyg[yygc] = new YYARec(-31,64);yygc++; 
					yyg[yygc] = new YYARec(-30,65);yygc++; 
					yyg[yygc] = new YYARec(-29,66);yygc++; 
					yyg[yygc] = new YYARec(-28,67);yygc++; 
					yyg[yygc] = new YYARec(-27,68);yygc++; 
					yyg[yygc] = new YYARec(-26,69);yygc++; 
					yyg[yygc] = new YYARec(-25,70);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-23,72);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-20,136);yygc++; 
					yyg[yygc] = new YYARec(-7,76);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-41,147);yygc++; 
					yyg[yygc] = new YYARec(-13,148);yygc++; 
					yyg[yygc] = new YYARec(-7,87);yygc++; 
					yyg[yygc] = new YYARec(-42,149);yygc++; 
					yyg[yygc] = new YYARec(-38,150);yygc++; 
					yyg[yygc] = new YYARec(-45,157);yygc++; 
					yyg[yygc] = new YYARec(-44,158);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-40,106);yygc++; 
					yyg[yygc] = new YYARec(-37,107);yygc++; 
					yyg[yygc] = new YYARec(-36,159);yygc++; 
					yyg[yygc] = new YYARec(-14,109);yygc++; 
					yyg[yygc] = new YYARec(-44,162);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-40,106);yygc++; 
					yyg[yygc] = new YYARec(-37,107);yygc++; 
					yyg[yygc] = new YYARec(-36,108);yygc++; 
					yyg[yygc] = new YYARec(-14,109);yygc++; 
					yyg[yygc] = new YYARec(-43,62);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-32,63);yygc++; 
					yyg[yygc] = new YYARec(-31,64);yygc++; 
					yyg[yygc] = new YYARec(-30,65);yygc++; 
					yyg[yygc] = new YYARec(-29,66);yygc++; 
					yyg[yygc] = new YYARec(-28,67);yygc++; 
					yyg[yygc] = new YYARec(-27,68);yygc++; 
					yyg[yygc] = new YYARec(-26,69);yygc++; 
					yyg[yygc] = new YYARec(-25,70);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-23,72);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-20,163);yygc++; 
					yyg[yygc] = new YYARec(-7,76);yygc++; 
					yyg[yygc] = new YYARec(-44,165);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-40,106);yygc++; 
					yyg[yygc] = new YYARec(-37,107);yygc++; 
					yyg[yygc] = new YYARec(-36,108);yygc++; 
					yyg[yygc] = new YYARec(-14,109);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-12,2);yygc++; 
					yyg[yygc] = new YYARec(-11,3);yygc++; 
					yyg[yygc] = new YYARec(-10,4);yygc++; 
					yyg[yygc] = new YYARec(-9,5);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-5,166);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-40,31);yygc++; 
					yyg[yygc] = new YYARec(-19,167);yygc++; 
					yyg[yygc] = new YYARec(-14,89);yygc++; 
					yyg[yygc] = new YYARec(-7,90);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-40,31);yygc++; 
					yyg[yygc] = new YYARec(-34,97);yygc++; 
					yyg[yygc] = new YYARec(-33,169);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-13,125);yygc++; 
					yyg[yygc] = new YYARec(-7,87);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-40,31);yygc++; 
					yyg[yygc] = new YYARec(-19,170);yygc++; 
					yyg[yygc] = new YYARec(-16,171);yygc++; 
					yyg[yygc] = new YYARec(-14,89);yygc++; 
					yyg[yygc] = new YYARec(-7,90);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-40,31);yygc++; 
					yyg[yygc] = new YYARec(-16,172);yygc++; 
					yyg[yygc] = new YYARec(-14,173);yygc++; 
					yyg[yygc] = new YYARec(-13,174);yygc++; 
					yyg[yygc] = new YYARec(-7,87);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-40,106);yygc++; 
					yyg[yygc] = new YYARec(-37,107);yygc++; 
					yyg[yygc] = new YYARec(-36,175);yygc++; 
					yyg[yygc] = new YYARec(-14,109);yygc++; 
					yyg[yygc] = new YYARec(-47,177);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-40,106);yygc++; 
					yyg[yygc] = new YYARec(-37,107);yygc++; 
					yyg[yygc] = new YYARec(-36,178);yygc++; 
					yyg[yygc] = new YYARec(-14,109);yygc++; 
					yyg[yygc] = new YYARec(-46,180);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-40,106);yygc++; 
					yyg[yygc] = new YYARec(-39,182);yygc++; 
					yyg[yygc] = new YYARec(-37,183);yygc++; 
					yyg[yygc] = new YYARec(-14,109);yygc++; 
					yyg[yygc] = new YYARec(-46,186);yygc++; 
					yyg[yygc] = new YYARec(-42,149);yygc++; 
					yyg[yygc] = new YYARec(-38,150);yygc++; 
					yyg[yygc] = new YYARec(-45,188);yygc++; 
					yyg[yygc] = new YYARec(-44,158);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-40,106);yygc++; 
					yyg[yygc] = new YYARec(-37,107);yygc++; 
					yyg[yygc] = new YYARec(-36,159);yygc++; 
					yyg[yygc] = new YYARec(-14,109);yygc++; 
					yyg[yygc] = new YYARec(-43,62);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-32,63);yygc++; 
					yyg[yygc] = new YYARec(-31,64);yygc++; 
					yyg[yygc] = new YYARec(-30,65);yygc++; 
					yyg[yygc] = new YYARec(-29,66);yygc++; 
					yyg[yygc] = new YYARec(-28,67);yygc++; 
					yyg[yygc] = new YYARec(-27,68);yygc++; 
					yyg[yygc] = new YYARec(-26,69);yygc++; 
					yyg[yygc] = new YYARec(-25,70);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-23,72);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-20,189);yygc++; 
					yyg[yygc] = new YYARec(-7,76);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-43,62);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-32,63);yygc++; 
					yyg[yygc] = new YYARec(-31,64);yygc++; 
					yyg[yygc] = new YYARec(-30,65);yygc++; 
					yyg[yygc] = new YYARec(-29,66);yygc++; 
					yyg[yygc] = new YYARec(-28,67);yygc++; 
					yyg[yygc] = new YYARec(-27,68);yygc++; 
					yyg[yygc] = new YYARec(-26,69);yygc++; 
					yyg[yygc] = new YYARec(-25,70);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-23,72);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-20,192);yygc++; 
					yyg[yygc] = new YYARec(-7,76);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-42,149);yygc++; 
					yyg[yygc] = new YYARec(-38,150);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-40,106);yygc++; 
					yyg[yygc] = new YYARec(-37,107);yygc++; 
					yyg[yygc] = new YYARec(-36,198);yygc++; 
					yyg[yygc] = new YYARec(-14,109);yygc++; 
					yyg[yygc] = new YYARec(-42,149);yygc++; 
					yyg[yygc] = new YYARec(-38,150);yygc++; 
					yyg[yygc] = new YYARec(-45,199);yygc++; 
					yyg[yygc] = new YYARec(-44,158);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-40,106);yygc++; 
					yyg[yygc] = new YYARec(-37,107);yygc++; 
					yyg[yygc] = new YYARec(-36,159);yygc++; 
					yyg[yygc] = new YYARec(-14,109);yygc++; 
					yyg[yygc] = new YYARec(-43,200);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-40,106);yygc++; 
					yyg[yygc] = new YYARec(-37,107);yygc++; 
					yyg[yygc] = new YYARec(-36,202);yygc++; 
					yyg[yygc] = new YYARec(-14,109);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-40,106);yygc++; 
					yyg[yygc] = new YYARec(-37,107);yygc++; 
					yyg[yygc] = new YYARec(-36,203);yygc++; 
					yyg[yygc] = new YYARec(-14,109);yygc++; 
					yyg[yygc] = new YYARec(-47,204);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-40,106);yygc++; 
					yyg[yygc] = new YYARec(-37,107);yygc++; 
					yyg[yygc] = new YYARec(-36,178);yygc++; 
					yyg[yygc] = new YYARec(-14,109);yygc++; 
					yyg[yygc] = new YYARec(-46,180);yygc++; 
					yyg[yygc] = new YYARec(-46,180);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-40,31);yygc++; 
					yyg[yygc] = new YYARec(-19,210);yygc++; 
					yyg[yygc] = new YYARec(-14,89);yygc++; 
					yyg[yygc] = new YYARec(-7,90);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-40,31);yygc++; 
					yyg[yygc] = new YYARec(-19,212);yygc++; 
					yyg[yygc] = new YYARec(-14,89);yygc++; 
					yyg[yygc] = new YYARec(-7,90);yygc++; 
					yyg[yygc] = new YYARec(-42,149);yygc++; 
					yyg[yygc] = new YYARec(-38,150);yygc++; 
					yyg[yygc] = new YYARec(-44,214);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-40,106);yygc++; 
					yyg[yygc] = new YYARec(-37,107);yygc++; 
					yyg[yygc] = new YYARec(-36,108);yygc++; 
					yyg[yygc] = new YYARec(-14,109);yygc++; 
					yyg[yygc] = new YYARec(-42,149);yygc++; 
					yyg[yygc] = new YYARec(-38,150);yygc++; 
					yyg[yygc] = new YYARec(-42,149);yygc++; 
					yyg[yygc] = new YYARec(-38,150);yygc++; 
					yyg[yygc] = new YYARec(-43,62);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-32,63);yygc++; 
					yyg[yygc] = new YYARec(-31,64);yygc++; 
					yyg[yygc] = new YYARec(-30,65);yygc++; 
					yyg[yygc] = new YYARec(-29,66);yygc++; 
					yyg[yygc] = new YYARec(-28,67);yygc++; 
					yyg[yygc] = new YYARec(-27,68);yygc++; 
					yyg[yygc] = new YYARec(-26,69);yygc++; 
					yyg[yygc] = new YYARec(-25,70);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-23,72);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-20,217);yygc++; 
					yyg[yygc] = new YYARec(-7,76);yygc++; 
					yyg[yygc] = new YYARec(-43,62);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-32,63);yygc++; 
					yyg[yygc] = new YYARec(-31,64);yygc++; 
					yyg[yygc] = new YYARec(-30,65);yygc++; 
					yyg[yygc] = new YYARec(-29,66);yygc++; 
					yyg[yygc] = new YYARec(-28,67);yygc++; 
					yyg[yygc] = new YYARec(-27,68);yygc++; 
					yyg[yygc] = new YYARec(-26,69);yygc++; 
					yyg[yygc] = new YYARec(-25,70);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-23,72);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-20,218);yygc++; 
					yyg[yygc] = new YYARec(-7,76);yygc++; 
					yyg[yygc] = new YYARec(-43,62);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-32,63);yygc++; 
					yyg[yygc] = new YYARec(-31,64);yygc++; 
					yyg[yygc] = new YYARec(-30,65);yygc++; 
					yyg[yygc] = new YYARec(-29,66);yygc++; 
					yyg[yygc] = new YYARec(-28,67);yygc++; 
					yyg[yygc] = new YYARec(-27,68);yygc++; 
					yyg[yygc] = new YYARec(-26,69);yygc++; 
					yyg[yygc] = new YYARec(-25,70);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-23,72);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-20,220);yygc++; 
					yyg[yygc] = new YYARec(-7,76);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-42,30);yygc++; 
					yyg[yygc] = new YYARec(-41,1);yygc++; 
					yyg[yygc] = new YYARec(-40,31);yygc++; 
					yyg[yygc] = new YYARec(-19,224);yygc++; 
					yyg[yygc] = new YYARec(-14,89);yygc++; 
					yyg[yygc] = new YYARec(-7,90);yygc++; 
					yyg[yygc] = new YYARec(-46,180);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = -126;  
					yyd[2] = -13;  
					yyd[3] = -12;  
					yyd[4] = -11;  
					yyd[5] = -10;  
					yyd[6] = 0;  
					yyd[7] = -5;  
					yyd[8] = -4;  
					yyd[9] = 0;  
					yyd[10] = -1;  
					yyd[11] = 0;  
					yyd[12] = 0;  
					yyd[13] = 0;  
					yyd[14] = 0;  
					yyd[15] = -83;  
					yyd[16] = -84;  
					yyd[17] = -85;  
					yyd[18] = -86;  
					yyd[19] = -87;  
					yyd[20] = -88;  
					yyd[21] = -89;  
					yyd[22] = -90;  
					yyd[23] = -91;  
					yyd[24] = -92;  
					yyd[25] = -93;  
					yyd[26] = -94;  
					yyd[27] = -95;  
					yyd[28] = -96;  
					yyd[29] = -125;  
					yyd[30] = -120;  
					yyd[31] = 0;  
					yyd[32] = -21;  
					yyd[33] = -18;  
					yyd[34] = 0;  
					yyd[35] = -20;  
					yyd[36] = -19;  
					yyd[37] = 0;  
					yyd[38] = -76;  
					yyd[39] = -77;  
					yyd[40] = -127;  
					yyd[41] = -128;  
					yyd[42] = -3;  
					yyd[43] = 0;  
					yyd[44] = 0;  
					yyd[45] = 0;  
					yyd[46] = -122;  
					yyd[47] = -17;  
					yyd[48] = 0;  
					yyd[49] = 0;  
					yyd[50] = -26;  
					yyd[51] = 0;  
					yyd[52] = 0;  
					yyd[53] = 0;  
					yyd[54] = 0;  
					yyd[55] = 0;  
					yyd[56] = -16;  
					yyd[57] = 0;  
					yyd[58] = -23;  
					yyd[59] = 0;  
					yyd[60] = 0;  
					yyd[61] = 0;  
					yyd[62] = 0;  
					yyd[63] = -44;  
					yyd[64] = -43;  
					yyd[65] = -42;  
					yyd[66] = -40;  
					yyd[67] = -39;  
					yyd[68] = -38;  
					yyd[69] = -37;  
					yyd[70] = -36;  
					yyd[71] = -35;  
					yyd[72] = -34;  
					yyd[73] = 0;  
					yyd[74] = 0;  
					yyd[75] = 0;  
					yyd[76] = 0;  
					yyd[77] = 0;  
					yyd[78] = 0;  
					yyd[79] = 0;  
					yyd[80] = 0;  
					yyd[81] = -118;  
					yyd[82] = -6;  
					yyd[83] = 0;  
					yyd[84] = -7;  
					yyd[85] = 0;  
					yyd[86] = 0;  
					yyd[87] = 0;  
					yyd[88] = 0;  
					yyd[89] = -124;  
					yyd[90] = -123;  
					yyd[91] = -22;  
					yyd[92] = 0;  
					yyd[93] = 0;  
					yyd[94] = -32;  
					yyd[95] = 0;  
					yyd[96] = -29;  
					yyd[97] = 0;  
					yyd[98] = -46;  
					yyd[99] = 0;  
					yyd[100] = 0;  
					yyd[101] = -51;  
					yyd[102] = 0;  
					yyd[103] = 0;  
					yyd[104] = 0;  
					yyd[105] = 0;  
					yyd[106] = 0;  
					yyd[107] = -62;  
					yyd[108] = 0;  
					yyd[109] = -68;  
					yyd[110] = 0;  
					yyd[111] = 0;  
					yyd[112] = 0;  
					yyd[113] = 0;  
					yyd[114] = 0;  
					yyd[115] = 0;  
					yyd[116] = 0;  
					yyd[117] = -9;  
					yyd[118] = -15;  
					yyd[119] = -14;  
					yyd[120] = 0;  
					yyd[121] = -28;  
					yyd[122] = 0;  
					yyd[123] = -31;  
					yyd[124] = -49;  
					yyd[125] = -50;  
					yyd[126] = 0;  
					yyd[127] = 0;  
					yyd[128] = 0;  
					yyd[129] = 0;  
					yyd[130] = -78;  
					yyd[131] = -79;  
					yyd[132] = -80;  
					yyd[133] = -81;  
					yyd[134] = -82;  
					yyd[135] = -45;  
					yyd[136] = -30;  
					yyd[137] = 0;  
					yyd[138] = 0;  
					yyd[139] = -110;  
					yyd[140] = -111;  
					yyd[141] = -112;  
					yyd[142] = -113;  
					yyd[143] = -114;  
					yyd[144] = -115;  
					yyd[145] = -116;  
					yyd[146] = -117;  
					yyd[147] = 0;  
					yyd[148] = -67;  
					yyd[149] = -75;  
					yyd[150] = 0;  
					yyd[151] = -69;  
					yyd[152] = -70;  
					yyd[153] = -71;  
					yyd[154] = -72;  
					yyd[155] = -73;  
					yyd[156] = -74;  
					yyd[157] = 0;  
					yyd[158] = 0;  
					yyd[159] = 0;  
					yyd[160] = 0;  
					yyd[161] = 0;  
					yyd[162] = 0;  
					yyd[163] = 0;  
					yyd[164] = 0;  
					yyd[165] = 0;  
					yyd[166] = 0;  
					yyd[167] = 0;  
					yyd[168] = 0;  
					yyd[169] = -47;  
					yyd[170] = 0;  
					yyd[171] = -54;  
					yyd[172] = -55;  
					yyd[173] = -56;  
					yyd[174] = -57;  
					yyd[175] = 0;  
					yyd[176] = 0;  
					yyd[177] = -105;  
					yyd[178] = 0;  
					yyd[179] = 0;  
					yyd[180] = 0;  
					yyd[181] = 0;  
					yyd[182] = -63;  
					yyd[183] = -65;  
					yyd[184] = 0;  
					yyd[185] = -103;  
					yyd[186] = 0;  
					yyd[187] = -61;  
					yyd[188] = 0;  
					yyd[189] = 0;  
					yyd[190] = 0;  
					yyd[191] = -100;  
					yyd[192] = 0;  
					yyd[193] = 0;  
					yyd[194] = -8;  
					yyd[195] = 0;  
					yyd[196] = 0;  
					yyd[197] = 0;  
					yyd[198] = 0;  
					yyd[199] = 0;  
					yyd[200] = -130;  
					yyd[201] = 0;  
					yyd[202] = 0;  
					yyd[203] = 0;  
					yyd[204] = 0;  
					yyd[205] = 0;  
					yyd[206] = -101;  
					yyd[207] = 0;  
					yyd[208] = -102;  
					yyd[209] = 0;  
					yyd[210] = 0;  
					yyd[211] = 0;  
					yyd[212] = -59;  
					yyd[213] = -106;  
					yyd[214] = 0;  
					yyd[215] = -66;  
					yyd[216] = -64;  
					yyd[217] = 0;  
					yyd[218] = 0;  
					yyd[219] = 0;  
					yyd[220] = 0;  
					yyd[221] = 0;  
					yyd[222] = -98;  
					yyd[223] = -99;  
					yyd[224] = -24;  
					yyd[225] = -97; 

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
					yyal[11] = 58;  
					yyal[12] = 59;  
					yyal[13] = 74;  
					yyal[14] = 89;  
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
					yyal[31] = 104;  
					yyal[32] = 105;  
					yyal[33] = 105;  
					yyal[34] = 105;  
					yyal[35] = 106;  
					yyal[36] = 106;  
					yyal[37] = 106;  
					yyal[38] = 112;  
					yyal[39] = 112;  
					yyal[40] = 112;  
					yyal[41] = 112;  
					yyal[42] = 112;  
					yyal[43] = 112;  
					yyal[44] = 113;  
					yyal[45] = 114;  
					yyal[46] = 116;  
					yyal[47] = 116;  
					yyal[48] = 116;  
					yyal[49] = 117;  
					yyal[50] = 119;  
					yyal[51] = 119;  
					yyal[52] = 122;  
					yyal[53] = 142;  
					yyal[54] = 157;  
					yyal[55] = 173;  
					yyal[56] = 189;  
					yyal[57] = 189;  
					yyal[58] = 207;  
					yyal[59] = 207;  
					yyal[60] = 225;  
					yyal[61] = 226;  
					yyal[62] = 246;  
					yyal[63] = 247;  
					yyal[64] = 247;  
					yyal[65] = 247;  
					yyal[66] = 247;  
					yyal[67] = 247;  
					yyal[68] = 247;  
					yyal[69] = 247;  
					yyal[70] = 247;  
					yyal[71] = 247;  
					yyal[72] = 247;  
					yyal[73] = 247;  
					yyal[74] = 267;  
					yyal[75] = 268;  
					yyal[76] = 269;  
					yyal[77] = 292;  
					yyal[78] = 311;  
					yyal[79] = 312;  
					yyal[80] = 313;  
					yyal[81] = 314;  
					yyal[82] = 314;  
					yyal[83] = 314;  
					yyal[84] = 316;  
					yyal[85] = 316;  
					yyal[86] = 317;  
					yyal[87] = 318;  
					yyal[88] = 359;  
					yyal[89] = 360;  
					yyal[90] = 360;  
					yyal[91] = 360;  
					yyal[92] = 360;  
					yyal[93] = 361;  
					yyal[94] = 381;  
					yyal[95] = 381;  
					yyal[96] = 401;  
					yyal[97] = 401;  
					yyal[98] = 421;  
					yyal[99] = 421;  
					yyal[100] = 423;  
					yyal[101] = 425;  
					yyal[102] = 425;  
					yyal[103] = 450;  
					yyal[104] = 468;  
					yyal[105] = 488;  
					yyal[106] = 497;  
					yyal[107] = 513;  
					yyal[108] = 513;  
					yyal[109] = 530;  
					yyal[110] = 530;  
					yyal[111] = 549;  
					yyal[112] = 550;  
					yyal[113] = 569;  
					yyal[114] = 589;  
					yyal[115] = 590;  
					yyal[116] = 609;  
					yyal[117] = 625;  
					yyal[118] = 625;  
					yyal[119] = 625;  
					yyal[120] = 625;  
					yyal[121] = 643;  
					yyal[122] = 643;  
					yyal[123] = 644;  
					yyal[124] = 644;  
					yyal[125] = 644;  
					yyal[126] = 644;  
					yyal[127] = 662;  
					yyal[128] = 681;  
					yyal[129] = 700;  
					yyal[130] = 719;  
					yyal[131] = 719;  
					yyal[132] = 719;  
					yyal[133] = 719;  
					yyal[134] = 719;  
					yyal[135] = 719;  
					yyal[136] = 719;  
					yyal[137] = 719;  
					yyal[138] = 738;  
					yyal[139] = 746;  
					yyal[140] = 746;  
					yyal[141] = 746;  
					yyal[142] = 746;  
					yyal[143] = 746;  
					yyal[144] = 746;  
					yyal[145] = 746;  
					yyal[146] = 746;  
					yyal[147] = 746;  
					yyal[148] = 766;  
					yyal[149] = 766;  
					yyal[150] = 766;  
					yyal[151] = 785;  
					yyal[152] = 785;  
					yyal[153] = 785;  
					yyal[154] = 785;  
					yyal[155] = 785;  
					yyal[156] = 785;  
					yyal[157] = 785;  
					yyal[158] = 786;  
					yyal[159] = 794;  
					yyal[160] = 811;  
					yyal[161] = 830;  
					yyal[162] = 850;  
					yyal[163] = 859;  
					yyal[164] = 860;  
					yyal[165] = 880;  
					yyal[166] = 889;  
					yyal[167] = 890;  
					yyal[168] = 892;  
					yyal[169] = 893;  
					yyal[170] = 893;  
					yyal[171] = 895;  
					yyal[172] = 895;  
					yyal[173] = 895;  
					yyal[174] = 895;  
					yyal[175] = 895;  
					yyal[176] = 904;  
					yyal[177] = 923;  
					yyal[178] = 923;  
					yyal[179] = 940;  
					yyal[180] = 959;  
					yyal[181] = 960;  
					yyal[182] = 979;  
					yyal[183] = 979;  
					yyal[184] = 979;  
					yyal[185] = 998;  
					yyal[186] = 998;  
					yyal[187] = 1017;  
					yyal[188] = 1017;  
					yyal[189] = 1018;  
					yyal[190] = 1019;  
					yyal[191] = 1028;  
					yyal[192] = 1028;  
					yyal[193] = 1029;  
					yyal[194] = 1038;  
					yyal[195] = 1038;  
					yyal[196] = 1056;  
					yyal[197] = 1057;  
					yyal[198] = 1075;  
					yyal[199] = 1084;  
					yyal[200] = 1085;  
					yyal[201] = 1085;  
					yyal[202] = 1104;  
					yyal[203] = 1113;  
					yyal[204] = 1122;  
					yyal[205] = 1131;  
					yyal[206] = 1140;  
					yyal[207] = 1140;  
					yyal[208] = 1160;  
					yyal[209] = 1160;  
					yyal[210] = 1180;  
					yyal[211] = 1181;  
					yyal[212] = 1201;  
					yyal[213] = 1201;  
					yyal[214] = 1201;  
					yyal[215] = 1210;  
					yyal[216] = 1210;  
					yyal[217] = 1210;  
					yyal[218] = 1211;  
					yyal[219] = 1212;  
					yyal[220] = 1230;  
					yyal[221] = 1231;  
					yyal[222] = 1240;  
					yyal[223] = 1240;  
					yyal[224] = 1240;  
					yyal[225] = 1240; 

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
					yyah[10] = 57;  
					yyah[11] = 58;  
					yyah[12] = 73;  
					yyah[13] = 88;  
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
					yyah[30] = 103;  
					yyah[31] = 104;  
					yyah[32] = 104;  
					yyah[33] = 104;  
					yyah[34] = 105;  
					yyah[35] = 105;  
					yyah[36] = 105;  
					yyah[37] = 111;  
					yyah[38] = 111;  
					yyah[39] = 111;  
					yyah[40] = 111;  
					yyah[41] = 111;  
					yyah[42] = 111;  
					yyah[43] = 112;  
					yyah[44] = 113;  
					yyah[45] = 115;  
					yyah[46] = 115;  
					yyah[47] = 115;  
					yyah[48] = 116;  
					yyah[49] = 118;  
					yyah[50] = 118;  
					yyah[51] = 121;  
					yyah[52] = 141;  
					yyah[53] = 156;  
					yyah[54] = 172;  
					yyah[55] = 188;  
					yyah[56] = 188;  
					yyah[57] = 206;  
					yyah[58] = 206;  
					yyah[59] = 224;  
					yyah[60] = 225;  
					yyah[61] = 245;  
					yyah[62] = 246;  
					yyah[63] = 246;  
					yyah[64] = 246;  
					yyah[65] = 246;  
					yyah[66] = 246;  
					yyah[67] = 246;  
					yyah[68] = 246;  
					yyah[69] = 246;  
					yyah[70] = 246;  
					yyah[71] = 246;  
					yyah[72] = 246;  
					yyah[73] = 266;  
					yyah[74] = 267;  
					yyah[75] = 268;  
					yyah[76] = 291;  
					yyah[77] = 310;  
					yyah[78] = 311;  
					yyah[79] = 312;  
					yyah[80] = 313;  
					yyah[81] = 313;  
					yyah[82] = 313;  
					yyah[83] = 315;  
					yyah[84] = 315;  
					yyah[85] = 316;  
					yyah[86] = 317;  
					yyah[87] = 358;  
					yyah[88] = 359;  
					yyah[89] = 359;  
					yyah[90] = 359;  
					yyah[91] = 359;  
					yyah[92] = 360;  
					yyah[93] = 380;  
					yyah[94] = 380;  
					yyah[95] = 400;  
					yyah[96] = 400;  
					yyah[97] = 420;  
					yyah[98] = 420;  
					yyah[99] = 422;  
					yyah[100] = 424;  
					yyah[101] = 424;  
					yyah[102] = 449;  
					yyah[103] = 467;  
					yyah[104] = 487;  
					yyah[105] = 496;  
					yyah[106] = 512;  
					yyah[107] = 512;  
					yyah[108] = 529;  
					yyah[109] = 529;  
					yyah[110] = 548;  
					yyah[111] = 549;  
					yyah[112] = 568;  
					yyah[113] = 588;  
					yyah[114] = 589;  
					yyah[115] = 608;  
					yyah[116] = 624;  
					yyah[117] = 624;  
					yyah[118] = 624;  
					yyah[119] = 624;  
					yyah[120] = 642;  
					yyah[121] = 642;  
					yyah[122] = 643;  
					yyah[123] = 643;  
					yyah[124] = 643;  
					yyah[125] = 643;  
					yyah[126] = 661;  
					yyah[127] = 680;  
					yyah[128] = 699;  
					yyah[129] = 718;  
					yyah[130] = 718;  
					yyah[131] = 718;  
					yyah[132] = 718;  
					yyah[133] = 718;  
					yyah[134] = 718;  
					yyah[135] = 718;  
					yyah[136] = 718;  
					yyah[137] = 737;  
					yyah[138] = 745;  
					yyah[139] = 745;  
					yyah[140] = 745;  
					yyah[141] = 745;  
					yyah[142] = 745;  
					yyah[143] = 745;  
					yyah[144] = 745;  
					yyah[145] = 745;  
					yyah[146] = 745;  
					yyah[147] = 765;  
					yyah[148] = 765;  
					yyah[149] = 765;  
					yyah[150] = 784;  
					yyah[151] = 784;  
					yyah[152] = 784;  
					yyah[153] = 784;  
					yyah[154] = 784;  
					yyah[155] = 784;  
					yyah[156] = 784;  
					yyah[157] = 785;  
					yyah[158] = 793;  
					yyah[159] = 810;  
					yyah[160] = 829;  
					yyah[161] = 849;  
					yyah[162] = 858;  
					yyah[163] = 859;  
					yyah[164] = 879;  
					yyah[165] = 888;  
					yyah[166] = 889;  
					yyah[167] = 891;  
					yyah[168] = 892;  
					yyah[169] = 892;  
					yyah[170] = 894;  
					yyah[171] = 894;  
					yyah[172] = 894;  
					yyah[173] = 894;  
					yyah[174] = 894;  
					yyah[175] = 903;  
					yyah[176] = 922;  
					yyah[177] = 922;  
					yyah[178] = 939;  
					yyah[179] = 958;  
					yyah[180] = 959;  
					yyah[181] = 978;  
					yyah[182] = 978;  
					yyah[183] = 978;  
					yyah[184] = 997;  
					yyah[185] = 997;  
					yyah[186] = 1016;  
					yyah[187] = 1016;  
					yyah[188] = 1017;  
					yyah[189] = 1018;  
					yyah[190] = 1027;  
					yyah[191] = 1027;  
					yyah[192] = 1028;  
					yyah[193] = 1037;  
					yyah[194] = 1037;  
					yyah[195] = 1055;  
					yyah[196] = 1056;  
					yyah[197] = 1074;  
					yyah[198] = 1083;  
					yyah[199] = 1084;  
					yyah[200] = 1084;  
					yyah[201] = 1103;  
					yyah[202] = 1112;  
					yyah[203] = 1121;  
					yyah[204] = 1130;  
					yyah[205] = 1139;  
					yyah[206] = 1139;  
					yyah[207] = 1159;  
					yyah[208] = 1159;  
					yyah[209] = 1179;  
					yyah[210] = 1180;  
					yyah[211] = 1200;  
					yyah[212] = 1200;  
					yyah[213] = 1200;  
					yyah[214] = 1209;  
					yyah[215] = 1209;  
					yyah[216] = 1209;  
					yyah[217] = 1210;  
					yyah[218] = 1211;  
					yyah[219] = 1229;  
					yyah[220] = 1230;  
					yyah[221] = 1239;  
					yyah[222] = 1239;  
					yyah[223] = 1239;  
					yyah[224] = 1239;  
					yyah[225] = 1239; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 12;  
					yygl[2] = 12;  
					yygl[3] = 12;  
					yygl[4] = 12;  
					yygl[5] = 12;  
					yygl[6] = 12;  
					yygl[7] = 21;  
					yygl[8] = 21;  
					yygl[9] = 21;  
					yygl[10] = 31;  
					yygl[11] = 31;  
					yygl[12] = 31;  
					yygl[13] = 33;  
					yygl[14] = 35;  
					yygl[15] = 37;  
					yygl[16] = 37;  
					yygl[17] = 37;  
					yygl[18] = 37;  
					yygl[19] = 37;  
					yygl[20] = 37;  
					yygl[21] = 37;  
					yygl[22] = 37;  
					yygl[23] = 37;  
					yygl[24] = 37;  
					yygl[25] = 37;  
					yygl[26] = 37;  
					yygl[27] = 37;  
					yygl[28] = 37;  
					yygl[29] = 37;  
					yygl[30] = 37;  
					yygl[31] = 37;  
					yygl[32] = 37;  
					yygl[33] = 37;  
					yygl[34] = 37;  
					yygl[35] = 37;  
					yygl[36] = 37;  
					yygl[37] = 37;  
					yygl[38] = 40;  
					yygl[39] = 40;  
					yygl[40] = 40;  
					yygl[41] = 40;  
					yygl[42] = 40;  
					yygl[43] = 40;  
					yygl[44] = 40;  
					yygl[45] = 40;  
					yygl[46] = 40;  
					yygl[47] = 40;  
					yygl[48] = 40;  
					yygl[49] = 40;  
					yygl[50] = 40;  
					yygl[51] = 40;  
					yygl[52] = 43;  
					yygl[53] = 59;  
					yygl[54] = 61;  
					yygl[55] = 69;  
					yygl[56] = 77;  
					yygl[57] = 77;  
					yygl[58] = 83;  
					yygl[59] = 83;  
					yygl[60] = 89;  
					yygl[61] = 89;  
					yygl[62] = 105;  
					yygl[63] = 105;  
					yygl[64] = 105;  
					yygl[65] = 105;  
					yygl[66] = 105;  
					yygl[67] = 105;  
					yygl[68] = 105;  
					yygl[69] = 105;  
					yygl[70] = 105;  
					yygl[71] = 105;  
					yygl[72] = 105;  
					yygl[73] = 105;  
					yygl[74] = 121;  
					yygl[75] = 121;  
					yygl[76] = 121;  
					yygl[77] = 131;  
					yygl[78] = 137;  
					yygl[79] = 138;  
					yygl[80] = 138;  
					yygl[81] = 139;  
					yygl[82] = 139;  
					yygl[83] = 139;  
					yygl[84] = 139;  
					yygl[85] = 139;  
					yygl[86] = 139;  
					yygl[87] = 139;  
					yygl[88] = 139;  
					yygl[89] = 139;  
					yygl[90] = 139;  
					yygl[91] = 139;  
					yygl[92] = 139;  
					yygl[93] = 139;  
					yygl[94] = 155;  
					yygl[95] = 155;  
					yygl[96] = 171;  
					yygl[97] = 171;  
					yygl[98] = 179;  
					yygl[99] = 179;  
					yygl[100] = 179;  
					yygl[101] = 179;  
					yygl[102] = 179;  
					yygl[103] = 180;  
					yygl[104] = 188;  
					yygl[105] = 204;  
					yygl[106] = 205;  
					yygl[107] = 208;  
					yygl[108] = 208;  
					yygl[109] = 210;  
					yygl[110] = 210;  
					yygl[111] = 217;  
					yygl[112] = 217;  
					yygl[113] = 223;  
					yygl[114] = 239;  
					yygl[115] = 239;  
					yygl[116] = 245;  
					yygl[117] = 252;  
					yygl[118] = 252;  
					yygl[119] = 252;  
					yygl[120] = 252;  
					yygl[121] = 258;  
					yygl[122] = 258;  
					yygl[123] = 258;  
					yygl[124] = 258;  
					yygl[125] = 258;  
					yygl[126] = 258;  
					yygl[127] = 266;  
					yygl[128] = 273;  
					yygl[129] = 280;  
					yygl[130] = 285;  
					yygl[131] = 285;  
					yygl[132] = 285;  
					yygl[133] = 285;  
					yygl[134] = 285;  
					yygl[135] = 285;  
					yygl[136] = 285;  
					yygl[137] = 285;  
					yygl[138] = 291;  
					yygl[139] = 292;  
					yygl[140] = 292;  
					yygl[141] = 292;  
					yygl[142] = 292;  
					yygl[143] = 292;  
					yygl[144] = 292;  
					yygl[145] = 292;  
					yygl[146] = 292;  
					yygl[147] = 292;  
					yygl[148] = 292;  
					yygl[149] = 292;  
					yygl[150] = 292;  
					yygl[151] = 297;  
					yygl[152] = 297;  
					yygl[153] = 297;  
					yygl[154] = 297;  
					yygl[155] = 297;  
					yygl[156] = 297;  
					yygl[157] = 297;  
					yygl[158] = 297;  
					yygl[159] = 298;  
					yygl[160] = 300;  
					yygl[161] = 307;  
					yygl[162] = 323;  
					yygl[163] = 324;  
					yygl[164] = 324;  
					yygl[165] = 340;  
					yygl[166] = 341;  
					yygl[167] = 341;  
					yygl[168] = 341;  
					yygl[169] = 341;  
					yygl[170] = 341;  
					yygl[171] = 341;  
					yygl[172] = 341;  
					yygl[173] = 341;  
					yygl[174] = 341;  
					yygl[175] = 341;  
					yygl[176] = 343;  
					yygl[177] = 348;  
					yygl[178] = 348;  
					yygl[179] = 350;  
					yygl[180] = 357;  
					yygl[181] = 358;  
					yygl[182] = 363;  
					yygl[183] = 363;  
					yygl[184] = 363;  
					yygl[185] = 368;  
					yygl[186] = 368;  
					yygl[187] = 374;  
					yygl[188] = 374;  
					yygl[189] = 374;  
					yygl[190] = 374;  
					yygl[191] = 375;  
					yygl[192] = 375;  
					yygl[193] = 375;  
					yygl[194] = 376;  
					yygl[195] = 376;  
					yygl[196] = 382;  
					yygl[197] = 382;  
					yygl[198] = 388;  
					yygl[199] = 390;  
					yygl[200] = 390;  
					yygl[201] = 390;  
					yygl[202] = 396;  
					yygl[203] = 398;  
					yygl[204] = 400;  
					yygl[205] = 400;  
					yygl[206] = 400;  
					yygl[207] = 400;  
					yygl[208] = 416;  
					yygl[209] = 416;  
					yygl[210] = 432;  
					yygl[211] = 432;  
					yygl[212] = 448;  
					yygl[213] = 448;  
					yygl[214] = 448;  
					yygl[215] = 449;  
					yygl[216] = 449;  
					yygl[217] = 449;  
					yygl[218] = 449;  
					yygl[219] = 449;  
					yygl[220] = 455;  
					yygl[221] = 455;  
					yygl[222] = 456;  
					yygl[223] = 456;  
					yygl[224] = 456;  
					yygl[225] = 456; 

					yygh = new int[yynstates];
					yygh[0] = 11;  
					yygh[1] = 11;  
					yygh[2] = 11;  
					yygh[3] = 11;  
					yygh[4] = 11;  
					yygh[5] = 11;  
					yygh[6] = 20;  
					yygh[7] = 20;  
					yygh[8] = 20;  
					yygh[9] = 30;  
					yygh[10] = 30;  
					yygh[11] = 30;  
					yygh[12] = 32;  
					yygh[13] = 34;  
					yygh[14] = 36;  
					yygh[15] = 36;  
					yygh[16] = 36;  
					yygh[17] = 36;  
					yygh[18] = 36;  
					yygh[19] = 36;  
					yygh[20] = 36;  
					yygh[21] = 36;  
					yygh[22] = 36;  
					yygh[23] = 36;  
					yygh[24] = 36;  
					yygh[25] = 36;  
					yygh[26] = 36;  
					yygh[27] = 36;  
					yygh[28] = 36;  
					yygh[29] = 36;  
					yygh[30] = 36;  
					yygh[31] = 36;  
					yygh[32] = 36;  
					yygh[33] = 36;  
					yygh[34] = 36;  
					yygh[35] = 36;  
					yygh[36] = 36;  
					yygh[37] = 39;  
					yygh[38] = 39;  
					yygh[39] = 39;  
					yygh[40] = 39;  
					yygh[41] = 39;  
					yygh[42] = 39;  
					yygh[43] = 39;  
					yygh[44] = 39;  
					yygh[45] = 39;  
					yygh[46] = 39;  
					yygh[47] = 39;  
					yygh[48] = 39;  
					yygh[49] = 39;  
					yygh[50] = 39;  
					yygh[51] = 42;  
					yygh[52] = 58;  
					yygh[53] = 60;  
					yygh[54] = 68;  
					yygh[55] = 76;  
					yygh[56] = 76;  
					yygh[57] = 82;  
					yygh[58] = 82;  
					yygh[59] = 88;  
					yygh[60] = 88;  
					yygh[61] = 104;  
					yygh[62] = 104;  
					yygh[63] = 104;  
					yygh[64] = 104;  
					yygh[65] = 104;  
					yygh[66] = 104;  
					yygh[67] = 104;  
					yygh[68] = 104;  
					yygh[69] = 104;  
					yygh[70] = 104;  
					yygh[71] = 104;  
					yygh[72] = 104;  
					yygh[73] = 120;  
					yygh[74] = 120;  
					yygh[75] = 120;  
					yygh[76] = 130;  
					yygh[77] = 136;  
					yygh[78] = 137;  
					yygh[79] = 137;  
					yygh[80] = 138;  
					yygh[81] = 138;  
					yygh[82] = 138;  
					yygh[83] = 138;  
					yygh[84] = 138;  
					yygh[85] = 138;  
					yygh[86] = 138;  
					yygh[87] = 138;  
					yygh[88] = 138;  
					yygh[89] = 138;  
					yygh[90] = 138;  
					yygh[91] = 138;  
					yygh[92] = 138;  
					yygh[93] = 154;  
					yygh[94] = 154;  
					yygh[95] = 170;  
					yygh[96] = 170;  
					yygh[97] = 178;  
					yygh[98] = 178;  
					yygh[99] = 178;  
					yygh[100] = 178;  
					yygh[101] = 178;  
					yygh[102] = 179;  
					yygh[103] = 187;  
					yygh[104] = 203;  
					yygh[105] = 204;  
					yygh[106] = 207;  
					yygh[107] = 207;  
					yygh[108] = 209;  
					yygh[109] = 209;  
					yygh[110] = 216;  
					yygh[111] = 216;  
					yygh[112] = 222;  
					yygh[113] = 238;  
					yygh[114] = 238;  
					yygh[115] = 244;  
					yygh[116] = 251;  
					yygh[117] = 251;  
					yygh[118] = 251;  
					yygh[119] = 251;  
					yygh[120] = 257;  
					yygh[121] = 257;  
					yygh[122] = 257;  
					yygh[123] = 257;  
					yygh[124] = 257;  
					yygh[125] = 257;  
					yygh[126] = 265;  
					yygh[127] = 272;  
					yygh[128] = 279;  
					yygh[129] = 284;  
					yygh[130] = 284;  
					yygh[131] = 284;  
					yygh[132] = 284;  
					yygh[133] = 284;  
					yygh[134] = 284;  
					yygh[135] = 284;  
					yygh[136] = 284;  
					yygh[137] = 290;  
					yygh[138] = 291;  
					yygh[139] = 291;  
					yygh[140] = 291;  
					yygh[141] = 291;  
					yygh[142] = 291;  
					yygh[143] = 291;  
					yygh[144] = 291;  
					yygh[145] = 291;  
					yygh[146] = 291;  
					yygh[147] = 291;  
					yygh[148] = 291;  
					yygh[149] = 291;  
					yygh[150] = 296;  
					yygh[151] = 296;  
					yygh[152] = 296;  
					yygh[153] = 296;  
					yygh[154] = 296;  
					yygh[155] = 296;  
					yygh[156] = 296;  
					yygh[157] = 296;  
					yygh[158] = 297;  
					yygh[159] = 299;  
					yygh[160] = 306;  
					yygh[161] = 322;  
					yygh[162] = 323;  
					yygh[163] = 323;  
					yygh[164] = 339;  
					yygh[165] = 340;  
					yygh[166] = 340;  
					yygh[167] = 340;  
					yygh[168] = 340;  
					yygh[169] = 340;  
					yygh[170] = 340;  
					yygh[171] = 340;  
					yygh[172] = 340;  
					yygh[173] = 340;  
					yygh[174] = 340;  
					yygh[175] = 342;  
					yygh[176] = 347;  
					yygh[177] = 347;  
					yygh[178] = 349;  
					yygh[179] = 356;  
					yygh[180] = 357;  
					yygh[181] = 362;  
					yygh[182] = 362;  
					yygh[183] = 362;  
					yygh[184] = 367;  
					yygh[185] = 367;  
					yygh[186] = 373;  
					yygh[187] = 373;  
					yygh[188] = 373;  
					yygh[189] = 373;  
					yygh[190] = 374;  
					yygh[191] = 374;  
					yygh[192] = 374;  
					yygh[193] = 375;  
					yygh[194] = 375;  
					yygh[195] = 381;  
					yygh[196] = 381;  
					yygh[197] = 387;  
					yygh[198] = 389;  
					yygh[199] = 389;  
					yygh[200] = 389;  
					yygh[201] = 395;  
					yygh[202] = 397;  
					yygh[203] = 399;  
					yygh[204] = 399;  
					yygh[205] = 399;  
					yygh[206] = 399;  
					yygh[207] = 415;  
					yygh[208] = 415;  
					yygh[209] = 431;  
					yygh[210] = 431;  
					yygh[211] = 447;  
					yygh[212] = 447;  
					yygh[213] = 447;  
					yygh[214] = 448;  
					yygh[215] = 448;  
					yygh[216] = 448;  
					yygh[217] = 448;  
					yygh[218] = 448;  
					yygh[219] = 454;  
					yygh[220] = 454;  
					yygh[221] = 455;  
					yygh[222] = 455;  
					yygh[223] = 455;  
					yygh[224] = 455;  
					yygh[225] = 455; 

					yyr[yyrc] = new YYRRec(1,-2);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(9,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(8,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
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

			if (Regex.IsMatch(Rest,"^(([0-9]*[.])?[0-9]+)")){
				Results.Add (t_number);
				ResultsV.Add(Regex.Match(Rest,"^(([0-9]*[.])?[0-9]+)").Value);}

			if (Regex.IsMatch(Rest,"^([A-Za-z0-9][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)")){
				Results.Add (t_identifier);
				ResultsV.Add(Regex.Match(Rest,"^([A-Za-z0-9][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)").Value);}

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
