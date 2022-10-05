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
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   11 : 
         yyval = "";
         
       break;
							case   12 : 
         yyval = yyv[yysp-0];
         
       break;
							case   13 : 
         yyval = yyv[yysp-0];
         
       break;
							case   14 : 
         yyval = yyv[yysp-0];
         
       break;
							case   15 : 
         yyval = yyv[yysp-0];
         
       break;
							case   16 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   17 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   18 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   19 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   20 : 
         yyval = yyv[yysp-0];
         
       break;
							case   21 : 
         yyval = yyv[yysp-0];
         
       break;
							case   22 : 
         yyval = yyv[yysp-0];
         
       break;
							case   23 : 
         yyval = yyv[yysp-0];
         
       break;
							case   24 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   25 : 
         yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   26 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   27 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   28 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   29 : 
         yyval = "";
         
       break;
							case   30 : 
         yyval = yyv[yysp-0];
         
       break;
							case   31 : 
         yyval = yyv[yysp-0];
         
       break;
							case   32 : 
         yyval = yyv[yysp-0];
         
       break;
							case   33 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   34 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   35 : 
         yyval = yyv[yysp-0];
         
       break;
							case   36 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = "";
         
       break;
							case   43 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   44 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   45 : 
         yyval = yyv[yysp-0];
         
       break;
							case   46 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   47 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   48 : 
         yyval = yyv[yysp-0];
         
       break;
							case   49 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   50 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   79 : 
         yyval = yyv[yysp-0];
         
       break;
							case   80 : 
         yyval = yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   81 : 
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   82 : 
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   83 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   84 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   85 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   90 : 
         yyval = yyv[yysp-0];
         
       break;
							case   91 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  102 : 
         yyval = yyv[yysp-0];
         
       break;
							case  103 : 
         yyval = yyv[yysp-0];
         
       break;
							case  104 : 
         yyval = "";
         
       break;
							case  105 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case  110 : 
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  111 : 
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

					int yynacts   = 1147;
					int yyngotos  = 331;
					int yynstates = 195;
					int yynrules  = 111;
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
					yya[yyac] = new YYARec(308,-104 );yyac++; 
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
					yya[yyac] = new YYARec(264,-42 );yyac++; 
					yya[yyac] = new YYARec(258,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(258,60);yyac++; 
					yya[yyac] = new YYARec(258,61);yyac++; 
					yya[yyac] = new YYARec(258,62);yyac++; 
					yya[yyac] = new YYARec(263,63);yyac++; 
					yya[yyac] = new YYARec(263,65);yyac++; 
					yya[yyac] = new YYARec(258,-35 );yyac++; 
					yya[yyac] = new YYARec(275,-42 );yyac++; 
					yya[yyac] = new YYARec(276,-42 );yyac++; 
					yya[yyac] = new YYARec(282,-42 );yyac++; 
					yya[yyac] = new YYARec(283,-42 );yyac++; 
					yya[yyac] = new YYARec(284,-42 );yyac++; 
					yya[yyac] = new YYARec(285,-42 );yyac++; 
					yya[yyac] = new YYARec(286,-42 );yyac++; 
					yya[yyac] = new YYARec(287,-42 );yyac++; 
					yya[yyac] = new YYARec(288,-42 );yyac++; 
					yya[yyac] = new YYARec(289,-42 );yyac++; 
					yya[yyac] = new YYARec(290,-42 );yyac++; 
					yya[yyac] = new YYARec(291,-42 );yyac++; 
					yya[yyac] = new YYARec(292,-42 );yyac++; 
					yya[yyac] = new YYARec(293,-42 );yyac++; 
					yya[yyac] = new YYARec(294,-42 );yyac++; 
					yya[yyac] = new YYARec(295,-42 );yyac++; 
					yya[yyac] = new YYARec(308,-42 );yyac++; 
					yya[yyac] = new YYARec(309,-42 );yyac++; 
					yya[yyac] = new YYARec(310,-42 );yyac++; 
					yya[yyac] = new YYARec(311,-42 );yyac++; 
					yya[yyac] = new YYARec(264,66);yyac++; 
					yya[yyac] = new YYARec(307,59);yyac++; 
					yya[yyac] = new YYARec(258,-102 );yyac++; 
					yya[yyac] = new YYARec(263,-102 );yyac++; 
					yya[yyac] = new YYARec(268,-102 );yyac++; 
					yya[yyac] = new YYARec(269,-102 );yyac++; 
					yya[yyac] = new YYARec(270,-102 );yyac++; 
					yya[yyac] = new YYARec(271,-102 );yyac++; 
					yya[yyac] = new YYARec(272,-102 );yyac++; 
					yya[yyac] = new YYARec(273,-102 );yyac++; 
					yya[yyac] = new YYARec(274,-102 );yyac++; 
					yya[yyac] = new YYARec(275,-102 );yyac++; 
					yya[yyac] = new YYARec(276,-102 );yyac++; 
					yya[yyac] = new YYARec(277,-102 );yyac++; 
					yya[yyac] = new YYARec(278,-102 );yyac++; 
					yya[yyac] = new YYARec(279,-102 );yyac++; 
					yya[yyac] = new YYARec(280,-102 );yyac++; 
					yya[yyac] = new YYARec(281,-102 );yyac++; 
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
					yya[yyac] = new YYARec(299,-102 );yyac++; 
					yya[yyac] = new YYARec(300,-102 );yyac++; 
					yya[yyac] = new YYARec(301,-102 );yyac++; 
					yya[yyac] = new YYARec(302,-102 );yyac++; 
					yya[yyac] = new YYARec(303,-102 );yyac++; 
					yya[yyac] = new YYARec(304,-102 );yyac++; 
					yya[yyac] = new YYARec(305,-102 );yyac++; 
					yya[yyac] = new YYARec(306,-102 );yyac++; 
					yya[yyac] = new YYARec(308,-102 );yyac++; 
					yya[yyac] = new YYARec(309,-102 );yyac++; 
					yya[yyac] = new YYARec(310,-102 );yyac++; 
					yya[yyac] = new YYARec(311,-102 );yyac++; 
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
					yya[yyac] = new YYARec(264,-41 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
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
					yya[yyac] = new YYARec(260,-11 );yyac++; 
					yya[yyac] = new YYARec(261,-11 );yyac++; 
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
					yya[yyac] = new YYARec(260,-11 );yyac++; 
					yya[yyac] = new YYARec(261,-11 );yyac++; 
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
					yya[yyac] = new YYARec(308,-104 );yyac++; 
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
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(267,83);yyac++; 
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
					yya[yyac] = new YYARec(296,84);yyac++; 
					yya[yyac] = new YYARec(297,85);yyac++; 
					yya[yyac] = new YYARec(298,86);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(260,87);yyac++; 
					yya[yyac] = new YYARec(261,88);yyac++; 
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
					yya[yyac] = new YYARec(260,-11 );yyac++; 
					yya[yyac] = new YYARec(261,-11 );yyac++; 
					yya[yyac] = new YYARec(258,90);yyac++; 
					yya[yyac] = new YYARec(258,91);yyac++; 
					yya[yyac] = new YYARec(264,92);yyac++; 
					yya[yyac] = new YYARec(267,83);yyac++; 
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
					yya[yyac] = new YYARec(296,84);yyac++; 
					yya[yyac] = new YYARec(297,85);yyac++; 
					yya[yyac] = new YYARec(298,86);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(258,94);yyac++; 
					yya[yyac] = new YYARec(265,95);yyac++; 
					yya[yyac] = new YYARec(263,97);yyac++; 
					yya[yyac] = new YYARec(266,98);yyac++; 
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
					yya[yyac] = new YYARec(258,-32 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(267,104);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-104 );yyac++; 
					yya[yyac] = new YYARec(283,-104 );yyac++; 
					yya[yyac] = new YYARec(284,-104 );yyac++; 
					yya[yyac] = new YYARec(285,-104 );yyac++; 
					yya[yyac] = new YYARec(286,-104 );yyac++; 
					yya[yyac] = new YYARec(287,-104 );yyac++; 
					yya[yyac] = new YYARec(288,-104 );yyac++; 
					yya[yyac] = new YYARec(289,-104 );yyac++; 
					yya[yyac] = new YYARec(290,-104 );yyac++; 
					yya[yyac] = new YYARec(291,-104 );yyac++; 
					yya[yyac] = new YYARec(292,-104 );yyac++; 
					yya[yyac] = new YYARec(293,-104 );yyac++; 
					yya[yyac] = new YYARec(294,-104 );yyac++; 
					yya[yyac] = new YYARec(295,-104 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(309,-104 );yyac++; 
					yya[yyac] = new YYARec(267,106);yyac++; 
					yya[yyac] = new YYARec(264,107);yyac++; 
					yya[yyac] = new YYARec(267,109);yyac++; 
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
					yya[yyac] = new YYARec(261,-11 );yyac++; 
					yya[yyac] = new YYARec(267,83);yyac++; 
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
					yya[yyac] = new YYARec(296,84);yyac++; 
					yya[yyac] = new YYARec(297,85);yyac++; 
					yya[yyac] = new YYARec(298,86);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(267,83);yyac++; 
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
					yya[yyac] = new YYARec(296,84);yyac++; 
					yya[yyac] = new YYARec(297,85);yyac++; 
					yya[yyac] = new YYARec(298,86);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(277,114);yyac++; 
					yya[yyac] = new YYARec(278,115);yyac++; 
					yya[yyac] = new YYARec(279,116);yyac++; 
					yya[yyac] = new YYARec(280,117);yyac++; 
					yya[yyac] = new YYARec(281,118);yyac++; 
					yya[yyac] = new YYARec(258,-40 );yyac++; 
					yya[yyac] = new YYARec(263,-40 );yyac++; 
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
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(267,83);yyac++; 
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
					yya[yyac] = new YYARec(296,84);yyac++; 
					yya[yyac] = new YYARec(297,85);yyac++; 
					yya[yyac] = new YYARec(298,86);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(268,121);yyac++; 
					yya[yyac] = new YYARec(299,122);yyac++; 
					yya[yyac] = new YYARec(300,123);yyac++; 
					yya[yyac] = new YYARec(301,124);yyac++; 
					yya[yyac] = new YYARec(302,125);yyac++; 
					yya[yyac] = new YYARec(303,126);yyac++; 
					yya[yyac] = new YYARec(304,127);yyac++; 
					yya[yyac] = new YYARec(305,128);yyac++; 
					yya[yyac] = new YYARec(306,129);yyac++; 
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
					yya[yyac] = new YYARec(269,134);yyac++; 
					yya[yyac] = new YYARec(270,135);yyac++; 
					yya[yyac] = new YYARec(271,136);yyac++; 
					yya[yyac] = new YYARec(272,137);yyac++; 
					yya[yyac] = new YYARec(273,138);yyac++; 
					yya[yyac] = new YYARec(274,139);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(268,-87 );yyac++; 
					yya[yyac] = new YYARec(299,-87 );yyac++; 
					yya[yyac] = new YYARec(300,-87 );yyac++; 
					yya[yyac] = new YYARec(301,-87 );yyac++; 
					yya[yyac] = new YYARec(302,-87 );yyac++; 
					yya[yyac] = new YYARec(303,-87 );yyac++; 
					yya[yyac] = new YYARec(304,-87 );yyac++; 
					yya[yyac] = new YYARec(305,-87 );yyac++; 
					yya[yyac] = new YYARec(306,-87 );yyac++; 
					yya[yyac] = new YYARec(267,143);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-104 );yyac++; 
					yya[yyac] = new YYARec(283,-104 );yyac++; 
					yya[yyac] = new YYARec(284,-104 );yyac++; 
					yya[yyac] = new YYARec(285,-104 );yyac++; 
					yya[yyac] = new YYARec(286,-104 );yyac++; 
					yya[yyac] = new YYARec(287,-104 );yyac++; 
					yya[yyac] = new YYARec(288,-104 );yyac++; 
					yya[yyac] = new YYARec(289,-104 );yyac++; 
					yya[yyac] = new YYARec(290,-104 );yyac++; 
					yya[yyac] = new YYARec(291,-104 );yyac++; 
					yya[yyac] = new YYARec(292,-104 );yyac++; 
					yya[yyac] = new YYARec(293,-104 );yyac++; 
					yya[yyac] = new YYARec(294,-104 );yyac++; 
					yya[yyac] = new YYARec(295,-104 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(309,-104 );yyac++; 
					yya[yyac] = new YYARec(264,144);yyac++; 
					yya[yyac] = new YYARec(267,104);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-104 );yyac++; 
					yya[yyac] = new YYARec(283,-104 );yyac++; 
					yya[yyac] = new YYARec(284,-104 );yyac++; 
					yya[yyac] = new YYARec(285,-104 );yyac++; 
					yya[yyac] = new YYARec(286,-104 );yyac++; 
					yya[yyac] = new YYARec(287,-104 );yyac++; 
					yya[yyac] = new YYARec(288,-104 );yyac++; 
					yya[yyac] = new YYARec(289,-104 );yyac++; 
					yya[yyac] = new YYARec(290,-104 );yyac++; 
					yya[yyac] = new YYARec(291,-104 );yyac++; 
					yya[yyac] = new YYARec(292,-104 );yyac++; 
					yya[yyac] = new YYARec(293,-104 );yyac++; 
					yya[yyac] = new YYARec(294,-104 );yyac++; 
					yya[yyac] = new YYARec(295,-104 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(309,-104 );yyac++; 
					yya[yyac] = new YYARec(267,83);yyac++; 
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
					yya[yyac] = new YYARec(296,84);yyac++; 
					yya[yyac] = new YYARec(297,85);yyac++; 
					yya[yyac] = new YYARec(298,86);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(264,147);yyac++; 
					yya[yyac] = new YYARec(267,104);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-104 );yyac++; 
					yya[yyac] = new YYARec(283,-104 );yyac++; 
					yya[yyac] = new YYARec(284,-104 );yyac++; 
					yya[yyac] = new YYARec(285,-104 );yyac++; 
					yya[yyac] = new YYARec(286,-104 );yyac++; 
					yya[yyac] = new YYARec(287,-104 );yyac++; 
					yya[yyac] = new YYARec(288,-104 );yyac++; 
					yya[yyac] = new YYARec(289,-104 );yyac++; 
					yya[yyac] = new YYARec(290,-104 );yyac++; 
					yya[yyac] = new YYARec(291,-104 );yyac++; 
					yya[yyac] = new YYARec(292,-104 );yyac++; 
					yya[yyac] = new YYARec(293,-104 );yyac++; 
					yya[yyac] = new YYARec(294,-104 );yyac++; 
					yya[yyac] = new YYARec(295,-104 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(309,-104 );yyac++; 
					yya[yyac] = new YYARec(261,149);yyac++; 
					yya[yyac] = new YYARec(265,150);yyac++; 
					yya[yyac] = new YYARec(267,152);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-104 );yyac++; 
					yya[yyac] = new YYARec(283,-104 );yyac++; 
					yya[yyac] = new YYARec(284,-104 );yyac++; 
					yya[yyac] = new YYARec(285,-104 );yyac++; 
					yya[yyac] = new YYARec(286,-104 );yyac++; 
					yya[yyac] = new YYARec(287,-104 );yyac++; 
					yya[yyac] = new YYARec(288,-104 );yyac++; 
					yya[yyac] = new YYARec(289,-104 );yyac++; 
					yya[yyac] = new YYARec(290,-104 );yyac++; 
					yya[yyac] = new YYARec(291,-104 );yyac++; 
					yya[yyac] = new YYARec(292,-104 );yyac++; 
					yya[yyac] = new YYARec(293,-104 );yyac++; 
					yya[yyac] = new YYARec(294,-104 );yyac++; 
					yya[yyac] = new YYARec(295,-104 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(309,-104 );yyac++; 
					yya[yyac] = new YYARec(267,155);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-104 );yyac++; 
					yya[yyac] = new YYARec(283,-104 );yyac++; 
					yya[yyac] = new YYARec(284,-104 );yyac++; 
					yya[yyac] = new YYARec(285,-104 );yyac++; 
					yya[yyac] = new YYARec(286,-104 );yyac++; 
					yya[yyac] = new YYARec(287,-104 );yyac++; 
					yya[yyac] = new YYARec(288,-104 );yyac++; 
					yya[yyac] = new YYARec(289,-104 );yyac++; 
					yya[yyac] = new YYARec(290,-104 );yyac++; 
					yya[yyac] = new YYARec(291,-104 );yyac++; 
					yya[yyac] = new YYARec(292,-104 );yyac++; 
					yya[yyac] = new YYARec(293,-104 );yyac++; 
					yya[yyac] = new YYARec(294,-104 );yyac++; 
					yya[yyac] = new YYARec(295,-104 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(309,-104 );yyac++; 
					yya[yyac] = new YYARec(299,122);yyac++; 
					yya[yyac] = new YYARec(300,123);yyac++; 
					yya[yyac] = new YYARec(301,124);yyac++; 
					yya[yyac] = new YYARec(302,125);yyac++; 
					yya[yyac] = new YYARec(303,126);yyac++; 
					yya[yyac] = new YYARec(304,127);yyac++; 
					yya[yyac] = new YYARec(305,128);yyac++; 
					yya[yyac] = new YYARec(306,129);yyac++; 
					yya[yyac] = new YYARec(267,157);yyac++; 
					yya[yyac] = new YYARec(258,-107 );yyac++; 
					yya[yyac] = new YYARec(268,-107 );yyac++; 
					yya[yyac] = new YYARec(269,-107 );yyac++; 
					yya[yyac] = new YYARec(270,-107 );yyac++; 
					yya[yyac] = new YYARec(271,-107 );yyac++; 
					yya[yyac] = new YYARec(272,-107 );yyac++; 
					yya[yyac] = new YYARec(273,-107 );yyac++; 
					yya[yyac] = new YYARec(274,-107 );yyac++; 
					yya[yyac] = new YYARec(275,-107 );yyac++; 
					yya[yyac] = new YYARec(276,-107 );yyac++; 
					yya[yyac] = new YYARec(299,-107 );yyac++; 
					yya[yyac] = new YYARec(300,-107 );yyac++; 
					yya[yyac] = new YYARec(301,-107 );yyac++; 
					yya[yyac] = new YYARec(302,-107 );yyac++; 
					yya[yyac] = new YYARec(303,-107 );yyac++; 
					yya[yyac] = new YYARec(304,-107 );yyac++; 
					yya[yyac] = new YYARec(305,-107 );yyac++; 
					yya[yyac] = new YYARec(306,-107 );yyac++; 
					yya[yyac] = new YYARec(307,-107 );yyac++; 
					yya[yyac] = new YYARec(267,160);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-104 );yyac++; 
					yya[yyac] = new YYARec(283,-104 );yyac++; 
					yya[yyac] = new YYARec(284,-104 );yyac++; 
					yya[yyac] = new YYARec(285,-104 );yyac++; 
					yya[yyac] = new YYARec(286,-104 );yyac++; 
					yya[yyac] = new YYARec(287,-104 );yyac++; 
					yya[yyac] = new YYARec(288,-104 );yyac++; 
					yya[yyac] = new YYARec(289,-104 );yyac++; 
					yya[yyac] = new YYARec(290,-104 );yyac++; 
					yya[yyac] = new YYARec(291,-104 );yyac++; 
					yya[yyac] = new YYARec(292,-104 );yyac++; 
					yya[yyac] = new YYARec(293,-104 );yyac++; 
					yya[yyac] = new YYARec(294,-104 );yyac++; 
					yya[yyac] = new YYARec(295,-104 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(309,-104 );yyac++; 
					yya[yyac] = new YYARec(268,161);yyac++; 
					yya[yyac] = new YYARec(299,122);yyac++; 
					yya[yyac] = new YYARec(300,123);yyac++; 
					yya[yyac] = new YYARec(301,124);yyac++; 
					yya[yyac] = new YYARec(302,125);yyac++; 
					yya[yyac] = new YYARec(303,126);yyac++; 
					yya[yyac] = new YYARec(304,127);yyac++; 
					yya[yyac] = new YYARec(305,128);yyac++; 
					yya[yyac] = new YYARec(306,129);yyac++; 
					yya[yyac] = new YYARec(268,163);yyac++; 
					yya[yyac] = new YYARec(269,134);yyac++; 
					yya[yyac] = new YYARec(270,135);yyac++; 
					yya[yyac] = new YYARec(271,136);yyac++; 
					yya[yyac] = new YYARec(272,137);yyac++; 
					yya[yyac] = new YYARec(273,138);yyac++; 
					yya[yyac] = new YYARec(274,139);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(299,-87 );yyac++; 
					yya[yyac] = new YYARec(300,-87 );yyac++; 
					yya[yyac] = new YYARec(301,-87 );yyac++; 
					yya[yyac] = new YYARec(302,-87 );yyac++; 
					yya[yyac] = new YYARec(303,-87 );yyac++; 
					yya[yyac] = new YYARec(304,-87 );yyac++; 
					yya[yyac] = new YYARec(305,-87 );yyac++; 
					yya[yyac] = new YYARec(306,-87 );yyac++; 
					yya[yyac] = new YYARec(267,143);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-104 );yyac++; 
					yya[yyac] = new YYARec(283,-104 );yyac++; 
					yya[yyac] = new YYARec(284,-104 );yyac++; 
					yya[yyac] = new YYARec(285,-104 );yyac++; 
					yya[yyac] = new YYARec(286,-104 );yyac++; 
					yya[yyac] = new YYARec(287,-104 );yyac++; 
					yya[yyac] = new YYARec(288,-104 );yyac++; 
					yya[yyac] = new YYARec(289,-104 );yyac++; 
					yya[yyac] = new YYARec(290,-104 );yyac++; 
					yya[yyac] = new YYARec(291,-104 );yyac++; 
					yya[yyac] = new YYARec(292,-104 );yyac++; 
					yya[yyac] = new YYARec(293,-104 );yyac++; 
					yya[yyac] = new YYARec(294,-104 );yyac++; 
					yya[yyac] = new YYARec(295,-104 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(309,-104 );yyac++; 
					yya[yyac] = new YYARec(267,83);yyac++; 
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
					yya[yyac] = new YYARec(296,84);yyac++; 
					yya[yyac] = new YYARec(297,85);yyac++; 
					yya[yyac] = new YYARec(298,86);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(268,166);yyac++; 
					yya[yyac] = new YYARec(299,122);yyac++; 
					yya[yyac] = new YYARec(300,123);yyac++; 
					yya[yyac] = new YYARec(301,124);yyac++; 
					yya[yyac] = new YYARec(302,125);yyac++; 
					yya[yyac] = new YYARec(303,126);yyac++; 
					yya[yyac] = new YYARec(304,127);yyac++; 
					yya[yyac] = new YYARec(305,128);yyac++; 
					yya[yyac] = new YYARec(306,129);yyac++; 
					yya[yyac] = new YYARec(265,167);yyac++; 
					yya[yyac] = new YYARec(267,83);yyac++; 
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
					yya[yyac] = new YYARec(296,84);yyac++; 
					yya[yyac] = new YYARec(297,85);yyac++; 
					yya[yyac] = new YYARec(298,86);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(268,169);yyac++; 
					yya[yyac] = new YYARec(299,122);yyac++; 
					yya[yyac] = new YYARec(300,123);yyac++; 
					yya[yyac] = new YYARec(301,124);yyac++; 
					yya[yyac] = new YYARec(302,125);yyac++; 
					yya[yyac] = new YYARec(303,126);yyac++; 
					yya[yyac] = new YYARec(304,127);yyac++; 
					yya[yyac] = new YYARec(305,128);yyac++; 
					yya[yyac] = new YYARec(306,129);yyac++; 
					yya[yyac] = new YYARec(297,170);yyac++; 
					yya[yyac] = new YYARec(269,134);yyac++; 
					yya[yyac] = new YYARec(270,135);yyac++; 
					yya[yyac] = new YYARec(271,136);yyac++; 
					yya[yyac] = new YYARec(272,137);yyac++; 
					yya[yyac] = new YYARec(273,138);yyac++; 
					yya[yyac] = new YYARec(274,139);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(258,-43 );yyac++; 
					yya[yyac] = new YYARec(267,152);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-104 );yyac++; 
					yya[yyac] = new YYARec(283,-104 );yyac++; 
					yya[yyac] = new YYARec(284,-104 );yyac++; 
					yya[yyac] = new YYARec(285,-104 );yyac++; 
					yya[yyac] = new YYARec(286,-104 );yyac++; 
					yya[yyac] = new YYARec(287,-104 );yyac++; 
					yya[yyac] = new YYARec(288,-104 );yyac++; 
					yya[yyac] = new YYARec(289,-104 );yyac++; 
					yya[yyac] = new YYARec(290,-104 );yyac++; 
					yya[yyac] = new YYARec(291,-104 );yyac++; 
					yya[yyac] = new YYARec(292,-104 );yyac++; 
					yya[yyac] = new YYARec(293,-104 );yyac++; 
					yya[yyac] = new YYARec(294,-104 );yyac++; 
					yya[yyac] = new YYARec(295,-104 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(309,-104 );yyac++; 
					yya[yyac] = new YYARec(269,134);yyac++; 
					yya[yyac] = new YYARec(270,135);yyac++; 
					yya[yyac] = new YYARec(271,136);yyac++; 
					yya[yyac] = new YYARec(272,137);yyac++; 
					yya[yyac] = new YYARec(273,138);yyac++; 
					yya[yyac] = new YYARec(274,139);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(268,-90 );yyac++; 
					yya[yyac] = new YYARec(299,-90 );yyac++; 
					yya[yyac] = new YYARec(300,-90 );yyac++; 
					yya[yyac] = new YYARec(301,-90 );yyac++; 
					yya[yyac] = new YYARec(302,-90 );yyac++; 
					yya[yyac] = new YYARec(303,-90 );yyac++; 
					yya[yyac] = new YYARec(304,-90 );yyac++; 
					yya[yyac] = new YYARec(305,-90 );yyac++; 
					yya[yyac] = new YYARec(306,-90 );yyac++; 
					yya[yyac] = new YYARec(267,143);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-104 );yyac++; 
					yya[yyac] = new YYARec(283,-104 );yyac++; 
					yya[yyac] = new YYARec(284,-104 );yyac++; 
					yya[yyac] = new YYARec(285,-104 );yyac++; 
					yya[yyac] = new YYARec(286,-104 );yyac++; 
					yya[yyac] = new YYARec(287,-104 );yyac++; 
					yya[yyac] = new YYARec(288,-104 );yyac++; 
					yya[yyac] = new YYARec(289,-104 );yyac++; 
					yya[yyac] = new YYARec(290,-104 );yyac++; 
					yya[yyac] = new YYARec(291,-104 );yyac++; 
					yya[yyac] = new YYARec(292,-104 );yyac++; 
					yya[yyac] = new YYARec(293,-104 );yyac++; 
					yya[yyac] = new YYARec(294,-104 );yyac++; 
					yya[yyac] = new YYARec(295,-104 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(309,-104 );yyac++; 
					yya[yyac] = new YYARec(267,174);yyac++; 
					yya[yyac] = new YYARec(267,152);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-104 );yyac++; 
					yya[yyac] = new YYARec(283,-104 );yyac++; 
					yya[yyac] = new YYARec(284,-104 );yyac++; 
					yya[yyac] = new YYARec(285,-104 );yyac++; 
					yya[yyac] = new YYARec(286,-104 );yyac++; 
					yya[yyac] = new YYARec(287,-104 );yyac++; 
					yya[yyac] = new YYARec(288,-104 );yyac++; 
					yya[yyac] = new YYARec(289,-104 );yyac++; 
					yya[yyac] = new YYARec(290,-104 );yyac++; 
					yya[yyac] = new YYARec(291,-104 );yyac++; 
					yya[yyac] = new YYARec(292,-104 );yyac++; 
					yya[yyac] = new YYARec(293,-104 );yyac++; 
					yya[yyac] = new YYARec(294,-104 );yyac++; 
					yya[yyac] = new YYARec(295,-104 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(309,-104 );yyac++; 
					yya[yyac] = new YYARec(267,152);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-104 );yyac++; 
					yya[yyac] = new YYARec(283,-104 );yyac++; 
					yya[yyac] = new YYARec(284,-104 );yyac++; 
					yya[yyac] = new YYARec(285,-104 );yyac++; 
					yya[yyac] = new YYARec(286,-104 );yyac++; 
					yya[yyac] = new YYARec(287,-104 );yyac++; 
					yya[yyac] = new YYARec(288,-104 );yyac++; 
					yya[yyac] = new YYARec(289,-104 );yyac++; 
					yya[yyac] = new YYARec(290,-104 );yyac++; 
					yya[yyac] = new YYARec(291,-104 );yyac++; 
					yya[yyac] = new YYARec(292,-104 );yyac++; 
					yya[yyac] = new YYARec(293,-104 );yyac++; 
					yya[yyac] = new YYARec(294,-104 );yyac++; 
					yya[yyac] = new YYARec(295,-104 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(309,-104 );yyac++; 
					yya[yyac] = new YYARec(267,155);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-104 );yyac++; 
					yya[yyac] = new YYARec(283,-104 );yyac++; 
					yya[yyac] = new YYARec(284,-104 );yyac++; 
					yya[yyac] = new YYARec(285,-104 );yyac++; 
					yya[yyac] = new YYARec(286,-104 );yyac++; 
					yya[yyac] = new YYARec(287,-104 );yyac++; 
					yya[yyac] = new YYARec(288,-104 );yyac++; 
					yya[yyac] = new YYARec(289,-104 );yyac++; 
					yya[yyac] = new YYARec(290,-104 );yyac++; 
					yya[yyac] = new YYARec(291,-104 );yyac++; 
					yya[yyac] = new YYARec(292,-104 );yyac++; 
					yya[yyac] = new YYARec(293,-104 );yyac++; 
					yya[yyac] = new YYARec(294,-104 );yyac++; 
					yya[yyac] = new YYARec(295,-104 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(309,-104 );yyac++; 
					yya[yyac] = new YYARec(268,178);yyac++; 
					yya[yyac] = new YYARec(265,179);yyac++; 
					yya[yyac] = new YYARec(264,180);yyac++; 
					yya[yyac] = new YYARec(299,122);yyac++; 
					yya[yyac] = new YYARec(300,123);yyac++; 
					yya[yyac] = new YYARec(301,124);yyac++; 
					yya[yyac] = new YYARec(302,125);yyac++; 
					yya[yyac] = new YYARec(303,126);yyac++; 
					yya[yyac] = new YYARec(304,127);yyac++; 
					yya[yyac] = new YYARec(305,128);yyac++; 
					yya[yyac] = new YYARec(306,129);yyac++; 
					yya[yyac] = new YYARec(265,181);yyac++; 
					yya[yyac] = new YYARec(264,182);yyac++; 
					yya[yyac] = new YYARec(299,122);yyac++; 
					yya[yyac] = new YYARec(300,123);yyac++; 
					yya[yyac] = new YYARec(301,124);yyac++; 
					yya[yyac] = new YYARec(302,125);yyac++; 
					yya[yyac] = new YYARec(303,126);yyac++; 
					yya[yyac] = new YYARec(304,127);yyac++; 
					yya[yyac] = new YYARec(305,128);yyac++; 
					yya[yyac] = new YYARec(306,129);yyac++; 
					yya[yyac] = new YYARec(264,183);yyac++; 
					yya[yyac] = new YYARec(268,163);yyac++; 
					yya[yyac] = new YYARec(269,134);yyac++; 
					yya[yyac] = new YYARec(270,135);yyac++; 
					yya[yyac] = new YYARec(271,136);yyac++; 
					yya[yyac] = new YYARec(272,137);yyac++; 
					yya[yyac] = new YYARec(273,138);yyac++; 
					yya[yyac] = new YYARec(274,139);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(268,184);yyac++; 
					yya[yyac] = new YYARec(267,104);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(282,-104 );yyac++; 
					yya[yyac] = new YYARec(283,-104 );yyac++; 
					yya[yyac] = new YYARec(284,-104 );yyac++; 
					yya[yyac] = new YYARec(285,-104 );yyac++; 
					yya[yyac] = new YYARec(286,-104 );yyac++; 
					yya[yyac] = new YYARec(287,-104 );yyac++; 
					yya[yyac] = new YYARec(288,-104 );yyac++; 
					yya[yyac] = new YYARec(289,-104 );yyac++; 
					yya[yyac] = new YYARec(290,-104 );yyac++; 
					yya[yyac] = new YYARec(291,-104 );yyac++; 
					yya[yyac] = new YYARec(292,-104 );yyac++; 
					yya[yyac] = new YYARec(293,-104 );yyac++; 
					yya[yyac] = new YYARec(294,-104 );yyac++; 
					yya[yyac] = new YYARec(295,-104 );yyac++; 
					yya[yyac] = new YYARec(308,-104 );yyac++; 
					yya[yyac] = new YYARec(309,-104 );yyac++; 
					yya[yyac] = new YYARec(268,186);yyac++; 
					yya[yyac] = new YYARec(269,134);yyac++; 
					yya[yyac] = new YYARec(270,135);yyac++; 
					yya[yyac] = new YYARec(271,136);yyac++; 
					yya[yyac] = new YYARec(272,137);yyac++; 
					yya[yyac] = new YYARec(273,138);yyac++; 
					yya[yyac] = new YYARec(274,139);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(268,187);yyac++; 
					yya[yyac] = new YYARec(269,134);yyac++; 
					yya[yyac] = new YYARec(270,135);yyac++; 
					yya[yyac] = new YYARec(271,136);yyac++; 
					yya[yyac] = new YYARec(272,137);yyac++; 
					yya[yyac] = new YYARec(273,138);yyac++; 
					yya[yyac] = new YYARec(274,139);yyac++; 
					yya[yyac] = new YYARec(275,39);yyac++; 
					yya[yyac] = new YYARec(276,40);yyac++; 
					yya[yyac] = new YYARec(299,-88 );yyac++; 
					yya[yyac] = new YYARec(300,-88 );yyac++; 
					yya[yyac] = new YYARec(301,-88 );yyac++; 
					yya[yyac] = new YYARec(302,-88 );yyac++; 
					yya[yyac] = new YYARec(303,-88 );yyac++; 
					yya[yyac] = new YYARec(304,-88 );yyac++; 
					yya[yyac] = new YYARec(305,-88 );yyac++; 
					yya[yyac] = new YYARec(306,-88 );yyac++; 
					yya[yyac] = new YYARec(268,-92 );yyac++; 
					yya[yyac] = new YYARec(299,-86 );yyac++; 
					yya[yyac] = new YYARec(300,-86 );yyac++; 
					yya[yyac] = new YYARec(301,-86 );yyac++; 
					yya[yyac] = new YYARec(302,-86 );yyac++; 
					yya[yyac] = new YYARec(303,-86 );yyac++; 
					yya[yyac] = new YYARec(304,-86 );yyac++; 
					yya[yyac] = new YYARec(305,-86 );yyac++; 
					yya[yyac] = new YYARec(306,-86 );yyac++; 
					yya[yyac] = new YYARec(268,-91 );yyac++; 
					yya[yyac] = new YYARec(267,83);yyac++; 
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
					yya[yyac] = new YYARec(296,84);yyac++; 
					yya[yyac] = new YYARec(297,85);yyac++; 
					yya[yyac] = new YYARec(298,86);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(267,83);yyac++; 
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
					yya[yyac] = new YYARec(296,84);yyac++; 
					yya[yyac] = new YYARec(297,85);yyac++; 
					yya[yyac] = new YYARec(298,86);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(267,83);yyac++; 
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
					yya[yyac] = new YYARec(296,84);yyac++; 
					yya[yyac] = new YYARec(297,85);yyac++; 
					yya[yyac] = new YYARec(298,86);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(268,191);yyac++; 
					yya[yyac] = new YYARec(299,122);yyac++; 
					yya[yyac] = new YYARec(300,123);yyac++; 
					yya[yyac] = new YYARec(301,124);yyac++; 
					yya[yyac] = new YYARec(302,125);yyac++; 
					yya[yyac] = new YYARec(303,126);yyac++; 
					yya[yyac] = new YYARec(304,127);yyac++; 
					yya[yyac] = new YYARec(305,128);yyac++; 
					yya[yyac] = new YYARec(306,129);yyac++; 
					yya[yyac] = new YYARec(265,192);yyac++; 
					yya[yyac] = new YYARec(265,193);yyac++; 
					yya[yyac] = new YYARec(265,194);yyac++; 
					yya[yyac] = new YYARec(299,122);yyac++; 
					yya[yyac] = new YYARec(300,123);yyac++; 
					yya[yyac] = new YYARec(301,124);yyac++; 
					yya[yyac] = new YYARec(302,125);yyac++; 
					yya[yyac] = new YYARec(303,126);yyac++; 
					yya[yyac] = new YYARec(304,127);yyac++; 
					yya[yyac] = new YYARec(305,128);yyac++; 
					yya[yyac] = new YYARec(306,129);yyac++; 
					yya[yyac] = new YYARec(264,-110 );yyac++;

					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-13,2);yygc++; 
					yyg[yygc] = new YYARec(-12,3);yygc++; 
					yyg[yygc] = new YYARec(-11,4);yygc++; 
					yyg[yygc] = new YYARec(-10,5);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-6,7);yygc++; 
					yyg[yygc] = new YYARec(-5,8);yygc++; 
					yyg[yygc] = new YYARec(-4,9);yygc++; 
					yyg[yygc] = new YYARec(-3,10);yygc++; 
					yyg[yygc] = new YYARec(-2,11);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-32,31);yygc++; 
					yyg[yygc] = new YYARec(-19,32);yygc++; 
					yyg[yygc] = new YYARec(-18,33);yygc++; 
					yyg[yygc] = new YYARec(-17,34);yygc++; 
					yyg[yygc] = new YYARec(-16,35);yygc++; 
					yyg[yygc] = new YYARec(-15,36);yygc++; 
					yyg[yygc] = new YYARec(-14,37);yygc++; 
					yyg[yygc] = new YYARec(-7,38);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-13,2);yygc++; 
					yyg[yygc] = new YYARec(-12,3);yygc++; 
					yyg[yygc] = new YYARec(-11,4);yygc++; 
					yyg[yygc] = new YYARec(-10,5);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-6,7);yygc++; 
					yyg[yygc] = new YYARec(-5,8);yygc++; 
					yyg[yygc] = new YYARec(-4,9);yygc++; 
					yyg[yygc] = new YYARec(-3,43);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-7,44);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-7,45);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-7,46);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-32,31);yygc++; 
					yyg[yygc] = new YYARec(-26,50);yygc++; 
					yyg[yygc] = new YYARec(-25,51);yygc++; 
					yyg[yygc] = new YYARec(-20,52);yygc++; 
					yyg[yygc] = new YYARec(-18,53);yygc++; 
					yyg[yygc] = new YYARec(-17,54);yygc++; 
					yyg[yygc] = new YYARec(-15,55);yygc++; 
					yyg[yygc] = new YYARec(-14,56);yygc++; 
					yyg[yygc] = new YYARec(-7,57);yygc++; 
					yyg[yygc] = new YYARec(-20,64);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-32,31);yygc++; 
					yyg[yygc] = new YYARec(-26,50);yygc++; 
					yyg[yygc] = new YYARec(-25,67);yygc++; 
					yyg[yygc] = new YYARec(-18,53);yygc++; 
					yyg[yygc] = new YYARec(-17,54);yygc++; 
					yyg[yygc] = new YYARec(-15,55);yygc++; 
					yyg[yygc] = new YYARec(-14,56);yygc++; 
					yyg[yygc] = new YYARec(-7,57);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-7,68);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-13,2);yygc++; 
					yyg[yygc] = new YYARec(-12,3);yygc++; 
					yyg[yygc] = new YYARec(-11,4);yygc++; 
					yyg[yygc] = new YYARec(-10,5);yygc++; 
					yyg[yygc] = new YYARec(-9,69);yygc++; 
					yyg[yygc] = new YYARec(-8,70);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-5,71);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-13,2);yygc++; 
					yyg[yygc] = new YYARec(-12,3);yygc++; 
					yyg[yygc] = new YYARec(-11,4);yygc++; 
					yyg[yygc] = new YYARec(-10,5);yygc++; 
					yyg[yygc] = new YYARec(-9,69);yygc++; 
					yyg[yygc] = new YYARec(-8,72);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-5,71);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-32,31);yygc++; 
					yyg[yygc] = new YYARec(-15,73);yygc++; 
					yyg[yygc] = new YYARec(-14,74);yygc++; 
					yyg[yygc] = new YYARec(-7,57);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-32,31);yygc++; 
					yyg[yygc] = new YYARec(-26,50);yygc++; 
					yyg[yygc] = new YYARec(-25,75);yygc++; 
					yyg[yygc] = new YYARec(-18,53);yygc++; 
					yyg[yygc] = new YYARec(-17,54);yygc++; 
					yyg[yygc] = new YYARec(-15,55);yygc++; 
					yyg[yygc] = new YYARec(-14,56);yygc++; 
					yyg[yygc] = new YYARec(-7,57);yygc++; 
					yyg[yygc] = new YYARec(-35,76);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-24,77);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-21,80);yygc++; 
					yyg[yygc] = new YYARec(-19,81);yygc++; 
					yyg[yygc] = new YYARec(-7,82);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-13,2);yygc++; 
					yyg[yygc] = new YYARec(-12,3);yygc++; 
					yyg[yygc] = new YYARec(-11,4);yygc++; 
					yyg[yygc] = new YYARec(-10,5);yygc++; 
					yyg[yygc] = new YYARec(-9,89);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-5,71);yygc++; 
					yyg[yygc] = new YYARec(-35,76);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-24,77);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-21,93);yygc++; 
					yyg[yygc] = new YYARec(-19,81);yygc++; 
					yyg[yygc] = new YYARec(-7,82);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-32,31);yygc++; 
					yyg[yygc] = new YYARec(-26,50);yygc++; 
					yyg[yygc] = new YYARec(-25,51);yygc++; 
					yyg[yygc] = new YYARec(-18,53);yygc++; 
					yyg[yygc] = new YYARec(-17,54);yygc++; 
					yyg[yygc] = new YYARec(-15,55);yygc++; 
					yyg[yygc] = new YYARec(-14,96);yygc++; 
					yyg[yygc] = new YYARec(-7,57);yygc++; 
					yyg[yygc] = new YYARec(-36,99);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-32,100);yygc++; 
					yyg[yygc] = new YYARec(-29,101);yygc++; 
					yyg[yygc] = new YYARec(-28,102);yygc++; 
					yyg[yygc] = new YYARec(-15,103);yygc++; 
					yyg[yygc] = new YYARec(-35,105);yygc++; 
					yyg[yygc] = new YYARec(-35,108);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-13,2);yygc++; 
					yyg[yygc] = new YYARec(-12,3);yygc++; 
					yyg[yygc] = new YYARec(-11,4);yygc++; 
					yyg[yygc] = new YYARec(-10,5);yygc++; 
					yyg[yygc] = new YYARec(-9,110);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-5,71);yygc++; 
					yyg[yygc] = new YYARec(-35,76);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-24,77);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-21,111);yygc++; 
					yyg[yygc] = new YYARec(-19,81);yygc++; 
					yyg[yygc] = new YYARec(-7,82);yygc++; 
					yyg[yygc] = new YYARec(-35,76);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-24,77);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-21,112);yygc++; 
					yyg[yygc] = new YYARec(-19,81);yygc++; 
					yyg[yygc] = new YYARec(-7,82);yygc++; 
					yyg[yygc] = new YYARec(-27,113);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-32,31);yygc++; 
					yyg[yygc] = new YYARec(-26,50);yygc++; 
					yyg[yygc] = new YYARec(-25,67);yygc++; 
					yyg[yygc] = new YYARec(-18,53);yygc++; 
					yyg[yygc] = new YYARec(-17,54);yygc++; 
					yyg[yygc] = new YYARec(-15,55);yygc++; 
					yyg[yygc] = new YYARec(-14,56);yygc++; 
					yyg[yygc] = new YYARec(-7,57);yygc++; 
					yyg[yygc] = new YYARec(-35,76);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-24,77);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-21,119);yygc++; 
					yyg[yygc] = new YYARec(-19,81);yygc++; 
					yyg[yygc] = new YYARec(-7,82);yygc++; 
					yyg[yygc] = new YYARec(-38,120);yygc++; 
					yyg[yygc] = new YYARec(-33,130);yygc++; 
					yyg[yygc] = new YYARec(-14,131);yygc++; 
					yyg[yygc] = new YYARec(-7,57);yygc++; 
					yyg[yygc] = new YYARec(-34,132);yygc++; 
					yyg[yygc] = new YYARec(-30,133);yygc++; 
					yyg[yygc] = new YYARec(-37,140);yygc++; 
					yyg[yygc] = new YYARec(-36,141);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-32,100);yygc++; 
					yyg[yygc] = new YYARec(-29,101);yygc++; 
					yyg[yygc] = new YYARec(-28,142);yygc++; 
					yyg[yygc] = new YYARec(-15,103);yygc++; 
					yyg[yygc] = new YYARec(-36,145);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-32,100);yygc++; 
					yyg[yygc] = new YYARec(-29,101);yygc++; 
					yyg[yygc] = new YYARec(-28,102);yygc++; 
					yyg[yygc] = new YYARec(-15,103);yygc++; 
					yyg[yygc] = new YYARec(-35,76);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-24,77);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-21,146);yygc++; 
					yyg[yygc] = new YYARec(-19,81);yygc++; 
					yyg[yygc] = new YYARec(-7,82);yygc++; 
					yyg[yygc] = new YYARec(-36,148);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-32,100);yygc++; 
					yyg[yygc] = new YYARec(-29,101);yygc++; 
					yyg[yygc] = new YYARec(-28,102);yygc++; 
					yyg[yygc] = new YYARec(-15,103);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-32,100);yygc++; 
					yyg[yygc] = new YYARec(-29,101);yygc++; 
					yyg[yygc] = new YYARec(-28,151);yygc++; 
					yyg[yygc] = new YYARec(-15,103);yygc++; 
					yyg[yygc] = new YYARec(-39,153);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-32,100);yygc++; 
					yyg[yygc] = new YYARec(-29,101);yygc++; 
					yyg[yygc] = new YYARec(-28,154);yygc++; 
					yyg[yygc] = new YYARec(-15,103);yygc++; 
					yyg[yygc] = new YYARec(-38,156);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-32,100);yygc++; 
					yyg[yygc] = new YYARec(-31,158);yygc++; 
					yyg[yygc] = new YYARec(-29,159);yygc++; 
					yyg[yygc] = new YYARec(-15,103);yygc++; 
					yyg[yygc] = new YYARec(-38,162);yygc++; 
					yyg[yygc] = new YYARec(-34,132);yygc++; 
					yyg[yygc] = new YYARec(-30,133);yygc++; 
					yyg[yygc] = new YYARec(-37,164);yygc++; 
					yyg[yygc] = new YYARec(-36,141);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-32,100);yygc++; 
					yyg[yygc] = new YYARec(-29,101);yygc++; 
					yyg[yygc] = new YYARec(-28,142);yygc++; 
					yyg[yygc] = new YYARec(-15,103);yygc++; 
					yyg[yygc] = new YYARec(-35,76);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-24,77);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-21,165);yygc++; 
					yyg[yygc] = new YYARec(-19,81);yygc++; 
					yyg[yygc] = new YYARec(-7,82);yygc++; 
					yyg[yygc] = new YYARec(-38,120);yygc++; 
					yyg[yygc] = new YYARec(-35,76);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-24,77);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-21,168);yygc++; 
					yyg[yygc] = new YYARec(-19,81);yygc++; 
					yyg[yygc] = new YYARec(-7,82);yygc++; 
					yyg[yygc] = new YYARec(-38,120);yygc++; 
					yyg[yygc] = new YYARec(-34,132);yygc++; 
					yyg[yygc] = new YYARec(-30,133);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-32,100);yygc++; 
					yyg[yygc] = new YYARec(-29,101);yygc++; 
					yyg[yygc] = new YYARec(-28,171);yygc++; 
					yyg[yygc] = new YYARec(-15,103);yygc++; 
					yyg[yygc] = new YYARec(-34,132);yygc++; 
					yyg[yygc] = new YYARec(-30,133);yygc++; 
					yyg[yygc] = new YYARec(-37,172);yygc++; 
					yyg[yygc] = new YYARec(-36,141);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-32,100);yygc++; 
					yyg[yygc] = new YYARec(-29,101);yygc++; 
					yyg[yygc] = new YYARec(-28,142);yygc++; 
					yyg[yygc] = new YYARec(-15,103);yygc++; 
					yyg[yygc] = new YYARec(-35,173);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-32,100);yygc++; 
					yyg[yygc] = new YYARec(-29,101);yygc++; 
					yyg[yygc] = new YYARec(-28,175);yygc++; 
					yyg[yygc] = new YYARec(-15,103);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-32,100);yygc++; 
					yyg[yygc] = new YYARec(-29,101);yygc++; 
					yyg[yygc] = new YYARec(-28,176);yygc++; 
					yyg[yygc] = new YYARec(-15,103);yygc++; 
					yyg[yygc] = new YYARec(-39,177);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-32,100);yygc++; 
					yyg[yygc] = new YYARec(-29,101);yygc++; 
					yyg[yygc] = new YYARec(-28,154);yygc++; 
					yyg[yygc] = new YYARec(-15,103);yygc++; 
					yyg[yygc] = new YYARec(-38,156);yygc++; 
					yyg[yygc] = new YYARec(-38,156);yygc++; 
					yyg[yygc] = new YYARec(-34,132);yygc++; 
					yyg[yygc] = new YYARec(-30,133);yygc++; 
					yyg[yygc] = new YYARec(-36,185);yygc++; 
					yyg[yygc] = new YYARec(-34,30);yygc++; 
					yyg[yygc] = new YYARec(-32,100);yygc++; 
					yyg[yygc] = new YYARec(-29,101);yygc++; 
					yyg[yygc] = new YYARec(-28,102);yygc++; 
					yyg[yygc] = new YYARec(-15,103);yygc++; 
					yyg[yygc] = new YYARec(-34,132);yygc++; 
					yyg[yygc] = new YYARec(-30,133);yygc++; 
					yyg[yygc] = new YYARec(-34,132);yygc++; 
					yyg[yygc] = new YYARec(-30,133);yygc++; 
					yyg[yygc] = new YYARec(-35,76);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-24,77);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-21,188);yygc++; 
					yyg[yygc] = new YYARec(-19,81);yygc++; 
					yyg[yygc] = new YYARec(-7,82);yygc++; 
					yyg[yygc] = new YYARec(-35,76);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-24,77);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-21,189);yygc++; 
					yyg[yygc] = new YYARec(-19,81);yygc++; 
					yyg[yygc] = new YYARec(-7,82);yygc++; 
					yyg[yygc] = new YYARec(-35,76);yygc++; 
					yyg[yygc] = new YYARec(-33,1);yygc++; 
					yyg[yygc] = new YYARec(-24,77);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-21,190);yygc++; 
					yyg[yygc] = new YYARec(-19,81);yygc++; 
					yyg[yygc] = new YYARec(-7,82);yygc++; 
					yyg[yygc] = new YYARec(-38,120);yygc++; 
					yyg[yygc] = new YYARec(-38,156);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = -107;  
					yyd[2] = -15;  
					yyd[3] = -14;  
					yyd[4] = -13;  
					yyd[5] = -12;  
					yyd[6] = 0;  
					yyd[7] = -5;  
					yyd[8] = -4;  
					yyd[9] = 0;  
					yyd[10] = -1;  
					yyd[11] = 0;  
					yyd[12] = 0;  
					yyd[13] = 0;  
					yyd[14] = 0;  
					yyd[15] = -66;  
					yyd[16] = -67;  
					yyd[17] = -68;  
					yyd[18] = -69;  
					yyd[19] = -70;  
					yyd[20] = -71;  
					yyd[21] = -72;  
					yyd[22] = -73;  
					yyd[23] = -74;  
					yyd[24] = -75;  
					yyd[25] = -76;  
					yyd[26] = -77;  
					yyd[27] = -78;  
					yyd[28] = -79;  
					yyd[29] = -106;  
					yyd[30] = -103;  
					yyd[31] = 0;  
					yyd[32] = 0;  
					yyd[33] = -23;  
					yyd[34] = -20;  
					yyd[35] = 0;  
					yyd[36] = -22;  
					yyd[37] = -21;  
					yyd[38] = 0;  
					yyd[39] = -59;  
					yyd[40] = -60;  
					yyd[41] = -108;  
					yyd[42] = -109;  
					yyd[43] = -3;  
					yyd[44] = 0;  
					yyd[45] = 0;  
					yyd[46] = 0;  
					yyd[47] = -105;  
					yyd[48] = -24;  
					yyd[49] = -19;  
					yyd[50] = 0;  
					yyd[51] = -34;  
					yyd[52] = 0;  
					yyd[53] = -37;  
					yyd[54] = -38;  
					yyd[55] = -39;  
					yyd[56] = -40;  
					yyd[57] = 0;  
					yyd[58] = 0;  
					yyd[59] = 0;  
					yyd[60] = 0;  
					yyd[61] = 0;  
					yyd[62] = -18;  
					yyd[63] = 0;  
					yyd[64] = 0;  
					yyd[65] = -41;  
					yyd[66] = 0;  
					yyd[67] = -33;  
					yyd[68] = -101;  
					yyd[69] = 0;  
					yyd[70] = -6;  
					yyd[71] = 0;  
					yyd[72] = -7;  
					yyd[73] = 0;  
					yyd[74] = 0;  
					yyd[75] = -36;  
					yyd[76] = 0;  
					yyd[77] = -30;  
					yyd[78] = 0;  
					yyd[79] = 0;  
					yyd[80] = 0;  
					yyd[81] = -31;  
					yyd[82] = 0;  
					yyd[83] = 0;  
					yyd[84] = 0;  
					yyd[85] = 0;  
					yyd[86] = 0;  
					yyd[87] = 0;  
					yyd[88] = -9;  
					yyd[89] = -10;  
					yyd[90] = -17;  
					yyd[91] = -16;  
					yyd[92] = 0;  
					yyd[93] = -28;  
					yyd[94] = 0;  
					yyd[95] = -25;  
					yyd[96] = 0;  
					yyd[97] = 0;  
					yyd[98] = 0;  
					yyd[99] = 0;  
					yyd[100] = 0;  
					yyd[101] = -45;  
					yyd[102] = 0;  
					yyd[103] = -51;  
					yyd[104] = 0;  
					yyd[105] = 0;  
					yyd[106] = 0;  
					yyd[107] = 0;  
					yyd[108] = 0;  
					yyd[109] = 0;  
					yyd[110] = 0;  
					yyd[111] = 0;  
					yyd[112] = -27;  
					yyd[113] = 0;  
					yyd[114] = -61;  
					yyd[115] = -62;  
					yyd[116] = -63;  
					yyd[117] = -64;  
					yyd[118] = -65;  
					yyd[119] = -26;  
					yyd[120] = 0;  
					yyd[121] = 0;  
					yyd[122] = -93;  
					yyd[123] = -94;  
					yyd[124] = -95;  
					yyd[125] = -96;  
					yyd[126] = -97;  
					yyd[127] = -98;  
					yyd[128] = -99;  
					yyd[129] = -100;  
					yyd[130] = 0;  
					yyd[131] = -50;  
					yyd[132] = -58;  
					yyd[133] = 0;  
					yyd[134] = -52;  
					yyd[135] = -53;  
					yyd[136] = -54;  
					yyd[137] = -55;  
					yyd[138] = -56;  
					yyd[139] = -57;  
					yyd[140] = 0;  
					yyd[141] = 0;  
					yyd[142] = 0;  
					yyd[143] = 0;  
					yyd[144] = 0;  
					yyd[145] = 0;  
					yyd[146] = 0;  
					yyd[147] = 0;  
					yyd[148] = 0;  
					yyd[149] = -8;  
					yyd[150] = 0;  
					yyd[151] = 0;  
					yyd[152] = 0;  
					yyd[153] = -88;  
					yyd[154] = 0;  
					yyd[155] = 0;  
					yyd[156] = 0;  
					yyd[157] = 0;  
					yyd[158] = -46;  
					yyd[159] = -48;  
					yyd[160] = 0;  
					yyd[161] = -86;  
					yyd[162] = 0;  
					yyd[163] = -44;  
					yyd[164] = 0;  
					yyd[165] = 0;  
					yyd[166] = 0;  
					yyd[167] = -83;  
					yyd[168] = 0;  
					yyd[169] = 0;  
					yyd[170] = 0;  
					yyd[171] = 0;  
					yyd[172] = 0;  
					yyd[173] = -111;  
					yyd[174] = 0;  
					yyd[175] = 0;  
					yyd[176] = 0;  
					yyd[177] = 0;  
					yyd[178] = 0;  
					yyd[179] = -84;  
					yyd[180] = 0;  
					yyd[181] = -85;  
					yyd[182] = 0;  
					yyd[183] = 0;  
					yyd[184] = -89;  
					yyd[185] = 0;  
					yyd[186] = -49;  
					yyd[187] = -47;  
					yyd[188] = 0;  
					yyd[189] = 0;  
					yyd[190] = 0;  
					yyd[191] = 0;  
					yyd[192] = -81;  
					yyd[193] = -82;  
					yyd[194] = -80; 

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
					yyal[61] = 255;  
					yyal[62] = 273;  
					yyal[63] = 273;  
					yyal[64] = 291;  
					yyal[65] = 311;  
					yyal[66] = 311;  
					yyal[67] = 331;  
					yyal[68] = 331;  
					yyal[69] = 331;  
					yyal[70] = 333;  
					yyal[71] = 333;  
					yyal[72] = 351;  
					yyal[73] = 351;  
					yyal[74] = 352;  
					yyal[75] = 353;  
					yyal[76] = 353;  
					yyal[77] = 354;  
					yyal[78] = 354;  
					yyal[79] = 374;  
					yyal[80] = 375;  
					yyal[81] = 376;  
					yyal[82] = 376;  
					yyal[83] = 399;  
					yyal[84] = 418;  
					yyal[85] = 419;  
					yyal[86] = 420;  
					yyal[87] = 421;  
					yyal[88] = 438;  
					yyal[89] = 438;  
					yyal[90] = 438;  
					yyal[91] = 438;  
					yyal[92] = 438;  
					yyal[93] = 458;  
					yyal[94] = 458;  
					yyal[95] = 478;  
					yyal[96] = 478;  
					yyal[97] = 505;  
					yyal[98] = 525;  
					yyal[99] = 545;  
					yyal[100] = 554;  
					yyal[101] = 570;  
					yyal[102] = 570;  
					yyal[103] = 587;  
					yyal[104] = 587;  
					yyal[105] = 606;  
					yyal[106] = 607;  
					yyal[107] = 626;  
					yyal[108] = 646;  
					yyal[109] = 647;  
					yyal[110] = 666;  
					yyal[111] = 667;  
					yyal[112] = 668;  
					yyal[113] = 668;  
					yyal[114] = 687;  
					yyal[115] = 687;  
					yyal[116] = 687;  
					yyal[117] = 687;  
					yyal[118] = 687;  
					yyal[119] = 687;  
					yyal[120] = 687;  
					yyal[121] = 706;  
					yyal[122] = 714;  
					yyal[123] = 714;  
					yyal[124] = 714;  
					yyal[125] = 714;  
					yyal[126] = 714;  
					yyal[127] = 714;  
					yyal[128] = 714;  
					yyal[129] = 714;  
					yyal[130] = 714;  
					yyal[131] = 734;  
					yyal[132] = 734;  
					yyal[133] = 734;  
					yyal[134] = 753;  
					yyal[135] = 753;  
					yyal[136] = 753;  
					yyal[137] = 753;  
					yyal[138] = 753;  
					yyal[139] = 753;  
					yyal[140] = 753;  
					yyal[141] = 754;  
					yyal[142] = 762;  
					yyal[143] = 779;  
					yyal[144] = 798;  
					yyal[145] = 818;  
					yyal[146] = 827;  
					yyal[147] = 828;  
					yyal[148] = 848;  
					yyal[149] = 857;  
					yyal[150] = 857;  
					yyal[151] = 858;  
					yyal[152] = 867;  
					yyal[153] = 886;  
					yyal[154] = 886;  
					yyal[155] = 903;  
					yyal[156] = 922;  
					yyal[157] = 923;  
					yyal[158] = 942;  
					yyal[159] = 942;  
					yyal[160] = 942;  
					yyal[161] = 961;  
					yyal[162] = 961;  
					yyal[163] = 980;  
					yyal[164] = 980;  
					yyal[165] = 981;  
					yyal[166] = 982;  
					yyal[167] = 991;  
					yyal[168] = 991;  
					yyal[169] = 992;  
					yyal[170] = 1001;  
					yyal[171] = 1002;  
					yyal[172] = 1011;  
					yyal[173] = 1012;  
					yyal[174] = 1012;  
					yyal[175] = 1031;  
					yyal[176] = 1040;  
					yyal[177] = 1049;  
					yyal[178] = 1058;  
					yyal[179] = 1067;  
					yyal[180] = 1067;  
					yyal[181] = 1087;  
					yyal[182] = 1087;  
					yyal[183] = 1107;  
					yyal[184] = 1127;  
					yyal[185] = 1127;  
					yyal[186] = 1136;  
					yyal[187] = 1136;  
					yyal[188] = 1136;  
					yyal[189] = 1137;  
					yyal[190] = 1138;  
					yyal[191] = 1139;  
					yyal[192] = 1148;  
					yyal[193] = 1148;  
					yyal[194] = 1148; 

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
					yyah[60] = 254;  
					yyah[61] = 272;  
					yyah[62] = 272;  
					yyah[63] = 290;  
					yyah[64] = 310;  
					yyah[65] = 310;  
					yyah[66] = 330;  
					yyah[67] = 330;  
					yyah[68] = 330;  
					yyah[69] = 332;  
					yyah[70] = 332;  
					yyah[71] = 350;  
					yyah[72] = 350;  
					yyah[73] = 351;  
					yyah[74] = 352;  
					yyah[75] = 352;  
					yyah[76] = 353;  
					yyah[77] = 353;  
					yyah[78] = 373;  
					yyah[79] = 374;  
					yyah[80] = 375;  
					yyah[81] = 375;  
					yyah[82] = 398;  
					yyah[83] = 417;  
					yyah[84] = 418;  
					yyah[85] = 419;  
					yyah[86] = 420;  
					yyah[87] = 437;  
					yyah[88] = 437;  
					yyah[89] = 437;  
					yyah[90] = 437;  
					yyah[91] = 437;  
					yyah[92] = 457;  
					yyah[93] = 457;  
					yyah[94] = 477;  
					yyah[95] = 477;  
					yyah[96] = 504;  
					yyah[97] = 524;  
					yyah[98] = 544;  
					yyah[99] = 553;  
					yyah[100] = 569;  
					yyah[101] = 569;  
					yyah[102] = 586;  
					yyah[103] = 586;  
					yyah[104] = 605;  
					yyah[105] = 606;  
					yyah[106] = 625;  
					yyah[107] = 645;  
					yyah[108] = 646;  
					yyah[109] = 665;  
					yyah[110] = 666;  
					yyah[111] = 667;  
					yyah[112] = 667;  
					yyah[113] = 686;  
					yyah[114] = 686;  
					yyah[115] = 686;  
					yyah[116] = 686;  
					yyah[117] = 686;  
					yyah[118] = 686;  
					yyah[119] = 686;  
					yyah[120] = 705;  
					yyah[121] = 713;  
					yyah[122] = 713;  
					yyah[123] = 713;  
					yyah[124] = 713;  
					yyah[125] = 713;  
					yyah[126] = 713;  
					yyah[127] = 713;  
					yyah[128] = 713;  
					yyah[129] = 713;  
					yyah[130] = 733;  
					yyah[131] = 733;  
					yyah[132] = 733;  
					yyah[133] = 752;  
					yyah[134] = 752;  
					yyah[135] = 752;  
					yyah[136] = 752;  
					yyah[137] = 752;  
					yyah[138] = 752;  
					yyah[139] = 752;  
					yyah[140] = 753;  
					yyah[141] = 761;  
					yyah[142] = 778;  
					yyah[143] = 797;  
					yyah[144] = 817;  
					yyah[145] = 826;  
					yyah[146] = 827;  
					yyah[147] = 847;  
					yyah[148] = 856;  
					yyah[149] = 856;  
					yyah[150] = 857;  
					yyah[151] = 866;  
					yyah[152] = 885;  
					yyah[153] = 885;  
					yyah[154] = 902;  
					yyah[155] = 921;  
					yyah[156] = 922;  
					yyah[157] = 941;  
					yyah[158] = 941;  
					yyah[159] = 941;  
					yyah[160] = 960;  
					yyah[161] = 960;  
					yyah[162] = 979;  
					yyah[163] = 979;  
					yyah[164] = 980;  
					yyah[165] = 981;  
					yyah[166] = 990;  
					yyah[167] = 990;  
					yyah[168] = 991;  
					yyah[169] = 1000;  
					yyah[170] = 1001;  
					yyah[171] = 1010;  
					yyah[172] = 1011;  
					yyah[173] = 1011;  
					yyah[174] = 1030;  
					yyah[175] = 1039;  
					yyah[176] = 1048;  
					yyah[177] = 1057;  
					yyah[178] = 1066;  
					yyah[179] = 1066;  
					yyah[180] = 1086;  
					yyah[181] = 1086;  
					yyah[182] = 1106;  
					yyah[183] = 1126;  
					yyah[184] = 1126;  
					yyah[185] = 1135;  
					yyah[186] = 1135;  
					yyah[187] = 1135;  
					yyah[188] = 1136;  
					yyah[189] = 1137;  
					yyah[190] = 1138;  
					yyah[191] = 1147;  
					yyah[192] = 1147;  
					yyah[193] = 1147;  
					yyah[194] = 1147; 

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
					yygl[61] = 71;  
					yygl[62] = 80;  
					yygl[63] = 80;  
					yygl[64] = 86;  
					yygl[65] = 96;  
					yygl[66] = 96;  
					yygl[67] = 104;  
					yygl[68] = 104;  
					yygl[69] = 104;  
					yygl[70] = 104;  
					yygl[71] = 104;  
					yygl[72] = 112;  
					yygl[73] = 112;  
					yygl[74] = 112;  
					yygl[75] = 112;  
					yygl[76] = 112;  
					yygl[77] = 112;  
					yygl[78] = 112;  
					yygl[79] = 120;  
					yygl[80] = 120;  
					yygl[81] = 120;  
					yygl[82] = 120;  
					yygl[83] = 130;  
					yygl[84] = 136;  
					yygl[85] = 137;  
					yygl[86] = 137;  
					yygl[87] = 138;  
					yygl[88] = 146;  
					yygl[89] = 146;  
					yygl[90] = 146;  
					yygl[91] = 146;  
					yygl[92] = 146;  
					yygl[93] = 154;  
					yygl[94] = 154;  
					yygl[95] = 162;  
					yygl[96] = 162;  
					yygl[97] = 163;  
					yygl[98] = 173;  
					yygl[99] = 181;  
					yygl[100] = 182;  
					yygl[101] = 185;  
					yygl[102] = 185;  
					yygl[103] = 187;  
					yygl[104] = 187;  
					yygl[105] = 194;  
					yygl[106] = 194;  
					yygl[107] = 200;  
					yygl[108] = 208;  
					yygl[109] = 208;  
					yygl[110] = 214;  
					yygl[111] = 214;  
					yygl[112] = 214;  
					yygl[113] = 214;  
					yygl[114] = 219;  
					yygl[115] = 219;  
					yygl[116] = 219;  
					yygl[117] = 219;  
					yygl[118] = 219;  
					yygl[119] = 219;  
					yygl[120] = 219;  
					yygl[121] = 225;  
					yygl[122] = 226;  
					yygl[123] = 226;  
					yygl[124] = 226;  
					yygl[125] = 226;  
					yygl[126] = 226;  
					yygl[127] = 226;  
					yygl[128] = 226;  
					yygl[129] = 226;  
					yygl[130] = 226;  
					yygl[131] = 226;  
					yygl[132] = 226;  
					yygl[133] = 226;  
					yygl[134] = 231;  
					yygl[135] = 231;  
					yygl[136] = 231;  
					yygl[137] = 231;  
					yygl[138] = 231;  
					yygl[139] = 231;  
					yygl[140] = 231;  
					yygl[141] = 231;  
					yygl[142] = 232;  
					yygl[143] = 234;  
					yygl[144] = 241;  
					yygl[145] = 249;  
					yygl[146] = 250;  
					yygl[147] = 250;  
					yygl[148] = 258;  
					yygl[149] = 259;  
					yygl[150] = 259;  
					yygl[151] = 259;  
					yygl[152] = 261;  
					yygl[153] = 266;  
					yygl[154] = 266;  
					yygl[155] = 268;  
					yygl[156] = 275;  
					yygl[157] = 276;  
					yygl[158] = 281;  
					yygl[159] = 281;  
					yygl[160] = 281;  
					yygl[161] = 286;  
					yygl[162] = 286;  
					yygl[163] = 292;  
					yygl[164] = 292;  
					yygl[165] = 292;  
					yygl[166] = 292;  
					yygl[167] = 293;  
					yygl[168] = 293;  
					yygl[169] = 293;  
					yygl[170] = 294;  
					yygl[171] = 294;  
					yygl[172] = 296;  
					yygl[173] = 296;  
					yygl[174] = 296;  
					yygl[175] = 302;  
					yygl[176] = 304;  
					yygl[177] = 306;  
					yygl[178] = 306;  
					yygl[179] = 306;  
					yygl[180] = 306;  
					yygl[181] = 314;  
					yygl[182] = 314;  
					yygl[183] = 322;  
					yygl[184] = 330;  
					yygl[185] = 330;  
					yygl[186] = 331;  
					yygl[187] = 331;  
					yygl[188] = 331;  
					yygl[189] = 331;  
					yygl[190] = 331;  
					yygl[191] = 331;  
					yygl[192] = 332;  
					yygl[193] = 332;  
					yygl[194] = 332; 

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
					yygh[60] = 70;  
					yygh[61] = 79;  
					yygh[62] = 79;  
					yygh[63] = 85;  
					yygh[64] = 95;  
					yygh[65] = 95;  
					yygh[66] = 103;  
					yygh[67] = 103;  
					yygh[68] = 103;  
					yygh[69] = 103;  
					yygh[70] = 103;  
					yygh[71] = 111;  
					yygh[72] = 111;  
					yygh[73] = 111;  
					yygh[74] = 111;  
					yygh[75] = 111;  
					yygh[76] = 111;  
					yygh[77] = 111;  
					yygh[78] = 119;  
					yygh[79] = 119;  
					yygh[80] = 119;  
					yygh[81] = 119;  
					yygh[82] = 129;  
					yygh[83] = 135;  
					yygh[84] = 136;  
					yygh[85] = 136;  
					yygh[86] = 137;  
					yygh[87] = 145;  
					yygh[88] = 145;  
					yygh[89] = 145;  
					yygh[90] = 145;  
					yygh[91] = 145;  
					yygh[92] = 153;  
					yygh[93] = 153;  
					yygh[94] = 161;  
					yygh[95] = 161;  
					yygh[96] = 162;  
					yygh[97] = 172;  
					yygh[98] = 180;  
					yygh[99] = 181;  
					yygh[100] = 184;  
					yygh[101] = 184;  
					yygh[102] = 186;  
					yygh[103] = 186;  
					yygh[104] = 193;  
					yygh[105] = 193;  
					yygh[106] = 199;  
					yygh[107] = 207;  
					yygh[108] = 207;  
					yygh[109] = 213;  
					yygh[110] = 213;  
					yygh[111] = 213;  
					yygh[112] = 213;  
					yygh[113] = 218;  
					yygh[114] = 218;  
					yygh[115] = 218;  
					yygh[116] = 218;  
					yygh[117] = 218;  
					yygh[118] = 218;  
					yygh[119] = 218;  
					yygh[120] = 224;  
					yygh[121] = 225;  
					yygh[122] = 225;  
					yygh[123] = 225;  
					yygh[124] = 225;  
					yygh[125] = 225;  
					yygh[126] = 225;  
					yygh[127] = 225;  
					yygh[128] = 225;  
					yygh[129] = 225;  
					yygh[130] = 225;  
					yygh[131] = 225;  
					yygh[132] = 225;  
					yygh[133] = 230;  
					yygh[134] = 230;  
					yygh[135] = 230;  
					yygh[136] = 230;  
					yygh[137] = 230;  
					yygh[138] = 230;  
					yygh[139] = 230;  
					yygh[140] = 230;  
					yygh[141] = 231;  
					yygh[142] = 233;  
					yygh[143] = 240;  
					yygh[144] = 248;  
					yygh[145] = 249;  
					yygh[146] = 249;  
					yygh[147] = 257;  
					yygh[148] = 258;  
					yygh[149] = 258;  
					yygh[150] = 258;  
					yygh[151] = 260;  
					yygh[152] = 265;  
					yygh[153] = 265;  
					yygh[154] = 267;  
					yygh[155] = 274;  
					yygh[156] = 275;  
					yygh[157] = 280;  
					yygh[158] = 280;  
					yygh[159] = 280;  
					yygh[160] = 285;  
					yygh[161] = 285;  
					yygh[162] = 291;  
					yygh[163] = 291;  
					yygh[164] = 291;  
					yygh[165] = 291;  
					yygh[166] = 292;  
					yygh[167] = 292;  
					yygh[168] = 292;  
					yygh[169] = 293;  
					yygh[170] = 293;  
					yygh[171] = 295;  
					yygh[172] = 295;  
					yygh[173] = 295;  
					yygh[174] = 301;  
					yygh[175] = 303;  
					yygh[176] = 305;  
					yygh[177] = 305;  
					yygh[178] = 305;  
					yygh[179] = 305;  
					yygh[180] = 313;  
					yygh[181] = 313;  
					yygh[182] = 321;  
					yygh[183] = 329;  
					yygh[184] = 329;  
					yygh[185] = 330;  
					yygh[186] = 330;  
					yygh[187] = 330;  
					yygh[188] = 330;  
					yygh[189] = 330;  
					yygh[190] = 330;  
					yygh[191] = 331;  
					yygh[192] = 331;  
					yygh[193] = 331;  
					yygh[194] = 331; 

					yyr[yyrc] = new YYRRec(1,-2);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(8,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-35);yyrc++;
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

			if (Regex.IsMatch(Rest,"^(\\{)")){
				Results.Add (t_Char123);
				ResultsV.Add(Regex.Match(Rest,"^(\\{)").Value);}

			if (Regex.IsMatch(Rest,"^(\\}([\\t\\s]*;+)?)")){
				Results.Add (t_Char125);
				ResultsV.Add(Regex.Match(Rest,"^(\\}([\\t\\s]*;+)?)").Value);}

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
