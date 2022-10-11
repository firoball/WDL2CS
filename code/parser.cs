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
                int t_FLIC = 266;
                int t_MODEL = 267;
                int t_MUSIC = 268;
                int t_SOUND = 269;
                int t_BMAP = 270;
                int t_OVLY = 271;
                int t_FONT = 272;
                int t_BMAPS = 273;
                int t_OVLYS = 274;
                int t_PANChar95MAP = 275;
                int t_ACTION = 276;
                int t_ACTOR = 277;
                int t_OVERLAY = 278;
                int t_PALETTE = 279;
                int t_PANEL = 280;
                int t_REGION = 281;
                int t_SKILL = 282;
                int t_STRING = 283;
                int t_SYNONYM = 284;
                int t_TEXT = 285;
                int t_TEXTURE = 286;
                int t_THING = 287;
                int t_WALL = 288;
                int t_WAY = 289;
                int t_Char123 = 290;
                int t_Char125 = 291;
                int t_Char58 = 292;
                int t_Char124Char124 = 293;
                int t_Char38Char38 = 294;
                int t_Char124 = 295;
                int t_Char94 = 296;
                int t_Char38 = 297;
                int t_Char40 = 298;
                int t_Char41 = 299;
                int t_Char33Char61 = 300;
                int t_Char61Char61 = 301;
                int t_Char60 = 302;
                int t_Char60Char61 = 303;
                int t_Char62 = 304;
                int t_Char62Char61 = 305;
                int t_Char43 = 306;
                int t_Char45 = 307;
                int t_Char37 = 308;
                int t_Char42 = 309;
                int t_Char47 = 310;
                int t_Char33 = 311;
                int t_RULE = 312;
                int t_Char42Char61 = 313;
                int t_Char43Char61 = 314;
                int t_Char45Char61 = 315;
                int t_Char47Char61 = 316;
                int t_Char61 = 317;
                int t_ABS = 318;
                int t_ACOS = 319;
                int t_ASIN = 320;
                int t_COS = 321;
                int t_EXP = 322;
                int t_INT = 323;
                int t_LOG = 324;
                int t_LOG10 = 325;
                int t_LOG2 = 326;
                int t_RANDOM = 327;
                int t_SIGN = 328;
                int t_SIN = 329;
                int t_SQRT = 330;
                int t_TAN = 331;
                int t_ELSE = 332;
                int t_IF = 333;
                int t_WHILE = 334;
                int t_Char46 = 335;
                int t_NULL = 336;
                int t_integer = 337;
                int t_flag = 338;
                int t_fixed = 339;
                int t_identifier = 340;
                int t_file = 341;
                int t_string = 342;
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
         yyval = yyv[yysp-12] + yyv[yysp-11] + yyv[yysp-10] + yyv[yysp-9] + yyv[yysp-8] + yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   28 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   29 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-16] + yyv[yysp-15] + yyv[yysp-14] + yyv[yysp-13] + yyv[yysp-12] + yyv[yysp-11] + yyv[yysp-10] + yyv[yysp-9] + yyv[yysp-8] + yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   38 : 
         yyval = yyv[yysp-8] + yyv[yysp-7] + yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   46 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   47 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   48 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   63 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   64 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   65 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   66 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   67 : 
         yyval = "";
         
       break;
							case   68 : 
         yyval = yyv[yysp-0];
         
       break;
							case   69 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   70 : 
         yyval = yyv[yysp-0];
         
       break;
							case   71 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   72 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = "";
         
       break;
							case   79 : 
         yyval = yyv[yysp-0];
         
       break;
							case   80 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   91 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   92 : 
         yyval = yyv[yysp-0];
         
       break;
							case   93 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   98 : 
         yyval = yyv[yysp-0];
         
       break;
							case   99 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  100 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  119 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case  131 : 
         yyval = yyv[yysp-0];
         
       break;
							case  132 : 
         yyval = yyv[yysp-0];
         
       break;
							case  133 : 
         yyval = yyv[yysp-0];
         
       break;
							case  134 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  140 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  141 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  142 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  143 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  152 : 
         yyval = yyv[yysp-0];
         
       break;
							case  153 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  154 : 
         yyval = yyv[yysp-0];
         
       break;
							case  155 : 
         yyval = yyv[yysp-0];
         
       break;
							case  156 : 
         yyval = yyv[yysp-0];
         
       break;
							case  157 : 
         yyval = yyv[yysp-0];
         
       break;
							case  158 : 
         yyval = yyv[yysp-0];
         
       break;
							case  159 : 
         yyval = yyv[yysp-0];
         
       break;
							case  160 : 
         yyval = yyv[yysp-0];
         
       break;
							case  161 : 
         yyval = yyv[yysp-0];
         
       break;
							case  162 : 
         yyval = yyv[yysp-0];
         
       break;
							case  163 : 
         yyval = yyv[yysp-0];
         
       break;
							case  164 : 
         yyval = yyv[yysp-0];
         
       break;
							case  165 : 
         yyval = yyv[yysp-0];
         
       break;
							case  166 : 
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

					int yynacts   = 2454;
					int yyngotos  = 682;
					int yynstates = 275;
					int yynrules  = 166;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,15);yyac++; 
					yya[yyac] = new YYARec(259,16);yyac++; 
					yya[yyac] = new YYARec(262,17);yyac++; 
					yya[yyac] = new YYARec(264,18);yyac++; 
					yya[yyac] = new YYARec(265,19);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,26);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(340,41);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(337,69);yyac++; 
					yya[yyac] = new YYARec(340,70);yyac++; 
					yya[yyac] = new YYARec(337,69);yyac++; 
					yya[yyac] = new YYARec(340,70);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,86);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,87);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(341,88);yyac++; 
					yya[yyac] = new YYARec(342,89);yyac++; 
					yya[yyac] = new YYARec(257,15);yyac++; 
					yya[yyac] = new YYARec(259,16);yyac++; 
					yya[yyac] = new YYARec(262,17);yyac++; 
					yya[yyac] = new YYARec(264,18);yyac++; 
					yya[yyac] = new YYARec(265,19);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,26);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(340,41);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(340,41);yyac++; 
					yya[yyac] = new YYARec(340,41);yyac++; 
					yya[yyac] = new YYARec(340,41);yyac++; 
					yya[yyac] = new YYARec(340,41);yyac++; 
					yya[yyac] = new YYARec(341,88);yyac++; 
					yya[yyac] = new YYARec(337,69);yyac++; 
					yya[yyac] = new YYARec(340,70);yyac++; 
					yya[yyac] = new YYARec(258,98);yyac++; 
					yya[yyac] = new YYARec(263,99);yyac++; 
					yya[yyac] = new YYARec(266,-78 );yyac++; 
					yya[yyac] = new YYARec(267,-78 );yyac++; 
					yya[yyac] = new YYARec(268,-78 );yyac++; 
					yya[yyac] = new YYARec(269,-78 );yyac++; 
					yya[yyac] = new YYARec(270,-78 );yyac++; 
					yya[yyac] = new YYARec(271,-78 );yyac++; 
					yya[yyac] = new YYARec(272,-78 );yyac++; 
					yya[yyac] = new YYARec(273,-78 );yyac++; 
					yya[yyac] = new YYARec(274,-78 );yyac++; 
					yya[yyac] = new YYARec(275,-78 );yyac++; 
					yya[yyac] = new YYARec(276,-78 );yyac++; 
					yya[yyac] = new YYARec(277,-78 );yyac++; 
					yya[yyac] = new YYARec(278,-78 );yyac++; 
					yya[yyac] = new YYARec(279,-78 );yyac++; 
					yya[yyac] = new YYARec(280,-78 );yyac++; 
					yya[yyac] = new YYARec(281,-78 );yyac++; 
					yya[yyac] = new YYARec(282,-78 );yyac++; 
					yya[yyac] = new YYARec(283,-78 );yyac++; 
					yya[yyac] = new YYARec(284,-78 );yyac++; 
					yya[yyac] = new YYARec(285,-78 );yyac++; 
					yya[yyac] = new YYARec(286,-78 );yyac++; 
					yya[yyac] = new YYARec(287,-78 );yyac++; 
					yya[yyac] = new YYARec(288,-78 );yyac++; 
					yya[yyac] = new YYARec(289,-78 );yyac++; 
					yya[yyac] = new YYARec(290,-78 );yyac++; 
					yya[yyac] = new YYARec(306,-78 );yyac++; 
					yya[yyac] = new YYARec(307,-78 );yyac++; 
					yya[yyac] = new YYARec(311,-78 );yyac++; 
					yya[yyac] = new YYARec(318,-78 );yyac++; 
					yya[yyac] = new YYARec(319,-78 );yyac++; 
					yya[yyac] = new YYARec(320,-78 );yyac++; 
					yya[yyac] = new YYARec(321,-78 );yyac++; 
					yya[yyac] = new YYARec(322,-78 );yyac++; 
					yya[yyac] = new YYARec(323,-78 );yyac++; 
					yya[yyac] = new YYARec(324,-78 );yyac++; 
					yya[yyac] = new YYARec(325,-78 );yyac++; 
					yya[yyac] = new YYARec(326,-78 );yyac++; 
					yya[yyac] = new YYARec(327,-78 );yyac++; 
					yya[yyac] = new YYARec(328,-78 );yyac++; 
					yya[yyac] = new YYARec(329,-78 );yyac++; 
					yya[yyac] = new YYARec(330,-78 );yyac++; 
					yya[yyac] = new YYARec(331,-78 );yyac++; 
					yya[yyac] = new YYARec(336,-78 );yyac++; 
					yya[yyac] = new YYARec(337,-78 );yyac++; 
					yya[yyac] = new YYARec(338,-78 );yyac++; 
					yya[yyac] = new YYARec(339,-78 );yyac++; 
					yya[yyac] = new YYARec(340,-78 );yyac++; 
					yya[yyac] = new YYARec(341,-78 );yyac++; 
					yya[yyac] = new YYARec(342,-78 );yyac++; 
					yya[yyac] = new YYARec(263,99);yyac++; 
					yya[yyac] = new YYARec(341,-78 );yyac++; 
					yya[yyac] = new YYARec(263,99);yyac++; 
					yya[yyac] = new YYARec(341,-78 );yyac++; 
					yya[yyac] = new YYARec(337,102);yyac++; 
					yya[yyac] = new YYARec(339,103);yyac++; 
					yya[yyac] = new YYARec(335,104);yyac++; 
					yya[yyac] = new YYARec(258,-145 );yyac++; 
					yya[yyac] = new YYARec(263,-145 );yyac++; 
					yya[yyac] = new YYARec(266,-145 );yyac++; 
					yya[yyac] = new YYARec(267,-145 );yyac++; 
					yya[yyac] = new YYARec(268,-145 );yyac++; 
					yya[yyac] = new YYARec(269,-145 );yyac++; 
					yya[yyac] = new YYARec(270,-145 );yyac++; 
					yya[yyac] = new YYARec(271,-145 );yyac++; 
					yya[yyac] = new YYARec(272,-145 );yyac++; 
					yya[yyac] = new YYARec(273,-145 );yyac++; 
					yya[yyac] = new YYARec(274,-145 );yyac++; 
					yya[yyac] = new YYARec(275,-145 );yyac++; 
					yya[yyac] = new YYARec(276,-145 );yyac++; 
					yya[yyac] = new YYARec(277,-145 );yyac++; 
					yya[yyac] = new YYARec(278,-145 );yyac++; 
					yya[yyac] = new YYARec(279,-145 );yyac++; 
					yya[yyac] = new YYARec(280,-145 );yyac++; 
					yya[yyac] = new YYARec(281,-145 );yyac++; 
					yya[yyac] = new YYARec(282,-145 );yyac++; 
					yya[yyac] = new YYARec(283,-145 );yyac++; 
					yya[yyac] = new YYARec(284,-145 );yyac++; 
					yya[yyac] = new YYARec(285,-145 );yyac++; 
					yya[yyac] = new YYARec(286,-145 );yyac++; 
					yya[yyac] = new YYARec(287,-145 );yyac++; 
					yya[yyac] = new YYARec(288,-145 );yyac++; 
					yya[yyac] = new YYARec(289,-145 );yyac++; 
					yya[yyac] = new YYARec(290,-145 );yyac++; 
					yya[yyac] = new YYARec(293,-145 );yyac++; 
					yya[yyac] = new YYARec(294,-145 );yyac++; 
					yya[yyac] = new YYARec(295,-145 );yyac++; 
					yya[yyac] = new YYARec(296,-145 );yyac++; 
					yya[yyac] = new YYARec(297,-145 );yyac++; 
					yya[yyac] = new YYARec(299,-145 );yyac++; 
					yya[yyac] = new YYARec(300,-145 );yyac++; 
					yya[yyac] = new YYARec(301,-145 );yyac++; 
					yya[yyac] = new YYARec(302,-145 );yyac++; 
					yya[yyac] = new YYARec(303,-145 );yyac++; 
					yya[yyac] = new YYARec(304,-145 );yyac++; 
					yya[yyac] = new YYARec(305,-145 );yyac++; 
					yya[yyac] = new YYARec(306,-145 );yyac++; 
					yya[yyac] = new YYARec(307,-145 );yyac++; 
					yya[yyac] = new YYARec(308,-145 );yyac++; 
					yya[yyac] = new YYARec(309,-145 );yyac++; 
					yya[yyac] = new YYARec(310,-145 );yyac++; 
					yya[yyac] = new YYARec(311,-145 );yyac++; 
					yya[yyac] = new YYARec(313,-145 );yyac++; 
					yya[yyac] = new YYARec(314,-145 );yyac++; 
					yya[yyac] = new YYARec(315,-145 );yyac++; 
					yya[yyac] = new YYARec(316,-145 );yyac++; 
					yya[yyac] = new YYARec(317,-145 );yyac++; 
					yya[yyac] = new YYARec(318,-145 );yyac++; 
					yya[yyac] = new YYARec(319,-145 );yyac++; 
					yya[yyac] = new YYARec(320,-145 );yyac++; 
					yya[yyac] = new YYARec(321,-145 );yyac++; 
					yya[yyac] = new YYARec(322,-145 );yyac++; 
					yya[yyac] = new YYARec(323,-145 );yyac++; 
					yya[yyac] = new YYARec(324,-145 );yyac++; 
					yya[yyac] = new YYARec(325,-145 );yyac++; 
					yya[yyac] = new YYARec(326,-145 );yyac++; 
					yya[yyac] = new YYARec(327,-145 );yyac++; 
					yya[yyac] = new YYARec(328,-145 );yyac++; 
					yya[yyac] = new YYARec(329,-145 );yyac++; 
					yya[yyac] = new YYARec(330,-145 );yyac++; 
					yya[yyac] = new YYARec(331,-145 );yyac++; 
					yya[yyac] = new YYARec(336,-145 );yyac++; 
					yya[yyac] = new YYARec(337,-145 );yyac++; 
					yya[yyac] = new YYARec(338,-145 );yyac++; 
					yya[yyac] = new YYARec(339,-145 );yyac++; 
					yya[yyac] = new YYARec(340,-145 );yyac++; 
					yya[yyac] = new YYARec(341,-145 );yyac++; 
					yya[yyac] = new YYARec(342,-145 );yyac++; 
					yya[yyac] = new YYARec(258,105);yyac++; 
					yya[yyac] = new YYARec(258,106);yyac++; 
					yya[yyac] = new YYARec(258,107);yyac++; 
					yya[yyac] = new YYARec(258,108);yyac++; 
					yya[yyac] = new YYARec(263,109);yyac++; 
					yya[yyac] = new YYARec(258,110);yyac++; 
					yya[yyac] = new YYARec(258,111);yyac++; 
					yya[yyac] = new YYARec(263,99);yyac++; 
					yya[yyac] = new YYARec(341,-78 );yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(290,119);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,86);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,87);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(341,88);yyac++; 
					yya[yyac] = new YYARec(342,89);yyac++; 
					yya[yyac] = new YYARec(341,88);yyac++; 
					yya[yyac] = new YYARec(341,88);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(257,15);yyac++; 
					yya[yyac] = new YYARec(259,16);yyac++; 
					yya[yyac] = new YYARec(262,17);yyac++; 
					yya[yyac] = new YYARec(264,18);yyac++; 
					yya[yyac] = new YYARec(265,19);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,26);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(340,41);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(257,15);yyac++; 
					yya[yyac] = new YYARec(259,16);yyac++; 
					yya[yyac] = new YYARec(262,17);yyac++; 
					yya[yyac] = new YYARec(264,18);yyac++; 
					yya[yyac] = new YYARec(265,19);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,26);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(340,41);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,86);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,87);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(341,88);yyac++; 
					yya[yyac] = new YYARec(342,89);yyac++; 
					yya[yyac] = new YYARec(341,88);yyac++; 
					yya[yyac] = new YYARec(258,130);yyac++; 
					yya[yyac] = new YYARec(263,99);yyac++; 
					yya[yyac] = new YYARec(258,-78 );yyac++; 
					yya[yyac] = new YYARec(266,-78 );yyac++; 
					yya[yyac] = new YYARec(267,-78 );yyac++; 
					yya[yyac] = new YYARec(268,-78 );yyac++; 
					yya[yyac] = new YYARec(269,-78 );yyac++; 
					yya[yyac] = new YYARec(270,-78 );yyac++; 
					yya[yyac] = new YYARec(271,-78 );yyac++; 
					yya[yyac] = new YYARec(272,-78 );yyac++; 
					yya[yyac] = new YYARec(273,-78 );yyac++; 
					yya[yyac] = new YYARec(274,-78 );yyac++; 
					yya[yyac] = new YYARec(275,-78 );yyac++; 
					yya[yyac] = new YYARec(276,-78 );yyac++; 
					yya[yyac] = new YYARec(277,-78 );yyac++; 
					yya[yyac] = new YYARec(278,-78 );yyac++; 
					yya[yyac] = new YYARec(279,-78 );yyac++; 
					yya[yyac] = new YYARec(280,-78 );yyac++; 
					yya[yyac] = new YYARec(281,-78 );yyac++; 
					yya[yyac] = new YYARec(282,-78 );yyac++; 
					yya[yyac] = new YYARec(283,-78 );yyac++; 
					yya[yyac] = new YYARec(284,-78 );yyac++; 
					yya[yyac] = new YYARec(285,-78 );yyac++; 
					yya[yyac] = new YYARec(286,-78 );yyac++; 
					yya[yyac] = new YYARec(287,-78 );yyac++; 
					yya[yyac] = new YYARec(288,-78 );yyac++; 
					yya[yyac] = new YYARec(289,-78 );yyac++; 
					yya[yyac] = new YYARec(306,-78 );yyac++; 
					yya[yyac] = new YYARec(307,-78 );yyac++; 
					yya[yyac] = new YYARec(311,-78 );yyac++; 
					yya[yyac] = new YYARec(318,-78 );yyac++; 
					yya[yyac] = new YYARec(319,-78 );yyac++; 
					yya[yyac] = new YYARec(320,-78 );yyac++; 
					yya[yyac] = new YYARec(321,-78 );yyac++; 
					yya[yyac] = new YYARec(322,-78 );yyac++; 
					yya[yyac] = new YYARec(323,-78 );yyac++; 
					yya[yyac] = new YYARec(324,-78 );yyac++; 
					yya[yyac] = new YYARec(325,-78 );yyac++; 
					yya[yyac] = new YYARec(326,-78 );yyac++; 
					yya[yyac] = new YYARec(327,-78 );yyac++; 
					yya[yyac] = new YYARec(328,-78 );yyac++; 
					yya[yyac] = new YYARec(329,-78 );yyac++; 
					yya[yyac] = new YYARec(330,-78 );yyac++; 
					yya[yyac] = new YYARec(331,-78 );yyac++; 
					yya[yyac] = new YYARec(336,-78 );yyac++; 
					yya[yyac] = new YYARec(337,-78 );yyac++; 
					yya[yyac] = new YYARec(338,-78 );yyac++; 
					yya[yyac] = new YYARec(339,-78 );yyac++; 
					yya[yyac] = new YYARec(340,-78 );yyac++; 
					yya[yyac] = new YYARec(341,-78 );yyac++; 
					yya[yyac] = new YYARec(342,-78 );yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(290,142);yyac++; 
					yya[yyac] = new YYARec(312,143);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(332,144);yyac++; 
					yya[yyac] = new YYARec(333,145);yyac++; 
					yya[yyac] = new YYARec(334,146);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(291,-67 );yyac++; 
					yya[yyac] = new YYARec(258,147);yyac++; 
					yya[yyac] = new YYARec(258,149);yyac++; 
					yya[yyac] = new YYARec(263,99);yyac++; 
					yya[yyac] = new YYARec(306,-78 );yyac++; 
					yya[yyac] = new YYARec(307,-78 );yyac++; 
					yya[yyac] = new YYARec(311,-78 );yyac++; 
					yya[yyac] = new YYARec(337,-78 );yyac++; 
					yya[yyac] = new YYARec(260,150);yyac++; 
					yya[yyac] = new YYARec(261,151);yyac++; 
					yya[yyac] = new YYARec(258,152);yyac++; 
					yya[yyac] = new YYARec(263,99);yyac++; 
					yya[yyac] = new YYARec(306,-78 );yyac++; 
					yya[yyac] = new YYARec(307,-78 );yyac++; 
					yya[yyac] = new YYARec(311,-78 );yyac++; 
					yya[yyac] = new YYARec(337,-78 );yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,86);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,87);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(341,88);yyac++; 
					yya[yyac] = new YYARec(342,89);yyac++; 
					yya[yyac] = new YYARec(258,-71 );yyac++; 
					yya[yyac] = new YYARec(263,99);yyac++; 
					yya[yyac] = new YYARec(258,-70 );yyac++; 
					yya[yyac] = new YYARec(266,-78 );yyac++; 
					yya[yyac] = new YYARec(267,-78 );yyac++; 
					yya[yyac] = new YYARec(268,-78 );yyac++; 
					yya[yyac] = new YYARec(269,-78 );yyac++; 
					yya[yyac] = new YYARec(270,-78 );yyac++; 
					yya[yyac] = new YYARec(271,-78 );yyac++; 
					yya[yyac] = new YYARec(272,-78 );yyac++; 
					yya[yyac] = new YYARec(273,-78 );yyac++; 
					yya[yyac] = new YYARec(274,-78 );yyac++; 
					yya[yyac] = new YYARec(275,-78 );yyac++; 
					yya[yyac] = new YYARec(276,-78 );yyac++; 
					yya[yyac] = new YYARec(277,-78 );yyac++; 
					yya[yyac] = new YYARec(278,-78 );yyac++; 
					yya[yyac] = new YYARec(279,-78 );yyac++; 
					yya[yyac] = new YYARec(280,-78 );yyac++; 
					yya[yyac] = new YYARec(281,-78 );yyac++; 
					yya[yyac] = new YYARec(282,-78 );yyac++; 
					yya[yyac] = new YYARec(283,-78 );yyac++; 
					yya[yyac] = new YYARec(284,-78 );yyac++; 
					yya[yyac] = new YYARec(285,-78 );yyac++; 
					yya[yyac] = new YYARec(286,-78 );yyac++; 
					yya[yyac] = new YYARec(287,-78 );yyac++; 
					yya[yyac] = new YYARec(288,-78 );yyac++; 
					yya[yyac] = new YYARec(289,-78 );yyac++; 
					yya[yyac] = new YYARec(306,-78 );yyac++; 
					yya[yyac] = new YYARec(307,-78 );yyac++; 
					yya[yyac] = new YYARec(311,-78 );yyac++; 
					yya[yyac] = new YYARec(318,-78 );yyac++; 
					yya[yyac] = new YYARec(319,-78 );yyac++; 
					yya[yyac] = new YYARec(320,-78 );yyac++; 
					yya[yyac] = new YYARec(321,-78 );yyac++; 
					yya[yyac] = new YYARec(322,-78 );yyac++; 
					yya[yyac] = new YYARec(323,-78 );yyac++; 
					yya[yyac] = new YYARec(324,-78 );yyac++; 
					yya[yyac] = new YYARec(325,-78 );yyac++; 
					yya[yyac] = new YYARec(326,-78 );yyac++; 
					yya[yyac] = new YYARec(327,-78 );yyac++; 
					yya[yyac] = new YYARec(328,-78 );yyac++; 
					yya[yyac] = new YYARec(329,-78 );yyac++; 
					yya[yyac] = new YYARec(330,-78 );yyac++; 
					yya[yyac] = new YYARec(331,-78 );yyac++; 
					yya[yyac] = new YYARec(336,-78 );yyac++; 
					yya[yyac] = new YYARec(337,-78 );yyac++; 
					yya[yyac] = new YYARec(338,-78 );yyac++; 
					yya[yyac] = new YYARec(339,-78 );yyac++; 
					yya[yyac] = new YYARec(340,-78 );yyac++; 
					yya[yyac] = new YYARec(341,-78 );yyac++; 
					yya[yyac] = new YYARec(342,-78 );yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(290,142);yyac++; 
					yya[yyac] = new YYARec(312,143);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(332,144);yyac++; 
					yya[yyac] = new YYARec(333,145);yyac++; 
					yya[yyac] = new YYARec(334,146);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(260,-67 );yyac++; 
					yya[yyac] = new YYARec(261,-67 );yyac++; 
					yya[yyac] = new YYARec(291,-67 );yyac++; 
					yya[yyac] = new YYARec(258,157);yyac++; 
					yya[yyac] = new YYARec(292,158);yyac++; 
					yya[yyac] = new YYARec(258,-163 );yyac++; 
					yya[yyac] = new YYARec(263,-163 );yyac++; 
					yya[yyac] = new YYARec(266,-163 );yyac++; 
					yya[yyac] = new YYARec(267,-163 );yyac++; 
					yya[yyac] = new YYARec(268,-163 );yyac++; 
					yya[yyac] = new YYARec(269,-163 );yyac++; 
					yya[yyac] = new YYARec(270,-163 );yyac++; 
					yya[yyac] = new YYARec(271,-163 );yyac++; 
					yya[yyac] = new YYARec(272,-163 );yyac++; 
					yya[yyac] = new YYARec(273,-163 );yyac++; 
					yya[yyac] = new YYARec(274,-163 );yyac++; 
					yya[yyac] = new YYARec(275,-163 );yyac++; 
					yya[yyac] = new YYARec(276,-163 );yyac++; 
					yya[yyac] = new YYARec(277,-163 );yyac++; 
					yya[yyac] = new YYARec(278,-163 );yyac++; 
					yya[yyac] = new YYARec(279,-163 );yyac++; 
					yya[yyac] = new YYARec(280,-163 );yyac++; 
					yya[yyac] = new YYARec(281,-163 );yyac++; 
					yya[yyac] = new YYARec(282,-163 );yyac++; 
					yya[yyac] = new YYARec(283,-163 );yyac++; 
					yya[yyac] = new YYARec(284,-163 );yyac++; 
					yya[yyac] = new YYARec(285,-163 );yyac++; 
					yya[yyac] = new YYARec(286,-163 );yyac++; 
					yya[yyac] = new YYARec(287,-163 );yyac++; 
					yya[yyac] = new YYARec(288,-163 );yyac++; 
					yya[yyac] = new YYARec(289,-163 );yyac++; 
					yya[yyac] = new YYARec(306,-163 );yyac++; 
					yya[yyac] = new YYARec(307,-163 );yyac++; 
					yya[yyac] = new YYARec(311,-163 );yyac++; 
					yya[yyac] = new YYARec(318,-163 );yyac++; 
					yya[yyac] = new YYARec(319,-163 );yyac++; 
					yya[yyac] = new YYARec(320,-163 );yyac++; 
					yya[yyac] = new YYARec(321,-163 );yyac++; 
					yya[yyac] = new YYARec(322,-163 );yyac++; 
					yya[yyac] = new YYARec(323,-163 );yyac++; 
					yya[yyac] = new YYARec(324,-163 );yyac++; 
					yya[yyac] = new YYARec(325,-163 );yyac++; 
					yya[yyac] = new YYARec(326,-163 );yyac++; 
					yya[yyac] = new YYARec(327,-163 );yyac++; 
					yya[yyac] = new YYARec(328,-163 );yyac++; 
					yya[yyac] = new YYARec(329,-163 );yyac++; 
					yya[yyac] = new YYARec(330,-163 );yyac++; 
					yya[yyac] = new YYARec(331,-163 );yyac++; 
					yya[yyac] = new YYARec(336,-163 );yyac++; 
					yya[yyac] = new YYARec(337,-163 );yyac++; 
					yya[yyac] = new YYARec(338,-163 );yyac++; 
					yya[yyac] = new YYARec(339,-163 );yyac++; 
					yya[yyac] = new YYARec(340,-163 );yyac++; 
					yya[yyac] = new YYARec(341,-163 );yyac++; 
					yya[yyac] = new YYARec(342,-163 );yyac++; 
					yya[yyac] = new YYARec(292,-164 );yyac++; 
					yya[yyac] = new YYARec(291,159);yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(290,142);yyac++; 
					yya[yyac] = new YYARec(312,143);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(332,144);yyac++; 
					yya[yyac] = new YYARec(333,145);yyac++; 
					yya[yyac] = new YYARec(334,146);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(260,-67 );yyac++; 
					yya[yyac] = new YYARec(261,-67 );yyac++; 
					yya[yyac] = new YYARec(291,-67 );yyac++; 
					yya[yyac] = new YYARec(340,41);yyac++; 
					yya[yyac] = new YYARec(340,41);yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(290,142);yyac++; 
					yya[yyac] = new YYARec(312,143);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(332,144);yyac++; 
					yya[yyac] = new YYARec(333,145);yyac++; 
					yya[yyac] = new YYARec(334,146);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(291,-67 );yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(290,183);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(337,86);yyac++; 
					yya[yyac] = new YYARec(257,15);yyac++; 
					yya[yyac] = new YYARec(259,16);yyac++; 
					yya[yyac] = new YYARec(262,17);yyac++; 
					yya[yyac] = new YYARec(264,18);yyac++; 
					yya[yyac] = new YYARec(265,19);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,26);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(340,41);yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(337,86);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,86);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,87);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(341,88);yyac++; 
					yya[yyac] = new YYARec(342,89);yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(290,142);yyac++; 
					yya[yyac] = new YYARec(312,143);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(332,144);yyac++; 
					yya[yyac] = new YYARec(333,145);yyac++; 
					yya[yyac] = new YYARec(334,146);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(260,-67 );yyac++; 
					yya[yyac] = new YYARec(261,-67 );yyac++; 
					yya[yyac] = new YYARec(291,-67 );yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(290,142);yyac++; 
					yya[yyac] = new YYARec(312,143);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(332,144);yyac++; 
					yya[yyac] = new YYARec(333,145);yyac++; 
					yya[yyac] = new YYARec(334,146);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(260,-67 );yyac++; 
					yya[yyac] = new YYARec(261,-67 );yyac++; 
					yya[yyac] = new YYARec(291,-67 );yyac++; 
					yya[yyac] = new YYARec(258,194);yyac++; 
					yya[yyac] = new YYARec(258,195);yyac++; 
					yya[yyac] = new YYARec(291,196);yyac++; 
					yya[yyac] = new YYARec(298,197);yyac++; 
					yya[yyac] = new YYARec(258,-160 );yyac++; 
					yya[yyac] = new YYARec(290,-160 );yyac++; 
					yya[yyac] = new YYARec(293,-160 );yyac++; 
					yya[yyac] = new YYARec(294,-160 );yyac++; 
					yya[yyac] = new YYARec(295,-160 );yyac++; 
					yya[yyac] = new YYARec(296,-160 );yyac++; 
					yya[yyac] = new YYARec(297,-160 );yyac++; 
					yya[yyac] = new YYARec(299,-160 );yyac++; 
					yya[yyac] = new YYARec(300,-160 );yyac++; 
					yya[yyac] = new YYARec(301,-160 );yyac++; 
					yya[yyac] = new YYARec(302,-160 );yyac++; 
					yya[yyac] = new YYARec(303,-160 );yyac++; 
					yya[yyac] = new YYARec(304,-160 );yyac++; 
					yya[yyac] = new YYARec(305,-160 );yyac++; 
					yya[yyac] = new YYARec(306,-160 );yyac++; 
					yya[yyac] = new YYARec(307,-160 );yyac++; 
					yya[yyac] = new YYARec(308,-160 );yyac++; 
					yya[yyac] = new YYARec(309,-160 );yyac++; 
					yya[yyac] = new YYARec(310,-160 );yyac++; 
					yya[yyac] = new YYARec(313,-160 );yyac++; 
					yya[yyac] = new YYARec(314,-160 );yyac++; 
					yya[yyac] = new YYARec(315,-160 );yyac++; 
					yya[yyac] = new YYARec(316,-160 );yyac++; 
					yya[yyac] = new YYARec(317,-160 );yyac++; 
					yya[yyac] = new YYARec(335,-160 );yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(308,200);yyac++; 
					yya[yyac] = new YYARec(309,201);yyac++; 
					yya[yyac] = new YYARec(310,202);yyac++; 
					yya[yyac] = new YYARec(258,-94 );yyac++; 
					yya[yyac] = new YYARec(290,-94 );yyac++; 
					yya[yyac] = new YYARec(293,-94 );yyac++; 
					yya[yyac] = new YYARec(294,-94 );yyac++; 
					yya[yyac] = new YYARec(295,-94 );yyac++; 
					yya[yyac] = new YYARec(296,-94 );yyac++; 
					yya[yyac] = new YYARec(297,-94 );yyac++; 
					yya[yyac] = new YYARec(299,-94 );yyac++; 
					yya[yyac] = new YYARec(300,-94 );yyac++; 
					yya[yyac] = new YYARec(301,-94 );yyac++; 
					yya[yyac] = new YYARec(302,-94 );yyac++; 
					yya[yyac] = new YYARec(303,-94 );yyac++; 
					yya[yyac] = new YYARec(304,-94 );yyac++; 
					yya[yyac] = new YYARec(305,-94 );yyac++; 
					yya[yyac] = new YYARec(306,-94 );yyac++; 
					yya[yyac] = new YYARec(307,-94 );yyac++; 
					yya[yyac] = new YYARec(306,204);yyac++; 
					yya[yyac] = new YYARec(307,205);yyac++; 
					yya[yyac] = new YYARec(258,-92 );yyac++; 
					yya[yyac] = new YYARec(290,-92 );yyac++; 
					yya[yyac] = new YYARec(293,-92 );yyac++; 
					yya[yyac] = new YYARec(294,-92 );yyac++; 
					yya[yyac] = new YYARec(295,-92 );yyac++; 
					yya[yyac] = new YYARec(296,-92 );yyac++; 
					yya[yyac] = new YYARec(297,-92 );yyac++; 
					yya[yyac] = new YYARec(299,-92 );yyac++; 
					yya[yyac] = new YYARec(300,-92 );yyac++; 
					yya[yyac] = new YYARec(301,-92 );yyac++; 
					yya[yyac] = new YYARec(302,-92 );yyac++; 
					yya[yyac] = new YYARec(303,-92 );yyac++; 
					yya[yyac] = new YYARec(304,-92 );yyac++; 
					yya[yyac] = new YYARec(305,-92 );yyac++; 
					yya[yyac] = new YYARec(302,207);yyac++; 
					yya[yyac] = new YYARec(303,208);yyac++; 
					yya[yyac] = new YYARec(304,209);yyac++; 
					yya[yyac] = new YYARec(305,210);yyac++; 
					yya[yyac] = new YYARec(258,-90 );yyac++; 
					yya[yyac] = new YYARec(290,-90 );yyac++; 
					yya[yyac] = new YYARec(293,-90 );yyac++; 
					yya[yyac] = new YYARec(294,-90 );yyac++; 
					yya[yyac] = new YYARec(295,-90 );yyac++; 
					yya[yyac] = new YYARec(296,-90 );yyac++; 
					yya[yyac] = new YYARec(297,-90 );yyac++; 
					yya[yyac] = new YYARec(299,-90 );yyac++; 
					yya[yyac] = new YYARec(300,-90 );yyac++; 
					yya[yyac] = new YYARec(301,-90 );yyac++; 
					yya[yyac] = new YYARec(300,212);yyac++; 
					yya[yyac] = new YYARec(301,213);yyac++; 
					yya[yyac] = new YYARec(258,-89 );yyac++; 
					yya[yyac] = new YYARec(290,-89 );yyac++; 
					yya[yyac] = new YYARec(293,-89 );yyac++; 
					yya[yyac] = new YYARec(294,-89 );yyac++; 
					yya[yyac] = new YYARec(295,-89 );yyac++; 
					yya[yyac] = new YYARec(296,-89 );yyac++; 
					yya[yyac] = new YYARec(297,-89 );yyac++; 
					yya[yyac] = new YYARec(299,-89 );yyac++; 
					yya[yyac] = new YYARec(297,214);yyac++; 
					yya[yyac] = new YYARec(258,-87 );yyac++; 
					yya[yyac] = new YYARec(290,-87 );yyac++; 
					yya[yyac] = new YYARec(293,-87 );yyac++; 
					yya[yyac] = new YYARec(294,-87 );yyac++; 
					yya[yyac] = new YYARec(295,-87 );yyac++; 
					yya[yyac] = new YYARec(296,-87 );yyac++; 
					yya[yyac] = new YYARec(299,-87 );yyac++; 
					yya[yyac] = new YYARec(296,215);yyac++; 
					yya[yyac] = new YYARec(258,-85 );yyac++; 
					yya[yyac] = new YYARec(290,-85 );yyac++; 
					yya[yyac] = new YYARec(293,-85 );yyac++; 
					yya[yyac] = new YYARec(294,-85 );yyac++; 
					yya[yyac] = new YYARec(295,-85 );yyac++; 
					yya[yyac] = new YYARec(299,-85 );yyac++; 
					yya[yyac] = new YYARec(295,216);yyac++; 
					yya[yyac] = new YYARec(258,-83 );yyac++; 
					yya[yyac] = new YYARec(290,-83 );yyac++; 
					yya[yyac] = new YYARec(293,-83 );yyac++; 
					yya[yyac] = new YYARec(294,-83 );yyac++; 
					yya[yyac] = new YYARec(299,-83 );yyac++; 
					yya[yyac] = new YYARec(294,217);yyac++; 
					yya[yyac] = new YYARec(258,-81 );yyac++; 
					yya[yyac] = new YYARec(290,-81 );yyac++; 
					yya[yyac] = new YYARec(293,-81 );yyac++; 
					yya[yyac] = new YYARec(299,-81 );yyac++; 
					yya[yyac] = new YYARec(293,218);yyac++; 
					yya[yyac] = new YYARec(258,-79 );yyac++; 
					yya[yyac] = new YYARec(290,-79 );yyac++; 
					yya[yyac] = new YYARec(299,-79 );yyac++; 
					yya[yyac] = new YYARec(313,220);yyac++; 
					yya[yyac] = new YYARec(314,221);yyac++; 
					yya[yyac] = new YYARec(315,222);yyac++; 
					yya[yyac] = new YYARec(316,223);yyac++; 
					yya[yyac] = new YYARec(317,224);yyac++; 
					yya[yyac] = new YYARec(258,-103 );yyac++; 
					yya[yyac] = new YYARec(293,-103 );yyac++; 
					yya[yyac] = new YYARec(294,-103 );yyac++; 
					yya[yyac] = new YYARec(295,-103 );yyac++; 
					yya[yyac] = new YYARec(296,-103 );yyac++; 
					yya[yyac] = new YYARec(297,-103 );yyac++; 
					yya[yyac] = new YYARec(300,-103 );yyac++; 
					yya[yyac] = new YYARec(301,-103 );yyac++; 
					yya[yyac] = new YYARec(302,-103 );yyac++; 
					yya[yyac] = new YYARec(303,-103 );yyac++; 
					yya[yyac] = new YYARec(304,-103 );yyac++; 
					yya[yyac] = new YYARec(305,-103 );yyac++; 
					yya[yyac] = new YYARec(306,-103 );yyac++; 
					yya[yyac] = new YYARec(307,-103 );yyac++; 
					yya[yyac] = new YYARec(308,-103 );yyac++; 
					yya[yyac] = new YYARec(309,-103 );yyac++; 
					yya[yyac] = new YYARec(310,-103 );yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(290,142);yyac++; 
					yya[yyac] = new YYARec(312,143);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(332,144);yyac++; 
					yya[yyac] = new YYARec(333,145);yyac++; 
					yya[yyac] = new YYARec(334,146);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(291,-67 );yyac++; 
					yya[yyac] = new YYARec(290,227);yyac++; 
					yya[yyac] = new YYARec(290,228);yyac++; 
					yya[yyac] = new YYARec(337,102);yyac++; 
					yya[yyac] = new YYARec(263,229);yyac++; 
					yya[yyac] = new YYARec(261,230);yyac++; 
					yya[yyac] = new YYARec(263,231);yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(290,142);yyac++; 
					yya[yyac] = new YYARec(312,143);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(332,144);yyac++; 
					yya[yyac] = new YYARec(333,145);yyac++; 
					yya[yyac] = new YYARec(334,146);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(260,-67 );yyac++; 
					yya[yyac] = new YYARec(261,-67 );yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(290,142);yyac++; 
					yya[yyac] = new YYARec(312,143);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(332,144);yyac++; 
					yya[yyac] = new YYARec(333,145);yyac++; 
					yya[yyac] = new YYARec(334,146);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(260,-67 );yyac++; 
					yya[yyac] = new YYARec(261,-67 );yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(290,142);yyac++; 
					yya[yyac] = new YYARec(312,143);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(332,144);yyac++; 
					yya[yyac] = new YYARec(333,145);yyac++; 
					yya[yyac] = new YYARec(334,146);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(260,-67 );yyac++; 
					yya[yyac] = new YYARec(261,-67 );yyac++; 
					yya[yyac] = new YYARec(291,-67 );yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(298,180);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(336,85);yyac++; 
					yya[yyac] = new YYARec(337,181);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(339,182);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(299,247);yyac++; 
					yya[yyac] = new YYARec(291,248);yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(290,142);yyac++; 
					yya[yyac] = new YYARec(312,143);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(332,144);yyac++; 
					yya[yyac] = new YYARec(333,145);yyac++; 
					yya[yyac] = new YYARec(334,146);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(291,-67 );yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(290,142);yyac++; 
					yya[yyac] = new YYARec(312,143);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(332,144);yyac++; 
					yya[yyac] = new YYARec(333,145);yyac++; 
					yya[yyac] = new YYARec(334,146);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(291,-67 );yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(337,86);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(337,86);yyac++; 
					yya[yyac] = new YYARec(260,253);yyac++; 
					yya[yyac] = new YYARec(261,254);yyac++; 
					yya[yyac] = new YYARec(299,255);yyac++; 
					yya[yyac] = new YYARec(308,200);yyac++; 
					yya[yyac] = new YYARec(309,201);yyac++; 
					yya[yyac] = new YYARec(310,202);yyac++; 
					yya[yyac] = new YYARec(258,-95 );yyac++; 
					yya[yyac] = new YYARec(290,-95 );yyac++; 
					yya[yyac] = new YYARec(293,-95 );yyac++; 
					yya[yyac] = new YYARec(294,-95 );yyac++; 
					yya[yyac] = new YYARec(295,-95 );yyac++; 
					yya[yyac] = new YYARec(296,-95 );yyac++; 
					yya[yyac] = new YYARec(297,-95 );yyac++; 
					yya[yyac] = new YYARec(299,-95 );yyac++; 
					yya[yyac] = new YYARec(300,-95 );yyac++; 
					yya[yyac] = new YYARec(301,-95 );yyac++; 
					yya[yyac] = new YYARec(302,-95 );yyac++; 
					yya[yyac] = new YYARec(303,-95 );yyac++; 
					yya[yyac] = new YYARec(304,-95 );yyac++; 
					yya[yyac] = new YYARec(305,-95 );yyac++; 
					yya[yyac] = new YYARec(306,-95 );yyac++; 
					yya[yyac] = new YYARec(307,-95 );yyac++; 
					yya[yyac] = new YYARec(306,204);yyac++; 
					yya[yyac] = new YYARec(307,205);yyac++; 
					yya[yyac] = new YYARec(258,-93 );yyac++; 
					yya[yyac] = new YYARec(290,-93 );yyac++; 
					yya[yyac] = new YYARec(293,-93 );yyac++; 
					yya[yyac] = new YYARec(294,-93 );yyac++; 
					yya[yyac] = new YYARec(295,-93 );yyac++; 
					yya[yyac] = new YYARec(296,-93 );yyac++; 
					yya[yyac] = new YYARec(297,-93 );yyac++; 
					yya[yyac] = new YYARec(299,-93 );yyac++; 
					yya[yyac] = new YYARec(300,-93 );yyac++; 
					yya[yyac] = new YYARec(301,-93 );yyac++; 
					yya[yyac] = new YYARec(302,-93 );yyac++; 
					yya[yyac] = new YYARec(303,-93 );yyac++; 
					yya[yyac] = new YYARec(304,-93 );yyac++; 
					yya[yyac] = new YYARec(305,-93 );yyac++; 
					yya[yyac] = new YYARec(302,207);yyac++; 
					yya[yyac] = new YYARec(303,208);yyac++; 
					yya[yyac] = new YYARec(304,209);yyac++; 
					yya[yyac] = new YYARec(305,210);yyac++; 
					yya[yyac] = new YYARec(258,-91 );yyac++; 
					yya[yyac] = new YYARec(290,-91 );yyac++; 
					yya[yyac] = new YYARec(293,-91 );yyac++; 
					yya[yyac] = new YYARec(294,-91 );yyac++; 
					yya[yyac] = new YYARec(295,-91 );yyac++; 
					yya[yyac] = new YYARec(296,-91 );yyac++; 
					yya[yyac] = new YYARec(297,-91 );yyac++; 
					yya[yyac] = new YYARec(299,-91 );yyac++; 
					yya[yyac] = new YYARec(300,-91 );yyac++; 
					yya[yyac] = new YYARec(301,-91 );yyac++; 
					yya[yyac] = new YYARec(300,212);yyac++; 
					yya[yyac] = new YYARec(301,213);yyac++; 
					yya[yyac] = new YYARec(258,-88 );yyac++; 
					yya[yyac] = new YYARec(290,-88 );yyac++; 
					yya[yyac] = new YYARec(293,-88 );yyac++; 
					yya[yyac] = new YYARec(294,-88 );yyac++; 
					yya[yyac] = new YYARec(295,-88 );yyac++; 
					yya[yyac] = new YYARec(296,-88 );yyac++; 
					yya[yyac] = new YYARec(297,-88 );yyac++; 
					yya[yyac] = new YYARec(299,-88 );yyac++; 
					yya[yyac] = new YYARec(297,214);yyac++; 
					yya[yyac] = new YYARec(258,-86 );yyac++; 
					yya[yyac] = new YYARec(290,-86 );yyac++; 
					yya[yyac] = new YYARec(293,-86 );yyac++; 
					yya[yyac] = new YYARec(294,-86 );yyac++; 
					yya[yyac] = new YYARec(295,-86 );yyac++; 
					yya[yyac] = new YYARec(296,-86 );yyac++; 
					yya[yyac] = new YYARec(299,-86 );yyac++; 
					yya[yyac] = new YYARec(296,215);yyac++; 
					yya[yyac] = new YYARec(258,-84 );yyac++; 
					yya[yyac] = new YYARec(290,-84 );yyac++; 
					yya[yyac] = new YYARec(293,-84 );yyac++; 
					yya[yyac] = new YYARec(294,-84 );yyac++; 
					yya[yyac] = new YYARec(295,-84 );yyac++; 
					yya[yyac] = new YYARec(299,-84 );yyac++; 
					yya[yyac] = new YYARec(295,216);yyac++; 
					yya[yyac] = new YYARec(258,-82 );yyac++; 
					yya[yyac] = new YYARec(290,-82 );yyac++; 
					yya[yyac] = new YYARec(293,-82 );yyac++; 
					yya[yyac] = new YYARec(294,-82 );yyac++; 
					yya[yyac] = new YYARec(299,-82 );yyac++; 
					yya[yyac] = new YYARec(294,217);yyac++; 
					yya[yyac] = new YYARec(258,-80 );yyac++; 
					yya[yyac] = new YYARec(290,-80 );yyac++; 
					yya[yyac] = new YYARec(293,-80 );yyac++; 
					yya[yyac] = new YYARec(299,-80 );yyac++; 
					yya[yyac] = new YYARec(291,256);yyac++; 
					yya[yyac] = new YYARec(291,257);yyac++; 
					yya[yyac] = new YYARec(263,258);yyac++; 
					yya[yyac] = new YYARec(258,259);yyac++; 
					yya[yyac] = new YYARec(263,260);yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(266,20);yyac++; 
					yya[yyac] = new YYARec(267,21);yyac++; 
					yya[yyac] = new YYARec(268,22);yyac++; 
					yya[yyac] = new YYARec(269,23);yyac++; 
					yya[yyac] = new YYARec(270,24);yyac++; 
					yya[yyac] = new YYARec(271,25);yyac++; 
					yya[yyac] = new YYARec(272,48);yyac++; 
					yya[yyac] = new YYARec(273,49);yyac++; 
					yya[yyac] = new YYARec(274,50);yyac++; 
					yya[yyac] = new YYARec(275,51);yyac++; 
					yya[yyac] = new YYARec(276,27);yyac++; 
					yya[yyac] = new YYARec(277,28);yyac++; 
					yya[yyac] = new YYARec(278,29);yyac++; 
					yya[yyac] = new YYARec(279,30);yyac++; 
					yya[yyac] = new YYARec(280,31);yyac++; 
					yya[yyac] = new YYARec(281,32);yyac++; 
					yya[yyac] = new YYARec(282,33);yyac++; 
					yya[yyac] = new YYARec(283,34);yyac++; 
					yya[yyac] = new YYARec(284,35);yyac++; 
					yya[yyac] = new YYARec(285,36);yyac++; 
					yya[yyac] = new YYARec(286,37);yyac++; 
					yya[yyac] = new YYARec(287,38);yyac++; 
					yya[yyac] = new YYARec(288,39);yyac++; 
					yya[yyac] = new YYARec(289,40);yyac++; 
					yya[yyac] = new YYARec(290,142);yyac++; 
					yya[yyac] = new YYARec(312,143);yyac++; 
					yya[yyac] = new YYARec(318,52);yyac++; 
					yya[yyac] = new YYARec(319,53);yyac++; 
					yya[yyac] = new YYARec(320,54);yyac++; 
					yya[yyac] = new YYARec(321,55);yyac++; 
					yya[yyac] = new YYARec(322,56);yyac++; 
					yya[yyac] = new YYARec(323,57);yyac++; 
					yya[yyac] = new YYARec(324,58);yyac++; 
					yya[yyac] = new YYARec(325,59);yyac++; 
					yya[yyac] = new YYARec(326,60);yyac++; 
					yya[yyac] = new YYARec(327,61);yyac++; 
					yya[yyac] = new YYARec(328,62);yyac++; 
					yya[yyac] = new YYARec(329,63);yyac++; 
					yya[yyac] = new YYARec(330,64);yyac++; 
					yya[yyac] = new YYARec(331,65);yyac++; 
					yya[yyac] = new YYARec(332,144);yyac++; 
					yya[yyac] = new YYARec(333,145);yyac++; 
					yya[yyac] = new YYARec(334,146);yyac++; 
					yya[yyac] = new YYARec(338,66);yyac++; 
					yya[yyac] = new YYARec(340,67);yyac++; 
					yya[yyac] = new YYARec(261,-67 );yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(337,86);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(337,86);yyac++; 
					yya[yyac] = new YYARec(261,264);yyac++; 
					yya[yyac] = new YYARec(263,265);yyac++; 
					yya[yyac] = new YYARec(263,266);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(337,86);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(337,86);yyac++; 
					yya[yyac] = new YYARec(258,269);yyac++; 
					yya[yyac] = new YYARec(263,270);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(337,86);yyac++; 
					yya[yyac] = new YYARec(263,272);yyac++; 
					yya[yyac] = new YYARec(306,82);yyac++; 
					yya[yyac] = new YYARec(307,83);yyac++; 
					yya[yyac] = new YYARec(311,84);yyac++; 
					yya[yyac] = new YYARec(337,86);yyac++; 
					yya[yyac] = new YYARec(258,274);yyac++;

					yyg[yygc] = new YYARec(-29,1);yygc++; 
					yyg[yygc] = new YYARec(-27,2);yygc++; 
					yyg[yygc] = new YYARec(-26,3);yygc++; 
					yyg[yygc] = new YYARec(-22,4);yygc++; 
					yyg[yygc] = new YYARec(-11,5);yygc++; 
					yyg[yygc] = new YYARec(-10,6);yygc++; 
					yyg[yygc] = new YYARec(-9,7);yygc++; 
					yyg[yygc] = new YYARec(-8,8);yygc++; 
					yyg[yygc] = new YYARec(-7,9);yygc++; 
					yyg[yygc] = new YYARec(-6,10);yygc++; 
					yyg[yygc] = new YYARec(-5,11);yygc++; 
					yyg[yygc] = new YYARec(-4,12);yygc++; 
					yyg[yygc] = new YYARec(-3,13);yygc++; 
					yyg[yygc] = new YYARec(-2,14);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-30,44);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-23,68);yygc++; 
					yyg[yygc] = new YYARec(-23,71);yygc++; 
					yyg[yygc] = new YYARec(-58,72);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-53,73);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-25,76);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-21,77);yygc++; 
					yyg[yygc] = new YYARec(-20,78);yygc++; 
					yyg[yygc] = new YYARec(-19,79);yygc++; 
					yyg[yygc] = new YYARec(-18,80);yygc++; 
					yyg[yygc] = new YYARec(-17,81);yygc++; 
					yyg[yygc] = new YYARec(-29,1);yygc++; 
					yyg[yygc] = new YYARec(-27,2);yygc++; 
					yyg[yygc] = new YYARec(-26,3);yygc++; 
					yyg[yygc] = new YYARec(-22,4);yygc++; 
					yyg[yygc] = new YYARec(-11,5);yygc++; 
					yyg[yygc] = new YYARec(-10,6);yygc++; 
					yyg[yygc] = new YYARec(-9,7);yygc++; 
					yyg[yygc] = new YYARec(-8,8);yygc++; 
					yyg[yygc] = new YYARec(-7,9);yygc++; 
					yyg[yygc] = new YYARec(-6,10);yygc++; 
					yyg[yygc] = new YYARec(-5,11);yygc++; 
					yyg[yygc] = new YYARec(-4,12);yygc++; 
					yyg[yygc] = new YYARec(-3,90);yygc++; 
					yyg[yygc] = new YYARec(-11,91);yygc++; 
					yyg[yygc] = new YYARec(-11,92);yygc++; 
					yyg[yygc] = new YYARec(-11,93);yygc++; 
					yyg[yygc] = new YYARec(-11,94);yygc++; 
					yyg[yygc] = new YYARec(-17,95);yygc++; 
					yyg[yygc] = new YYARec(-23,96);yygc++; 
					yyg[yygc] = new YYARec(-24,97);yygc++; 
					yyg[yygc] = new YYARec(-24,100);yygc++; 
					yyg[yygc] = new YYARec(-24,101);yygc++; 
					yyg[yygc] = new YYARec(-24,112);yygc++; 
					yyg[yygc] = new YYARec(-58,72);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-53,73);yygc++; 
					yyg[yygc] = new YYARec(-31,113);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-25,76);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-21,114);yygc++; 
					yyg[yygc] = new YYARec(-20,115);yygc++; 
					yyg[yygc] = new YYARec(-19,116);yygc++; 
					yyg[yygc] = new YYARec(-17,117);yygc++; 
					yyg[yygc] = new YYARec(-16,118);yygc++; 
					yyg[yygc] = new YYARec(-17,120);yygc++; 
					yyg[yygc] = new YYARec(-17,121);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-36,122);yygc++; 
					yyg[yygc] = new YYARec(-30,123);yygc++; 
					yyg[yygc] = new YYARec(-29,124);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-29,1);yygc++; 
					yyg[yygc] = new YYARec(-27,2);yygc++; 
					yyg[yygc] = new YYARec(-26,3);yygc++; 
					yyg[yygc] = new YYARec(-22,4);yygc++; 
					yyg[yygc] = new YYARec(-12,125);yygc++; 
					yyg[yygc] = new YYARec(-11,5);yygc++; 
					yyg[yygc] = new YYARec(-10,6);yygc++; 
					yyg[yygc] = new YYARec(-9,7);yygc++; 
					yyg[yygc] = new YYARec(-8,8);yygc++; 
					yyg[yygc] = new YYARec(-7,9);yygc++; 
					yyg[yygc] = new YYARec(-6,10);yygc++; 
					yyg[yygc] = new YYARec(-5,11);yygc++; 
					yyg[yygc] = new YYARec(-4,12);yygc++; 
					yyg[yygc] = new YYARec(-3,126);yygc++; 
					yyg[yygc] = new YYARec(-29,1);yygc++; 
					yyg[yygc] = new YYARec(-27,2);yygc++; 
					yyg[yygc] = new YYARec(-26,3);yygc++; 
					yyg[yygc] = new YYARec(-22,4);yygc++; 
					yyg[yygc] = new YYARec(-12,127);yygc++; 
					yyg[yygc] = new YYARec(-11,5);yygc++; 
					yyg[yygc] = new YYARec(-10,6);yygc++; 
					yyg[yygc] = new YYARec(-9,7);yygc++; 
					yyg[yygc] = new YYARec(-8,8);yygc++; 
					yyg[yygc] = new YYARec(-7,9);yygc++; 
					yyg[yygc] = new YYARec(-6,10);yygc++; 
					yyg[yygc] = new YYARec(-5,11);yygc++; 
					yyg[yygc] = new YYARec(-4,12);yygc++; 
					yyg[yygc] = new YYARec(-3,126);yygc++; 
					yyg[yygc] = new YYARec(-58,72);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-53,73);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-25,76);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-21,114);yygc++; 
					yyg[yygc] = new YYARec(-20,115);yygc++; 
					yyg[yygc] = new YYARec(-19,116);yygc++; 
					yyg[yygc] = new YYARec(-17,117);yygc++; 
					yyg[yygc] = new YYARec(-16,128);yygc++; 
					yyg[yygc] = new YYARec(-17,129);yygc++; 
					yyg[yygc] = new YYARec(-24,131);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-34,134);yygc++; 
					yyg[yygc] = new YYARec(-33,135);yygc++; 
					yyg[yygc] = new YYARec(-32,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-29,124);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-15,138);yygc++; 
					yyg[yygc] = new YYARec(-13,139);yygc++; 
					yyg[yygc] = new YYARec(-24,148);yygc++; 
					yyg[yygc] = new YYARec(-24,153);yygc++; 
					yyg[yygc] = new YYARec(-58,72);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-53,73);yygc++; 
					yyg[yygc] = new YYARec(-31,154);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-25,76);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-21,114);yygc++; 
					yyg[yygc] = new YYARec(-20,115);yygc++; 
					yyg[yygc] = new YYARec(-19,116);yygc++; 
					yyg[yygc] = new YYARec(-17,117);yygc++; 
					yyg[yygc] = new YYARec(-16,118);yygc++; 
					yyg[yygc] = new YYARec(-24,155);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-34,134);yygc++; 
					yyg[yygc] = new YYARec(-33,135);yygc++; 
					yyg[yygc] = new YYARec(-32,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-29,124);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-15,156);yygc++; 
					yyg[yygc] = new YYARec(-13,139);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-34,134);yygc++; 
					yyg[yygc] = new YYARec(-33,135);yygc++; 
					yyg[yygc] = new YYARec(-32,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-29,124);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-15,160);yygc++; 
					yyg[yygc] = new YYARec(-13,139);yygc++; 
					yyg[yygc] = new YYARec(-11,161);yygc++; 
					yyg[yygc] = new YYARec(-11,162);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-34,134);yygc++; 
					yyg[yygc] = new YYARec(-33,135);yygc++; 
					yyg[yygc] = new YYARec(-32,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-29,124);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-15,163);yygc++; 
					yyg[yygc] = new YYARec(-13,139);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,168);yygc++; 
					yyg[yygc] = new YYARec(-48,169);yygc++; 
					yyg[yygc] = new YYARec(-46,170);yygc++; 
					yyg[yygc] = new YYARec(-44,171);yygc++; 
					yyg[yygc] = new YYARec(-43,172);yygc++; 
					yyg[yygc] = new YYARec(-42,173);yygc++; 
					yyg[yygc] = new YYARec(-41,174);yygc++; 
					yyg[yygc] = new YYARec(-40,175);yygc++; 
					yyg[yygc] = new YYARec(-39,176);yygc++; 
					yyg[yygc] = new YYARec(-38,177);yygc++; 
					yyg[yygc] = new YYARec(-37,178);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,179);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,168);yygc++; 
					yyg[yygc] = new YYARec(-48,169);yygc++; 
					yyg[yygc] = new YYARec(-46,170);yygc++; 
					yyg[yygc] = new YYARec(-44,171);yygc++; 
					yyg[yygc] = new YYARec(-43,172);yygc++; 
					yyg[yygc] = new YYARec(-42,173);yygc++; 
					yyg[yygc] = new YYARec(-41,174);yygc++; 
					yyg[yygc] = new YYARec(-40,175);yygc++; 
					yyg[yygc] = new YYARec(-39,176);yygc++; 
					yyg[yygc] = new YYARec(-38,177);yygc++; 
					yyg[yygc] = new YYARec(-37,184);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,185);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,168);yygc++; 
					yyg[yygc] = new YYARec(-48,169);yygc++; 
					yyg[yygc] = new YYARec(-46,170);yygc++; 
					yyg[yygc] = new YYARec(-44,171);yygc++; 
					yyg[yygc] = new YYARec(-43,172);yygc++; 
					yyg[yygc] = new YYARec(-42,173);yygc++; 
					yyg[yygc] = new YYARec(-41,174);yygc++; 
					yyg[yygc] = new YYARec(-40,175);yygc++; 
					yyg[yygc] = new YYARec(-39,176);yygc++; 
					yyg[yygc] = new YYARec(-38,177);yygc++; 
					yyg[yygc] = new YYARec(-37,186);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,185);yygc++; 
					yyg[yygc] = new YYARec(-53,187);yygc++; 
					yyg[yygc] = new YYARec(-25,188);yygc++; 
					yyg[yygc] = new YYARec(-29,1);yygc++; 
					yyg[yygc] = new YYARec(-27,2);yygc++; 
					yyg[yygc] = new YYARec(-26,3);yygc++; 
					yyg[yygc] = new YYARec(-22,4);yygc++; 
					yyg[yygc] = new YYARec(-11,5);yygc++; 
					yyg[yygc] = new YYARec(-10,6);yygc++; 
					yyg[yygc] = new YYARec(-9,7);yygc++; 
					yyg[yygc] = new YYARec(-8,8);yygc++; 
					yyg[yygc] = new YYARec(-7,9);yygc++; 
					yyg[yygc] = new YYARec(-6,10);yygc++; 
					yyg[yygc] = new YYARec(-5,11);yygc++; 
					yyg[yygc] = new YYARec(-4,12);yygc++; 
					yyg[yygc] = new YYARec(-3,189);yygc++; 
					yyg[yygc] = new YYARec(-53,187);yygc++; 
					yyg[yygc] = new YYARec(-25,190);yygc++; 
					yyg[yygc] = new YYARec(-58,72);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-53,73);yygc++; 
					yyg[yygc] = new YYARec(-31,191);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-25,76);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-21,114);yygc++; 
					yyg[yygc] = new YYARec(-20,115);yygc++; 
					yyg[yygc] = new YYARec(-19,116);yygc++; 
					yyg[yygc] = new YYARec(-17,117);yygc++; 
					yyg[yygc] = new YYARec(-16,118);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-34,134);yygc++; 
					yyg[yygc] = new YYARec(-33,135);yygc++; 
					yyg[yygc] = new YYARec(-32,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-29,124);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-15,192);yygc++; 
					yyg[yygc] = new YYARec(-13,139);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-34,134);yygc++; 
					yyg[yygc] = new YYARec(-33,135);yygc++; 
					yyg[yygc] = new YYARec(-32,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-29,124);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-15,193);yygc++; 
					yyg[yygc] = new YYARec(-13,139);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,198);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,185);yygc++; 
					yyg[yygc] = new YYARec(-51,199);yygc++; 
					yyg[yygc] = new YYARec(-49,203);yygc++; 
					yyg[yygc] = new YYARec(-47,206);yygc++; 
					yyg[yygc] = new YYARec(-45,211);yygc++; 
					yyg[yygc] = new YYARec(-56,219);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,168);yygc++; 
					yyg[yygc] = new YYARec(-48,169);yygc++; 
					yyg[yygc] = new YYARec(-46,170);yygc++; 
					yyg[yygc] = new YYARec(-44,171);yygc++; 
					yyg[yygc] = new YYARec(-43,172);yygc++; 
					yyg[yygc] = new YYARec(-42,173);yygc++; 
					yyg[yygc] = new YYARec(-41,174);yygc++; 
					yyg[yygc] = new YYARec(-40,175);yygc++; 
					yyg[yygc] = new YYARec(-39,176);yygc++; 
					yyg[yygc] = new YYARec(-38,177);yygc++; 
					yyg[yygc] = new YYARec(-37,225);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,185);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-34,134);yygc++; 
					yyg[yygc] = new YYARec(-33,135);yygc++; 
					yyg[yygc] = new YYARec(-32,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-29,124);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-15,226);yygc++; 
					yyg[yygc] = new YYARec(-13,139);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-34,134);yygc++; 
					yyg[yygc] = new YYARec(-33,135);yygc++; 
					yyg[yygc] = new YYARec(-32,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-29,124);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-15,232);yygc++; 
					yyg[yygc] = new YYARec(-14,233);yygc++; 
					yyg[yygc] = new YYARec(-13,139);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-34,134);yygc++; 
					yyg[yygc] = new YYARec(-33,135);yygc++; 
					yyg[yygc] = new YYARec(-32,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-29,124);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-15,232);yygc++; 
					yyg[yygc] = new YYARec(-14,234);yygc++; 
					yyg[yygc] = new YYARec(-13,139);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-34,134);yygc++; 
					yyg[yygc] = new YYARec(-33,135);yygc++; 
					yyg[yygc] = new YYARec(-32,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-29,124);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-15,235);yygc++; 
					yyg[yygc] = new YYARec(-13,139);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,168);yygc++; 
					yyg[yygc] = new YYARec(-48,169);yygc++; 
					yyg[yygc] = new YYARec(-46,170);yygc++; 
					yyg[yygc] = new YYARec(-44,171);yygc++; 
					yyg[yygc] = new YYARec(-43,172);yygc++; 
					yyg[yygc] = new YYARec(-42,173);yygc++; 
					yyg[yygc] = new YYARec(-41,174);yygc++; 
					yyg[yygc] = new YYARec(-40,175);yygc++; 
					yyg[yygc] = new YYARec(-39,176);yygc++; 
					yyg[yygc] = new YYARec(-38,177);yygc++; 
					yyg[yygc] = new YYARec(-37,236);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,185);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,237);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,185);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,168);yygc++; 
					yyg[yygc] = new YYARec(-48,238);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,185);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,168);yygc++; 
					yyg[yygc] = new YYARec(-48,169);yygc++; 
					yyg[yygc] = new YYARec(-46,239);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,185);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,168);yygc++; 
					yyg[yygc] = new YYARec(-48,169);yygc++; 
					yyg[yygc] = new YYARec(-46,170);yygc++; 
					yyg[yygc] = new YYARec(-44,240);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,185);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,168);yygc++; 
					yyg[yygc] = new YYARec(-48,169);yygc++; 
					yyg[yygc] = new YYARec(-46,170);yygc++; 
					yyg[yygc] = new YYARec(-44,171);yygc++; 
					yyg[yygc] = new YYARec(-43,241);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,185);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,168);yygc++; 
					yyg[yygc] = new YYARec(-48,169);yygc++; 
					yyg[yygc] = new YYARec(-46,170);yygc++; 
					yyg[yygc] = new YYARec(-44,171);yygc++; 
					yyg[yygc] = new YYARec(-43,172);yygc++; 
					yyg[yygc] = new YYARec(-42,242);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,185);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,168);yygc++; 
					yyg[yygc] = new YYARec(-48,169);yygc++; 
					yyg[yygc] = new YYARec(-46,170);yygc++; 
					yyg[yygc] = new YYARec(-44,171);yygc++; 
					yyg[yygc] = new YYARec(-43,172);yygc++; 
					yyg[yygc] = new YYARec(-42,173);yygc++; 
					yyg[yygc] = new YYARec(-41,243);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,185);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,168);yygc++; 
					yyg[yygc] = new YYARec(-48,169);yygc++; 
					yyg[yygc] = new YYARec(-46,170);yygc++; 
					yyg[yygc] = new YYARec(-44,171);yygc++; 
					yyg[yygc] = new YYARec(-43,172);yygc++; 
					yyg[yygc] = new YYARec(-42,173);yygc++; 
					yyg[yygc] = new YYARec(-41,174);yygc++; 
					yyg[yygc] = new YYARec(-40,244);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,185);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,168);yygc++; 
					yyg[yygc] = new YYARec(-48,169);yygc++; 
					yyg[yygc] = new YYARec(-46,170);yygc++; 
					yyg[yygc] = new YYARec(-44,171);yygc++; 
					yyg[yygc] = new YYARec(-43,172);yygc++; 
					yyg[yygc] = new YYARec(-42,173);yygc++; 
					yyg[yygc] = new YYARec(-41,174);yygc++; 
					yyg[yygc] = new YYARec(-40,175);yygc++; 
					yyg[yygc] = new YYARec(-39,245);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,185);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-55,164);yygc++; 
					yyg[yygc] = new YYARec(-54,165);yygc++; 
					yyg[yygc] = new YYARec(-53,166);yygc++; 
					yyg[yygc] = new YYARec(-52,167);yygc++; 
					yyg[yygc] = new YYARec(-50,168);yygc++; 
					yyg[yygc] = new YYARec(-48,169);yygc++; 
					yyg[yygc] = new YYARec(-46,170);yygc++; 
					yyg[yygc] = new YYARec(-44,171);yygc++; 
					yyg[yygc] = new YYARec(-43,172);yygc++; 
					yyg[yygc] = new YYARec(-42,173);yygc++; 
					yyg[yygc] = new YYARec(-41,174);yygc++; 
					yyg[yygc] = new YYARec(-40,175);yygc++; 
					yyg[yygc] = new YYARec(-39,176);yygc++; 
					yyg[yygc] = new YYARec(-38,177);yygc++; 
					yyg[yygc] = new YYARec(-37,246);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-29,75);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-20,185);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-34,134);yygc++; 
					yyg[yygc] = new YYARec(-33,135);yygc++; 
					yyg[yygc] = new YYARec(-32,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-29,124);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-15,249);yygc++; 
					yyg[yygc] = new YYARec(-13,139);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-34,134);yygc++; 
					yyg[yygc] = new YYARec(-33,135);yygc++; 
					yyg[yygc] = new YYARec(-32,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-29,124);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-15,250);yygc++; 
					yyg[yygc] = new YYARec(-13,139);yygc++; 
					yyg[yygc] = new YYARec(-53,187);yygc++; 
					yyg[yygc] = new YYARec(-25,251);yygc++; 
					yyg[yygc] = new YYARec(-53,187);yygc++; 
					yyg[yygc] = new YYARec(-25,252);yygc++; 
					yyg[yygc] = new YYARec(-51,199);yygc++; 
					yyg[yygc] = new YYARec(-49,203);yygc++; 
					yyg[yygc] = new YYARec(-47,206);yygc++; 
					yyg[yygc] = new YYARec(-45,211);yygc++; 
					yyg[yygc] = new YYARec(-57,42);yygc++; 
					yyg[yygc] = new YYARec(-54,43);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-34,134);yygc++; 
					yyg[yygc] = new YYARec(-33,135);yygc++; 
					yyg[yygc] = new YYARec(-32,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-29,124);yygc++; 
					yyg[yygc] = new YYARec(-28,45);yygc++; 
					yyg[yygc] = new YYARec(-26,46);yygc++; 
					yyg[yygc] = new YYARec(-22,47);yygc++; 
					yyg[yygc] = new YYARec(-15,261);yygc++; 
					yyg[yygc] = new YYARec(-13,139);yygc++; 
					yyg[yygc] = new YYARec(-53,187);yygc++; 
					yyg[yygc] = new YYARec(-25,262);yygc++; 
					yyg[yygc] = new YYARec(-53,187);yygc++; 
					yyg[yygc] = new YYARec(-25,263);yygc++; 
					yyg[yygc] = new YYARec(-53,187);yygc++; 
					yyg[yygc] = new YYARec(-25,267);yygc++; 
					yyg[yygc] = new YYARec(-53,187);yygc++; 
					yyg[yygc] = new YYARec(-25,268);yygc++; 
					yyg[yygc] = new YYARec(-53,187);yygc++; 
					yyg[yygc] = new YYARec(-25,271);yygc++; 
					yyg[yygc] = new YYARec(-53,187);yygc++; 
					yyg[yygc] = new YYARec(-25,273);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = 0;  
					yyd[2] = -30;  
					yyd[3] = 0;  
					yyd[4] = 0;  
					yyd[5] = 0;  
					yyd[6] = -9;  
					yyd[7] = -8;  
					yyd[8] = -7;  
					yyd[9] = -6;  
					yyd[10] = -5;  
					yyd[11] = -4;  
					yyd[12] = 0;  
					yyd[13] = -1;  
					yyd[14] = 0;  
					yyd[15] = 0;  
					yyd[16] = 0;  
					yyd[17] = 0;  
					yyd[18] = 0;  
					yyd[19] = 0;  
					yyd[20] = -31;  
					yyd[21] = -32;  
					yyd[22] = -33;  
					yyd[23] = -34;  
					yyd[24] = -35;  
					yyd[25] = -36;  
					yyd[26] = 0;  
					yyd[27] = -48;  
					yyd[28] = -49;  
					yyd[29] = -50;  
					yyd[30] = -51;  
					yyd[31] = -52;  
					yyd[32] = -53;  
					yyd[33] = -54;  
					yyd[34] = -55;  
					yyd[35] = -56;  
					yyd[36] = -57;  
					yyd[37] = -58;  
					yyd[38] = -59;  
					yyd[39] = -60;  
					yyd[40] = -61;  
					yyd[41] = -155;  
					yyd[42] = -159;  
					yyd[43] = -160;  
					yyd[44] = 0;  
					yyd[45] = -161;  
					yyd[46] = -44;  
					yyd[47] = -43;  
					yyd[48] = -40;  
					yyd[49] = -39;  
					yyd[50] = -41;  
					yyd[51] = -42;  
					yyd[52] = -125;  
					yyd[53] = -126;  
					yyd[54] = -127;  
					yyd[55] = -128;  
					yyd[56] = -129;  
					yyd[57] = -130;  
					yyd[58] = -131;  
					yyd[59] = -132;  
					yyd[60] = -133;  
					yyd[61] = -134;  
					yyd[62] = -135;  
					yyd[63] = -136;  
					yyd[64] = -137;  
					yyd[65] = -138;  
					yyd[66] = -146;  
					yyd[67] = -158;  
					yyd[68] = 0;  
					yyd[69] = -157;  
					yyd[70] = -156;  
					yyd[71] = 0;  
					yyd[72] = -147;  
					yyd[73] = 0;  
					yyd[74] = 0;  
					yyd[75] = -144;  
					yyd[76] = -148;  
					yyd[77] = -25;  
					yyd[78] = -24;  
					yyd[79] = -23;  
					yyd[80] = 0;  
					yyd[81] = -26;  
					yyd[82] = -116;  
					yyd[83] = -117;  
					yyd[84] = -115;  
					yyd[85] = -143;  
					yyd[86] = -152;  
					yyd[87] = -154;  
					yyd[88] = -165;  
					yyd[89] = -166;  
					yyd[90] = -2;  
					yyd[91] = 0;  
					yyd[92] = 0;  
					yyd[93] = 0;  
					yyd[94] = 0;  
					yyd[95] = 0;  
					yyd[96] = 0;  
					yyd[97] = 0;  
					yyd[98] = -47;  
					yyd[99] = -77;  
					yyd[100] = 0;  
					yyd[101] = 0;  
					yyd[102] = -151;  
					yyd[103] = -153;  
					yyd[104] = 0;  
					yyd[105] = -22;  
					yyd[106] = 0;  
					yyd[107] = 0;  
					yyd[108] = -19;  
					yyd[109] = 0;  
					yyd[110] = -20;  
					yyd[111] = -21;  
					yyd[112] = 0;  
					yyd[113] = 0;  
					yyd[114] = -75;  
					yyd[115] = -76;  
					yyd[116] = -74;  
					yyd[117] = -73;  
					yyd[118] = 0;  
					yyd[119] = 0;  
					yyd[120] = 0;  
					yyd[121] = 0;  
					yyd[122] = -142;  
					yyd[123] = -163;  
					yyd[124] = -162;  
					yyd[125] = -10;  
					yyd[126] = 0;  
					yyd[127] = -11;  
					yyd[128] = 0;  
					yyd[129] = 0;  
					yyd[130] = -46;  
					yyd[131] = 0;  
					yyd[132] = 0;  
					yyd[133] = -68;  
					yyd[134] = 0;  
					yyd[135] = 0;  
					yyd[136] = 0;  
					yyd[137] = 0;  
					yyd[138] = 0;  
					yyd[139] = 0;  
					yyd[140] = 0;  
					yyd[141] = 0;  
					yyd[142] = 0;  
					yyd[143] = 0;  
					yyd[144] = 0;  
					yyd[145] = 0;  
					yyd[146] = 0;  
					yyd[147] = -29;  
					yyd[148] = 0;  
					yyd[149] = -28;  
					yyd[150] = 0;  
					yyd[151] = -13;  
					yyd[152] = -18;  
					yyd[153] = 0;  
					yyd[154] = -72;  
					yyd[155] = 0;  
					yyd[156] = -66;  
					yyd[157] = 0;  
					yyd[158] = 0;  
					yyd[159] = -45;  
					yyd[160] = -65;  
					yyd[161] = 0;  
					yyd[162] = 0;  
					yyd[163] = 0;  
					yyd[164] = -102;  
					yyd[165] = 0;  
					yyd[166] = 0;  
					yyd[167] = -98;  
					yyd[168] = -96;  
					yyd[169] = 0;  
					yyd[170] = 0;  
					yyd[171] = 0;  
					yyd[172] = 0;  
					yyd[173] = 0;  
					yyd[174] = 0;  
					yyd[175] = 0;  
					yyd[176] = 0;  
					yyd[177] = 0;  
					yyd[178] = -118;  
					yyd[179] = 0;  
					yyd[180] = 0;  
					yyd[181] = -150;  
					yyd[182] = -149;  
					yyd[183] = 0;  
					yyd[184] = 0;  
					yyd[185] = -103;  
					yyd[186] = 0;  
					yyd[187] = 0;  
					yyd[188] = 0;  
					yyd[189] = 0;  
					yyd[190] = 0;  
					yyd[191] = -69;  
					yyd[192] = -64;  
					yyd[193] = -63;  
					yyd[194] = 0;  
					yyd[195] = 0;  
					yyd[196] = 0;  
					yyd[197] = 0;  
					yyd[198] = -99;  
					yyd[199] = 0;  
					yyd[200] = -112;  
					yyd[201] = -113;  
					yyd[202] = -114;  
					yyd[203] = 0;  
					yyd[204] = -110;  
					yyd[205] = -111;  
					yyd[206] = 0;  
					yyd[207] = -106;  
					yyd[208] = -107;  
					yyd[209] = -108;  
					yyd[210] = -109;  
					yyd[211] = 0;  
					yyd[212] = -104;  
					yyd[213] = -105;  
					yyd[214] = 0;  
					yyd[215] = 0;  
					yyd[216] = 0;  
					yyd[217] = 0;  
					yyd[218] = 0;  
					yyd[219] = 0;  
					yyd[220] = -120;  
					yyd[221] = -121;  
					yyd[222] = -122;  
					yyd[223] = -123;  
					yyd[224] = -124;  
					yyd[225] = 0;  
					yyd[226] = 0;  
					yyd[227] = 0;  
					yyd[228] = 0;  
					yyd[229] = 0;  
					yyd[230] = -12;  
					yyd[231] = 0;  
					yyd[232] = 0;  
					yyd[233] = -14;  
					yyd[234] = -15;  
					yyd[235] = -62;  
					yyd[236] = 0;  
					yyd[237] = -97;  
					yyd[238] = 0;  
					yyd[239] = 0;  
					yyd[240] = 0;  
					yyd[241] = 0;  
					yyd[242] = 0;  
					yyd[243] = 0;  
					yyd[244] = 0;  
					yyd[245] = 0;  
					yyd[246] = -119;  
					yyd[247] = -101;  
					yyd[248] = -139;  
					yyd[249] = 0;  
					yyd[250] = 0;  
					yyd[251] = 0;  
					yyd[252] = 0;  
					yyd[253] = 0;  
					yyd[254] = -17;  
					yyd[255] = -100;  
					yyd[256] = -140;  
					yyd[257] = -141;  
					yyd[258] = 0;  
					yyd[259] = -38;  
					yyd[260] = 0;  
					yyd[261] = 0;  
					yyd[262] = 0;  
					yyd[263] = 0;  
					yyd[264] = -16;  
					yyd[265] = 0;  
					yyd[266] = 0;  
					yyd[267] = 0;  
					yyd[268] = 0;  
					yyd[269] = -27;  
					yyd[270] = 0;  
					yyd[271] = 0;  
					yyd[272] = 0;  
					yyd[273] = 0;  
					yyd[274] = -37; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 29;  
					yyal[2] = 55;  
					yyal[3] = 55;  
					yyal[4] = 57;  
					yyal[5] = 59;  
					yyal[6] = 107;  
					yyal[7] = 107;  
					yyal[8] = 107;  
					yyal[9] = 107;  
					yyal[10] = 107;  
					yyal[11] = 107;  
					yyal[12] = 107;  
					yyal[13] = 137;  
					yyal[14] = 137;  
					yyal[15] = 138;  
					yyal[16] = 139;  
					yyal[17] = 140;  
					yyal[18] = 141;  
					yyal[19] = 142;  
					yyal[20] = 143;  
					yyal[21] = 143;  
					yyal[22] = 143;  
					yyal[23] = 143;  
					yyal[24] = 143;  
					yyal[25] = 143;  
					yyal[26] = 143;  
					yyal[27] = 145;  
					yyal[28] = 145;  
					yyal[29] = 145;  
					yyal[30] = 145;  
					yyal[31] = 145;  
					yyal[32] = 145;  
					yyal[33] = 145;  
					yyal[34] = 145;  
					yyal[35] = 145;  
					yyal[36] = 145;  
					yyal[37] = 145;  
					yyal[38] = 145;  
					yyal[39] = 145;  
					yyal[40] = 145;  
					yyal[41] = 145;  
					yyal[42] = 145;  
					yyal[43] = 145;  
					yyal[44] = 145;  
					yyal[45] = 196;  
					yyal[46] = 196;  
					yyal[47] = 196;  
					yyal[48] = 196;  
					yyal[49] = 196;  
					yyal[50] = 196;  
					yyal[51] = 196;  
					yyal[52] = 196;  
					yyal[53] = 196;  
					yyal[54] = 196;  
					yyal[55] = 196;  
					yyal[56] = 196;  
					yyal[57] = 196;  
					yyal[58] = 196;  
					yyal[59] = 196;  
					yyal[60] = 196;  
					yyal[61] = 196;  
					yyal[62] = 196;  
					yyal[63] = 196;  
					yyal[64] = 196;  
					yyal[65] = 196;  
					yyal[66] = 196;  
					yyal[67] = 196;  
					yyal[68] = 196;  
					yyal[69] = 198;  
					yyal[70] = 198;  
					yyal[71] = 198;  
					yyal[72] = 200;  
					yyal[73] = 200;  
					yyal[74] = 202;  
					yyal[75] = 274;  
					yyal[76] = 274;  
					yyal[77] = 274;  
					yyal[78] = 274;  
					yyal[79] = 274;  
					yyal[80] = 274;  
					yyal[81] = 275;  
					yyal[82] = 275;  
					yyal[83] = 275;  
					yyal[84] = 275;  
					yyal[85] = 275;  
					yyal[86] = 275;  
					yyal[87] = 275;  
					yyal[88] = 275;  
					yyal[89] = 275;  
					yyal[90] = 275;  
					yyal[91] = 275;  
					yyal[92] = 276;  
					yyal[93] = 277;  
					yyal[94] = 279;  
					yyal[95] = 280;  
					yyal[96] = 281;  
					yyal[97] = 283;  
					yyal[98] = 332;  
					yyal[99] = 332;  
					yyal[100] = 332;  
					yyal[101] = 333;  
					yyal[102] = 334;  
					yyal[103] = 334;  
					yyal[104] = 334;  
					yyal[105] = 374;  
					yyal[106] = 374;  
					yyal[107] = 403;  
					yyal[108] = 432;  
					yyal[109] = 432;  
					yyal[110] = 480;  
					yyal[111] = 480;  
					yyal[112] = 480;  
					yyal[113] = 481;  
					yyal[114] = 482;  
					yyal[115] = 482;  
					yyal[116] = 482;  
					yyal[117] = 482;  
					yyal[118] = 482;  
					yyal[119] = 532;  
					yyal[120] = 580;  
					yyal[121] = 581;  
					yyal[122] = 587;  
					yyal[123] = 587;  
					yyal[124] = 587;  
					yyal[125] = 587;  
					yyal[126] = 587;  
					yyal[127] = 589;  
					yyal[128] = 589;  
					yyal[129] = 590;  
					yyal[130] = 595;  
					yyal[131] = 595;  
					yyal[132] = 644;  
					yyal[133] = 694;  
					yyal[134] = 694;  
					yyal[135] = 744;  
					yyal[136] = 745;  
					yyal[137] = 746;  
					yyal[138] = 797;  
					yyal[139] = 798;  
					yyal[140] = 848;  
					yyal[141] = 849;  
					yyal[142] = 850;  
					yyal[143] = 898;  
					yyal[144] = 945;  
					yyal[145] = 946;  
					yyal[146] = 993;  
					yyal[147] = 1040;  
					yyal[148] = 1040;  
					yyal[149] = 1044;  
					yyal[150] = 1044;  
					yyal[151] = 1072;  
					yyal[152] = 1072;  
					yyal[153] = 1072;  
					yyal[154] = 1076;  
					yyal[155] = 1076;  
					yyal[156] = 1124;  
					yyal[157] = 1124;  
					yyal[158] = 1174;  
					yyal[159] = 1224;  
					yyal[160] = 1224;  
					yyal[161] = 1224;  
					yyal[162] = 1225;  
					yyal[163] = 1226;  
					yyal[164] = 1227;  
					yyal[165] = 1227;  
					yyal[166] = 1253;  
					yyal[167] = 1300;  
					yyal[168] = 1300;  
					yyal[169] = 1300;  
					yyal[170] = 1319;  
					yyal[171] = 1335;  
					yyal[172] = 1349;  
					yyal[173] = 1359;  
					yyal[174] = 1367;  
					yyal[175] = 1374;  
					yyal[176] = 1380;  
					yyal[177] = 1385;  
					yyal[178] = 1389;  
					yyal[179] = 1389;  
					yyal[180] = 1411;  
					yyal[181] = 1458;  
					yyal[182] = 1458;  
					yyal[183] = 1458;  
					yyal[184] = 1506;  
					yyal[185] = 1507;  
					yyal[186] = 1507;  
					yyal[187] = 1508;  
					yyal[188] = 1509;  
					yyal[189] = 1510;  
					yyal[190] = 1511;  
					yyal[191] = 1512;  
					yyal[192] = 1512;  
					yyal[193] = 1512;  
					yyal[194] = 1512;  
					yyal[195] = 1561;  
					yyal[196] = 1610;  
					yyal[197] = 1660;  
					yyal[198] = 1707;  
					yyal[199] = 1707;  
					yyal[200] = 1754;  
					yyal[201] = 1754;  
					yyal[202] = 1754;  
					yyal[203] = 1754;  
					yyal[204] = 1801;  
					yyal[205] = 1801;  
					yyal[206] = 1801;  
					yyal[207] = 1848;  
					yyal[208] = 1848;  
					yyal[209] = 1848;  
					yyal[210] = 1848;  
					yyal[211] = 1848;  
					yyal[212] = 1895;  
					yyal[213] = 1895;  
					yyal[214] = 1895;  
					yyal[215] = 1942;  
					yyal[216] = 1989;  
					yyal[217] = 2036;  
					yyal[218] = 2083;  
					yyal[219] = 2130;  
					yyal[220] = 2177;  
					yyal[221] = 2177;  
					yyal[222] = 2177;  
					yyal[223] = 2177;  
					yyal[224] = 2177;  
					yyal[225] = 2177;  
					yyal[226] = 2178;  
					yyal[227] = 2179;  
					yyal[228] = 2227;  
					yyal[229] = 2275;  
					yyal[230] = 2279;  
					yyal[231] = 2279;  
					yyal[232] = 2283;  
					yyal[233] = 2285;  
					yyal[234] = 2285;  
					yyal[235] = 2285;  
					yyal[236] = 2285;  
					yyal[237] = 2286;  
					yyal[238] = 2286;  
					yyal[239] = 2305;  
					yyal[240] = 2321;  
					yyal[241] = 2335;  
					yyal[242] = 2345;  
					yyal[243] = 2353;  
					yyal[244] = 2360;  
					yyal[245] = 2366;  
					yyal[246] = 2371;  
					yyal[247] = 2371;  
					yyal[248] = 2371;  
					yyal[249] = 2371;  
					yyal[250] = 2372;  
					yyal[251] = 2373;  
					yyal[252] = 2374;  
					yyal[253] = 2376;  
					yyal[254] = 2424;  
					yyal[255] = 2424;  
					yyal[256] = 2424;  
					yyal[257] = 2424;  
					yyal[258] = 2424;  
					yyal[259] = 2428;  
					yyal[260] = 2428;  
					yyal[261] = 2432;  
					yyal[262] = 2433;  
					yyal[263] = 2434;  
					yyal[264] = 2435;  
					yyal[265] = 2435;  
					yyal[266] = 2439;  
					yyal[267] = 2443;  
					yyal[268] = 2444;  
					yyal[269] = 2445;  
					yyal[270] = 2445;  
					yyal[271] = 2449;  
					yyal[272] = 2450;  
					yyal[273] = 2454;  
					yyal[274] = 2455; 

					yyah = new int[yynstates];
					yyah[0] = 28;  
					yyah[1] = 54;  
					yyah[2] = 54;  
					yyah[3] = 56;  
					yyah[4] = 58;  
					yyah[5] = 106;  
					yyah[6] = 106;  
					yyah[7] = 106;  
					yyah[8] = 106;  
					yyah[9] = 106;  
					yyah[10] = 106;  
					yyah[11] = 106;  
					yyah[12] = 136;  
					yyah[13] = 136;  
					yyah[14] = 137;  
					yyah[15] = 138;  
					yyah[16] = 139;  
					yyah[17] = 140;  
					yyah[18] = 141;  
					yyah[19] = 142;  
					yyah[20] = 142;  
					yyah[21] = 142;  
					yyah[22] = 142;  
					yyah[23] = 142;  
					yyah[24] = 142;  
					yyah[25] = 142;  
					yyah[26] = 144;  
					yyah[27] = 144;  
					yyah[28] = 144;  
					yyah[29] = 144;  
					yyah[30] = 144;  
					yyah[31] = 144;  
					yyah[32] = 144;  
					yyah[33] = 144;  
					yyah[34] = 144;  
					yyah[35] = 144;  
					yyah[36] = 144;  
					yyah[37] = 144;  
					yyah[38] = 144;  
					yyah[39] = 144;  
					yyah[40] = 144;  
					yyah[41] = 144;  
					yyah[42] = 144;  
					yyah[43] = 144;  
					yyah[44] = 195;  
					yyah[45] = 195;  
					yyah[46] = 195;  
					yyah[47] = 195;  
					yyah[48] = 195;  
					yyah[49] = 195;  
					yyah[50] = 195;  
					yyah[51] = 195;  
					yyah[52] = 195;  
					yyah[53] = 195;  
					yyah[54] = 195;  
					yyah[55] = 195;  
					yyah[56] = 195;  
					yyah[57] = 195;  
					yyah[58] = 195;  
					yyah[59] = 195;  
					yyah[60] = 195;  
					yyah[61] = 195;  
					yyah[62] = 195;  
					yyah[63] = 195;  
					yyah[64] = 195;  
					yyah[65] = 195;  
					yyah[66] = 195;  
					yyah[67] = 195;  
					yyah[68] = 197;  
					yyah[69] = 197;  
					yyah[70] = 197;  
					yyah[71] = 199;  
					yyah[72] = 199;  
					yyah[73] = 201;  
					yyah[74] = 273;  
					yyah[75] = 273;  
					yyah[76] = 273;  
					yyah[77] = 273;  
					yyah[78] = 273;  
					yyah[79] = 273;  
					yyah[80] = 274;  
					yyah[81] = 274;  
					yyah[82] = 274;  
					yyah[83] = 274;  
					yyah[84] = 274;  
					yyah[85] = 274;  
					yyah[86] = 274;  
					yyah[87] = 274;  
					yyah[88] = 274;  
					yyah[89] = 274;  
					yyah[90] = 274;  
					yyah[91] = 275;  
					yyah[92] = 276;  
					yyah[93] = 278;  
					yyah[94] = 279;  
					yyah[95] = 280;  
					yyah[96] = 282;  
					yyah[97] = 331;  
					yyah[98] = 331;  
					yyah[99] = 331;  
					yyah[100] = 332;  
					yyah[101] = 333;  
					yyah[102] = 333;  
					yyah[103] = 333;  
					yyah[104] = 373;  
					yyah[105] = 373;  
					yyah[106] = 402;  
					yyah[107] = 431;  
					yyah[108] = 431;  
					yyah[109] = 479;  
					yyah[110] = 479;  
					yyah[111] = 479;  
					yyah[112] = 480;  
					yyah[113] = 481;  
					yyah[114] = 481;  
					yyah[115] = 481;  
					yyah[116] = 481;  
					yyah[117] = 481;  
					yyah[118] = 531;  
					yyah[119] = 579;  
					yyah[120] = 580;  
					yyah[121] = 586;  
					yyah[122] = 586;  
					yyah[123] = 586;  
					yyah[124] = 586;  
					yyah[125] = 586;  
					yyah[126] = 588;  
					yyah[127] = 588;  
					yyah[128] = 589;  
					yyah[129] = 594;  
					yyah[130] = 594;  
					yyah[131] = 643;  
					yyah[132] = 693;  
					yyah[133] = 693;  
					yyah[134] = 743;  
					yyah[135] = 744;  
					yyah[136] = 745;  
					yyah[137] = 796;  
					yyah[138] = 797;  
					yyah[139] = 847;  
					yyah[140] = 848;  
					yyah[141] = 849;  
					yyah[142] = 897;  
					yyah[143] = 944;  
					yyah[144] = 945;  
					yyah[145] = 992;  
					yyah[146] = 1039;  
					yyah[147] = 1039;  
					yyah[148] = 1043;  
					yyah[149] = 1043;  
					yyah[150] = 1071;  
					yyah[151] = 1071;  
					yyah[152] = 1071;  
					yyah[153] = 1075;  
					yyah[154] = 1075;  
					yyah[155] = 1123;  
					yyah[156] = 1123;  
					yyah[157] = 1173;  
					yyah[158] = 1223;  
					yyah[159] = 1223;  
					yyah[160] = 1223;  
					yyah[161] = 1224;  
					yyah[162] = 1225;  
					yyah[163] = 1226;  
					yyah[164] = 1226;  
					yyah[165] = 1252;  
					yyah[166] = 1299;  
					yyah[167] = 1299;  
					yyah[168] = 1299;  
					yyah[169] = 1318;  
					yyah[170] = 1334;  
					yyah[171] = 1348;  
					yyah[172] = 1358;  
					yyah[173] = 1366;  
					yyah[174] = 1373;  
					yyah[175] = 1379;  
					yyah[176] = 1384;  
					yyah[177] = 1388;  
					yyah[178] = 1388;  
					yyah[179] = 1410;  
					yyah[180] = 1457;  
					yyah[181] = 1457;  
					yyah[182] = 1457;  
					yyah[183] = 1505;  
					yyah[184] = 1506;  
					yyah[185] = 1506;  
					yyah[186] = 1507;  
					yyah[187] = 1508;  
					yyah[188] = 1509;  
					yyah[189] = 1510;  
					yyah[190] = 1511;  
					yyah[191] = 1511;  
					yyah[192] = 1511;  
					yyah[193] = 1511;  
					yyah[194] = 1560;  
					yyah[195] = 1609;  
					yyah[196] = 1659;  
					yyah[197] = 1706;  
					yyah[198] = 1706;  
					yyah[199] = 1753;  
					yyah[200] = 1753;  
					yyah[201] = 1753;  
					yyah[202] = 1753;  
					yyah[203] = 1800;  
					yyah[204] = 1800;  
					yyah[205] = 1800;  
					yyah[206] = 1847;  
					yyah[207] = 1847;  
					yyah[208] = 1847;  
					yyah[209] = 1847;  
					yyah[210] = 1847;  
					yyah[211] = 1894;  
					yyah[212] = 1894;  
					yyah[213] = 1894;  
					yyah[214] = 1941;  
					yyah[215] = 1988;  
					yyah[216] = 2035;  
					yyah[217] = 2082;  
					yyah[218] = 2129;  
					yyah[219] = 2176;  
					yyah[220] = 2176;  
					yyah[221] = 2176;  
					yyah[222] = 2176;  
					yyah[223] = 2176;  
					yyah[224] = 2176;  
					yyah[225] = 2177;  
					yyah[226] = 2178;  
					yyah[227] = 2226;  
					yyah[228] = 2274;  
					yyah[229] = 2278;  
					yyah[230] = 2278;  
					yyah[231] = 2282;  
					yyah[232] = 2284;  
					yyah[233] = 2284;  
					yyah[234] = 2284;  
					yyah[235] = 2284;  
					yyah[236] = 2285;  
					yyah[237] = 2285;  
					yyah[238] = 2304;  
					yyah[239] = 2320;  
					yyah[240] = 2334;  
					yyah[241] = 2344;  
					yyah[242] = 2352;  
					yyah[243] = 2359;  
					yyah[244] = 2365;  
					yyah[245] = 2370;  
					yyah[246] = 2370;  
					yyah[247] = 2370;  
					yyah[248] = 2370;  
					yyah[249] = 2371;  
					yyah[250] = 2372;  
					yyah[251] = 2373;  
					yyah[252] = 2375;  
					yyah[253] = 2423;  
					yyah[254] = 2423;  
					yyah[255] = 2423;  
					yyah[256] = 2423;  
					yyah[257] = 2423;  
					yyah[258] = 2427;  
					yyah[259] = 2427;  
					yyah[260] = 2431;  
					yyah[261] = 2432;  
					yyah[262] = 2433;  
					yyah[263] = 2434;  
					yyah[264] = 2434;  
					yyah[265] = 2438;  
					yyah[266] = 2442;  
					yyah[267] = 2443;  
					yyah[268] = 2444;  
					yyah[269] = 2444;  
					yyah[270] = 2448;  
					yyah[271] = 2449;  
					yyah[272] = 2453;  
					yyah[273] = 2454;  
					yyah[274] = 2454; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 15;  
					yygl[2] = 21;  
					yygl[3] = 21;  
					yygl[4] = 22;  
					yygl[5] = 23;  
					yygl[6] = 38;  
					yygl[7] = 38;  
					yygl[8] = 38;  
					yygl[9] = 38;  
					yygl[10] = 38;  
					yygl[11] = 38;  
					yygl[12] = 38;  
					yygl[13] = 51;  
					yygl[14] = 51;  
					yygl[15] = 51;  
					yygl[16] = 52;  
					yygl[17] = 53;  
					yygl[18] = 54;  
					yygl[19] = 55;  
					yygl[20] = 56;  
					yygl[21] = 56;  
					yygl[22] = 56;  
					yygl[23] = 56;  
					yygl[24] = 56;  
					yygl[25] = 56;  
					yygl[26] = 56;  
					yygl[27] = 57;  
					yygl[28] = 57;  
					yygl[29] = 57;  
					yygl[30] = 57;  
					yygl[31] = 57;  
					yygl[32] = 57;  
					yygl[33] = 57;  
					yygl[34] = 57;  
					yygl[35] = 57;  
					yygl[36] = 57;  
					yygl[37] = 57;  
					yygl[38] = 57;  
					yygl[39] = 57;  
					yygl[40] = 57;  
					yygl[41] = 57;  
					yygl[42] = 57;  
					yygl[43] = 57;  
					yygl[44] = 57;  
					yygl[45] = 58;  
					yygl[46] = 58;  
					yygl[47] = 58;  
					yygl[48] = 58;  
					yygl[49] = 58;  
					yygl[50] = 58;  
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
					yygl[69] = 59;  
					yygl[70] = 59;  
					yygl[71] = 59;  
					yygl[72] = 60;  
					yygl[73] = 60;  
					yygl[74] = 60;  
					yygl[75] = 60;  
					yygl[76] = 60;  
					yygl[77] = 60;  
					yygl[78] = 60;  
					yygl[79] = 60;  
					yygl[80] = 60;  
					yygl[81] = 60;  
					yygl[82] = 60;  
					yygl[83] = 60;  
					yygl[84] = 60;  
					yygl[85] = 60;  
					yygl[86] = 60;  
					yygl[87] = 60;  
					yygl[88] = 60;  
					yygl[89] = 60;  
					yygl[90] = 60;  
					yygl[91] = 60;  
					yygl[92] = 60;  
					yygl[93] = 60;  
					yygl[94] = 60;  
					yygl[95] = 60;  
					yygl[96] = 60;  
					yygl[97] = 61;  
					yygl[98] = 77;  
					yygl[99] = 77;  
					yygl[100] = 77;  
					yygl[101] = 78;  
					yygl[102] = 79;  
					yygl[103] = 79;  
					yygl[104] = 79;  
					yygl[105] = 87;  
					yygl[106] = 87;  
					yygl[107] = 101;  
					yygl[108] = 115;  
					yygl[109] = 115;  
					yygl[110] = 130;  
					yygl[111] = 130;  
					yygl[112] = 130;  
					yygl[113] = 131;  
					yygl[114] = 131;  
					yygl[115] = 131;  
					yygl[116] = 131;  
					yygl[117] = 131;  
					yygl[118] = 131;  
					yygl[119] = 132;  
					yygl[120] = 146;  
					yygl[121] = 146;  
					yygl[122] = 147;  
					yygl[123] = 147;  
					yygl[124] = 147;  
					yygl[125] = 147;  
					yygl[126] = 147;  
					yygl[127] = 147;  
					yygl[128] = 147;  
					yygl[129] = 147;  
					yygl[130] = 148;  
					yygl[131] = 148;  
					yygl[132] = 164;  
					yygl[133] = 165;  
					yygl[134] = 165;  
					yygl[135] = 179;  
					yygl[136] = 179;  
					yygl[137] = 179;  
					yygl[138] = 179;  
					yygl[139] = 179;  
					yygl[140] = 193;  
					yygl[141] = 194;  
					yygl[142] = 195;  
					yygl[143] = 209;  
					yygl[144] = 231;  
					yygl[145] = 231;  
					yygl[146] = 253;  
					yygl[147] = 275;  
					yygl[148] = 275;  
					yygl[149] = 277;  
					yygl[150] = 277;  
					yygl[151] = 290;  
					yygl[152] = 290;  
					yygl[153] = 290;  
					yygl[154] = 292;  
					yygl[155] = 292;  
					yygl[156] = 308;  
					yygl[157] = 308;  
					yygl[158] = 322;  
					yygl[159] = 336;  
					yygl[160] = 336;  
					yygl[161] = 336;  
					yygl[162] = 336;  
					yygl[163] = 336;  
					yygl[164] = 336;  
					yygl[165] = 336;  
					yygl[166] = 336;  
					yygl[167] = 348;  
					yygl[168] = 348;  
					yygl[169] = 348;  
					yygl[170] = 349;  
					yygl[171] = 350;  
					yygl[172] = 351;  
					yygl[173] = 352;  
					yygl[174] = 352;  
					yygl[175] = 352;  
					yygl[176] = 352;  
					yygl[177] = 352;  
					yygl[178] = 352;  
					yygl[179] = 352;  
					yygl[180] = 353;  
					yygl[181] = 375;  
					yygl[182] = 375;  
					yygl[183] = 375;  
					yygl[184] = 389;  
					yygl[185] = 389;  
					yygl[186] = 389;  
					yygl[187] = 389;  
					yygl[188] = 389;  
					yygl[189] = 389;  
					yygl[190] = 389;  
					yygl[191] = 389;  
					yygl[192] = 389;  
					yygl[193] = 389;  
					yygl[194] = 389;  
					yygl[195] = 404;  
					yygl[196] = 419;  
					yygl[197] = 433;  
					yygl[198] = 455;  
					yygl[199] = 455;  
					yygl[200] = 467;  
					yygl[201] = 467;  
					yygl[202] = 467;  
					yygl[203] = 467;  
					yygl[204] = 480;  
					yygl[205] = 480;  
					yygl[206] = 480;  
					yygl[207] = 494;  
					yygl[208] = 494;  
					yygl[209] = 494;  
					yygl[210] = 494;  
					yygl[211] = 494;  
					yygl[212] = 509;  
					yygl[213] = 509;  
					yygl[214] = 509;  
					yygl[215] = 525;  
					yygl[216] = 542;  
					yygl[217] = 560;  
					yygl[218] = 579;  
					yygl[219] = 599;  
					yygl[220] = 621;  
					yygl[221] = 621;  
					yygl[222] = 621;  
					yygl[223] = 621;  
					yygl[224] = 621;  
					yygl[225] = 621;  
					yygl[226] = 621;  
					yygl[227] = 621;  
					yygl[228] = 635;  
					yygl[229] = 649;  
					yygl[230] = 651;  
					yygl[231] = 651;  
					yygl[232] = 653;  
					yygl[233] = 653;  
					yygl[234] = 653;  
					yygl[235] = 653;  
					yygl[236] = 653;  
					yygl[237] = 653;  
					yygl[238] = 653;  
					yygl[239] = 654;  
					yygl[240] = 655;  
					yygl[241] = 656;  
					yygl[242] = 657;  
					yygl[243] = 657;  
					yygl[244] = 657;  
					yygl[245] = 657;  
					yygl[246] = 657;  
					yygl[247] = 657;  
					yygl[248] = 657;  
					yygl[249] = 657;  
					yygl[250] = 657;  
					yygl[251] = 657;  
					yygl[252] = 657;  
					yygl[253] = 657;  
					yygl[254] = 671;  
					yygl[255] = 671;  
					yygl[256] = 671;  
					yygl[257] = 671;  
					yygl[258] = 671;  
					yygl[259] = 673;  
					yygl[260] = 673;  
					yygl[261] = 675;  
					yygl[262] = 675;  
					yygl[263] = 675;  
					yygl[264] = 675;  
					yygl[265] = 675;  
					yygl[266] = 677;  
					yygl[267] = 679;  
					yygl[268] = 679;  
					yygl[269] = 679;  
					yygl[270] = 679;  
					yygl[271] = 681;  
					yygl[272] = 681;  
					yygl[273] = 683;  
					yygl[274] = 683; 

					yygh = new int[yynstates];
					yygh[0] = 14;  
					yygh[1] = 20;  
					yygh[2] = 20;  
					yygh[3] = 21;  
					yygh[4] = 22;  
					yygh[5] = 37;  
					yygh[6] = 37;  
					yygh[7] = 37;  
					yygh[8] = 37;  
					yygh[9] = 37;  
					yygh[10] = 37;  
					yygh[11] = 37;  
					yygh[12] = 50;  
					yygh[13] = 50;  
					yygh[14] = 50;  
					yygh[15] = 51;  
					yygh[16] = 52;  
					yygh[17] = 53;  
					yygh[18] = 54;  
					yygh[19] = 55;  
					yygh[20] = 55;  
					yygh[21] = 55;  
					yygh[22] = 55;  
					yygh[23] = 55;  
					yygh[24] = 55;  
					yygh[25] = 55;  
					yygh[26] = 56;  
					yygh[27] = 56;  
					yygh[28] = 56;  
					yygh[29] = 56;  
					yygh[30] = 56;  
					yygh[31] = 56;  
					yygh[32] = 56;  
					yygh[33] = 56;  
					yygh[34] = 56;  
					yygh[35] = 56;  
					yygh[36] = 56;  
					yygh[37] = 56;  
					yygh[38] = 56;  
					yygh[39] = 56;  
					yygh[40] = 56;  
					yygh[41] = 56;  
					yygh[42] = 56;  
					yygh[43] = 56;  
					yygh[44] = 57;  
					yygh[45] = 57;  
					yygh[46] = 57;  
					yygh[47] = 57;  
					yygh[48] = 57;  
					yygh[49] = 57;  
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
					yygh[68] = 58;  
					yygh[69] = 58;  
					yygh[70] = 58;  
					yygh[71] = 59;  
					yygh[72] = 59;  
					yygh[73] = 59;  
					yygh[74] = 59;  
					yygh[75] = 59;  
					yygh[76] = 59;  
					yygh[77] = 59;  
					yygh[78] = 59;  
					yygh[79] = 59;  
					yygh[80] = 59;  
					yygh[81] = 59;  
					yygh[82] = 59;  
					yygh[83] = 59;  
					yygh[84] = 59;  
					yygh[85] = 59;  
					yygh[86] = 59;  
					yygh[87] = 59;  
					yygh[88] = 59;  
					yygh[89] = 59;  
					yygh[90] = 59;  
					yygh[91] = 59;  
					yygh[92] = 59;  
					yygh[93] = 59;  
					yygh[94] = 59;  
					yygh[95] = 59;  
					yygh[96] = 60;  
					yygh[97] = 76;  
					yygh[98] = 76;  
					yygh[99] = 76;  
					yygh[100] = 77;  
					yygh[101] = 78;  
					yygh[102] = 78;  
					yygh[103] = 78;  
					yygh[104] = 86;  
					yygh[105] = 86;  
					yygh[106] = 100;  
					yygh[107] = 114;  
					yygh[108] = 114;  
					yygh[109] = 129;  
					yygh[110] = 129;  
					yygh[111] = 129;  
					yygh[112] = 130;  
					yygh[113] = 130;  
					yygh[114] = 130;  
					yygh[115] = 130;  
					yygh[116] = 130;  
					yygh[117] = 130;  
					yygh[118] = 131;  
					yygh[119] = 145;  
					yygh[120] = 145;  
					yygh[121] = 146;  
					yygh[122] = 146;  
					yygh[123] = 146;  
					yygh[124] = 146;  
					yygh[125] = 146;  
					yygh[126] = 146;  
					yygh[127] = 146;  
					yygh[128] = 146;  
					yygh[129] = 147;  
					yygh[130] = 147;  
					yygh[131] = 163;  
					yygh[132] = 164;  
					yygh[133] = 164;  
					yygh[134] = 178;  
					yygh[135] = 178;  
					yygh[136] = 178;  
					yygh[137] = 178;  
					yygh[138] = 178;  
					yygh[139] = 192;  
					yygh[140] = 193;  
					yygh[141] = 194;  
					yygh[142] = 208;  
					yygh[143] = 230;  
					yygh[144] = 230;  
					yygh[145] = 252;  
					yygh[146] = 274;  
					yygh[147] = 274;  
					yygh[148] = 276;  
					yygh[149] = 276;  
					yygh[150] = 289;  
					yygh[151] = 289;  
					yygh[152] = 289;  
					yygh[153] = 291;  
					yygh[154] = 291;  
					yygh[155] = 307;  
					yygh[156] = 307;  
					yygh[157] = 321;  
					yygh[158] = 335;  
					yygh[159] = 335;  
					yygh[160] = 335;  
					yygh[161] = 335;  
					yygh[162] = 335;  
					yygh[163] = 335;  
					yygh[164] = 335;  
					yygh[165] = 335;  
					yygh[166] = 347;  
					yygh[167] = 347;  
					yygh[168] = 347;  
					yygh[169] = 348;  
					yygh[170] = 349;  
					yygh[171] = 350;  
					yygh[172] = 351;  
					yygh[173] = 351;  
					yygh[174] = 351;  
					yygh[175] = 351;  
					yygh[176] = 351;  
					yygh[177] = 351;  
					yygh[178] = 351;  
					yygh[179] = 352;  
					yygh[180] = 374;  
					yygh[181] = 374;  
					yygh[182] = 374;  
					yygh[183] = 388;  
					yygh[184] = 388;  
					yygh[185] = 388;  
					yygh[186] = 388;  
					yygh[187] = 388;  
					yygh[188] = 388;  
					yygh[189] = 388;  
					yygh[190] = 388;  
					yygh[191] = 388;  
					yygh[192] = 388;  
					yygh[193] = 388;  
					yygh[194] = 403;  
					yygh[195] = 418;  
					yygh[196] = 432;  
					yygh[197] = 454;  
					yygh[198] = 454;  
					yygh[199] = 466;  
					yygh[200] = 466;  
					yygh[201] = 466;  
					yygh[202] = 466;  
					yygh[203] = 479;  
					yygh[204] = 479;  
					yygh[205] = 479;  
					yygh[206] = 493;  
					yygh[207] = 493;  
					yygh[208] = 493;  
					yygh[209] = 493;  
					yygh[210] = 493;  
					yygh[211] = 508;  
					yygh[212] = 508;  
					yygh[213] = 508;  
					yygh[214] = 524;  
					yygh[215] = 541;  
					yygh[216] = 559;  
					yygh[217] = 578;  
					yygh[218] = 598;  
					yygh[219] = 620;  
					yygh[220] = 620;  
					yygh[221] = 620;  
					yygh[222] = 620;  
					yygh[223] = 620;  
					yygh[224] = 620;  
					yygh[225] = 620;  
					yygh[226] = 620;  
					yygh[227] = 634;  
					yygh[228] = 648;  
					yygh[229] = 650;  
					yygh[230] = 650;  
					yygh[231] = 652;  
					yygh[232] = 652;  
					yygh[233] = 652;  
					yygh[234] = 652;  
					yygh[235] = 652;  
					yygh[236] = 652;  
					yygh[237] = 652;  
					yygh[238] = 653;  
					yygh[239] = 654;  
					yygh[240] = 655;  
					yygh[241] = 656;  
					yygh[242] = 656;  
					yygh[243] = 656;  
					yygh[244] = 656;  
					yygh[245] = 656;  
					yygh[246] = 656;  
					yygh[247] = 656;  
					yygh[248] = 656;  
					yygh[249] = 656;  
					yygh[250] = 656;  
					yygh[251] = 656;  
					yygh[252] = 656;  
					yygh[253] = 670;  
					yygh[254] = 670;  
					yygh[255] = 670;  
					yygh[256] = 670;  
					yygh[257] = 670;  
					yygh[258] = 672;  
					yygh[259] = 672;  
					yygh[260] = 674;  
					yygh[261] = 674;  
					yygh[262] = 674;  
					yygh[263] = 674;  
					yygh[264] = 674;  
					yygh[265] = 676;  
					yygh[266] = 678;  
					yygh[267] = 678;  
					yygh[268] = 678;  
					yygh[269] = 678;  
					yygh[270] = 680;  
					yygh[271] = 680;  
					yygh[272] = 682;  
					yygh[273] = 682;  
					yygh[274] = 682; 

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
					yyr[yyrc] = new YYRRec(13,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(17,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(9,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-24);yyrc++; 
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
					yyr[yyrc] = new YYRRec(2,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-35);yyrc++; 
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
					yyr[yyrc] = new YYRRec(4,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
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

			if (Regex.IsMatch(Rest,"^((?i)PAN_MAP)")){
				Results.Add (t_PANChar95MAP);
				ResultsV.Add(Regex.Match(Rest,"^((?i)PAN_MAP)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)ACTION)")){
				Results.Add (t_ACTION);
				ResultsV.Add(Regex.Match(Rest,"^((?i)ACTION)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)ACTOR)")){
				Results.Add (t_ACTOR);
				ResultsV.Add(Regex.Match(Rest,"^((?i)ACTOR)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)OVERLAY)")){
				Results.Add (t_OVERLAY);
				ResultsV.Add(Regex.Match(Rest,"^((?i)OVERLAY)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)PALETTE)")){
				Results.Add (t_PALETTE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)PALETTE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)PANEL)")){
				Results.Add (t_PANEL);
				ResultsV.Add(Regex.Match(Rest,"^((?i)PANEL)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)REGION)")){
				Results.Add (t_REGION);
				ResultsV.Add(Regex.Match(Rest,"^((?i)REGION)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)SKILL)")){
				Results.Add (t_SKILL);
				ResultsV.Add(Regex.Match(Rest,"^((?i)SKILL)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)STRING)")){
				Results.Add (t_STRING);
				ResultsV.Add(Regex.Match(Rest,"^((?i)STRING)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)SYNONYM)")){
				Results.Add (t_SYNONYM);
				ResultsV.Add(Regex.Match(Rest,"^((?i)SYNONYM)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)TEXT)")){
				Results.Add (t_TEXT);
				ResultsV.Add(Regex.Match(Rest,"^((?i)TEXT)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)TEXTURE)")){
				Results.Add (t_TEXTURE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)TEXTURE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)THING)")){
				Results.Add (t_THING);
				ResultsV.Add(Regex.Match(Rest,"^((?i)THING)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)WALL)")){
				Results.Add (t_WALL);
				ResultsV.Add(Regex.Match(Rest,"^((?i)WALL)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)WAY)")){
				Results.Add (t_WAY);
				ResultsV.Add(Regex.Match(Rest,"^((?i)WAY)").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)NULL)")){
				Results.Add (t_NULL);
				ResultsV.Add(Regex.Match(Rest,"^((?i)NULL)").Value);}

			if (Regex.IsMatch(Rest,"^([0-9]+)")){
				Results.Add (t_integer);
				ResultsV.Add(Regex.Match(Rest,"^([0-9]+)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ABSPOS|AUTORANGE|BASE|BEHIND|BERKELEY|BLUR|CANDELABER|CAREFULLY|CEIL_ASCEND|CEIL_DESCEND|CEIL_LIFTED|CENTER_X|CENTER_Y|CLIP|CLUSTER|CONDENSED|CURTAIN|DIAPHANOUS|FAR|FENCE|FLAG1|FLAG2|FLAG3|FLAG4|FLAG5|FLAG6|FLAG7|FLAG8|FLAT|FLOOR_ASCEND|FLOOR_DESCEND|FLOOR_LIFTED|FRAGILE|GHOST|GROUND|HARD|HERE|IMMATERIAL|IMPASSABLE|INVISIBLE|LIBER|LIFTED|LIGHTMAP|MOVED|NARROW|NO_CLIP|ONESHOT|PASSABLE|PLAY|PORTCULLIS|REFRESH|RELPOS|SAVE|SAVE_ALL|SEEN|SENSITIVE|SHADOW|SKY|SLOOP|STICKY|TRANSPARENT|VISIBLE|WIRE))")){
				Results.Add (t_flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ABSPOS|AUTORANGE|BASE|BEHIND|BERKELEY|BLUR|CANDELABER|CAREFULLY|CEIL_ASCEND|CEIL_DESCEND|CEIL_LIFTED|CENTER_X|CENTER_Y|CLIP|CLUSTER|CONDENSED|CURTAIN|DIAPHANOUS|FAR|FENCE|FLAG1|FLAG2|FLAG3|FLAG4|FLAG5|FLAG6|FLAG7|FLAG8|FLAT|FLOOR_ASCEND|FLOOR_DESCEND|FLOOR_LIFTED|FRAGILE|GHOST|GROUND|HARD|HERE|IMMATERIAL|IMPASSABLE|INVISIBLE|LIBER|LIFTED|LIGHTMAP|MOVED|NARROW|NO_CLIP|ONESHOT|PASSABLE|PLAY|PORTCULLIS|REFRESH|RELPOS|SAVE|SAVE_ALL|SEEN|SENSITIVE|SHADOW|SKY|SLOOP|STICKY|TRANSPARENT|VISIBLE|WIRE))").Value);}

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
