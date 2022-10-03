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
         yyval = yyv[yysp-0];
         
       break;
							case  116 : 
         yyval = "";
         
       break;
							case  117 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
                                default : return;
                        }
                }

                public void InitTables()
                {
                        ////////////////////////////////////////////////////////////////
                        /// Init Table code:
                        ////////////////////////////////////////////////////////////////

					int yynacts   = 481;
					int yyngotos  = 301;
					int yynstates = 186;
					int yynrules  = 121;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,10);yyac++; 
					yya[yyac] = new YYARec(259,11);yyac++; 
					yya[yyac] = new YYARec(262,12);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(311,25);yyac++; 
					yya[yyac] = new YYARec(312,26);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(257,10);yyac++; 
					yya[yyac] = new YYARec(259,11);yyac++; 
					yya[yyac] = new YYARec(262,12);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(0,-1 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,31);yyac++; 
					yya[yyac] = new YYARec(258,32);yyac++; 
					yya[yyac] = new YYARec(263,36);yyac++; 
					yya[yyac] = new YYARec(264,37);yyac++; 
					yya[yyac] = new YYARec(308,38);yyac++; 
					yya[yyac] = new YYARec(311,25);yyac++; 
					yya[yyac] = new YYARec(312,26);yyac++; 
					yya[yyac] = new YYARec(258,-112 );yyac++; 
					yya[yyac] = new YYARec(258,39);yyac++; 
					yya[yyac] = new YYARec(258,40);yyac++; 
					yya[yyac] = new YYARec(258,41);yyac++; 
					yya[yyac] = new YYARec(263,42);yyac++; 
					yya[yyac] = new YYARec(258,43);yyac++; 
					yya[yyac] = new YYARec(263,44);yyac++; 
					yya[yyac] = new YYARec(258,-26 );yyac++; 
					yya[yyac] = new YYARec(311,25);yyac++; 
					yya[yyac] = new YYARec(312,26);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(296,60);yyac++; 
					yya[yyac] = new YYARec(298,61);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(262,12);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(262,12);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(258,73);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(296,60);yyac++; 
					yya[yyac] = new YYARec(298,61);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(258,75);yyac++; 
					yya[yyac] = new YYARec(265,76);yyac++; 
					yya[yyac] = new YYARec(263,83);yyac++; 
					yya[yyac] = new YYARec(266,84);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(311,25);yyac++; 
					yya[yyac] = new YYARec(312,26);yyac++; 
					yya[yyac] = new YYARec(258,-39 );yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(267,85);yyac++; 
					yya[yyac] = new YYARec(267,86);yyac++; 
					yya[yyac] = new YYARec(260,87);yyac++; 
					yya[yyac] = new YYARec(261,88);yyac++; 
					yya[yyac] = new YYARec(258,89);yyac++; 
					yya[yyac] = new YYARec(258,90);yyac++; 
					yya[yyac] = new YYARec(308,38);yyac++; 
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
					yya[yyac] = new YYARec(263,91);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(296,60);yyac++; 
					yya[yyac] = new YYARec(298,61);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(263,93);yyac++; 
					yya[yyac] = new YYARec(258,-46 );yyac++; 
					yya[yyac] = new YYARec(263,94);yyac++; 
					yya[yyac] = new YYARec(258,-49 );yyac++; 
					yya[yyac] = new YYARec(263,95);yyac++; 
					yya[yyac] = new YYARec(258,-50 );yyac++; 
					yya[yyac] = new YYARec(277,97);yyac++; 
					yya[yyac] = new YYARec(278,98);yyac++; 
					yya[yyac] = new YYARec(279,99);yyac++; 
					yya[yyac] = new YYARec(280,100);yyac++; 
					yya[yyac] = new YYARec(281,101);yyac++; 
					yya[yyac] = new YYARec(258,-47 );yyac++; 
					yya[yyac] = new YYARec(263,-47 );yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(296,60);yyac++; 
					yya[yyac] = new YYARec(298,61);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(267,111);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(282,112);yyac++; 
					yya[yyac] = new YYARec(283,113);yyac++; 
					yya[yyac] = new YYARec(284,114);yyac++; 
					yya[yyac] = new YYARec(285,115);yyac++; 
					yya[yyac] = new YYARec(286,116);yyac++; 
					yya[yyac] = new YYARec(287,117);yyac++; 
					yya[yyac] = new YYARec(288,118);yyac++; 
					yya[yyac] = new YYARec(289,119);yyac++; 
					yya[yyac] = new YYARec(290,120);yyac++; 
					yya[yyac] = new YYARec(291,121);yyac++; 
					yya[yyac] = new YYARec(292,122);yyac++; 
					yya[yyac] = new YYARec(293,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(299,126);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(267,111);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(282,112);yyac++; 
					yya[yyac] = new YYARec(283,113);yyac++; 
					yya[yyac] = new YYARec(284,114);yyac++; 
					yya[yyac] = new YYARec(285,115);yyac++; 
					yya[yyac] = new YYARec(286,116);yyac++; 
					yya[yyac] = new YYARec(287,117);yyac++; 
					yya[yyac] = new YYARec(288,118);yyac++; 
					yya[yyac] = new YYARec(289,119);yyac++; 
					yya[yyac] = new YYARec(290,120);yyac++; 
					yya[yyac] = new YYARec(291,121);yyac++; 
					yya[yyac] = new YYARec(292,122);yyac++; 
					yya[yyac] = new YYARec(293,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(299,126);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(262,12);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(312,26);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(312,26);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(267,111);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(282,112);yyac++; 
					yya[yyac] = new YYARec(283,113);yyac++; 
					yya[yyac] = new YYARec(284,114);yyac++; 
					yya[yyac] = new YYARec(285,115);yyac++; 
					yya[yyac] = new YYARec(286,116);yyac++; 
					yya[yyac] = new YYARec(287,117);yyac++; 
					yya[yyac] = new YYARec(288,118);yyac++; 
					yya[yyac] = new YYARec(289,119);yyac++; 
					yya[yyac] = new YYARec(290,120);yyac++; 
					yya[yyac] = new YYARec(291,121);yyac++; 
					yya[yyac] = new YYARec(292,122);yyac++; 
					yya[yyac] = new YYARec(293,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(268,138);yyac++; 
					yya[yyac] = new YYARec(300,139);yyac++; 
					yya[yyac] = new YYARec(301,140);yyac++; 
					yya[yyac] = new YYARec(302,141);yyac++; 
					yya[yyac] = new YYARec(303,142);yyac++; 
					yya[yyac] = new YYARec(304,143);yyac++; 
					yya[yyac] = new YYARec(305,144);yyac++; 
					yya[yyac] = new YYARec(306,145);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(267,147);yyac++; 
					yya[yyac] = new YYARec(269,150);yyac++; 
					yya[yyac] = new YYARec(270,151);yyac++; 
					yya[yyac] = new YYARec(271,152);yyac++; 
					yya[yyac] = new YYARec(272,153);yyac++; 
					yya[yyac] = new YYARec(273,154);yyac++; 
					yya[yyac] = new YYARec(274,155);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(268,-98 );yyac++; 
					yya[yyac] = new YYARec(300,-98 );yyac++; 
					yya[yyac] = new YYARec(301,-98 );yyac++; 
					yya[yyac] = new YYARec(302,-98 );yyac++; 
					yya[yyac] = new YYARec(303,-98 );yyac++; 
					yya[yyac] = new YYARec(304,-98 );yyac++; 
					yya[yyac] = new YYARec(305,-98 );yyac++; 
					yya[yyac] = new YYARec(306,-98 );yyac++; 
					yya[yyac] = new YYARec(307,-98 );yyac++; 
					yya[yyac] = new YYARec(267,111);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(282,112);yyac++; 
					yya[yyac] = new YYARec(283,113);yyac++; 
					yya[yyac] = new YYARec(284,114);yyac++; 
					yya[yyac] = new YYARec(285,115);yyac++; 
					yya[yyac] = new YYARec(286,116);yyac++; 
					yya[yyac] = new YYARec(287,117);yyac++; 
					yya[yyac] = new YYARec(288,118);yyac++; 
					yya[yyac] = new YYARec(289,119);yyac++; 
					yya[yyac] = new YYARec(290,120);yyac++; 
					yya[yyac] = new YYARec(291,121);yyac++; 
					yya[yyac] = new YYARec(292,122);yyac++; 
					yya[yyac] = new YYARec(293,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(267,-89 );yyac++; 
					yya[yyac] = new YYARec(258,-113 );yyac++; 
					yya[yyac] = new YYARec(268,-113 );yyac++; 
					yya[yyac] = new YYARec(269,-113 );yyac++; 
					yya[yyac] = new YYARec(270,-113 );yyac++; 
					yya[yyac] = new YYARec(271,-113 );yyac++; 
					yya[yyac] = new YYARec(272,-113 );yyac++; 
					yya[yyac] = new YYARec(273,-113 );yyac++; 
					yya[yyac] = new YYARec(274,-113 );yyac++; 
					yya[yyac] = new YYARec(275,-113 );yyac++; 
					yya[yyac] = new YYARec(276,-113 );yyac++; 
					yya[yyac] = new YYARec(300,-113 );yyac++; 
					yya[yyac] = new YYARec(301,-113 );yyac++; 
					yya[yyac] = new YYARec(302,-113 );yyac++; 
					yya[yyac] = new YYARec(303,-113 );yyac++; 
					yya[yyac] = new YYARec(304,-113 );yyac++; 
					yya[yyac] = new YYARec(305,-113 );yyac++; 
					yya[yyac] = new YYARec(306,-113 );yyac++; 
					yya[yyac] = new YYARec(307,-113 );yyac++; 
					yya[yyac] = new YYARec(308,-113 );yyac++; 
					yya[yyac] = new YYARec(268,157);yyac++; 
					yya[yyac] = new YYARec(300,139);yyac++; 
					yya[yyac] = new YYARec(301,140);yyac++; 
					yya[yyac] = new YYARec(302,141);yyac++; 
					yya[yyac] = new YYARec(303,142);yyac++; 
					yya[yyac] = new YYARec(304,143);yyac++; 
					yya[yyac] = new YYARec(305,144);yyac++; 
					yya[yyac] = new YYARec(306,145);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(261,158);yyac++; 
					yya[yyac] = new YYARec(263,159);yyac++; 
					yya[yyac] = new YYARec(258,-24 );yyac++; 
					yya[yyac] = new YYARec(263,160);yyac++; 
					yya[yyac] = new YYARec(258,-55 );yyac++; 
					yya[yyac] = new YYARec(269,150);yyac++; 
					yya[yyac] = new YYARec(270,151);yyac++; 
					yya[yyac] = new YYARec(271,152);yyac++; 
					yya[yyac] = new YYARec(272,153);yyac++; 
					yya[yyac] = new YYARec(273,154);yyac++; 
					yya[yyac] = new YYARec(274,155);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(258,-57 );yyac++; 
					yya[yyac] = new YYARec(267,111);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(282,112);yyac++; 
					yya[yyac] = new YYARec(283,113);yyac++; 
					yya[yyac] = new YYARec(284,114);yyac++; 
					yya[yyac] = new YYARec(285,115);yyac++; 
					yya[yyac] = new YYARec(286,116);yyac++; 
					yya[yyac] = new YYARec(287,117);yyac++; 
					yya[yyac] = new YYARec(288,118);yyac++; 
					yya[yyac] = new YYARec(289,119);yyac++; 
					yya[yyac] = new YYARec(290,120);yyac++; 
					yya[yyac] = new YYARec(291,121);yyac++; 
					yya[yyac] = new YYARec(292,122);yyac++; 
					yya[yyac] = new YYARec(293,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(299,163);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(264,164);yyac++; 
					yya[yyac] = new YYARec(267,111);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(282,112);yyac++; 
					yya[yyac] = new YYARec(283,113);yyac++; 
					yya[yyac] = new YYARec(284,114);yyac++; 
					yya[yyac] = new YYARec(285,115);yyac++; 
					yya[yyac] = new YYARec(286,116);yyac++; 
					yya[yyac] = new YYARec(287,117);yyac++; 
					yya[yyac] = new YYARec(288,118);yyac++; 
					yya[yyac] = new YYARec(289,119);yyac++; 
					yya[yyac] = new YYARec(290,120);yyac++; 
					yya[yyac] = new YYARec(291,121);yyac++; 
					yya[yyac] = new YYARec(292,122);yyac++; 
					yya[yyac] = new YYARec(293,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(267,168);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(282,112);yyac++; 
					yya[yyac] = new YYARec(283,113);yyac++; 
					yya[yyac] = new YYARec(284,114);yyac++; 
					yya[yyac] = new YYARec(285,115);yyac++; 
					yya[yyac] = new YYARec(286,116);yyac++; 
					yya[yyac] = new YYARec(287,117);yyac++; 
					yya[yyac] = new YYARec(288,118);yyac++; 
					yya[yyac] = new YYARec(289,119);yyac++; 
					yya[yyac] = new YYARec(290,120);yyac++; 
					yya[yyac] = new YYARec(291,121);yyac++; 
					yya[yyac] = new YYARec(292,122);yyac++; 
					yya[yyac] = new YYARec(293,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(268,169);yyac++; 
					yya[yyac] = new YYARec(269,150);yyac++; 
					yya[yyac] = new YYARec(270,151);yyac++; 
					yya[yyac] = new YYARec(271,152);yyac++; 
					yya[yyac] = new YYARec(272,153);yyac++; 
					yya[yyac] = new YYARec(273,154);yyac++; 
					yya[yyac] = new YYARec(274,155);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(264,170);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(269,150);yyac++; 
					yya[yyac] = new YYARec(270,151);yyac++; 
					yya[yyac] = new YYARec(271,152);yyac++; 
					yya[yyac] = new YYARec(272,153);yyac++; 
					yya[yyac] = new YYARec(273,154);yyac++; 
					yya[yyac] = new YYARec(274,155);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(268,-101 );yyac++; 
					yya[yyac] = new YYARec(300,-101 );yyac++; 
					yya[yyac] = new YYARec(301,-101 );yyac++; 
					yya[yyac] = new YYARec(302,-101 );yyac++; 
					yya[yyac] = new YYARec(303,-101 );yyac++; 
					yya[yyac] = new YYARec(304,-101 );yyac++; 
					yya[yyac] = new YYARec(305,-101 );yyac++; 
					yya[yyac] = new YYARec(306,-101 );yyac++; 
					yya[yyac] = new YYARec(307,-101 );yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(296,60);yyac++; 
					yya[yyac] = new YYARec(298,61);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(268,174);yyac++; 
					yya[yyac] = new YYARec(269,150);yyac++; 
					yya[yyac] = new YYARec(270,151);yyac++; 
					yya[yyac] = new YYARec(271,152);yyac++; 
					yya[yyac] = new YYARec(272,153);yyac++; 
					yya[yyac] = new YYARec(273,154);yyac++; 
					yya[yyac] = new YYARec(274,155);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(267,111);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(282,112);yyac++; 
					yya[yyac] = new YYARec(283,113);yyac++; 
					yya[yyac] = new YYARec(284,114);yyac++; 
					yya[yyac] = new YYARec(285,115);yyac++; 
					yya[yyac] = new YYARec(286,116);yyac++; 
					yya[yyac] = new YYARec(287,117);yyac++; 
					yya[yyac] = new YYARec(288,118);yyac++; 
					yya[yyac] = new YYARec(289,119);yyac++; 
					yya[yyac] = new YYARec(290,120);yyac++; 
					yya[yyac] = new YYARec(291,121);yyac++; 
					yya[yyac] = new YYARec(292,122);yyac++; 
					yya[yyac] = new YYARec(293,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(296,60);yyac++; 
					yya[yyac] = new YYARec(298,61);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(263,177);yyac++; 
					yya[yyac] = new YYARec(265,178);yyac++; 
					yya[yyac] = new YYARec(268,179);yyac++; 
					yya[yyac] = new YYARec(269,150);yyac++; 
					yya[yyac] = new YYARec(270,151);yyac++; 
					yya[yyac] = new YYARec(271,152);yyac++; 
					yya[yyac] = new YYARec(272,153);yyac++; 
					yya[yyac] = new YYARec(273,154);yyac++; 
					yya[yyac] = new YYARec(274,155);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(265,180);yyac++; 
					yya[yyac] = new YYARec(275,23);yyac++; 
					yya[yyac] = new YYARec(276,24);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(309,-116 );yyac++; 
					yya[yyac] = new YYARec(297,182);yyac++; 
					yya[yyac] = new YYARec(265,-95 );yyac++; 
					yya[yyac] = new YYARec(291,-95 );yyac++; 
					yya[yyac] = new YYARec(296,-95 );yyac++; 
					yya[yyac] = new YYARec(298,-95 );yyac++; 
					yya[yyac] = new YYARec(310,-95 );yyac++; 
					yya[yyac] = new YYARec(264,183);yyac++; 
					yya[yyac] = new YYARec(291,13);yyac++; 
					yya[yyac] = new YYARec(296,60);yyac++; 
					yya[yyac] = new YYARec(298,61);yyac++; 
					yya[yyac] = new YYARec(310,14);yyac++; 
					yya[yyac] = new YYARec(265,-31 );yyac++; 
					yya[yyac] = new YYARec(265,185);yyac++;

					yyg[yygc] = new YYARec(-11,1);yygc++; 
					yyg[yygc] = new YYARec(-10,2);yygc++; 
					yyg[yygc] = new YYARec(-9,3);yygc++; 
					yyg[yygc] = new YYARec(-8,4);yygc++; 
					yyg[yygc] = new YYARec(-6,5);yygc++; 
					yyg[yygc] = new YYARec(-5,6);yygc++; 
					yyg[yygc] = new YYARec(-4,7);yygc++; 
					yyg[yygc] = new YYARec(-3,8);yygc++; 
					yyg[yygc] = new YYARec(-2,9);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-16,17);yygc++; 
					yyg[yygc] = new YYARec(-15,18);yygc++; 
					yyg[yygc] = new YYARec(-14,19);yygc++; 
					yyg[yygc] = new YYARec(-13,20);yygc++; 
					yyg[yygc] = new YYARec(-12,21);yygc++; 
					yyg[yygc] = new YYARec(-6,22);yygc++; 
					yyg[yygc] = new YYARec(-11,1);yygc++; 
					yyg[yygc] = new YYARec(-10,2);yygc++; 
					yyg[yygc] = new YYARec(-9,3);yygc++; 
					yyg[yygc] = new YYARec(-8,4);yygc++; 
					yyg[yygc] = new YYARec(-6,5);yygc++; 
					yyg[yygc] = new YYARec(-5,6);yygc++; 
					yyg[yygc] = new YYARec(-4,7);yygc++; 
					yyg[yygc] = new YYARec(-3,8);yygc++; 
					yyg[yygc] = new YYARec(-2,27);yygc++; 
					yyg[yygc] = new YYARec(-6,28);yygc++; 
					yyg[yygc] = new YYARec(-6,29);yygc++; 
					yyg[yygc] = new YYARec(-6,30);yygc++; 
					yyg[yygc] = new YYARec(-17,33);yygc++; 
					yyg[yygc] = new YYARec(-16,34);yygc++; 
					yyg[yygc] = new YYARec(-15,35);yygc++; 
					yyg[yygc] = new YYARec(-17,45);yygc++; 
					yyg[yygc] = new YYARec(-16,34);yygc++; 
					yyg[yygc] = new YYARec(-15,35);yygc++; 
					yyg[yygc] = new YYARec(-31,46);yygc++; 
					yyg[yygc] = new YYARec(-30,47);yygc++; 
					yyg[yygc] = new YYARec(-29,48);yygc++; 
					yyg[yygc] = new YYARec(-28,49);yygc++; 
					yyg[yygc] = new YYARec(-27,50);yygc++; 
					yyg[yygc] = new YYARec(-26,51);yygc++; 
					yyg[yygc] = new YYARec(-25,52);yygc++; 
					yyg[yygc] = new YYARec(-24,53);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-22,55);yygc++; 
					yyg[yygc] = new YYARec(-21,56);yygc++; 
					yyg[yygc] = new YYARec(-20,57);yygc++; 
					yyg[yygc] = new YYARec(-19,58);yygc++; 
					yyg[yygc] = new YYARec(-6,59);yygc++; 
					yyg[yygc] = new YYARec(-6,62);yygc++; 
					yyg[yygc] = new YYARec(-11,1);yygc++; 
					yyg[yygc] = new YYARec(-10,2);yygc++; 
					yyg[yygc] = new YYARec(-9,3);yygc++; 
					yyg[yygc] = new YYARec(-8,4);yygc++; 
					yyg[yygc] = new YYARec(-7,64);yygc++; 
					yyg[yygc] = new YYARec(-6,5);yygc++; 
					yyg[yygc] = new YYARec(-4,65);yygc++; 
					yyg[yygc] = new YYARec(-11,1);yygc++; 
					yyg[yygc] = new YYARec(-10,2);yygc++; 
					yyg[yygc] = new YYARec(-9,3);yygc++; 
					yyg[yygc] = new YYARec(-8,4);yygc++; 
					yyg[yygc] = new YYARec(-7,66);yygc++; 
					yyg[yygc] = new YYARec(-6,5);yygc++; 
					yyg[yygc] = new YYARec(-4,65);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-13,67);yygc++; 
					yyg[yygc] = new YYARec(-12,68);yygc++; 
					yyg[yygc] = new YYARec(-6,69);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-18,70);yygc++; 
					yyg[yygc] = new YYARec(-13,71);yygc++; 
					yyg[yygc] = new YYARec(-6,72);yygc++; 
					yyg[yygc] = new YYARec(-31,46);yygc++; 
					yyg[yygc] = new YYARec(-30,47);yygc++; 
					yyg[yygc] = new YYARec(-29,48);yygc++; 
					yyg[yygc] = new YYARec(-28,49);yygc++; 
					yyg[yygc] = new YYARec(-27,50);yygc++; 
					yyg[yygc] = new YYARec(-26,51);yygc++; 
					yyg[yygc] = new YYARec(-25,52);yygc++; 
					yyg[yygc] = new YYARec(-24,53);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-22,55);yygc++; 
					yyg[yygc] = new YYARec(-21,56);yygc++; 
					yyg[yygc] = new YYARec(-20,57);yygc++; 
					yyg[yygc] = new YYARec(-19,74);yygc++; 
					yyg[yygc] = new YYARec(-6,59);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-33,77);yygc++; 
					yyg[yygc] = new YYARec(-32,78);yygc++; 
					yyg[yygc] = new YYARec(-16,79);yygc++; 
					yyg[yygc] = new YYARec(-15,80);yygc++; 
					yyg[yygc] = new YYARec(-13,81);yygc++; 
					yyg[yygc] = new YYARec(-12,82);yygc++; 
					yyg[yygc] = new YYARec(-6,69);yygc++; 
					yyg[yygc] = new YYARec(-31,46);yygc++; 
					yyg[yygc] = new YYARec(-30,47);yygc++; 
					yyg[yygc] = new YYARec(-29,48);yygc++; 
					yyg[yygc] = new YYARec(-28,49);yygc++; 
					yyg[yygc] = new YYARec(-27,50);yygc++; 
					yyg[yygc] = new YYARec(-26,51);yygc++; 
					yyg[yygc] = new YYARec(-25,52);yygc++; 
					yyg[yygc] = new YYARec(-24,53);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-22,55);yygc++; 
					yyg[yygc] = new YYARec(-21,56);yygc++; 
					yyg[yygc] = new YYARec(-20,57);yygc++; 
					yyg[yygc] = new YYARec(-19,92);yygc++; 
					yyg[yygc] = new YYARec(-6,59);yygc++; 
					yyg[yygc] = new YYARec(-34,96);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-33,77);yygc++; 
					yyg[yygc] = new YYARec(-32,102);yygc++; 
					yyg[yygc] = new YYARec(-13,81);yygc++; 
					yyg[yygc] = new YYARec(-12,103);yygc++; 
					yyg[yygc] = new YYARec(-6,69);yygc++; 
					yyg[yygc] = new YYARec(-31,46);yygc++; 
					yyg[yygc] = new YYARec(-30,47);yygc++; 
					yyg[yygc] = new YYARec(-29,48);yygc++; 
					yyg[yygc] = new YYARec(-28,49);yygc++; 
					yyg[yygc] = new YYARec(-27,50);yygc++; 
					yyg[yygc] = new YYARec(-26,51);yygc++; 
					yyg[yygc] = new YYARec(-25,52);yygc++; 
					yyg[yygc] = new YYARec(-24,53);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-22,55);yygc++; 
					yyg[yygc] = new YYARec(-21,56);yygc++; 
					yyg[yygc] = new YYARec(-20,57);yygc++; 
					yyg[yygc] = new YYARec(-19,104);yygc++; 
					yyg[yygc] = new YYARec(-6,59);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-41,105);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-39,106);yygc++; 
					yyg[yygc] = new YYARec(-36,107);yygc++; 
					yyg[yygc] = new YYARec(-35,108);yygc++; 
					yyg[yygc] = new YYARec(-13,109);yygc++; 
					yyg[yygc] = new YYARec(-12,110);yygc++; 
					yyg[yygc] = new YYARec(-6,69);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-41,127);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-39,106);yygc++; 
					yyg[yygc] = new YYARec(-36,107);yygc++; 
					yyg[yygc] = new YYARec(-35,108);yygc++; 
					yyg[yygc] = new YYARec(-13,109);yygc++; 
					yyg[yygc] = new YYARec(-12,110);yygc++; 
					yyg[yygc] = new YYARec(-6,69);yygc++; 
					yyg[yygc] = new YYARec(-11,1);yygc++; 
					yyg[yygc] = new YYARec(-10,2);yygc++; 
					yyg[yygc] = new YYARec(-9,3);yygc++; 
					yyg[yygc] = new YYARec(-8,4);yygc++; 
					yyg[yygc] = new YYARec(-6,5);yygc++; 
					yyg[yygc] = new YYARec(-4,128);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-18,129);yygc++; 
					yyg[yygc] = new YYARec(-13,71);yygc++; 
					yyg[yygc] = new YYARec(-6,72);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-33,77);yygc++; 
					yyg[yygc] = new YYARec(-32,130);yygc++; 
					yyg[yygc] = new YYARec(-13,81);yygc++; 
					yyg[yygc] = new YYARec(-12,103);yygc++; 
					yyg[yygc] = new YYARec(-6,69);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-18,131);yygc++; 
					yyg[yygc] = new YYARec(-15,132);yygc++; 
					yyg[yygc] = new YYARec(-13,71);yygc++; 
					yyg[yygc] = new YYARec(-6,72);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-15,133);yygc++; 
					yyg[yygc] = new YYARec(-13,134);yygc++; 
					yyg[yygc] = new YYARec(-12,135);yygc++; 
					yyg[yygc] = new YYARec(-6,69);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-39,106);yygc++; 
					yyg[yygc] = new YYARec(-36,107);yygc++; 
					yyg[yygc] = new YYARec(-35,136);yygc++; 
					yyg[yygc] = new YYARec(-13,109);yygc++; 
					yyg[yygc] = new YYARec(-12,110);yygc++; 
					yyg[yygc] = new YYARec(-6,69);yygc++; 
					yyg[yygc] = new YYARec(-42,137);yygc++; 
					yyg[yygc] = new YYARec(-40,148);yygc++; 
					yyg[yygc] = new YYARec(-37,149);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-39,106);yygc++; 
					yyg[yygc] = new YYARec(-36,107);yygc++; 
					yyg[yygc] = new YYARec(-35,156);yygc++; 
					yyg[yygc] = new YYARec(-13,109);yygc++; 
					yyg[yygc] = new YYARec(-12,110);yygc++; 
					yyg[yygc] = new YYARec(-6,69);yygc++; 
					yyg[yygc] = new YYARec(-42,137);yygc++; 
					yyg[yygc] = new YYARec(-40,148);yygc++; 
					yyg[yygc] = new YYARec(-37,149);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-43,161);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-39,106);yygc++; 
					yyg[yygc] = new YYARec(-36,107);yygc++; 
					yyg[yygc] = new YYARec(-35,162);yygc++; 
					yyg[yygc] = new YYARec(-13,109);yygc++; 
					yyg[yygc] = new YYARec(-12,110);yygc++; 
					yyg[yygc] = new YYARec(-6,69);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-39,106);yygc++; 
					yyg[yygc] = new YYARec(-36,107);yygc++; 
					yyg[yygc] = new YYARec(-35,165);yygc++; 
					yyg[yygc] = new YYARec(-13,109);yygc++; 
					yyg[yygc] = new YYARec(-12,110);yygc++; 
					yyg[yygc] = new YYARec(-6,69);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-39,106);yygc++; 
					yyg[yygc] = new YYARec(-38,166);yygc++; 
					yyg[yygc] = new YYARec(-36,167);yygc++; 
					yyg[yygc] = new YYARec(-13,109);yygc++; 
					yyg[yygc] = new YYARec(-12,110);yygc++; 
					yyg[yygc] = new YYARec(-6,69);yygc++; 
					yyg[yygc] = new YYARec(-40,148);yygc++; 
					yyg[yygc] = new YYARec(-37,149);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-18,171);yygc++; 
					yyg[yygc] = new YYARec(-13,71);yygc++; 
					yyg[yygc] = new YYARec(-6,72);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-18,172);yygc++; 
					yyg[yygc] = new YYARec(-13,71);yygc++; 
					yyg[yygc] = new YYARec(-6,72);yygc++; 
					yyg[yygc] = new YYARec(-40,148);yygc++; 
					yyg[yygc] = new YYARec(-37,149);yygc++; 
					yyg[yygc] = new YYARec(-31,46);yygc++; 
					yyg[yygc] = new YYARec(-30,47);yygc++; 
					yyg[yygc] = new YYARec(-29,48);yygc++; 
					yyg[yygc] = new YYARec(-28,49);yygc++; 
					yyg[yygc] = new YYARec(-27,50);yygc++; 
					yyg[yygc] = new YYARec(-26,51);yygc++; 
					yyg[yygc] = new YYARec(-25,52);yygc++; 
					yyg[yygc] = new YYARec(-24,53);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-22,55);yygc++; 
					yyg[yygc] = new YYARec(-21,56);yygc++; 
					yyg[yygc] = new YYARec(-20,57);yygc++; 
					yyg[yygc] = new YYARec(-19,173);yygc++; 
					yyg[yygc] = new YYARec(-6,59);yygc++; 
					yyg[yygc] = new YYARec(-40,148);yygc++; 
					yyg[yygc] = new YYARec(-37,149);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-39,106);yygc++; 
					yyg[yygc] = new YYARec(-36,107);yygc++; 
					yyg[yygc] = new YYARec(-35,175);yygc++; 
					yyg[yygc] = new YYARec(-13,109);yygc++; 
					yyg[yygc] = new YYARec(-12,110);yygc++; 
					yyg[yygc] = new YYARec(-6,69);yygc++; 
					yyg[yygc] = new YYARec(-31,46);yygc++; 
					yyg[yygc] = new YYARec(-30,47);yygc++; 
					yyg[yygc] = new YYARec(-29,48);yygc++; 
					yyg[yygc] = new YYARec(-28,49);yygc++; 
					yyg[yygc] = new YYARec(-27,50);yygc++; 
					yyg[yygc] = new YYARec(-26,51);yygc++; 
					yyg[yygc] = new YYARec(-25,52);yygc++; 
					yyg[yygc] = new YYARec(-24,53);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-22,55);yygc++; 
					yyg[yygc] = new YYARec(-21,56);yygc++; 
					yyg[yygc] = new YYARec(-20,57);yygc++; 
					yyg[yygc] = new YYARec(-19,176);yygc++; 
					yyg[yygc] = new YYARec(-6,59);yygc++; 
					yyg[yygc] = new YYARec(-40,148);yygc++; 
					yyg[yygc] = new YYARec(-37,149);yygc++; 
					yyg[yygc] = new YYARec(-44,15);yygc++; 
					yyg[yygc] = new YYARec(-40,16);yygc++; 
					yyg[yygc] = new YYARec(-18,181);yygc++; 
					yyg[yygc] = new YYARec(-13,71);yygc++; 
					yyg[yygc] = new YYARec(-6,72);yygc++; 
					yyg[yygc] = new YYARec(-31,46);yygc++; 
					yyg[yygc] = new YYARec(-30,47);yygc++; 
					yyg[yygc] = new YYARec(-29,48);yygc++; 
					yyg[yygc] = new YYARec(-28,49);yygc++; 
					yyg[yygc] = new YYARec(-27,50);yygc++; 
					yyg[yygc] = new YYARec(-26,51);yygc++; 
					yyg[yygc] = new YYARec(-25,52);yygc++; 
					yyg[yygc] = new YYARec(-24,53);yygc++; 
					yyg[yygc] = new YYARec(-23,54);yygc++; 
					yyg[yygc] = new YYARec(-22,55);yygc++; 
					yyg[yygc] = new YYARec(-21,56);yygc++; 
					yyg[yygc] = new YYARec(-20,57);yygc++; 
					yyg[yygc] = new YYARec(-19,184);yygc++; 
					yyg[yygc] = new YYARec(-6,59);yygc++;

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
					yyd[14] = -114;  
					yyd[15] = 0;  
					yyd[16] = -115;  
					yyd[17] = -20;  
					yyd[18] = -17;  
					yyd[19] = 0;  
					yyd[20] = -19;  
					yyd[21] = -18;  
					yyd[22] = 0;  
					yyd[23] = -73;  
					yyd[24] = -74;  
					yyd[25] = -120;  
					yyd[26] = -121;  
					yyd[27] = -2;  
					yyd[28] = 0;  
					yyd[29] = 0;  
					yyd[30] = 0;  
					yyd[31] = -117;  
					yyd[32] = -16;  
					yyd[33] = 0;  
					yyd[34] = 0;  
					yyd[35] = -25;  
					yyd[36] = 0;  
					yyd[37] = 0;  
					yyd[38] = 0;  
					yyd[39] = 0;  
					yyd[40] = 0;  
					yyd[41] = -15;  
					yyd[42] = 0;  
					yyd[43] = -22;  
					yyd[44] = 0;  
					yyd[45] = 0;  
					yyd[46] = -42;  
					yyd[47] = -41;  
					yyd[48] = -40;  
					yyd[49] = -38;  
					yyd[50] = -37;  
					yyd[51] = -36;  
					yyd[52] = -35;  
					yyd[53] = -34;  
					yyd[54] = -33;  
					yyd[55] = -32;  
					yyd[56] = 0;  
					yyd[57] = 0;  
					yyd[58] = 0;  
					yyd[59] = 0;  
					yyd[60] = 0;  
					yyd[61] = 0;  
					yyd[62] = -111;  
					yyd[63] = -110;  
					yyd[64] = -5;  
					yyd[65] = 0;  
					yyd[66] = -6;  
					yyd[67] = 0;  
					yyd[68] = 0;  
					yyd[69] = 0;  
					yyd[70] = 0;  
					yyd[71] = -119;  
					yyd[72] = -118;  
					yyd[73] = -21;  
					yyd[74] = -30;  
					yyd[75] = 0;  
					yyd[76] = -27;  
					yyd[77] = 0;  
					yyd[78] = -44;  
					yyd[79] = 0;  
					yyd[80] = 0;  
					yyd[81] = -48;  
					yyd[82] = 0;  
					yyd[83] = 0;  
					yyd[84] = 0;  
					yyd[85] = 0;  
					yyd[86] = 0;  
					yyd[87] = 0;  
					yyd[88] = -8;  
					yyd[89] = -14;  
					yyd[90] = -13;  
					yyd[91] = 0;  
					yyd[92] = -29;  
					yyd[93] = 0;  
					yyd[94] = 0;  
					yyd[95] = 0;  
					yyd[96] = 0;  
					yyd[97] = -75;  
					yyd[98] = -76;  
					yyd[99] = -77;  
					yyd[100] = -78;  
					yyd[101] = -79;  
					yyd[102] = -43;  
					yyd[103] = -47;  
					yyd[104] = -28;  
					yyd[105] = 0;  
					yyd[106] = 0;  
					yyd[107] = -59;  
					yyd[108] = 0;  
					yyd[109] = -65;  
					yyd[110] = -64;  
					yyd[111] = 0;  
					yyd[112] = -80;  
					yyd[113] = -81;  
					yyd[114] = -82;  
					yyd[115] = -83;  
					yyd[116] = -84;  
					yyd[117] = -85;  
					yyd[118] = -86;  
					yyd[119] = -87;  
					yyd[120] = -88;  
					yyd[121] = 0;  
					yyd[122] = -90;  
					yyd[123] = -91;  
					yyd[124] = -92;  
					yyd[125] = -93;  
					yyd[126] = -97;  
					yyd[127] = 0;  
					yyd[128] = 0;  
					yyd[129] = 0;  
					yyd[130] = -45;  
					yyd[131] = 0;  
					yyd[132] = -51;  
					yyd[133] = -52;  
					yyd[134] = -53;  
					yyd[135] = -54;  
					yyd[136] = 0;  
					yyd[137] = 0;  
					yyd[138] = 0;  
					yyd[139] = -102;  
					yyd[140] = -103;  
					yyd[141] = -104;  
					yyd[142] = -105;  
					yyd[143] = -106;  
					yyd[144] = -107;  
					yyd[145] = -108;  
					yyd[146] = -109;  
					yyd[147] = 0;  
					yyd[148] = -72;  
					yyd[149] = 0;  
					yyd[150] = -66;  
					yyd[151] = -67;  
					yyd[152] = -68;  
					yyd[153] = -69;  
					yyd[154] = -70;  
					yyd[155] = -71;  
					yyd[156] = 0;  
					yyd[157] = 0;  
					yyd[158] = -7;  
					yyd[159] = 0;  
					yyd[160] = 0;  
					yyd[161] = -99;  
					yyd[162] = 0;  
					yyd[163] = -100;  
					yyd[164] = 0;  
					yyd[165] = 0;  
					yyd[166] = -60;  
					yyd[167] = -62;  
					yyd[168] = 0;  
					yyd[169] = -58;  
					yyd[170] = 0;  
					yyd[171] = 0;  
					yyd[172] = -56;  
					yyd[173] = 0;  
					yyd[174] = -63;  
					yyd[175] = 0;  
					yyd[176] = 0;  
					yyd[177] = 0;  
					yyd[178] = 0;  
					yyd[179] = -61;  
					yyd[180] = -96;  
					yyd[181] = -23;  
					yyd[182] = 0;  
					yyd[183] = 0;  
					yyd[184] = 0;  
					yyd[185] = -94; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 6;  
					yyal[2] = 6;  
					yyal[3] = 6;  
					yyal[4] = 6;  
					yyal[5] = 6;  
					yyal[6] = 13;  
					yyal[7] = 13;  
					yyal[8] = 13;  
					yyal[9] = 19;  
					yyal[10] = 20;  
					yyal[11] = 22;  
					yyal[12] = 24;  
					yyal[13] = 26;  
					yyal[14] = 26;  
					yyal[15] = 26;  
					yyal[16] = 27;  
					yyal[17] = 27;  
					yyal[18] = 27;  
					yyal[19] = 27;  
					yyal[20] = 28;  
					yyal[21] = 28;  
					yyal[22] = 28;  
					yyal[23] = 34;  
					yyal[24] = 34;  
					yyal[25] = 34;  
					yyal[26] = 34;  
					yyal[27] = 34;  
					yyal[28] = 34;  
					yyal[29] = 35;  
					yyal[30] = 36;  
					yyal[31] = 38;  
					yyal[32] = 38;  
					yyal[33] = 38;  
					yyal[34] = 39;  
					yyal[35] = 41;  
					yyal[36] = 41;  
					yyal[37] = 43;  
					yyal[38] = 48;  
					yyal[39] = 51;  
					yyal[40] = 54;  
					yyal[41] = 57;  
					yyal[42] = 57;  
					yyal[43] = 62;  
					yyal[44] = 62;  
					yyal[45] = 67;  
					yyal[46] = 68;  
					yyal[47] = 68;  
					yyal[48] = 68;  
					yyal[49] = 68;  
					yyal[50] = 68;  
					yyal[51] = 68;  
					yyal[52] = 68;  
					yyal[53] = 68;  
					yyal[54] = 68;  
					yyal[55] = 68;  
					yyal[56] = 68;  
					yyal[57] = 73;  
					yyal[58] = 74;  
					yyal[59] = 75;  
					yyal[60] = 85;  
					yyal[61] = 86;  
					yyal[62] = 87;  
					yyal[63] = 87;  
					yyal[64] = 87;  
					yyal[65] = 87;  
					yyal[66] = 89;  
					yyal[67] = 89;  
					yyal[68] = 90;  
					yyal[69] = 91;  
					yyal[70] = 116;  
					yyal[71] = 117;  
					yyal[72] = 117;  
					yyal[73] = 117;  
					yyal[74] = 117;  
					yyal[75] = 117;  
					yyal[76] = 122;  
					yyal[77] = 122;  
					yyal[78] = 124;  
					yyal[79] = 124;  
					yyal[80] = 126;  
					yyal[81] = 128;  
					yyal[82] = 128;  
					yyal[83] = 135;  
					yyal[84] = 140;  
					yyal[85] = 145;  
					yyal[86] = 165;  
					yyal[87] = 185;  
					yyal[88] = 188;  
					yyal[89] = 188;  
					yyal[90] = 188;  
					yyal[91] = 188;  
					yyal[92] = 193;  
					yyal[93] = 193;  
					yyal[94] = 198;  
					yyal[95] = 204;  
					yyal[96] = 210;  
					yyal[97] = 229;  
					yyal[98] = 229;  
					yyal[99] = 229;  
					yyal[100] = 229;  
					yyal[101] = 229;  
					yyal[102] = 229;  
					yyal[103] = 229;  
					yyal[104] = 229;  
					yyal[105] = 229;  
					yyal[106] = 238;  
					yyal[107] = 239;  
					yyal[108] = 239;  
					yyal[109] = 256;  
					yyal[110] = 256;  
					yyal[111] = 256;  
					yyal[112] = 275;  
					yyal[113] = 275;  
					yyal[114] = 275;  
					yyal[115] = 275;  
					yyal[116] = 275;  
					yyal[117] = 275;  
					yyal[118] = 275;  
					yyal[119] = 275;  
					yyal[120] = 275;  
					yyal[121] = 275;  
					yyal[122] = 295;  
					yyal[123] = 295;  
					yyal[124] = 295;  
					yyal[125] = 295;  
					yyal[126] = 295;  
					yyal[127] = 295;  
					yyal[128] = 304;  
					yyal[129] = 305;  
					yyal[130] = 307;  
					yyal[131] = 307;  
					yyal[132] = 309;  
					yyal[133] = 309;  
					yyal[134] = 309;  
					yyal[135] = 309;  
					yyal[136] = 309;  
					yyal[137] = 318;  
					yyal[138] = 338;  
					yyal[139] = 339;  
					yyal[140] = 339;  
					yyal[141] = 339;  
					yyal[142] = 339;  
					yyal[143] = 339;  
					yyal[144] = 339;  
					yyal[145] = 339;  
					yyal[146] = 339;  
					yyal[147] = 339;  
					yyal[148] = 358;  
					yyal[149] = 358;  
					yyal[150] = 377;  
					yyal[151] = 377;  
					yyal[152] = 377;  
					yyal[153] = 377;  
					yyal[154] = 377;  
					yyal[155] = 377;  
					yyal[156] = 377;  
					yyal[157] = 386;  
					yyal[158] = 387;  
					yyal[159] = 387;  
					yyal[160] = 392;  
					yyal[161] = 397;  
					yyal[162] = 397;  
					yyal[163] = 414;  
					yyal[164] = 414;  
					yyal[165] = 419;  
					yyal[166] = 428;  
					yyal[167] = 428;  
					yyal[168] = 428;  
					yyal[169] = 447;  
					yyal[170] = 447;  
					yyal[171] = 452;  
					yyal[172] = 453;  
					yyal[173] = 453;  
					yyal[174] = 454;  
					yyal[175] = 454;  
					yyal[176] = 463;  
					yyal[177] = 464;  
					yyal[178] = 469;  
					yyal[179] = 475;  
					yyal[180] = 475;  
					yyal[181] = 475;  
					yyal[182] = 475;  
					yyal[183] = 476;  
					yyal[184] = 481;  
					yyal[185] = 482; 

					yyah = new int[yynstates];
					yyah[0] = 5;  
					yyah[1] = 5;  
					yyah[2] = 5;  
					yyah[3] = 5;  
					yyah[4] = 5;  
					yyah[5] = 12;  
					yyah[6] = 12;  
					yyah[7] = 12;  
					yyah[8] = 18;  
					yyah[9] = 19;  
					yyah[10] = 21;  
					yyah[11] = 23;  
					yyah[12] = 25;  
					yyah[13] = 25;  
					yyah[14] = 25;  
					yyah[15] = 26;  
					yyah[16] = 26;  
					yyah[17] = 26;  
					yyah[18] = 26;  
					yyah[19] = 27;  
					yyah[20] = 27;  
					yyah[21] = 27;  
					yyah[22] = 33;  
					yyah[23] = 33;  
					yyah[24] = 33;  
					yyah[25] = 33;  
					yyah[26] = 33;  
					yyah[27] = 33;  
					yyah[28] = 34;  
					yyah[29] = 35;  
					yyah[30] = 37;  
					yyah[31] = 37;  
					yyah[32] = 37;  
					yyah[33] = 38;  
					yyah[34] = 40;  
					yyah[35] = 40;  
					yyah[36] = 42;  
					yyah[37] = 47;  
					yyah[38] = 50;  
					yyah[39] = 53;  
					yyah[40] = 56;  
					yyah[41] = 56;  
					yyah[42] = 61;  
					yyah[43] = 61;  
					yyah[44] = 66;  
					yyah[45] = 67;  
					yyah[46] = 67;  
					yyah[47] = 67;  
					yyah[48] = 67;  
					yyah[49] = 67;  
					yyah[50] = 67;  
					yyah[51] = 67;  
					yyah[52] = 67;  
					yyah[53] = 67;  
					yyah[54] = 67;  
					yyah[55] = 67;  
					yyah[56] = 72;  
					yyah[57] = 73;  
					yyah[58] = 74;  
					yyah[59] = 84;  
					yyah[60] = 85;  
					yyah[61] = 86;  
					yyah[62] = 86;  
					yyah[63] = 86;  
					yyah[64] = 86;  
					yyah[65] = 88;  
					yyah[66] = 88;  
					yyah[67] = 89;  
					yyah[68] = 90;  
					yyah[69] = 115;  
					yyah[70] = 116;  
					yyah[71] = 116;  
					yyah[72] = 116;  
					yyah[73] = 116;  
					yyah[74] = 116;  
					yyah[75] = 121;  
					yyah[76] = 121;  
					yyah[77] = 123;  
					yyah[78] = 123;  
					yyah[79] = 125;  
					yyah[80] = 127;  
					yyah[81] = 127;  
					yyah[82] = 134;  
					yyah[83] = 139;  
					yyah[84] = 144;  
					yyah[85] = 164;  
					yyah[86] = 184;  
					yyah[87] = 187;  
					yyah[88] = 187;  
					yyah[89] = 187;  
					yyah[90] = 187;  
					yyah[91] = 192;  
					yyah[92] = 192;  
					yyah[93] = 197;  
					yyah[94] = 203;  
					yyah[95] = 209;  
					yyah[96] = 228;  
					yyah[97] = 228;  
					yyah[98] = 228;  
					yyah[99] = 228;  
					yyah[100] = 228;  
					yyah[101] = 228;  
					yyah[102] = 228;  
					yyah[103] = 228;  
					yyah[104] = 228;  
					yyah[105] = 237;  
					yyah[106] = 238;  
					yyah[107] = 238;  
					yyah[108] = 255;  
					yyah[109] = 255;  
					yyah[110] = 255;  
					yyah[111] = 274;  
					yyah[112] = 274;  
					yyah[113] = 274;  
					yyah[114] = 274;  
					yyah[115] = 274;  
					yyah[116] = 274;  
					yyah[117] = 274;  
					yyah[118] = 274;  
					yyah[119] = 274;  
					yyah[120] = 274;  
					yyah[121] = 294;  
					yyah[122] = 294;  
					yyah[123] = 294;  
					yyah[124] = 294;  
					yyah[125] = 294;  
					yyah[126] = 294;  
					yyah[127] = 303;  
					yyah[128] = 304;  
					yyah[129] = 306;  
					yyah[130] = 306;  
					yyah[131] = 308;  
					yyah[132] = 308;  
					yyah[133] = 308;  
					yyah[134] = 308;  
					yyah[135] = 308;  
					yyah[136] = 317;  
					yyah[137] = 337;  
					yyah[138] = 338;  
					yyah[139] = 338;  
					yyah[140] = 338;  
					yyah[141] = 338;  
					yyah[142] = 338;  
					yyah[143] = 338;  
					yyah[144] = 338;  
					yyah[145] = 338;  
					yyah[146] = 338;  
					yyah[147] = 357;  
					yyah[148] = 357;  
					yyah[149] = 376;  
					yyah[150] = 376;  
					yyah[151] = 376;  
					yyah[152] = 376;  
					yyah[153] = 376;  
					yyah[154] = 376;  
					yyah[155] = 376;  
					yyah[156] = 385;  
					yyah[157] = 386;  
					yyah[158] = 386;  
					yyah[159] = 391;  
					yyah[160] = 396;  
					yyah[161] = 396;  
					yyah[162] = 413;  
					yyah[163] = 413;  
					yyah[164] = 418;  
					yyah[165] = 427;  
					yyah[166] = 427;  
					yyah[167] = 427;  
					yyah[168] = 446;  
					yyah[169] = 446;  
					yyah[170] = 451;  
					yyah[171] = 452;  
					yyah[172] = 452;  
					yyah[173] = 453;  
					yyah[174] = 453;  
					yyah[175] = 462;  
					yyah[176] = 463;  
					yyah[177] = 468;  
					yyah[178] = 474;  
					yyah[179] = 474;  
					yyah[180] = 474;  
					yyah[181] = 474;  
					yyah[182] = 475;  
					yyah[183] = 480;  
					yyah[184] = 481;  
					yyah[185] = 481; 

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
					yygl[22] = 30;  
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
					yygl[36] = 33;  
					yygl[37] = 36;  
					yygl[38] = 50;  
					yygl[39] = 51;  
					yygl[40] = 58;  
					yygl[41] = 65;  
					yygl[42] = 65;  
					yygl[43] = 70;  
					yygl[44] = 70;  
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
					yygl[57] = 89;  
					yygl[58] = 89;  
					yygl[59] = 89;  
					yygl[60] = 98;  
					yygl[61] = 98;  
					yygl[62] = 98;  
					yygl[63] = 98;  
					yygl[64] = 98;  
					yygl[65] = 98;  
					yygl[66] = 98;  
					yygl[67] = 98;  
					yygl[68] = 98;  
					yygl[69] = 98;  
					yygl[70] = 98;  
					yygl[71] = 98;  
					yygl[72] = 98;  
					yygl[73] = 98;  
					yygl[74] = 98;  
					yygl[75] = 98;  
					yygl[76] = 112;  
					yygl[77] = 112;  
					yygl[78] = 112;  
					yygl[79] = 112;  
					yygl[80] = 112;  
					yygl[81] = 112;  
					yygl[82] = 112;  
					yygl[83] = 113;  
					yygl[84] = 120;  
					yygl[85] = 134;  
					yygl[86] = 143;  
					yygl[87] = 152;  
					yygl[88] = 158;  
					yygl[89] = 158;  
					yygl[90] = 158;  
					yygl[91] = 158;  
					yygl[92] = 163;  
					yygl[93] = 163;  
					yygl[94] = 170;  
					yygl[95] = 176;  
					yygl[96] = 182;  
					yygl[97] = 190;  
					yygl[98] = 190;  
					yygl[99] = 190;  
					yygl[100] = 190;  
					yygl[101] = 190;  
					yygl[102] = 190;  
					yygl[103] = 190;  
					yygl[104] = 190;  
					yygl[105] = 190;  
					yygl[106] = 191;  
					yygl[107] = 191;  
					yygl[108] = 191;  
					yygl[109] = 193;  
					yygl[110] = 193;  
					yygl[111] = 193;  
					yygl[112] = 201;  
					yygl[113] = 201;  
					yygl[114] = 201;  
					yygl[115] = 201;  
					yygl[116] = 201;  
					yygl[117] = 201;  
					yygl[118] = 201;  
					yygl[119] = 201;  
					yygl[120] = 201;  
					yygl[121] = 201;  
					yygl[122] = 201;  
					yygl[123] = 201;  
					yygl[124] = 201;  
					yygl[125] = 201;  
					yygl[126] = 201;  
					yygl[127] = 201;  
					yygl[128] = 202;  
					yygl[129] = 202;  
					yygl[130] = 202;  
					yygl[131] = 202;  
					yygl[132] = 202;  
					yygl[133] = 202;  
					yygl[134] = 202;  
					yygl[135] = 202;  
					yygl[136] = 202;  
					yygl[137] = 204;  
					yygl[138] = 213;  
					yygl[139] = 213;  
					yygl[140] = 213;  
					yygl[141] = 213;  
					yygl[142] = 213;  
					yygl[143] = 213;  
					yygl[144] = 213;  
					yygl[145] = 213;  
					yygl[146] = 213;  
					yygl[147] = 213;  
					yygl[148] = 221;  
					yygl[149] = 221;  
					yygl[150] = 229;  
					yygl[151] = 229;  
					yygl[152] = 229;  
					yygl[153] = 229;  
					yygl[154] = 229;  
					yygl[155] = 229;  
					yygl[156] = 229;  
					yygl[157] = 231;  
					yygl[158] = 231;  
					yygl[159] = 231;  
					yygl[160] = 236;  
					yygl[161] = 241;  
					yygl[162] = 241;  
					yygl[163] = 243;  
					yygl[164] = 243;  
					yygl[165] = 257;  
					yygl[166] = 259;  
					yygl[167] = 259;  
					yygl[168] = 259;  
					yygl[169] = 267;  
					yygl[170] = 267;  
					yygl[171] = 281;  
					yygl[172] = 281;  
					yygl[173] = 281;  
					yygl[174] = 281;  
					yygl[175] = 281;  
					yygl[176] = 283;  
					yygl[177] = 283;  
					yygl[178] = 288;  
					yygl[179] = 288;  
					yygl[180] = 288;  
					yygl[181] = 288;  
					yygl[182] = 288;  
					yygl[183] = 288;  
					yygl[184] = 302;  
					yygl[185] = 302; 

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
					yygh[21] = 29;  
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
					yygh[35] = 32;  
					yygh[36] = 35;  
					yygh[37] = 49;  
					yygh[38] = 50;  
					yygh[39] = 57;  
					yygh[40] = 64;  
					yygh[41] = 64;  
					yygh[42] = 69;  
					yygh[43] = 69;  
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
					yygh[56] = 88;  
					yygh[57] = 88;  
					yygh[58] = 88;  
					yygh[59] = 97;  
					yygh[60] = 97;  
					yygh[61] = 97;  
					yygh[62] = 97;  
					yygh[63] = 97;  
					yygh[64] = 97;  
					yygh[65] = 97;  
					yygh[66] = 97;  
					yygh[67] = 97;  
					yygh[68] = 97;  
					yygh[69] = 97;  
					yygh[70] = 97;  
					yygh[71] = 97;  
					yygh[72] = 97;  
					yygh[73] = 97;  
					yygh[74] = 97;  
					yygh[75] = 111;  
					yygh[76] = 111;  
					yygh[77] = 111;  
					yygh[78] = 111;  
					yygh[79] = 111;  
					yygh[80] = 111;  
					yygh[81] = 111;  
					yygh[82] = 112;  
					yygh[83] = 119;  
					yygh[84] = 133;  
					yygh[85] = 142;  
					yygh[86] = 151;  
					yygh[87] = 157;  
					yygh[88] = 157;  
					yygh[89] = 157;  
					yygh[90] = 157;  
					yygh[91] = 162;  
					yygh[92] = 162;  
					yygh[93] = 169;  
					yygh[94] = 175;  
					yygh[95] = 181;  
					yygh[96] = 189;  
					yygh[97] = 189;  
					yygh[98] = 189;  
					yygh[99] = 189;  
					yygh[100] = 189;  
					yygh[101] = 189;  
					yygh[102] = 189;  
					yygh[103] = 189;  
					yygh[104] = 189;  
					yygh[105] = 190;  
					yygh[106] = 190;  
					yygh[107] = 190;  
					yygh[108] = 192;  
					yygh[109] = 192;  
					yygh[110] = 192;  
					yygh[111] = 200;  
					yygh[112] = 200;  
					yygh[113] = 200;  
					yygh[114] = 200;  
					yygh[115] = 200;  
					yygh[116] = 200;  
					yygh[117] = 200;  
					yygh[118] = 200;  
					yygh[119] = 200;  
					yygh[120] = 200;  
					yygh[121] = 200;  
					yygh[122] = 200;  
					yygh[123] = 200;  
					yygh[124] = 200;  
					yygh[125] = 200;  
					yygh[126] = 200;  
					yygh[127] = 201;  
					yygh[128] = 201;  
					yygh[129] = 201;  
					yygh[130] = 201;  
					yygh[131] = 201;  
					yygh[132] = 201;  
					yygh[133] = 201;  
					yygh[134] = 201;  
					yygh[135] = 201;  
					yygh[136] = 203;  
					yygh[137] = 212;  
					yygh[138] = 212;  
					yygh[139] = 212;  
					yygh[140] = 212;  
					yygh[141] = 212;  
					yygh[142] = 212;  
					yygh[143] = 212;  
					yygh[144] = 212;  
					yygh[145] = 212;  
					yygh[146] = 212;  
					yygh[147] = 220;  
					yygh[148] = 220;  
					yygh[149] = 228;  
					yygh[150] = 228;  
					yygh[151] = 228;  
					yygh[152] = 228;  
					yygh[153] = 228;  
					yygh[154] = 228;  
					yygh[155] = 228;  
					yygh[156] = 230;  
					yygh[157] = 230;  
					yygh[158] = 230;  
					yygh[159] = 235;  
					yygh[160] = 240;  
					yygh[161] = 240;  
					yygh[162] = 242;  
					yygh[163] = 242;  
					yygh[164] = 256;  
					yygh[165] = 258;  
					yygh[166] = 258;  
					yygh[167] = 258;  
					yygh[168] = 266;  
					yygh[169] = 266;  
					yygh[170] = 280;  
					yygh[171] = 280;  
					yygh[172] = 280;  
					yygh[173] = 280;  
					yygh[174] = 280;  
					yygh[175] = 282;  
					yygh[176] = 282;  
					yygh[177] = 287;  
					yygh[178] = 287;  
					yygh[179] = 287;  
					yygh[180] = 287;  
					yygh[181] = 287;  
					yygh[182] = 287;  
					yygh[183] = 301;  
					yygh[184] = 301;  
					yygh[185] = 301; 

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
					yyr[yyrc] = new YYRRec(11,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-21);yyrc++; 
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

			if (Regex.IsMatch(Rest,"^([A-Za-z][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)")){
				Results.Add (t_identifier);
				ResultsV.Add(Regex.Match(Rest,"^([A-Za-z][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)").Value);}

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
