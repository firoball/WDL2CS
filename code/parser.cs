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
                int t_Char40ComparisonChar41 = 299;
                int t_Char33Char61 = 300;
                int t_Char38Char38 = 301;
                int t_Char60 = 302;
                int t_Char60Char61 = 303;
                int t_Char61Char61 = 304;
                int t_Char62 = 305;
                int t_Char62Char61 = 306;
                int t_Char124Char124 = 307;
                int t_Char46 = 308;
                int t_number = 309;
                int t_identifier = 310;
                int t_file = 311;
                int t_string = 312;
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
         yyval = "";
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   48 : 
         yyval = yyv[yysp-0];
         
       break;
							case   49 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   50 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   51 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   57 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   58 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   59 : 
         yyval = yyv[yysp-0];
         
       break;
							case   60 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   61 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   62 : 
         yyval = yyv[yysp-0];
         
       break;
							case   63 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-10] + yyv[yysp-9] + yyv[yysp-8] + yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   95 : 
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   96 : 
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case  110 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  111 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = "";
         
       break;
							case  116 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
                                default : return;
                        }
                }

                public void InitTables()
                {
                        ////////////////////////////////////////////////////////////////
                        /// Init Table code:
                        ////////////////////////////////////////////////////////////////

					int yynacts   = 425;
					int yyngotos  = 287;
					int yynstates = 184;
					int yynrules  = 120;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,10);yyac++; 
					yya[yyac] = new YYARec(259,11);yyac++; 
					yya[yyac] = new YYARec(262,12);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(311,24);yyac++; 
					yya[yyac] = new YYARec(312,25);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(257,10);yyac++; 
					yya[yyac] = new YYARec(259,11);yyac++; 
					yya[yyac] = new YYARec(262,12);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(0,-1 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(258,31);yyac++; 
					yya[yyac] = new YYARec(263,35);yyac++; 
					yya[yyac] = new YYARec(264,36);yyac++; 
					yya[yyac] = new YYARec(308,37);yyac++; 
					yya[yyac] = new YYARec(311,24);yyac++; 
					yya[yyac] = new YYARec(312,25);yyac++; 
					yya[yyac] = new YYARec(258,-112 );yyac++; 
					yya[yyac] = new YYARec(258,38);yyac++; 
					yya[yyac] = new YYARec(258,39);yyac++; 
					yya[yyac] = new YYARec(258,40);yyac++; 
					yya[yyac] = new YYARec(263,41);yyac++; 
					yya[yyac] = new YYARec(258,42);yyac++; 
					yya[yyac] = new YYARec(263,43);yyac++; 
					yya[yyac] = new YYARec(258,-26 );yyac++; 
					yya[yyac] = new YYARec(311,24);yyac++; 
					yya[yyac] = new YYARec(312,25);yyac++; 
					yya[yyac] = new YYARec(296,59);yyac++; 
					yya[yyac] = new YYARec(298,60);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(265,-30 );yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(262,12);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(262,12);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(258,72);yyac++; 
					yya[yyac] = new YYARec(258,73);yyac++; 
					yya[yyac] = new YYARec(265,74);yyac++; 
					yya[yyac] = new YYARec(263,81);yyac++; 
					yya[yyac] = new YYARec(266,82);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(311,24);yyac++; 
					yya[yyac] = new YYARec(312,25);yyac++; 
					yya[yyac] = new YYARec(258,-39 );yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(267,83);yyac++; 
					yya[yyac] = new YYARec(267,84);yyac++; 
					yya[yyac] = new YYARec(260,85);yyac++; 
					yya[yyac] = new YYARec(261,86);yyac++; 
					yya[yyac] = new YYARec(258,87);yyac++; 
					yya[yyac] = new YYARec(258,88);yyac++; 
					yya[yyac] = new YYARec(308,37);yyac++; 
					yya[yyac] = new YYARec(258,-112 );yyac++; 
					yya[yyac] = new YYARec(263,-112 );yyac++; 
					yya[yyac] = new YYARec(268,-112 );yyac++; 
					yya[yyac] = new YYARec(269,-112 );yyac++; 
					yya[yyac] = new YYARec(270,-112 );yyac++; 
					yya[yyac] = new YYARec(271,-112 );yyac++; 
					yya[yyac] = new YYARec(272,-112 );yyac++; 
					yya[yyac] = new YYARec(273,-112 );yyac++; 
					yya[yyac] = new YYARec(274,-112 );yyac++; 
					yya[yyac] = new YYARec(275,-112 );yyac++; 
					yya[yyac] = new YYARec(276,-112 );yyac++; 
					yya[yyac] = new YYARec(277,-112 );yyac++; 
					yya[yyac] = new YYARec(278,-112 );yyac++; 
					yya[yyac] = new YYARec(279,-112 );yyac++; 
					yya[yyac] = new YYARec(280,-112 );yyac++; 
					yya[yyac] = new YYARec(281,-112 );yyac++; 
					yya[yyac] = new YYARec(300,-112 );yyac++; 
					yya[yyac] = new YYARec(301,-112 );yyac++; 
					yya[yyac] = new YYARec(302,-112 );yyac++; 
					yya[yyac] = new YYARec(303,-112 );yyac++; 
					yya[yyac] = new YYARec(304,-112 );yyac++; 
					yya[yyac] = new YYARec(305,-112 );yyac++; 
					yya[yyac] = new YYARec(306,-112 );yyac++; 
					yya[yyac] = new YYARec(307,-112 );yyac++; 
					yya[yyac] = new YYARec(263,89);yyac++; 
					yya[yyac] = new YYARec(296,59);yyac++; 
					yya[yyac] = new YYARec(298,60);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(265,-30 );yyac++; 
					yya[yyac] = new YYARec(263,91);yyac++; 
					yya[yyac] = new YYARec(258,-46 );yyac++; 
					yya[yyac] = new YYARec(263,92);yyac++; 
					yya[yyac] = new YYARec(258,-49 );yyac++; 
					yya[yyac] = new YYARec(263,93);yyac++; 
					yya[yyac] = new YYARec(258,-50 );yyac++; 
					yya[yyac] = new YYARec(277,95);yyac++; 
					yya[yyac] = new YYARec(278,96);yyac++; 
					yya[yyac] = new YYARec(279,97);yyac++; 
					yya[yyac] = new YYARec(280,98);yyac++; 
					yya[yyac] = new YYARec(281,99);yyac++; 
					yya[yyac] = new YYARec(258,-47 );yyac++; 
					yya[yyac] = new YYARec(263,-47 );yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(296,59);yyac++; 
					yya[yyac] = new YYARec(298,60);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(265,-30 );yyac++; 
					yya[yyac] = new YYARec(267,109);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(282,110);yyac++; 
					yya[yyac] = new YYARec(283,111);yyac++; 
					yya[yyac] = new YYARec(284,112);yyac++; 
					yya[yyac] = new YYARec(285,113);yyac++; 
					yya[yyac] = new YYARec(286,114);yyac++; 
					yya[yyac] = new YYARec(287,115);yyac++; 
					yya[yyac] = new YYARec(288,116);yyac++; 
					yya[yyac] = new YYARec(289,117);yyac++; 
					yya[yyac] = new YYARec(290,118);yyac++; 
					yya[yyac] = new YYARec(291,119);yyac++; 
					yya[yyac] = new YYARec(292,120);yyac++; 
					yya[yyac] = new YYARec(293,121);yyac++; 
					yya[yyac] = new YYARec(294,122);yyac++; 
					yya[yyac] = new YYARec(295,123);yyac++; 
					yya[yyac] = new YYARec(299,124);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(267,109);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(282,110);yyac++; 
					yya[yyac] = new YYARec(283,111);yyac++; 
					yya[yyac] = new YYARec(284,112);yyac++; 
					yya[yyac] = new YYARec(285,113);yyac++; 
					yya[yyac] = new YYARec(286,114);yyac++; 
					yya[yyac] = new YYARec(287,115);yyac++; 
					yya[yyac] = new YYARec(288,116);yyac++; 
					yya[yyac] = new YYARec(289,117);yyac++; 
					yya[yyac] = new YYARec(290,118);yyac++; 
					yya[yyac] = new YYARec(291,119);yyac++; 
					yya[yyac] = new YYARec(292,120);yyac++; 
					yya[yyac] = new YYARec(293,121);yyac++; 
					yya[yyac] = new YYARec(294,122);yyac++; 
					yya[yyac] = new YYARec(295,123);yyac++; 
					yya[yyac] = new YYARec(299,124);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(262,12);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(312,25);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(312,25);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(267,109);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(282,110);yyac++; 
					yya[yyac] = new YYARec(283,111);yyac++; 
					yya[yyac] = new YYARec(284,112);yyac++; 
					yya[yyac] = new YYARec(285,113);yyac++; 
					yya[yyac] = new YYARec(286,114);yyac++; 
					yya[yyac] = new YYARec(287,115);yyac++; 
					yya[yyac] = new YYARec(288,116);yyac++; 
					yya[yyac] = new YYARec(289,117);yyac++; 
					yya[yyac] = new YYARec(290,118);yyac++; 
					yya[yyac] = new YYARec(291,119);yyac++; 
					yya[yyac] = new YYARec(292,120);yyac++; 
					yya[yyac] = new YYARec(293,121);yyac++; 
					yya[yyac] = new YYARec(294,122);yyac++; 
					yya[yyac] = new YYARec(295,123);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(268,136);yyac++; 
					yya[yyac] = new YYARec(300,137);yyac++; 
					yya[yyac] = new YYARec(301,138);yyac++; 
					yya[yyac] = new YYARec(302,139);yyac++; 
					yya[yyac] = new YYARec(303,140);yyac++; 
					yya[yyac] = new YYARec(304,141);yyac++; 
					yya[yyac] = new YYARec(305,142);yyac++; 
					yya[yyac] = new YYARec(306,143);yyac++; 
					yya[yyac] = new YYARec(307,144);yyac++; 
					yya[yyac] = new YYARec(267,145);yyac++; 
					yya[yyac] = new YYARec(269,148);yyac++; 
					yya[yyac] = new YYARec(270,149);yyac++; 
					yya[yyac] = new YYARec(271,150);yyac++; 
					yya[yyac] = new YYARec(272,151);yyac++; 
					yya[yyac] = new YYARec(273,152);yyac++; 
					yya[yyac] = new YYARec(274,153);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(268,-98 );yyac++; 
					yya[yyac] = new YYARec(300,-98 );yyac++; 
					yya[yyac] = new YYARec(301,-98 );yyac++; 
					yya[yyac] = new YYARec(302,-98 );yyac++; 
					yya[yyac] = new YYARec(303,-98 );yyac++; 
					yya[yyac] = new YYARec(304,-98 );yyac++; 
					yya[yyac] = new YYARec(305,-98 );yyac++; 
					yya[yyac] = new YYARec(306,-98 );yyac++; 
					yya[yyac] = new YYARec(307,-98 );yyac++; 
					yya[yyac] = new YYARec(267,109);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(282,110);yyac++; 
					yya[yyac] = new YYARec(283,111);yyac++; 
					yya[yyac] = new YYARec(284,112);yyac++; 
					yya[yyac] = new YYARec(285,113);yyac++; 
					yya[yyac] = new YYARec(286,114);yyac++; 
					yya[yyac] = new YYARec(287,115);yyac++; 
					yya[yyac] = new YYARec(288,116);yyac++; 
					yya[yyac] = new YYARec(289,117);yyac++; 
					yya[yyac] = new YYARec(290,118);yyac++; 
					yya[yyac] = new YYARec(291,119);yyac++; 
					yya[yyac] = new YYARec(292,120);yyac++; 
					yya[yyac] = new YYARec(293,121);yyac++; 
					yya[yyac] = new YYARec(294,122);yyac++; 
					yya[yyac] = new YYARec(295,123);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(268,155);yyac++; 
					yya[yyac] = new YYARec(300,137);yyac++; 
					yya[yyac] = new YYARec(301,138);yyac++; 
					yya[yyac] = new YYARec(302,139);yyac++; 
					yya[yyac] = new YYARec(303,140);yyac++; 
					yya[yyac] = new YYARec(304,141);yyac++; 
					yya[yyac] = new YYARec(305,142);yyac++; 
					yya[yyac] = new YYARec(306,143);yyac++; 
					yya[yyac] = new YYARec(307,144);yyac++; 
					yya[yyac] = new YYARec(261,156);yyac++; 
					yya[yyac] = new YYARec(263,157);yyac++; 
					yya[yyac] = new YYARec(258,-24 );yyac++; 
					yya[yyac] = new YYARec(263,158);yyac++; 
					yya[yyac] = new YYARec(258,-55 );yyac++; 
					yya[yyac] = new YYARec(269,148);yyac++; 
					yya[yyac] = new YYARec(270,149);yyac++; 
					yya[yyac] = new YYARec(271,150);yyac++; 
					yya[yyac] = new YYARec(272,151);yyac++; 
					yya[yyac] = new YYARec(273,152);yyac++; 
					yya[yyac] = new YYARec(274,153);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(258,-57 );yyac++; 
					yya[yyac] = new YYARec(267,109);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(282,110);yyac++; 
					yya[yyac] = new YYARec(283,111);yyac++; 
					yya[yyac] = new YYARec(284,112);yyac++; 
					yya[yyac] = new YYARec(285,113);yyac++; 
					yya[yyac] = new YYARec(286,114);yyac++; 
					yya[yyac] = new YYARec(287,115);yyac++; 
					yya[yyac] = new YYARec(288,116);yyac++; 
					yya[yyac] = new YYARec(289,117);yyac++; 
					yya[yyac] = new YYARec(290,118);yyac++; 
					yya[yyac] = new YYARec(291,119);yyac++; 
					yya[yyac] = new YYARec(292,120);yyac++; 
					yya[yyac] = new YYARec(293,121);yyac++; 
					yya[yyac] = new YYARec(294,122);yyac++; 
					yya[yyac] = new YYARec(295,123);yyac++; 
					yya[yyac] = new YYARec(299,161);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(264,162);yyac++; 
					yya[yyac] = new YYARec(267,109);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(282,110);yyac++; 
					yya[yyac] = new YYARec(283,111);yyac++; 
					yya[yyac] = new YYARec(284,112);yyac++; 
					yya[yyac] = new YYARec(285,113);yyac++; 
					yya[yyac] = new YYARec(286,114);yyac++; 
					yya[yyac] = new YYARec(287,115);yyac++; 
					yya[yyac] = new YYARec(288,116);yyac++; 
					yya[yyac] = new YYARec(289,117);yyac++; 
					yya[yyac] = new YYARec(290,118);yyac++; 
					yya[yyac] = new YYARec(291,119);yyac++; 
					yya[yyac] = new YYARec(292,120);yyac++; 
					yya[yyac] = new YYARec(293,121);yyac++; 
					yya[yyac] = new YYARec(294,122);yyac++; 
					yya[yyac] = new YYARec(295,123);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(267,166);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(282,110);yyac++; 
					yya[yyac] = new YYARec(283,111);yyac++; 
					yya[yyac] = new YYARec(284,112);yyac++; 
					yya[yyac] = new YYARec(285,113);yyac++; 
					yya[yyac] = new YYARec(286,114);yyac++; 
					yya[yyac] = new YYARec(287,115);yyac++; 
					yya[yyac] = new YYARec(288,116);yyac++; 
					yya[yyac] = new YYARec(289,117);yyac++; 
					yya[yyac] = new YYARec(290,118);yyac++; 
					yya[yyac] = new YYARec(291,119);yyac++; 
					yya[yyac] = new YYARec(292,120);yyac++; 
					yya[yyac] = new YYARec(293,121);yyac++; 
					yya[yyac] = new YYARec(294,122);yyac++; 
					yya[yyac] = new YYARec(295,123);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(268,167);yyac++; 
					yya[yyac] = new YYARec(269,148);yyac++; 
					yya[yyac] = new YYARec(270,149);yyac++; 
					yya[yyac] = new YYARec(271,150);yyac++; 
					yya[yyac] = new YYARec(272,151);yyac++; 
					yya[yyac] = new YYARec(273,152);yyac++; 
					yya[yyac] = new YYARec(274,153);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(264,168);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(269,148);yyac++; 
					yya[yyac] = new YYARec(270,149);yyac++; 
					yya[yyac] = new YYARec(271,150);yyac++; 
					yya[yyac] = new YYARec(272,151);yyac++; 
					yya[yyac] = new YYARec(273,152);yyac++; 
					yya[yyac] = new YYARec(274,153);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(268,-101 );yyac++; 
					yya[yyac] = new YYARec(300,-101 );yyac++; 
					yya[yyac] = new YYARec(301,-101 );yyac++; 
					yya[yyac] = new YYARec(302,-101 );yyac++; 
					yya[yyac] = new YYARec(303,-101 );yyac++; 
					yya[yyac] = new YYARec(304,-101 );yyac++; 
					yya[yyac] = new YYARec(305,-101 );yyac++; 
					yya[yyac] = new YYARec(306,-101 );yyac++; 
					yya[yyac] = new YYARec(307,-101 );yyac++; 
					yya[yyac] = new YYARec(296,59);yyac++; 
					yya[yyac] = new YYARec(298,60);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(265,-30 );yyac++; 
					yya[yyac] = new YYARec(268,172);yyac++; 
					yya[yyac] = new YYARec(269,148);yyac++; 
					yya[yyac] = new YYARec(270,149);yyac++; 
					yya[yyac] = new YYARec(271,150);yyac++; 
					yya[yyac] = new YYARec(272,151);yyac++; 
					yya[yyac] = new YYARec(273,152);yyac++; 
					yya[yyac] = new YYARec(274,153);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(267,109);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(282,110);yyac++; 
					yya[yyac] = new YYARec(283,111);yyac++; 
					yya[yyac] = new YYARec(284,112);yyac++; 
					yya[yyac] = new YYARec(285,113);yyac++; 
					yya[yyac] = new YYARec(286,114);yyac++; 
					yya[yyac] = new YYARec(287,115);yyac++; 
					yya[yyac] = new YYARec(288,116);yyac++; 
					yya[yyac] = new YYARec(289,117);yyac++; 
					yya[yyac] = new YYARec(290,118);yyac++; 
					yya[yyac] = new YYARec(291,119);yyac++; 
					yya[yyac] = new YYARec(292,120);yyac++; 
					yya[yyac] = new YYARec(293,121);yyac++; 
					yya[yyac] = new YYARec(294,122);yyac++; 
					yya[yyac] = new YYARec(295,123);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(296,59);yyac++; 
					yya[yyac] = new YYARec(298,60);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(265,-30 );yyac++; 
					yya[yyac] = new YYARec(263,175);yyac++; 
					yya[yyac] = new YYARec(265,176);yyac++; 
					yya[yyac] = new YYARec(268,177);yyac++; 
					yya[yyac] = new YYARec(269,148);yyac++; 
					yya[yyac] = new YYARec(270,149);yyac++; 
					yya[yyac] = new YYARec(271,150);yyac++; 
					yya[yyac] = new YYARec(272,151);yyac++; 
					yya[yyac] = new YYARec(273,152);yyac++; 
					yya[yyac] = new YYARec(274,153);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(265,178);yyac++; 
					yya[yyac] = new YYARec(275,22);yyac++; 
					yya[yyac] = new YYARec(276,23);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(309,-115 );yyac++; 
					yya[yyac] = new YYARec(297,180);yyac++; 
					yya[yyac] = new YYARec(258,-95 );yyac++; 
					yya[yyac] = new YYARec(264,181);yyac++; 
					yya[yyac] = new YYARec(296,59);yyac++; 
					yya[yyac] = new YYARec(298,60);yyac++; 
					yya[yyac] = new YYARec(310,13);yyac++; 
					yya[yyac] = new YYARec(265,-30 );yyac++; 
					yya[yyac] = new YYARec(265,183);yyac++;

					yyg[yygc] = new YYARec(-11,1);yygc++; 
					yyg[yygc] = new YYARec(-10,2);yygc++; 
					yyg[yygc] = new YYARec(-9,3);yygc++; 
					yyg[yygc] = new YYARec(-8,4);yygc++; 
					yyg[yygc] = new YYARec(-6,5);yygc++; 
					yyg[yygc] = new YYARec(-5,6);yygc++; 
					yyg[yygc] = new YYARec(-4,7);yygc++; 
					yyg[yygc] = new YYARec(-3,8);yygc++; 
					yyg[yygc] = new YYARec(-2,9);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-16,16);yygc++; 
					yyg[yygc] = new YYARec(-15,17);yygc++; 
					yyg[yygc] = new YYARec(-14,18);yygc++; 
					yyg[yygc] = new YYARec(-13,19);yygc++; 
					yyg[yygc] = new YYARec(-12,20);yygc++; 
					yyg[yygc] = new YYARec(-6,21);yygc++; 
					yyg[yygc] = new YYARec(-11,1);yygc++; 
					yyg[yygc] = new YYARec(-10,2);yygc++; 
					yyg[yygc] = new YYARec(-9,3);yygc++; 
					yyg[yygc] = new YYARec(-8,4);yygc++; 
					yyg[yygc] = new YYARec(-6,5);yygc++; 
					yyg[yygc] = new YYARec(-5,6);yygc++; 
					yyg[yygc] = new YYARec(-4,7);yygc++; 
					yyg[yygc] = new YYARec(-3,8);yygc++; 
					yyg[yygc] = new YYARec(-2,26);yygc++; 
					yyg[yygc] = new YYARec(-6,27);yygc++; 
					yyg[yygc] = new YYARec(-6,28);yygc++; 
					yyg[yygc] = new YYARec(-6,29);yygc++; 
					yyg[yygc] = new YYARec(-17,32);yygc++; 
					yyg[yygc] = new YYARec(-16,33);yygc++; 
					yyg[yygc] = new YYARec(-15,34);yygc++; 
					yyg[yygc] = new YYARec(-17,44);yygc++; 
					yyg[yygc] = new YYARec(-16,33);yygc++; 
					yyg[yygc] = new YYARec(-15,34);yygc++; 
					yyg[yygc] = new YYARec(-31,45);yygc++; 
					yyg[yygc] = new YYARec(-30,46);yygc++; 
					yyg[yygc] = new YYARec(-29,47);yygc++; 
					yyg[yygc] = new YYARec(-28,48);yygc++; 
					yyg[yygc] = new YYARec(-27,49);yygc++; 
					yyg[yygc] = new YYARec(-26,50);yygc++; 
					yyg[yygc] = new YYARec(-25,51);yygc++; 
					yyg[yygc] = new YYARec(-24,52);yygc++; 
					yyg[yygc] = new YYARec(-23,53);yygc++; 
					yyg[yygc] = new YYARec(-22,54);yygc++; 
					yyg[yygc] = new YYARec(-21,55);yygc++; 
					yyg[yygc] = new YYARec(-20,56);yygc++; 
					yyg[yygc] = new YYARec(-19,57);yygc++; 
					yyg[yygc] = new YYARec(-6,58);yygc++; 
					yyg[yygc] = new YYARec(-6,61);yygc++; 
					yyg[yygc] = new YYARec(-11,1);yygc++; 
					yyg[yygc] = new YYARec(-10,2);yygc++; 
					yyg[yygc] = new YYARec(-9,3);yygc++; 
					yyg[yygc] = new YYARec(-8,4);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-6,5);yygc++; 
					yyg[yygc] = new YYARec(-4,64);yygc++; 
					yyg[yygc] = new YYARec(-11,1);yygc++; 
					yyg[yygc] = new YYARec(-10,2);yygc++; 
					yyg[yygc] = new YYARec(-9,3);yygc++; 
					yyg[yygc] = new YYARec(-8,4);yygc++; 
					yyg[yygc] = new YYARec(-7,65);yygc++; 
					yyg[yygc] = new YYARec(-6,5);yygc++; 
					yyg[yygc] = new YYARec(-4,64);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-13,66);yygc++; 
					yyg[yygc] = new YYARec(-12,67);yygc++; 
					yyg[yygc] = new YYARec(-6,68);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-18,69);yygc++; 
					yyg[yygc] = new YYARec(-13,70);yygc++; 
					yyg[yygc] = new YYARec(-6,71);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-33,75);yygc++; 
					yyg[yygc] = new YYARec(-32,76);yygc++; 
					yyg[yygc] = new YYARec(-16,77);yygc++; 
					yyg[yygc] = new YYARec(-15,78);yygc++; 
					yyg[yygc] = new YYARec(-13,79);yygc++; 
					yyg[yygc] = new YYARec(-12,80);yygc++; 
					yyg[yygc] = new YYARec(-6,68);yygc++; 
					yyg[yygc] = new YYARec(-31,45);yygc++; 
					yyg[yygc] = new YYARec(-30,46);yygc++; 
					yyg[yygc] = new YYARec(-29,47);yygc++; 
					yyg[yygc] = new YYARec(-28,48);yygc++; 
					yyg[yygc] = new YYARec(-27,49);yygc++; 
					yyg[yygc] = new YYARec(-26,50);yygc++; 
					yyg[yygc] = new YYARec(-25,51);yygc++; 
					yyg[yygc] = new YYARec(-24,52);yygc++; 
					yyg[yygc] = new YYARec(-23,53);yygc++; 
					yyg[yygc] = new YYARec(-22,54);yygc++; 
					yyg[yygc] = new YYARec(-21,55);yygc++; 
					yyg[yygc] = new YYARec(-20,56);yygc++; 
					yyg[yygc] = new YYARec(-19,90);yygc++; 
					yyg[yygc] = new YYARec(-6,58);yygc++; 
					yyg[yygc] = new YYARec(-34,94);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-33,75);yygc++; 
					yyg[yygc] = new YYARec(-32,100);yygc++; 
					yyg[yygc] = new YYARec(-13,79);yygc++; 
					yyg[yygc] = new YYARec(-12,101);yygc++; 
					yyg[yygc] = new YYARec(-6,68);yygc++; 
					yyg[yygc] = new YYARec(-31,45);yygc++; 
					yyg[yygc] = new YYARec(-30,46);yygc++; 
					yyg[yygc] = new YYARec(-29,47);yygc++; 
					yyg[yygc] = new YYARec(-28,48);yygc++; 
					yyg[yygc] = new YYARec(-27,49);yygc++; 
					yyg[yygc] = new YYARec(-26,50);yygc++; 
					yyg[yygc] = new YYARec(-25,51);yygc++; 
					yyg[yygc] = new YYARec(-24,52);yygc++; 
					yyg[yygc] = new YYARec(-23,53);yygc++; 
					yyg[yygc] = new YYARec(-22,54);yygc++; 
					yyg[yygc] = new YYARec(-21,55);yygc++; 
					yyg[yygc] = new YYARec(-20,56);yygc++; 
					yyg[yygc] = new YYARec(-19,102);yygc++; 
					yyg[yygc] = new YYARec(-6,58);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-41,103);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-39,104);yygc++; 
					yyg[yygc] = new YYARec(-36,105);yygc++; 
					yyg[yygc] = new YYARec(-35,106);yygc++; 
					yyg[yygc] = new YYARec(-13,107);yygc++; 
					yyg[yygc] = new YYARec(-12,108);yygc++; 
					yyg[yygc] = new YYARec(-6,68);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-41,125);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-39,104);yygc++; 
					yyg[yygc] = new YYARec(-36,105);yygc++; 
					yyg[yygc] = new YYARec(-35,106);yygc++; 
					yyg[yygc] = new YYARec(-13,107);yygc++; 
					yyg[yygc] = new YYARec(-12,108);yygc++; 
					yyg[yygc] = new YYARec(-6,68);yygc++; 
					yyg[yygc] = new YYARec(-11,1);yygc++; 
					yyg[yygc] = new YYARec(-10,2);yygc++; 
					yyg[yygc] = new YYARec(-9,3);yygc++; 
					yyg[yygc] = new YYARec(-8,4);yygc++; 
					yyg[yygc] = new YYARec(-6,5);yygc++; 
					yyg[yygc] = new YYARec(-4,126);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-18,127);yygc++; 
					yyg[yygc] = new YYARec(-13,70);yygc++; 
					yyg[yygc] = new YYARec(-6,71);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-33,75);yygc++; 
					yyg[yygc] = new YYARec(-32,128);yygc++; 
					yyg[yygc] = new YYARec(-13,79);yygc++; 
					yyg[yygc] = new YYARec(-12,101);yygc++; 
					yyg[yygc] = new YYARec(-6,68);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-18,129);yygc++; 
					yyg[yygc] = new YYARec(-15,130);yygc++; 
					yyg[yygc] = new YYARec(-13,70);yygc++; 
					yyg[yygc] = new YYARec(-6,71);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-15,131);yygc++; 
					yyg[yygc] = new YYARec(-13,132);yygc++; 
					yyg[yygc] = new YYARec(-12,133);yygc++; 
					yyg[yygc] = new YYARec(-6,68);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-39,104);yygc++; 
					yyg[yygc] = new YYARec(-36,105);yygc++; 
					yyg[yygc] = new YYARec(-35,134);yygc++; 
					yyg[yygc] = new YYARec(-13,107);yygc++; 
					yyg[yygc] = new YYARec(-12,108);yygc++; 
					yyg[yygc] = new YYARec(-6,68);yygc++; 
					yyg[yygc] = new YYARec(-42,135);yygc++; 
					yyg[yygc] = new YYARec(-40,146);yygc++; 
					yyg[yygc] = new YYARec(-37,147);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-39,104);yygc++; 
					yyg[yygc] = new YYARec(-36,105);yygc++; 
					yyg[yygc] = new YYARec(-35,154);yygc++; 
					yyg[yygc] = new YYARec(-13,107);yygc++; 
					yyg[yygc] = new YYARec(-12,108);yygc++; 
					yyg[yygc] = new YYARec(-6,68);yygc++; 
					yyg[yygc] = new YYARec(-42,135);yygc++; 
					yyg[yygc] = new YYARec(-40,146);yygc++; 
					yyg[yygc] = new YYARec(-37,147);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-43,159);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-39,104);yygc++; 
					yyg[yygc] = new YYARec(-36,105);yygc++; 
					yyg[yygc] = new YYARec(-35,160);yygc++; 
					yyg[yygc] = new YYARec(-13,107);yygc++; 
					yyg[yygc] = new YYARec(-12,108);yygc++; 
					yyg[yygc] = new YYARec(-6,68);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-39,104);yygc++; 
					yyg[yygc] = new YYARec(-36,105);yygc++; 
					yyg[yygc] = new YYARec(-35,163);yygc++; 
					yyg[yygc] = new YYARec(-13,107);yygc++; 
					yyg[yygc] = new YYARec(-12,108);yygc++; 
					yyg[yygc] = new YYARec(-6,68);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-39,104);yygc++; 
					yyg[yygc] = new YYARec(-38,164);yygc++; 
					yyg[yygc] = new YYARec(-36,165);yygc++; 
					yyg[yygc] = new YYARec(-13,107);yygc++; 
					yyg[yygc] = new YYARec(-12,108);yygc++; 
					yyg[yygc] = new YYARec(-6,68);yygc++; 
					yyg[yygc] = new YYARec(-40,146);yygc++; 
					yyg[yygc] = new YYARec(-37,147);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-18,169);yygc++; 
					yyg[yygc] = new YYARec(-13,70);yygc++; 
					yyg[yygc] = new YYARec(-6,71);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-18,170);yygc++; 
					yyg[yygc] = new YYARec(-13,70);yygc++; 
					yyg[yygc] = new YYARec(-6,71);yygc++; 
					yyg[yygc] = new YYARec(-40,146);yygc++; 
					yyg[yygc] = new YYARec(-37,147);yygc++; 
					yyg[yygc] = new YYARec(-31,45);yygc++; 
					yyg[yygc] = new YYARec(-30,46);yygc++; 
					yyg[yygc] = new YYARec(-29,47);yygc++; 
					yyg[yygc] = new YYARec(-28,48);yygc++; 
					yyg[yygc] = new YYARec(-27,49);yygc++; 
					yyg[yygc] = new YYARec(-26,50);yygc++; 
					yyg[yygc] = new YYARec(-25,51);yygc++; 
					yyg[yygc] = new YYARec(-24,52);yygc++; 
					yyg[yygc] = new YYARec(-23,53);yygc++; 
					yyg[yygc] = new YYARec(-22,54);yygc++; 
					yyg[yygc] = new YYARec(-21,55);yygc++; 
					yyg[yygc] = new YYARec(-20,56);yygc++; 
					yyg[yygc] = new YYARec(-19,171);yygc++; 
					yyg[yygc] = new YYARec(-6,58);yygc++; 
					yyg[yygc] = new YYARec(-40,146);yygc++; 
					yyg[yygc] = new YYARec(-37,147);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-39,104);yygc++; 
					yyg[yygc] = new YYARec(-36,105);yygc++; 
					yyg[yygc] = new YYARec(-35,173);yygc++; 
					yyg[yygc] = new YYARec(-13,107);yygc++; 
					yyg[yygc] = new YYARec(-12,108);yygc++; 
					yyg[yygc] = new YYARec(-6,68);yygc++; 
					yyg[yygc] = new YYARec(-31,45);yygc++; 
					yyg[yygc] = new YYARec(-30,46);yygc++; 
					yyg[yygc] = new YYARec(-29,47);yygc++; 
					yyg[yygc] = new YYARec(-28,48);yygc++; 
					yyg[yygc] = new YYARec(-27,49);yygc++; 
					yyg[yygc] = new YYARec(-26,50);yygc++; 
					yyg[yygc] = new YYARec(-25,51);yygc++; 
					yyg[yygc] = new YYARec(-24,52);yygc++; 
					yyg[yygc] = new YYARec(-23,53);yygc++; 
					yyg[yygc] = new YYARec(-22,54);yygc++; 
					yyg[yygc] = new YYARec(-21,55);yygc++; 
					yyg[yygc] = new YYARec(-20,56);yygc++; 
					yyg[yygc] = new YYARec(-19,174);yygc++; 
					yyg[yygc] = new YYARec(-6,58);yygc++; 
					yyg[yygc] = new YYARec(-40,146);yygc++; 
					yyg[yygc] = new YYARec(-37,147);yygc++; 
					yyg[yygc] = new YYARec(-44,14);yygc++; 
					yyg[yygc] = new YYARec(-40,15);yygc++; 
					yyg[yygc] = new YYARec(-18,179);yygc++; 
					yyg[yygc] = new YYARec(-13,70);yygc++; 
					yyg[yygc] = new YYARec(-6,71);yygc++; 
					yyg[yygc] = new YYARec(-31,45);yygc++; 
					yyg[yygc] = new YYARec(-30,46);yygc++; 
					yyg[yygc] = new YYARec(-29,47);yygc++; 
					yyg[yygc] = new YYARec(-28,48);yygc++; 
					yyg[yygc] = new YYARec(-27,49);yygc++; 
					yyg[yygc] = new YYARec(-26,50);yygc++; 
					yyg[yygc] = new YYARec(-25,51);yygc++; 
					yyg[yygc] = new YYARec(-24,52);yygc++; 
					yyg[yygc] = new YYARec(-23,53);yygc++; 
					yyg[yygc] = new YYARec(-22,54);yygc++; 
					yyg[yygc] = new YYARec(-21,55);yygc++; 
					yyg[yygc] = new YYARec(-20,56);yygc++; 
					yyg[yygc] = new YYARec(-19,182);yygc++; 
					yyg[yygc] = new YYARec(-6,58);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = -12;  
					yyd[2] = -11;  
					yyd[3] = -10;  
					yyd[4] = -9;  
					yyd[5] = 0;  
					yyd[6] = -4;  
					yyd[7] = -3;  
					yyd[8] = 0;  
					yyd[9] = 0;  
					yyd[10] = 0;  
					yyd[11] = 0;  
					yyd[12] = 0;  
					yyd[13] = -113;  
					yyd[14] = 0;  
					yyd[15] = -114;  
					yyd[16] = -20;  
					yyd[17] = -17;  
					yyd[18] = 0;  
					yyd[19] = -19;  
					yyd[20] = -18;  
					yyd[21] = 0;  
					yyd[22] = -73;  
					yyd[23] = -74;  
					yyd[24] = -119;  
					yyd[25] = -120;  
					yyd[26] = -2;  
					yyd[27] = 0;  
					yyd[28] = 0;  
					yyd[29] = 0;  
					yyd[30] = -116;  
					yyd[31] = -16;  
					yyd[32] = 0;  
					yyd[33] = 0;  
					yyd[34] = -25;  
					yyd[35] = 0;  
					yyd[36] = 0;  
					yyd[37] = 0;  
					yyd[38] = 0;  
					yyd[39] = 0;  
					yyd[40] = -15;  
					yyd[41] = 0;  
					yyd[42] = -22;  
					yyd[43] = 0;  
					yyd[44] = 0;  
					yyd[45] = -42;  
					yyd[46] = -41;  
					yyd[47] = -40;  
					yyd[48] = -38;  
					yyd[49] = -37;  
					yyd[50] = -36;  
					yyd[51] = -35;  
					yyd[52] = -34;  
					yyd[53] = -33;  
					yyd[54] = -32;  
					yyd[55] = -31;  
					yyd[56] = 0;  
					yyd[57] = 0;  
					yyd[58] = 0;  
					yyd[59] = 0;  
					yyd[60] = 0;  
					yyd[61] = -111;  
					yyd[62] = -110;  
					yyd[63] = -5;  
					yyd[64] = 0;  
					yyd[65] = -6;  
					yyd[66] = 0;  
					yyd[67] = 0;  
					yyd[68] = 0;  
					yyd[69] = 0;  
					yyd[70] = -118;  
					yyd[71] = -117;  
					yyd[72] = -21;  
					yyd[73] = 0;  
					yyd[74] = -27;  
					yyd[75] = 0;  
					yyd[76] = -44;  
					yyd[77] = 0;  
					yyd[78] = 0;  
					yyd[79] = -48;  
					yyd[80] = 0;  
					yyd[81] = 0;  
					yyd[82] = 0;  
					yyd[83] = 0;  
					yyd[84] = 0;  
					yyd[85] = 0;  
					yyd[86] = -8;  
					yyd[87] = -14;  
					yyd[88] = -13;  
					yyd[89] = 0;  
					yyd[90] = -29;  
					yyd[91] = 0;  
					yyd[92] = 0;  
					yyd[93] = 0;  
					yyd[94] = 0;  
					yyd[95] = -75;  
					yyd[96] = -76;  
					yyd[97] = -77;  
					yyd[98] = -78;  
					yyd[99] = -79;  
					yyd[100] = -43;  
					yyd[101] = -47;  
					yyd[102] = -28;  
					yyd[103] = 0;  
					yyd[104] = 0;  
					yyd[105] = -59;  
					yyd[106] = 0;  
					yyd[107] = -65;  
					yyd[108] = -64;  
					yyd[109] = 0;  
					yyd[110] = -80;  
					yyd[111] = -81;  
					yyd[112] = -82;  
					yyd[113] = -83;  
					yyd[114] = -84;  
					yyd[115] = -85;  
					yyd[116] = -86;  
					yyd[117] = -87;  
					yyd[118] = -88;  
					yyd[119] = -89;  
					yyd[120] = -90;  
					yyd[121] = -91;  
					yyd[122] = -92;  
					yyd[123] = -93;  
					yyd[124] = -97;  
					yyd[125] = 0;  
					yyd[126] = 0;  
					yyd[127] = 0;  
					yyd[128] = -45;  
					yyd[129] = 0;  
					yyd[130] = -51;  
					yyd[131] = -52;  
					yyd[132] = -53;  
					yyd[133] = -54;  
					yyd[134] = 0;  
					yyd[135] = 0;  
					yyd[136] = 0;  
					yyd[137] = -102;  
					yyd[138] = -103;  
					yyd[139] = -104;  
					yyd[140] = -105;  
					yyd[141] = -106;  
					yyd[142] = -107;  
					yyd[143] = -108;  
					yyd[144] = -109;  
					yyd[145] = 0;  
					yyd[146] = -72;  
					yyd[147] = 0;  
					yyd[148] = -66;  
					yyd[149] = -67;  
					yyd[150] = -68;  
					yyd[151] = -69;  
					yyd[152] = -70;  
					yyd[153] = -71;  
					yyd[154] = 0;  
					yyd[155] = 0;  
					yyd[156] = -7;  
					yyd[157] = 0;  
					yyd[158] = 0;  
					yyd[159] = -99;  
					yyd[160] = 0;  
					yyd[161] = -100;  
					yyd[162] = 0;  
					yyd[163] = 0;  
					yyd[164] = -60;  
					yyd[165] = -62;  
					yyd[166] = 0;  
					yyd[167] = -58;  
					yyd[168] = 0;  
					yyd[169] = 0;  
					yyd[170] = -56;  
					yyd[171] = 0;  
					yyd[172] = -63;  
					yyd[173] = 0;  
					yyd[174] = 0;  
					yyd[175] = 0;  
					yyd[176] = 0;  
					yyd[177] = -61;  
					yyd[178] = -96;  
					yyd[179] = -23;  
					yyd[180] = 0;  
					yyd[181] = 0;  
					yyd[182] = 0;  
					yyd[183] = -94; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 5;  
					yyal[2] = 5;  
					yyal[3] = 5;  
					yyal[4] = 5;  
					yyal[5] = 5;  
					yyal[6] = 11;  
					yyal[7] = 11;  
					yyal[8] = 11;  
					yyal[9] = 16;  
					yyal[10] = 17;  
					yyal[11] = 18;  
					yyal[12] = 19;  
					yyal[13] = 20;  
					yyal[14] = 20;  
					yyal[15] = 21;  
					yyal[16] = 21;  
					yyal[17] = 21;  
					yyal[18] = 21;  
					yyal[19] = 22;  
					yyal[20] = 22;  
					yyal[21] = 22;  
					yyal[22] = 28;  
					yyal[23] = 28;  
					yyal[24] = 28;  
					yyal[25] = 28;  
					yyal[26] = 28;  
					yyal[27] = 28;  
					yyal[28] = 29;  
					yyal[29] = 30;  
					yyal[30] = 32;  
					yyal[31] = 32;  
					yyal[32] = 32;  
					yyal[33] = 33;  
					yyal[34] = 35;  
					yyal[35] = 35;  
					yyal[36] = 37;  
					yyal[37] = 41;  
					yyal[38] = 43;  
					yyal[39] = 45;  
					yyal[40] = 47;  
					yyal[41] = 47;  
					yyal[42] = 51;  
					yyal[43] = 51;  
					yyal[44] = 55;  
					yyal[45] = 56;  
					yyal[46] = 56;  
					yyal[47] = 56;  
					yyal[48] = 56;  
					yyal[49] = 56;  
					yyal[50] = 56;  
					yyal[51] = 56;  
					yyal[52] = 56;  
					yyal[53] = 56;  
					yyal[54] = 56;  
					yyal[55] = 56;  
					yyal[56] = 56;  
					yyal[57] = 57;  
					yyal[58] = 58;  
					yyal[59] = 67;  
					yyal[60] = 68;  
					yyal[61] = 69;  
					yyal[62] = 69;  
					yyal[63] = 69;  
					yyal[64] = 69;  
					yyal[65] = 71;  
					yyal[66] = 71;  
					yyal[67] = 72;  
					yyal[68] = 73;  
					yyal[69] = 98;  
					yyal[70] = 99;  
					yyal[71] = 99;  
					yyal[72] = 99;  
					yyal[73] = 99;  
					yyal[74] = 103;  
					yyal[75] = 103;  
					yyal[76] = 105;  
					yyal[77] = 105;  
					yyal[78] = 107;  
					yyal[79] = 109;  
					yyal[80] = 109;  
					yyal[81] = 116;  
					yyal[82] = 120;  
					yyal[83] = 124;  
					yyal[84] = 144;  
					yyal[85] = 164;  
					yyal[86] = 166;  
					yyal[87] = 166;  
					yyal[88] = 166;  
					yyal[89] = 166;  
					yyal[90] = 170;  
					yyal[91] = 170;  
					yyal[92] = 174;  
					yyal[93] = 179;  
					yyal[94] = 184;  
					yyal[95] = 203;  
					yyal[96] = 203;  
					yyal[97] = 203;  
					yyal[98] = 203;  
					yyal[99] = 203;  
					yyal[100] = 203;  
					yyal[101] = 203;  
					yyal[102] = 203;  
					yyal[103] = 203;  
					yyal[104] = 212;  
					yyal[105] = 213;  
					yyal[106] = 213;  
					yyal[107] = 230;  
					yyal[108] = 230;  
					yyal[109] = 230;  
					yyal[110] = 249;  
					yyal[111] = 249;  
					yyal[112] = 249;  
					yyal[113] = 249;  
					yyal[114] = 249;  
					yyal[115] = 249;  
					yyal[116] = 249;  
					yyal[117] = 249;  
					yyal[118] = 249;  
					yyal[119] = 249;  
					yyal[120] = 249;  
					yyal[121] = 249;  
					yyal[122] = 249;  
					yyal[123] = 249;  
					yyal[124] = 249;  
					yyal[125] = 249;  
					yyal[126] = 258;  
					yyal[127] = 259;  
					yyal[128] = 261;  
					yyal[129] = 261;  
					yyal[130] = 263;  
					yyal[131] = 263;  
					yyal[132] = 263;  
					yyal[133] = 263;  
					yyal[134] = 263;  
					yyal[135] = 272;  
					yyal[136] = 292;  
					yyal[137] = 293;  
					yyal[138] = 293;  
					yyal[139] = 293;  
					yyal[140] = 293;  
					yyal[141] = 293;  
					yyal[142] = 293;  
					yyal[143] = 293;  
					yyal[144] = 293;  
					yyal[145] = 293;  
					yyal[146] = 312;  
					yyal[147] = 312;  
					yyal[148] = 331;  
					yyal[149] = 331;  
					yyal[150] = 331;  
					yyal[151] = 331;  
					yyal[152] = 331;  
					yyal[153] = 331;  
					yyal[154] = 331;  
					yyal[155] = 340;  
					yyal[156] = 341;  
					yyal[157] = 341;  
					yyal[158] = 345;  
					yyal[159] = 349;  
					yyal[160] = 349;  
					yyal[161] = 366;  
					yyal[162] = 366;  
					yyal[163] = 370;  
					yyal[164] = 379;  
					yyal[165] = 379;  
					yyal[166] = 379;  
					yyal[167] = 398;  
					yyal[168] = 398;  
					yyal[169] = 402;  
					yyal[170] = 403;  
					yyal[171] = 403;  
					yyal[172] = 404;  
					yyal[173] = 404;  
					yyal[174] = 413;  
					yyal[175] = 414;  
					yyal[176] = 418;  
					yyal[177] = 420;  
					yyal[178] = 420;  
					yyal[179] = 420;  
					yyal[180] = 420;  
					yyal[181] = 421;  
					yyal[182] = 425;  
					yyal[183] = 426; 

					yyah = new int[yynstates];
					yyah[0] = 4;  
					yyah[1] = 4;  
					yyah[2] = 4;  
					yyah[3] = 4;  
					yyah[4] = 4;  
					yyah[5] = 10;  
					yyah[6] = 10;  
					yyah[7] = 10;  
					yyah[8] = 15;  
					yyah[9] = 16;  
					yyah[10] = 17;  
					yyah[11] = 18;  
					yyah[12] = 19;  
					yyah[13] = 19;  
					yyah[14] = 20;  
					yyah[15] = 20;  
					yyah[16] = 20;  
					yyah[17] = 20;  
					yyah[18] = 21;  
					yyah[19] = 21;  
					yyah[20] = 21;  
					yyah[21] = 27;  
					yyah[22] = 27;  
					yyah[23] = 27;  
					yyah[24] = 27;  
					yyah[25] = 27;  
					yyah[26] = 27;  
					yyah[27] = 28;  
					yyah[28] = 29;  
					yyah[29] = 31;  
					yyah[30] = 31;  
					yyah[31] = 31;  
					yyah[32] = 32;  
					yyah[33] = 34;  
					yyah[34] = 34;  
					yyah[35] = 36;  
					yyah[36] = 40;  
					yyah[37] = 42;  
					yyah[38] = 44;  
					yyah[39] = 46;  
					yyah[40] = 46;  
					yyah[41] = 50;  
					yyah[42] = 50;  
					yyah[43] = 54;  
					yyah[44] = 55;  
					yyah[45] = 55;  
					yyah[46] = 55;  
					yyah[47] = 55;  
					yyah[48] = 55;  
					yyah[49] = 55;  
					yyah[50] = 55;  
					yyah[51] = 55;  
					yyah[52] = 55;  
					yyah[53] = 55;  
					yyah[54] = 55;  
					yyah[55] = 55;  
					yyah[56] = 56;  
					yyah[57] = 57;  
					yyah[58] = 66;  
					yyah[59] = 67;  
					yyah[60] = 68;  
					yyah[61] = 68;  
					yyah[62] = 68;  
					yyah[63] = 68;  
					yyah[64] = 70;  
					yyah[65] = 70;  
					yyah[66] = 71;  
					yyah[67] = 72;  
					yyah[68] = 97;  
					yyah[69] = 98;  
					yyah[70] = 98;  
					yyah[71] = 98;  
					yyah[72] = 98;  
					yyah[73] = 102;  
					yyah[74] = 102;  
					yyah[75] = 104;  
					yyah[76] = 104;  
					yyah[77] = 106;  
					yyah[78] = 108;  
					yyah[79] = 108;  
					yyah[80] = 115;  
					yyah[81] = 119;  
					yyah[82] = 123;  
					yyah[83] = 143;  
					yyah[84] = 163;  
					yyah[85] = 165;  
					yyah[86] = 165;  
					yyah[87] = 165;  
					yyah[88] = 165;  
					yyah[89] = 169;  
					yyah[90] = 169;  
					yyah[91] = 173;  
					yyah[92] = 178;  
					yyah[93] = 183;  
					yyah[94] = 202;  
					yyah[95] = 202;  
					yyah[96] = 202;  
					yyah[97] = 202;  
					yyah[98] = 202;  
					yyah[99] = 202;  
					yyah[100] = 202;  
					yyah[101] = 202;  
					yyah[102] = 202;  
					yyah[103] = 211;  
					yyah[104] = 212;  
					yyah[105] = 212;  
					yyah[106] = 229;  
					yyah[107] = 229;  
					yyah[108] = 229;  
					yyah[109] = 248;  
					yyah[110] = 248;  
					yyah[111] = 248;  
					yyah[112] = 248;  
					yyah[113] = 248;  
					yyah[114] = 248;  
					yyah[115] = 248;  
					yyah[116] = 248;  
					yyah[117] = 248;  
					yyah[118] = 248;  
					yyah[119] = 248;  
					yyah[120] = 248;  
					yyah[121] = 248;  
					yyah[122] = 248;  
					yyah[123] = 248;  
					yyah[124] = 248;  
					yyah[125] = 257;  
					yyah[126] = 258;  
					yyah[127] = 260;  
					yyah[128] = 260;  
					yyah[129] = 262;  
					yyah[130] = 262;  
					yyah[131] = 262;  
					yyah[132] = 262;  
					yyah[133] = 262;  
					yyah[134] = 271;  
					yyah[135] = 291;  
					yyah[136] = 292;  
					yyah[137] = 292;  
					yyah[138] = 292;  
					yyah[139] = 292;  
					yyah[140] = 292;  
					yyah[141] = 292;  
					yyah[142] = 292;  
					yyah[143] = 292;  
					yyah[144] = 292;  
					yyah[145] = 311;  
					yyah[146] = 311;  
					yyah[147] = 330;  
					yyah[148] = 330;  
					yyah[149] = 330;  
					yyah[150] = 330;  
					yyah[151] = 330;  
					yyah[152] = 330;  
					yyah[153] = 330;  
					yyah[154] = 339;  
					yyah[155] = 340;  
					yyah[156] = 340;  
					yyah[157] = 344;  
					yyah[158] = 348;  
					yyah[159] = 348;  
					yyah[160] = 365;  
					yyah[161] = 365;  
					yyah[162] = 369;  
					yyah[163] = 378;  
					yyah[164] = 378;  
					yyah[165] = 378;  
					yyah[166] = 397;  
					yyah[167] = 397;  
					yyah[168] = 401;  
					yyah[169] = 402;  
					yyah[170] = 402;  
					yyah[171] = 403;  
					yyah[172] = 403;  
					yyah[173] = 412;  
					yyah[174] = 413;  
					yyah[175] = 417;  
					yyah[176] = 419;  
					yyah[177] = 419;  
					yyah[178] = 419;  
					yyah[179] = 419;  
					yyah[180] = 420;  
					yyah[181] = 424;  
					yyah[182] = 425;  
					yyah[183] = 425; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 10;  
					yygl[2] = 10;  
					yygl[3] = 10;  
					yygl[4] = 10;  
					yygl[5] = 10;  
					yygl[6] = 18;  
					yygl[7] = 18;  
					yygl[8] = 18;  
					yygl[9] = 27;  
					yygl[10] = 27;  
					yygl[11] = 28;  
					yygl[12] = 29;  
					yygl[13] = 30;  
					yygl[14] = 30;  
					yygl[15] = 30;  
					yygl[16] = 30;  
					yygl[17] = 30;  
					yygl[18] = 30;  
					yygl[19] = 30;  
					yygl[20] = 30;  
					yygl[21] = 30;  
					yygl[22] = 33;  
					yygl[23] = 33;  
					yygl[24] = 33;  
					yygl[25] = 33;  
					yygl[26] = 33;  
					yygl[27] = 33;  
					yygl[28] = 33;  
					yygl[29] = 33;  
					yygl[30] = 33;  
					yygl[31] = 33;  
					yygl[32] = 33;  
					yygl[33] = 33;  
					yygl[34] = 33;  
					yygl[35] = 33;  
					yygl[36] = 36;  
					yygl[37] = 50;  
					yygl[38] = 51;  
					yygl[39] = 58;  
					yygl[40] = 65;  
					yygl[41] = 65;  
					yygl[42] = 70;  
					yygl[43] = 70;  
					yygl[44] = 75;  
					yygl[45] = 75;  
					yygl[46] = 75;  
					yygl[47] = 75;  
					yygl[48] = 75;  
					yygl[49] = 75;  
					yygl[50] = 75;  
					yygl[51] = 75;  
					yygl[52] = 75;  
					yygl[53] = 75;  
					yygl[54] = 75;  
					yygl[55] = 75;  
					yygl[56] = 75;  
					yygl[57] = 75;  
					yygl[58] = 75;  
					yygl[59] = 84;  
					yygl[60] = 84;  
					yygl[61] = 84;  
					yygl[62] = 84;  
					yygl[63] = 84;  
					yygl[64] = 84;  
					yygl[65] = 84;  
					yygl[66] = 84;  
					yygl[67] = 84;  
					yygl[68] = 84;  
					yygl[69] = 84;  
					yygl[70] = 84;  
					yygl[71] = 84;  
					yygl[72] = 84;  
					yygl[73] = 84;  
					yygl[74] = 98;  
					yygl[75] = 98;  
					yygl[76] = 98;  
					yygl[77] = 98;  
					yygl[78] = 98;  
					yygl[79] = 98;  
					yygl[80] = 98;  
					yygl[81] = 99;  
					yygl[82] = 106;  
					yygl[83] = 120;  
					yygl[84] = 129;  
					yygl[85] = 138;  
					yygl[86] = 144;  
					yygl[87] = 144;  
					yygl[88] = 144;  
					yygl[89] = 144;  
					yygl[90] = 149;  
					yygl[91] = 149;  
					yygl[92] = 156;  
					yygl[93] = 162;  
					yygl[94] = 168;  
					yygl[95] = 176;  
					yygl[96] = 176;  
					yygl[97] = 176;  
					yygl[98] = 176;  
					yygl[99] = 176;  
					yygl[100] = 176;  
					yygl[101] = 176;  
					yygl[102] = 176;  
					yygl[103] = 176;  
					yygl[104] = 177;  
					yygl[105] = 177;  
					yygl[106] = 177;  
					yygl[107] = 179;  
					yygl[108] = 179;  
					yygl[109] = 179;  
					yygl[110] = 187;  
					yygl[111] = 187;  
					yygl[112] = 187;  
					yygl[113] = 187;  
					yygl[114] = 187;  
					yygl[115] = 187;  
					yygl[116] = 187;  
					yygl[117] = 187;  
					yygl[118] = 187;  
					yygl[119] = 187;  
					yygl[120] = 187;  
					yygl[121] = 187;  
					yygl[122] = 187;  
					yygl[123] = 187;  
					yygl[124] = 187;  
					yygl[125] = 187;  
					yygl[126] = 188;  
					yygl[127] = 188;  
					yygl[128] = 188;  
					yygl[129] = 188;  
					yygl[130] = 188;  
					yygl[131] = 188;  
					yygl[132] = 188;  
					yygl[133] = 188;  
					yygl[134] = 188;  
					yygl[135] = 190;  
					yygl[136] = 199;  
					yygl[137] = 199;  
					yygl[138] = 199;  
					yygl[139] = 199;  
					yygl[140] = 199;  
					yygl[141] = 199;  
					yygl[142] = 199;  
					yygl[143] = 199;  
					yygl[144] = 199;  
					yygl[145] = 199;  
					yygl[146] = 207;  
					yygl[147] = 207;  
					yygl[148] = 215;  
					yygl[149] = 215;  
					yygl[150] = 215;  
					yygl[151] = 215;  
					yygl[152] = 215;  
					yygl[153] = 215;  
					yygl[154] = 215;  
					yygl[155] = 217;  
					yygl[156] = 217;  
					yygl[157] = 217;  
					yygl[158] = 222;  
					yygl[159] = 227;  
					yygl[160] = 227;  
					yygl[161] = 229;  
					yygl[162] = 229;  
					yygl[163] = 243;  
					yygl[164] = 245;  
					yygl[165] = 245;  
					yygl[166] = 245;  
					yygl[167] = 253;  
					yygl[168] = 253;  
					yygl[169] = 267;  
					yygl[170] = 267;  
					yygl[171] = 267;  
					yygl[172] = 267;  
					yygl[173] = 267;  
					yygl[174] = 269;  
					yygl[175] = 269;  
					yygl[176] = 274;  
					yygl[177] = 274;  
					yygl[178] = 274;  
					yygl[179] = 274;  
					yygl[180] = 274;  
					yygl[181] = 274;  
					yygl[182] = 288;  
					yygl[183] = 288; 

					yygh = new int[yynstates];
					yygh[0] = 9;  
					yygh[1] = 9;  
					yygh[2] = 9;  
					yygh[3] = 9;  
					yygh[4] = 9;  
					yygh[5] = 17;  
					yygh[6] = 17;  
					yygh[7] = 17;  
					yygh[8] = 26;  
					yygh[9] = 26;  
					yygh[10] = 27;  
					yygh[11] = 28;  
					yygh[12] = 29;  
					yygh[13] = 29;  
					yygh[14] = 29;  
					yygh[15] = 29;  
					yygh[16] = 29;  
					yygh[17] = 29;  
					yygh[18] = 29;  
					yygh[19] = 29;  
					yygh[20] = 29;  
					yygh[21] = 32;  
					yygh[22] = 32;  
					yygh[23] = 32;  
					yygh[24] = 32;  
					yygh[25] = 32;  
					yygh[26] = 32;  
					yygh[27] = 32;  
					yygh[28] = 32;  
					yygh[29] = 32;  
					yygh[30] = 32;  
					yygh[31] = 32;  
					yygh[32] = 32;  
					yygh[33] = 32;  
					yygh[34] = 32;  
					yygh[35] = 35;  
					yygh[36] = 49;  
					yygh[37] = 50;  
					yygh[38] = 57;  
					yygh[39] = 64;  
					yygh[40] = 64;  
					yygh[41] = 69;  
					yygh[42] = 69;  
					yygh[43] = 74;  
					yygh[44] = 74;  
					yygh[45] = 74;  
					yygh[46] = 74;  
					yygh[47] = 74;  
					yygh[48] = 74;  
					yygh[49] = 74;  
					yygh[50] = 74;  
					yygh[51] = 74;  
					yygh[52] = 74;  
					yygh[53] = 74;  
					yygh[54] = 74;  
					yygh[55] = 74;  
					yygh[56] = 74;  
					yygh[57] = 74;  
					yygh[58] = 83;  
					yygh[59] = 83;  
					yygh[60] = 83;  
					yygh[61] = 83;  
					yygh[62] = 83;  
					yygh[63] = 83;  
					yygh[64] = 83;  
					yygh[65] = 83;  
					yygh[66] = 83;  
					yygh[67] = 83;  
					yygh[68] = 83;  
					yygh[69] = 83;  
					yygh[70] = 83;  
					yygh[71] = 83;  
					yygh[72] = 83;  
					yygh[73] = 97;  
					yygh[74] = 97;  
					yygh[75] = 97;  
					yygh[76] = 97;  
					yygh[77] = 97;  
					yygh[78] = 97;  
					yygh[79] = 97;  
					yygh[80] = 98;  
					yygh[81] = 105;  
					yygh[82] = 119;  
					yygh[83] = 128;  
					yygh[84] = 137;  
					yygh[85] = 143;  
					yygh[86] = 143;  
					yygh[87] = 143;  
					yygh[88] = 143;  
					yygh[89] = 148;  
					yygh[90] = 148;  
					yygh[91] = 155;  
					yygh[92] = 161;  
					yygh[93] = 167;  
					yygh[94] = 175;  
					yygh[95] = 175;  
					yygh[96] = 175;  
					yygh[97] = 175;  
					yygh[98] = 175;  
					yygh[99] = 175;  
					yygh[100] = 175;  
					yygh[101] = 175;  
					yygh[102] = 175;  
					yygh[103] = 176;  
					yygh[104] = 176;  
					yygh[105] = 176;  
					yygh[106] = 178;  
					yygh[107] = 178;  
					yygh[108] = 178;  
					yygh[109] = 186;  
					yygh[110] = 186;  
					yygh[111] = 186;  
					yygh[112] = 186;  
					yygh[113] = 186;  
					yygh[114] = 186;  
					yygh[115] = 186;  
					yygh[116] = 186;  
					yygh[117] = 186;  
					yygh[118] = 186;  
					yygh[119] = 186;  
					yygh[120] = 186;  
					yygh[121] = 186;  
					yygh[122] = 186;  
					yygh[123] = 186;  
					yygh[124] = 186;  
					yygh[125] = 187;  
					yygh[126] = 187;  
					yygh[127] = 187;  
					yygh[128] = 187;  
					yygh[129] = 187;  
					yygh[130] = 187;  
					yygh[131] = 187;  
					yygh[132] = 187;  
					yygh[133] = 187;  
					yygh[134] = 189;  
					yygh[135] = 198;  
					yygh[136] = 198;  
					yygh[137] = 198;  
					yygh[138] = 198;  
					yygh[139] = 198;  
					yygh[140] = 198;  
					yygh[141] = 198;  
					yygh[142] = 198;  
					yygh[143] = 198;  
					yygh[144] = 198;  
					yygh[145] = 206;  
					yygh[146] = 206;  
					yygh[147] = 214;  
					yygh[148] = 214;  
					yygh[149] = 214;  
					yygh[150] = 214;  
					yygh[151] = 214;  
					yygh[152] = 214;  
					yygh[153] = 214;  
					yygh[154] = 216;  
					yygh[155] = 216;  
					yygh[156] = 216;  
					yygh[157] = 221;  
					yygh[158] = 226;  
					yygh[159] = 226;  
					yygh[160] = 228;  
					yygh[161] = 228;  
					yygh[162] = 242;  
					yygh[163] = 244;  
					yygh[164] = 244;  
					yygh[165] = 244;  
					yygh[166] = 252;  
					yygh[167] = 252;  
					yygh[168] = 266;  
					yygh[169] = 266;  
					yygh[170] = 266;  
					yygh[171] = 266;  
					yygh[172] = 266;  
					yygh[173] = 268;  
					yygh[174] = 268;  
					yygh[175] = 273;  
					yygh[176] = 273;  
					yygh[177] = 273;  
					yygh[178] = 273;  
					yygh[179] = 273;  
					yygh[180] = 273;  
					yygh[181] = 287;  
					yygh[182] = 287;  
					yygh[183] = 287; 

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
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(11,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-15);yyrc++;
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

			if (Regex.IsMatch(Rest,"^(IFDEF)")){
				Results.Add (t_IFDEF);
				ResultsV.Add(Regex.Match(Rest,"^(IFDEF)").Value);}

			if (Regex.IsMatch(Rest,"^(;)")){
				Results.Add (t_Char59);
				ResultsV.Add(Regex.Match(Rest,"^(;)").Value);}

			if (Regex.IsMatch(Rest,"^(IFNDEF)")){
				Results.Add (t_IFNDEF);
				ResultsV.Add(Regex.Match(Rest,"^(IFNDEF)").Value);}

			if (Regex.IsMatch(Rest,"^(IFELSE;)")){
				Results.Add (t_IFELSEChar59);
				ResultsV.Add(Regex.Match(Rest,"^(IFELSE;)").Value);}

			if (Regex.IsMatch(Rest,"^(ENDIF;)")){
				Results.Add (t_ENDIFChar59);
				ResultsV.Add(Regex.Match(Rest,"^(ENDIF;)").Value);}

			if (Regex.IsMatch(Rest,"^(DEFINE)")){
				Results.Add (t_DEFINE);
				ResultsV.Add(Regex.Match(Rest,"^(DEFINE)").Value);}

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

			if (Regex.IsMatch(Rest,"^(\\*=)")){
				Results.Add (t_Char42Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\*=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\+=)")){
				Results.Add (t_Char43Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\+=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\-=)")){
				Results.Add (t_Char45Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\-=)").Value);}

			if (Regex.IsMatch(Rest,"^(/=)")){
				Results.Add (t_Char47Char61);
				ResultsV.Add(Regex.Match(Rest,"^(/=)").Value);}

			if (Regex.IsMatch(Rest,"^(=)")){
				Results.Add (t_Char61);
				ResultsV.Add(Regex.Match(Rest,"^(=)").Value);}

			if (Regex.IsMatch(Rest,"^(ABS)")){
				Results.Add (t_ABS);
				ResultsV.Add(Regex.Match(Rest,"^(ABS)").Value);}

			if (Regex.IsMatch(Rest,"^(ACOS)")){
				Results.Add (t_ACOS);
				ResultsV.Add(Regex.Match(Rest,"^(ACOS)").Value);}

			if (Regex.IsMatch(Rest,"^(ASIN)")){
				Results.Add (t_ASIN);
				ResultsV.Add(Regex.Match(Rest,"^(ASIN)").Value);}

			if (Regex.IsMatch(Rest,"^(COS)")){
				Results.Add (t_COS);
				ResultsV.Add(Regex.Match(Rest,"^(COS)").Value);}

			if (Regex.IsMatch(Rest,"^(EXP)")){
				Results.Add (t_EXP);
				ResultsV.Add(Regex.Match(Rest,"^(EXP)").Value);}

			if (Regex.IsMatch(Rest,"^(INT)")){
				Results.Add (t_INT);
				ResultsV.Add(Regex.Match(Rest,"^(INT)").Value);}

			if (Regex.IsMatch(Rest,"^(LOG)")){
				Results.Add (t_LOG);
				ResultsV.Add(Regex.Match(Rest,"^(LOG)").Value);}

			if (Regex.IsMatch(Rest,"^(LOG10)")){
				Results.Add (t_LOG10);
				ResultsV.Add(Regex.Match(Rest,"^(LOG10)").Value);}

			if (Regex.IsMatch(Rest,"^(LOG2)")){
				Results.Add (t_LOG2);
				ResultsV.Add(Regex.Match(Rest,"^(LOG2)").Value);}

			if (Regex.IsMatch(Rest,"^(RANDOM)")){
				Results.Add (t_RANDOM);
				ResultsV.Add(Regex.Match(Rest,"^(RANDOM)").Value);}

			if (Regex.IsMatch(Rest,"^(SIGN)")){
				Results.Add (t_SIGN);
				ResultsV.Add(Regex.Match(Rest,"^(SIGN)").Value);}

			if (Regex.IsMatch(Rest,"^(SIN)")){
				Results.Add (t_SIN);
				ResultsV.Add(Regex.Match(Rest,"^(SIN)").Value);}

			if (Regex.IsMatch(Rest,"^(SQRT)")){
				Results.Add (t_SQRT);
				ResultsV.Add(Regex.Match(Rest,"^(SQRT)").Value);}

			if (Regex.IsMatch(Rest,"^(TAN)")){
				Results.Add (t_TAN);
				ResultsV.Add(Regex.Match(Rest,"^(TAN)").Value);}

			if (Regex.IsMatch(Rest,"^(IF)")){
				Results.Add (t_IF);
				ResultsV.Add(Regex.Match(Rest,"^(IF)").Value);}

			if (Regex.IsMatch(Rest,"^(ELSE)")){
				Results.Add (t_ELSE);
				ResultsV.Add(Regex.Match(Rest,"^(ELSE)").Value);}

			if (Regex.IsMatch(Rest,"^(WHILE)")){
				Results.Add (t_WHILE);
				ResultsV.Add(Regex.Match(Rest,"^(WHILE)").Value);}

			if (Regex.IsMatch(Rest,"^(\\(Comparison\\))")){
				Results.Add (t_Char40ComparisonChar41);
				ResultsV.Add(Regex.Match(Rest,"^(\\(Comparison\\))").Value);}

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

			if (Regex.IsMatch(Rest,"^([A-Za-z][A-Za-z0-9_]*)")){
				Results.Add (t_identifier);
				ResultsV.Add(Regex.Match(Rest,"^([A-Za-z][A-Za-z0-9_]*)").Value);}

			if (Regex.IsMatch(Rest,"^(<[^<;:\" ]+>)")){
				Results.Add (t_file);
				ResultsV.Add(Regex.Match(Rest,"^(<[^<;:\" ]+>)").Value);}

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
