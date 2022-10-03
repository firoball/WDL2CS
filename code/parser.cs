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
                int t_Char43 = 272;
                int t_Char45 = 273;
                int t_Char47 = 274;
                int t_Char94 = 275;
                int t_Char124 = 276;
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
         yyval = yyv[yysp-8] + yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   23 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   24 : 
         yyval = yyv[yysp-0];
         
       break;
							case   25 : 
         yyval = yyv[yysp-0];
         
       break;
							case   26 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   27 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   28 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   43 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   44 : 
         yyval = yyv[yysp-0];
         
       break;
							case   45 : 
         yyval = yyv[yysp-0];
         
       break;
							case   46 : 
         yyval = yyv[yysp-0];
         
       break;
							case   47 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   48 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   49 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   50 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   55 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   56 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   57 : 
         yyval = yyv[yysp-0];
         
       break;
							case   58 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   59 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   60 : 
         yyval = yyv[yysp-0];
         
       break;
							case   61 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-10] + yyv[yysp-9] + yyv[yysp-8] + yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   92 : 
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   93 : 
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   94 : 
         yyval = yyv[yysp-0];
         
       break;
							case   95 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case  111 : 
         yyval = yyv[yysp-0];
         
       break;
							case  112 : 
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

					int yynacts   = 351;
					int yyngotos  = 231;
					int yynstates = 174;
					int yynrules  = 112;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,10);yyac++; 
					yya[yyac] = new YYARec(259,11);yyac++; 
					yya[yyac] = new YYARec(262,12);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(310,21);yyac++; 
					yya[yyac] = new YYARec(311,22);yyac++; 
					yya[yyac] = new YYARec(257,10);yyac++; 
					yya[yyac] = new YYARec(259,11);yyac++; 
					yya[yyac] = new YYARec(262,12);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(0,-1 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(258,27);yyac++; 
					yya[yyac] = new YYARec(263,28);yyac++; 
					yya[yyac] = new YYARec(264,29);yyac++; 
					yya[yyac] = new YYARec(307,30);yyac++; 
					yya[yyac] = new YYARec(258,-106 );yyac++; 
					yya[yyac] = new YYARec(258,31);yyac++; 
					yya[yyac] = new YYARec(258,32);yyac++; 
					yya[yyac] = new YYARec(258,33);yyac++; 
					yya[yyac] = new YYARec(263,34);yyac++; 
					yya[yyac] = new YYARec(310,21);yyac++; 
					yya[yyac] = new YYARec(311,22);yyac++; 
					yya[yyac] = new YYARec(296,52);yyac++; 
					yya[yyac] = new YYARec(298,53);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(308,55);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(262,12);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(262,12);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(258,62);yyac++; 
					yya[yyac] = new YYARec(263,63);yyac++; 
					yya[yyac] = new YYARec(258,-25 );yyac++; 
					yya[yyac] = new YYARec(258,64);yyac++; 
					yya[yyac] = new YYARec(265,65);yyac++; 
					yya[yyac] = new YYARec(266,72);yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(310,21);yyac++; 
					yya[yyac] = new YYARec(311,22);yyac++; 
					yya[yyac] = new YYARec(258,-38 );yyac++; 
					yya[yyac] = new YYARec(267,73);yyac++; 
					yya[yyac] = new YYARec(267,74);yyac++; 
					yya[yyac] = new YYARec(260,75);yyac++; 
					yya[yyac] = new YYARec(261,76);yyac++; 
					yya[yyac] = new YYARec(258,77);yyac++; 
					yya[yyac] = new YYARec(258,78);yyac++; 
					yya[yyac] = new YYARec(307,30);yyac++; 
					yya[yyac] = new YYARec(258,-106 );yyac++; 
					yya[yyac] = new YYARec(263,-106 );yyac++; 
					yya[yyac] = new YYARec(268,-106 );yyac++; 
					yya[yyac] = new YYARec(269,-106 );yyac++; 
					yya[yyac] = new YYARec(270,-106 );yyac++; 
					yya[yyac] = new YYARec(271,-106 );yyac++; 
					yya[yyac] = new YYARec(272,-106 );yyac++; 
					yya[yyac] = new YYARec(273,-106 );yyac++; 
					yya[yyac] = new YYARec(274,-106 );yyac++; 
					yya[yyac] = new YYARec(275,-106 );yyac++; 
					yya[yyac] = new YYARec(276,-106 );yyac++; 
					yya[yyac] = new YYARec(277,-106 );yyac++; 
					yya[yyac] = new YYARec(278,-106 );yyac++; 
					yya[yyac] = new YYARec(279,-106 );yyac++; 
					yya[yyac] = new YYARec(280,-106 );yyac++; 
					yya[yyac] = new YYARec(281,-106 );yyac++; 
					yya[yyac] = new YYARec(299,-106 );yyac++; 
					yya[yyac] = new YYARec(300,-106 );yyac++; 
					yya[yyac] = new YYARec(301,-106 );yyac++; 
					yya[yyac] = new YYARec(302,-106 );yyac++; 
					yya[yyac] = new YYARec(303,-106 );yyac++; 
					yya[yyac] = new YYARec(304,-106 );yyac++; 
					yya[yyac] = new YYARec(305,-106 );yyac++; 
					yya[yyac] = new YYARec(306,-106 );yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(296,52);yyac++; 
					yya[yyac] = new YYARec(298,53);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(263,83);yyac++; 
					yya[yyac] = new YYARec(258,-44 );yyac++; 
					yya[yyac] = new YYARec(263,84);yyac++; 
					yya[yyac] = new YYARec(258,-47 );yyac++; 
					yya[yyac] = new YYARec(263,85);yyac++; 
					yya[yyac] = new YYARec(258,-48 );yyac++; 
					yya[yyac] = new YYARec(277,87);yyac++; 
					yya[yyac] = new YYARec(278,88);yyac++; 
					yya[yyac] = new YYARec(279,89);yyac++; 
					yya[yyac] = new YYARec(280,90);yyac++; 
					yya[yyac] = new YYARec(281,91);yyac++; 
					yya[yyac] = new YYARec(258,-45 );yyac++; 
					yya[yyac] = new YYARec(263,-45 );yyac++; 
					yya[yyac] = new YYARec(296,52);yyac++; 
					yya[yyac] = new YYARec(298,53);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(267,99);yyac++; 
					yya[yyac] = new YYARec(282,100);yyac++; 
					yya[yyac] = new YYARec(283,101);yyac++; 
					yya[yyac] = new YYARec(284,102);yyac++; 
					yya[yyac] = new YYARec(285,103);yyac++; 
					yya[yyac] = new YYARec(286,104);yyac++; 
					yya[yyac] = new YYARec(287,105);yyac++; 
					yya[yyac] = new YYARec(288,106);yyac++; 
					yya[yyac] = new YYARec(289,107);yyac++; 
					yya[yyac] = new YYARec(290,108);yyac++; 
					yya[yyac] = new YYARec(291,109);yyac++; 
					yya[yyac] = new YYARec(292,110);yyac++; 
					yya[yyac] = new YYARec(293,111);yyac++; 
					yya[yyac] = new YYARec(294,112);yyac++; 
					yya[yyac] = new YYARec(295,113);yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(267,99);yyac++; 
					yya[yyac] = new YYARec(282,100);yyac++; 
					yya[yyac] = new YYARec(283,101);yyac++; 
					yya[yyac] = new YYARec(284,102);yyac++; 
					yya[yyac] = new YYARec(285,103);yyac++; 
					yya[yyac] = new YYARec(286,104);yyac++; 
					yya[yyac] = new YYARec(287,105);yyac++; 
					yya[yyac] = new YYARec(288,106);yyac++; 
					yya[yyac] = new YYARec(289,107);yyac++; 
					yya[yyac] = new YYARec(290,108);yyac++; 
					yya[yyac] = new YYARec(291,109);yyac++; 
					yya[yyac] = new YYARec(292,110);yyac++; 
					yya[yyac] = new YYARec(293,111);yyac++; 
					yya[yyac] = new YYARec(294,112);yyac++; 
					yya[yyac] = new YYARec(295,113);yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(262,12);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(263,116);yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(311,22);yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(311,22);yyac++; 
					yya[yyac] = new YYARec(267,99);yyac++; 
					yya[yyac] = new YYARec(282,100);yyac++; 
					yya[yyac] = new YYARec(283,101);yyac++; 
					yya[yyac] = new YYARec(284,102);yyac++; 
					yya[yyac] = new YYARec(285,103);yyac++; 
					yya[yyac] = new YYARec(286,104);yyac++; 
					yya[yyac] = new YYARec(287,105);yyac++; 
					yya[yyac] = new YYARec(288,106);yyac++; 
					yya[yyac] = new YYARec(289,107);yyac++; 
					yya[yyac] = new YYARec(290,108);yyac++; 
					yya[yyac] = new YYARec(291,109);yyac++; 
					yya[yyac] = new YYARec(292,110);yyac++; 
					yya[yyac] = new YYARec(293,111);yyac++; 
					yya[yyac] = new YYARec(294,112);yyac++; 
					yya[yyac] = new YYARec(295,113);yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(268,125);yyac++; 
					yya[yyac] = new YYARec(267,126);yyac++; 
					yya[yyac] = new YYARec(269,129);yyac++; 
					yya[yyac] = new YYARec(270,130);yyac++; 
					yya[yyac] = new YYARec(271,131);yyac++; 
					yya[yyac] = new YYARec(272,132);yyac++; 
					yya[yyac] = new YYARec(273,133);yyac++; 
					yya[yyac] = new YYARec(274,134);yyac++; 
					yya[yyac] = new YYARec(275,135);yyac++; 
					yya[yyac] = new YYARec(276,136);yyac++; 
					yya[yyac] = new YYARec(299,137);yyac++; 
					yya[yyac] = new YYARec(300,138);yyac++; 
					yya[yyac] = new YYARec(301,139);yyac++; 
					yya[yyac] = new YYARec(302,140);yyac++; 
					yya[yyac] = new YYARec(303,141);yyac++; 
					yya[yyac] = new YYARec(304,142);yyac++; 
					yya[yyac] = new YYARec(305,143);yyac++; 
					yya[yyac] = new YYARec(306,144);yyac++; 
					yya[yyac] = new YYARec(268,-94 );yyac++; 
					yya[yyac] = new YYARec(267,99);yyac++; 
					yya[yyac] = new YYARec(282,100);yyac++; 
					yya[yyac] = new YYARec(283,101);yyac++; 
					yya[yyac] = new YYARec(284,102);yyac++; 
					yya[yyac] = new YYARec(285,103);yyac++; 
					yya[yyac] = new YYARec(286,104);yyac++; 
					yya[yyac] = new YYARec(287,105);yyac++; 
					yya[yyac] = new YYARec(288,106);yyac++; 
					yya[yyac] = new YYARec(289,107);yyac++; 
					yya[yyac] = new YYARec(290,108);yyac++; 
					yya[yyac] = new YYARec(291,109);yyac++; 
					yya[yyac] = new YYARec(292,110);yyac++; 
					yya[yyac] = new YYARec(293,111);yyac++; 
					yya[yyac] = new YYARec(294,112);yyac++; 
					yya[yyac] = new YYARec(295,113);yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(268,146);yyac++; 
					yya[yyac] = new YYARec(261,147);yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(263,149);yyac++; 
					yya[yyac] = new YYARec(258,-53 );yyac++; 
					yya[yyac] = new YYARec(269,129);yyac++; 
					yya[yyac] = new YYARec(270,130);yyac++; 
					yya[yyac] = new YYARec(271,131);yyac++; 
					yya[yyac] = new YYARec(272,132);yyac++; 
					yya[yyac] = new YYARec(273,133);yyac++; 
					yya[yyac] = new YYARec(274,134);yyac++; 
					yya[yyac] = new YYARec(275,135);yyac++; 
					yya[yyac] = new YYARec(276,136);yyac++; 
					yya[yyac] = new YYARec(258,-55 );yyac++; 
					yya[yyac] = new YYARec(264,150);yyac++; 
					yya[yyac] = new YYARec(267,99);yyac++; 
					yya[yyac] = new YYARec(282,100);yyac++; 
					yya[yyac] = new YYARec(283,101);yyac++; 
					yya[yyac] = new YYARec(284,102);yyac++; 
					yya[yyac] = new YYARec(285,103);yyac++; 
					yya[yyac] = new YYARec(286,104);yyac++; 
					yya[yyac] = new YYARec(287,105);yyac++; 
					yya[yyac] = new YYARec(288,106);yyac++; 
					yya[yyac] = new YYARec(289,107);yyac++; 
					yya[yyac] = new YYARec(290,108);yyac++; 
					yya[yyac] = new YYARec(291,109);yyac++; 
					yya[yyac] = new YYARec(292,110);yyac++; 
					yya[yyac] = new YYARec(293,111);yyac++; 
					yya[yyac] = new YYARec(294,112);yyac++; 
					yya[yyac] = new YYARec(295,113);yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(267,99);yyac++; 
					yya[yyac] = new YYARec(282,100);yyac++; 
					yya[yyac] = new YYARec(283,101);yyac++; 
					yya[yyac] = new YYARec(284,102);yyac++; 
					yya[yyac] = new YYARec(285,103);yyac++; 
					yya[yyac] = new YYARec(286,104);yyac++; 
					yya[yyac] = new YYARec(287,105);yyac++; 
					yya[yyac] = new YYARec(288,106);yyac++; 
					yya[yyac] = new YYARec(289,107);yyac++; 
					yya[yyac] = new YYARec(290,108);yyac++; 
					yya[yyac] = new YYARec(291,109);yyac++; 
					yya[yyac] = new YYARec(292,110);yyac++; 
					yya[yyac] = new YYARec(293,111);yyac++; 
					yya[yyac] = new YYARec(294,112);yyac++; 
					yya[yyac] = new YYARec(295,113);yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(267,155);yyac++; 
					yya[yyac] = new YYARec(282,100);yyac++; 
					yya[yyac] = new YYARec(283,101);yyac++; 
					yya[yyac] = new YYARec(284,102);yyac++; 
					yya[yyac] = new YYARec(285,103);yyac++; 
					yya[yyac] = new YYARec(286,104);yyac++; 
					yya[yyac] = new YYARec(287,105);yyac++; 
					yya[yyac] = new YYARec(288,106);yyac++; 
					yya[yyac] = new YYARec(289,107);yyac++; 
					yya[yyac] = new YYARec(290,108);yyac++; 
					yya[yyac] = new YYARec(291,109);yyac++; 
					yya[yyac] = new YYARec(292,110);yyac++; 
					yya[yyac] = new YYARec(293,111);yyac++; 
					yya[yyac] = new YYARec(294,112);yyac++; 
					yya[yyac] = new YYARec(295,113);yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(268,156);yyac++; 
					yya[yyac] = new YYARec(269,129);yyac++; 
					yya[yyac] = new YYARec(270,130);yyac++; 
					yya[yyac] = new YYARec(271,131);yyac++; 
					yya[yyac] = new YYARec(272,132);yyac++; 
					yya[yyac] = new YYARec(273,133);yyac++; 
					yya[yyac] = new YYARec(274,134);yyac++; 
					yya[yyac] = new YYARec(275,135);yyac++; 
					yya[yyac] = new YYARec(276,136);yyac++; 
					yya[yyac] = new YYARec(264,157);yyac++; 
					yya[yyac] = new YYARec(263,158);yyac++; 
					yya[yyac] = new YYARec(258,-23 );yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(296,52);yyac++; 
					yya[yyac] = new YYARec(298,53);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(268,161);yyac++; 
					yya[yyac] = new YYARec(269,129);yyac++; 
					yya[yyac] = new YYARec(270,130);yyac++; 
					yya[yyac] = new YYARec(271,131);yyac++; 
					yya[yyac] = new YYARec(272,132);yyac++; 
					yya[yyac] = new YYARec(273,133);yyac++; 
					yya[yyac] = new YYARec(274,134);yyac++; 
					yya[yyac] = new YYARec(275,135);yyac++; 
					yya[yyac] = new YYARec(276,136);yyac++; 
					yya[yyac] = new YYARec(269,129);yyac++; 
					yya[yyac] = new YYARec(270,130);yyac++; 
					yya[yyac] = new YYARec(271,131);yyac++; 
					yya[yyac] = new YYARec(272,132);yyac++; 
					yya[yyac] = new YYARec(273,133);yyac++; 
					yya[yyac] = new YYARec(274,134);yyac++; 
					yya[yyac] = new YYARec(275,135);yyac++; 
					yya[yyac] = new YYARec(276,136);yyac++; 
					yya[yyac] = new YYARec(268,-95 );yyac++; 
					yya[yyac] = new YYARec(267,99);yyac++; 
					yya[yyac] = new YYARec(282,100);yyac++; 
					yya[yyac] = new YYARec(283,101);yyac++; 
					yya[yyac] = new YYARec(284,102);yyac++; 
					yya[yyac] = new YYARec(285,103);yyac++; 
					yya[yyac] = new YYARec(286,104);yyac++; 
					yya[yyac] = new YYARec(287,105);yyac++; 
					yya[yyac] = new YYARec(288,106);yyac++; 
					yya[yyac] = new YYARec(289,107);yyac++; 
					yya[yyac] = new YYARec(290,108);yyac++; 
					yya[yyac] = new YYARec(291,109);yyac++; 
					yya[yyac] = new YYARec(292,110);yyac++; 
					yya[yyac] = new YYARec(293,111);yyac++; 
					yya[yyac] = new YYARec(294,112);yyac++; 
					yya[yyac] = new YYARec(295,113);yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(296,52);yyac++; 
					yya[yyac] = new YYARec(298,53);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(265,165);yyac++; 
					yya[yyac] = new YYARec(268,166);yyac++; 
					yya[yyac] = new YYARec(269,129);yyac++; 
					yya[yyac] = new YYARec(270,130);yyac++; 
					yya[yyac] = new YYARec(271,131);yyac++; 
					yya[yyac] = new YYARec(272,132);yyac++; 
					yya[yyac] = new YYARec(273,133);yyac++; 
					yya[yyac] = new YYARec(274,134);yyac++; 
					yya[yyac] = new YYARec(275,135);yyac++; 
					yya[yyac] = new YYARec(276,136);yyac++; 
					yya[yyac] = new YYARec(265,167);yyac++; 
					yya[yyac] = new YYARec(263,168);yyac++; 
					yya[yyac] = new YYARec(297,169);yyac++; 
					yya[yyac] = new YYARec(258,-92 );yyac++; 
					yya[yyac] = new YYARec(308,20);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(264,171);yyac++; 
					yya[yyac] = new YYARec(296,52);yyac++; 
					yya[yyac] = new YYARec(298,53);yyac++; 
					yya[yyac] = new YYARec(309,13);yyac++; 
					yya[yyac] = new YYARec(265,-29 );yyac++; 
					yya[yyac] = new YYARec(265,173);yyac++;

					yyg[yygc] = new YYARec(-11,1);yygc++; 
					yyg[yygc] = new YYARec(-10,2);yygc++; 
					yyg[yygc] = new YYARec(-9,3);yygc++; 
					yyg[yygc] = new YYARec(-8,4);yygc++; 
					yyg[yygc] = new YYARec(-6,5);yygc++; 
					yyg[yygc] = new YYARec(-5,6);yygc++; 
					yyg[yygc] = new YYARec(-4,7);yygc++; 
					yyg[yygc] = new YYARec(-3,8);yygc++; 
					yyg[yygc] = new YYARec(-2,9);yygc++; 
					yyg[yygc] = new YYARec(-16,14);yygc++; 
					yyg[yygc] = new YYARec(-15,15);yygc++; 
					yyg[yygc] = new YYARec(-14,16);yygc++; 
					yyg[yygc] = new YYARec(-13,17);yygc++; 
					yyg[yygc] = new YYARec(-12,18);yygc++; 
					yyg[yygc] = new YYARec(-6,19);yygc++; 
					yyg[yygc] = new YYARec(-11,1);yygc++; 
					yyg[yygc] = new YYARec(-10,2);yygc++; 
					yyg[yygc] = new YYARec(-9,3);yygc++; 
					yyg[yygc] = new YYARec(-8,4);yygc++; 
					yyg[yygc] = new YYARec(-6,5);yygc++; 
					yyg[yygc] = new YYARec(-5,6);yygc++; 
					yyg[yygc] = new YYARec(-4,7);yygc++; 
					yyg[yygc] = new YYARec(-3,8);yygc++; 
					yyg[yygc] = new YYARec(-2,23);yygc++; 
					yyg[yygc] = new YYARec(-6,24);yygc++; 
					yyg[yygc] = new YYARec(-6,25);yygc++; 
					yyg[yygc] = new YYARec(-6,26);yygc++; 
					yyg[yygc] = new YYARec(-17,35);yygc++; 
					yyg[yygc] = new YYARec(-16,36);yygc++; 
					yyg[yygc] = new YYARec(-15,37);yygc++; 
					yyg[yygc] = new YYARec(-31,38);yygc++; 
					yyg[yygc] = new YYARec(-30,39);yygc++; 
					yyg[yygc] = new YYARec(-29,40);yygc++; 
					yyg[yygc] = new YYARec(-28,41);yygc++; 
					yyg[yygc] = new YYARec(-27,42);yygc++; 
					yyg[yygc] = new YYARec(-26,43);yygc++; 
					yyg[yygc] = new YYARec(-25,44);yygc++; 
					yyg[yygc] = new YYARec(-24,45);yygc++; 
					yyg[yygc] = new YYARec(-23,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-21,48);yygc++; 
					yyg[yygc] = new YYARec(-20,49);yygc++; 
					yyg[yygc] = new YYARec(-19,50);yygc++; 
					yyg[yygc] = new YYARec(-6,51);yygc++; 
					yyg[yygc] = new YYARec(-6,54);yygc++; 
					yyg[yygc] = new YYARec(-11,1);yygc++; 
					yyg[yygc] = new YYARec(-10,2);yygc++; 
					yyg[yygc] = new YYARec(-9,3);yygc++; 
					yyg[yygc] = new YYARec(-8,4);yygc++; 
					yyg[yygc] = new YYARec(-7,56);yygc++; 
					yyg[yygc] = new YYARec(-6,5);yygc++; 
					yyg[yygc] = new YYARec(-4,57);yygc++; 
					yyg[yygc] = new YYARec(-11,1);yygc++; 
					yyg[yygc] = new YYARec(-10,2);yygc++; 
					yyg[yygc] = new YYARec(-9,3);yygc++; 
					yyg[yygc] = new YYARec(-8,4);yygc++; 
					yyg[yygc] = new YYARec(-7,58);yygc++; 
					yyg[yygc] = new YYARec(-6,5);yygc++; 
					yyg[yygc] = new YYARec(-4,57);yygc++; 
					yyg[yygc] = new YYARec(-13,59);yygc++; 
					yyg[yygc] = new YYARec(-12,60);yygc++; 
					yyg[yygc] = new YYARec(-6,61);yygc++; 
					yyg[yygc] = new YYARec(-33,66);yygc++; 
					yyg[yygc] = new YYARec(-32,67);yygc++; 
					yyg[yygc] = new YYARec(-16,68);yygc++; 
					yyg[yygc] = new YYARec(-15,69);yygc++; 
					yyg[yygc] = new YYARec(-13,70);yygc++; 
					yyg[yygc] = new YYARec(-12,71);yygc++; 
					yyg[yygc] = new YYARec(-6,61);yygc++; 
					yyg[yygc] = new YYARec(-18,79);yygc++; 
					yyg[yygc] = new YYARec(-13,80);yygc++; 
					yyg[yygc] = new YYARec(-6,81);yygc++; 
					yyg[yygc] = new YYARec(-31,38);yygc++; 
					yyg[yygc] = new YYARec(-30,39);yygc++; 
					yyg[yygc] = new YYARec(-29,40);yygc++; 
					yyg[yygc] = new YYARec(-28,41);yygc++; 
					yyg[yygc] = new YYARec(-27,42);yygc++; 
					yyg[yygc] = new YYARec(-26,43);yygc++; 
					yyg[yygc] = new YYARec(-25,44);yygc++; 
					yyg[yygc] = new YYARec(-24,45);yygc++; 
					yyg[yygc] = new YYARec(-23,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-21,48);yygc++; 
					yyg[yygc] = new YYARec(-20,49);yygc++; 
					yyg[yygc] = new YYARec(-19,82);yygc++; 
					yyg[yygc] = new YYARec(-6,51);yygc++; 
					yyg[yygc] = new YYARec(-34,86);yygc++; 
					yyg[yygc] = new YYARec(-31,38);yygc++; 
					yyg[yygc] = new YYARec(-30,39);yygc++; 
					yyg[yygc] = new YYARec(-29,40);yygc++; 
					yyg[yygc] = new YYARec(-28,41);yygc++; 
					yyg[yygc] = new YYARec(-27,42);yygc++; 
					yyg[yygc] = new YYARec(-26,43);yygc++; 
					yyg[yygc] = new YYARec(-25,44);yygc++; 
					yyg[yygc] = new YYARec(-24,45);yygc++; 
					yyg[yygc] = new YYARec(-23,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-21,48);yygc++; 
					yyg[yygc] = new YYARec(-20,49);yygc++; 
					yyg[yygc] = new YYARec(-19,92);yygc++; 
					yyg[yygc] = new YYARec(-6,51);yygc++; 
					yyg[yygc] = new YYARec(-40,93);yygc++; 
					yyg[yygc] = new YYARec(-39,94);yygc++; 
					yyg[yygc] = new YYARec(-36,95);yygc++; 
					yyg[yygc] = new YYARec(-35,96);yygc++; 
					yyg[yygc] = new YYARec(-13,97);yygc++; 
					yyg[yygc] = new YYARec(-12,98);yygc++; 
					yyg[yygc] = new YYARec(-6,61);yygc++; 
					yyg[yygc] = new YYARec(-40,114);yygc++; 
					yyg[yygc] = new YYARec(-39,94);yygc++; 
					yyg[yygc] = new YYARec(-36,95);yygc++; 
					yyg[yygc] = new YYARec(-35,96);yygc++; 
					yyg[yygc] = new YYARec(-13,97);yygc++; 
					yyg[yygc] = new YYARec(-12,98);yygc++; 
					yyg[yygc] = new YYARec(-6,61);yygc++; 
					yyg[yygc] = new YYARec(-11,1);yygc++; 
					yyg[yygc] = new YYARec(-10,2);yygc++; 
					yyg[yygc] = new YYARec(-9,3);yygc++; 
					yyg[yygc] = new YYARec(-8,4);yygc++; 
					yyg[yygc] = new YYARec(-6,5);yygc++; 
					yyg[yygc] = new YYARec(-4,115);yygc++; 
					yyg[yygc] = new YYARec(-33,66);yygc++; 
					yyg[yygc] = new YYARec(-32,117);yygc++; 
					yyg[yygc] = new YYARec(-13,70);yygc++; 
					yyg[yygc] = new YYARec(-12,118);yygc++; 
					yyg[yygc] = new YYARec(-6,61);yygc++; 
					yyg[yygc] = new YYARec(-18,119);yygc++; 
					yyg[yygc] = new YYARec(-15,120);yygc++; 
					yyg[yygc] = new YYARec(-13,80);yygc++; 
					yyg[yygc] = new YYARec(-6,81);yygc++; 
					yyg[yygc] = new YYARec(-15,121);yygc++; 
					yyg[yygc] = new YYARec(-13,122);yygc++; 
					yyg[yygc] = new YYARec(-12,123);yygc++; 
					yyg[yygc] = new YYARec(-6,61);yygc++; 
					yyg[yygc] = new YYARec(-39,94);yygc++; 
					yyg[yygc] = new YYARec(-36,95);yygc++; 
					yyg[yygc] = new YYARec(-35,124);yygc++; 
					yyg[yygc] = new YYARec(-13,97);yygc++; 
					yyg[yygc] = new YYARec(-12,98);yygc++; 
					yyg[yygc] = new YYARec(-6,61);yygc++; 
					yyg[yygc] = new YYARec(-41,127);yygc++; 
					yyg[yygc] = new YYARec(-37,128);yygc++; 
					yyg[yygc] = new YYARec(-39,94);yygc++; 
					yyg[yygc] = new YYARec(-36,95);yygc++; 
					yyg[yygc] = new YYARec(-35,145);yygc++; 
					yyg[yygc] = new YYARec(-13,97);yygc++; 
					yyg[yygc] = new YYARec(-12,98);yygc++; 
					yyg[yygc] = new YYARec(-6,61);yygc++; 
					yyg[yygc] = new YYARec(-18,148);yygc++; 
					yyg[yygc] = new YYARec(-13,80);yygc++; 
					yyg[yygc] = new YYARec(-6,81);yygc++; 
					yyg[yygc] = new YYARec(-37,128);yygc++; 
					yyg[yygc] = new YYARec(-39,94);yygc++; 
					yyg[yygc] = new YYARec(-36,95);yygc++; 
					yyg[yygc] = new YYARec(-35,151);yygc++; 
					yyg[yygc] = new YYARec(-13,97);yygc++; 
					yyg[yygc] = new YYARec(-12,98);yygc++; 
					yyg[yygc] = new YYARec(-6,61);yygc++; 
					yyg[yygc] = new YYARec(-39,94);yygc++; 
					yyg[yygc] = new YYARec(-36,95);yygc++; 
					yyg[yygc] = new YYARec(-35,152);yygc++; 
					yyg[yygc] = new YYARec(-13,97);yygc++; 
					yyg[yygc] = new YYARec(-12,98);yygc++; 
					yyg[yygc] = new YYARec(-6,61);yygc++; 
					yyg[yygc] = new YYARec(-39,94);yygc++; 
					yyg[yygc] = new YYARec(-38,153);yygc++; 
					yyg[yygc] = new YYARec(-36,154);yygc++; 
					yyg[yygc] = new YYARec(-13,97);yygc++; 
					yyg[yygc] = new YYARec(-12,98);yygc++; 
					yyg[yygc] = new YYARec(-6,61);yygc++; 
					yyg[yygc] = new YYARec(-37,128);yygc++; 
					yyg[yygc] = new YYARec(-18,159);yygc++; 
					yyg[yygc] = new YYARec(-13,80);yygc++; 
					yyg[yygc] = new YYARec(-6,81);yygc++; 
					yyg[yygc] = new YYARec(-31,38);yygc++; 
					yyg[yygc] = new YYARec(-30,39);yygc++; 
					yyg[yygc] = new YYARec(-29,40);yygc++; 
					yyg[yygc] = new YYARec(-28,41);yygc++; 
					yyg[yygc] = new YYARec(-27,42);yygc++; 
					yyg[yygc] = new YYARec(-26,43);yygc++; 
					yyg[yygc] = new YYARec(-25,44);yygc++; 
					yyg[yygc] = new YYARec(-24,45);yygc++; 
					yyg[yygc] = new YYARec(-23,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-21,48);yygc++; 
					yyg[yygc] = new YYARec(-20,49);yygc++; 
					yyg[yygc] = new YYARec(-19,160);yygc++; 
					yyg[yygc] = new YYARec(-6,51);yygc++; 
					yyg[yygc] = new YYARec(-37,128);yygc++; 
					yyg[yygc] = new YYARec(-37,128);yygc++; 
					yyg[yygc] = new YYARec(-39,94);yygc++; 
					yyg[yygc] = new YYARec(-36,95);yygc++; 
					yyg[yygc] = new YYARec(-35,162);yygc++; 
					yyg[yygc] = new YYARec(-13,97);yygc++; 
					yyg[yygc] = new YYARec(-12,98);yygc++; 
					yyg[yygc] = new YYARec(-6,61);yygc++; 
					yyg[yygc] = new YYARec(-31,38);yygc++; 
					yyg[yygc] = new YYARec(-30,39);yygc++; 
					yyg[yygc] = new YYARec(-29,40);yygc++; 
					yyg[yygc] = new YYARec(-28,41);yygc++; 
					yyg[yygc] = new YYARec(-27,42);yygc++; 
					yyg[yygc] = new YYARec(-26,43);yygc++; 
					yyg[yygc] = new YYARec(-25,44);yygc++; 
					yyg[yygc] = new YYARec(-24,45);yygc++; 
					yyg[yygc] = new YYARec(-23,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-21,48);yygc++; 
					yyg[yygc] = new YYARec(-20,49);yygc++; 
					yyg[yygc] = new YYARec(-19,163);yygc++; 
					yyg[yygc] = new YYARec(-6,51);yygc++; 
					yyg[yygc] = new YYARec(-18,164);yygc++; 
					yyg[yygc] = new YYARec(-13,80);yygc++; 
					yyg[yygc] = new YYARec(-6,81);yygc++; 
					yyg[yygc] = new YYARec(-37,128);yygc++; 
					yyg[yygc] = new YYARec(-18,170);yygc++; 
					yyg[yygc] = new YYARec(-13,80);yygc++; 
					yyg[yygc] = new YYARec(-6,81);yygc++; 
					yyg[yygc] = new YYARec(-31,38);yygc++; 
					yyg[yygc] = new YYARec(-30,39);yygc++; 
					yyg[yygc] = new YYARec(-29,40);yygc++; 
					yyg[yygc] = new YYARec(-28,41);yygc++; 
					yyg[yygc] = new YYARec(-27,42);yygc++; 
					yyg[yygc] = new YYARec(-26,43);yygc++; 
					yyg[yygc] = new YYARec(-25,44);yygc++; 
					yyg[yygc] = new YYARec(-24,45);yygc++; 
					yyg[yygc] = new YYARec(-23,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-21,48);yygc++; 
					yyg[yygc] = new YYARec(-20,49);yygc++; 
					yyg[yygc] = new YYARec(-19,172);yygc++; 
					yyg[yygc] = new YYARec(-6,51);yygc++;

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
					yyd[13] = -107;  
					yyd[14] = -20;  
					yyd[15] = -17;  
					yyd[16] = 0;  
					yyd[17] = -19;  
					yyd[18] = -18;  
					yyd[19] = 0;  
					yyd[20] = -108;  
					yyd[21] = -111;  
					yyd[22] = -112;  
					yyd[23] = -2;  
					yyd[24] = 0;  
					yyd[25] = 0;  
					yyd[26] = 0;  
					yyd[27] = -16;  
					yyd[28] = 0;  
					yyd[29] = 0;  
					yyd[30] = 0;  
					yyd[31] = 0;  
					yyd[32] = 0;  
					yyd[33] = -15;  
					yyd[34] = 0;  
					yyd[35] = 0;  
					yyd[36] = 0;  
					yyd[37] = -24;  
					yyd[38] = -41;  
					yyd[39] = -40;  
					yyd[40] = -39;  
					yyd[41] = -37;  
					yyd[42] = -36;  
					yyd[43] = -35;  
					yyd[44] = -34;  
					yyd[45] = -33;  
					yyd[46] = -32;  
					yyd[47] = -31;  
					yyd[48] = -30;  
					yyd[49] = 0;  
					yyd[50] = 0;  
					yyd[51] = 0;  
					yyd[52] = 0;  
					yyd[53] = 0;  
					yyd[54] = -105;  
					yyd[55] = -104;  
					yyd[56] = -5;  
					yyd[57] = 0;  
					yyd[58] = -6;  
					yyd[59] = 0;  
					yyd[60] = 0;  
					yyd[61] = 0;  
					yyd[62] = -21;  
					yyd[63] = 0;  
					yyd[64] = 0;  
					yyd[65] = -26;  
					yyd[66] = 0;  
					yyd[67] = -42;  
					yyd[68] = 0;  
					yyd[69] = 0;  
					yyd[70] = -46;  
					yyd[71] = 0;  
					yyd[72] = 0;  
					yyd[73] = 0;  
					yyd[74] = 0;  
					yyd[75] = 0;  
					yyd[76] = -8;  
					yyd[77] = -14;  
					yyd[78] = -13;  
					yyd[79] = 0;  
					yyd[80] = -110;  
					yyd[81] = -109;  
					yyd[82] = -28;  
					yyd[83] = 0;  
					yyd[84] = 0;  
					yyd[85] = 0;  
					yyd[86] = 0;  
					yyd[87] = -72;  
					yyd[88] = -73;  
					yyd[89] = -74;  
					yyd[90] = -75;  
					yyd[91] = -76;  
					yyd[92] = -27;  
					yyd[93] = 0;  
					yyd[94] = 0;  
					yyd[95] = -57;  
					yyd[96] = 0;  
					yyd[97] = -63;  
					yyd[98] = -62;  
					yyd[99] = 0;  
					yyd[100] = -77;  
					yyd[101] = -78;  
					yyd[102] = -79;  
					yyd[103] = -80;  
					yyd[104] = -81;  
					yyd[105] = -82;  
					yyd[106] = -83;  
					yyd[107] = -84;  
					yyd[108] = -85;  
					yyd[109] = -86;  
					yyd[110] = -87;  
					yyd[111] = -88;  
					yyd[112] = -89;  
					yyd[113] = -90;  
					yyd[114] = 0;  
					yyd[115] = 0;  
					yyd[116] = 0;  
					yyd[117] = -43;  
					yyd[118] = -45;  
					yyd[119] = 0;  
					yyd[120] = -49;  
					yyd[121] = -50;  
					yyd[122] = -51;  
					yyd[123] = -52;  
					yyd[124] = 0;  
					yyd[125] = 0;  
					yyd[126] = 0;  
					yyd[127] = 0;  
					yyd[128] = 0;  
					yyd[129] = -64;  
					yyd[130] = -65;  
					yyd[131] = -66;  
					yyd[132] = -67;  
					yyd[133] = -68;  
					yyd[134] = -69;  
					yyd[135] = -70;  
					yyd[136] = -71;  
					yyd[137] = -96;  
					yyd[138] = -97;  
					yyd[139] = -98;  
					yyd[140] = -99;  
					yyd[141] = -100;  
					yyd[142] = -101;  
					yyd[143] = -102;  
					yyd[144] = -103;  
					yyd[145] = 0;  
					yyd[146] = 0;  
					yyd[147] = -7;  
					yyd[148] = 0;  
					yyd[149] = 0;  
					yyd[150] = 0;  
					yyd[151] = 0;  
					yyd[152] = 0;  
					yyd[153] = -58;  
					yyd[154] = -60;  
					yyd[155] = 0;  
					yyd[156] = -56;  
					yyd[157] = 0;  
					yyd[158] = 0;  
					yyd[159] = -54;  
					yyd[160] = 0;  
					yyd[161] = -61;  
					yyd[162] = 0;  
					yyd[163] = 0;  
					yyd[164] = 0;  
					yyd[165] = 0;  
					yyd[166] = -59;  
					yyd[167] = -93;  
					yyd[168] = 0;  
					yyd[169] = 0;  
					yyd[170] = -22;  
					yyd[171] = 0;  
					yyd[172] = 0;  
					yyd[173] = -91; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 5;  
					yyal[2] = 5;  
					yyal[3] = 5;  
					yyal[4] = 5;  
					yyal[5] = 5;  
					yyal[6] = 9;  
					yyal[7] = 9;  
					yyal[8] = 9;  
					yyal[9] = 14;  
					yyal[10] = 15;  
					yyal[11] = 16;  
					yyal[12] = 17;  
					yyal[13] = 18;  
					yyal[14] = 18;  
					yyal[15] = 18;  
					yyal[16] = 18;  
					yyal[17] = 19;  
					yyal[18] = 19;  
					yyal[19] = 19;  
					yyal[20] = 23;  
					yyal[21] = 23;  
					yyal[22] = 23;  
					yyal[23] = 23;  
					yyal[24] = 23;  
					yyal[25] = 24;  
					yyal[26] = 25;  
					yyal[27] = 27;  
					yyal[28] = 27;  
					yyal[29] = 29;  
					yyal[30] = 33;  
					yyal[31] = 35;  
					yyal[32] = 37;  
					yyal[33] = 39;  
					yyal[34] = 39;  
					yyal[35] = 41;  
					yyal[36] = 42;  
					yyal[37] = 44;  
					yyal[38] = 44;  
					yyal[39] = 44;  
					yyal[40] = 44;  
					yyal[41] = 44;  
					yyal[42] = 44;  
					yyal[43] = 44;  
					yyal[44] = 44;  
					yyal[45] = 44;  
					yyal[46] = 44;  
					yyal[47] = 44;  
					yyal[48] = 44;  
					yyal[49] = 44;  
					yyal[50] = 45;  
					yyal[51] = 46;  
					yyal[52] = 52;  
					yyal[53] = 53;  
					yyal[54] = 54;  
					yyal[55] = 54;  
					yyal[56] = 54;  
					yyal[57] = 54;  
					yyal[58] = 56;  
					yyal[59] = 56;  
					yyal[60] = 57;  
					yyal[61] = 58;  
					yyal[62] = 83;  
					yyal[63] = 83;  
					yyal[64] = 85;  
					yyal[65] = 89;  
					yyal[66] = 89;  
					yyal[67] = 91;  
					yyal[68] = 91;  
					yyal[69] = 93;  
					yyal[70] = 95;  
					yyal[71] = 95;  
					yyal[72] = 102;  
					yyal[73] = 106;  
					yyal[74] = 123;  
					yyal[75] = 140;  
					yyal[76] = 142;  
					yyal[77] = 142;  
					yyal[78] = 142;  
					yyal[79] = 142;  
					yyal[80] = 143;  
					yyal[81] = 143;  
					yyal[82] = 143;  
					yyal[83] = 143;  
					yyal[84] = 145;  
					yyal[85] = 148;  
					yyal[86] = 151;  
					yyal[87] = 168;  
					yyal[88] = 168;  
					yyal[89] = 168;  
					yyal[90] = 168;  
					yyal[91] = 168;  
					yyal[92] = 168;  
					yyal[93] = 168;  
					yyal[94] = 169;  
					yyal[95] = 170;  
					yyal[96] = 170;  
					yyal[97] = 187;  
					yyal[98] = 187;  
					yyal[99] = 187;  
					yyal[100] = 204;  
					yyal[101] = 204;  
					yyal[102] = 204;  
					yyal[103] = 204;  
					yyal[104] = 204;  
					yyal[105] = 204;  
					yyal[106] = 204;  
					yyal[107] = 204;  
					yyal[108] = 204;  
					yyal[109] = 204;  
					yyal[110] = 204;  
					yyal[111] = 204;  
					yyal[112] = 204;  
					yyal[113] = 204;  
					yyal[114] = 204;  
					yyal[115] = 205;  
					yyal[116] = 206;  
					yyal[117] = 208;  
					yyal[118] = 208;  
					yyal[119] = 208;  
					yyal[120] = 210;  
					yyal[121] = 210;  
					yyal[122] = 210;  
					yyal[123] = 210;  
					yyal[124] = 210;  
					yyal[125] = 219;  
					yyal[126] = 220;  
					yyal[127] = 237;  
					yyal[128] = 254;  
					yyal[129] = 271;  
					yyal[130] = 271;  
					yyal[131] = 271;  
					yyal[132] = 271;  
					yyal[133] = 271;  
					yyal[134] = 271;  
					yyal[135] = 271;  
					yyal[136] = 271;  
					yyal[137] = 271;  
					yyal[138] = 271;  
					yyal[139] = 271;  
					yyal[140] = 271;  
					yyal[141] = 271;  
					yyal[142] = 271;  
					yyal[143] = 271;  
					yyal[144] = 271;  
					yyal[145] = 271;  
					yyal[146] = 280;  
					yyal[147] = 281;  
					yyal[148] = 281;  
					yyal[149] = 283;  
					yyal[150] = 285;  
					yyal[151] = 289;  
					yyal[152] = 298;  
					yyal[153] = 307;  
					yyal[154] = 307;  
					yyal[155] = 307;  
					yyal[156] = 324;  
					yyal[157] = 324;  
					yyal[158] = 328;  
					yyal[159] = 330;  
					yyal[160] = 330;  
					yyal[161] = 331;  
					yyal[162] = 331;  
					yyal[163] = 340;  
					yyal[164] = 341;  
					yyal[165] = 342;  
					yyal[166] = 344;  
					yyal[167] = 344;  
					yyal[168] = 344;  
					yyal[169] = 346;  
					yyal[170] = 347;  
					yyal[171] = 347;  
					yyal[172] = 351;  
					yyal[173] = 352; 

					yyah = new int[yynstates];
					yyah[0] = 4;  
					yyah[1] = 4;  
					yyah[2] = 4;  
					yyah[3] = 4;  
					yyah[4] = 4;  
					yyah[5] = 8;  
					yyah[6] = 8;  
					yyah[7] = 8;  
					yyah[8] = 13;  
					yyah[9] = 14;  
					yyah[10] = 15;  
					yyah[11] = 16;  
					yyah[12] = 17;  
					yyah[13] = 17;  
					yyah[14] = 17;  
					yyah[15] = 17;  
					yyah[16] = 18;  
					yyah[17] = 18;  
					yyah[18] = 18;  
					yyah[19] = 22;  
					yyah[20] = 22;  
					yyah[21] = 22;  
					yyah[22] = 22;  
					yyah[23] = 22;  
					yyah[24] = 23;  
					yyah[25] = 24;  
					yyah[26] = 26;  
					yyah[27] = 26;  
					yyah[28] = 28;  
					yyah[29] = 32;  
					yyah[30] = 34;  
					yyah[31] = 36;  
					yyah[32] = 38;  
					yyah[33] = 38;  
					yyah[34] = 40;  
					yyah[35] = 41;  
					yyah[36] = 43;  
					yyah[37] = 43;  
					yyah[38] = 43;  
					yyah[39] = 43;  
					yyah[40] = 43;  
					yyah[41] = 43;  
					yyah[42] = 43;  
					yyah[43] = 43;  
					yyah[44] = 43;  
					yyah[45] = 43;  
					yyah[46] = 43;  
					yyah[47] = 43;  
					yyah[48] = 43;  
					yyah[49] = 44;  
					yyah[50] = 45;  
					yyah[51] = 51;  
					yyah[52] = 52;  
					yyah[53] = 53;  
					yyah[54] = 53;  
					yyah[55] = 53;  
					yyah[56] = 53;  
					yyah[57] = 55;  
					yyah[58] = 55;  
					yyah[59] = 56;  
					yyah[60] = 57;  
					yyah[61] = 82;  
					yyah[62] = 82;  
					yyah[63] = 84;  
					yyah[64] = 88;  
					yyah[65] = 88;  
					yyah[66] = 90;  
					yyah[67] = 90;  
					yyah[68] = 92;  
					yyah[69] = 94;  
					yyah[70] = 94;  
					yyah[71] = 101;  
					yyah[72] = 105;  
					yyah[73] = 122;  
					yyah[74] = 139;  
					yyah[75] = 141;  
					yyah[76] = 141;  
					yyah[77] = 141;  
					yyah[78] = 141;  
					yyah[79] = 142;  
					yyah[80] = 142;  
					yyah[81] = 142;  
					yyah[82] = 142;  
					yyah[83] = 144;  
					yyah[84] = 147;  
					yyah[85] = 150;  
					yyah[86] = 167;  
					yyah[87] = 167;  
					yyah[88] = 167;  
					yyah[89] = 167;  
					yyah[90] = 167;  
					yyah[91] = 167;  
					yyah[92] = 167;  
					yyah[93] = 168;  
					yyah[94] = 169;  
					yyah[95] = 169;  
					yyah[96] = 186;  
					yyah[97] = 186;  
					yyah[98] = 186;  
					yyah[99] = 203;  
					yyah[100] = 203;  
					yyah[101] = 203;  
					yyah[102] = 203;  
					yyah[103] = 203;  
					yyah[104] = 203;  
					yyah[105] = 203;  
					yyah[106] = 203;  
					yyah[107] = 203;  
					yyah[108] = 203;  
					yyah[109] = 203;  
					yyah[110] = 203;  
					yyah[111] = 203;  
					yyah[112] = 203;  
					yyah[113] = 203;  
					yyah[114] = 204;  
					yyah[115] = 205;  
					yyah[116] = 207;  
					yyah[117] = 207;  
					yyah[118] = 207;  
					yyah[119] = 209;  
					yyah[120] = 209;  
					yyah[121] = 209;  
					yyah[122] = 209;  
					yyah[123] = 209;  
					yyah[124] = 218;  
					yyah[125] = 219;  
					yyah[126] = 236;  
					yyah[127] = 253;  
					yyah[128] = 270;  
					yyah[129] = 270;  
					yyah[130] = 270;  
					yyah[131] = 270;  
					yyah[132] = 270;  
					yyah[133] = 270;  
					yyah[134] = 270;  
					yyah[135] = 270;  
					yyah[136] = 270;  
					yyah[137] = 270;  
					yyah[138] = 270;  
					yyah[139] = 270;  
					yyah[140] = 270;  
					yyah[141] = 270;  
					yyah[142] = 270;  
					yyah[143] = 270;  
					yyah[144] = 270;  
					yyah[145] = 279;  
					yyah[146] = 280;  
					yyah[147] = 280;  
					yyah[148] = 282;  
					yyah[149] = 284;  
					yyah[150] = 288;  
					yyah[151] = 297;  
					yyah[152] = 306;  
					yyah[153] = 306;  
					yyah[154] = 306;  
					yyah[155] = 323;  
					yyah[156] = 323;  
					yyah[157] = 327;  
					yyah[158] = 329;  
					yyah[159] = 329;  
					yyah[160] = 330;  
					yyah[161] = 330;  
					yyah[162] = 339;  
					yyah[163] = 340;  
					yyah[164] = 341;  
					yyah[165] = 343;  
					yyah[166] = 343;  
					yyah[167] = 343;  
					yyah[168] = 345;  
					yyah[169] = 346;  
					yyah[170] = 346;  
					yyah[171] = 350;  
					yyah[172] = 351;  
					yyah[173] = 351; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 10;  
					yygl[2] = 10;  
					yygl[3] = 10;  
					yygl[4] = 10;  
					yygl[5] = 10;  
					yygl[6] = 16;  
					yygl[7] = 16;  
					yygl[8] = 16;  
					yygl[9] = 25;  
					yygl[10] = 25;  
					yygl[11] = 26;  
					yygl[12] = 27;  
					yygl[13] = 28;  
					yygl[14] = 28;  
					yygl[15] = 28;  
					yygl[16] = 28;  
					yygl[17] = 28;  
					yygl[18] = 28;  
					yygl[19] = 28;  
					yygl[20] = 28;  
					yygl[21] = 28;  
					yygl[22] = 28;  
					yygl[23] = 28;  
					yygl[24] = 28;  
					yygl[25] = 28;  
					yygl[26] = 28;  
					yygl[27] = 28;  
					yygl[28] = 28;  
					yygl[29] = 31;  
					yygl[30] = 45;  
					yygl[31] = 46;  
					yygl[32] = 53;  
					yygl[33] = 60;  
					yygl[34] = 60;  
					yygl[35] = 63;  
					yygl[36] = 63;  
					yygl[37] = 63;  
					yygl[38] = 63;  
					yygl[39] = 63;  
					yygl[40] = 63;  
					yygl[41] = 63;  
					yygl[42] = 63;  
					yygl[43] = 63;  
					yygl[44] = 63;  
					yygl[45] = 63;  
					yygl[46] = 63;  
					yygl[47] = 63;  
					yygl[48] = 63;  
					yygl[49] = 63;  
					yygl[50] = 63;  
					yygl[51] = 63;  
					yygl[52] = 70;  
					yygl[53] = 70;  
					yygl[54] = 70;  
					yygl[55] = 70;  
					yygl[56] = 70;  
					yygl[57] = 70;  
					yygl[58] = 70;  
					yygl[59] = 70;  
					yygl[60] = 70;  
					yygl[61] = 70;  
					yygl[62] = 70;  
					yygl[63] = 70;  
					yygl[64] = 73;  
					yygl[65] = 87;  
					yygl[66] = 87;  
					yygl[67] = 87;  
					yygl[68] = 87;  
					yygl[69] = 87;  
					yygl[70] = 87;  
					yygl[71] = 87;  
					yygl[72] = 88;  
					yygl[73] = 102;  
					yygl[74] = 109;  
					yygl[75] = 116;  
					yygl[76] = 122;  
					yygl[77] = 122;  
					yygl[78] = 122;  
					yygl[79] = 122;  
					yygl[80] = 122;  
					yygl[81] = 122;  
					yygl[82] = 122;  
					yygl[83] = 122;  
					yygl[84] = 127;  
					yygl[85] = 131;  
					yygl[86] = 135;  
					yygl[87] = 141;  
					yygl[88] = 141;  
					yygl[89] = 141;  
					yygl[90] = 141;  
					yygl[91] = 141;  
					yygl[92] = 141;  
					yygl[93] = 141;  
					yygl[94] = 141;  
					yygl[95] = 141;  
					yygl[96] = 141;  
					yygl[97] = 143;  
					yygl[98] = 143;  
					yygl[99] = 143;  
					yygl[100] = 149;  
					yygl[101] = 149;  
					yygl[102] = 149;  
					yygl[103] = 149;  
					yygl[104] = 149;  
					yygl[105] = 149;  
					yygl[106] = 149;  
					yygl[107] = 149;  
					yygl[108] = 149;  
					yygl[109] = 149;  
					yygl[110] = 149;  
					yygl[111] = 149;  
					yygl[112] = 149;  
					yygl[113] = 149;  
					yygl[114] = 149;  
					yygl[115] = 149;  
					yygl[116] = 149;  
					yygl[117] = 152;  
					yygl[118] = 152;  
					yygl[119] = 152;  
					yygl[120] = 152;  
					yygl[121] = 152;  
					yygl[122] = 152;  
					yygl[123] = 152;  
					yygl[124] = 152;  
					yygl[125] = 153;  
					yygl[126] = 153;  
					yygl[127] = 159;  
					yygl[128] = 165;  
					yygl[129] = 171;  
					yygl[130] = 171;  
					yygl[131] = 171;  
					yygl[132] = 171;  
					yygl[133] = 171;  
					yygl[134] = 171;  
					yygl[135] = 171;  
					yygl[136] = 171;  
					yygl[137] = 171;  
					yygl[138] = 171;  
					yygl[139] = 171;  
					yygl[140] = 171;  
					yygl[141] = 171;  
					yygl[142] = 171;  
					yygl[143] = 171;  
					yygl[144] = 171;  
					yygl[145] = 171;  
					yygl[146] = 172;  
					yygl[147] = 172;  
					yygl[148] = 172;  
					yygl[149] = 172;  
					yygl[150] = 175;  
					yygl[151] = 189;  
					yygl[152] = 190;  
					yygl[153] = 191;  
					yygl[154] = 191;  
					yygl[155] = 191;  
					yygl[156] = 197;  
					yygl[157] = 197;  
					yygl[158] = 211;  
					yygl[159] = 214;  
					yygl[160] = 214;  
					yygl[161] = 214;  
					yygl[162] = 214;  
					yygl[163] = 215;  
					yygl[164] = 215;  
					yygl[165] = 215;  
					yygl[166] = 215;  
					yygl[167] = 215;  
					yygl[168] = 215;  
					yygl[169] = 218;  
					yygl[170] = 218;  
					yygl[171] = 218;  
					yygl[172] = 232;  
					yygl[173] = 232; 

					yygh = new int[yynstates];
					yygh[0] = 9;  
					yygh[1] = 9;  
					yygh[2] = 9;  
					yygh[3] = 9;  
					yygh[4] = 9;  
					yygh[5] = 15;  
					yygh[6] = 15;  
					yygh[7] = 15;  
					yygh[8] = 24;  
					yygh[9] = 24;  
					yygh[10] = 25;  
					yygh[11] = 26;  
					yygh[12] = 27;  
					yygh[13] = 27;  
					yygh[14] = 27;  
					yygh[15] = 27;  
					yygh[16] = 27;  
					yygh[17] = 27;  
					yygh[18] = 27;  
					yygh[19] = 27;  
					yygh[20] = 27;  
					yygh[21] = 27;  
					yygh[22] = 27;  
					yygh[23] = 27;  
					yygh[24] = 27;  
					yygh[25] = 27;  
					yygh[26] = 27;  
					yygh[27] = 27;  
					yygh[28] = 30;  
					yygh[29] = 44;  
					yygh[30] = 45;  
					yygh[31] = 52;  
					yygh[32] = 59;  
					yygh[33] = 59;  
					yygh[34] = 62;  
					yygh[35] = 62;  
					yygh[36] = 62;  
					yygh[37] = 62;  
					yygh[38] = 62;  
					yygh[39] = 62;  
					yygh[40] = 62;  
					yygh[41] = 62;  
					yygh[42] = 62;  
					yygh[43] = 62;  
					yygh[44] = 62;  
					yygh[45] = 62;  
					yygh[46] = 62;  
					yygh[47] = 62;  
					yygh[48] = 62;  
					yygh[49] = 62;  
					yygh[50] = 62;  
					yygh[51] = 69;  
					yygh[52] = 69;  
					yygh[53] = 69;  
					yygh[54] = 69;  
					yygh[55] = 69;  
					yygh[56] = 69;  
					yygh[57] = 69;  
					yygh[58] = 69;  
					yygh[59] = 69;  
					yygh[60] = 69;  
					yygh[61] = 69;  
					yygh[62] = 69;  
					yygh[63] = 72;  
					yygh[64] = 86;  
					yygh[65] = 86;  
					yygh[66] = 86;  
					yygh[67] = 86;  
					yygh[68] = 86;  
					yygh[69] = 86;  
					yygh[70] = 86;  
					yygh[71] = 87;  
					yygh[72] = 101;  
					yygh[73] = 108;  
					yygh[74] = 115;  
					yygh[75] = 121;  
					yygh[76] = 121;  
					yygh[77] = 121;  
					yygh[78] = 121;  
					yygh[79] = 121;  
					yygh[80] = 121;  
					yygh[81] = 121;  
					yygh[82] = 121;  
					yygh[83] = 126;  
					yygh[84] = 130;  
					yygh[85] = 134;  
					yygh[86] = 140;  
					yygh[87] = 140;  
					yygh[88] = 140;  
					yygh[89] = 140;  
					yygh[90] = 140;  
					yygh[91] = 140;  
					yygh[92] = 140;  
					yygh[93] = 140;  
					yygh[94] = 140;  
					yygh[95] = 140;  
					yygh[96] = 142;  
					yygh[97] = 142;  
					yygh[98] = 142;  
					yygh[99] = 148;  
					yygh[100] = 148;  
					yygh[101] = 148;  
					yygh[102] = 148;  
					yygh[103] = 148;  
					yygh[104] = 148;  
					yygh[105] = 148;  
					yygh[106] = 148;  
					yygh[107] = 148;  
					yygh[108] = 148;  
					yygh[109] = 148;  
					yygh[110] = 148;  
					yygh[111] = 148;  
					yygh[112] = 148;  
					yygh[113] = 148;  
					yygh[114] = 148;  
					yygh[115] = 148;  
					yygh[116] = 151;  
					yygh[117] = 151;  
					yygh[118] = 151;  
					yygh[119] = 151;  
					yygh[120] = 151;  
					yygh[121] = 151;  
					yygh[122] = 151;  
					yygh[123] = 151;  
					yygh[124] = 152;  
					yygh[125] = 152;  
					yygh[126] = 158;  
					yygh[127] = 164;  
					yygh[128] = 170;  
					yygh[129] = 170;  
					yygh[130] = 170;  
					yygh[131] = 170;  
					yygh[132] = 170;  
					yygh[133] = 170;  
					yygh[134] = 170;  
					yygh[135] = 170;  
					yygh[136] = 170;  
					yygh[137] = 170;  
					yygh[138] = 170;  
					yygh[139] = 170;  
					yygh[140] = 170;  
					yygh[141] = 170;  
					yygh[142] = 170;  
					yygh[143] = 170;  
					yygh[144] = 170;  
					yygh[145] = 171;  
					yygh[146] = 171;  
					yygh[147] = 171;  
					yygh[148] = 171;  
					yygh[149] = 174;  
					yygh[150] = 188;  
					yygh[151] = 189;  
					yygh[152] = 190;  
					yygh[153] = 190;  
					yygh[154] = 190;  
					yygh[155] = 196;  
					yygh[156] = 196;  
					yygh[157] = 210;  
					yygh[158] = 213;  
					yygh[159] = 213;  
					yygh[160] = 213;  
					yygh[161] = 213;  
					yygh[162] = 214;  
					yygh[163] = 214;  
					yygh[164] = 214;  
					yygh[165] = 214;  
					yygh[166] = 214;  
					yygh[167] = 214;  
					yygh[168] = 217;  
					yygh[169] = 217;  
					yygh[170] = 217;  
					yygh[171] = 231;  
					yygh[172] = 231;  
					yygh[173] = 231; 

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
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
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
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-13);yyrc++; 
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

			if (Regex.IsMatch(Rest,"^(\\+)")){
				Results.Add (t_Char43);
				ResultsV.Add(Regex.Match(Rest,"^(\\+)").Value);}

			if (Regex.IsMatch(Rest,"^(\\-)")){
				Results.Add (t_Char45);
				ResultsV.Add(Regex.Match(Rest,"^(\\-)").Value);}

			if (Regex.IsMatch(Rest,"^(/)")){
				Results.Add (t_Char47);
				ResultsV.Add(Regex.Match(Rest,"^(/)").Value);}

			if (Regex.IsMatch(Rest,"^(\\^)")){
				Results.Add (t_Char94);
				ResultsV.Add(Regex.Match(Rest,"^(\\^)").Value);}

			if (Regex.IsMatch(Rest,"^(|)")){
				Results.Add (t_Char124);
				ResultsV.Add(Regex.Match(Rest,"^(|)").Value);}

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

			if (Regex.IsMatch(Rest,"^(||)")){
				Results.Add (t_Char124Char124);
				ResultsV.Add(Regex.Match(Rest,"^(||)").Value);}

			if (Regex.IsMatch(Rest,"^(\\.)")){
				Results.Add (t_Char46);
				ResultsV.Add(Regex.Match(Rest,"^(\\.)").Value);}

			if (Regex.IsMatch(Rest,"^([+-]?([0-9]*[.])?[0-9]+)")){
				Results.Add (t_number);
				ResultsV.Add(Regex.Match(Rest,"^([+-]?([0-9]*[.])?[0-9]+)").Value);}

			if (Regex.IsMatch(Rest,"^([A-Za-z][A-Za-z0-9_]*)")){
				Results.Add (t_identifier);
				ResultsV.Add(Regex.Match(Rest,"^([A-Za-z][A-Za-z0-9_]*)").Value);}

			if (Regex.IsMatch(Rest,"^(<.*>)")){
				Results.Add (t_file);
				ResultsV.Add(Regex.Match(Rest,"^(<.*>)").Value);}

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
