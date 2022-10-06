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
                int yymaxdepth = 10240;
                int yyflag = 0;
                int yyfnone   = 0;
                int[] yys = new int[10240];
                string[] yyv = new string[10240];

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
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   11 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   12 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   13 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   14 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   15 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   16 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   17 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   18 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   19 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   38 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   39 : 
         yyval = yyv[yysp-0];
         
       break;
							case   40 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   46 : 
         yyval = "";
         
       break;
							case   47 : 
         yyval = yyv[yysp-0];
         
       break;
							case   48 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   49 : 
         yyval = yyv[yysp-0];
         
       break;
							case   50 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   51 : 
         yyval = yyv[yysp-0];
         
       break;
							case   52 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   53 : 
         yyval = yyv[yysp-0];
         
       break;
							case   54 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   55 : 
         yyval = yyv[yysp-0];
         
       break;
							case   56 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   57 : 
         yyval = yyv[yysp-0];
         
       break;
							case   58 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   63 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   64 : 
         yyval = yyv[yysp-0];
         
       break;
							case   65 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   66 : 
         yyval = yyv[yysp-0];
         
       break;
							case   67 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   68 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   69 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  107 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  108 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
                                default : return;
                        }
                }

                public void InitTables()
                {
                        ////////////////////////////////////////////////////////////////
                        /// Init Table code:
                        ////////////////////////////////////////////////////////////////

					int yynacts   = 1257;
					int yyngotos  = 422;
					int yynstates = 189;
					int yynrules  = 118;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,12);yyac++; 
					yya[yyac] = new YYARec(259,13);yyac++; 
					yya[yyac] = new YYARec(262,14);yyac++; 
					yya[yyac] = new YYARec(264,15);yyac++; 
					yya[yyac] = new YYARec(265,16);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(257,12);yyac++; 
					yya[yyac] = new YYARec(259,13);yyac++; 
					yya[yyac] = new YYARec(262,14);yyac++; 
					yya[yyac] = new YYARec(264,15);yyac++; 
					yya[yyac] = new YYARec(265,16);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(312,53);yyac++; 
					yya[yyac] = new YYARec(258,54);yyac++; 
					yya[yyac] = new YYARec(258,55);yyac++; 
					yya[yyac] = new YYARec(263,64);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(266,-46 );yyac++; 
					yya[yyac] = new YYARec(258,-111 );yyac++; 
					yya[yyac] = new YYARec(258,66);yyac++; 
					yya[yyac] = new YYARec(258,67);yyac++; 
					yya[yyac] = new YYARec(258,68);yyac++; 
					yya[yyac] = new YYARec(263,69);yyac++; 
					yya[yyac] = new YYARec(258,70);yyac++; 
					yya[yyac] = new YYARec(258,71);yyac++; 
					yya[yyac] = new YYARec(266,72);yyac++; 
					yya[yyac] = new YYARec(263,74);yyac++; 
					yya[yyac] = new YYARec(258,-39 );yyac++; 
					yya[yyac] = new YYARec(282,-46 );yyac++; 
					yya[yyac] = new YYARec(283,-46 );yyac++; 
					yya[yyac] = new YYARec(287,-46 );yyac++; 
					yya[yyac] = new YYARec(293,-46 );yyac++; 
					yya[yyac] = new YYARec(294,-46 );yyac++; 
					yya[yyac] = new YYARec(295,-46 );yyac++; 
					yya[yyac] = new YYARec(296,-46 );yyac++; 
					yya[yyac] = new YYARec(297,-46 );yyac++; 
					yya[yyac] = new YYARec(298,-46 );yyac++; 
					yya[yyac] = new YYARec(299,-46 );yyac++; 
					yya[yyac] = new YYARec(300,-46 );yyac++; 
					yya[yyac] = new YYARec(301,-46 );yyac++; 
					yya[yyac] = new YYARec(302,-46 );yyac++; 
					yya[yyac] = new YYARec(303,-46 );yyac++; 
					yya[yyac] = new YYARec(304,-46 );yyac++; 
					yya[yyac] = new YYARec(305,-46 );yyac++; 
					yya[yyac] = new YYARec(306,-46 );yyac++; 
					yya[yyac] = new YYARec(311,-46 );yyac++; 
					yya[yyac] = new YYARec(312,-46 );yyac++; 
					yya[yyac] = new YYARec(313,-46 );yyac++; 
					yya[yyac] = new YYARec(314,-46 );yyac++; 
					yya[yyac] = new YYARec(315,-46 );yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(258,-111 );yyac++; 
					yya[yyac] = new YYARec(263,-111 );yyac++; 
					yya[yyac] = new YYARec(266,-111 );yyac++; 
					yya[yyac] = new YYARec(269,-111 );yyac++; 
					yya[yyac] = new YYARec(270,-111 );yyac++; 
					yya[yyac] = new YYARec(271,-111 );yyac++; 
					yya[yyac] = new YYARec(272,-111 );yyac++; 
					yya[yyac] = new YYARec(273,-111 );yyac++; 
					yya[yyac] = new YYARec(275,-111 );yyac++; 
					yya[yyac] = new YYARec(276,-111 );yyac++; 
					yya[yyac] = new YYARec(277,-111 );yyac++; 
					yya[yyac] = new YYARec(278,-111 );yyac++; 
					yya[yyac] = new YYARec(279,-111 );yyac++; 
					yya[yyac] = new YYARec(280,-111 );yyac++; 
					yya[yyac] = new YYARec(281,-111 );yyac++; 
					yya[yyac] = new YYARec(282,-111 );yyac++; 
					yya[yyac] = new YYARec(283,-111 );yyac++; 
					yya[yyac] = new YYARec(284,-111 );yyac++; 
					yya[yyac] = new YYARec(285,-111 );yyac++; 
					yya[yyac] = new YYARec(286,-111 );yyac++; 
					yya[yyac] = new YYARec(287,-111 );yyac++; 
					yya[yyac] = new YYARec(288,-111 );yyac++; 
					yya[yyac] = new YYARec(289,-111 );yyac++; 
					yya[yyac] = new YYARec(290,-111 );yyac++; 
					yya[yyac] = new YYARec(291,-111 );yyac++; 
					yya[yyac] = new YYARec(292,-111 );yyac++; 
					yya[yyac] = new YYARec(293,-111 );yyac++; 
					yya[yyac] = new YYARec(294,-111 );yyac++; 
					yya[yyac] = new YYARec(295,-111 );yyac++; 
					yya[yyac] = new YYARec(296,-111 );yyac++; 
					yya[yyac] = new YYARec(297,-111 );yyac++; 
					yya[yyac] = new YYARec(298,-111 );yyac++; 
					yya[yyac] = new YYARec(299,-111 );yyac++; 
					yya[yyac] = new YYARec(300,-111 );yyac++; 
					yya[yyac] = new YYARec(301,-111 );yyac++; 
					yya[yyac] = new YYARec(302,-111 );yyac++; 
					yya[yyac] = new YYARec(303,-111 );yyac++; 
					yya[yyac] = new YYARec(304,-111 );yyac++; 
					yya[yyac] = new YYARec(305,-111 );yyac++; 
					yya[yyac] = new YYARec(306,-111 );yyac++; 
					yya[yyac] = new YYARec(311,-111 );yyac++; 
					yya[yyac] = new YYARec(312,-111 );yyac++; 
					yya[yyac] = new YYARec(313,-111 );yyac++; 
					yya[yyac] = new YYARec(314,-111 );yyac++; 
					yya[yyac] = new YYARec(315,-111 );yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(266,-45 );yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(257,12);yyac++; 
					yya[yyac] = new YYARec(259,13);yyac++; 
					yya[yyac] = new YYARec(262,14);yyac++; 
					yya[yyac] = new YYARec(264,15);yyac++; 
					yya[yyac] = new YYARec(265,16);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(257,12);yyac++; 
					yya[yyac] = new YYARec(259,13);yyac++; 
					yya[yyac] = new YYARec(262,14);yyac++; 
					yya[yyac] = new YYARec(264,15);yyac++; 
					yya[yyac] = new YYARec(265,16);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(257,88);yyac++; 
					yya[yyac] = new YYARec(259,89);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,90);yyac++; 
					yya[yyac] = new YYARec(308,91);yyac++; 
					yya[yyac] = new YYARec(309,92);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(267,-33 );yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(260,94);yyac++; 
					yya[yyac] = new YYARec(261,95);yyac++; 
					yya[yyac] = new YYARec(258,96);yyac++; 
					yya[yyac] = new YYARec(257,88);yyac++; 
					yya[yyac] = new YYARec(259,89);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,90);yyac++; 
					yya[yyac] = new YYARec(308,91);yyac++; 
					yya[yyac] = new YYARec(309,92);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(260,-33 );yyac++; 
					yya[yyac] = new YYARec(261,-33 );yyac++; 
					yya[yyac] = new YYARec(267,-33 );yyac++; 
					yya[yyac] = new YYARec(258,98);yyac++; 
					yya[yyac] = new YYARec(267,99);yyac++; 
					yya[yyac] = new YYARec(257,88);yyac++; 
					yya[yyac] = new YYARec(259,89);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,90);yyac++; 
					yya[yyac] = new YYARec(308,91);yyac++; 
					yya[yyac] = new YYARec(309,92);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(260,-33 );yyac++; 
					yya[yyac] = new YYARec(261,-33 );yyac++; 
					yya[yyac] = new YYARec(267,-33 );yyac++; 
					yya[yyac] = new YYARec(263,102);yyac++; 
					yya[yyac] = new YYARec(268,103);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(258,-36 );yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(274,122);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,123);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(266,124);yyac++; 
					yya[yyac] = new YYARec(274,122);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,123);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(257,12);yyac++; 
					yya[yyac] = new YYARec(259,13);yyac++; 
					yya[yyac] = new YYARec(262,14);yyac++; 
					yya[yyac] = new YYARec(264,15);yyac++; 
					yya[yyac] = new YYARec(265,16);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(257,88);yyac++; 
					yya[yyac] = new YYARec(259,89);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,90);yyac++; 
					yya[yyac] = new YYARec(308,91);yyac++; 
					yya[yyac] = new YYARec(309,92);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(260,-33 );yyac++; 
					yya[yyac] = new YYARec(261,-33 );yyac++; 
					yya[yyac] = new YYARec(267,-33 );yyac++; 
					yya[yyac] = new YYARec(288,129);yyac++; 
					yya[yyac] = new YYARec(289,130);yyac++; 
					yya[yyac] = new YYARec(290,131);yyac++; 
					yya[yyac] = new YYARec(291,132);yyac++; 
					yya[yyac] = new YYARec(292,133);yyac++; 
					yya[yyac] = new YYARec(258,-44 );yyac++; 
					yya[yyac] = new YYARec(263,-44 );yyac++; 
					yya[yyac] = new YYARec(282,-44 );yyac++; 
					yya[yyac] = new YYARec(283,-44 );yyac++; 
					yya[yyac] = new YYARec(287,-44 );yyac++; 
					yya[yyac] = new YYARec(293,-44 );yyac++; 
					yya[yyac] = new YYARec(294,-44 );yyac++; 
					yya[yyac] = new YYARec(295,-44 );yyac++; 
					yya[yyac] = new YYARec(296,-44 );yyac++; 
					yya[yyac] = new YYARec(297,-44 );yyac++; 
					yya[yyac] = new YYARec(298,-44 );yyac++; 
					yya[yyac] = new YYARec(299,-44 );yyac++; 
					yya[yyac] = new YYARec(300,-44 );yyac++; 
					yya[yyac] = new YYARec(301,-44 );yyac++; 
					yya[yyac] = new YYARec(302,-44 );yyac++; 
					yya[yyac] = new YYARec(303,-44 );yyac++; 
					yya[yyac] = new YYARec(304,-44 );yyac++; 
					yya[yyac] = new YYARec(305,-44 );yyac++; 
					yya[yyac] = new YYARec(306,-44 );yyac++; 
					yya[yyac] = new YYARec(311,-44 );yyac++; 
					yya[yyac] = new YYARec(312,-44 );yyac++; 
					yya[yyac] = new YYARec(313,-44 );yyac++; 
					yya[yyac] = new YYARec(314,-44 );yyac++; 
					yya[yyac] = new YYARec(315,-44 );yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(257,88);yyac++; 
					yya[yyac] = new YYARec(259,89);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,90);yyac++; 
					yya[yyac] = new YYARec(308,91);yyac++; 
					yya[yyac] = new YYARec(309,92);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(260,-33 );yyac++; 
					yya[yyac] = new YYARec(261,-33 );yyac++; 
					yya[yyac] = new YYARec(267,-33 );yyac++; 
					yya[yyac] = new YYARec(258,135);yyac++; 
					yya[yyac] = new YYARec(258,136);yyac++; 
					yya[yyac] = new YYARec(274,137);yyac++; 
					yya[yyac] = new YYARec(258,-116 );yyac++; 
					yya[yyac] = new YYARec(266,-116 );yyac++; 
					yya[yyac] = new YYARec(269,-116 );yyac++; 
					yya[yyac] = new YYARec(270,-116 );yyac++; 
					yya[yyac] = new YYARec(271,-116 );yyac++; 
					yya[yyac] = new YYARec(272,-116 );yyac++; 
					yya[yyac] = new YYARec(273,-116 );yyac++; 
					yya[yyac] = new YYARec(275,-116 );yyac++; 
					yya[yyac] = new YYARec(276,-116 );yyac++; 
					yya[yyac] = new YYARec(277,-116 );yyac++; 
					yya[yyac] = new YYARec(278,-116 );yyac++; 
					yya[yyac] = new YYARec(279,-116 );yyac++; 
					yya[yyac] = new YYARec(280,-116 );yyac++; 
					yya[yyac] = new YYARec(281,-116 );yyac++; 
					yya[yyac] = new YYARec(282,-116 );yyac++; 
					yya[yyac] = new YYARec(283,-116 );yyac++; 
					yya[yyac] = new YYARec(284,-116 );yyac++; 
					yya[yyac] = new YYARec(285,-116 );yyac++; 
					yya[yyac] = new YYARec(286,-116 );yyac++; 
					yya[yyac] = new YYARec(310,-116 );yyac++; 
					yya[yyac] = new YYARec(274,122);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,123);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(284,140);yyac++; 
					yya[yyac] = new YYARec(285,141);yyac++; 
					yya[yyac] = new YYARec(286,142);yyac++; 
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
					yya[yyac] = new YYARec(282,144);yyac++; 
					yya[yyac] = new YYARec(283,145);yyac++; 
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
					yya[yyac] = new YYARec(278,147);yyac++; 
					yya[yyac] = new YYARec(279,148);yyac++; 
					yya[yyac] = new YYARec(280,149);yyac++; 
					yya[yyac] = new YYARec(281,150);yyac++; 
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
					yya[yyac] = new YYARec(276,152);yyac++; 
					yya[yyac] = new YYARec(277,153);yyac++; 
					yya[yyac] = new YYARec(258,-57 );yyac++; 
					yya[yyac] = new YYARec(266,-57 );yyac++; 
					yya[yyac] = new YYARec(269,-57 );yyac++; 
					yya[yyac] = new YYARec(270,-57 );yyac++; 
					yya[yyac] = new YYARec(271,-57 );yyac++; 
					yya[yyac] = new YYARec(272,-57 );yyac++; 
					yya[yyac] = new YYARec(273,-57 );yyac++; 
					yya[yyac] = new YYARec(275,-57 );yyac++; 
					yya[yyac] = new YYARec(273,154);yyac++; 
					yya[yyac] = new YYARec(258,-55 );yyac++; 
					yya[yyac] = new YYARec(266,-55 );yyac++; 
					yya[yyac] = new YYARec(269,-55 );yyac++; 
					yya[yyac] = new YYARec(270,-55 );yyac++; 
					yya[yyac] = new YYARec(271,-55 );yyac++; 
					yya[yyac] = new YYARec(272,-55 );yyac++; 
					yya[yyac] = new YYARec(275,-55 );yyac++; 
					yya[yyac] = new YYARec(272,155);yyac++; 
					yya[yyac] = new YYARec(258,-53 );yyac++; 
					yya[yyac] = new YYARec(266,-53 );yyac++; 
					yya[yyac] = new YYARec(269,-53 );yyac++; 
					yya[yyac] = new YYARec(270,-53 );yyac++; 
					yya[yyac] = new YYARec(271,-53 );yyac++; 
					yya[yyac] = new YYARec(275,-53 );yyac++; 
					yya[yyac] = new YYARec(271,156);yyac++; 
					yya[yyac] = new YYARec(258,-51 );yyac++; 
					yya[yyac] = new YYARec(266,-51 );yyac++; 
					yya[yyac] = new YYARec(269,-51 );yyac++; 
					yya[yyac] = new YYARec(270,-51 );yyac++; 
					yya[yyac] = new YYARec(275,-51 );yyac++; 
					yya[yyac] = new YYARec(270,157);yyac++; 
					yya[yyac] = new YYARec(258,-49 );yyac++; 
					yya[yyac] = new YYARec(266,-49 );yyac++; 
					yya[yyac] = new YYARec(269,-49 );yyac++; 
					yya[yyac] = new YYARec(275,-49 );yyac++; 
					yya[yyac] = new YYARec(269,158);yyac++; 
					yya[yyac] = new YYARec(258,-47 );yyac++; 
					yya[yyac] = new YYARec(266,-47 );yyac++; 
					yya[yyac] = new YYARec(275,-47 );yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,122);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,123);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(257,88);yyac++; 
					yya[yyac] = new YYARec(259,89);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,90);yyac++; 
					yya[yyac] = new YYARec(308,91);yyac++; 
					yya[yyac] = new YYARec(309,92);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(267,-33 );yyac++; 
					yya[yyac] = new YYARec(266,162);yyac++; 
					yya[yyac] = new YYARec(261,163);yyac++; 
					yya[yyac] = new YYARec(274,122);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,123);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(257,88);yyac++; 
					yya[yyac] = new YYARec(259,89);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,90);yyac++; 
					yya[yyac] = new YYARec(308,91);yyac++; 
					yya[yyac] = new YYARec(309,92);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(260,-33 );yyac++; 
					yya[yyac] = new YYARec(261,-33 );yyac++; 
					yya[yyac] = new YYARec(257,88);yyac++; 
					yya[yyac] = new YYARec(259,89);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,90);yyac++; 
					yya[yyac] = new YYARec(308,91);yyac++; 
					yya[yyac] = new YYARec(309,92);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(260,-33 );yyac++; 
					yya[yyac] = new YYARec(261,-33 );yyac++; 
					yya[yyac] = new YYARec(274,122);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,123);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(274,122);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,123);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(274,122);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,123);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(274,122);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,123);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(274,122);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,123);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(274,122);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,123);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(274,122);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,123);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(274,122);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,123);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(274,122);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,123);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(274,122);yyac++; 
					yya[yyac] = new YYARec(282,40);yyac++; 
					yya[yyac] = new YYARec(283,41);yyac++; 
					yya[yyac] = new YYARec(287,42);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,123);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(257,88);yyac++; 
					yya[yyac] = new YYARec(259,89);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,90);yyac++; 
					yya[yyac] = new YYARec(308,91);yyac++; 
					yya[yyac] = new YYARec(309,92);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(267,-33 );yyac++; 
					yya[yyac] = new YYARec(275,179);yyac++; 
					yya[yyac] = new YYARec(267,180);yyac++; 
					yya[yyac] = new YYARec(257,88);yyac++; 
					yya[yyac] = new YYARec(259,89);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,90);yyac++; 
					yya[yyac] = new YYARec(308,91);yyac++; 
					yya[yyac] = new YYARec(309,92);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(267,-33 );yyac++; 
					yya[yyac] = new YYARec(260,182);yyac++; 
					yya[yyac] = new YYARec(261,183);yyac++; 
					yya[yyac] = new YYARec(275,184);yyac++; 
					yya[yyac] = new YYARec(284,140);yyac++; 
					yya[yyac] = new YYARec(285,141);yyac++; 
					yya[yyac] = new YYARec(286,142);yyac++; 
					yya[yyac] = new YYARec(258,-63 );yyac++; 
					yya[yyac] = new YYARec(266,-63 );yyac++; 
					yya[yyac] = new YYARec(269,-63 );yyac++; 
					yya[yyac] = new YYARec(270,-63 );yyac++; 
					yya[yyac] = new YYARec(271,-63 );yyac++; 
					yya[yyac] = new YYARec(272,-63 );yyac++; 
					yya[yyac] = new YYARec(273,-63 );yyac++; 
					yya[yyac] = new YYARec(275,-63 );yyac++; 
					yya[yyac] = new YYARec(276,-63 );yyac++; 
					yya[yyac] = new YYARec(277,-63 );yyac++; 
					yya[yyac] = new YYARec(278,-63 );yyac++; 
					yya[yyac] = new YYARec(279,-63 );yyac++; 
					yya[yyac] = new YYARec(280,-63 );yyac++; 
					yya[yyac] = new YYARec(281,-63 );yyac++; 
					yya[yyac] = new YYARec(282,-63 );yyac++; 
					yya[yyac] = new YYARec(283,-63 );yyac++; 
					yya[yyac] = new YYARec(282,144);yyac++; 
					yya[yyac] = new YYARec(283,145);yyac++; 
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
					yya[yyac] = new YYARec(278,147);yyac++; 
					yya[yyac] = new YYARec(279,148);yyac++; 
					yya[yyac] = new YYARec(280,149);yyac++; 
					yya[yyac] = new YYARec(281,150);yyac++; 
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
					yya[yyac] = new YYARec(276,152);yyac++; 
					yya[yyac] = new YYARec(277,153);yyac++; 
					yya[yyac] = new YYARec(258,-56 );yyac++; 
					yya[yyac] = new YYARec(266,-56 );yyac++; 
					yya[yyac] = new YYARec(269,-56 );yyac++; 
					yya[yyac] = new YYARec(270,-56 );yyac++; 
					yya[yyac] = new YYARec(271,-56 );yyac++; 
					yya[yyac] = new YYARec(272,-56 );yyac++; 
					yya[yyac] = new YYARec(273,-56 );yyac++; 
					yya[yyac] = new YYARec(275,-56 );yyac++; 
					yya[yyac] = new YYARec(273,154);yyac++; 
					yya[yyac] = new YYARec(258,-54 );yyac++; 
					yya[yyac] = new YYARec(266,-54 );yyac++; 
					yya[yyac] = new YYARec(269,-54 );yyac++; 
					yya[yyac] = new YYARec(270,-54 );yyac++; 
					yya[yyac] = new YYARec(271,-54 );yyac++; 
					yya[yyac] = new YYARec(272,-54 );yyac++; 
					yya[yyac] = new YYARec(275,-54 );yyac++; 
					yya[yyac] = new YYARec(272,155);yyac++; 
					yya[yyac] = new YYARec(258,-52 );yyac++; 
					yya[yyac] = new YYARec(266,-52 );yyac++; 
					yya[yyac] = new YYARec(269,-52 );yyac++; 
					yya[yyac] = new YYARec(270,-52 );yyac++; 
					yya[yyac] = new YYARec(271,-52 );yyac++; 
					yya[yyac] = new YYARec(275,-52 );yyac++; 
					yya[yyac] = new YYARec(271,156);yyac++; 
					yya[yyac] = new YYARec(258,-50 );yyac++; 
					yya[yyac] = new YYARec(266,-50 );yyac++; 
					yya[yyac] = new YYARec(269,-50 );yyac++; 
					yya[yyac] = new YYARec(270,-50 );yyac++; 
					yya[yyac] = new YYARec(275,-50 );yyac++; 
					yya[yyac] = new YYARec(270,157);yyac++; 
					yya[yyac] = new YYARec(258,-48 );yyac++; 
					yya[yyac] = new YYARec(266,-48 );yyac++; 
					yya[yyac] = new YYARec(269,-48 );yyac++; 
					yya[yyac] = new YYARec(275,-48 );yyac++; 
					yya[yyac] = new YYARec(267,185);yyac++; 
					yya[yyac] = new YYARec(267,186);yyac++; 
					yya[yyac] = new YYARec(257,88);yyac++; 
					yya[yyac] = new YYARec(259,89);yyac++; 
					yya[yyac] = new YYARec(293,17);yyac++; 
					yya[yyac] = new YYARec(294,18);yyac++; 
					yya[yyac] = new YYARec(295,19);yyac++; 
					yya[yyac] = new YYARec(296,20);yyac++; 
					yya[yyac] = new YYARec(297,21);yyac++; 
					yya[yyac] = new YYARec(298,22);yyac++; 
					yya[yyac] = new YYARec(299,23);yyac++; 
					yya[yyac] = new YYARec(300,24);yyac++; 
					yya[yyac] = new YYARec(301,25);yyac++; 
					yya[yyac] = new YYARec(302,26);yyac++; 
					yya[yyac] = new YYARec(303,27);yyac++; 
					yya[yyac] = new YYARec(304,28);yyac++; 
					yya[yyac] = new YYARec(305,29);yyac++; 
					yya[yyac] = new YYARec(306,30);yyac++; 
					yya[yyac] = new YYARec(307,90);yyac++; 
					yya[yyac] = new YYARec(308,91);yyac++; 
					yya[yyac] = new YYARec(309,92);yyac++; 
					yya[yyac] = new YYARec(313,31);yyac++; 
					yya[yyac] = new YYARec(261,-33 );yyac++; 
					yya[yyac] = new YYARec(261,188);yyac++;

					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-11,2);yygc++; 
					yyg[yygc] = new YYARec(-10,3);yygc++; 
					yyg[yygc] = new YYARec(-9,4);yygc++; 
					yyg[yygc] = new YYARec(-8,5);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-6,7);yygc++; 
					yyg[yygc] = new YYARec(-5,8);yygc++; 
					yyg[yygc] = new YYARec(-4,9);yygc++; 
					yyg[yygc] = new YYARec(-3,10);yygc++; 
					yyg[yygc] = new YYARec(-2,11);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-44,32);yygc++; 
					yyg[yygc] = new YYARec(-22,33);yygc++; 
					yyg[yygc] = new YYARec(-21,34);yygc++; 
					yyg[yygc] = new YYARec(-20,35);yygc++; 
					yyg[yygc] = new YYARec(-19,36);yygc++; 
					yyg[yygc] = new YYARec(-18,37);yygc++; 
					yyg[yygc] = new YYARec(-17,38);yygc++; 
					yyg[yygc] = new YYARec(-11,39);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-11,2);yygc++; 
					yyg[yygc] = new YYARec(-10,3);yygc++; 
					yyg[yygc] = new YYARec(-9,4);yygc++; 
					yyg[yygc] = new YYARec(-8,5);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-6,7);yygc++; 
					yyg[yygc] = new YYARec(-5,8);yygc++; 
					yyg[yygc] = new YYARec(-4,9);yygc++; 
					yyg[yygc] = new YYARec(-3,47);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-11,48);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-11,49);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-11,50);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-11,51);yygc++; 
					yyg[yygc] = new YYARec(-17,52);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-44,32);yygc++; 
					yyg[yygc] = new YYARec(-27,56);yygc++; 
					yyg[yygc] = new YYARec(-23,57);yygc++; 
					yyg[yygc] = new YYARec(-21,58);yygc++; 
					yyg[yygc] = new YYARec(-20,59);yygc++; 
					yyg[yygc] = new YYARec(-19,60);yygc++; 
					yyg[yygc] = new YYARec(-17,61);yygc++; 
					yyg[yygc] = new YYARec(-16,62);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-23,73);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-44,32);yygc++; 
					yyg[yygc] = new YYARec(-27,75);yygc++; 
					yyg[yygc] = new YYARec(-21,58);yygc++; 
					yyg[yygc] = new YYARec(-20,59);yygc++; 
					yyg[yygc] = new YYARec(-19,60);yygc++; 
					yyg[yygc] = new YYARec(-17,61);yygc++; 
					yyg[yygc] = new YYARec(-16,62);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-11,76);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-12,77);yygc++; 
					yyg[yygc] = new YYARec(-11,2);yygc++; 
					yyg[yygc] = new YYARec(-10,3);yygc++; 
					yyg[yygc] = new YYARec(-9,4);yygc++; 
					yyg[yygc] = new YYARec(-8,5);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-6,7);yygc++; 
					yyg[yygc] = new YYARec(-5,8);yygc++; 
					yyg[yygc] = new YYARec(-4,9);yygc++; 
					yyg[yygc] = new YYARec(-3,78);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-12,79);yygc++; 
					yyg[yygc] = new YYARec(-11,2);yygc++; 
					yyg[yygc] = new YYARec(-10,3);yygc++; 
					yyg[yygc] = new YYARec(-9,4);yygc++; 
					yyg[yygc] = new YYARec(-8,5);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-6,7);yygc++; 
					yyg[yygc] = new YYARec(-5,8);yygc++; 
					yyg[yygc] = new YYARec(-4,9);yygc++; 
					yyg[yygc] = new YYARec(-3,78);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-44,32);yygc++; 
					yyg[yygc] = new YYARec(-21,58);yygc++; 
					yyg[yygc] = new YYARec(-20,59);yygc++; 
					yyg[yygc] = new YYARec(-19,60);yygc++; 
					yyg[yygc] = new YYARec(-17,61);yygc++; 
					yyg[yygc] = new YYARec(-16,80);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-26,81);yygc++; 
					yyg[yygc] = new YYARec(-25,82);yygc++; 
					yyg[yygc] = new YYARec(-24,83);yygc++; 
					yyg[yygc] = new YYARec(-22,84);yygc++; 
					yyg[yygc] = new YYARec(-15,85);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-11,87);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-44,32);yygc++; 
					yyg[yygc] = new YYARec(-27,93);yygc++; 
					yyg[yygc] = new YYARec(-21,58);yygc++; 
					yyg[yygc] = new YYARec(-20,59);yygc++; 
					yyg[yygc] = new YYARec(-19,60);yygc++; 
					yyg[yygc] = new YYARec(-17,61);yygc++; 
					yyg[yygc] = new YYARec(-16,62);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-26,81);yygc++; 
					yyg[yygc] = new YYARec(-25,82);yygc++; 
					yyg[yygc] = new YYARec(-24,83);yygc++; 
					yyg[yygc] = new YYARec(-22,84);yygc++; 
					yyg[yygc] = new YYARec(-15,97);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-11,87);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-26,81);yygc++; 
					yyg[yygc] = new YYARec(-25,82);yygc++; 
					yyg[yygc] = new YYARec(-24,83);yygc++; 
					yyg[yygc] = new YYARec(-22,84);yygc++; 
					yyg[yygc] = new YYARec(-15,100);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-11,87);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-44,32);yygc++; 
					yyg[yygc] = new YYARec(-27,56);yygc++; 
					yyg[yygc] = new YYARec(-21,58);yygc++; 
					yyg[yygc] = new YYARec(-20,101);yygc++; 
					yyg[yygc] = new YYARec(-19,60);yygc++; 
					yyg[yygc] = new YYARec(-17,61);yygc++; 
					yyg[yygc] = new YYARec(-16,62);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-11,104);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-11,105);yygc++; 
					yyg[yygc] = new YYARec(-46,106);yygc++; 
					yyg[yygc] = new YYARec(-45,107);yygc++; 
					yyg[yygc] = new YYARec(-44,108);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-41,110);yygc++; 
					yyg[yygc] = new YYARec(-39,111);yygc++; 
					yyg[yygc] = new YYARec(-37,112);yygc++; 
					yyg[yygc] = new YYARec(-35,113);yygc++; 
					yyg[yygc] = new YYARec(-34,114);yygc++; 
					yyg[yygc] = new YYARec(-33,115);yygc++; 
					yyg[yygc] = new YYARec(-32,116);yygc++; 
					yyg[yygc] = new YYARec(-31,117);yygc++; 
					yyg[yygc] = new YYARec(-30,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-28,120);yygc++; 
					yyg[yygc] = new YYARec(-20,121);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-46,106);yygc++; 
					yyg[yygc] = new YYARec(-45,107);yygc++; 
					yyg[yygc] = new YYARec(-44,108);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-41,110);yygc++; 
					yyg[yygc] = new YYARec(-39,111);yygc++; 
					yyg[yygc] = new YYARec(-37,112);yygc++; 
					yyg[yygc] = new YYARec(-35,113);yygc++; 
					yyg[yygc] = new YYARec(-34,114);yygc++; 
					yyg[yygc] = new YYARec(-33,115);yygc++; 
					yyg[yygc] = new YYARec(-32,116);yygc++; 
					yyg[yygc] = new YYARec(-31,117);yygc++; 
					yyg[yygc] = new YYARec(-30,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-28,125);yygc++; 
					yyg[yygc] = new YYARec(-20,121);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-11,2);yygc++; 
					yyg[yygc] = new YYARec(-10,3);yygc++; 
					yyg[yygc] = new YYARec(-9,4);yygc++; 
					yyg[yygc] = new YYARec(-8,5);yygc++; 
					yyg[yygc] = new YYARec(-7,6);yygc++; 
					yyg[yygc] = new YYARec(-6,7);yygc++; 
					yyg[yygc] = new YYARec(-5,8);yygc++; 
					yyg[yygc] = new YYARec(-4,9);yygc++; 
					yyg[yygc] = new YYARec(-3,126);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-26,81);yygc++; 
					yyg[yygc] = new YYARec(-25,82);yygc++; 
					yyg[yygc] = new YYARec(-24,83);yygc++; 
					yyg[yygc] = new YYARec(-22,84);yygc++; 
					yyg[yygc] = new YYARec(-15,127);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-11,87);yygc++; 
					yyg[yygc] = new YYARec(-47,128);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-44,32);yygc++; 
					yyg[yygc] = new YYARec(-27,75);yygc++; 
					yyg[yygc] = new YYARec(-21,58);yygc++; 
					yyg[yygc] = new YYARec(-20,59);yygc++; 
					yyg[yygc] = new YYARec(-19,60);yygc++; 
					yyg[yygc] = new YYARec(-17,61);yygc++; 
					yyg[yygc] = new YYARec(-16,62);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-26,81);yygc++; 
					yyg[yygc] = new YYARec(-25,82);yygc++; 
					yyg[yygc] = new YYARec(-24,83);yygc++; 
					yyg[yygc] = new YYARec(-22,84);yygc++; 
					yyg[yygc] = new YYARec(-15,134);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-11,87);yygc++; 
					yyg[yygc] = new YYARec(-46,106);yygc++; 
					yyg[yygc] = new YYARec(-45,107);yygc++; 
					yyg[yygc] = new YYARec(-44,108);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-41,138);yygc++; 
					yyg[yygc] = new YYARec(-20,121);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-42,139);yygc++; 
					yyg[yygc] = new YYARec(-40,143);yygc++; 
					yyg[yygc] = new YYARec(-38,146);yygc++; 
					yyg[yygc] = new YYARec(-36,151);yygc++; 
					yyg[yygc] = new YYARec(-46,106);yygc++; 
					yyg[yygc] = new YYARec(-45,107);yygc++; 
					yyg[yygc] = new YYARec(-44,108);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-41,110);yygc++; 
					yyg[yygc] = new YYARec(-39,111);yygc++; 
					yyg[yygc] = new YYARec(-37,112);yygc++; 
					yyg[yygc] = new YYARec(-35,113);yygc++; 
					yyg[yygc] = new YYARec(-34,114);yygc++; 
					yyg[yygc] = new YYARec(-33,115);yygc++; 
					yyg[yygc] = new YYARec(-32,116);yygc++; 
					yyg[yygc] = new YYARec(-31,117);yygc++; 
					yyg[yygc] = new YYARec(-30,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-28,160);yygc++; 
					yyg[yygc] = new YYARec(-20,121);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-26,81);yygc++; 
					yyg[yygc] = new YYARec(-25,82);yygc++; 
					yyg[yygc] = new YYARec(-24,83);yygc++; 
					yyg[yygc] = new YYARec(-22,84);yygc++; 
					yyg[yygc] = new YYARec(-15,161);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-11,87);yygc++; 
					yyg[yygc] = new YYARec(-46,106);yygc++; 
					yyg[yygc] = new YYARec(-45,107);yygc++; 
					yyg[yygc] = new YYARec(-44,108);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-41,110);yygc++; 
					yyg[yygc] = new YYARec(-39,111);yygc++; 
					yyg[yygc] = new YYARec(-37,112);yygc++; 
					yyg[yygc] = new YYARec(-35,113);yygc++; 
					yyg[yygc] = new YYARec(-34,114);yygc++; 
					yyg[yygc] = new YYARec(-33,115);yygc++; 
					yyg[yygc] = new YYARec(-32,116);yygc++; 
					yyg[yygc] = new YYARec(-31,117);yygc++; 
					yyg[yygc] = new YYARec(-30,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-28,164);yygc++; 
					yyg[yygc] = new YYARec(-20,121);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-26,81);yygc++; 
					yyg[yygc] = new YYARec(-25,82);yygc++; 
					yyg[yygc] = new YYARec(-24,83);yygc++; 
					yyg[yygc] = new YYARec(-22,84);yygc++; 
					yyg[yygc] = new YYARec(-15,165);yygc++; 
					yyg[yygc] = new YYARec(-14,166);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-11,87);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-26,81);yygc++; 
					yyg[yygc] = new YYARec(-25,82);yygc++; 
					yyg[yygc] = new YYARec(-24,83);yygc++; 
					yyg[yygc] = new YYARec(-22,84);yygc++; 
					yyg[yygc] = new YYARec(-15,165);yygc++; 
					yyg[yygc] = new YYARec(-14,167);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-11,87);yygc++; 
					yyg[yygc] = new YYARec(-46,106);yygc++; 
					yyg[yygc] = new YYARec(-45,107);yygc++; 
					yyg[yygc] = new YYARec(-44,108);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-41,110);yygc++; 
					yyg[yygc] = new YYARec(-39,111);yygc++; 
					yyg[yygc] = new YYARec(-37,112);yygc++; 
					yyg[yygc] = new YYARec(-35,113);yygc++; 
					yyg[yygc] = new YYARec(-34,114);yygc++; 
					yyg[yygc] = new YYARec(-33,115);yygc++; 
					yyg[yygc] = new YYARec(-32,116);yygc++; 
					yyg[yygc] = new YYARec(-31,117);yygc++; 
					yyg[yygc] = new YYARec(-30,118);yygc++; 
					yyg[yygc] = new YYARec(-29,119);yygc++; 
					yyg[yygc] = new YYARec(-28,168);yygc++; 
					yyg[yygc] = new YYARec(-20,121);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-46,106);yygc++; 
					yyg[yygc] = new YYARec(-45,107);yygc++; 
					yyg[yygc] = new YYARec(-44,108);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-41,169);yygc++; 
					yyg[yygc] = new YYARec(-20,121);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-46,106);yygc++; 
					yyg[yygc] = new YYARec(-45,107);yygc++; 
					yyg[yygc] = new YYARec(-44,108);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-41,110);yygc++; 
					yyg[yygc] = new YYARec(-39,170);yygc++; 
					yyg[yygc] = new YYARec(-20,121);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-46,106);yygc++; 
					yyg[yygc] = new YYARec(-45,107);yygc++; 
					yyg[yygc] = new YYARec(-44,108);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-41,110);yygc++; 
					yyg[yygc] = new YYARec(-39,111);yygc++; 
					yyg[yygc] = new YYARec(-37,171);yygc++; 
					yyg[yygc] = new YYARec(-20,121);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-46,106);yygc++; 
					yyg[yygc] = new YYARec(-45,107);yygc++; 
					yyg[yygc] = new YYARec(-44,108);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-41,110);yygc++; 
					yyg[yygc] = new YYARec(-39,111);yygc++; 
					yyg[yygc] = new YYARec(-37,112);yygc++; 
					yyg[yygc] = new YYARec(-35,172);yygc++; 
					yyg[yygc] = new YYARec(-20,121);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-46,106);yygc++; 
					yyg[yygc] = new YYARec(-45,107);yygc++; 
					yyg[yygc] = new YYARec(-44,108);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-41,110);yygc++; 
					yyg[yygc] = new YYARec(-39,111);yygc++; 
					yyg[yygc] = new YYARec(-37,112);yygc++; 
					yyg[yygc] = new YYARec(-35,113);yygc++; 
					yyg[yygc] = new YYARec(-34,173);yygc++; 
					yyg[yygc] = new YYARec(-20,121);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-46,106);yygc++; 
					yyg[yygc] = new YYARec(-45,107);yygc++; 
					yyg[yygc] = new YYARec(-44,108);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-41,110);yygc++; 
					yyg[yygc] = new YYARec(-39,111);yygc++; 
					yyg[yygc] = new YYARec(-37,112);yygc++; 
					yyg[yygc] = new YYARec(-35,113);yygc++; 
					yyg[yygc] = new YYARec(-34,114);yygc++; 
					yyg[yygc] = new YYARec(-33,174);yygc++; 
					yyg[yygc] = new YYARec(-20,121);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-46,106);yygc++; 
					yyg[yygc] = new YYARec(-45,107);yygc++; 
					yyg[yygc] = new YYARec(-44,108);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-41,110);yygc++; 
					yyg[yygc] = new YYARec(-39,111);yygc++; 
					yyg[yygc] = new YYARec(-37,112);yygc++; 
					yyg[yygc] = new YYARec(-35,113);yygc++; 
					yyg[yygc] = new YYARec(-34,114);yygc++; 
					yyg[yygc] = new YYARec(-33,115);yygc++; 
					yyg[yygc] = new YYARec(-32,175);yygc++; 
					yyg[yygc] = new YYARec(-20,121);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-46,106);yygc++; 
					yyg[yygc] = new YYARec(-45,107);yygc++; 
					yyg[yygc] = new YYARec(-44,108);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-41,110);yygc++; 
					yyg[yygc] = new YYARec(-39,111);yygc++; 
					yyg[yygc] = new YYARec(-37,112);yygc++; 
					yyg[yygc] = new YYARec(-35,113);yygc++; 
					yyg[yygc] = new YYARec(-34,114);yygc++; 
					yyg[yygc] = new YYARec(-33,115);yygc++; 
					yyg[yygc] = new YYARec(-32,116);yygc++; 
					yyg[yygc] = new YYARec(-31,176);yygc++; 
					yyg[yygc] = new YYARec(-20,121);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-46,106);yygc++; 
					yyg[yygc] = new YYARec(-45,107);yygc++; 
					yyg[yygc] = new YYARec(-44,108);yygc++; 
					yyg[yygc] = new YYARec(-43,109);yygc++; 
					yyg[yygc] = new YYARec(-41,110);yygc++; 
					yyg[yygc] = new YYARec(-39,111);yygc++; 
					yyg[yygc] = new YYARec(-37,112);yygc++; 
					yyg[yygc] = new YYARec(-35,113);yygc++; 
					yyg[yygc] = new YYARec(-34,114);yygc++; 
					yyg[yygc] = new YYARec(-33,115);yygc++; 
					yyg[yygc] = new YYARec(-32,116);yygc++; 
					yyg[yygc] = new YYARec(-31,117);yygc++; 
					yyg[yygc] = new YYARec(-30,177);yygc++; 
					yyg[yygc] = new YYARec(-20,121);yygc++; 
					yyg[yygc] = new YYARec(-11,63);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-26,81);yygc++; 
					yyg[yygc] = new YYARec(-25,82);yygc++; 
					yyg[yygc] = new YYARec(-24,83);yygc++; 
					yyg[yygc] = new YYARec(-22,84);yygc++; 
					yyg[yygc] = new YYARec(-15,178);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-11,87);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-26,81);yygc++; 
					yyg[yygc] = new YYARec(-25,82);yygc++; 
					yyg[yygc] = new YYARec(-24,83);yygc++; 
					yyg[yygc] = new YYARec(-22,84);yygc++; 
					yyg[yygc] = new YYARec(-15,181);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-11,87);yygc++; 
					yyg[yygc] = new YYARec(-42,139);yygc++; 
					yyg[yygc] = new YYARec(-40,143);yygc++; 
					yyg[yygc] = new YYARec(-38,146);yygc++; 
					yyg[yygc] = new YYARec(-36,151);yygc++; 
					yyg[yygc] = new YYARec(-45,1);yygc++; 
					yyg[yygc] = new YYARec(-26,81);yygc++; 
					yyg[yygc] = new YYARec(-25,82);yygc++; 
					yyg[yygc] = new YYARec(-24,83);yygc++; 
					yyg[yygc] = new YYARec(-22,84);yygc++; 
					yyg[yygc] = new YYARec(-15,187);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-11,87);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = -116;  
					yyd[2] = 0;  
					yyd[3] = -9;  
					yyd[4] = -8;  
					yyd[5] = -7;  
					yyd[6] = -6;  
					yyd[7] = -5;  
					yyd[8] = -4;  
					yyd[9] = 0;  
					yyd[10] = -1;  
					yyd[11] = 0;  
					yyd[12] = 0;  
					yyd[13] = 0;  
					yyd[14] = 0;  
					yyd[15] = 0;  
					yyd[16] = 0;  
					yyd[17] = -92;  
					yyd[18] = -93;  
					yyd[19] = -94;  
					yyd[20] = -95;  
					yyd[21] = -96;  
					yyd[22] = -97;  
					yyd[23] = -98;  
					yyd[24] = -99;  
					yyd[25] = -100;  
					yyd[26] = -101;  
					yyd[27] = -102;  
					yyd[28] = -103;  
					yyd[29] = -104;  
					yyd[30] = -105;  
					yyd[31] = -115;  
					yyd[32] = 0;  
					yyd[33] = 0;  
					yyd[34] = -25;  
					yyd[35] = -24;  
					yyd[36] = -23;  
					yyd[37] = 0;  
					yyd[38] = -26;  
					yyd[39] = 0;  
					yyd[40] = -84;  
					yyd[41] = -85;  
					yyd[42] = -83;  
					yyd[43] = -110;  
					yyd[44] = -113;  
					yyd[45] = -117;  
					yyd[46] = -118;  
					yyd[47] = -2;  
					yyd[48] = 0;  
					yyd[49] = 0;  
					yyd[50] = 0;  
					yyd[51] = 0;  
					yyd[52] = 0;  
					yyd[53] = -112;  
					yyd[54] = -27;  
					yyd[55] = -22;  
					yyd[56] = -38;  
					yyd[57] = 0;  
					yyd[58] = -43;  
					yyd[59] = -44;  
					yyd[60] = -42;  
					yyd[61] = -41;  
					yyd[62] = 0;  
					yyd[63] = 0;  
					yyd[64] = 0;  
					yyd[65] = 0;  
					yyd[66] = 0;  
					yyd[67] = 0;  
					yyd[68] = -19;  
					yyd[69] = 0;  
					yyd[70] = -20;  
					yyd[71] = -21;  
					yyd[72] = 0;  
					yyd[73] = 0;  
					yyd[74] = -45;  
					yyd[75] = -37;  
					yyd[76] = -109;  
					yyd[77] = -10;  
					yyd[78] = 0;  
					yyd[79] = -11;  
					yyd[80] = 0;  
					yyd[81] = -34;  
					yyd[82] = 0;  
					yyd[83] = 0;  
					yyd[84] = -35;  
					yyd[85] = 0;  
					yyd[86] = 0;  
					yyd[87] = 0;  
					yyd[88] = 0;  
					yyd[89] = 0;  
					yyd[90] = 0;  
					yyd[91] = 0;  
					yyd[92] = 0;  
					yyd[93] = -40;  
					yyd[94] = 0;  
					yyd[95] = -13;  
					yyd[96] = -18;  
					yyd[97] = -31;  
					yyd[98] = 0;  
					yyd[99] = -28;  
					yyd[100] = -32;  
					yyd[101] = 0;  
					yyd[102] = 0;  
					yyd[103] = 0;  
					yyd[104] = 0;  
					yyd[105] = 0;  
					yyd[106] = -70;  
					yyd[107] = 0;  
					yyd[108] = 0;  
					yyd[109] = -66;  
					yyd[110] = -64;  
					yyd[111] = 0;  
					yyd[112] = 0;  
					yyd[113] = 0;  
					yyd[114] = 0;  
					yyd[115] = 0;  
					yyd[116] = 0;  
					yyd[117] = 0;  
					yyd[118] = 0;  
					yyd[119] = 0;  
					yyd[120] = 0;  
					yyd[121] = -71;  
					yyd[122] = 0;  
					yyd[123] = -114;  
					yyd[124] = 0;  
					yyd[125] = 0;  
					yyd[126] = 0;  
					yyd[127] = -30;  
					yyd[128] = 0;  
					yyd[129] = -87;  
					yyd[130] = -88;  
					yyd[131] = -89;  
					yyd[132] = -90;  
					yyd[133] = -91;  
					yyd[134] = -29;  
					yyd[135] = 0;  
					yyd[136] = 0;  
					yyd[137] = 0;  
					yyd[138] = -67;  
					yyd[139] = 0;  
					yyd[140] = -80;  
					yyd[141] = -81;  
					yyd[142] = -82;  
					yyd[143] = 0;  
					yyd[144] = -78;  
					yyd[145] = -79;  
					yyd[146] = 0;  
					yyd[147] = -74;  
					yyd[148] = -75;  
					yyd[149] = -76;  
					yyd[150] = -77;  
					yyd[151] = 0;  
					yyd[152] = -72;  
					yyd[153] = -73;  
					yyd[154] = 0;  
					yyd[155] = 0;  
					yyd[156] = 0;  
					yyd[157] = 0;  
					yyd[158] = 0;  
					yyd[159] = 0;  
					yyd[160] = 0;  
					yyd[161] = 0;  
					yyd[162] = 0;  
					yyd[163] = -12;  
					yyd[164] = -86;  
					yyd[165] = 0;  
					yyd[166] = -14;  
					yyd[167] = -15;  
					yyd[168] = 0;  
					yyd[169] = -65;  
					yyd[170] = 0;  
					yyd[171] = 0;  
					yyd[172] = 0;  
					yyd[173] = 0;  
					yyd[174] = 0;  
					yyd[175] = 0;  
					yyd[176] = 0;  
					yyd[177] = 0;  
					yyd[178] = 0;  
					yyd[179] = -69;  
					yyd[180] = -106;  
					yyd[181] = 0;  
					yyd[182] = 0;  
					yyd[183] = -17;  
					yyd[184] = -68;  
					yyd[185] = -107;  
					yyd[186] = -108;  
					yyd[187] = 0;  
					yyd[188] = -16; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 22;  
					yyal[2] = 22;  
					yyal[3] = 44;  
					yyal[4] = 44;  
					yyal[5] = 44;  
					yyal[6] = 44;  
					yyal[7] = 44;  
					yyal[8] = 44;  
					yyal[9] = 44;  
					yyal[10] = 67;  
					yyal[11] = 67;  
					yyal[12] = 68;  
					yyal[13] = 83;  
					yyal[14] = 98;  
					yyal[15] = 113;  
					yyal[16] = 128;  
					yyal[17] = 129;  
					yyal[18] = 129;  
					yyal[19] = 129;  
					yyal[20] = 129;  
					yyal[21] = 129;  
					yyal[22] = 129;  
					yyal[23] = 129;  
					yyal[24] = 129;  
					yyal[25] = 129;  
					yyal[26] = 129;  
					yyal[27] = 129;  
					yyal[28] = 129;  
					yyal[29] = 129;  
					yyal[30] = 129;  
					yyal[31] = 129;  
					yyal[32] = 129;  
					yyal[33] = 130;  
					yyal[34] = 131;  
					yyal[35] = 131;  
					yyal[36] = 131;  
					yyal[37] = 131;  
					yyal[38] = 132;  
					yyal[39] = 132;  
					yyal[40] = 158;  
					yyal[41] = 158;  
					yyal[42] = 158;  
					yyal[43] = 158;  
					yyal[44] = 158;  
					yyal[45] = 158;  
					yyal[46] = 158;  
					yyal[47] = 158;  
					yyal[48] = 158;  
					yyal[49] = 159;  
					yyal[50] = 160;  
					yyal[51] = 162;  
					yyal[52] = 163;  
					yyal[53] = 164;  
					yyal[54] = 164;  
					yyal[55] = 164;  
					yyal[56] = 164;  
					yyal[57] = 164;  
					yyal[58] = 165;  
					yyal[59] = 165;  
					yyal[60] = 165;  
					yyal[61] = 165;  
					yyal[62] = 165;  
					yyal[63] = 189;  
					yyal[64] = 235;  
					yyal[65] = 258;  
					yyal[66] = 273;  
					yyal[67] = 295;  
					yyal[68] = 317;  
					yyal[69] = 317;  
					yyal[70] = 339;  
					yyal[71] = 339;  
					yyal[72] = 339;  
					yyal[73] = 360;  
					yyal[74] = 382;  
					yyal[75] = 382;  
					yyal[76] = 382;  
					yyal[77] = 382;  
					yyal[78] = 382;  
					yyal[79] = 384;  
					yyal[80] = 384;  
					yyal[81] = 385;  
					yyal[82] = 385;  
					yyal[83] = 408;  
					yyal[84] = 409;  
					yyal[85] = 409;  
					yyal[86] = 410;  
					yyal[87] = 433;  
					yyal[88] = 458;  
					yyal[89] = 473;  
					yyal[90] = 488;  
					yyal[91] = 509;  
					yyal[92] = 510;  
					yyal[93] = 531;  
					yyal[94] = 531;  
					yyal[95] = 552;  
					yyal[96] = 552;  
					yyal[97] = 552;  
					yyal[98] = 552;  
					yyal[99] = 575;  
					yyal[100] = 575;  
					yyal[101] = 575;  
					yyal[102] = 604;  
					yyal[103] = 626;  
					yyal[104] = 649;  
					yyal[105] = 650;  
					yyal[106] = 651;  
					yyal[107] = 651;  
					yyal[108] = 672;  
					yyal[109] = 693;  
					yyal[110] = 693;  
					yyal[111] = 693;  
					yyal[112] = 712;  
					yyal[113] = 728;  
					yyal[114] = 742;  
					yyal[115] = 752;  
					yyal[116] = 760;  
					yyal[117] = 767;  
					yyal[118] = 773;  
					yyal[119] = 778;  
					yyal[120] = 782;  
					yyal[121] = 783;  
					yyal[122] = 783;  
					yyal[123] = 804;  
					yyal[124] = 804;  
					yyal[125] = 825;  
					yyal[126] = 826;  
					yyal[127] = 827;  
					yyal[128] = 827;  
					yyal[129] = 848;  
					yyal[130] = 848;  
					yyal[131] = 848;  
					yyal[132] = 848;  
					yyal[133] = 848;  
					yyal[134] = 848;  
					yyal[135] = 848;  
					yyal[136] = 870;  
					yyal[137] = 892;  
					yyal[138] = 913;  
					yyal[139] = 913;  
					yyal[140] = 934;  
					yyal[141] = 934;  
					yyal[142] = 934;  
					yyal[143] = 934;  
					yyal[144] = 955;  
					yyal[145] = 955;  
					yyal[146] = 955;  
					yyal[147] = 976;  
					yyal[148] = 976;  
					yyal[149] = 976;  
					yyal[150] = 976;  
					yyal[151] = 976;  
					yyal[152] = 997;  
					yyal[153] = 997;  
					yyal[154] = 997;  
					yyal[155] = 1018;  
					yyal[156] = 1039;  
					yyal[157] = 1060;  
					yyal[158] = 1081;  
					yyal[159] = 1102;  
					yyal[160] = 1123;  
					yyal[161] = 1124;  
					yyal[162] = 1125;  
					yyal[163] = 1146;  
					yyal[164] = 1146;  
					yyal[165] = 1146;  
					yyal[166] = 1148;  
					yyal[167] = 1148;  
					yyal[168] = 1148;  
					yyal[169] = 1149;  
					yyal[170] = 1149;  
					yyal[171] = 1168;  
					yyal[172] = 1184;  
					yyal[173] = 1198;  
					yyal[174] = 1208;  
					yyal[175] = 1216;  
					yyal[176] = 1223;  
					yyal[177] = 1229;  
					yyal[178] = 1234;  
					yyal[179] = 1235;  
					yyal[180] = 1235;  
					yyal[181] = 1235;  
					yyal[182] = 1236;  
					yyal[183] = 1257;  
					yyal[184] = 1257;  
					yyal[185] = 1257;  
					yyal[186] = 1257;  
					yyal[187] = 1257;  
					yyal[188] = 1258; 

					yyah = new int[yynstates];
					yyah[0] = 21;  
					yyah[1] = 21;  
					yyah[2] = 43;  
					yyah[3] = 43;  
					yyah[4] = 43;  
					yyah[5] = 43;  
					yyah[6] = 43;  
					yyah[7] = 43;  
					yyah[8] = 43;  
					yyah[9] = 66;  
					yyah[10] = 66;  
					yyah[11] = 67;  
					yyah[12] = 82;  
					yyah[13] = 97;  
					yyah[14] = 112;  
					yyah[15] = 127;  
					yyah[16] = 128;  
					yyah[17] = 128;  
					yyah[18] = 128;  
					yyah[19] = 128;  
					yyah[20] = 128;  
					yyah[21] = 128;  
					yyah[22] = 128;  
					yyah[23] = 128;  
					yyah[24] = 128;  
					yyah[25] = 128;  
					yyah[26] = 128;  
					yyah[27] = 128;  
					yyah[28] = 128;  
					yyah[29] = 128;  
					yyah[30] = 128;  
					yyah[31] = 128;  
					yyah[32] = 129;  
					yyah[33] = 130;  
					yyah[34] = 130;  
					yyah[35] = 130;  
					yyah[36] = 130;  
					yyah[37] = 131;  
					yyah[38] = 131;  
					yyah[39] = 157;  
					yyah[40] = 157;  
					yyah[41] = 157;  
					yyah[42] = 157;  
					yyah[43] = 157;  
					yyah[44] = 157;  
					yyah[45] = 157;  
					yyah[46] = 157;  
					yyah[47] = 157;  
					yyah[48] = 158;  
					yyah[49] = 159;  
					yyah[50] = 161;  
					yyah[51] = 162;  
					yyah[52] = 163;  
					yyah[53] = 163;  
					yyah[54] = 163;  
					yyah[55] = 163;  
					yyah[56] = 163;  
					yyah[57] = 164;  
					yyah[58] = 164;  
					yyah[59] = 164;  
					yyah[60] = 164;  
					yyah[61] = 164;  
					yyah[62] = 188;  
					yyah[63] = 234;  
					yyah[64] = 257;  
					yyah[65] = 272;  
					yyah[66] = 294;  
					yyah[67] = 316;  
					yyah[68] = 316;  
					yyah[69] = 338;  
					yyah[70] = 338;  
					yyah[71] = 338;  
					yyah[72] = 359;  
					yyah[73] = 381;  
					yyah[74] = 381;  
					yyah[75] = 381;  
					yyah[76] = 381;  
					yyah[77] = 381;  
					yyah[78] = 383;  
					yyah[79] = 383;  
					yyah[80] = 384;  
					yyah[81] = 384;  
					yyah[82] = 407;  
					yyah[83] = 408;  
					yyah[84] = 408;  
					yyah[85] = 409;  
					yyah[86] = 432;  
					yyah[87] = 457;  
					yyah[88] = 472;  
					yyah[89] = 487;  
					yyah[90] = 508;  
					yyah[91] = 509;  
					yyah[92] = 530;  
					yyah[93] = 530;  
					yyah[94] = 551;  
					yyah[95] = 551;  
					yyah[96] = 551;  
					yyah[97] = 551;  
					yyah[98] = 574;  
					yyah[99] = 574;  
					yyah[100] = 574;  
					yyah[101] = 603;  
					yyah[102] = 625;  
					yyah[103] = 648;  
					yyah[104] = 649;  
					yyah[105] = 650;  
					yyah[106] = 650;  
					yyah[107] = 671;  
					yyah[108] = 692;  
					yyah[109] = 692;  
					yyah[110] = 692;  
					yyah[111] = 711;  
					yyah[112] = 727;  
					yyah[113] = 741;  
					yyah[114] = 751;  
					yyah[115] = 759;  
					yyah[116] = 766;  
					yyah[117] = 772;  
					yyah[118] = 777;  
					yyah[119] = 781;  
					yyah[120] = 782;  
					yyah[121] = 782;  
					yyah[122] = 803;  
					yyah[123] = 803;  
					yyah[124] = 824;  
					yyah[125] = 825;  
					yyah[126] = 826;  
					yyah[127] = 826;  
					yyah[128] = 847;  
					yyah[129] = 847;  
					yyah[130] = 847;  
					yyah[131] = 847;  
					yyah[132] = 847;  
					yyah[133] = 847;  
					yyah[134] = 847;  
					yyah[135] = 869;  
					yyah[136] = 891;  
					yyah[137] = 912;  
					yyah[138] = 912;  
					yyah[139] = 933;  
					yyah[140] = 933;  
					yyah[141] = 933;  
					yyah[142] = 933;  
					yyah[143] = 954;  
					yyah[144] = 954;  
					yyah[145] = 954;  
					yyah[146] = 975;  
					yyah[147] = 975;  
					yyah[148] = 975;  
					yyah[149] = 975;  
					yyah[150] = 975;  
					yyah[151] = 996;  
					yyah[152] = 996;  
					yyah[153] = 996;  
					yyah[154] = 1017;  
					yyah[155] = 1038;  
					yyah[156] = 1059;  
					yyah[157] = 1080;  
					yyah[158] = 1101;  
					yyah[159] = 1122;  
					yyah[160] = 1123;  
					yyah[161] = 1124;  
					yyah[162] = 1145;  
					yyah[163] = 1145;  
					yyah[164] = 1145;  
					yyah[165] = 1147;  
					yyah[166] = 1147;  
					yyah[167] = 1147;  
					yyah[168] = 1148;  
					yyah[169] = 1148;  
					yyah[170] = 1167;  
					yyah[171] = 1183;  
					yyah[172] = 1197;  
					yyah[173] = 1207;  
					yyah[174] = 1215;  
					yyah[175] = 1222;  
					yyah[176] = 1228;  
					yyah[177] = 1233;  
					yyah[178] = 1234;  
					yyah[179] = 1234;  
					yyah[180] = 1234;  
					yyah[181] = 1235;  
					yyah[182] = 1256;  
					yyah[183] = 1256;  
					yyah[184] = 1256;  
					yyah[185] = 1256;  
					yyah[186] = 1256;  
					yyah[187] = 1257;  
					yyah[188] = 1257; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 12;  
					yygl[2] = 12;  
					yygl[3] = 21;  
					yygl[4] = 21;  
					yygl[5] = 21;  
					yygl[6] = 21;  
					yygl[7] = 21;  
					yygl[8] = 21;  
					yygl[9] = 21;  
					yygl[10] = 31;  
					yygl[11] = 31;  
					yygl[12] = 31;  
					yygl[13] = 33;  
					yygl[14] = 35;  
					yygl[15] = 37;  
					yygl[16] = 39;  
					yygl[17] = 40;  
					yygl[18] = 40;  
					yygl[19] = 40;  
					yygl[20] = 40;  
					yygl[21] = 40;  
					yygl[22] = 40;  
					yygl[23] = 40;  
					yygl[24] = 40;  
					yygl[25] = 40;  
					yygl[26] = 40;  
					yygl[27] = 40;  
					yygl[28] = 40;  
					yygl[29] = 40;  
					yygl[30] = 40;  
					yygl[31] = 40;  
					yygl[32] = 40;  
					yygl[33] = 40;  
					yygl[34] = 40;  
					yygl[35] = 40;  
					yygl[36] = 40;  
					yygl[37] = 40;  
					yygl[38] = 40;  
					yygl[39] = 40;  
					yygl[40] = 50;  
					yygl[41] = 50;  
					yygl[42] = 50;  
					yygl[43] = 50;  
					yygl[44] = 50;  
					yygl[45] = 50;  
					yygl[46] = 50;  
					yygl[47] = 50;  
					yygl[48] = 50;  
					yygl[49] = 50;  
					yygl[50] = 50;  
					yygl[51] = 50;  
					yygl[52] = 50;  
					yygl[53] = 50;  
					yygl[54] = 50;  
					yygl[55] = 50;  
					yygl[56] = 50;  
					yygl[57] = 50;  
					yygl[58] = 50;  
					yygl[59] = 50;  
					yygl[60] = 50;  
					yygl[61] = 50;  
					yygl[62] = 50;  
					yygl[63] = 51;  
					yygl[64] = 51;  
					yygl[65] = 60;  
					yygl[66] = 62;  
					yygl[67] = 73;  
					yygl[68] = 84;  
					yygl[69] = 84;  
					yygl[70] = 92;  
					yygl[71] = 92;  
					yygl[72] = 92;  
					yygl[73] = 100;  
					yygl[74] = 109;  
					yygl[75] = 109;  
					yygl[76] = 109;  
					yygl[77] = 109;  
					yygl[78] = 109;  
					yygl[79] = 109;  
					yygl[80] = 109;  
					yygl[81] = 109;  
					yygl[82] = 109;  
					yygl[83] = 117;  
					yygl[84] = 117;  
					yygl[85] = 117;  
					yygl[86] = 117;  
					yygl[87] = 125;  
					yygl[88] = 134;  
					yygl[89] = 136;  
					yygl[90] = 138;  
					yygl[91] = 155;  
					yygl[92] = 155;  
					yygl[93] = 172;  
					yygl[94] = 172;  
					yygl[95] = 182;  
					yygl[96] = 182;  
					yygl[97] = 182;  
					yygl[98] = 182;  
					yygl[99] = 190;  
					yygl[100] = 190;  
					yygl[101] = 190;  
					yygl[102] = 191;  
					yygl[103] = 200;  
					yygl[104] = 208;  
					yygl[105] = 208;  
					yygl[106] = 208;  
					yygl[107] = 208;  
					yygl[108] = 208;  
					yygl[109] = 215;  
					yygl[110] = 215;  
					yygl[111] = 215;  
					yygl[112] = 216;  
					yygl[113] = 217;  
					yygl[114] = 218;  
					yygl[115] = 219;  
					yygl[116] = 219;  
					yygl[117] = 219;  
					yygl[118] = 219;  
					yygl[119] = 219;  
					yygl[120] = 219;  
					yygl[121] = 219;  
					yygl[122] = 219;  
					yygl[123] = 236;  
					yygl[124] = 236;  
					yygl[125] = 244;  
					yygl[126] = 244;  
					yygl[127] = 244;  
					yygl[128] = 244;  
					yygl[129] = 261;  
					yygl[130] = 261;  
					yygl[131] = 261;  
					yygl[132] = 261;  
					yygl[133] = 261;  
					yygl[134] = 261;  
					yygl[135] = 261;  
					yygl[136] = 270;  
					yygl[137] = 279;  
					yygl[138] = 296;  
					yygl[139] = 296;  
					yygl[140] = 303;  
					yygl[141] = 303;  
					yygl[142] = 303;  
					yygl[143] = 303;  
					yygl[144] = 311;  
					yygl[145] = 311;  
					yygl[146] = 311;  
					yygl[147] = 320;  
					yygl[148] = 320;  
					yygl[149] = 320;  
					yygl[150] = 320;  
					yygl[151] = 320;  
					yygl[152] = 330;  
					yygl[153] = 330;  
					yygl[154] = 330;  
					yygl[155] = 341;  
					yygl[156] = 353;  
					yygl[157] = 366;  
					yygl[158] = 380;  
					yygl[159] = 395;  
					yygl[160] = 403;  
					yygl[161] = 403;  
					yygl[162] = 403;  
					yygl[163] = 411;  
					yygl[164] = 411;  
					yygl[165] = 411;  
					yygl[166] = 411;  
					yygl[167] = 411;  
					yygl[168] = 411;  
					yygl[169] = 411;  
					yygl[170] = 411;  
					yygl[171] = 412;  
					yygl[172] = 413;  
					yygl[173] = 414;  
					yygl[174] = 415;  
					yygl[175] = 415;  
					yygl[176] = 415;  
					yygl[177] = 415;  
					yygl[178] = 415;  
					yygl[179] = 415;  
					yygl[180] = 415;  
					yygl[181] = 415;  
					yygl[182] = 415;  
					yygl[183] = 423;  
					yygl[184] = 423;  
					yygl[185] = 423;  
					yygl[186] = 423;  
					yygl[187] = 423;  
					yygl[188] = 423; 

					yygh = new int[yynstates];
					yygh[0] = 11;  
					yygh[1] = 11;  
					yygh[2] = 20;  
					yygh[3] = 20;  
					yygh[4] = 20;  
					yygh[5] = 20;  
					yygh[6] = 20;  
					yygh[7] = 20;  
					yygh[8] = 20;  
					yygh[9] = 30;  
					yygh[10] = 30;  
					yygh[11] = 30;  
					yygh[12] = 32;  
					yygh[13] = 34;  
					yygh[14] = 36;  
					yygh[15] = 38;  
					yygh[16] = 39;  
					yygh[17] = 39;  
					yygh[18] = 39;  
					yygh[19] = 39;  
					yygh[20] = 39;  
					yygh[21] = 39;  
					yygh[22] = 39;  
					yygh[23] = 39;  
					yygh[24] = 39;  
					yygh[25] = 39;  
					yygh[26] = 39;  
					yygh[27] = 39;  
					yygh[28] = 39;  
					yygh[29] = 39;  
					yygh[30] = 39;  
					yygh[31] = 39;  
					yygh[32] = 39;  
					yygh[33] = 39;  
					yygh[34] = 39;  
					yygh[35] = 39;  
					yygh[36] = 39;  
					yygh[37] = 39;  
					yygh[38] = 39;  
					yygh[39] = 49;  
					yygh[40] = 49;  
					yygh[41] = 49;  
					yygh[42] = 49;  
					yygh[43] = 49;  
					yygh[44] = 49;  
					yygh[45] = 49;  
					yygh[46] = 49;  
					yygh[47] = 49;  
					yygh[48] = 49;  
					yygh[49] = 49;  
					yygh[50] = 49;  
					yygh[51] = 49;  
					yygh[52] = 49;  
					yygh[53] = 49;  
					yygh[54] = 49;  
					yygh[55] = 49;  
					yygh[56] = 49;  
					yygh[57] = 49;  
					yygh[58] = 49;  
					yygh[59] = 49;  
					yygh[60] = 49;  
					yygh[61] = 49;  
					yygh[62] = 50;  
					yygh[63] = 50;  
					yygh[64] = 59;  
					yygh[65] = 61;  
					yygh[66] = 72;  
					yygh[67] = 83;  
					yygh[68] = 83;  
					yygh[69] = 91;  
					yygh[70] = 91;  
					yygh[71] = 91;  
					yygh[72] = 99;  
					yygh[73] = 108;  
					yygh[74] = 108;  
					yygh[75] = 108;  
					yygh[76] = 108;  
					yygh[77] = 108;  
					yygh[78] = 108;  
					yygh[79] = 108;  
					yygh[80] = 108;  
					yygh[81] = 108;  
					yygh[82] = 116;  
					yygh[83] = 116;  
					yygh[84] = 116;  
					yygh[85] = 116;  
					yygh[86] = 124;  
					yygh[87] = 133;  
					yygh[88] = 135;  
					yygh[89] = 137;  
					yygh[90] = 154;  
					yygh[91] = 154;  
					yygh[92] = 171;  
					yygh[93] = 171;  
					yygh[94] = 181;  
					yygh[95] = 181;  
					yygh[96] = 181;  
					yygh[97] = 181;  
					yygh[98] = 189;  
					yygh[99] = 189;  
					yygh[100] = 189;  
					yygh[101] = 190;  
					yygh[102] = 199;  
					yygh[103] = 207;  
					yygh[104] = 207;  
					yygh[105] = 207;  
					yygh[106] = 207;  
					yygh[107] = 207;  
					yygh[108] = 214;  
					yygh[109] = 214;  
					yygh[110] = 214;  
					yygh[111] = 215;  
					yygh[112] = 216;  
					yygh[113] = 217;  
					yygh[114] = 218;  
					yygh[115] = 218;  
					yygh[116] = 218;  
					yygh[117] = 218;  
					yygh[118] = 218;  
					yygh[119] = 218;  
					yygh[120] = 218;  
					yygh[121] = 218;  
					yygh[122] = 235;  
					yygh[123] = 235;  
					yygh[124] = 243;  
					yygh[125] = 243;  
					yygh[126] = 243;  
					yygh[127] = 243;  
					yygh[128] = 260;  
					yygh[129] = 260;  
					yygh[130] = 260;  
					yygh[131] = 260;  
					yygh[132] = 260;  
					yygh[133] = 260;  
					yygh[134] = 260;  
					yygh[135] = 269;  
					yygh[136] = 278;  
					yygh[137] = 295;  
					yygh[138] = 295;  
					yygh[139] = 302;  
					yygh[140] = 302;  
					yygh[141] = 302;  
					yygh[142] = 302;  
					yygh[143] = 310;  
					yygh[144] = 310;  
					yygh[145] = 310;  
					yygh[146] = 319;  
					yygh[147] = 319;  
					yygh[148] = 319;  
					yygh[149] = 319;  
					yygh[150] = 319;  
					yygh[151] = 329;  
					yygh[152] = 329;  
					yygh[153] = 329;  
					yygh[154] = 340;  
					yygh[155] = 352;  
					yygh[156] = 365;  
					yygh[157] = 379;  
					yygh[158] = 394;  
					yygh[159] = 402;  
					yygh[160] = 402;  
					yygh[161] = 402;  
					yygh[162] = 410;  
					yygh[163] = 410;  
					yygh[164] = 410;  
					yygh[165] = 410;  
					yygh[166] = 410;  
					yygh[167] = 410;  
					yygh[168] = 410;  
					yygh[169] = 410;  
					yygh[170] = 411;  
					yygh[171] = 412;  
					yygh[172] = 413;  
					yygh[173] = 414;  
					yygh[174] = 414;  
					yygh[175] = 414;  
					yygh[176] = 414;  
					yygh[177] = 414;  
					yygh[178] = 414;  
					yygh[179] = 414;  
					yygh[180] = 414;  
					yygh[181] = 414;  
					yygh[182] = 422;  
					yygh[183] = 422;  
					yygh[184] = 422;  
					yygh[185] = 422;  
					yygh[186] = 422;  
					yygh[187] = 422;  
					yygh[188] = 422; 

					yyr[yyrc] = new YYRRec(1,-2);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-23);yyrc++; 
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
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-26);yyrc++; 
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
					yyr[yyrc] = new YYRRec(4,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-11);yyrc++; 
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
