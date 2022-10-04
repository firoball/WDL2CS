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
         //Output.WriteLine(yyval);
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   23 : 
         yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   24 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   25 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   26 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   27 : 
         yyval = "";
         
       break;
							case   28 : 
         yyval = yyv[yysp-0];
         
       break;
							case   29 : 
         yyval = yyv[yysp-0];
         
       break;
							case   30 : 
         yyval = yyv[yysp-0];
         
       break;
							case   31 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   32 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   33 : 
         yyval = yyv[yysp-0];
         
       break;
							case   34 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = "";
         
       break;
							case   41 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   42 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   43 : 
         yyval = yyv[yysp-0];
         
       break;
							case   44 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   45 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   46 : 
         yyval = yyv[yysp-0];
         
       break;
							case   47 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   48 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   49 : 
         yyval = yyv[yysp-0];
         
       break;
							case   50 : 
         yyval = yyv[yysp-0];
         
       break;
							case   51 : 
         yyval = yyv[yysp-0];
         
       break;
							case   52 : 
         yyval = yyv[yysp-0];
         
       break;
							case   53 : 
         yyval = yyv[yysp-0];
         
       break;
							case   54 : 
         yyval = yyv[yysp-0];
         
       break;
							case   55 : 
         yyval = yyv[yysp-0];
         
       break;
							case   56 : 
         yyval = yyv[yysp-0];
         
       break;
							case   57 : 
         yyval = yyv[yysp-0];
         
       break;
							case   58 : 
         yyval = yyv[yysp-0];
         
       break;
							case   59 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   65 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   79 : 
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   80 : 
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   81 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   82 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   83 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   88 : 
         yyval = yyv[yysp-0];
         
       break;
							case   89 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   90 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   98 : 
         yyval = yyv[yysp-0];
         
       break;
							case   99 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  100 : 
         yyval = yyv[yysp-0];
         
       break;
							case  101 : 
         yyval = yyv[yysp-0];
         
       break;
							case  102 : 
         yyval = "";
         
       break;
							case  103 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  109 : 
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

					int yynacts   = 1124;
					int yyngotos  = 320;
					int yynstates = 193;
					int yynrules  = 109;
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
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
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
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
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
					yya[yyac] = new YYARec(308,47);yyac++; 
					yya[yyac] = new YYARec(258,48);yyac++; 
					yya[yyac] = new YYARec(258,49);yyac++; 
					yya[yyac] = new YYARec(263,58);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
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
					yya[yyac] = new YYARec(307,59);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(264,-40 );yyac++; 
					yya[yyac] = new YYARec(258,-100 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(258,60);yyac++; 
					yya[yyac] = new YYARec(258,61);yyac++; 
					yya[yyac] = new YYARec(258,62);yyac++; 
					yya[yyac] = new YYARec(263,63);yyac++; 
					yya[yyac] = new YYARec(263,65);yyac++; 
					yya[yyac] = new YYARec(258,-33 );yyac++; 
					yya[yyac] = new YYARec(275,-40 );yyac++; 
					yya[yyac] = new YYARec(276,-40 );yyac++; 
					yya[yyac] = new YYARec(282,-40 );yyac++; 
					yya[yyac] = new YYARec(283,-40 );yyac++; 
					yya[yyac] = new YYARec(284,-40 );yyac++; 
					yya[yyac] = new YYARec(285,-40 );yyac++; 
					yya[yyac] = new YYARec(286,-40 );yyac++; 
					yya[yyac] = new YYARec(287,-40 );yyac++; 
					yya[yyac] = new YYARec(288,-40 );yyac++; 
					yya[yyac] = new YYARec(289,-40 );yyac++; 
					yya[yyac] = new YYARec(290,-40 );yyac++; 
					yya[yyac] = new YYARec(291,-40 );yyac++; 
					yya[yyac] = new YYARec(292,-40 );yyac++; 
					yya[yyac] = new YYARec(293,-40 );yyac++; 
					yya[yyac] = new YYARec(294,-40 );yyac++; 
					yya[yyac] = new YYARec(295,-40 );yyac++; 
					yya[yyac] = new YYARec(308,-40 );yyac++; 
					yya[yyac] = new YYARec(309,-40 );yyac++; 
					yya[yyac] = new YYARec(310,-40 );yyac++; 
					yya[yyac] = new YYARec(311,-40 );yyac++; 
					yya[yyac] = new YYARec(264,66);yyac++; 
					yya[yyac] = new YYARec(307,59);yyac++; 
					yya[yyac] = new YYARec(258,-100 );yyac++; 
					yya[yyac] = new YYARec(263,-100 );yyac++; 
					yya[yyac] = new YYARec(268,-100 );yyac++; 
					yya[yyac] = new YYARec(269,-100 );yyac++; 
					yya[yyac] = new YYARec(270,-100 );yyac++; 
					yya[yyac] = new YYARec(271,-100 );yyac++; 
					yya[yyac] = new YYARec(272,-100 );yyac++; 
					yya[yyac] = new YYARec(273,-100 );yyac++; 
					yya[yyac] = new YYARec(274,-100 );yyac++; 
					yya[yyac] = new YYARec(275,-100 );yyac++; 
					yya[yyac] = new YYARec(276,-100 );yyac++; 
					yya[yyac] = new YYARec(277,-100 );yyac++; 
					yya[yyac] = new YYARec(278,-100 );yyac++; 
					yya[yyac] = new YYARec(279,-100 );yyac++; 
					yya[yyac] = new YYARec(280,-100 );yyac++; 
					yya[yyac] = new YYARec(281,-100 );yyac++; 
					yya[yyac] = new YYARec(282,-100 );yyac++; 
					yya[yyac] = new YYARec(283,-100 );yyac++; 
					yya[yyac] = new YYARec(284,-100 );yyac++; 
					yya[yyac] = new YYARec(285,-100 );yyac++; 
					yya[yyac] = new YYARec(286,-100 );yyac++; 
					yya[yyac] = new YYARec(287,-100 );yyac++; 
					yya[yyac] = new YYARec(288,-100 );yyac++; 
					yya[yyac] = new YYARec(289,-100 );yyac++; 
					yya[yyac] = new YYARec(290,-100 );yyac++; 
					yya[yyac] = new YYARec(291,-100 );yyac++; 
					yya[yyac] = new YYARec(292,-100 );yyac++; 
					yya[yyac] = new YYARec(293,-100 );yyac++; 
					yya[yyac] = new YYARec(294,-100 );yyac++; 
					yya[yyac] = new YYARec(295,-100 );yyac++; 
					yya[yyac] = new YYARec(299,-100 );yyac++; 
					yya[yyac] = new YYARec(300,-100 );yyac++; 
					yya[yyac] = new YYARec(301,-100 );yyac++; 
					yya[yyac] = new YYARec(302,-100 );yyac++; 
					yya[yyac] = new YYARec(303,-100 );yyac++; 
					yya[yyac] = new YYARec(304,-100 );yyac++; 
					yya[yyac] = new YYARec(305,-100 );yyac++; 
					yya[yyac] = new YYARec(306,-100 );yyac++; 
					yya[yyac] = new YYARec(308,-100 );yyac++; 
					yya[yyac] = new YYARec(309,-100 );yyac++; 
					yya[yyac] = new YYARec(310,-100 );yyac++; 
					yya[yyac] = new YYARec(311,-100 );yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
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
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(264,-39 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
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
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
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
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
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
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(267,82);yyac++; 
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
					yya[yyac] = new YYARec(296,83);yyac++; 
					yya[yyac] = new YYARec(297,84);yyac++; 
					yya[yyac] = new YYARec(298,85);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-27 );yyac++; 
					yya[yyac] = new YYARec(260,86);yyac++; 
					yya[yyac] = new YYARec(261,87);yyac++; 
					yya[yyac] = new YYARec(258,88);yyac++; 
					yya[yyac] = new YYARec(258,89);yyac++; 
					yya[yyac] = new YYARec(264,90);yyac++; 
					yya[yyac] = new YYARec(267,82);yyac++; 
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
					yya[yyac] = new YYARec(296,83);yyac++; 
					yya[yyac] = new YYARec(297,84);yyac++; 
					yya[yyac] = new YYARec(298,85);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-27 );yyac++; 
					yya[yyac] = new YYARec(258,92);yyac++; 
					yya[yyac] = new YYARec(265,93);yyac++; 
					yya[yyac] = new YYARec(263,95);yyac++; 
					yya[yyac] = new YYARec(266,96);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
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
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(258,-30 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(267,102);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-102 );yyac++; 
					yya[yyac] = new YYARec(283,-102 );yyac++; 
					yya[yyac] = new YYARec(284,-102 );yyac++; 
					yya[yyac] = new YYARec(285,-102 );yyac++; 
					yya[yyac] = new YYARec(286,-102 );yyac++; 
					yya[yyac] = new YYARec(287,-102 );yyac++; 
					yya[yyac] = new YYARec(288,-102 );yyac++; 
					yya[yyac] = new YYARec(289,-102 );yyac++; 
					yya[yyac] = new YYARec(290,-102 );yyac++; 
					yya[yyac] = new YYARec(291,-102 );yyac++; 
					yya[yyac] = new YYARec(292,-102 );yyac++; 
					yya[yyac] = new YYARec(293,-102 );yyac++; 
					yya[yyac] = new YYARec(294,-102 );yyac++; 
					yya[yyac] = new YYARec(295,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(309,-102 );yyac++; 
					yya[yyac] = new YYARec(267,104);yyac++; 
					yya[yyac] = new YYARec(264,105);yyac++; 
					yya[yyac] = new YYARec(267,107);yyac++; 
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
					yya[yyac] = new YYARec(267,82);yyac++; 
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
					yya[yyac] = new YYARec(296,83);yyac++; 
					yya[yyac] = new YYARec(297,84);yyac++; 
					yya[yyac] = new YYARec(298,85);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-27 );yyac++; 
					yya[yyac] = new YYARec(267,82);yyac++; 
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
					yya[yyac] = new YYARec(296,83);yyac++; 
					yya[yyac] = new YYARec(297,84);yyac++; 
					yya[yyac] = new YYARec(298,85);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-27 );yyac++; 
					yya[yyac] = new YYARec(277,112);yyac++; 
					yya[yyac] = new YYARec(278,113);yyac++; 
					yya[yyac] = new YYARec(279,114);yyac++; 
					yya[yyac] = new YYARec(280,115);yyac++; 
					yya[yyac] = new YYARec(281,116);yyac++; 
					yya[yyac] = new YYARec(258,-38 );yyac++; 
					yya[yyac] = new YYARec(263,-38 );yyac++; 
					yya[yyac] = new YYARec(275,-38 );yyac++; 
					yya[yyac] = new YYARec(276,-38 );yyac++; 
					yya[yyac] = new YYARec(282,-38 );yyac++; 
					yya[yyac] = new YYARec(283,-38 );yyac++; 
					yya[yyac] = new YYARec(284,-38 );yyac++; 
					yya[yyac] = new YYARec(285,-38 );yyac++; 
					yya[yyac] = new YYARec(286,-38 );yyac++; 
					yya[yyac] = new YYARec(287,-38 );yyac++; 
					yya[yyac] = new YYARec(288,-38 );yyac++; 
					yya[yyac] = new YYARec(289,-38 );yyac++; 
					yya[yyac] = new YYARec(290,-38 );yyac++; 
					yya[yyac] = new YYARec(291,-38 );yyac++; 
					yya[yyac] = new YYARec(292,-38 );yyac++; 
					yya[yyac] = new YYARec(293,-38 );yyac++; 
					yya[yyac] = new YYARec(294,-38 );yyac++; 
					yya[yyac] = new YYARec(295,-38 );yyac++; 
					yya[yyac] = new YYARec(308,-38 );yyac++; 
					yya[yyac] = new YYARec(309,-38 );yyac++; 
					yya[yyac] = new YYARec(310,-38 );yyac++; 
					yya[yyac] = new YYARec(311,-38 );yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
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
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(267,82);yyac++; 
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
					yya[yyac] = new YYARec(296,83);yyac++; 
					yya[yyac] = new YYARec(297,84);yyac++; 
					yya[yyac] = new YYARec(298,85);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-27 );yyac++; 
					yya[yyac] = new YYARec(268,119);yyac++; 
					yya[yyac] = new YYARec(299,120);yyac++; 
					yya[yyac] = new YYARec(300,121);yyac++; 
					yya[yyac] = new YYARec(301,122);yyac++; 
					yya[yyac] = new YYARec(302,123);yyac++; 
					yya[yyac] = new YYARec(303,124);yyac++; 
					yya[yyac] = new YYARec(304,125);yyac++; 
					yya[yyac] = new YYARec(305,126);yyac++; 
					yya[yyac] = new YYARec(306,127);yyac++; 
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
					yya[yyac] = new YYARec(308,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(269,132);yyac++; 
					yya[yyac] = new YYARec(270,133);yyac++; 
					yya[yyac] = new YYARec(271,134);yyac++; 
					yya[yyac] = new YYARec(272,135);yyac++; 
					yya[yyac] = new YYARec(273,136);yyac++; 
					yya[yyac] = new YYARec(274,137);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(268,-85 );yyac++; 
					yya[yyac] = new YYARec(299,-85 );yyac++; 
					yya[yyac] = new YYARec(300,-85 );yyac++; 
					yya[yyac] = new YYARec(301,-85 );yyac++; 
					yya[yyac] = new YYARec(302,-85 );yyac++; 
					yya[yyac] = new YYARec(303,-85 );yyac++; 
					yya[yyac] = new YYARec(304,-85 );yyac++; 
					yya[yyac] = new YYARec(305,-85 );yyac++; 
					yya[yyac] = new YYARec(306,-85 );yyac++; 
					yya[yyac] = new YYARec(267,141);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-102 );yyac++; 
					yya[yyac] = new YYARec(283,-102 );yyac++; 
					yya[yyac] = new YYARec(284,-102 );yyac++; 
					yya[yyac] = new YYARec(285,-102 );yyac++; 
					yya[yyac] = new YYARec(286,-102 );yyac++; 
					yya[yyac] = new YYARec(287,-102 );yyac++; 
					yya[yyac] = new YYARec(288,-102 );yyac++; 
					yya[yyac] = new YYARec(289,-102 );yyac++; 
					yya[yyac] = new YYARec(290,-102 );yyac++; 
					yya[yyac] = new YYARec(291,-102 );yyac++; 
					yya[yyac] = new YYARec(292,-102 );yyac++; 
					yya[yyac] = new YYARec(293,-102 );yyac++; 
					yya[yyac] = new YYARec(294,-102 );yyac++; 
					yya[yyac] = new YYARec(295,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(309,-102 );yyac++; 
					yya[yyac] = new YYARec(264,142);yyac++; 
					yya[yyac] = new YYARec(267,102);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-102 );yyac++; 
					yya[yyac] = new YYARec(283,-102 );yyac++; 
					yya[yyac] = new YYARec(284,-102 );yyac++; 
					yya[yyac] = new YYARec(285,-102 );yyac++; 
					yya[yyac] = new YYARec(286,-102 );yyac++; 
					yya[yyac] = new YYARec(287,-102 );yyac++; 
					yya[yyac] = new YYARec(288,-102 );yyac++; 
					yya[yyac] = new YYARec(289,-102 );yyac++; 
					yya[yyac] = new YYARec(290,-102 );yyac++; 
					yya[yyac] = new YYARec(291,-102 );yyac++; 
					yya[yyac] = new YYARec(292,-102 );yyac++; 
					yya[yyac] = new YYARec(293,-102 );yyac++; 
					yya[yyac] = new YYARec(294,-102 );yyac++; 
					yya[yyac] = new YYARec(295,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(309,-102 );yyac++; 
					yya[yyac] = new YYARec(267,82);yyac++; 
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
					yya[yyac] = new YYARec(296,83);yyac++; 
					yya[yyac] = new YYARec(297,84);yyac++; 
					yya[yyac] = new YYARec(298,85);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-27 );yyac++; 
					yya[yyac] = new YYARec(264,145);yyac++; 
					yya[yyac] = new YYARec(267,102);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-102 );yyac++; 
					yya[yyac] = new YYARec(283,-102 );yyac++; 
					yya[yyac] = new YYARec(284,-102 );yyac++; 
					yya[yyac] = new YYARec(285,-102 );yyac++; 
					yya[yyac] = new YYARec(286,-102 );yyac++; 
					yya[yyac] = new YYARec(287,-102 );yyac++; 
					yya[yyac] = new YYARec(288,-102 );yyac++; 
					yya[yyac] = new YYARec(289,-102 );yyac++; 
					yya[yyac] = new YYARec(290,-102 );yyac++; 
					yya[yyac] = new YYARec(291,-102 );yyac++; 
					yya[yyac] = new YYARec(292,-102 );yyac++; 
					yya[yyac] = new YYARec(293,-102 );yyac++; 
					yya[yyac] = new YYARec(294,-102 );yyac++; 
					yya[yyac] = new YYARec(295,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(309,-102 );yyac++; 
					yya[yyac] = new YYARec(261,147);yyac++; 
					yya[yyac] = new YYARec(265,148);yyac++; 
					yya[yyac] = new YYARec(267,150);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-102 );yyac++; 
					yya[yyac] = new YYARec(283,-102 );yyac++; 
					yya[yyac] = new YYARec(284,-102 );yyac++; 
					yya[yyac] = new YYARec(285,-102 );yyac++; 
					yya[yyac] = new YYARec(286,-102 );yyac++; 
					yya[yyac] = new YYARec(287,-102 );yyac++; 
					yya[yyac] = new YYARec(288,-102 );yyac++; 
					yya[yyac] = new YYARec(289,-102 );yyac++; 
					yya[yyac] = new YYARec(290,-102 );yyac++; 
					yya[yyac] = new YYARec(291,-102 );yyac++; 
					yya[yyac] = new YYARec(292,-102 );yyac++; 
					yya[yyac] = new YYARec(293,-102 );yyac++; 
					yya[yyac] = new YYARec(294,-102 );yyac++; 
					yya[yyac] = new YYARec(295,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(309,-102 );yyac++; 
					yya[yyac] = new YYARec(267,153);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-102 );yyac++; 
					yya[yyac] = new YYARec(283,-102 );yyac++; 
					yya[yyac] = new YYARec(284,-102 );yyac++; 
					yya[yyac] = new YYARec(285,-102 );yyac++; 
					yya[yyac] = new YYARec(286,-102 );yyac++; 
					yya[yyac] = new YYARec(287,-102 );yyac++; 
					yya[yyac] = new YYARec(288,-102 );yyac++; 
					yya[yyac] = new YYARec(289,-102 );yyac++; 
					yya[yyac] = new YYARec(290,-102 );yyac++; 
					yya[yyac] = new YYARec(291,-102 );yyac++; 
					yya[yyac] = new YYARec(292,-102 );yyac++; 
					yya[yyac] = new YYARec(293,-102 );yyac++; 
					yya[yyac] = new YYARec(294,-102 );yyac++; 
					yya[yyac] = new YYARec(295,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(309,-102 );yyac++; 
					yya[yyac] = new YYARec(299,120);yyac++; 
					yya[yyac] = new YYARec(300,121);yyac++; 
					yya[yyac] = new YYARec(301,122);yyac++; 
					yya[yyac] = new YYARec(302,123);yyac++; 
					yya[yyac] = new YYARec(303,124);yyac++; 
					yya[yyac] = new YYARec(304,125);yyac++; 
					yya[yyac] = new YYARec(305,126);yyac++; 
					yya[yyac] = new YYARec(306,127);yyac++; 
					yya[yyac] = new YYARec(267,155);yyac++; 
					yya[yyac] = new YYARec(258,-105 );yyac++; 
					yya[yyac] = new YYARec(268,-105 );yyac++; 
					yya[yyac] = new YYARec(269,-105 );yyac++; 
					yya[yyac] = new YYARec(270,-105 );yyac++; 
					yya[yyac] = new YYARec(271,-105 );yyac++; 
					yya[yyac] = new YYARec(272,-105 );yyac++; 
					yya[yyac] = new YYARec(273,-105 );yyac++; 
					yya[yyac] = new YYARec(274,-105 );yyac++; 
					yya[yyac] = new YYARec(275,-105 );yyac++; 
					yya[yyac] = new YYARec(276,-105 );yyac++; 
					yya[yyac] = new YYARec(299,-105 );yyac++; 
					yya[yyac] = new YYARec(300,-105 );yyac++; 
					yya[yyac] = new YYARec(301,-105 );yyac++; 
					yya[yyac] = new YYARec(302,-105 );yyac++; 
					yya[yyac] = new YYARec(303,-105 );yyac++; 
					yya[yyac] = new YYARec(304,-105 );yyac++; 
					yya[yyac] = new YYARec(305,-105 );yyac++; 
					yya[yyac] = new YYARec(306,-105 );yyac++; 
					yya[yyac] = new YYARec(307,-105 );yyac++; 
					yya[yyac] = new YYARec(267,158);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-102 );yyac++; 
					yya[yyac] = new YYARec(283,-102 );yyac++; 
					yya[yyac] = new YYARec(284,-102 );yyac++; 
					yya[yyac] = new YYARec(285,-102 );yyac++; 
					yya[yyac] = new YYARec(286,-102 );yyac++; 
					yya[yyac] = new YYARec(287,-102 );yyac++; 
					yya[yyac] = new YYARec(288,-102 );yyac++; 
					yya[yyac] = new YYARec(289,-102 );yyac++; 
					yya[yyac] = new YYARec(290,-102 );yyac++; 
					yya[yyac] = new YYARec(291,-102 );yyac++; 
					yya[yyac] = new YYARec(292,-102 );yyac++; 
					yya[yyac] = new YYARec(293,-102 );yyac++; 
					yya[yyac] = new YYARec(294,-102 );yyac++; 
					yya[yyac] = new YYARec(295,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(309,-102 );yyac++; 
					yya[yyac] = new YYARec(268,159);yyac++; 
					yya[yyac] = new YYARec(299,120);yyac++; 
					yya[yyac] = new YYARec(300,121);yyac++; 
					yya[yyac] = new YYARec(301,122);yyac++; 
					yya[yyac] = new YYARec(302,123);yyac++; 
					yya[yyac] = new YYARec(303,124);yyac++; 
					yya[yyac] = new YYARec(304,125);yyac++; 
					yya[yyac] = new YYARec(305,126);yyac++; 
					yya[yyac] = new YYARec(306,127);yyac++; 
					yya[yyac] = new YYARec(268,161);yyac++; 
					yya[yyac] = new YYARec(269,132);yyac++; 
					yya[yyac] = new YYARec(270,133);yyac++; 
					yya[yyac] = new YYARec(271,134);yyac++; 
					yya[yyac] = new YYARec(272,135);yyac++; 
					yya[yyac] = new YYARec(273,136);yyac++; 
					yya[yyac] = new YYARec(274,137);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(299,-85 );yyac++; 
					yya[yyac] = new YYARec(300,-85 );yyac++; 
					yya[yyac] = new YYARec(301,-85 );yyac++; 
					yya[yyac] = new YYARec(302,-85 );yyac++; 
					yya[yyac] = new YYARec(303,-85 );yyac++; 
					yya[yyac] = new YYARec(304,-85 );yyac++; 
					yya[yyac] = new YYARec(305,-85 );yyac++; 
					yya[yyac] = new YYARec(306,-85 );yyac++; 
					yya[yyac] = new YYARec(267,141);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-102 );yyac++; 
					yya[yyac] = new YYARec(283,-102 );yyac++; 
					yya[yyac] = new YYARec(284,-102 );yyac++; 
					yya[yyac] = new YYARec(285,-102 );yyac++; 
					yya[yyac] = new YYARec(286,-102 );yyac++; 
					yya[yyac] = new YYARec(287,-102 );yyac++; 
					yya[yyac] = new YYARec(288,-102 );yyac++; 
					yya[yyac] = new YYARec(289,-102 );yyac++; 
					yya[yyac] = new YYARec(290,-102 );yyac++; 
					yya[yyac] = new YYARec(291,-102 );yyac++; 
					yya[yyac] = new YYARec(292,-102 );yyac++; 
					yya[yyac] = new YYARec(293,-102 );yyac++; 
					yya[yyac] = new YYARec(294,-102 );yyac++; 
					yya[yyac] = new YYARec(295,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(309,-102 );yyac++; 
					yya[yyac] = new YYARec(267,82);yyac++; 
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
					yya[yyac] = new YYARec(296,83);yyac++; 
					yya[yyac] = new YYARec(297,84);yyac++; 
					yya[yyac] = new YYARec(298,85);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-27 );yyac++; 
					yya[yyac] = new YYARec(268,164);yyac++; 
					yya[yyac] = new YYARec(299,120);yyac++; 
					yya[yyac] = new YYARec(300,121);yyac++; 
					yya[yyac] = new YYARec(301,122);yyac++; 
					yya[yyac] = new YYARec(302,123);yyac++; 
					yya[yyac] = new YYARec(303,124);yyac++; 
					yya[yyac] = new YYARec(304,125);yyac++; 
					yya[yyac] = new YYARec(305,126);yyac++; 
					yya[yyac] = new YYARec(306,127);yyac++; 
					yya[yyac] = new YYARec(265,165);yyac++; 
					yya[yyac] = new YYARec(267,82);yyac++; 
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
					yya[yyac] = new YYARec(296,83);yyac++; 
					yya[yyac] = new YYARec(297,84);yyac++; 
					yya[yyac] = new YYARec(298,85);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-27 );yyac++; 
					yya[yyac] = new YYARec(268,167);yyac++; 
					yya[yyac] = new YYARec(299,120);yyac++; 
					yya[yyac] = new YYARec(300,121);yyac++; 
					yya[yyac] = new YYARec(301,122);yyac++; 
					yya[yyac] = new YYARec(302,123);yyac++; 
					yya[yyac] = new YYARec(303,124);yyac++; 
					yya[yyac] = new YYARec(304,125);yyac++; 
					yya[yyac] = new YYARec(305,126);yyac++; 
					yya[yyac] = new YYARec(306,127);yyac++; 
					yya[yyac] = new YYARec(297,168);yyac++; 
					yya[yyac] = new YYARec(269,132);yyac++; 
					yya[yyac] = new YYARec(270,133);yyac++; 
					yya[yyac] = new YYARec(271,134);yyac++; 
					yya[yyac] = new YYARec(272,135);yyac++; 
					yya[yyac] = new YYARec(273,136);yyac++; 
					yya[yyac] = new YYARec(274,137);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(258,-41 );yyac++; 
					yya[yyac] = new YYARec(267,150);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-102 );yyac++; 
					yya[yyac] = new YYARec(283,-102 );yyac++; 
					yya[yyac] = new YYARec(284,-102 );yyac++; 
					yya[yyac] = new YYARec(285,-102 );yyac++; 
					yya[yyac] = new YYARec(286,-102 );yyac++; 
					yya[yyac] = new YYARec(287,-102 );yyac++; 
					yya[yyac] = new YYARec(288,-102 );yyac++; 
					yya[yyac] = new YYARec(289,-102 );yyac++; 
					yya[yyac] = new YYARec(290,-102 );yyac++; 
					yya[yyac] = new YYARec(291,-102 );yyac++; 
					yya[yyac] = new YYARec(292,-102 );yyac++; 
					yya[yyac] = new YYARec(293,-102 );yyac++; 
					yya[yyac] = new YYARec(294,-102 );yyac++; 
					yya[yyac] = new YYARec(295,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(309,-102 );yyac++; 
					yya[yyac] = new YYARec(269,132);yyac++; 
					yya[yyac] = new YYARec(270,133);yyac++; 
					yya[yyac] = new YYARec(271,134);yyac++; 
					yya[yyac] = new YYARec(272,135);yyac++; 
					yya[yyac] = new YYARec(273,136);yyac++; 
					yya[yyac] = new YYARec(274,137);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(268,-88 );yyac++; 
					yya[yyac] = new YYARec(299,-88 );yyac++; 
					yya[yyac] = new YYARec(300,-88 );yyac++; 
					yya[yyac] = new YYARec(301,-88 );yyac++; 
					yya[yyac] = new YYARec(302,-88 );yyac++; 
					yya[yyac] = new YYARec(303,-88 );yyac++; 
					yya[yyac] = new YYARec(304,-88 );yyac++; 
					yya[yyac] = new YYARec(305,-88 );yyac++; 
					yya[yyac] = new YYARec(306,-88 );yyac++; 
					yya[yyac] = new YYARec(267,141);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-102 );yyac++; 
					yya[yyac] = new YYARec(283,-102 );yyac++; 
					yya[yyac] = new YYARec(284,-102 );yyac++; 
					yya[yyac] = new YYARec(285,-102 );yyac++; 
					yya[yyac] = new YYARec(286,-102 );yyac++; 
					yya[yyac] = new YYARec(287,-102 );yyac++; 
					yya[yyac] = new YYARec(288,-102 );yyac++; 
					yya[yyac] = new YYARec(289,-102 );yyac++; 
					yya[yyac] = new YYARec(290,-102 );yyac++; 
					yya[yyac] = new YYARec(291,-102 );yyac++; 
					yya[yyac] = new YYARec(292,-102 );yyac++; 
					yya[yyac] = new YYARec(293,-102 );yyac++; 
					yya[yyac] = new YYARec(294,-102 );yyac++; 
					yya[yyac] = new YYARec(295,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(309,-102 );yyac++; 
					yya[yyac] = new YYARec(267,172);yyac++; 
					yya[yyac] = new YYARec(267,150);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-102 );yyac++; 
					yya[yyac] = new YYARec(283,-102 );yyac++; 
					yya[yyac] = new YYARec(284,-102 );yyac++; 
					yya[yyac] = new YYARec(285,-102 );yyac++; 
					yya[yyac] = new YYARec(286,-102 );yyac++; 
					yya[yyac] = new YYARec(287,-102 );yyac++; 
					yya[yyac] = new YYARec(288,-102 );yyac++; 
					yya[yyac] = new YYARec(289,-102 );yyac++; 
					yya[yyac] = new YYARec(290,-102 );yyac++; 
					yya[yyac] = new YYARec(291,-102 );yyac++; 
					yya[yyac] = new YYARec(292,-102 );yyac++; 
					yya[yyac] = new YYARec(293,-102 );yyac++; 
					yya[yyac] = new YYARec(294,-102 );yyac++; 
					yya[yyac] = new YYARec(295,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(309,-102 );yyac++; 
					yya[yyac] = new YYARec(267,150);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-102 );yyac++; 
					yya[yyac] = new YYARec(283,-102 );yyac++; 
					yya[yyac] = new YYARec(284,-102 );yyac++; 
					yya[yyac] = new YYARec(285,-102 );yyac++; 
					yya[yyac] = new YYARec(286,-102 );yyac++; 
					yya[yyac] = new YYARec(287,-102 );yyac++; 
					yya[yyac] = new YYARec(288,-102 );yyac++; 
					yya[yyac] = new YYARec(289,-102 );yyac++; 
					yya[yyac] = new YYARec(290,-102 );yyac++; 
					yya[yyac] = new YYARec(291,-102 );yyac++; 
					yya[yyac] = new YYARec(292,-102 );yyac++; 
					yya[yyac] = new YYARec(293,-102 );yyac++; 
					yya[yyac] = new YYARec(294,-102 );yyac++; 
					yya[yyac] = new YYARec(295,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(309,-102 );yyac++; 
					yya[yyac] = new YYARec(267,153);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-102 );yyac++; 
					yya[yyac] = new YYARec(283,-102 );yyac++; 
					yya[yyac] = new YYARec(284,-102 );yyac++; 
					yya[yyac] = new YYARec(285,-102 );yyac++; 
					yya[yyac] = new YYARec(286,-102 );yyac++; 
					yya[yyac] = new YYARec(287,-102 );yyac++; 
					yya[yyac] = new YYARec(288,-102 );yyac++; 
					yya[yyac] = new YYARec(289,-102 );yyac++; 
					yya[yyac] = new YYARec(290,-102 );yyac++; 
					yya[yyac] = new YYARec(291,-102 );yyac++; 
					yya[yyac] = new YYARec(292,-102 );yyac++; 
					yya[yyac] = new YYARec(293,-102 );yyac++; 
					yya[yyac] = new YYARec(294,-102 );yyac++; 
					yya[yyac] = new YYARec(295,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(309,-102 );yyac++; 
					yya[yyac] = new YYARec(268,176);yyac++; 
					yya[yyac] = new YYARec(265,177);yyac++; 
					yya[yyac] = new YYARec(264,178);yyac++; 
					yya[yyac] = new YYARec(299,120);yyac++; 
					yya[yyac] = new YYARec(300,121);yyac++; 
					yya[yyac] = new YYARec(301,122);yyac++; 
					yya[yyac] = new YYARec(302,123);yyac++; 
					yya[yyac] = new YYARec(303,124);yyac++; 
					yya[yyac] = new YYARec(304,125);yyac++; 
					yya[yyac] = new YYARec(305,126);yyac++; 
					yya[yyac] = new YYARec(306,127);yyac++; 
					yya[yyac] = new YYARec(265,179);yyac++; 
					yya[yyac] = new YYARec(264,180);yyac++; 
					yya[yyac] = new YYARec(299,120);yyac++; 
					yya[yyac] = new YYARec(300,121);yyac++; 
					yya[yyac] = new YYARec(301,122);yyac++; 
					yya[yyac] = new YYARec(302,123);yyac++; 
					yya[yyac] = new YYARec(303,124);yyac++; 
					yya[yyac] = new YYARec(304,125);yyac++; 
					yya[yyac] = new YYARec(305,126);yyac++; 
					yya[yyac] = new YYARec(306,127);yyac++; 
					yya[yyac] = new YYARec(264,181);yyac++; 
					yya[yyac] = new YYARec(268,161);yyac++; 
					yya[yyac] = new YYARec(269,132);yyac++; 
					yya[yyac] = new YYARec(270,133);yyac++; 
					yya[yyac] = new YYARec(271,134);yyac++; 
					yya[yyac] = new YYARec(272,135);yyac++; 
					yya[yyac] = new YYARec(273,136);yyac++; 
					yya[yyac] = new YYARec(274,137);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(268,182);yyac++; 
					yya[yyac] = new YYARec(267,102);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-102 );yyac++; 
					yya[yyac] = new YYARec(283,-102 );yyac++; 
					yya[yyac] = new YYARec(284,-102 );yyac++; 
					yya[yyac] = new YYARec(285,-102 );yyac++; 
					yya[yyac] = new YYARec(286,-102 );yyac++; 
					yya[yyac] = new YYARec(287,-102 );yyac++; 
					yya[yyac] = new YYARec(288,-102 );yyac++; 
					yya[yyac] = new YYARec(289,-102 );yyac++; 
					yya[yyac] = new YYARec(290,-102 );yyac++; 
					yya[yyac] = new YYARec(291,-102 );yyac++; 
					yya[yyac] = new YYARec(292,-102 );yyac++; 
					yya[yyac] = new YYARec(293,-102 );yyac++; 
					yya[yyac] = new YYARec(294,-102 );yyac++; 
					yya[yyac] = new YYARec(295,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(309,-102 );yyac++; 
					yya[yyac] = new YYARec(268,184);yyac++; 
					yya[yyac] = new YYARec(269,132);yyac++; 
					yya[yyac] = new YYARec(270,133);yyac++; 
					yya[yyac] = new YYARec(271,134);yyac++; 
					yya[yyac] = new YYARec(272,135);yyac++; 
					yya[yyac] = new YYARec(273,136);yyac++; 
					yya[yyac] = new YYARec(274,137);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(268,185);yyac++; 
					yya[yyac] = new YYARec(269,132);yyac++; 
					yya[yyac] = new YYARec(270,133);yyac++; 
					yya[yyac] = new YYARec(271,134);yyac++; 
					yya[yyac] = new YYARec(272,135);yyac++; 
					yya[yyac] = new YYARec(273,136);yyac++; 
					yya[yyac] = new YYARec(274,137);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(299,-86 );yyac++; 
					yya[yyac] = new YYARec(300,-86 );yyac++; 
					yya[yyac] = new YYARec(301,-86 );yyac++; 
					yya[yyac] = new YYARec(302,-86 );yyac++; 
					yya[yyac] = new YYARec(303,-86 );yyac++; 
					yya[yyac] = new YYARec(304,-86 );yyac++; 
					yya[yyac] = new YYARec(305,-86 );yyac++; 
					yya[yyac] = new YYARec(306,-86 );yyac++; 
					yya[yyac] = new YYARec(268,-90 );yyac++; 
					yya[yyac] = new YYARec(299,-84 );yyac++; 
					yya[yyac] = new YYARec(300,-84 );yyac++; 
					yya[yyac] = new YYARec(301,-84 );yyac++; 
					yya[yyac] = new YYARec(302,-84 );yyac++; 
					yya[yyac] = new YYARec(303,-84 );yyac++; 
					yya[yyac] = new YYARec(304,-84 );yyac++; 
					yya[yyac] = new YYARec(305,-84 );yyac++; 
					yya[yyac] = new YYARec(306,-84 );yyac++; 
					yya[yyac] = new YYARec(268,-89 );yyac++; 
					yya[yyac] = new YYARec(267,82);yyac++; 
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
					yya[yyac] = new YYARec(296,83);yyac++; 
					yya[yyac] = new YYARec(297,84);yyac++; 
					yya[yyac] = new YYARec(298,85);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-27 );yyac++; 
					yya[yyac] = new YYARec(267,82);yyac++; 
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
					yya[yyac] = new YYARec(296,83);yyac++; 
					yya[yyac] = new YYARec(297,84);yyac++; 
					yya[yyac] = new YYARec(298,85);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-27 );yyac++; 
					yya[yyac] = new YYARec(267,82);yyac++; 
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
					yya[yyac] = new YYARec(296,83);yyac++; 
					yya[yyac] = new YYARec(297,84);yyac++; 
					yya[yyac] = new YYARec(298,85);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-27 );yyac++; 
					yya[yyac] = new YYARec(268,189);yyac++; 
					yya[yyac] = new YYARec(299,120);yyac++; 
					yya[yyac] = new YYARec(300,121);yyac++; 
					yya[yyac] = new YYARec(301,122);yyac++; 
					yya[yyac] = new YYARec(302,123);yyac++; 
					yya[yyac] = new YYARec(303,124);yyac++; 
					yya[yyac] = new YYARec(304,125);yyac++; 
					yya[yyac] = new YYARec(305,126);yyac++; 
					yya[yyac] = new YYARec(306,127);yyac++; 
					yya[yyac] = new YYARec(265,190);yyac++; 
					yya[yyac] = new YYARec(265,191);yyac++; 
					yya[yyac] = new YYARec(265,192);yyac++; 
					yya[yyac] = new YYARec(299,120);yyac++; 
					yya[yyac] = new YYARec(300,121);yyac++; 
					yya[yyac] = new YYARec(301,122);yyac++; 
					yya[yyac] = new YYARec(302,123);yyac++; 
					yya[yyac] = new YYARec(303,124);yyac++; 
					yya[yyac] = new YYARec(304,125);yyac++; 
					yya[yyac] = new YYARec(305,126);yyac++; 
					yya[yyac] = new YYARec(306,127);yyac++; 
					yya[yyac] = new YYARec(264,-108 );yyac++;

					yyg[yygc] = new YYARec(-32,1);yygc++; 
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
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-31,31);yygc++; 
					yyg[yygc] = new YYARec(-18,32);yygc++; 
					yyg[yygc] = new YYARec(-17,33);yygc++; 
					yyg[yygc] = new YYARec(-16,34);yygc++; 
					yyg[yygc] = new YYARec(-15,35);yygc++; 
					yyg[yygc] = new YYARec(-14,36);yygc++; 
					yyg[yygc] = new YYARec(-13,37);yygc++; 
					yyg[yygc] = new YYARec(-7,38);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-12,2);yygc++; 
					yyg[yygc] = new YYARec(-11,3);yygc++; 
					yyg[yygc] = new YYARec(-10,4);yygc++; 
					yyg[yygc] = new YYARec(-9,5);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-6,7);yygc++; 
					yyg[yygc] = new YYARec(-5,8);yygc++; 
					yyg[yygc] = new YYARec(-4,9);yygc++; 
					yyg[yygc] = new YYARec(-3,43);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-7,44);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-7,45);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-7,46);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-31,31);yygc++; 
					yyg[yygc] = new YYARec(-25,50);yygc++; 
					yyg[yygc] = new YYARec(-24,51);yygc++; 
					yyg[yygc] = new YYARec(-19,52);yygc++; 
					yyg[yygc] = new YYARec(-17,53);yygc++; 
					yyg[yygc] = new YYARec(-16,54);yygc++; 
					yyg[yygc] = new YYARec(-14,55);yygc++; 
					yyg[yygc] = new YYARec(-13,56);yygc++; 
					yyg[yygc] = new YYARec(-7,57);yygc++; 
					yyg[yygc] = new YYARec(-19,64);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-31,31);yygc++; 
					yyg[yygc] = new YYARec(-25,50);yygc++; 
					yyg[yygc] = new YYARec(-24,67);yygc++; 
					yyg[yygc] = new YYARec(-17,53);yygc++; 
					yyg[yygc] = new YYARec(-16,54);yygc++; 
					yyg[yygc] = new YYARec(-14,55);yygc++; 
					yyg[yygc] = new YYARec(-13,56);yygc++; 
					yyg[yygc] = new YYARec(-7,57);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-7,68);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-12,2);yygc++; 
					yyg[yygc] = new YYARec(-11,3);yygc++; 
					yyg[yygc] = new YYARec(-10,4);yygc++; 
					yyg[yygc] = new YYARec(-9,5);yygc++; 
					yyg[yygc] = new YYARec(-8,69);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-5,70);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-12,2);yygc++; 
					yyg[yygc] = new YYARec(-11,3);yygc++; 
					yyg[yygc] = new YYARec(-10,4);yygc++; 
					yyg[yygc] = new YYARec(-9,5);yygc++; 
					yyg[yygc] = new YYARec(-8,71);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-5,70);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-31,31);yygc++; 
					yyg[yygc] = new YYARec(-14,72);yygc++; 
					yyg[yygc] = new YYARec(-13,73);yygc++; 
					yyg[yygc] = new YYARec(-7,57);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-31,31);yygc++; 
					yyg[yygc] = new YYARec(-25,50);yygc++; 
					yyg[yygc] = new YYARec(-24,74);yygc++; 
					yyg[yygc] = new YYARec(-17,53);yygc++; 
					yyg[yygc] = new YYARec(-16,54);yygc++; 
					yyg[yygc] = new YYARec(-14,55);yygc++; 
					yyg[yygc] = new YYARec(-13,56);yygc++; 
					yyg[yygc] = new YYARec(-7,57);yygc++; 
					yyg[yygc] = new YYARec(-34,75);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-23,76);yygc++; 
					yyg[yygc] = new YYARec(-22,77);yygc++; 
					yyg[yygc] = new YYARec(-21,78);yygc++; 
					yyg[yygc] = new YYARec(-20,79);yygc++; 
					yyg[yygc] = new YYARec(-18,80);yygc++; 
					yyg[yygc] = new YYARec(-7,81);yygc++; 
					yyg[yygc] = new YYARec(-34,75);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-23,76);yygc++; 
					yyg[yygc] = new YYARec(-22,77);yygc++; 
					yyg[yygc] = new YYARec(-21,78);yygc++; 
					yyg[yygc] = new YYARec(-20,91);yygc++; 
					yyg[yygc] = new YYARec(-18,80);yygc++; 
					yyg[yygc] = new YYARec(-7,81);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-31,31);yygc++; 
					yyg[yygc] = new YYARec(-25,50);yygc++; 
					yyg[yygc] = new YYARec(-24,51);yygc++; 
					yyg[yygc] = new YYARec(-17,53);yygc++; 
					yyg[yygc] = new YYARec(-16,54);yygc++; 
					yyg[yygc] = new YYARec(-14,55);yygc++; 
					yyg[yygc] = new YYARec(-13,94);yygc++; 
					yyg[yygc] = new YYARec(-7,57);yygc++; 
					yyg[yygc] = new YYARec(-35,97);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-31,98);yygc++; 
					yyg[yygc] = new YYARec(-28,99);yygc++; 
					yyg[yygc] = new YYARec(-27,100);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-34,106);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-12,2);yygc++; 
					yyg[yygc] = new YYARec(-11,3);yygc++; 
					yyg[yygc] = new YYARec(-10,4);yygc++; 
					yyg[yygc] = new YYARec(-9,5);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-5,108);yygc++; 
					yyg[yygc] = new YYARec(-34,75);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-23,76);yygc++; 
					yyg[yygc] = new YYARec(-22,77);yygc++; 
					yyg[yygc] = new YYARec(-21,78);yygc++; 
					yyg[yygc] = new YYARec(-20,109);yygc++; 
					yyg[yygc] = new YYARec(-18,80);yygc++; 
					yyg[yygc] = new YYARec(-7,81);yygc++; 
					yyg[yygc] = new YYARec(-34,75);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-23,76);yygc++; 
					yyg[yygc] = new YYARec(-22,77);yygc++; 
					yyg[yygc] = new YYARec(-21,78);yygc++; 
					yyg[yygc] = new YYARec(-20,110);yygc++; 
					yyg[yygc] = new YYARec(-18,80);yygc++; 
					yyg[yygc] = new YYARec(-7,81);yygc++; 
					yyg[yygc] = new YYARec(-26,111);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-31,31);yygc++; 
					yyg[yygc] = new YYARec(-25,50);yygc++; 
					yyg[yygc] = new YYARec(-24,67);yygc++; 
					yyg[yygc] = new YYARec(-17,53);yygc++; 
					yyg[yygc] = new YYARec(-16,54);yygc++; 
					yyg[yygc] = new YYARec(-14,55);yygc++; 
					yyg[yygc] = new YYARec(-13,56);yygc++; 
					yyg[yygc] = new YYARec(-7,57);yygc++; 
					yyg[yygc] = new YYARec(-34,75);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-23,76);yygc++; 
					yyg[yygc] = new YYARec(-22,77);yygc++; 
					yyg[yygc] = new YYARec(-21,78);yygc++; 
					yyg[yygc] = new YYARec(-20,117);yygc++; 
					yyg[yygc] = new YYARec(-18,80);yygc++; 
					yyg[yygc] = new YYARec(-7,81);yygc++; 
					yyg[yygc] = new YYARec(-37,118);yygc++; 
					yyg[yygc] = new YYARec(-32,128);yygc++; 
					yyg[yygc] = new YYARec(-13,129);yygc++; 
					yyg[yygc] = new YYARec(-7,57);yygc++; 
					yyg[yygc] = new YYARec(-33,130);yygc++; 
					yyg[yygc] = new YYARec(-29,131);yygc++; 
					yyg[yygc] = new YYARec(-36,138);yygc++; 
					yyg[yygc] = new YYARec(-35,139);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-31,98);yygc++; 
					yyg[yygc] = new YYARec(-28,99);yygc++; 
					yyg[yygc] = new YYARec(-27,140);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-35,143);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-31,98);yygc++; 
					yyg[yygc] = new YYARec(-28,99);yygc++; 
					yyg[yygc] = new YYARec(-27,100);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-34,75);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-23,76);yygc++; 
					yyg[yygc] = new YYARec(-22,77);yygc++; 
					yyg[yygc] = new YYARec(-21,78);yygc++; 
					yyg[yygc] = new YYARec(-20,144);yygc++; 
					yyg[yygc] = new YYARec(-18,80);yygc++; 
					yyg[yygc] = new YYARec(-7,81);yygc++; 
					yyg[yygc] = new YYARec(-35,146);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-31,98);yygc++; 
					yyg[yygc] = new YYARec(-28,99);yygc++; 
					yyg[yygc] = new YYARec(-27,100);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-31,98);yygc++; 
					yyg[yygc] = new YYARec(-28,99);yygc++; 
					yyg[yygc] = new YYARec(-27,149);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-38,151);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-31,98);yygc++; 
					yyg[yygc] = new YYARec(-28,99);yygc++; 
					yyg[yygc] = new YYARec(-27,152);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-37,154);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-31,98);yygc++; 
					yyg[yygc] = new YYARec(-30,156);yygc++; 
					yyg[yygc] = new YYARec(-28,157);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-37,160);yygc++; 
					yyg[yygc] = new YYARec(-33,130);yygc++; 
					yyg[yygc] = new YYARec(-29,131);yygc++; 
					yyg[yygc] = new YYARec(-36,162);yygc++; 
					yyg[yygc] = new YYARec(-35,139);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-31,98);yygc++; 
					yyg[yygc] = new YYARec(-28,99);yygc++; 
					yyg[yygc] = new YYARec(-27,140);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-34,75);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-23,76);yygc++; 
					yyg[yygc] = new YYARec(-22,77);yygc++; 
					yyg[yygc] = new YYARec(-21,78);yygc++; 
					yyg[yygc] = new YYARec(-20,163);yygc++; 
					yyg[yygc] = new YYARec(-18,80);yygc++; 
					yyg[yygc] = new YYARec(-7,81);yygc++; 
					yyg[yygc] = new YYARec(-37,118);yygc++; 
					yyg[yygc] = new YYARec(-34,75);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-23,76);yygc++; 
					yyg[yygc] = new YYARec(-22,77);yygc++; 
					yyg[yygc] = new YYARec(-21,78);yygc++; 
					yyg[yygc] = new YYARec(-20,166);yygc++; 
					yyg[yygc] = new YYARec(-18,80);yygc++; 
					yyg[yygc] = new YYARec(-7,81);yygc++; 
					yyg[yygc] = new YYARec(-37,118);yygc++; 
					yyg[yygc] = new YYARec(-33,130);yygc++; 
					yyg[yygc] = new YYARec(-29,131);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-31,98);yygc++; 
					yyg[yygc] = new YYARec(-28,99);yygc++; 
					yyg[yygc] = new YYARec(-27,169);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-33,130);yygc++; 
					yyg[yygc] = new YYARec(-29,131);yygc++; 
					yyg[yygc] = new YYARec(-36,170);yygc++; 
					yyg[yygc] = new YYARec(-35,139);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-31,98);yygc++; 
					yyg[yygc] = new YYARec(-28,99);yygc++; 
					yyg[yygc] = new YYARec(-27,140);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-34,171);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-31,98);yygc++; 
					yyg[yygc] = new YYARec(-28,99);yygc++; 
					yyg[yygc] = new YYARec(-27,173);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-31,98);yygc++; 
					yyg[yygc] = new YYARec(-28,99);yygc++; 
					yyg[yygc] = new YYARec(-27,174);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-38,175);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-31,98);yygc++; 
					yyg[yygc] = new YYARec(-28,99);yygc++; 
					yyg[yygc] = new YYARec(-27,152);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-37,154);yygc++; 
					yyg[yygc] = new YYARec(-37,154);yygc++; 
					yyg[yygc] = new YYARec(-33,130);yygc++; 
					yyg[yygc] = new YYARec(-29,131);yygc++; 
					yyg[yygc] = new YYARec(-35,183);yygc++; 
					yyg[yygc] = new YYARec(-33,30);yygc++; 
					yyg[yygc] = new YYARec(-31,98);yygc++; 
					yyg[yygc] = new YYARec(-28,99);yygc++; 
					yyg[yygc] = new YYARec(-27,100);yygc++; 
					yyg[yygc] = new YYARec(-14,101);yygc++; 
					yyg[yygc] = new YYARec(-33,130);yygc++; 
					yyg[yygc] = new YYARec(-29,131);yygc++; 
					yyg[yygc] = new YYARec(-33,130);yygc++; 
					yyg[yygc] = new YYARec(-29,131);yygc++; 
					yyg[yygc] = new YYARec(-34,75);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-23,76);yygc++; 
					yyg[yygc] = new YYARec(-22,77);yygc++; 
					yyg[yygc] = new YYARec(-21,78);yygc++; 
					yyg[yygc] = new YYARec(-20,186);yygc++; 
					yyg[yygc] = new YYARec(-18,80);yygc++; 
					yyg[yygc] = new YYARec(-7,81);yygc++; 
					yyg[yygc] = new YYARec(-34,75);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-23,76);yygc++; 
					yyg[yygc] = new YYARec(-22,77);yygc++; 
					yyg[yygc] = new YYARec(-21,78);yygc++; 
					yyg[yygc] = new YYARec(-20,187);yygc++; 
					yyg[yygc] = new YYARec(-18,80);yygc++; 
					yyg[yygc] = new YYARec(-7,81);yygc++; 
					yyg[yygc] = new YYARec(-34,75);yygc++; 
					yyg[yygc] = new YYARec(-32,1);yygc++; 
					yyg[yygc] = new YYARec(-23,76);yygc++; 
					yyg[yygc] = new YYARec(-22,77);yygc++; 
					yyg[yygc] = new YYARec(-21,78);yygc++; 
					yyg[yygc] = new YYARec(-20,188);yygc++; 
					yyg[yygc] = new YYARec(-18,80);yygc++; 
					yyg[yygc] = new YYARec(-7,81);yygc++; 
					yyg[yygc] = new YYARec(-37,118);yygc++; 
					yyg[yygc] = new YYARec(-37,154);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = -105;  
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
					yyd[15] = -64;  
					yyd[16] = -65;  
					yyd[17] = -66;  
					yyd[18] = -67;  
					yyd[19] = -68;  
					yyd[20] = -69;  
					yyd[21] = -70;  
					yyd[22] = -71;  
					yyd[23] = -72;  
					yyd[24] = -73;  
					yyd[25] = -74;  
					yyd[26] = -75;  
					yyd[27] = -76;  
					yyd[28] = -77;  
					yyd[29] = -104;  
					yyd[30] = -101;  
					yyd[31] = 0;  
					yyd[32] = 0;  
					yyd[33] = -21;  
					yyd[34] = -18;  
					yyd[35] = 0;  
					yyd[36] = -20;  
					yyd[37] = -19;  
					yyd[38] = 0;  
					yyd[39] = -57;  
					yyd[40] = -58;  
					yyd[41] = -106;  
					yyd[42] = -107;  
					yyd[43] = -3;  
					yyd[44] = 0;  
					yyd[45] = 0;  
					yyd[46] = 0;  
					yyd[47] = -103;  
					yyd[48] = -22;  
					yyd[49] = -17;  
					yyd[50] = 0;  
					yyd[51] = -32;  
					yyd[52] = 0;  
					yyd[53] = -35;  
					yyd[54] = -36;  
					yyd[55] = -37;  
					yyd[56] = -38;  
					yyd[57] = 0;  
					yyd[58] = 0;  
					yyd[59] = 0;  
					yyd[60] = 0;  
					yyd[61] = 0;  
					yyd[62] = -16;  
					yyd[63] = 0;  
					yyd[64] = 0;  
					yyd[65] = -39;  
					yyd[66] = 0;  
					yyd[67] = -31;  
					yyd[68] = -99;  
					yyd[69] = -6;  
					yyd[70] = 0;  
					yyd[71] = -7;  
					yyd[72] = 0;  
					yyd[73] = 0;  
					yyd[74] = -34;  
					yyd[75] = 0;  
					yyd[76] = -28;  
					yyd[77] = 0;  
					yyd[78] = 0;  
					yyd[79] = 0;  
					yyd[80] = -29;  
					yyd[81] = 0;  
					yyd[82] = 0;  
					yyd[83] = 0;  
					yyd[84] = 0;  
					yyd[85] = 0;  
					yyd[86] = 0;  
					yyd[87] = -9;  
					yyd[88] = -15;  
					yyd[89] = -14;  
					yyd[90] = 0;  
					yyd[91] = -26;  
					yyd[92] = 0;  
					yyd[93] = -23;  
					yyd[94] = 0;  
					yyd[95] = 0;  
					yyd[96] = 0;  
					yyd[97] = 0;  
					yyd[98] = 0;  
					yyd[99] = -43;  
					yyd[100] = 0;  
					yyd[101] = -49;  
					yyd[102] = 0;  
					yyd[103] = 0;  
					yyd[104] = 0;  
					yyd[105] = 0;  
					yyd[106] = 0;  
					yyd[107] = 0;  
					yyd[108] = 0;  
					yyd[109] = 0;  
					yyd[110] = -25;  
					yyd[111] = 0;  
					yyd[112] = -59;  
					yyd[113] = -60;  
					yyd[114] = -61;  
					yyd[115] = -62;  
					yyd[116] = -63;  
					yyd[117] = -24;  
					yyd[118] = 0;  
					yyd[119] = 0;  
					yyd[120] = -91;  
					yyd[121] = -92;  
					yyd[122] = -93;  
					yyd[123] = -94;  
					yyd[124] = -95;  
					yyd[125] = -96;  
					yyd[126] = -97;  
					yyd[127] = -98;  
					yyd[128] = 0;  
					yyd[129] = -48;  
					yyd[130] = -56;  
					yyd[131] = 0;  
					yyd[132] = -50;  
					yyd[133] = -51;  
					yyd[134] = -52;  
					yyd[135] = -53;  
					yyd[136] = -54;  
					yyd[137] = -55;  
					yyd[138] = 0;  
					yyd[139] = 0;  
					yyd[140] = 0;  
					yyd[141] = 0;  
					yyd[142] = 0;  
					yyd[143] = 0;  
					yyd[144] = 0;  
					yyd[145] = 0;  
					yyd[146] = 0;  
					yyd[147] = -8;  
					yyd[148] = 0;  
					yyd[149] = 0;  
					yyd[150] = 0;  
					yyd[151] = -86;  
					yyd[152] = 0;  
					yyd[153] = 0;  
					yyd[154] = 0;  
					yyd[155] = 0;  
					yyd[156] = -44;  
					yyd[157] = -46;  
					yyd[158] = 0;  
					yyd[159] = -84;  
					yyd[160] = 0;  
					yyd[161] = -42;  
					yyd[162] = 0;  
					yyd[163] = 0;  
					yyd[164] = 0;  
					yyd[165] = -81;  
					yyd[166] = 0;  
					yyd[167] = 0;  
					yyd[168] = 0;  
					yyd[169] = 0;  
					yyd[170] = 0;  
					yyd[171] = -109;  
					yyd[172] = 0;  
					yyd[173] = 0;  
					yyd[174] = 0;  
					yyd[175] = 0;  
					yyd[176] = 0;  
					yyd[177] = -82;  
					yyd[178] = 0;  
					yyd[179] = -83;  
					yyd[180] = 0;  
					yyd[181] = 0;  
					yyd[182] = -87;  
					yyd[183] = 0;  
					yyd[184] = -47;  
					yyd[185] = -45;  
					yyd[186] = 0;  
					yyd[187] = 0;  
					yyd[188] = 0;  
					yyd[189] = 0;  
					yyd[190] = -79;  
					yyd[191] = -80;  
					yyd[192] = -78; 

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
					yyal[33] = 106;  
					yyal[34] = 106;  
					yyal[35] = 106;  
					yyal[36] = 107;  
					yyal[37] = 107;  
					yyal[38] = 107;  
					yyal[39] = 131;  
					yyal[40] = 131;  
					yyal[41] = 131;  
					yyal[42] = 131;  
					yyal[43] = 131;  
					yyal[44] = 131;  
					yyal[45] = 132;  
					yyal[46] = 133;  
					yyal[47] = 135;  
					yyal[48] = 135;  
					yyal[49] = 135;  
					yyal[50] = 135;  
					yyal[51] = 157;  
					yyal[52] = 157;  
					yyal[53] = 158;  
					yyal[54] = 158;  
					yyal[55] = 158;  
					yyal[56] = 158;  
					yyal[57] = 158;  
					yyal[58] = 201;  
					yyal[59] = 222;  
					yyal[60] = 237;  
					yyal[61] = 253;  
					yyal[62] = 269;  
					yyal[63] = 269;  
					yyal[64] = 287;  
					yyal[65] = 307;  
					yyal[66] = 307;  
					yyal[67] = 327;  
					yyal[68] = 327;  
					yyal[69] = 327;  
					yyal[70] = 327;  
					yyal[71] = 329;  
					yyal[72] = 329;  
					yyal[73] = 330;  
					yyal[74] = 331;  
					yyal[75] = 331;  
					yyal[76] = 332;  
					yyal[77] = 332;  
					yyal[78] = 352;  
					yyal[79] = 353;  
					yyal[80] = 354;  
					yyal[81] = 354;  
					yyal[82] = 377;  
					yyal[83] = 396;  
					yyal[84] = 397;  
					yyal[85] = 398;  
					yyal[86] = 399;  
					yyal[87] = 415;  
					yyal[88] = 415;  
					yyal[89] = 415;  
					yyal[90] = 415;  
					yyal[91] = 435;  
					yyal[92] = 435;  
					yyal[93] = 455;  
					yyal[94] = 455;  
					yyal[95] = 482;  
					yyal[96] = 502;  
					yyal[97] = 522;  
					yyal[98] = 531;  
					yyal[99] = 547;  
					yyal[100] = 547;  
					yyal[101] = 564;  
					yyal[102] = 564;  
					yyal[103] = 583;  
					yyal[104] = 584;  
					yyal[105] = 603;  
					yyal[106] = 623;  
					yyal[107] = 624;  
					yyal[108] = 643;  
					yyal[109] = 644;  
					yyal[110] = 645;  
					yyal[111] = 645;  
					yyal[112] = 664;  
					yyal[113] = 664;  
					yyal[114] = 664;  
					yyal[115] = 664;  
					yyal[116] = 664;  
					yyal[117] = 664;  
					yyal[118] = 664;  
					yyal[119] = 683;  
					yyal[120] = 691;  
					yyal[121] = 691;  
					yyal[122] = 691;  
					yyal[123] = 691;  
					yyal[124] = 691;  
					yyal[125] = 691;  
					yyal[126] = 691;  
					yyal[127] = 691;  
					yyal[128] = 691;  
					yyal[129] = 711;  
					yyal[130] = 711;  
					yyal[131] = 711;  
					yyal[132] = 730;  
					yyal[133] = 730;  
					yyal[134] = 730;  
					yyal[135] = 730;  
					yyal[136] = 730;  
					yyal[137] = 730;  
					yyal[138] = 730;  
					yyal[139] = 731;  
					yyal[140] = 739;  
					yyal[141] = 756;  
					yyal[142] = 775;  
					yyal[143] = 795;  
					yyal[144] = 804;  
					yyal[145] = 805;  
					yyal[146] = 825;  
					yyal[147] = 834;  
					yyal[148] = 834;  
					yyal[149] = 835;  
					yyal[150] = 844;  
					yyal[151] = 863;  
					yyal[152] = 863;  
					yyal[153] = 880;  
					yyal[154] = 899;  
					yyal[155] = 900;  
					yyal[156] = 919;  
					yyal[157] = 919;  
					yyal[158] = 919;  
					yyal[159] = 938;  
					yyal[160] = 938;  
					yyal[161] = 957;  
					yyal[162] = 957;  
					yyal[163] = 958;  
					yyal[164] = 959;  
					yyal[165] = 968;  
					yyal[166] = 968;  
					yyal[167] = 969;  
					yyal[168] = 978;  
					yyal[169] = 979;  
					yyal[170] = 988;  
					yyal[171] = 989;  
					yyal[172] = 989;  
					yyal[173] = 1008;  
					yyal[174] = 1017;  
					yyal[175] = 1026;  
					yyal[176] = 1035;  
					yyal[177] = 1044;  
					yyal[178] = 1044;  
					yyal[179] = 1064;  
					yyal[180] = 1064;  
					yyal[181] = 1084;  
					yyal[182] = 1104;  
					yyal[183] = 1104;  
					yyal[184] = 1113;  
					yyal[185] = 1113;  
					yyal[186] = 1113;  
					yyal[187] = 1114;  
					yyal[188] = 1115;  
					yyal[189] = 1116;  
					yyal[190] = 1125;  
					yyal[191] = 1125;  
					yyal[192] = 1125; 

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
					yyah[32] = 105;  
					yyah[33] = 105;  
					yyah[34] = 105;  
					yyah[35] = 106;  
					yyah[36] = 106;  
					yyah[37] = 106;  
					yyah[38] = 130;  
					yyah[39] = 130;  
					yyah[40] = 130;  
					yyah[41] = 130;  
					yyah[42] = 130;  
					yyah[43] = 130;  
					yyah[44] = 131;  
					yyah[45] = 132;  
					yyah[46] = 134;  
					yyah[47] = 134;  
					yyah[48] = 134;  
					yyah[49] = 134;  
					yyah[50] = 156;  
					yyah[51] = 156;  
					yyah[52] = 157;  
					yyah[53] = 157;  
					yyah[54] = 157;  
					yyah[55] = 157;  
					yyah[56] = 157;  
					yyah[57] = 200;  
					yyah[58] = 221;  
					yyah[59] = 236;  
					yyah[60] = 252;  
					yyah[61] = 268;  
					yyah[62] = 268;  
					yyah[63] = 286;  
					yyah[64] = 306;  
					yyah[65] = 306;  
					yyah[66] = 326;  
					yyah[67] = 326;  
					yyah[68] = 326;  
					yyah[69] = 326;  
					yyah[70] = 328;  
					yyah[71] = 328;  
					yyah[72] = 329;  
					yyah[73] = 330;  
					yyah[74] = 330;  
					yyah[75] = 331;  
					yyah[76] = 331;  
					yyah[77] = 351;  
					yyah[78] = 352;  
					yyah[79] = 353;  
					yyah[80] = 353;  
					yyah[81] = 376;  
					yyah[82] = 395;  
					yyah[83] = 396;  
					yyah[84] = 397;  
					yyah[85] = 398;  
					yyah[86] = 414;  
					yyah[87] = 414;  
					yyah[88] = 414;  
					yyah[89] = 414;  
					yyah[90] = 434;  
					yyah[91] = 434;  
					yyah[92] = 454;  
					yyah[93] = 454;  
					yyah[94] = 481;  
					yyah[95] = 501;  
					yyah[96] = 521;  
					yyah[97] = 530;  
					yyah[98] = 546;  
					yyah[99] = 546;  
					yyah[100] = 563;  
					yyah[101] = 563;  
					yyah[102] = 582;  
					yyah[103] = 583;  
					yyah[104] = 602;  
					yyah[105] = 622;  
					yyah[106] = 623;  
					yyah[107] = 642;  
					yyah[108] = 643;  
					yyah[109] = 644;  
					yyah[110] = 644;  
					yyah[111] = 663;  
					yyah[112] = 663;  
					yyah[113] = 663;  
					yyah[114] = 663;  
					yyah[115] = 663;  
					yyah[116] = 663;  
					yyah[117] = 663;  
					yyah[118] = 682;  
					yyah[119] = 690;  
					yyah[120] = 690;  
					yyah[121] = 690;  
					yyah[122] = 690;  
					yyah[123] = 690;  
					yyah[124] = 690;  
					yyah[125] = 690;  
					yyah[126] = 690;  
					yyah[127] = 690;  
					yyah[128] = 710;  
					yyah[129] = 710;  
					yyah[130] = 710;  
					yyah[131] = 729;  
					yyah[132] = 729;  
					yyah[133] = 729;  
					yyah[134] = 729;  
					yyah[135] = 729;  
					yyah[136] = 729;  
					yyah[137] = 729;  
					yyah[138] = 730;  
					yyah[139] = 738;  
					yyah[140] = 755;  
					yyah[141] = 774;  
					yyah[142] = 794;  
					yyah[143] = 803;  
					yyah[144] = 804;  
					yyah[145] = 824;  
					yyah[146] = 833;  
					yyah[147] = 833;  
					yyah[148] = 834;  
					yyah[149] = 843;  
					yyah[150] = 862;  
					yyah[151] = 862;  
					yyah[152] = 879;  
					yyah[153] = 898;  
					yyah[154] = 899;  
					yyah[155] = 918;  
					yyah[156] = 918;  
					yyah[157] = 918;  
					yyah[158] = 937;  
					yyah[159] = 937;  
					yyah[160] = 956;  
					yyah[161] = 956;  
					yyah[162] = 957;  
					yyah[163] = 958;  
					yyah[164] = 967;  
					yyah[165] = 967;  
					yyah[166] = 968;  
					yyah[167] = 977;  
					yyah[168] = 978;  
					yyah[169] = 987;  
					yyah[170] = 988;  
					yyah[171] = 988;  
					yyah[172] = 1007;  
					yyah[173] = 1016;  
					yyah[174] = 1025;  
					yyah[175] = 1034;  
					yyah[176] = 1043;  
					yyah[177] = 1043;  
					yyah[178] = 1063;  
					yyah[179] = 1063;  
					yyah[180] = 1083;  
					yyah[181] = 1103;  
					yyah[182] = 1103;  
					yyah[183] = 1112;  
					yyah[184] = 1112;  
					yyah[185] = 1112;  
					yyah[186] = 1113;  
					yyah[187] = 1114;  
					yyah[188] = 1115;  
					yyah[189] = 1124;  
					yyah[190] = 1124;  
					yyah[191] = 1124;  
					yyah[192] = 1124; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 12;  
					yygl[2] = 12;  
					yygl[3] = 12;  
					yygl[4] = 12;  
					yygl[5] = 12;  
					yygl[6] = 12;  
					yygl[7] = 22;  
					yygl[8] = 22;  
					yygl[9] = 22;  
					yygl[10] = 32;  
					yygl[11] = 32;  
					yygl[12] = 32;  
					yygl[13] = 34;  
					yygl[14] = 36;  
					yygl[15] = 38;  
					yygl[16] = 38;  
					yygl[17] = 38;  
					yygl[18] = 38;  
					yygl[19] = 38;  
					yygl[20] = 38;  
					yygl[21] = 38;  
					yygl[22] = 38;  
					yygl[23] = 38;  
					yygl[24] = 38;  
					yygl[25] = 38;  
					yygl[26] = 38;  
					yygl[27] = 38;  
					yygl[28] = 38;  
					yygl[29] = 38;  
					yygl[30] = 38;  
					yygl[31] = 38;  
					yygl[32] = 38;  
					yygl[33] = 38;  
					yygl[34] = 38;  
					yygl[35] = 38;  
					yygl[36] = 38;  
					yygl[37] = 38;  
					yygl[38] = 38;  
					yygl[39] = 49;  
					yygl[40] = 49;  
					yygl[41] = 49;  
					yygl[42] = 49;  
					yygl[43] = 49;  
					yygl[44] = 49;  
					yygl[45] = 49;  
					yygl[46] = 49;  
					yygl[47] = 49;  
					yygl[48] = 49;  
					yygl[49] = 49;  
					yygl[50] = 49;  
					yygl[51] = 50;  
					yygl[52] = 50;  
					yygl[53] = 50;  
					yygl[54] = 50;  
					yygl[55] = 50;  
					yygl[56] = 50;  
					yygl[57] = 50;  
					yygl[58] = 50;  
					yygl[59] = 60;  
					yygl[60] = 62;  
					yygl[61] = 70;  
					yygl[62] = 78;  
					yygl[63] = 78;  
					yygl[64] = 84;  
					yygl[65] = 94;  
					yygl[66] = 94;  
					yygl[67] = 102;  
					yygl[68] = 102;  
					yygl[69] = 102;  
					yygl[70] = 102;  
					yygl[71] = 102;  
					yygl[72] = 102;  
					yygl[73] = 102;  
					yygl[74] = 102;  
					yygl[75] = 102;  
					yygl[76] = 102;  
					yygl[77] = 102;  
					yygl[78] = 110;  
					yygl[79] = 110;  
					yygl[80] = 110;  
					yygl[81] = 110;  
					yygl[82] = 120;  
					yygl[83] = 126;  
					yygl[84] = 127;  
					yygl[85] = 127;  
					yygl[86] = 128;  
					yygl[87] = 135;  
					yygl[88] = 135;  
					yygl[89] = 135;  
					yygl[90] = 135;  
					yygl[91] = 143;  
					yygl[92] = 143;  
					yygl[93] = 151;  
					yygl[94] = 151;  
					yygl[95] = 152;  
					yygl[96] = 162;  
					yygl[97] = 170;  
					yygl[98] = 171;  
					yygl[99] = 174;  
					yygl[100] = 174;  
					yygl[101] = 176;  
					yygl[102] = 176;  
					yygl[103] = 183;  
					yygl[104] = 183;  
					yygl[105] = 189;  
					yygl[106] = 197;  
					yygl[107] = 197;  
					yygl[108] = 203;  
					yygl[109] = 203;  
					yygl[110] = 203;  
					yygl[111] = 203;  
					yygl[112] = 208;  
					yygl[113] = 208;  
					yygl[114] = 208;  
					yygl[115] = 208;  
					yygl[116] = 208;  
					yygl[117] = 208;  
					yygl[118] = 208;  
					yygl[119] = 214;  
					yygl[120] = 215;  
					yygl[121] = 215;  
					yygl[122] = 215;  
					yygl[123] = 215;  
					yygl[124] = 215;  
					yygl[125] = 215;  
					yygl[126] = 215;  
					yygl[127] = 215;  
					yygl[128] = 215;  
					yygl[129] = 215;  
					yygl[130] = 215;  
					yygl[131] = 215;  
					yygl[132] = 220;  
					yygl[133] = 220;  
					yygl[134] = 220;  
					yygl[135] = 220;  
					yygl[136] = 220;  
					yygl[137] = 220;  
					yygl[138] = 220;  
					yygl[139] = 220;  
					yygl[140] = 221;  
					yygl[141] = 223;  
					yygl[142] = 230;  
					yygl[143] = 238;  
					yygl[144] = 239;  
					yygl[145] = 239;  
					yygl[146] = 247;  
					yygl[147] = 248;  
					yygl[148] = 248;  
					yygl[149] = 248;  
					yygl[150] = 250;  
					yygl[151] = 255;  
					yygl[152] = 255;  
					yygl[153] = 257;  
					yygl[154] = 264;  
					yygl[155] = 265;  
					yygl[156] = 270;  
					yygl[157] = 270;  
					yygl[158] = 270;  
					yygl[159] = 275;  
					yygl[160] = 275;  
					yygl[161] = 281;  
					yygl[162] = 281;  
					yygl[163] = 281;  
					yygl[164] = 281;  
					yygl[165] = 282;  
					yygl[166] = 282;  
					yygl[167] = 282;  
					yygl[168] = 283;  
					yygl[169] = 283;  
					yygl[170] = 285;  
					yygl[171] = 285;  
					yygl[172] = 285;  
					yygl[173] = 291;  
					yygl[174] = 293;  
					yygl[175] = 295;  
					yygl[176] = 295;  
					yygl[177] = 295;  
					yygl[178] = 295;  
					yygl[179] = 303;  
					yygl[180] = 303;  
					yygl[181] = 311;  
					yygl[182] = 319;  
					yygl[183] = 319;  
					yygl[184] = 320;  
					yygl[185] = 320;  
					yygl[186] = 320;  
					yygl[187] = 320;  
					yygl[188] = 320;  
					yygl[189] = 320;  
					yygl[190] = 321;  
					yygl[191] = 321;  
					yygl[192] = 321; 

					yygh = new int[yynstates];
					yygh[0] = 11;  
					yygh[1] = 11;  
					yygh[2] = 11;  
					yygh[3] = 11;  
					yygh[4] = 11;  
					yygh[5] = 11;  
					yygh[6] = 21;  
					yygh[7] = 21;  
					yygh[8] = 21;  
					yygh[9] = 31;  
					yygh[10] = 31;  
					yygh[11] = 31;  
					yygh[12] = 33;  
					yygh[13] = 35;  
					yygh[14] = 37;  
					yygh[15] = 37;  
					yygh[16] = 37;  
					yygh[17] = 37;  
					yygh[18] = 37;  
					yygh[19] = 37;  
					yygh[20] = 37;  
					yygh[21] = 37;  
					yygh[22] = 37;  
					yygh[23] = 37;  
					yygh[24] = 37;  
					yygh[25] = 37;  
					yygh[26] = 37;  
					yygh[27] = 37;  
					yygh[28] = 37;  
					yygh[29] = 37;  
					yygh[30] = 37;  
					yygh[31] = 37;  
					yygh[32] = 37;  
					yygh[33] = 37;  
					yygh[34] = 37;  
					yygh[35] = 37;  
					yygh[36] = 37;  
					yygh[37] = 37;  
					yygh[38] = 48;  
					yygh[39] = 48;  
					yygh[40] = 48;  
					yygh[41] = 48;  
					yygh[42] = 48;  
					yygh[43] = 48;  
					yygh[44] = 48;  
					yygh[45] = 48;  
					yygh[46] = 48;  
					yygh[47] = 48;  
					yygh[48] = 48;  
					yygh[49] = 48;  
					yygh[50] = 49;  
					yygh[51] = 49;  
					yygh[52] = 49;  
					yygh[53] = 49;  
					yygh[54] = 49;  
					yygh[55] = 49;  
					yygh[56] = 49;  
					yygh[57] = 49;  
					yygh[58] = 59;  
					yygh[59] = 61;  
					yygh[60] = 69;  
					yygh[61] = 77;  
					yygh[62] = 77;  
					yygh[63] = 83;  
					yygh[64] = 93;  
					yygh[65] = 93;  
					yygh[66] = 101;  
					yygh[67] = 101;  
					yygh[68] = 101;  
					yygh[69] = 101;  
					yygh[70] = 101;  
					yygh[71] = 101;  
					yygh[72] = 101;  
					yygh[73] = 101;  
					yygh[74] = 101;  
					yygh[75] = 101;  
					yygh[76] = 101;  
					yygh[77] = 109;  
					yygh[78] = 109;  
					yygh[79] = 109;  
					yygh[80] = 109;  
					yygh[81] = 119;  
					yygh[82] = 125;  
					yygh[83] = 126;  
					yygh[84] = 126;  
					yygh[85] = 127;  
					yygh[86] = 134;  
					yygh[87] = 134;  
					yygh[88] = 134;  
					yygh[89] = 134;  
					yygh[90] = 142;  
					yygh[91] = 142;  
					yygh[92] = 150;  
					yygh[93] = 150;  
					yygh[94] = 151;  
					yygh[95] = 161;  
					yygh[96] = 169;  
					yygh[97] = 170;  
					yygh[98] = 173;  
					yygh[99] = 173;  
					yygh[100] = 175;  
					yygh[101] = 175;  
					yygh[102] = 182;  
					yygh[103] = 182;  
					yygh[104] = 188;  
					yygh[105] = 196;  
					yygh[106] = 196;  
					yygh[107] = 202;  
					yygh[108] = 202;  
					yygh[109] = 202;  
					yygh[110] = 202;  
					yygh[111] = 207;  
					yygh[112] = 207;  
					yygh[113] = 207;  
					yygh[114] = 207;  
					yygh[115] = 207;  
					yygh[116] = 207;  
					yygh[117] = 207;  
					yygh[118] = 213;  
					yygh[119] = 214;  
					yygh[120] = 214;  
					yygh[121] = 214;  
					yygh[122] = 214;  
					yygh[123] = 214;  
					yygh[124] = 214;  
					yygh[125] = 214;  
					yygh[126] = 214;  
					yygh[127] = 214;  
					yygh[128] = 214;  
					yygh[129] = 214;  
					yygh[130] = 214;  
					yygh[131] = 219;  
					yygh[132] = 219;  
					yygh[133] = 219;  
					yygh[134] = 219;  
					yygh[135] = 219;  
					yygh[136] = 219;  
					yygh[137] = 219;  
					yygh[138] = 219;  
					yygh[139] = 220;  
					yygh[140] = 222;  
					yygh[141] = 229;  
					yygh[142] = 237;  
					yygh[143] = 238;  
					yygh[144] = 238;  
					yygh[145] = 246;  
					yygh[146] = 247;  
					yygh[147] = 247;  
					yygh[148] = 247;  
					yygh[149] = 249;  
					yygh[150] = 254;  
					yygh[151] = 254;  
					yygh[152] = 256;  
					yygh[153] = 263;  
					yygh[154] = 264;  
					yygh[155] = 269;  
					yygh[156] = 269;  
					yygh[157] = 269;  
					yygh[158] = 274;  
					yygh[159] = 274;  
					yygh[160] = 280;  
					yygh[161] = 280;  
					yygh[162] = 280;  
					yygh[163] = 280;  
					yygh[164] = 281;  
					yygh[165] = 281;  
					yygh[166] = 281;  
					yygh[167] = 282;  
					yygh[168] = 282;  
					yygh[169] = 284;  
					yygh[170] = 284;  
					yygh[171] = 284;  
					yygh[172] = 290;  
					yygh[173] = 292;  
					yygh[174] = 294;  
					yygh[175] = 294;  
					yygh[176] = 294;  
					yygh[177] = 294;  
					yygh[178] = 302;  
					yygh[179] = 302;  
					yygh[180] = 310;  
					yygh[181] = 318;  
					yygh[182] = 318;  
					yygh[183] = 319;  
					yygh[184] = 319;  
					yygh[185] = 319;  
					yygh[186] = 319;  
					yygh[187] = 319;  
					yygh[188] = 319;  
					yygh[189] = 320;  
					yygh[190] = 320;  
					yygh[191] = 320;  
					yygh[192] = 320; 

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
					yyr[yyrc] = new YYRRec(3,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(8,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-34);yyrc++;
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

			if (Regex.IsMatch(Rest,"^(;+)")){
				Results.Add (t_Char59);
				ResultsV.Add(Regex.Match(Rest,"^(;+)").Value);}

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
