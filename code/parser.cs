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
                int t_UNDEF = 264;
                int t_INCLUDE = 265;
                int t_Char123 = 266;
                int t_Char125 = 267;
                int t_Char58 = 268;
                int t_Char124Char124 = 269;
                int t_Char38Char38 = 270;
                int t_Char124 = 271;
                int t_Char94 = 272;
                int t_Char38 = 273;
                int t_Char40 = 274;
                int t_Char41 = 275;
                int t_Char33Char61 = 276;
                int t_Char61Char61 = 277;
                int t_Char60 = 278;
                int t_Char60Char61 = 279;
                int t_Char62 = 280;
                int t_Char62Char61 = 281;
                int t_Char43 = 282;
                int t_Char45 = 283;
                int t_Char37 = 284;
                int t_Char42 = 285;
                int t_Char47 = 286;
                int t_Char33 = 287;
                int t_Char42Char61 = 288;
                int t_Char43Char61 = 289;
                int t_Char45Char61 = 290;
                int t_Char47Char61 = 291;
                int t_Char61 = 292;
                int t_ABS = 293;
                int t_ACOS = 294;
                int t_ASIN = 295;
                int t_COS = 296;
                int t_EXP = 297;
                int t_INT = 298;
                int t_LOG = 299;
                int t_LOG10 = 300;
                int t_LOG2 = 301;
                int t_RANDOM = 302;
                int t_SIGN = 303;
                int t_SIN = 304;
                int t_SQRT = 305;
                int t_TAN = 306;
                int t_IF = 307;
                int t_ELSE = 308;
                int t_WHILE = 309;
                int t_Char46 = 310;
                int t_NULL = 311;
                int t_number = 312;
                int t_identifier = 313;
                int t_file = 314;
                int t_string = 315;
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
         yyval = yyv[yysp-0];
         
       break;
							case   17 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   18 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   19 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   20 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   21 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   22 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   23 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   28 : 
         yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   29 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   30 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   31 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   32 : 
         yyval = "";
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   37 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   38 : 
         yyval = yyv[yysp-0];
         
       break;
							case   39 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = "";
         
       break;
							case   46 : 
         yyval = yyv[yysp-0];
         
       break;
							case   47 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   48 : 
         yyval = yyv[yysp-0];
         
       break;
							case   49 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   50 : 
         yyval = yyv[yysp-0];
         
       break;
							case   51 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   52 : 
         yyval = yyv[yysp-0];
         
       break;
							case   53 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   62 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   63 : 
         yyval = yyv[yysp-0];
         
       break;
							case   64 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   65 : 
         yyval = yyv[yysp-0];
         
       break;
							case   66 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   67 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   68 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
                                default : return;
                        }
                }

                public void InitTables()
                {
                        ////////////////////////////////////////////////////////////////
                        /// Init Table code:
                        ////////////////////////////////////////////////////////////////

					int yynacts   = 1122;
					int yyngotos  = 382;
					int yynstates = 177;
					int yynrules  = 116;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,13);yyac++; 
					yya[yyac] = new YYARec(259,14);yyac++; 
					yya[yyac] = new YYARec(262,15);yyac++; 
					yya[yyac] = new YYARec(264,16);yyac++; 
					yya[yyac] = new YYARec(265,17);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(257,13);yyac++; 
					yya[yyac] = new YYARec(259,14);yyac++; 
					yya[yyac] = new YYARec(262,15);yyac++; 
					yya[yyac] = new YYARec(264,16);yyac++; 
					yya[yyac] = new YYARec(265,17);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(0,-2 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(312,53);yyac++; 
					yya[yyac] = new YYARec(258,54);yyac++; 
					yya[yyac] = new YYARec(258,55);yyac++; 
					yya[yyac] = new YYARec(263,64);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(266,-45 );yyac++; 
					yya[yyac] = new YYARec(258,-109 );yyac++; 
					yya[yyac] = new YYARec(258,66);yyac++; 
					yya[yyac] = new YYARec(258,67);yyac++; 
					yya[yyac] = new YYARec(263,68);yyac++; 
					yya[yyac] = new YYARec(0,-20 );yyac++; 
					yya[yyac] = new YYARec(257,-20 );yyac++; 
					yya[yyac] = new YYARec(259,-20 );yyac++; 
					yya[yyac] = new YYARec(260,-20 );yyac++; 
					yya[yyac] = new YYARec(261,-20 );yyac++; 
					yya[yyac] = new YYARec(262,-20 );yyac++; 
					yya[yyac] = new YYARec(264,-20 );yyac++; 
					yya[yyac] = new YYARec(265,-20 );yyac++; 
					yya[yyac] = new YYARec(293,-20 );yyac++; 
					yya[yyac] = new YYARec(294,-20 );yyac++; 
					yya[yyac] = new YYARec(295,-20 );yyac++; 
					yya[yyac] = new YYARec(296,-20 );yyac++; 
					yya[yyac] = new YYARec(297,-20 );yyac++; 
					yya[yyac] = new YYARec(298,-20 );yyac++; 
					yya[yyac] = new YYARec(299,-20 );yyac++; 
					yya[yyac] = new YYARec(300,-20 );yyac++; 
					yya[yyac] = new YYARec(301,-20 );yyac++; 
					yya[yyac] = new YYARec(302,-20 );yyac++; 
					yya[yyac] = new YYARec(303,-20 );yyac++; 
					yya[yyac] = new YYARec(304,-20 );yyac++; 
					yya[yyac] = new YYARec(305,-20 );yyac++; 
					yya[yyac] = new YYARec(306,-20 );yyac++; 
					yya[yyac] = new YYARec(313,-20 );yyac++; 
					yya[yyac] = new YYARec(258,69);yyac++; 
					yya[yyac] = new YYARec(258,70);yyac++; 
					yya[yyac] = new YYARec(263,72);yyac++; 
					yya[yyac] = new YYARec(258,-38 );yyac++; 
					yya[yyac] = new YYARec(283,-45 );yyac++; 
					yya[yyac] = new YYARec(287,-45 );yyac++; 
					yya[yyac] = new YYARec(293,-45 );yyac++; 
					yya[yyac] = new YYARec(294,-45 );yyac++; 
					yya[yyac] = new YYARec(295,-45 );yyac++; 
					yya[yyac] = new YYARec(296,-45 );yyac++; 
					yya[yyac] = new YYARec(297,-45 );yyac++; 
					yya[yyac] = new YYARec(298,-45 );yyac++; 
					yya[yyac] = new YYARec(299,-45 );yyac++; 
					yya[yyac] = new YYARec(300,-45 );yyac++; 
					yya[yyac] = new YYARec(301,-45 );yyac++; 
					yya[yyac] = new YYARec(302,-45 );yyac++; 
					yya[yyac] = new YYARec(303,-45 );yyac++; 
					yya[yyac] = new YYARec(304,-45 );yyac++; 
					yya[yyac] = new YYARec(305,-45 );yyac++; 
					yya[yyac] = new YYARec(306,-45 );yyac++; 
					yya[yyac] = new YYARec(311,-45 );yyac++; 
					yya[yyac] = new YYARec(312,-45 );yyac++; 
					yya[yyac] = new YYARec(313,-45 );yyac++; 
					yya[yyac] = new YYARec(314,-45 );yyac++; 
					yya[yyac] = new YYARec(315,-45 );yyac++; 
					yya[yyac] = new YYARec(266,73);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(258,-109 );yyac++; 
					yya[yyac] = new YYARec(263,-109 );yyac++; 
					yya[yyac] = new YYARec(266,-109 );yyac++; 
					yya[yyac] = new YYARec(269,-109 );yyac++; 
					yya[yyac] = new YYARec(270,-109 );yyac++; 
					yya[yyac] = new YYARec(271,-109 );yyac++; 
					yya[yyac] = new YYARec(272,-109 );yyac++; 
					yya[yyac] = new YYARec(273,-109 );yyac++; 
					yya[yyac] = new YYARec(275,-109 );yyac++; 
					yya[yyac] = new YYARec(276,-109 );yyac++; 
					yya[yyac] = new YYARec(277,-109 );yyac++; 
					yya[yyac] = new YYARec(278,-109 );yyac++; 
					yya[yyac] = new YYARec(279,-109 );yyac++; 
					yya[yyac] = new YYARec(280,-109 );yyac++; 
					yya[yyac] = new YYARec(281,-109 );yyac++; 
					yya[yyac] = new YYARec(282,-109 );yyac++; 
					yya[yyac] = new YYARec(283,-109 );yyac++; 
					yya[yyac] = new YYARec(284,-109 );yyac++; 
					yya[yyac] = new YYARec(285,-109 );yyac++; 
					yya[yyac] = new YYARec(286,-109 );yyac++; 
					yya[yyac] = new YYARec(287,-109 );yyac++; 
					yya[yyac] = new YYARec(288,-109 );yyac++; 
					yya[yyac] = new YYARec(289,-109 );yyac++; 
					yya[yyac] = new YYARec(290,-109 );yyac++; 
					yya[yyac] = new YYARec(291,-109 );yyac++; 
					yya[yyac] = new YYARec(292,-109 );yyac++; 
					yya[yyac] = new YYARec(293,-109 );yyac++; 
					yya[yyac] = new YYARec(294,-109 );yyac++; 
					yya[yyac] = new YYARec(295,-109 );yyac++; 
					yya[yyac] = new YYARec(296,-109 );yyac++; 
					yya[yyac] = new YYARec(297,-109 );yyac++; 
					yya[yyac] = new YYARec(298,-109 );yyac++; 
					yya[yyac] = new YYARec(299,-109 );yyac++; 
					yya[yyac] = new YYARec(300,-109 );yyac++; 
					yya[yyac] = new YYARec(301,-109 );yyac++; 
					yya[yyac] = new YYARec(302,-109 );yyac++; 
					yya[yyac] = new YYARec(303,-109 );yyac++; 
					yya[yyac] = new YYARec(304,-109 );yyac++; 
					yya[yyac] = new YYARec(305,-109 );yyac++; 
					yya[yyac] = new YYARec(306,-109 );yyac++; 
					yya[yyac] = new YYARec(311,-109 );yyac++; 
					yya[yyac] = new YYARec(312,-109 );yyac++; 
					yya[yyac] = new YYARec(313,-109 );yyac++; 
					yya[yyac] = new YYARec(314,-109 );yyac++; 
					yya[yyac] = new YYARec(315,-109 );yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(266,-44 );yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(262,15);yyac++; 
					yya[yyac] = new YYARec(264,16);yyac++; 
					yya[yyac] = new YYARec(265,17);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(260,-11 );yyac++; 
					yya[yyac] = new YYARec(261,-11 );yyac++; 
					yya[yyac] = new YYARec(262,15);yyac++; 
					yya[yyac] = new YYARec(264,16);yyac++; 
					yya[yyac] = new YYARec(265,17);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(260,-11 );yyac++; 
					yya[yyac] = new YYARec(261,-11 );yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(307,89);yyac++; 
					yya[yyac] = new YYARec(308,90);yyac++; 
					yya[yyac] = new YYARec(309,91);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(267,-32 );yyac++; 
					yya[yyac] = new YYARec(260,92);yyac++; 
					yya[yyac] = new YYARec(261,93);yyac++; 
					yya[yyac] = new YYARec(262,15);yyac++; 
					yya[yyac] = new YYARec(264,16);yyac++; 
					yya[yyac] = new YYARec(265,17);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(260,-11 );yyac++; 
					yya[yyac] = new YYARec(261,-11 );yyac++; 
					yya[yyac] = new YYARec(258,95);yyac++; 
					yya[yyac] = new YYARec(258,96);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(307,89);yyac++; 
					yya[yyac] = new YYARec(308,90);yyac++; 
					yya[yyac] = new YYARec(309,91);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(267,-32 );yyac++; 
					yya[yyac] = new YYARec(258,98);yyac++; 
					yya[yyac] = new YYARec(267,99);yyac++; 
					yya[yyac] = new YYARec(263,101);yyac++; 
					yya[yyac] = new YYARec(268,102);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(258,-35 );yyac++; 
					yya[yyac] = new YYARec(274,119);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,120);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(266,121);yyac++; 
					yya[yyac] = new YYARec(274,119);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,120);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(262,15);yyac++; 
					yya[yyac] = new YYARec(264,16);yyac++; 
					yya[yyac] = new YYARec(265,17);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(261,-11 );yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(307,89);yyac++; 
					yya[yyac] = new YYARec(308,90);yyac++; 
					yya[yyac] = new YYARec(309,91);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(267,-32 );yyac++; 
					yya[yyac] = new YYARec(288,126);yyac++; 
					yya[yyac] = new YYARec(289,127);yyac++; 
					yya[yyac] = new YYARec(290,128);yyac++; 
					yya[yyac] = new YYARec(291,129);yyac++; 
					yya[yyac] = new YYARec(292,130);yyac++; 
					yya[yyac] = new YYARec(258,-43 );yyac++; 
					yya[yyac] = new YYARec(263,-43 );yyac++; 
					yya[yyac] = new YYARec(283,-43 );yyac++; 
					yya[yyac] = new YYARec(287,-43 );yyac++; 
					yya[yyac] = new YYARec(293,-43 );yyac++; 
					yya[yyac] = new YYARec(294,-43 );yyac++; 
					yya[yyac] = new YYARec(295,-43 );yyac++; 
					yya[yyac] = new YYARec(296,-43 );yyac++; 
					yya[yyac] = new YYARec(297,-43 );yyac++; 
					yya[yyac] = new YYARec(298,-43 );yyac++; 
					yya[yyac] = new YYARec(299,-43 );yyac++; 
					yya[yyac] = new YYARec(300,-43 );yyac++; 
					yya[yyac] = new YYARec(301,-43 );yyac++; 
					yya[yyac] = new YYARec(302,-43 );yyac++; 
					yya[yyac] = new YYARec(303,-43 );yyac++; 
					yya[yyac] = new YYARec(304,-43 );yyac++; 
					yya[yyac] = new YYARec(305,-43 );yyac++; 
					yya[yyac] = new YYARec(306,-43 );yyac++; 
					yya[yyac] = new YYARec(311,-43 );yyac++; 
					yya[yyac] = new YYARec(312,-43 );yyac++; 
					yya[yyac] = new YYARec(313,-43 );yyac++; 
					yya[yyac] = new YYARec(314,-43 );yyac++; 
					yya[yyac] = new YYARec(315,-43 );yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(307,89);yyac++; 
					yya[yyac] = new YYARec(308,90);yyac++; 
					yya[yyac] = new YYARec(309,91);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(267,-32 );yyac++; 
					yya[yyac] = new YYARec(274,132);yyac++; 
					yya[yyac] = new YYARec(258,-114 );yyac++; 
					yya[yyac] = new YYARec(266,-114 );yyac++; 
					yya[yyac] = new YYARec(269,-114 );yyac++; 
					yya[yyac] = new YYARec(270,-114 );yyac++; 
					yya[yyac] = new YYARec(271,-114 );yyac++; 
					yya[yyac] = new YYARec(272,-114 );yyac++; 
					yya[yyac] = new YYARec(273,-114 );yyac++; 
					yya[yyac] = new YYARec(275,-114 );yyac++; 
					yya[yyac] = new YYARec(276,-114 );yyac++; 
					yya[yyac] = new YYARec(277,-114 );yyac++; 
					yya[yyac] = new YYARec(278,-114 );yyac++; 
					yya[yyac] = new YYARec(279,-114 );yyac++; 
					yya[yyac] = new YYARec(280,-114 );yyac++; 
					yya[yyac] = new YYARec(281,-114 );yyac++; 
					yya[yyac] = new YYARec(282,-114 );yyac++; 
					yya[yyac] = new YYARec(283,-114 );yyac++; 
					yya[yyac] = new YYARec(284,-114 );yyac++; 
					yya[yyac] = new YYARec(285,-114 );yyac++; 
					yya[yyac] = new YYARec(286,-114 );yyac++; 
					yya[yyac] = new YYARec(310,-114 );yyac++; 
					yya[yyac] = new YYARec(274,119);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,120);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(284,135);yyac++; 
					yya[yyac] = new YYARec(285,136);yyac++; 
					yya[yyac] = new YYARec(286,137);yyac++; 
					yya[yyac] = new YYARec(258,-61 );yyac++; 
					yya[yyac] = new YYARec(266,-61 );yyac++; 
					yya[yyac] = new YYARec(269,-61 );yyac++; 
					yya[yyac] = new YYARec(270,-61 );yyac++; 
					yya[yyac] = new YYARec(271,-61 );yyac++; 
					yya[yyac] = new YYARec(272,-61 );yyac++; 
					yya[yyac] = new YYARec(273,-61 );yyac++; 
					yya[yyac] = new YYARec(275,-61 );yyac++; 
					yya[yyac] = new YYARec(276,-61 );yyac++; 
					yya[yyac] = new YYARec(277,-61 );yyac++; 
					yya[yyac] = new YYARec(278,-61 );yyac++; 
					yya[yyac] = new YYARec(279,-61 );yyac++; 
					yya[yyac] = new YYARec(280,-61 );yyac++; 
					yya[yyac] = new YYARec(281,-61 );yyac++; 
					yya[yyac] = new YYARec(282,-61 );yyac++; 
					yya[yyac] = new YYARec(283,-61 );yyac++; 
					yya[yyac] = new YYARec(282,139);yyac++; 
					yya[yyac] = new YYARec(283,140);yyac++; 
					yya[yyac] = new YYARec(258,-59 );yyac++; 
					yya[yyac] = new YYARec(266,-59 );yyac++; 
					yya[yyac] = new YYARec(269,-59 );yyac++; 
					yya[yyac] = new YYARec(270,-59 );yyac++; 
					yya[yyac] = new YYARec(271,-59 );yyac++; 
					yya[yyac] = new YYARec(272,-59 );yyac++; 
					yya[yyac] = new YYARec(273,-59 );yyac++; 
					yya[yyac] = new YYARec(275,-59 );yyac++; 
					yya[yyac] = new YYARec(276,-59 );yyac++; 
					yya[yyac] = new YYARec(277,-59 );yyac++; 
					yya[yyac] = new YYARec(278,-59 );yyac++; 
					yya[yyac] = new YYARec(279,-59 );yyac++; 
					yya[yyac] = new YYARec(280,-59 );yyac++; 
					yya[yyac] = new YYARec(281,-59 );yyac++; 
					yya[yyac] = new YYARec(278,142);yyac++; 
					yya[yyac] = new YYARec(279,143);yyac++; 
					yya[yyac] = new YYARec(280,144);yyac++; 
					yya[yyac] = new YYARec(281,145);yyac++; 
					yya[yyac] = new YYARec(258,-57 );yyac++; 
					yya[yyac] = new YYARec(266,-57 );yyac++; 
					yya[yyac] = new YYARec(269,-57 );yyac++; 
					yya[yyac] = new YYARec(270,-57 );yyac++; 
					yya[yyac] = new YYARec(271,-57 );yyac++; 
					yya[yyac] = new YYARec(272,-57 );yyac++; 
					yya[yyac] = new YYARec(273,-57 );yyac++; 
					yya[yyac] = new YYARec(275,-57 );yyac++; 
					yya[yyac] = new YYARec(276,-57 );yyac++; 
					yya[yyac] = new YYARec(277,-57 );yyac++; 
					yya[yyac] = new YYARec(276,147);yyac++; 
					yya[yyac] = new YYARec(277,148);yyac++; 
					yya[yyac] = new YYARec(258,-56 );yyac++; 
					yya[yyac] = new YYARec(266,-56 );yyac++; 
					yya[yyac] = new YYARec(269,-56 );yyac++; 
					yya[yyac] = new YYARec(270,-56 );yyac++; 
					yya[yyac] = new YYARec(271,-56 );yyac++; 
					yya[yyac] = new YYARec(272,-56 );yyac++; 
					yya[yyac] = new YYARec(273,-56 );yyac++; 
					yya[yyac] = new YYARec(275,-56 );yyac++; 
					yya[yyac] = new YYARec(273,149);yyac++; 
					yya[yyac] = new YYARec(258,-54 );yyac++; 
					yya[yyac] = new YYARec(266,-54 );yyac++; 
					yya[yyac] = new YYARec(269,-54 );yyac++; 
					yya[yyac] = new YYARec(270,-54 );yyac++; 
					yya[yyac] = new YYARec(271,-54 );yyac++; 
					yya[yyac] = new YYARec(272,-54 );yyac++; 
					yya[yyac] = new YYARec(275,-54 );yyac++; 
					yya[yyac] = new YYARec(272,150);yyac++; 
					yya[yyac] = new YYARec(258,-52 );yyac++; 
					yya[yyac] = new YYARec(266,-52 );yyac++; 
					yya[yyac] = new YYARec(269,-52 );yyac++; 
					yya[yyac] = new YYARec(270,-52 );yyac++; 
					yya[yyac] = new YYARec(271,-52 );yyac++; 
					yya[yyac] = new YYARec(275,-52 );yyac++; 
					yya[yyac] = new YYARec(271,151);yyac++; 
					yya[yyac] = new YYARec(258,-50 );yyac++; 
					yya[yyac] = new YYARec(266,-50 );yyac++; 
					yya[yyac] = new YYARec(269,-50 );yyac++; 
					yya[yyac] = new YYARec(270,-50 );yyac++; 
					yya[yyac] = new YYARec(275,-50 );yyac++; 
					yya[yyac] = new YYARec(270,152);yyac++; 
					yya[yyac] = new YYARec(258,-48 );yyac++; 
					yya[yyac] = new YYARec(266,-48 );yyac++; 
					yya[yyac] = new YYARec(269,-48 );yyac++; 
					yya[yyac] = new YYARec(275,-48 );yyac++; 
					yya[yyac] = new YYARec(269,153);yyac++; 
					yya[yyac] = new YYARec(258,-46 );yyac++; 
					yya[yyac] = new YYARec(266,-46 );yyac++; 
					yya[yyac] = new YYARec(275,-46 );yyac++; 
					yya[yyac] = new YYARec(266,154);yyac++; 
					yya[yyac] = new YYARec(274,119);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,120);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(307,89);yyac++; 
					yya[yyac] = new YYARec(308,90);yyac++; 
					yya[yyac] = new YYARec(309,91);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(267,-32 );yyac++; 
					yya[yyac] = new YYARec(266,157);yyac++; 
					yya[yyac] = new YYARec(261,158);yyac++; 
					yya[yyac] = new YYARec(274,119);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,120);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(274,119);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,120);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(274,119);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,120);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(274,119);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,120);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(274,119);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,120);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(274,119);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,120);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(274,119);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,120);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(274,119);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,120);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(274,119);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,120);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(274,119);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,120);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(274,119);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,120);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(307,89);yyac++; 
					yya[yyac] = new YYARec(308,90);yyac++; 
					yya[yyac] = new YYARec(309,91);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(267,-32 );yyac++; 
					yya[yyac] = new YYARec(275,171);yyac++; 
					yya[yyac] = new YYARec(267,172);yyac++; 
					yya[yyac] = new YYARec(293,18);yyac++; 
					yya[yyac] = new YYARec(294,19);yyac++; 
					yya[yyac] = new YYARec(295,20);yyac++; 
					yya[yyac] = new YYARec(296,21);yyac++; 
					yya[yyac] = new YYARec(297,22);yyac++; 
					yya[yyac] = new YYARec(298,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,29);yyac++; 
					yya[yyac] = new YYARec(305,30);yyac++; 
					yya[yyac] = new YYARec(306,31);yyac++; 
					yya[yyac] = new YYARec(307,89);yyac++; 
					yya[yyac] = new YYARec(308,90);yyac++; 
					yya[yyac] = new YYARec(309,91);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(267,-32 );yyac++; 
					yya[yyac] = new YYARec(275,174);yyac++; 
					yya[yyac] = new YYARec(284,135);yyac++; 
					yya[yyac] = new YYARec(285,136);yyac++; 
					yya[yyac] = new YYARec(286,137);yyac++; 
					yya[yyac] = new YYARec(258,-62 );yyac++; 
					yya[yyac] = new YYARec(266,-62 );yyac++; 
					yya[yyac] = new YYARec(269,-62 );yyac++; 
					yya[yyac] = new YYARec(270,-62 );yyac++; 
					yya[yyac] = new YYARec(271,-62 );yyac++; 
					yya[yyac] = new YYARec(272,-62 );yyac++; 
					yya[yyac] = new YYARec(273,-62 );yyac++; 
					yya[yyac] = new YYARec(275,-62 );yyac++; 
					yya[yyac] = new YYARec(276,-62 );yyac++; 
					yya[yyac] = new YYARec(277,-62 );yyac++; 
					yya[yyac] = new YYARec(278,-62 );yyac++; 
					yya[yyac] = new YYARec(279,-62 );yyac++; 
					yya[yyac] = new YYARec(280,-62 );yyac++; 
					yya[yyac] = new YYARec(281,-62 );yyac++; 
					yya[yyac] = new YYARec(282,-62 );yyac++; 
					yya[yyac] = new YYARec(283,-62 );yyac++; 
					yya[yyac] = new YYARec(282,139);yyac++; 
					yya[yyac] = new YYARec(283,140);yyac++; 
					yya[yyac] = new YYARec(258,-60 );yyac++; 
					yya[yyac] = new YYARec(266,-60 );yyac++; 
					yya[yyac] = new YYARec(269,-60 );yyac++; 
					yya[yyac] = new YYARec(270,-60 );yyac++; 
					yya[yyac] = new YYARec(271,-60 );yyac++; 
					yya[yyac] = new YYARec(272,-60 );yyac++; 
					yya[yyac] = new YYARec(273,-60 );yyac++; 
					yya[yyac] = new YYARec(275,-60 );yyac++; 
					yya[yyac] = new YYARec(276,-60 );yyac++; 
					yya[yyac] = new YYARec(277,-60 );yyac++; 
					yya[yyac] = new YYARec(278,-60 );yyac++; 
					yya[yyac] = new YYARec(279,-60 );yyac++; 
					yya[yyac] = new YYARec(280,-60 );yyac++; 
					yya[yyac] = new YYARec(281,-60 );yyac++; 
					yya[yyac] = new YYARec(278,142);yyac++; 
					yya[yyac] = new YYARec(279,143);yyac++; 
					yya[yyac] = new YYARec(280,144);yyac++; 
					yya[yyac] = new YYARec(281,145);yyac++; 
					yya[yyac] = new YYARec(258,-58 );yyac++; 
					yya[yyac] = new YYARec(266,-58 );yyac++; 
					yya[yyac] = new YYARec(269,-58 );yyac++; 
					yya[yyac] = new YYARec(270,-58 );yyac++; 
					yya[yyac] = new YYARec(271,-58 );yyac++; 
					yya[yyac] = new YYARec(272,-58 );yyac++; 
					yya[yyac] = new YYARec(273,-58 );yyac++; 
					yya[yyac] = new YYARec(275,-58 );yyac++; 
					yya[yyac] = new YYARec(276,-58 );yyac++; 
					yya[yyac] = new YYARec(277,-58 );yyac++; 
					yya[yyac] = new YYARec(276,147);yyac++; 
					yya[yyac] = new YYARec(277,148);yyac++; 
					yya[yyac] = new YYARec(258,-55 );yyac++; 
					yya[yyac] = new YYARec(266,-55 );yyac++; 
					yya[yyac] = new YYARec(269,-55 );yyac++; 
					yya[yyac] = new YYARec(270,-55 );yyac++; 
					yya[yyac] = new YYARec(271,-55 );yyac++; 
					yya[yyac] = new YYARec(272,-55 );yyac++; 
					yya[yyac] = new YYARec(273,-55 );yyac++; 
					yya[yyac] = new YYARec(275,-55 );yyac++; 
					yya[yyac] = new YYARec(273,149);yyac++; 
					yya[yyac] = new YYARec(258,-53 );yyac++; 
					yya[yyac] = new YYARec(266,-53 );yyac++; 
					yya[yyac] = new YYARec(269,-53 );yyac++; 
					yya[yyac] = new YYARec(270,-53 );yyac++; 
					yya[yyac] = new YYARec(271,-53 );yyac++; 
					yya[yyac] = new YYARec(272,-53 );yyac++; 
					yya[yyac] = new YYARec(275,-53 );yyac++; 
					yya[yyac] = new YYARec(272,150);yyac++; 
					yya[yyac] = new YYARec(258,-51 );yyac++; 
					yya[yyac] = new YYARec(266,-51 );yyac++; 
					yya[yyac] = new YYARec(269,-51 );yyac++; 
					yya[yyac] = new YYARec(270,-51 );yyac++; 
					yya[yyac] = new YYARec(271,-51 );yyac++; 
					yya[yyac] = new YYARec(275,-51 );yyac++; 
					yya[yyac] = new YYARec(271,151);yyac++; 
					yya[yyac] = new YYARec(258,-49 );yyac++; 
					yya[yyac] = new YYARec(266,-49 );yyac++; 
					yya[yyac] = new YYARec(269,-49 );yyac++; 
					yya[yyac] = new YYARec(270,-49 );yyac++; 
					yya[yyac] = new YYARec(275,-49 );yyac++; 
					yya[yyac] = new YYARec(270,152);yyac++; 
					yya[yyac] = new YYARec(258,-47 );yyac++; 
					yya[yyac] = new YYARec(266,-47 );yyac++; 
					yya[yyac] = new YYARec(269,-47 );yyac++; 
					yya[yyac] = new YYARec(275,-47 );yyac++; 
					yya[yyac] = new YYARec(267,175);yyac++; 
					yya[yyac] = new YYARec(267,176);yyac++;

					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-14,2);yygc++; 
					yyg[yygc] = new YYARec(-13,3);yygc++; 
					yyg[yygc] = new YYARec(-12,4);yygc++; 
					yyg[yygc] = new YYARec(-11,5);yygc++; 
					yyg[yygc] = new YYARec(-10,6);yygc++; 
					yyg[yygc] = new YYARec(-7,7);yygc++; 
					yyg[yygc] = new YYARec(-6,8);yygc++; 
					yyg[yygc] = new YYARec(-5,9);yygc++; 
					yyg[yygc] = new YYARec(-4,10);yygc++; 
					yyg[yygc] = new YYARec(-3,11);yygc++; 
					yyg[yygc] = new YYARec(-2,12);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-44,33);yygc++; 
					yyg[yygc] = new YYARec(-20,34);yygc++; 
					yyg[yygc] = new YYARec(-19,35);yygc++; 
					yyg[yygc] = new YYARec(-18,36);yygc++; 
					yyg[yygc] = new YYARec(-17,37);yygc++; 
					yyg[yygc] = new YYARec(-16,38);yygc++; 
					yyg[yygc] = new YYARec(-15,39);yygc++; 
					yyg[yygc] = new YYARec(-7,40);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-14,2);yygc++; 
					yyg[yygc] = new YYARec(-13,3);yygc++; 
					yyg[yygc] = new YYARec(-12,4);yygc++; 
					yyg[yygc] = new YYARec(-11,5);yygc++; 
					yyg[yygc] = new YYARec(-10,6);yygc++; 
					yyg[yygc] = new YYARec(-7,7);yygc++; 
					yyg[yygc] = new YYARec(-6,8);yygc++; 
					yyg[yygc] = new YYARec(-5,9);yygc++; 
					yyg[yygc] = new YYARec(-4,10);yygc++; 
					yyg[yygc] = new YYARec(-3,47);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-7,48);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-7,49);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-7,50);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-7,51);yygc++; 
					yyg[yygc] = new YYARec(-17,52);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-44,33);yygc++; 
					yyg[yygc] = new YYARec(-27,56);yygc++; 
					yyg[yygc] = new YYARec(-26,57);yygc++; 
					yyg[yygc] = new YYARec(-21,58);yygc++; 
					yyg[yygc] = new YYARec(-19,59);yygc++; 
					yyg[yygc] = new YYARec(-17,60);yygc++; 
					yyg[yygc] = new YYARec(-16,61);yygc++; 
					yyg[yygc] = new YYARec(-15,62);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-21,71);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-44,33);yygc++; 
					yyg[yygc] = new YYARec(-27,56);yygc++; 
					yyg[yygc] = new YYARec(-26,74);yygc++; 
					yyg[yygc] = new YYARec(-19,59);yygc++; 
					yyg[yygc] = new YYARec(-17,60);yygc++; 
					yyg[yygc] = new YYARec(-16,61);yygc++; 
					yyg[yygc] = new YYARec(-15,62);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-7,75);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-14,2);yygc++; 
					yyg[yygc] = new YYARec(-13,3);yygc++; 
					yyg[yygc] = new YYARec(-12,4);yygc++; 
					yyg[yygc] = new YYARec(-11,5);yygc++; 
					yyg[yygc] = new YYARec(-10,6);yygc++; 
					yyg[yygc] = new YYARec(-9,76);yygc++; 
					yyg[yygc] = new YYARec(-8,77);yygc++; 
					yyg[yygc] = new YYARec(-7,7);yygc++; 
					yyg[yygc] = new YYARec(-5,78);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-14,2);yygc++; 
					yyg[yygc] = new YYARec(-13,3);yygc++; 
					yyg[yygc] = new YYARec(-12,4);yygc++; 
					yyg[yygc] = new YYARec(-11,5);yygc++; 
					yyg[yygc] = new YYARec(-10,6);yygc++; 
					yyg[yygc] = new YYARec(-9,76);yygc++; 
					yyg[yygc] = new YYARec(-8,79);yygc++; 
					yyg[yygc] = new YYARec(-7,7);yygc++; 
					yyg[yygc] = new YYARec(-5,78);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-44,33);yygc++; 
					yyg[yygc] = new YYARec(-16,80);yygc++; 
					yyg[yygc] = new YYARec(-15,81);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-44,33);yygc++; 
					yyg[yygc] = new YYARec(-27,56);yygc++; 
					yyg[yygc] = new YYARec(-26,82);yygc++; 
					yyg[yygc] = new YYARec(-19,59);yygc++; 
					yyg[yygc] = new YYARec(-17,60);yygc++; 
					yyg[yygc] = new YYARec(-16,61);yygc++; 
					yyg[yygc] = new YYARec(-15,62);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-25,83);yygc++; 
					yyg[yygc] = new YYARec(-24,84);yygc++; 
					yyg[yygc] = new YYARec(-23,85);yygc++; 
					yyg[yygc] = new YYARec(-22,86);yygc++; 
					yyg[yygc] = new YYARec(-20,87);yygc++; 
					yyg[yygc] = new YYARec(-7,88);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-14,2);yygc++; 
					yyg[yygc] = new YYARec(-13,3);yygc++; 
					yyg[yygc] = new YYARec(-12,4);yygc++; 
					yyg[yygc] = new YYARec(-11,5);yygc++; 
					yyg[yygc] = new YYARec(-10,6);yygc++; 
					yyg[yygc] = new YYARec(-9,94);yygc++; 
					yyg[yygc] = new YYARec(-7,7);yygc++; 
					yyg[yygc] = new YYARec(-5,78);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-25,83);yygc++; 
					yyg[yygc] = new YYARec(-24,84);yygc++; 
					yyg[yygc] = new YYARec(-23,85);yygc++; 
					yyg[yygc] = new YYARec(-22,97);yygc++; 
					yyg[yygc] = new YYARec(-20,87);yygc++; 
					yyg[yygc] = new YYARec(-7,88);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-44,33);yygc++; 
					yyg[yygc] = new YYARec(-27,56);yygc++; 
					yyg[yygc] = new YYARec(-26,57);yygc++; 
					yyg[yygc] = new YYARec(-19,59);yygc++; 
					yyg[yygc] = new YYARec(-17,60);yygc++; 
					yyg[yygc] = new YYARec(-16,61);yygc++; 
					yyg[yygc] = new YYARec(-15,100);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-46,103);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-43,106);yygc++; 
					yyg[yygc] = new YYARec(-41,107);yygc++; 
					yyg[yygc] = new YYARec(-39,108);yygc++; 
					yyg[yygc] = new YYARec(-37,109);yygc++; 
					yyg[yygc] = new YYARec(-35,110);yygc++; 
					yyg[yygc] = new YYARec(-34,111);yygc++; 
					yyg[yygc] = new YYARec(-33,112);yygc++; 
					yyg[yygc] = new YYARec(-32,113);yygc++; 
					yyg[yygc] = new YYARec(-31,114);yygc++; 
					yyg[yygc] = new YYARec(-30,115);yygc++; 
					yyg[yygc] = new YYARec(-29,116);yygc++; 
					yyg[yygc] = new YYARec(-28,117);yygc++; 
					yyg[yygc] = new YYARec(-15,118);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-46,103);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-43,106);yygc++; 
					yyg[yygc] = new YYARec(-41,107);yygc++; 
					yyg[yygc] = new YYARec(-39,108);yygc++; 
					yyg[yygc] = new YYARec(-37,109);yygc++; 
					yyg[yygc] = new YYARec(-35,110);yygc++; 
					yyg[yygc] = new YYARec(-34,111);yygc++; 
					yyg[yygc] = new YYARec(-33,112);yygc++; 
					yyg[yygc] = new YYARec(-32,113);yygc++; 
					yyg[yygc] = new YYARec(-31,114);yygc++; 
					yyg[yygc] = new YYARec(-30,115);yygc++; 
					yyg[yygc] = new YYARec(-29,116);yygc++; 
					yyg[yygc] = new YYARec(-28,122);yygc++; 
					yyg[yygc] = new YYARec(-15,118);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-14,2);yygc++; 
					yyg[yygc] = new YYARec(-13,3);yygc++; 
					yyg[yygc] = new YYARec(-12,4);yygc++; 
					yyg[yygc] = new YYARec(-11,5);yygc++; 
					yyg[yygc] = new YYARec(-10,6);yygc++; 
					yyg[yygc] = new YYARec(-9,123);yygc++; 
					yyg[yygc] = new YYARec(-7,7);yygc++; 
					yyg[yygc] = new YYARec(-5,78);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-25,83);yygc++; 
					yyg[yygc] = new YYARec(-24,84);yygc++; 
					yyg[yygc] = new YYARec(-23,85);yygc++; 
					yyg[yygc] = new YYARec(-22,124);yygc++; 
					yyg[yygc] = new YYARec(-20,87);yygc++; 
					yyg[yygc] = new YYARec(-7,88);yygc++; 
					yyg[yygc] = new YYARec(-47,125);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-44,33);yygc++; 
					yyg[yygc] = new YYARec(-27,56);yygc++; 
					yyg[yygc] = new YYARec(-26,74);yygc++; 
					yyg[yygc] = new YYARec(-19,59);yygc++; 
					yyg[yygc] = new YYARec(-17,60);yygc++; 
					yyg[yygc] = new YYARec(-16,61);yygc++; 
					yyg[yygc] = new YYARec(-15,62);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-25,83);yygc++; 
					yyg[yygc] = new YYARec(-24,84);yygc++; 
					yyg[yygc] = new YYARec(-23,85);yygc++; 
					yyg[yygc] = new YYARec(-22,131);yygc++; 
					yyg[yygc] = new YYARec(-20,87);yygc++; 
					yyg[yygc] = new YYARec(-7,88);yygc++; 
					yyg[yygc] = new YYARec(-46,103);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-43,106);yygc++; 
					yyg[yygc] = new YYARec(-41,133);yygc++; 
					yyg[yygc] = new YYARec(-15,118);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-42,134);yygc++; 
					yyg[yygc] = new YYARec(-40,138);yygc++; 
					yyg[yygc] = new YYARec(-38,141);yygc++; 
					yyg[yygc] = new YYARec(-36,146);yygc++; 
					yyg[yygc] = new YYARec(-46,103);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-43,106);yygc++; 
					yyg[yygc] = new YYARec(-41,107);yygc++; 
					yyg[yygc] = new YYARec(-39,108);yygc++; 
					yyg[yygc] = new YYARec(-37,109);yygc++; 
					yyg[yygc] = new YYARec(-35,110);yygc++; 
					yyg[yygc] = new YYARec(-34,111);yygc++; 
					yyg[yygc] = new YYARec(-33,112);yygc++; 
					yyg[yygc] = new YYARec(-32,113);yygc++; 
					yyg[yygc] = new YYARec(-31,114);yygc++; 
					yyg[yygc] = new YYARec(-30,115);yygc++; 
					yyg[yygc] = new YYARec(-29,116);yygc++; 
					yyg[yygc] = new YYARec(-28,155);yygc++; 
					yyg[yygc] = new YYARec(-15,118);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-25,83);yygc++; 
					yyg[yygc] = new YYARec(-24,84);yygc++; 
					yyg[yygc] = new YYARec(-23,85);yygc++; 
					yyg[yygc] = new YYARec(-22,156);yygc++; 
					yyg[yygc] = new YYARec(-20,87);yygc++; 
					yyg[yygc] = new YYARec(-7,88);yygc++; 
					yyg[yygc] = new YYARec(-46,103);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-43,106);yygc++; 
					yyg[yygc] = new YYARec(-41,107);yygc++; 
					yyg[yygc] = new YYARec(-39,108);yygc++; 
					yyg[yygc] = new YYARec(-37,109);yygc++; 
					yyg[yygc] = new YYARec(-35,110);yygc++; 
					yyg[yygc] = new YYARec(-34,111);yygc++; 
					yyg[yygc] = new YYARec(-33,112);yygc++; 
					yyg[yygc] = new YYARec(-32,113);yygc++; 
					yyg[yygc] = new YYARec(-31,114);yygc++; 
					yyg[yygc] = new YYARec(-30,115);yygc++; 
					yyg[yygc] = new YYARec(-29,116);yygc++; 
					yyg[yygc] = new YYARec(-28,159);yygc++; 
					yyg[yygc] = new YYARec(-15,118);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-46,103);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-43,106);yygc++; 
					yyg[yygc] = new YYARec(-41,107);yygc++; 
					yyg[yygc] = new YYARec(-39,108);yygc++; 
					yyg[yygc] = new YYARec(-37,109);yygc++; 
					yyg[yygc] = new YYARec(-35,110);yygc++; 
					yyg[yygc] = new YYARec(-34,111);yygc++; 
					yyg[yygc] = new YYARec(-33,112);yygc++; 
					yyg[yygc] = new YYARec(-32,113);yygc++; 
					yyg[yygc] = new YYARec(-31,114);yygc++; 
					yyg[yygc] = new YYARec(-30,115);yygc++; 
					yyg[yygc] = new YYARec(-29,116);yygc++; 
					yyg[yygc] = new YYARec(-28,160);yygc++; 
					yyg[yygc] = new YYARec(-15,118);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-46,103);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-43,106);yygc++; 
					yyg[yygc] = new YYARec(-41,161);yygc++; 
					yyg[yygc] = new YYARec(-15,118);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-46,103);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-43,106);yygc++; 
					yyg[yygc] = new YYARec(-41,107);yygc++; 
					yyg[yygc] = new YYARec(-39,162);yygc++; 
					yyg[yygc] = new YYARec(-15,118);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-46,103);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-43,106);yygc++; 
					yyg[yygc] = new YYARec(-41,107);yygc++; 
					yyg[yygc] = new YYARec(-39,108);yygc++; 
					yyg[yygc] = new YYARec(-37,163);yygc++; 
					yyg[yygc] = new YYARec(-15,118);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-46,103);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-43,106);yygc++; 
					yyg[yygc] = new YYARec(-41,107);yygc++; 
					yyg[yygc] = new YYARec(-39,108);yygc++; 
					yyg[yygc] = new YYARec(-37,109);yygc++; 
					yyg[yygc] = new YYARec(-35,164);yygc++; 
					yyg[yygc] = new YYARec(-15,118);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-46,103);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-43,106);yygc++; 
					yyg[yygc] = new YYARec(-41,107);yygc++; 
					yyg[yygc] = new YYARec(-39,108);yygc++; 
					yyg[yygc] = new YYARec(-37,109);yygc++; 
					yyg[yygc] = new YYARec(-35,110);yygc++; 
					yyg[yygc] = new YYARec(-34,165);yygc++; 
					yyg[yygc] = new YYARec(-15,118);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-46,103);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-43,106);yygc++; 
					yyg[yygc] = new YYARec(-41,107);yygc++; 
					yyg[yygc] = new YYARec(-39,108);yygc++; 
					yyg[yygc] = new YYARec(-37,109);yygc++; 
					yyg[yygc] = new YYARec(-35,110);yygc++; 
					yyg[yygc] = new YYARec(-34,111);yygc++; 
					yyg[yygc] = new YYARec(-33,166);yygc++; 
					yyg[yygc] = new YYARec(-15,118);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-46,103);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-43,106);yygc++; 
					yyg[yygc] = new YYARec(-41,107);yygc++; 
					yyg[yygc] = new YYARec(-39,108);yygc++; 
					yyg[yygc] = new YYARec(-37,109);yygc++; 
					yyg[yygc] = new YYARec(-35,110);yygc++; 
					yyg[yygc] = new YYARec(-34,111);yygc++; 
					yyg[yygc] = new YYARec(-33,112);yygc++; 
					yyg[yygc] = new YYARec(-32,167);yygc++; 
					yyg[yygc] = new YYARec(-15,118);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-46,103);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-43,106);yygc++; 
					yyg[yygc] = new YYARec(-41,107);yygc++; 
					yyg[yygc] = new YYARec(-39,108);yygc++; 
					yyg[yygc] = new YYARec(-37,109);yygc++; 
					yyg[yygc] = new YYARec(-35,110);yygc++; 
					yyg[yygc] = new YYARec(-34,111);yygc++; 
					yyg[yygc] = new YYARec(-33,112);yygc++; 
					yyg[yygc] = new YYARec(-32,113);yygc++; 
					yyg[yygc] = new YYARec(-31,168);yygc++; 
					yyg[yygc] = new YYARec(-15,118);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-46,103);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-44,105);yygc++; 
					yyg[yygc] = new YYARec(-43,106);yygc++; 
					yyg[yygc] = new YYARec(-41,107);yygc++; 
					yyg[yygc] = new YYARec(-39,108);yygc++; 
					yyg[yygc] = new YYARec(-37,109);yygc++; 
					yyg[yygc] = new YYARec(-35,110);yygc++; 
					yyg[yygc] = new YYARec(-34,111);yygc++; 
					yyg[yygc] = new YYARec(-33,112);yygc++; 
					yyg[yygc] = new YYARec(-32,113);yygc++; 
					yyg[yygc] = new YYARec(-31,114);yygc++; 
					yyg[yygc] = new YYARec(-30,169);yygc++; 
					yyg[yygc] = new YYARec(-15,118);yygc++; 
					yyg[yygc] = new YYARec(-7,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-25,83);yygc++; 
					yyg[yygc] = new YYARec(-24,84);yygc++; 
					yyg[yygc] = new YYARec(-23,85);yygc++; 
					yyg[yygc] = new YYARec(-22,170);yygc++; 
					yyg[yygc] = new YYARec(-20,87);yygc++; 
					yyg[yygc] = new YYARec(-7,88);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-25,83);yygc++; 
					yyg[yygc] = new YYARec(-24,84);yygc++; 
					yyg[yygc] = new YYARec(-23,85);yygc++; 
					yyg[yygc] = new YYARec(-22,173);yygc++; 
					yyg[yygc] = new YYARec(-20,87);yygc++; 
					yyg[yygc] = new YYARec(-7,88);yygc++; 
					yyg[yygc] = new YYARec(-42,134);yygc++; 
					yyg[yygc] = new YYARec(-40,138);yygc++; 
					yyg[yygc] = new YYARec(-38,141);yygc++; 
					yyg[yygc] = new YYARec(-36,146);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = -114;  
					yyd[2] = -16;  
					yyd[3] = -15;  
					yyd[4] = -14;  
					yyd[5] = -13;  
					yyd[6] = -12;  
					yyd[7] = 0;  
					yyd[8] = -5;  
					yyd[9] = -4;  
					yyd[10] = 0;  
					yyd[11] = -1;  
					yyd[12] = 0;  
					yyd[13] = 0;  
					yyd[14] = 0;  
					yyd[15] = 0;  
					yyd[16] = 0;  
					yyd[17] = 0;  
					yyd[18] = -90;  
					yyd[19] = -91;  
					yyd[20] = -92;  
					yyd[21] = -93;  
					yyd[22] = -94;  
					yyd[23] = -95;  
					yyd[24] = -96;  
					yyd[25] = -97;  
					yyd[26] = -98;  
					yyd[27] = -99;  
					yyd[28] = -100;  
					yyd[29] = -101;  
					yyd[30] = -102;  
					yyd[31] = -103;  
					yyd[32] = -113;  
					yyd[33] = 0;  
					yyd[34] = 0;  
					yyd[35] = -23;  
					yyd[36] = 0;  
					yyd[37] = -26;  
					yyd[38] = -25;  
					yyd[39] = -24;  
					yyd[40] = 0;  
					yyd[41] = -83;  
					yyd[42] = -82;  
					yyd[43] = -108;  
					yyd[44] = -111;  
					yyd[45] = -115;  
					yyd[46] = -116;  
					yyd[47] = -3;  
					yyd[48] = 0;  
					yyd[49] = 0;  
					yyd[50] = 0;  
					yyd[51] = 0;  
					yyd[52] = 0;  
					yyd[53] = -110;  
					yyd[54] = -27;  
					yyd[55] = -22;  
					yyd[56] = 0;  
					yyd[57] = -37;  
					yyd[58] = 0;  
					yyd[59] = -41;  
					yyd[60] = -40;  
					yyd[61] = -42;  
					yyd[62] = -43;  
					yyd[63] = 0;  
					yyd[64] = 0;  
					yyd[65] = 0;  
					yyd[66] = 0;  
					yyd[67] = 0;  
					yyd[68] = 0;  
					yyd[69] = -19;  
					yyd[70] = -21;  
					yyd[71] = 0;  
					yyd[72] = -44;  
					yyd[73] = 0;  
					yyd[74] = -36;  
					yyd[75] = -107;  
					yyd[76] = 0;  
					yyd[77] = -6;  
					yyd[78] = 0;  
					yyd[79] = -7;  
					yyd[80] = 0;  
					yyd[81] = 0;  
					yyd[82] = -39;  
					yyd[83] = -33;  
					yyd[84] = 0;  
					yyd[85] = 0;  
					yyd[86] = 0;  
					yyd[87] = -34;  
					yyd[88] = 0;  
					yyd[89] = 0;  
					yyd[90] = 0;  
					yyd[91] = 0;  
					yyd[92] = 0;  
					yyd[93] = -9;  
					yyd[94] = -10;  
					yyd[95] = -18;  
					yyd[96] = -17;  
					yyd[97] = -31;  
					yyd[98] = 0;  
					yyd[99] = -28;  
					yyd[100] = 0;  
					yyd[101] = 0;  
					yyd[102] = 0;  
					yyd[103] = -69;  
					yyd[104] = 0;  
					yyd[105] = 0;  
					yyd[106] = -65;  
					yyd[107] = -63;  
					yyd[108] = 0;  
					yyd[109] = 0;  
					yyd[110] = 0;  
					yyd[111] = 0;  
					yyd[112] = 0;  
					yyd[113] = 0;  
					yyd[114] = 0;  
					yyd[115] = 0;  
					yyd[116] = 0;  
					yyd[117] = 0;  
					yyd[118] = -70;  
					yyd[119] = 0;  
					yyd[120] = -112;  
					yyd[121] = 0;  
					yyd[122] = 0;  
					yyd[123] = 0;  
					yyd[124] = -30;  
					yyd[125] = 0;  
					yyd[126] = -85;  
					yyd[127] = -86;  
					yyd[128] = -87;  
					yyd[129] = -88;  
					yyd[130] = -89;  
					yyd[131] = -29;  
					yyd[132] = 0;  
					yyd[133] = -66;  
					yyd[134] = 0;  
					yyd[135] = -79;  
					yyd[136] = -80;  
					yyd[137] = -81;  
					yyd[138] = 0;  
					yyd[139] = -77;  
					yyd[140] = -78;  
					yyd[141] = 0;  
					yyd[142] = -73;  
					yyd[143] = -74;  
					yyd[144] = -75;  
					yyd[145] = -76;  
					yyd[146] = 0;  
					yyd[147] = -71;  
					yyd[148] = -72;  
					yyd[149] = 0;  
					yyd[150] = 0;  
					yyd[151] = 0;  
					yyd[152] = 0;  
					yyd[153] = 0;  
					yyd[154] = 0;  
					yyd[155] = 0;  
					yyd[156] = 0;  
					yyd[157] = 0;  
					yyd[158] = -8;  
					yyd[159] = -84;  
					yyd[160] = 0;  
					yyd[161] = -64;  
					yyd[162] = 0;  
					yyd[163] = 0;  
					yyd[164] = 0;  
					yyd[165] = 0;  
					yyd[166] = 0;  
					yyd[167] = 0;  
					yyd[168] = 0;  
					yyd[169] = 0;  
					yyd[170] = 0;  
					yyd[171] = -68;  
					yyd[172] = -104;  
					yyd[173] = 0;  
					yyd[174] = -67;  
					yyd[175] = -105;  
					yyd[176] = -106; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 21;  
					yyal[2] = 21;  
					yyal[3] = 21;  
					yyal[4] = 21;  
					yyal[5] = 21;  
					yyal[6] = 21;  
					yyal[7] = 21;  
					yyal[8] = 42;  
					yyal[9] = 42;  
					yyal[10] = 42;  
					yyal[11] = 63;  
					yyal[12] = 63;  
					yyal[13] = 64;  
					yyal[14] = 79;  
					yyal[15] = 94;  
					yyal[16] = 109;  
					yyal[17] = 124;  
					yyal[18] = 125;  
					yyal[19] = 125;  
					yyal[20] = 125;  
					yyal[21] = 125;  
					yyal[22] = 125;  
					yyal[23] = 125;  
					yyal[24] = 125;  
					yyal[25] = 125;  
					yyal[26] = 125;  
					yyal[27] = 125;  
					yyal[28] = 125;  
					yyal[29] = 125;  
					yyal[30] = 125;  
					yyal[31] = 125;  
					yyal[32] = 125;  
					yyal[33] = 125;  
					yyal[34] = 126;  
					yyal[35] = 127;  
					yyal[36] = 127;  
					yyal[37] = 128;  
					yyal[38] = 128;  
					yyal[39] = 128;  
					yyal[40] = 128;  
					yyal[41] = 153;  
					yyal[42] = 153;  
					yyal[43] = 153;  
					yyal[44] = 153;  
					yyal[45] = 153;  
					yyal[46] = 153;  
					yyal[47] = 153;  
					yyal[48] = 153;  
					yyal[49] = 154;  
					yyal[50] = 155;  
					yyal[51] = 179;  
					yyal[52] = 180;  
					yyal[53] = 181;  
					yyal[54] = 181;  
					yyal[55] = 181;  
					yyal[56] = 181;  
					yyal[57] = 204;  
					yyal[58] = 204;  
					yyal[59] = 205;  
					yyal[60] = 205;  
					yyal[61] = 205;  
					yyal[62] = 205;  
					yyal[63] = 205;  
					yyal[64] = 251;  
					yyal[65] = 273;  
					yyal[66] = 288;  
					yyal[67] = 308;  
					yyal[68] = 328;  
					yyal[69] = 347;  
					yyal[70] = 347;  
					yyal[71] = 347;  
					yyal[72] = 368;  
					yyal[73] = 368;  
					yyal[74] = 387;  
					yyal[75] = 387;  
					yyal[76] = 387;  
					yyal[77] = 389;  
					yyal[78] = 389;  
					yyal[79] = 409;  
					yyal[80] = 409;  
					yyal[81] = 410;  
					yyal[82] = 411;  
					yyal[83] = 411;  
					yyal[84] = 411;  
					yyal[85] = 430;  
					yyal[86] = 431;  
					yyal[87] = 432;  
					yyal[88] = 432;  
					yyal[89] = 456;  
					yyal[90] = 476;  
					yyal[91] = 477;  
					yyal[92] = 497;  
					yyal[93] = 516;  
					yyal[94] = 516;  
					yyal[95] = 516;  
					yyal[96] = 516;  
					yyal[97] = 516;  
					yyal[98] = 516;  
					yyal[99] = 535;  
					yyal[100] = 535;  
					yyal[101] = 563;  
					yyal[102] = 584;  
					yyal[103] = 603;  
					yyal[104] = 603;  
					yyal[105] = 624;  
					yyal[106] = 644;  
					yyal[107] = 644;  
					yyal[108] = 644;  
					yyal[109] = 663;  
					yyal[110] = 679;  
					yyal[111] = 693;  
					yyal[112] = 703;  
					yyal[113] = 711;  
					yyal[114] = 718;  
					yyal[115] = 724;  
					yyal[116] = 729;  
					yyal[117] = 733;  
					yyal[118] = 734;  
					yyal[119] = 734;  
					yyal[120] = 754;  
					yyal[121] = 754;  
					yyal[122] = 773;  
					yyal[123] = 774;  
					yyal[124] = 775;  
					yyal[125] = 775;  
					yyal[126] = 795;  
					yyal[127] = 795;  
					yyal[128] = 795;  
					yyal[129] = 795;  
					yyal[130] = 795;  
					yyal[131] = 795;  
					yyal[132] = 795;  
					yyal[133] = 815;  
					yyal[134] = 815;  
					yyal[135] = 835;  
					yyal[136] = 835;  
					yyal[137] = 835;  
					yyal[138] = 835;  
					yyal[139] = 855;  
					yyal[140] = 855;  
					yyal[141] = 855;  
					yyal[142] = 875;  
					yyal[143] = 875;  
					yyal[144] = 875;  
					yyal[145] = 875;  
					yyal[146] = 875;  
					yyal[147] = 895;  
					yyal[148] = 895;  
					yyal[149] = 895;  
					yyal[150] = 915;  
					yyal[151] = 935;  
					yyal[152] = 955;  
					yyal[153] = 975;  
					yyal[154] = 995;  
					yyal[155] = 1014;  
					yyal[156] = 1015;  
					yyal[157] = 1016;  
					yyal[158] = 1035;  
					yyal[159] = 1035;  
					yyal[160] = 1035;  
					yyal[161] = 1036;  
					yyal[162] = 1036;  
					yyal[163] = 1055;  
					yyal[164] = 1071;  
					yyal[165] = 1085;  
					yyal[166] = 1095;  
					yyal[167] = 1103;  
					yyal[168] = 1110;  
					yyal[169] = 1116;  
					yyal[170] = 1121;  
					yyal[171] = 1122;  
					yyal[172] = 1122;  
					yyal[173] = 1122;  
					yyal[174] = 1123;  
					yyal[175] = 1123;  
					yyal[176] = 1123; 

					yyah = new int[yynstates];
					yyah[0] = 20;  
					yyah[1] = 20;  
					yyah[2] = 20;  
					yyah[3] = 20;  
					yyah[4] = 20;  
					yyah[5] = 20;  
					yyah[6] = 20;  
					yyah[7] = 41;  
					yyah[8] = 41;  
					yyah[9] = 41;  
					yyah[10] = 62;  
					yyah[11] = 62;  
					yyah[12] = 63;  
					yyah[13] = 78;  
					yyah[14] = 93;  
					yyah[15] = 108;  
					yyah[16] = 123;  
					yyah[17] = 124;  
					yyah[18] = 124;  
					yyah[19] = 124;  
					yyah[20] = 124;  
					yyah[21] = 124;  
					yyah[22] = 124;  
					yyah[23] = 124;  
					yyah[24] = 124;  
					yyah[25] = 124;  
					yyah[26] = 124;  
					yyah[27] = 124;  
					yyah[28] = 124;  
					yyah[29] = 124;  
					yyah[30] = 124;  
					yyah[31] = 124;  
					yyah[32] = 124;  
					yyah[33] = 125;  
					yyah[34] = 126;  
					yyah[35] = 126;  
					yyah[36] = 127;  
					yyah[37] = 127;  
					yyah[38] = 127;  
					yyah[39] = 127;  
					yyah[40] = 152;  
					yyah[41] = 152;  
					yyah[42] = 152;  
					yyah[43] = 152;  
					yyah[44] = 152;  
					yyah[45] = 152;  
					yyah[46] = 152;  
					yyah[47] = 152;  
					yyah[48] = 153;  
					yyah[49] = 154;  
					yyah[50] = 178;  
					yyah[51] = 179;  
					yyah[52] = 180;  
					yyah[53] = 180;  
					yyah[54] = 180;  
					yyah[55] = 180;  
					yyah[56] = 203;  
					yyah[57] = 203;  
					yyah[58] = 204;  
					yyah[59] = 204;  
					yyah[60] = 204;  
					yyah[61] = 204;  
					yyah[62] = 204;  
					yyah[63] = 250;  
					yyah[64] = 272;  
					yyah[65] = 287;  
					yyah[66] = 307;  
					yyah[67] = 327;  
					yyah[68] = 346;  
					yyah[69] = 346;  
					yyah[70] = 346;  
					yyah[71] = 367;  
					yyah[72] = 367;  
					yyah[73] = 386;  
					yyah[74] = 386;  
					yyah[75] = 386;  
					yyah[76] = 388;  
					yyah[77] = 388;  
					yyah[78] = 408;  
					yyah[79] = 408;  
					yyah[80] = 409;  
					yyah[81] = 410;  
					yyah[82] = 410;  
					yyah[83] = 410;  
					yyah[84] = 429;  
					yyah[85] = 430;  
					yyah[86] = 431;  
					yyah[87] = 431;  
					yyah[88] = 455;  
					yyah[89] = 475;  
					yyah[90] = 476;  
					yyah[91] = 496;  
					yyah[92] = 515;  
					yyah[93] = 515;  
					yyah[94] = 515;  
					yyah[95] = 515;  
					yyah[96] = 515;  
					yyah[97] = 515;  
					yyah[98] = 534;  
					yyah[99] = 534;  
					yyah[100] = 562;  
					yyah[101] = 583;  
					yyah[102] = 602;  
					yyah[103] = 602;  
					yyah[104] = 623;  
					yyah[105] = 643;  
					yyah[106] = 643;  
					yyah[107] = 643;  
					yyah[108] = 662;  
					yyah[109] = 678;  
					yyah[110] = 692;  
					yyah[111] = 702;  
					yyah[112] = 710;  
					yyah[113] = 717;  
					yyah[114] = 723;  
					yyah[115] = 728;  
					yyah[116] = 732;  
					yyah[117] = 733;  
					yyah[118] = 733;  
					yyah[119] = 753;  
					yyah[120] = 753;  
					yyah[121] = 772;  
					yyah[122] = 773;  
					yyah[123] = 774;  
					yyah[124] = 774;  
					yyah[125] = 794;  
					yyah[126] = 794;  
					yyah[127] = 794;  
					yyah[128] = 794;  
					yyah[129] = 794;  
					yyah[130] = 794;  
					yyah[131] = 794;  
					yyah[132] = 814;  
					yyah[133] = 814;  
					yyah[134] = 834;  
					yyah[135] = 834;  
					yyah[136] = 834;  
					yyah[137] = 834;  
					yyah[138] = 854;  
					yyah[139] = 854;  
					yyah[140] = 854;  
					yyah[141] = 874;  
					yyah[142] = 874;  
					yyah[143] = 874;  
					yyah[144] = 874;  
					yyah[145] = 874;  
					yyah[146] = 894;  
					yyah[147] = 894;  
					yyah[148] = 894;  
					yyah[149] = 914;  
					yyah[150] = 934;  
					yyah[151] = 954;  
					yyah[152] = 974;  
					yyah[153] = 994;  
					yyah[154] = 1013;  
					yyah[155] = 1014;  
					yyah[156] = 1015;  
					yyah[157] = 1034;  
					yyah[158] = 1034;  
					yyah[159] = 1034;  
					yyah[160] = 1035;  
					yyah[161] = 1035;  
					yyah[162] = 1054;  
					yyah[163] = 1070;  
					yyah[164] = 1084;  
					yyah[165] = 1094;  
					yyah[166] = 1102;  
					yyah[167] = 1109;  
					yyah[168] = 1115;  
					yyah[169] = 1120;  
					yyah[170] = 1121;  
					yyah[171] = 1121;  
					yyah[172] = 1121;  
					yyah[173] = 1122;  
					yyah[174] = 1122;  
					yyah[175] = 1122;  
					yyah[176] = 1122; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 13;  
					yygl[2] = 13;  
					yygl[3] = 13;  
					yygl[4] = 13;  
					yygl[5] = 13;  
					yygl[6] = 13;  
					yygl[7] = 13;  
					yygl[8] = 22;  
					yygl[9] = 22;  
					yygl[10] = 22;  
					yygl[11] = 33;  
					yygl[12] = 33;  
					yygl[13] = 33;  
					yygl[14] = 35;  
					yygl[15] = 37;  
					yygl[16] = 39;  
					yygl[17] = 41;  
					yygl[18] = 42;  
					yygl[19] = 42;  
					yygl[20] = 42;  
					yygl[21] = 42;  
					yygl[22] = 42;  
					yygl[23] = 42;  
					yygl[24] = 42;  
					yygl[25] = 42;  
					yygl[26] = 42;  
					yygl[27] = 42;  
					yygl[28] = 42;  
					yygl[29] = 42;  
					yygl[30] = 42;  
					yygl[31] = 42;  
					yygl[32] = 42;  
					yygl[33] = 42;  
					yygl[34] = 42;  
					yygl[35] = 42;  
					yygl[36] = 42;  
					yygl[37] = 42;  
					yygl[38] = 42;  
					yygl[39] = 42;  
					yygl[40] = 42;  
					yygl[41] = 52;  
					yygl[42] = 52;  
					yygl[43] = 52;  
					yygl[44] = 52;  
					yygl[45] = 52;  
					yygl[46] = 52;  
					yygl[47] = 52;  
					yygl[48] = 52;  
					yygl[49] = 52;  
					yygl[50] = 52;  
					yygl[51] = 52;  
					yygl[52] = 52;  
					yygl[53] = 52;  
					yygl[54] = 52;  
					yygl[55] = 52;  
					yygl[56] = 52;  
					yygl[57] = 53;  
					yygl[58] = 53;  
					yygl[59] = 53;  
					yygl[60] = 53;  
					yygl[61] = 53;  
					yygl[62] = 53;  
					yygl[63] = 53;  
					yygl[64] = 53;  
					yygl[65] = 62;  
					yygl[66] = 64;  
					yygl[67] = 74;  
					yygl[68] = 84;  
					yygl[69] = 89;  
					yygl[70] = 89;  
					yygl[71] = 89;  
					yygl[72] = 98;  
					yygl[73] = 98;  
					yygl[74] = 105;  
					yygl[75] = 105;  
					yygl[76] = 105;  
					yygl[77] = 105;  
					yygl[78] = 105;  
					yygl[79] = 114;  
					yygl[80] = 114;  
					yygl[81] = 114;  
					yygl[82] = 114;  
					yygl[83] = 114;  
					yygl[84] = 114;  
					yygl[85] = 121;  
					yygl[86] = 121;  
					yygl[87] = 121;  
					yygl[88] = 121;  
					yygl[89] = 130;  
					yygl[90] = 147;  
					yygl[91] = 147;  
					yygl[92] = 164;  
					yygl[93] = 173;  
					yygl[94] = 173;  
					yygl[95] = 173;  
					yygl[96] = 173;  
					yygl[97] = 173;  
					yygl[98] = 173;  
					yygl[99] = 180;  
					yygl[100] = 180;  
					yygl[101] = 181;  
					yygl[102] = 190;  
					yygl[103] = 197;  
					yygl[104] = 197;  
					yygl[105] = 197;  
					yygl[106] = 204;  
					yygl[107] = 204;  
					yygl[108] = 204;  
					yygl[109] = 205;  
					yygl[110] = 206;  
					yygl[111] = 207;  
					yygl[112] = 208;  
					yygl[113] = 208;  
					yygl[114] = 208;  
					yygl[115] = 208;  
					yygl[116] = 208;  
					yygl[117] = 208;  
					yygl[118] = 208;  
					yygl[119] = 208;  
					yygl[120] = 225;  
					yygl[121] = 225;  
					yygl[122] = 232;  
					yygl[123] = 232;  
					yygl[124] = 232;  
					yygl[125] = 232;  
					yygl[126] = 249;  
					yygl[127] = 249;  
					yygl[128] = 249;  
					yygl[129] = 249;  
					yygl[130] = 249;  
					yygl[131] = 249;  
					yygl[132] = 249;  
					yygl[133] = 266;  
					yygl[134] = 266;  
					yygl[135] = 273;  
					yygl[136] = 273;  
					yygl[137] = 273;  
					yygl[138] = 273;  
					yygl[139] = 281;  
					yygl[140] = 281;  
					yygl[141] = 281;  
					yygl[142] = 290;  
					yygl[143] = 290;  
					yygl[144] = 290;  
					yygl[145] = 290;  
					yygl[146] = 290;  
					yygl[147] = 300;  
					yygl[148] = 300;  
					yygl[149] = 300;  
					yygl[150] = 311;  
					yygl[151] = 323;  
					yygl[152] = 336;  
					yygl[153] = 350;  
					yygl[154] = 365;  
					yygl[155] = 372;  
					yygl[156] = 372;  
					yygl[157] = 372;  
					yygl[158] = 379;  
					yygl[159] = 379;  
					yygl[160] = 379;  
					yygl[161] = 379;  
					yygl[162] = 379;  
					yygl[163] = 380;  
					yygl[164] = 381;  
					yygl[165] = 382;  
					yygl[166] = 383;  
					yygl[167] = 383;  
					yygl[168] = 383;  
					yygl[169] = 383;  
					yygl[170] = 383;  
					yygl[171] = 383;  
					yygl[172] = 383;  
					yygl[173] = 383;  
					yygl[174] = 383;  
					yygl[175] = 383;  
					yygl[176] = 383; 

					yygh = new int[yynstates];
					yygh[0] = 12;  
					yygh[1] = 12;  
					yygh[2] = 12;  
					yygh[3] = 12;  
					yygh[4] = 12;  
					yygh[5] = 12;  
					yygh[6] = 12;  
					yygh[7] = 21;  
					yygh[8] = 21;  
					yygh[9] = 21;  
					yygh[10] = 32;  
					yygh[11] = 32;  
					yygh[12] = 32;  
					yygh[13] = 34;  
					yygh[14] = 36;  
					yygh[15] = 38;  
					yygh[16] = 40;  
					yygh[17] = 41;  
					yygh[18] = 41;  
					yygh[19] = 41;  
					yygh[20] = 41;  
					yygh[21] = 41;  
					yygh[22] = 41;  
					yygh[23] = 41;  
					yygh[24] = 41;  
					yygh[25] = 41;  
					yygh[26] = 41;  
					yygh[27] = 41;  
					yygh[28] = 41;  
					yygh[29] = 41;  
					yygh[30] = 41;  
					yygh[31] = 41;  
					yygh[32] = 41;  
					yygh[33] = 41;  
					yygh[34] = 41;  
					yygh[35] = 41;  
					yygh[36] = 41;  
					yygh[37] = 41;  
					yygh[38] = 41;  
					yygh[39] = 41;  
					yygh[40] = 51;  
					yygh[41] = 51;  
					yygh[42] = 51;  
					yygh[43] = 51;  
					yygh[44] = 51;  
					yygh[45] = 51;  
					yygh[46] = 51;  
					yygh[47] = 51;  
					yygh[48] = 51;  
					yygh[49] = 51;  
					yygh[50] = 51;  
					yygh[51] = 51;  
					yygh[52] = 51;  
					yygh[53] = 51;  
					yygh[54] = 51;  
					yygh[55] = 51;  
					yygh[56] = 52;  
					yygh[57] = 52;  
					yygh[58] = 52;  
					yygh[59] = 52;  
					yygh[60] = 52;  
					yygh[61] = 52;  
					yygh[62] = 52;  
					yygh[63] = 52;  
					yygh[64] = 61;  
					yygh[65] = 63;  
					yygh[66] = 73;  
					yygh[67] = 83;  
					yygh[68] = 88;  
					yygh[69] = 88;  
					yygh[70] = 88;  
					yygh[71] = 97;  
					yygh[72] = 97;  
					yygh[73] = 104;  
					yygh[74] = 104;  
					yygh[75] = 104;  
					yygh[76] = 104;  
					yygh[77] = 104;  
					yygh[78] = 113;  
					yygh[79] = 113;  
					yygh[80] = 113;  
					yygh[81] = 113;  
					yygh[82] = 113;  
					yygh[83] = 113;  
					yygh[84] = 120;  
					yygh[85] = 120;  
					yygh[86] = 120;  
					yygh[87] = 120;  
					yygh[88] = 129;  
					yygh[89] = 146;  
					yygh[90] = 146;  
					yygh[91] = 163;  
					yygh[92] = 172;  
					yygh[93] = 172;  
					yygh[94] = 172;  
					yygh[95] = 172;  
					yygh[96] = 172;  
					yygh[97] = 172;  
					yygh[98] = 179;  
					yygh[99] = 179;  
					yygh[100] = 180;  
					yygh[101] = 189;  
					yygh[102] = 196;  
					yygh[103] = 196;  
					yygh[104] = 196;  
					yygh[105] = 203;  
					yygh[106] = 203;  
					yygh[107] = 203;  
					yygh[108] = 204;  
					yygh[109] = 205;  
					yygh[110] = 206;  
					yygh[111] = 207;  
					yygh[112] = 207;  
					yygh[113] = 207;  
					yygh[114] = 207;  
					yygh[115] = 207;  
					yygh[116] = 207;  
					yygh[117] = 207;  
					yygh[118] = 207;  
					yygh[119] = 224;  
					yygh[120] = 224;  
					yygh[121] = 231;  
					yygh[122] = 231;  
					yygh[123] = 231;  
					yygh[124] = 231;  
					yygh[125] = 248;  
					yygh[126] = 248;  
					yygh[127] = 248;  
					yygh[128] = 248;  
					yygh[129] = 248;  
					yygh[130] = 248;  
					yygh[131] = 248;  
					yygh[132] = 265;  
					yygh[133] = 265;  
					yygh[134] = 272;  
					yygh[135] = 272;  
					yygh[136] = 272;  
					yygh[137] = 272;  
					yygh[138] = 280;  
					yygh[139] = 280;  
					yygh[140] = 280;  
					yygh[141] = 289;  
					yygh[142] = 289;  
					yygh[143] = 289;  
					yygh[144] = 289;  
					yygh[145] = 289;  
					yygh[146] = 299;  
					yygh[147] = 299;  
					yygh[148] = 299;  
					yygh[149] = 310;  
					yygh[150] = 322;  
					yygh[151] = 335;  
					yygh[152] = 349;  
					yygh[153] = 364;  
					yygh[154] = 371;  
					yygh[155] = 371;  
					yygh[156] = 371;  
					yygh[157] = 378;  
					yygh[158] = 378;  
					yygh[159] = 378;  
					yygh[160] = 378;  
					yygh[161] = 378;  
					yygh[162] = 379;  
					yygh[163] = 380;  
					yygh[164] = 381;  
					yygh[165] = 382;  
					yygh[166] = 382;  
					yygh[167] = 382;  
					yygh[168] = 382;  
					yygh[169] = 382;  
					yygh[170] = 382;  
					yygh[171] = 382;  
					yygh[172] = 382;  
					yygh[173] = 382;  
					yygh[174] = 382;  
					yygh[175] = 382;  
					yygh[176] = 382; 

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
					yyr[yyrc] = new YYRRec(1,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-19);yyrc++;
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

			if (Regex.IsMatch(Rest,"^((?i)UNDEF)")){
				Results.Add (t_UNDEF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)UNDEF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)INCLUDE)")){
				Results.Add (t_INCLUDE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)INCLUDE)").Value);}

			if (Regex.IsMatch(Rest,"^(\\{)")){
				Results.Add (t_Char123);
				ResultsV.Add(Regex.Match(Rest,"^(\\{)").Value);}

			if (Regex.IsMatch(Rest,"^(\\}([\\t\\s]*;+)?)")){
				Results.Add (t_Char125);
				ResultsV.Add(Regex.Match(Rest,"^(\\}([\\t\\s]*;+)?)").Value);}

			if (Regex.IsMatch(Rest,"^(:)")){
				Results.Add (t_Char58);
				ResultsV.Add(Regex.Match(Rest,"^(:)").Value);}

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

			if (Regex.IsMatch(Rest,"^(\\.)")){
				Results.Add (t_Char46);
				ResultsV.Add(Regex.Match(Rest,"^(\\.)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)NULL)")){
				Results.Add (t_NULL);
				ResultsV.Add(Regex.Match(Rest,"^((?i)NULL)").Value);}

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
