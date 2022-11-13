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
                int yymaxdepth = 4096;
                int yyflag = 0;
                int yyfnone   = 0;
                int[] yys = new int[4096];
                string[] yyv = new string[4096];

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
                int t_IFELSE = 260;
                int t_ENDIF = 261;
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
                int t_RULE = 288;
                int t_Char42Char61 = 289;
                int t_Char43Char61 = 290;
                int t_Char45Char61 = 291;
                int t_Char47Char61 = 292;
                int t_Char61 = 293;
                int t_ELSE = 294;
                int t_IF = 295;
                int t_WHILE = 296;
                int t_Char46 = 297;
                int t_NULL = 298;
                int t_event = 299;
                int t_global = 300;
                int t_asset = 301;
                int t_object = 302;
                int t_function = 303;
                int t_math = 304;
                int t_flag = 305;
                int t_property = 306;
                int t_command = 307;
                int t_list = 308;
                int t_skill = 309;
                int t_synonym = 310;
                int t_ambigChar95globalChar95property = 311;
                int t_ambigChar95eventChar95property = 312;
                int t_ambigChar95objectChar95flag = 313;
                int t_ambigChar95mathChar95command = 314;
                int t_ambigChar95mathChar95skillChar95property = 315;
                int t_ambigChar95synonymChar95flag = 316;
                int t_ambigChar95skillChar95property = 317;
                int t_ambigChar95commandChar95flag = 318;
                int t_ambigChar95synonymChar95property = 319;
                int t_integer = 320;
                int t_fixed = 321;
                int t_identifier = 322;
                int t_file = 323;
                int t_string = 324;
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
         yyval = yyv[yysp-0];
         
       break;
							case   11 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   12 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   13 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   14 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   15 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   16 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   17 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   18 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   19 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   20 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   21 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   22 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   23 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   24 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   25 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   26 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   27 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   28 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   29 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   40 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   41 : 
         yyval = yyv[yysp-0];
         
       break;
							case   42 : 
         yyval = "";
         
       break;
							case   43 : 
         yyval = yyv[yysp-0];
         
       break;
							case   44 : 
         yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   45 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   46 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   47 : 
         yyval = yyv[yysp-0];
         
       break;
							case   48 : 
         yyval = yyv[yysp-0];
         
       break;
							case   49 : 
         yyval = yyv[yysp-0];
         
       break;
							case   50 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   51 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   52 : 
         yyval = "";
         
       break;
							case   53 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   54 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   55 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   65 : 
         yyval = yyv[yysp-0];
         
       break;
							case   66 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   67 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   68 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   69 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   70 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   71 : 
         yyval = "";
         
       break;
							case   72 : 
         yyval = yyv[yysp-0];
         
       break;
							case   73 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   74 : 
         yyval = yyv[yysp-0];
         
       break;
							case   75 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   76 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = "";
         
       break;
							case   84 : 
         yyval = yyv[yysp-0];
         
       break;
							case   85 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   86 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   96 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   97 : 
         yyval = yyv[yysp-0];
         
       break;
							case   98 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   99 : 
         yyval = yyv[yysp-0];
         
       break;
							case  100 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  101 : 
         yyval = yyv[yysp-0];
         
       break;
							case  102 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  103 : 
         yyval = yyv[yysp-0];
         
       break;
							case  104 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
							case  122 : 
         yyval = yyv[yysp-0];
         
       break;
							case  123 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  124 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  125 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  135 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  136 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  137 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  138 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  139 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  140 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  141 : 
         yyval = yyv[yysp-0];
         
       break;
							case  142 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case  152 : 
         yyval = yyv[yysp-0];
         
       break;
							case  153 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  167 : 
         yyval = yyv[yysp-0];
         
       break;
							case  168 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  169 : 
         yyval = yyv[yysp-0];
         
       break;
							case  170 : 
         yyval = yyv[yysp-0];
         
       break;
							case  171 : 
         yyval = yyv[yysp-0];
         
       break;
							case  172 : 
         yyval = yyv[yysp-0];
         
       break;
							case  173 : 
         yyval = yyv[yysp-0];
         
       break;
							case  174 : 
         yyval = yyv[yysp-0];
         
       break;
							case  175 : 
         yyval = yyv[yysp-0];
         
       break;
							case  176 : 
         yyval = yyv[yysp-0];
         
       break;
							case  177 : 
         yyval = yyv[yysp-0];
         
       break;
							case  178 : 
         yyval = yyv[yysp-0];
         
       break;
							case  179 : 
         yyval = yyv[yysp-0];
         
       break;
							case  180 : 
         yyval = yyv[yysp-0];
         
       break;
							case  181 : 
         yyval = yyv[yysp-0];
         
       break;
							case  182 : 
         yyval = yyv[yysp-0];
         
       break;
							case  183 : 
         yyval = yyv[yysp-0];
         
       break;
							case  184 : 
         yyval = yyv[yysp-0];
         
       break;
							case  185 : 
         yyval = yyv[yysp-0];
         
       break;
							case  186 : 
         yyval = yyv[yysp-0];
         
       break;
							case  187 : 
         yyval = yyv[yysp-0];
         
       break;
							case  188 : 
         yyval = yyv[yysp-0];
         
       break;
							case  189 : 
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

					int yynacts   = 1854;
					int yyngotos  = 810;
					int yynstates = 306;
					int yynrules  = 189;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,17);yyac++; 
					yya[yyac] = new YYARec(259,18);yyac++; 
					yya[yyac] = new YYARec(262,19);yyac++; 
					yya[yyac] = new YYARec(264,20);yyac++; 
					yya[yyac] = new YYARec(265,21);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,23);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(311,27);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(322,47);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(320,59);yyac++; 
					yya[yyac] = new YYARec(321,60);yyac++; 
					yya[yyac] = new YYARec(322,61);yyac++; 
					yya[yyac] = new YYARec(323,62);yyac++; 
					yya[yyac] = new YYARec(324,63);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(257,17);yyac++; 
					yya[yyac] = new YYARec(259,18);yyac++; 
					yya[yyac] = new YYARec(262,19);yyac++; 
					yya[yyac] = new YYARec(264,20);yyac++; 
					yya[yyac] = new YYARec(265,21);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,23);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(311,27);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(322,61);yyac++; 
					yya[yyac] = new YYARec(322,61);yyac++; 
					yya[yyac] = new YYARec(322,61);yyac++; 
					yya[yyac] = new YYARec(322,61);yyac++; 
					yya[yyac] = new YYARec(323,62);yyac++; 
					yya[yyac] = new YYARec(263,73);yyac++; 
					yya[yyac] = new YYARec(266,-83 );yyac++; 
					yya[yyac] = new YYARec(258,75);yyac++; 
					yya[yyac] = new YYARec(263,73);yyac++; 
					yya[yyac] = new YYARec(266,-83 );yyac++; 
					yya[yyac] = new YYARec(324,-83 );yyac++; 
					yya[yyac] = new YYARec(263,73);yyac++; 
					yya[yyac] = new YYARec(323,-83 );yyac++; 
					yya[yyac] = new YYARec(320,77);yyac++; 
					yya[yyac] = new YYARec(321,78);yyac++; 
					yya[yyac] = new YYARec(258,79);yyac++; 
					yya[yyac] = new YYARec(263,73);yyac++; 
					yya[yyac] = new YYARec(258,-83 );yyac++; 
					yya[yyac] = new YYARec(301,-83 );yyac++; 
					yya[yyac] = new YYARec(304,-83 );yyac++; 
					yya[yyac] = new YYARec(305,-83 );yyac++; 
					yya[yyac] = new YYARec(306,-83 );yyac++; 
					yya[yyac] = new YYARec(307,-83 );yyac++; 
					yya[yyac] = new YYARec(309,-83 );yyac++; 
					yya[yyac] = new YYARec(314,-83 );yyac++; 
					yya[yyac] = new YYARec(315,-83 );yyac++; 
					yya[yyac] = new YYARec(317,-83 );yyac++; 
					yya[yyac] = new YYARec(318,-83 );yyac++; 
					yya[yyac] = new YYARec(322,-83 );yyac++; 
					yya[yyac] = new YYARec(258,81);yyac++; 
					yya[yyac] = new YYARec(258,82);yyac++; 
					yya[yyac] = new YYARec(258,83);yyac++; 
					yya[yyac] = new YYARec(258,84);yyac++; 
					yya[yyac] = new YYARec(263,85);yyac++; 
					yya[yyac] = new YYARec(258,86);yyac++; 
					yya[yyac] = new YYARec(258,87);yyac++; 
					yya[yyac] = new YYARec(266,88);yyac++; 
					yya[yyac] = new YYARec(266,90);yyac++; 
					yya[yyac] = new YYARec(324,63);yyac++; 
					yya[yyac] = new YYARec(323,62);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(258,-33 );yyac++; 
					yya[yyac] = new YYARec(257,17);yyac++; 
					yya[yyac] = new YYARec(259,18);yyac++; 
					yya[yyac] = new YYARec(262,19);yyac++; 
					yya[yyac] = new YYARec(264,20);yyac++; 
					yya[yyac] = new YYARec(265,21);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,23);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(311,27);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(257,17);yyac++; 
					yya[yyac] = new YYARec(259,18);yyac++; 
					yya[yyac] = new YYARec(262,19);yyac++; 
					yya[yyac] = new YYARec(264,20);yyac++; 
					yya[yyac] = new YYARec(265,21);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,23);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(311,27);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(308,107);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,59);yyac++; 
					yya[yyac] = new YYARec(321,60);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(323,62);yyac++; 
					yya[yyac] = new YYARec(324,63);yyac++; 
					yya[yyac] = new YYARec(257,120);yyac++; 
					yya[yyac] = new YYARec(259,121);yyac++; 
					yya[yyac] = new YYARec(266,122);yyac++; 
					yya[yyac] = new YYARec(288,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(296,126);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,127);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,128);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,129);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(267,-71 );yyac++; 
					yya[yyac] = new YYARec(258,130);yyac++; 
					yya[yyac] = new YYARec(257,137);yyac++; 
					yya[yyac] = new YYARec(259,138);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,139);yyac++; 
					yya[yyac] = new YYARec(311,140);yyac++; 
					yya[yyac] = new YYARec(312,141);yyac++; 
					yya[yyac] = new YYARec(315,142);yyac++; 
					yya[yyac] = new YYARec(317,143);yyac++; 
					yya[yyac] = new YYARec(319,144);yyac++; 
					yya[yyac] = new YYARec(267,-52 );yyac++; 
					yya[yyac] = new YYARec(263,73);yyac++; 
					yya[yyac] = new YYARec(258,-83 );yyac++; 
					yya[yyac] = new YYARec(282,-83 );yyac++; 
					yya[yyac] = new YYARec(283,-83 );yyac++; 
					yya[yyac] = new YYARec(287,-83 );yyac++; 
					yya[yyac] = new YYARec(320,-83 );yyac++; 
					yya[yyac] = new YYARec(258,146);yyac++; 
					yya[yyac] = new YYARec(260,147);yyac++; 
					yya[yyac] = new YYARec(261,148);yyac++; 
					yya[yyac] = new YYARec(258,149);yyac++; 
					yya[yyac] = new YYARec(297,150);yyac++; 
					yya[yyac] = new YYARec(258,-145 );yyac++; 
					yya[yyac] = new YYARec(263,-145 );yyac++; 
					yya[yyac] = new YYARec(266,-145 );yyac++; 
					yya[yyac] = new YYARec(269,-145 );yyac++; 
					yya[yyac] = new YYARec(270,-145 );yyac++; 
					yya[yyac] = new YYARec(271,-145 );yyac++; 
					yya[yyac] = new YYARec(272,-145 );yyac++; 
					yya[yyac] = new YYARec(273,-145 );yyac++; 
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
					yya[yyac] = new YYARec(289,-145 );yyac++; 
					yya[yyac] = new YYARec(290,-145 );yyac++; 
					yya[yyac] = new YYARec(291,-145 );yyac++; 
					yya[yyac] = new YYARec(292,-145 );yyac++; 
					yya[yyac] = new YYARec(293,-145 );yyac++; 
					yya[yyac] = new YYARec(298,-145 );yyac++; 
					yya[yyac] = new YYARec(299,-145 );yyac++; 
					yya[yyac] = new YYARec(301,-145 );yyac++; 
					yya[yyac] = new YYARec(302,-145 );yyac++; 
					yya[yyac] = new YYARec(304,-145 );yyac++; 
					yya[yyac] = new YYARec(305,-145 );yyac++; 
					yya[yyac] = new YYARec(306,-145 );yyac++; 
					yya[yyac] = new YYARec(307,-145 );yyac++; 
					yya[yyac] = new YYARec(308,-145 );yyac++; 
					yya[yyac] = new YYARec(309,-145 );yyac++; 
					yya[yyac] = new YYARec(310,-145 );yyac++; 
					yya[yyac] = new YYARec(312,-145 );yyac++; 
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
					yya[yyac] = new YYARec(297,151);yyac++; 
					yya[yyac] = new YYARec(258,-144 );yyac++; 
					yya[yyac] = new YYARec(263,-144 );yyac++; 
					yya[yyac] = new YYARec(266,-144 );yyac++; 
					yya[yyac] = new YYARec(269,-144 );yyac++; 
					yya[yyac] = new YYARec(270,-144 );yyac++; 
					yya[yyac] = new YYARec(271,-144 );yyac++; 
					yya[yyac] = new YYARec(272,-144 );yyac++; 
					yya[yyac] = new YYARec(273,-144 );yyac++; 
					yya[yyac] = new YYARec(275,-144 );yyac++; 
					yya[yyac] = new YYARec(276,-144 );yyac++; 
					yya[yyac] = new YYARec(277,-144 );yyac++; 
					yya[yyac] = new YYARec(278,-144 );yyac++; 
					yya[yyac] = new YYARec(279,-144 );yyac++; 
					yya[yyac] = new YYARec(280,-144 );yyac++; 
					yya[yyac] = new YYARec(281,-144 );yyac++; 
					yya[yyac] = new YYARec(282,-144 );yyac++; 
					yya[yyac] = new YYARec(283,-144 );yyac++; 
					yya[yyac] = new YYARec(284,-144 );yyac++; 
					yya[yyac] = new YYARec(285,-144 );yyac++; 
					yya[yyac] = new YYARec(286,-144 );yyac++; 
					yya[yyac] = new YYARec(287,-144 );yyac++; 
					yya[yyac] = new YYARec(289,-144 );yyac++; 
					yya[yyac] = new YYARec(290,-144 );yyac++; 
					yya[yyac] = new YYARec(291,-144 );yyac++; 
					yya[yyac] = new YYARec(292,-144 );yyac++; 
					yya[yyac] = new YYARec(293,-144 );yyac++; 
					yya[yyac] = new YYARec(298,-144 );yyac++; 
					yya[yyac] = new YYARec(299,-144 );yyac++; 
					yya[yyac] = new YYARec(301,-144 );yyac++; 
					yya[yyac] = new YYARec(302,-144 );yyac++; 
					yya[yyac] = new YYARec(304,-144 );yyac++; 
					yya[yyac] = new YYARec(305,-144 );yyac++; 
					yya[yyac] = new YYARec(306,-144 );yyac++; 
					yya[yyac] = new YYARec(307,-144 );yyac++; 
					yya[yyac] = new YYARec(308,-144 );yyac++; 
					yya[yyac] = new YYARec(309,-144 );yyac++; 
					yya[yyac] = new YYARec(310,-144 );yyac++; 
					yya[yyac] = new YYARec(312,-144 );yyac++; 
					yya[yyac] = new YYARec(313,-144 );yyac++; 
					yya[yyac] = new YYARec(314,-144 );yyac++; 
					yya[yyac] = new YYARec(315,-144 );yyac++; 
					yya[yyac] = new YYARec(316,-144 );yyac++; 
					yya[yyac] = new YYARec(317,-144 );yyac++; 
					yya[yyac] = new YYARec(318,-144 );yyac++; 
					yya[yyac] = new YYARec(319,-144 );yyac++; 
					yya[yyac] = new YYARec(320,-144 );yyac++; 
					yya[yyac] = new YYARec(321,-144 );yyac++; 
					yya[yyac] = new YYARec(322,-144 );yyac++; 
					yya[yyac] = new YYARec(323,-144 );yyac++; 
					yya[yyac] = new YYARec(324,-144 );yyac++; 
					yya[yyac] = new YYARec(258,152);yyac++; 
					yya[yyac] = new YYARec(289,154);yyac++; 
					yya[yyac] = new YYARec(290,155);yyac++; 
					yya[yyac] = new YYARec(291,156);yyac++; 
					yya[yyac] = new YYARec(292,157);yyac++; 
					yya[yyac] = new YYARec(293,158);yyac++; 
					yya[yyac] = new YYARec(263,73);yyac++; 
					yya[yyac] = new YYARec(258,-74 );yyac++; 
					yya[yyac] = new YYARec(282,-83 );yyac++; 
					yya[yyac] = new YYARec(283,-83 );yyac++; 
					yya[yyac] = new YYARec(287,-83 );yyac++; 
					yya[yyac] = new YYARec(298,-83 );yyac++; 
					yya[yyac] = new YYARec(299,-83 );yyac++; 
					yya[yyac] = new YYARec(301,-83 );yyac++; 
					yya[yyac] = new YYARec(302,-83 );yyac++; 
					yya[yyac] = new YYARec(304,-83 );yyac++; 
					yya[yyac] = new YYARec(305,-83 );yyac++; 
					yya[yyac] = new YYARec(306,-83 );yyac++; 
					yya[yyac] = new YYARec(307,-83 );yyac++; 
					yya[yyac] = new YYARec(308,-83 );yyac++; 
					yya[yyac] = new YYARec(309,-83 );yyac++; 
					yya[yyac] = new YYARec(310,-83 );yyac++; 
					yya[yyac] = new YYARec(312,-83 );yyac++; 
					yya[yyac] = new YYARec(313,-83 );yyac++; 
					yya[yyac] = new YYARec(314,-83 );yyac++; 
					yya[yyac] = new YYARec(315,-83 );yyac++; 
					yya[yyac] = new YYARec(316,-83 );yyac++; 
					yya[yyac] = new YYARec(317,-83 );yyac++; 
					yya[yyac] = new YYARec(318,-83 );yyac++; 
					yya[yyac] = new YYARec(319,-83 );yyac++; 
					yya[yyac] = new YYARec(320,-83 );yyac++; 
					yya[yyac] = new YYARec(321,-83 );yyac++; 
					yya[yyac] = new YYARec(322,-83 );yyac++; 
					yya[yyac] = new YYARec(323,-83 );yyac++; 
					yya[yyac] = new YYARec(324,-83 );yyac++; 
					yya[yyac] = new YYARec(257,120);yyac++; 
					yya[yyac] = new YYARec(259,121);yyac++; 
					yya[yyac] = new YYARec(266,122);yyac++; 
					yya[yyac] = new YYARec(288,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(296,126);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,127);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,128);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,129);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(260,-71 );yyac++; 
					yya[yyac] = new YYARec(261,-71 );yyac++; 
					yya[yyac] = new YYARec(267,-71 );yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(268,162);yyac++; 
					yya[yyac] = new YYARec(297,151);yyac++; 
					yya[yyac] = new YYARec(289,-144 );yyac++; 
					yya[yyac] = new YYARec(290,-144 );yyac++; 
					yya[yyac] = new YYARec(291,-144 );yyac++; 
					yya[yyac] = new YYARec(292,-144 );yyac++; 
					yya[yyac] = new YYARec(293,-144 );yyac++; 
					yya[yyac] = new YYARec(268,-187 );yyac++; 
					yya[yyac] = new YYARec(267,163);yyac++; 
					yya[yyac] = new YYARec(257,120);yyac++; 
					yya[yyac] = new YYARec(259,121);yyac++; 
					yya[yyac] = new YYARec(266,122);yyac++; 
					yya[yyac] = new YYARec(288,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(296,126);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,127);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,128);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,129);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(260,-71 );yyac++; 
					yya[yyac] = new YYARec(261,-71 );yyac++; 
					yya[yyac] = new YYARec(267,-71 );yyac++; 
					yya[yyac] = new YYARec(322,61);yyac++; 
					yya[yyac] = new YYARec(322,61);yyac++; 
					yya[yyac] = new YYARec(257,120);yyac++; 
					yya[yyac] = new YYARec(259,121);yyac++; 
					yya[yyac] = new YYARec(266,122);yyac++; 
					yya[yyac] = new YYARec(288,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(296,126);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,127);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,128);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,129);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(267,-71 );yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(266,187);yyac++; 
					yya[yyac] = new YYARec(258,-158 );yyac++; 
					yya[yyac] = new YYARec(263,-158 );yyac++; 
					yya[yyac] = new YYARec(282,-158 );yyac++; 
					yya[yyac] = new YYARec(283,-158 );yyac++; 
					yya[yyac] = new YYARec(287,-158 );yyac++; 
					yya[yyac] = new YYARec(298,-158 );yyac++; 
					yya[yyac] = new YYARec(299,-158 );yyac++; 
					yya[yyac] = new YYARec(301,-158 );yyac++; 
					yya[yyac] = new YYARec(302,-158 );yyac++; 
					yya[yyac] = new YYARec(304,-158 );yyac++; 
					yya[yyac] = new YYARec(305,-158 );yyac++; 
					yya[yyac] = new YYARec(306,-158 );yyac++; 
					yya[yyac] = new YYARec(307,-158 );yyac++; 
					yya[yyac] = new YYARec(308,-158 );yyac++; 
					yya[yyac] = new YYARec(309,-158 );yyac++; 
					yya[yyac] = new YYARec(310,-158 );yyac++; 
					yya[yyac] = new YYARec(312,-158 );yyac++; 
					yya[yyac] = new YYARec(313,-158 );yyac++; 
					yya[yyac] = new YYARec(314,-158 );yyac++; 
					yya[yyac] = new YYARec(315,-158 );yyac++; 
					yya[yyac] = new YYARec(316,-158 );yyac++; 
					yya[yyac] = new YYARec(317,-158 );yyac++; 
					yya[yyac] = new YYARec(318,-158 );yyac++; 
					yya[yyac] = new YYARec(319,-158 );yyac++; 
					yya[yyac] = new YYARec(320,-158 );yyac++; 
					yya[yyac] = new YYARec(321,-158 );yyac++; 
					yya[yyac] = new YYARec(322,-158 );yyac++; 
					yya[yyac] = new YYARec(323,-158 );yyac++; 
					yya[yyac] = new YYARec(324,-158 );yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(258,-161 );yyac++; 
					yya[yyac] = new YYARec(263,-161 );yyac++; 
					yya[yyac] = new YYARec(282,-161 );yyac++; 
					yya[yyac] = new YYARec(283,-161 );yyac++; 
					yya[yyac] = new YYARec(287,-161 );yyac++; 
					yya[yyac] = new YYARec(298,-161 );yyac++; 
					yya[yyac] = new YYARec(299,-161 );yyac++; 
					yya[yyac] = new YYARec(301,-161 );yyac++; 
					yya[yyac] = new YYARec(302,-161 );yyac++; 
					yya[yyac] = new YYARec(304,-161 );yyac++; 
					yya[yyac] = new YYARec(305,-161 );yyac++; 
					yya[yyac] = new YYARec(306,-161 );yyac++; 
					yya[yyac] = new YYARec(307,-161 );yyac++; 
					yya[yyac] = new YYARec(308,-161 );yyac++; 
					yya[yyac] = new YYARec(309,-161 );yyac++; 
					yya[yyac] = new YYARec(310,-161 );yyac++; 
					yya[yyac] = new YYARec(312,-161 );yyac++; 
					yya[yyac] = new YYARec(313,-161 );yyac++; 
					yya[yyac] = new YYARec(314,-161 );yyac++; 
					yya[yyac] = new YYARec(315,-161 );yyac++; 
					yya[yyac] = new YYARec(316,-161 );yyac++; 
					yya[yyac] = new YYARec(317,-161 );yyac++; 
					yya[yyac] = new YYARec(318,-161 );yyac++; 
					yya[yyac] = new YYARec(319,-161 );yyac++; 
					yya[yyac] = new YYARec(320,-161 );yyac++; 
					yya[yyac] = new YYARec(321,-161 );yyac++; 
					yya[yyac] = new YYARec(322,-161 );yyac++; 
					yya[yyac] = new YYARec(323,-161 );yyac++; 
					yya[yyac] = new YYARec(324,-161 );yyac++; 
					yya[yyac] = new YYARec(268,-180 );yyac++; 
					yya[yyac] = new YYARec(289,-180 );yyac++; 
					yya[yyac] = new YYARec(290,-180 );yyac++; 
					yya[yyac] = new YYARec(291,-180 );yyac++; 
					yya[yyac] = new YYARec(292,-180 );yyac++; 
					yya[yyac] = new YYARec(293,-180 );yyac++; 
					yya[yyac] = new YYARec(297,-180 );yyac++; 
					yya[yyac] = new YYARec(268,-131 );yyac++; 
					yya[yyac] = new YYARec(289,-131 );yyac++; 
					yya[yyac] = new YYARec(290,-131 );yyac++; 
					yya[yyac] = new YYARec(291,-131 );yyac++; 
					yya[yyac] = new YYARec(292,-131 );yyac++; 
					yya[yyac] = new YYARec(293,-131 );yyac++; 
					yya[yyac] = new YYARec(297,-131 );yyac++; 
					yya[yyac] = new YYARec(258,-160 );yyac++; 
					yya[yyac] = new YYARec(263,-160 );yyac++; 
					yya[yyac] = new YYARec(282,-160 );yyac++; 
					yya[yyac] = new YYARec(283,-160 );yyac++; 
					yya[yyac] = new YYARec(287,-160 );yyac++; 
					yya[yyac] = new YYARec(298,-160 );yyac++; 
					yya[yyac] = new YYARec(299,-160 );yyac++; 
					yya[yyac] = new YYARec(301,-160 );yyac++; 
					yya[yyac] = new YYARec(302,-160 );yyac++; 
					yya[yyac] = new YYARec(304,-160 );yyac++; 
					yya[yyac] = new YYARec(305,-160 );yyac++; 
					yya[yyac] = new YYARec(306,-160 );yyac++; 
					yya[yyac] = new YYARec(307,-160 );yyac++; 
					yya[yyac] = new YYARec(308,-160 );yyac++; 
					yya[yyac] = new YYARec(309,-160 );yyac++; 
					yya[yyac] = new YYARec(310,-160 );yyac++; 
					yya[yyac] = new YYARec(312,-160 );yyac++; 
					yya[yyac] = new YYARec(313,-160 );yyac++; 
					yya[yyac] = new YYARec(314,-160 );yyac++; 
					yya[yyac] = new YYARec(315,-160 );yyac++; 
					yya[yyac] = new YYARec(316,-160 );yyac++; 
					yya[yyac] = new YYARec(317,-160 );yyac++; 
					yya[yyac] = new YYARec(318,-160 );yyac++; 
					yya[yyac] = new YYARec(319,-160 );yyac++; 
					yya[yyac] = new YYARec(320,-160 );yyac++; 
					yya[yyac] = new YYARec(321,-160 );yyac++; 
					yya[yyac] = new YYARec(322,-160 );yyac++; 
					yya[yyac] = new YYARec(323,-160 );yyac++; 
					yya[yyac] = new YYARec(324,-160 );yyac++; 
					yya[yyac] = new YYARec(258,-159 );yyac++; 
					yya[yyac] = new YYARec(263,-159 );yyac++; 
					yya[yyac] = new YYARec(282,-159 );yyac++; 
					yya[yyac] = new YYARec(283,-159 );yyac++; 
					yya[yyac] = new YYARec(287,-159 );yyac++; 
					yya[yyac] = new YYARec(298,-159 );yyac++; 
					yya[yyac] = new YYARec(299,-159 );yyac++; 
					yya[yyac] = new YYARec(301,-159 );yyac++; 
					yya[yyac] = new YYARec(302,-159 );yyac++; 
					yya[yyac] = new YYARec(304,-159 );yyac++; 
					yya[yyac] = new YYARec(305,-159 );yyac++; 
					yya[yyac] = new YYARec(306,-159 );yyac++; 
					yya[yyac] = new YYARec(307,-159 );yyac++; 
					yya[yyac] = new YYARec(308,-159 );yyac++; 
					yya[yyac] = new YYARec(309,-159 );yyac++; 
					yya[yyac] = new YYARec(310,-159 );yyac++; 
					yya[yyac] = new YYARec(312,-159 );yyac++; 
					yya[yyac] = new YYARec(313,-159 );yyac++; 
					yya[yyac] = new YYARec(314,-159 );yyac++; 
					yya[yyac] = new YYARec(315,-159 );yyac++; 
					yya[yyac] = new YYARec(316,-159 );yyac++; 
					yya[yyac] = new YYARec(317,-159 );yyac++; 
					yya[yyac] = new YYARec(318,-159 );yyac++; 
					yya[yyac] = new YYARec(319,-159 );yyac++; 
					yya[yyac] = new YYARec(320,-159 );yyac++; 
					yya[yyac] = new YYARec(321,-159 );yyac++; 
					yya[yyac] = new YYARec(322,-159 );yyac++; 
					yya[yyac] = new YYARec(323,-159 );yyac++; 
					yya[yyac] = new YYARec(324,-159 );yyac++; 
					yya[yyac] = new YYARec(268,-179 );yyac++; 
					yya[yyac] = new YYARec(289,-179 );yyac++; 
					yya[yyac] = new YYARec(290,-179 );yyac++; 
					yya[yyac] = new YYARec(291,-179 );yyac++; 
					yya[yyac] = new YYARec(292,-179 );yyac++; 
					yya[yyac] = new YYARec(293,-179 );yyac++; 
					yya[yyac] = new YYARec(297,-179 );yyac++; 
					yya[yyac] = new YYARec(263,73);yyac++; 
					yya[yyac] = new YYARec(282,-83 );yyac++; 
					yya[yyac] = new YYARec(283,-83 );yyac++; 
					yya[yyac] = new YYARec(287,-83 );yyac++; 
					yya[yyac] = new YYARec(298,-83 );yyac++; 
					yya[yyac] = new YYARec(301,-83 );yyac++; 
					yya[yyac] = new YYARec(302,-83 );yyac++; 
					yya[yyac] = new YYARec(303,-83 );yyac++; 
					yya[yyac] = new YYARec(304,-83 );yyac++; 
					yya[yyac] = new YYARec(305,-83 );yyac++; 
					yya[yyac] = new YYARec(306,-83 );yyac++; 
					yya[yyac] = new YYARec(307,-83 );yyac++; 
					yya[yyac] = new YYARec(309,-83 );yyac++; 
					yya[yyac] = new YYARec(313,-83 );yyac++; 
					yya[yyac] = new YYARec(314,-83 );yyac++; 
					yya[yyac] = new YYARec(315,-83 );yyac++; 
					yya[yyac] = new YYARec(317,-83 );yyac++; 
					yya[yyac] = new YYARec(318,-83 );yyac++; 
					yya[yyac] = new YYARec(320,-83 );yyac++; 
					yya[yyac] = new YYARec(321,-83 );yyac++; 
					yya[yyac] = new YYARec(322,-83 );yyac++; 
					yya[yyac] = new YYARec(323,-83 );yyac++; 
					yya[yyac] = new YYARec(324,-83 );yyac++; 
					yya[yyac] = new YYARec(258,192);yyac++; 
					yya[yyac] = new YYARec(267,193);yyac++; 
					yya[yyac] = new YYARec(257,137);yyac++; 
					yya[yyac] = new YYARec(259,138);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,139);yyac++; 
					yya[yyac] = new YYARec(311,140);yyac++; 
					yya[yyac] = new YYARec(312,141);yyac++; 
					yya[yyac] = new YYARec(315,142);yyac++; 
					yya[yyac] = new YYARec(317,143);yyac++; 
					yya[yyac] = new YYARec(319,144);yyac++; 
					yya[yyac] = new YYARec(260,-52 );yyac++; 
					yya[yyac] = new YYARec(261,-52 );yyac++; 
					yya[yyac] = new YYARec(267,-52 );yyac++; 
					yya[yyac] = new YYARec(322,61);yyac++; 
					yya[yyac] = new YYARec(322,61);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(320,59);yyac++; 
					yya[yyac] = new YYARec(258,-42 );yyac++; 
					yya[yyac] = new YYARec(258,200);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(305,203);yyac++; 
					yya[yyac] = new YYARec(306,139);yyac++; 
					yya[yyac] = new YYARec(311,140);yyac++; 
					yya[yyac] = new YYARec(312,141);yyac++; 
					yya[yyac] = new YYARec(313,204);yyac++; 
					yya[yyac] = new YYARec(315,142);yyac++; 
					yya[yyac] = new YYARec(316,205);yyac++; 
					yya[yyac] = new YYARec(317,143);yyac++; 
					yya[yyac] = new YYARec(318,206);yyac++; 
					yya[yyac] = new YYARec(319,144);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(305,203);yyac++; 
					yya[yyac] = new YYARec(306,139);yyac++; 
					yya[yyac] = new YYARec(311,140);yyac++; 
					yya[yyac] = new YYARec(312,141);yyac++; 
					yya[yyac] = new YYARec(313,204);yyac++; 
					yya[yyac] = new YYARec(315,142);yyac++; 
					yya[yyac] = new YYARec(316,205);yyac++; 
					yya[yyac] = new YYARec(317,143);yyac++; 
					yya[yyac] = new YYARec(318,206);yyac++; 
					yya[yyac] = new YYARec(319,144);yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(308,107);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,59);yyac++; 
					yya[yyac] = new YYARec(321,60);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(323,62);yyac++; 
					yya[yyac] = new YYARec(324,63);yyac++; 
					yya[yyac] = new YYARec(257,120);yyac++; 
					yya[yyac] = new YYARec(259,121);yyac++; 
					yya[yyac] = new YYARec(266,122);yyac++; 
					yya[yyac] = new YYARec(288,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(296,126);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,127);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,128);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,129);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(260,-71 );yyac++; 
					yya[yyac] = new YYARec(261,-71 );yyac++; 
					yya[yyac] = new YYARec(267,-71 );yyac++; 
					yya[yyac] = new YYARec(257,120);yyac++; 
					yya[yyac] = new YYARec(259,121);yyac++; 
					yya[yyac] = new YYARec(266,122);yyac++; 
					yya[yyac] = new YYARec(288,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(296,126);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,127);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,128);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,129);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(260,-71 );yyac++; 
					yya[yyac] = new YYARec(261,-71 );yyac++; 
					yya[yyac] = new YYARec(267,-71 );yyac++; 
					yya[yyac] = new YYARec(258,214);yyac++; 
					yya[yyac] = new YYARec(258,215);yyac++; 
					yya[yyac] = new YYARec(267,216);yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(258,-184 );yyac++; 
					yya[yyac] = new YYARec(266,-184 );yyac++; 
					yya[yyac] = new YYARec(269,-184 );yyac++; 
					yya[yyac] = new YYARec(270,-184 );yyac++; 
					yya[yyac] = new YYARec(271,-184 );yyac++; 
					yya[yyac] = new YYARec(272,-184 );yyac++; 
					yya[yyac] = new YYARec(273,-184 );yyac++; 
					yya[yyac] = new YYARec(275,-184 );yyac++; 
					yya[yyac] = new YYARec(276,-184 );yyac++; 
					yya[yyac] = new YYARec(277,-184 );yyac++; 
					yya[yyac] = new YYARec(278,-184 );yyac++; 
					yya[yyac] = new YYARec(279,-184 );yyac++; 
					yya[yyac] = new YYARec(280,-184 );yyac++; 
					yya[yyac] = new YYARec(281,-184 );yyac++; 
					yya[yyac] = new YYARec(282,-184 );yyac++; 
					yya[yyac] = new YYARec(283,-184 );yyac++; 
					yya[yyac] = new YYARec(284,-184 );yyac++; 
					yya[yyac] = new YYARec(285,-184 );yyac++; 
					yya[yyac] = new YYARec(286,-184 );yyac++; 
					yya[yyac] = new YYARec(289,-184 );yyac++; 
					yya[yyac] = new YYARec(290,-184 );yyac++; 
					yya[yyac] = new YYARec(291,-184 );yyac++; 
					yya[yyac] = new YYARec(292,-184 );yyac++; 
					yya[yyac] = new YYARec(293,-184 );yyac++; 
					yya[yyac] = new YYARec(297,-184 );yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(284,220);yyac++; 
					yya[yyac] = new YYARec(285,221);yyac++; 
					yya[yyac] = new YYARec(286,222);yyac++; 
					yya[yyac] = new YYARec(258,-99 );yyac++; 
					yya[yyac] = new YYARec(266,-99 );yyac++; 
					yya[yyac] = new YYARec(269,-99 );yyac++; 
					yya[yyac] = new YYARec(270,-99 );yyac++; 
					yya[yyac] = new YYARec(271,-99 );yyac++; 
					yya[yyac] = new YYARec(272,-99 );yyac++; 
					yya[yyac] = new YYARec(273,-99 );yyac++; 
					yya[yyac] = new YYARec(275,-99 );yyac++; 
					yya[yyac] = new YYARec(276,-99 );yyac++; 
					yya[yyac] = new YYARec(277,-99 );yyac++; 
					yya[yyac] = new YYARec(278,-99 );yyac++; 
					yya[yyac] = new YYARec(279,-99 );yyac++; 
					yya[yyac] = new YYARec(280,-99 );yyac++; 
					yya[yyac] = new YYARec(281,-99 );yyac++; 
					yya[yyac] = new YYARec(282,-99 );yyac++; 
					yya[yyac] = new YYARec(283,-99 );yyac++; 
					yya[yyac] = new YYARec(282,224);yyac++; 
					yya[yyac] = new YYARec(283,225);yyac++; 
					yya[yyac] = new YYARec(258,-97 );yyac++; 
					yya[yyac] = new YYARec(266,-97 );yyac++; 
					yya[yyac] = new YYARec(269,-97 );yyac++; 
					yya[yyac] = new YYARec(270,-97 );yyac++; 
					yya[yyac] = new YYARec(271,-97 );yyac++; 
					yya[yyac] = new YYARec(272,-97 );yyac++; 
					yya[yyac] = new YYARec(273,-97 );yyac++; 
					yya[yyac] = new YYARec(275,-97 );yyac++; 
					yya[yyac] = new YYARec(276,-97 );yyac++; 
					yya[yyac] = new YYARec(277,-97 );yyac++; 
					yya[yyac] = new YYARec(278,-97 );yyac++; 
					yya[yyac] = new YYARec(279,-97 );yyac++; 
					yya[yyac] = new YYARec(280,-97 );yyac++; 
					yya[yyac] = new YYARec(281,-97 );yyac++; 
					yya[yyac] = new YYARec(278,227);yyac++; 
					yya[yyac] = new YYARec(279,228);yyac++; 
					yya[yyac] = new YYARec(280,229);yyac++; 
					yya[yyac] = new YYARec(281,230);yyac++; 
					yya[yyac] = new YYARec(258,-95 );yyac++; 
					yya[yyac] = new YYARec(266,-95 );yyac++; 
					yya[yyac] = new YYARec(269,-95 );yyac++; 
					yya[yyac] = new YYARec(270,-95 );yyac++; 
					yya[yyac] = new YYARec(271,-95 );yyac++; 
					yya[yyac] = new YYARec(272,-95 );yyac++; 
					yya[yyac] = new YYARec(273,-95 );yyac++; 
					yya[yyac] = new YYARec(275,-95 );yyac++; 
					yya[yyac] = new YYARec(276,-95 );yyac++; 
					yya[yyac] = new YYARec(277,-95 );yyac++; 
					yya[yyac] = new YYARec(276,232);yyac++; 
					yya[yyac] = new YYARec(277,233);yyac++; 
					yya[yyac] = new YYARec(258,-94 );yyac++; 
					yya[yyac] = new YYARec(266,-94 );yyac++; 
					yya[yyac] = new YYARec(269,-94 );yyac++; 
					yya[yyac] = new YYARec(270,-94 );yyac++; 
					yya[yyac] = new YYARec(271,-94 );yyac++; 
					yya[yyac] = new YYARec(272,-94 );yyac++; 
					yya[yyac] = new YYARec(273,-94 );yyac++; 
					yya[yyac] = new YYARec(275,-94 );yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(258,-92 );yyac++; 
					yya[yyac] = new YYARec(266,-92 );yyac++; 
					yya[yyac] = new YYARec(269,-92 );yyac++; 
					yya[yyac] = new YYARec(270,-92 );yyac++; 
					yya[yyac] = new YYARec(271,-92 );yyac++; 
					yya[yyac] = new YYARec(272,-92 );yyac++; 
					yya[yyac] = new YYARec(275,-92 );yyac++; 
					yya[yyac] = new YYARec(272,235);yyac++; 
					yya[yyac] = new YYARec(258,-90 );yyac++; 
					yya[yyac] = new YYARec(266,-90 );yyac++; 
					yya[yyac] = new YYARec(269,-90 );yyac++; 
					yya[yyac] = new YYARec(270,-90 );yyac++; 
					yya[yyac] = new YYARec(271,-90 );yyac++; 
					yya[yyac] = new YYARec(275,-90 );yyac++; 
					yya[yyac] = new YYARec(271,236);yyac++; 
					yya[yyac] = new YYARec(258,-88 );yyac++; 
					yya[yyac] = new YYARec(266,-88 );yyac++; 
					yya[yyac] = new YYARec(269,-88 );yyac++; 
					yya[yyac] = new YYARec(270,-88 );yyac++; 
					yya[yyac] = new YYARec(275,-88 );yyac++; 
					yya[yyac] = new YYARec(270,237);yyac++; 
					yya[yyac] = new YYARec(258,-86 );yyac++; 
					yya[yyac] = new YYARec(266,-86 );yyac++; 
					yya[yyac] = new YYARec(269,-86 );yyac++; 
					yya[yyac] = new YYARec(275,-86 );yyac++; 
					yya[yyac] = new YYARec(269,238);yyac++; 
					yya[yyac] = new YYARec(258,-84 );yyac++; 
					yya[yyac] = new YYARec(266,-84 );yyac++; 
					yya[yyac] = new YYARec(275,-84 );yyac++; 
					yya[yyac] = new YYARec(289,154);yyac++; 
					yya[yyac] = new YYARec(290,155);yyac++; 
					yya[yyac] = new YYARec(291,156);yyac++; 
					yya[yyac] = new YYARec(292,157);yyac++; 
					yya[yyac] = new YYARec(293,158);yyac++; 
					yya[yyac] = new YYARec(258,-108 );yyac++; 
					yya[yyac] = new YYARec(269,-108 );yyac++; 
					yya[yyac] = new YYARec(270,-108 );yyac++; 
					yya[yyac] = new YYARec(271,-108 );yyac++; 
					yya[yyac] = new YYARec(272,-108 );yyac++; 
					yya[yyac] = new YYARec(273,-108 );yyac++; 
					yya[yyac] = new YYARec(276,-108 );yyac++; 
					yya[yyac] = new YYARec(277,-108 );yyac++; 
					yya[yyac] = new YYARec(278,-108 );yyac++; 
					yya[yyac] = new YYARec(279,-108 );yyac++; 
					yya[yyac] = new YYARec(280,-108 );yyac++; 
					yya[yyac] = new YYARec(281,-108 );yyac++; 
					yya[yyac] = new YYARec(282,-108 );yyac++; 
					yya[yyac] = new YYARec(283,-108 );yyac++; 
					yya[yyac] = new YYARec(284,-108 );yyac++; 
					yya[yyac] = new YYARec(285,-108 );yyac++; 
					yya[yyac] = new YYARec(286,-108 );yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(257,120);yyac++; 
					yya[yyac] = new YYARec(259,121);yyac++; 
					yya[yyac] = new YYARec(266,122);yyac++; 
					yya[yyac] = new YYARec(288,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(296,126);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,127);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,128);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,129);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(267,-71 );yyac++; 
					yya[yyac] = new YYARec(266,242);yyac++; 
					yya[yyac] = new YYARec(266,243);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,252);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(320,59);yyac++; 
					yya[yyac] = new YYARec(321,60);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(323,62);yyac++; 
					yya[yyac] = new YYARec(324,63);yyac++; 
					yya[yyac] = new YYARec(257,137);yyac++; 
					yya[yyac] = new YYARec(259,138);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,139);yyac++; 
					yya[yyac] = new YYARec(311,140);yyac++; 
					yya[yyac] = new YYARec(312,141);yyac++; 
					yya[yyac] = new YYARec(315,142);yyac++; 
					yya[yyac] = new YYARec(317,143);yyac++; 
					yya[yyac] = new YYARec(319,144);yyac++; 
					yya[yyac] = new YYARec(260,-52 );yyac++; 
					yya[yyac] = new YYARec(261,-52 );yyac++; 
					yya[yyac] = new YYARec(267,-52 );yyac++; 
					yya[yyac] = new YYARec(258,254);yyac++; 
					yya[yyac] = new YYARec(258,255);yyac++; 
					yya[yyac] = new YYARec(320,77);yyac++; 
					yya[yyac] = new YYARec(263,256);yyac++; 
					yya[yyac] = new YYARec(258,-41 );yyac++; 
					yya[yyac] = new YYARec(258,257);yyac++; 
					yya[yyac] = new YYARec(257,17);yyac++; 
					yya[yyac] = new YYARec(259,18);yyac++; 
					yya[yyac] = new YYARec(262,19);yyac++; 
					yya[yyac] = new YYARec(264,20);yyac++; 
					yya[yyac] = new YYARec(265,21);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,23);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(311,27);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(263,73);yyac++; 
					yya[yyac] = new YYARec(258,-83 );yyac++; 
					yya[yyac] = new YYARec(282,-83 );yyac++; 
					yya[yyac] = new YYARec(283,-83 );yyac++; 
					yya[yyac] = new YYARec(287,-83 );yyac++; 
					yya[yyac] = new YYARec(298,-83 );yyac++; 
					yya[yyac] = new YYARec(299,-83 );yyac++; 
					yya[yyac] = new YYARec(301,-83 );yyac++; 
					yya[yyac] = new YYARec(302,-83 );yyac++; 
					yya[yyac] = new YYARec(304,-83 );yyac++; 
					yya[yyac] = new YYARec(305,-83 );yyac++; 
					yya[yyac] = new YYARec(306,-83 );yyac++; 
					yya[yyac] = new YYARec(307,-83 );yyac++; 
					yya[yyac] = new YYARec(308,-83 );yyac++; 
					yya[yyac] = new YYARec(309,-83 );yyac++; 
					yya[yyac] = new YYARec(310,-83 );yyac++; 
					yya[yyac] = new YYARec(312,-83 );yyac++; 
					yya[yyac] = new YYARec(313,-83 );yyac++; 
					yya[yyac] = new YYARec(314,-83 );yyac++; 
					yya[yyac] = new YYARec(315,-83 );yyac++; 
					yya[yyac] = new YYARec(316,-83 );yyac++; 
					yya[yyac] = new YYARec(317,-83 );yyac++; 
					yya[yyac] = new YYARec(318,-83 );yyac++; 
					yya[yyac] = new YYARec(319,-83 );yyac++; 
					yya[yyac] = new YYARec(320,-83 );yyac++; 
					yya[yyac] = new YYARec(321,-83 );yyac++; 
					yya[yyac] = new YYARec(322,-83 );yyac++; 
					yya[yyac] = new YYARec(323,-83 );yyac++; 
					yya[yyac] = new YYARec(324,-83 );yyac++; 
					yya[yyac] = new YYARec(257,120);yyac++; 
					yya[yyac] = new YYARec(259,121);yyac++; 
					yya[yyac] = new YYARec(266,122);yyac++; 
					yya[yyac] = new YYARec(288,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(296,126);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,127);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,128);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,129);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(260,-71 );yyac++; 
					yya[yyac] = new YYARec(261,-71 );yyac++; 
					yya[yyac] = new YYARec(257,120);yyac++; 
					yya[yyac] = new YYARec(259,121);yyac++; 
					yya[yyac] = new YYARec(266,122);yyac++; 
					yya[yyac] = new YYARec(288,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(296,126);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,127);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,128);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,129);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(260,-71 );yyac++; 
					yya[yyac] = new YYARec(261,-71 );yyac++; 
					yya[yyac] = new YYARec(257,120);yyac++; 
					yya[yyac] = new YYARec(259,121);yyac++; 
					yya[yyac] = new YYARec(266,122);yyac++; 
					yya[yyac] = new YYARec(288,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(296,126);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,127);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,128);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,129);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(260,-71 );yyac++; 
					yya[yyac] = new YYARec(261,-71 );yyac++; 
					yya[yyac] = new YYARec(267,-71 );yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(274,184);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,185);yyac++; 
					yya[yyac] = new YYARec(321,186);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(275,275);yyac++; 
					yya[yyac] = new YYARec(267,276);yyac++; 
					yya[yyac] = new YYARec(257,120);yyac++; 
					yya[yyac] = new YYARec(259,121);yyac++; 
					yya[yyac] = new YYARec(266,122);yyac++; 
					yya[yyac] = new YYARec(288,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(296,126);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,127);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,128);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,129);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(267,-71 );yyac++; 
					yya[yyac] = new YYARec(257,120);yyac++; 
					yya[yyac] = new YYARec(259,121);yyac++; 
					yya[yyac] = new YYARec(266,122);yyac++; 
					yya[yyac] = new YYARec(288,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(296,126);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,127);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,128);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,129);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(267,-71 );yyac++; 
					yya[yyac] = new YYARec(263,73);yyac++; 
					yya[yyac] = new YYARec(258,-83 );yyac++; 
					yya[yyac] = new YYARec(282,-83 );yyac++; 
					yya[yyac] = new YYARec(283,-83 );yyac++; 
					yya[yyac] = new YYARec(287,-83 );yyac++; 
					yya[yyac] = new YYARec(298,-83 );yyac++; 
					yya[yyac] = new YYARec(301,-83 );yyac++; 
					yya[yyac] = new YYARec(302,-83 );yyac++; 
					yya[yyac] = new YYARec(303,-83 );yyac++; 
					yya[yyac] = new YYARec(304,-83 );yyac++; 
					yya[yyac] = new YYARec(305,-83 );yyac++; 
					yya[yyac] = new YYARec(306,-83 );yyac++; 
					yya[yyac] = new YYARec(307,-83 );yyac++; 
					yya[yyac] = new YYARec(309,-83 );yyac++; 
					yya[yyac] = new YYARec(313,-83 );yyac++; 
					yya[yyac] = new YYARec(314,-83 );yyac++; 
					yya[yyac] = new YYARec(315,-83 );yyac++; 
					yya[yyac] = new YYARec(317,-83 );yyac++; 
					yya[yyac] = new YYARec(318,-83 );yyac++; 
					yya[yyac] = new YYARec(320,-83 );yyac++; 
					yya[yyac] = new YYARec(321,-83 );yyac++; 
					yya[yyac] = new YYARec(322,-83 );yyac++; 
					yya[yyac] = new YYARec(323,-83 );yyac++; 
					yya[yyac] = new YYARec(324,-83 );yyac++; 
					yya[yyac] = new YYARec(297,280);yyac++; 
					yya[yyac] = new YYARec(258,-62 );yyac++; 
					yya[yyac] = new YYARec(263,-62 );yyac++; 
					yya[yyac] = new YYARec(282,-62 );yyac++; 
					yya[yyac] = new YYARec(283,-62 );yyac++; 
					yya[yyac] = new YYARec(287,-62 );yyac++; 
					yya[yyac] = new YYARec(298,-62 );yyac++; 
					yya[yyac] = new YYARec(301,-62 );yyac++; 
					yya[yyac] = new YYARec(302,-62 );yyac++; 
					yya[yyac] = new YYARec(303,-62 );yyac++; 
					yya[yyac] = new YYARec(304,-62 );yyac++; 
					yya[yyac] = new YYARec(305,-62 );yyac++; 
					yya[yyac] = new YYARec(306,-62 );yyac++; 
					yya[yyac] = new YYARec(307,-62 );yyac++; 
					yya[yyac] = new YYARec(309,-62 );yyac++; 
					yya[yyac] = new YYARec(313,-62 );yyac++; 
					yya[yyac] = new YYARec(314,-62 );yyac++; 
					yya[yyac] = new YYARec(315,-62 );yyac++; 
					yya[yyac] = new YYARec(317,-62 );yyac++; 
					yya[yyac] = new YYARec(318,-62 );yyac++; 
					yya[yyac] = new YYARec(320,-62 );yyac++; 
					yya[yyac] = new YYARec(321,-62 );yyac++; 
					yya[yyac] = new YYARec(322,-62 );yyac++; 
					yya[yyac] = new YYARec(323,-62 );yyac++; 
					yya[yyac] = new YYARec(324,-62 );yyac++; 
					yya[yyac] = new YYARec(257,137);yyac++; 
					yya[yyac] = new YYARec(259,138);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,139);yyac++; 
					yya[yyac] = new YYARec(311,140);yyac++; 
					yya[yyac] = new YYARec(312,141);yyac++; 
					yya[yyac] = new YYARec(315,142);yyac++; 
					yya[yyac] = new YYARec(317,143);yyac++; 
					yya[yyac] = new YYARec(319,144);yyac++; 
					yya[yyac] = new YYARec(260,-52 );yyac++; 
					yya[yyac] = new YYARec(261,-52 );yyac++; 
					yya[yyac] = new YYARec(257,137);yyac++; 
					yya[yyac] = new YYARec(259,138);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,139);yyac++; 
					yya[yyac] = new YYARec(311,140);yyac++; 
					yya[yyac] = new YYARec(312,141);yyac++; 
					yya[yyac] = new YYARec(315,142);yyac++; 
					yya[yyac] = new YYARec(317,143);yyac++; 
					yya[yyac] = new YYARec(319,144);yyac++; 
					yya[yyac] = new YYARec(260,-52 );yyac++; 
					yya[yyac] = new YYARec(261,-52 );yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(320,59);yyac++; 
					yya[yyac] = new YYARec(258,-42 );yyac++; 
					yya[yyac] = new YYARec(261,285);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(308,107);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(320,59);yyac++; 
					yya[yyac] = new YYARec(321,60);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(323,62);yyac++; 
					yya[yyac] = new YYARec(324,63);yyac++; 
					yya[yyac] = new YYARec(258,-75 );yyac++; 
					yya[yyac] = new YYARec(260,287);yyac++; 
					yya[yyac] = new YYARec(261,288);yyac++; 
					yya[yyac] = new YYARec(258,289);yyac++; 
					yya[yyac] = new YYARec(258,290);yyac++; 
					yya[yyac] = new YYARec(275,291);yyac++; 
					yya[yyac] = new YYARec(284,220);yyac++; 
					yya[yyac] = new YYARec(285,221);yyac++; 
					yya[yyac] = new YYARec(286,222);yyac++; 
					yya[yyac] = new YYARec(258,-100 );yyac++; 
					yya[yyac] = new YYARec(266,-100 );yyac++; 
					yya[yyac] = new YYARec(269,-100 );yyac++; 
					yya[yyac] = new YYARec(270,-100 );yyac++; 
					yya[yyac] = new YYARec(271,-100 );yyac++; 
					yya[yyac] = new YYARec(272,-100 );yyac++; 
					yya[yyac] = new YYARec(273,-100 );yyac++; 
					yya[yyac] = new YYARec(275,-100 );yyac++; 
					yya[yyac] = new YYARec(276,-100 );yyac++; 
					yya[yyac] = new YYARec(277,-100 );yyac++; 
					yya[yyac] = new YYARec(278,-100 );yyac++; 
					yya[yyac] = new YYARec(279,-100 );yyac++; 
					yya[yyac] = new YYARec(280,-100 );yyac++; 
					yya[yyac] = new YYARec(281,-100 );yyac++; 
					yya[yyac] = new YYARec(282,-100 );yyac++; 
					yya[yyac] = new YYARec(283,-100 );yyac++; 
					yya[yyac] = new YYARec(282,224);yyac++; 
					yya[yyac] = new YYARec(283,225);yyac++; 
					yya[yyac] = new YYARec(258,-98 );yyac++; 
					yya[yyac] = new YYARec(266,-98 );yyac++; 
					yya[yyac] = new YYARec(269,-98 );yyac++; 
					yya[yyac] = new YYARec(270,-98 );yyac++; 
					yya[yyac] = new YYARec(271,-98 );yyac++; 
					yya[yyac] = new YYARec(272,-98 );yyac++; 
					yya[yyac] = new YYARec(273,-98 );yyac++; 
					yya[yyac] = new YYARec(275,-98 );yyac++; 
					yya[yyac] = new YYARec(276,-98 );yyac++; 
					yya[yyac] = new YYARec(277,-98 );yyac++; 
					yya[yyac] = new YYARec(278,-98 );yyac++; 
					yya[yyac] = new YYARec(279,-98 );yyac++; 
					yya[yyac] = new YYARec(280,-98 );yyac++; 
					yya[yyac] = new YYARec(281,-98 );yyac++; 
					yya[yyac] = new YYARec(278,227);yyac++; 
					yya[yyac] = new YYARec(279,228);yyac++; 
					yya[yyac] = new YYARec(280,229);yyac++; 
					yya[yyac] = new YYARec(281,230);yyac++; 
					yya[yyac] = new YYARec(258,-96 );yyac++; 
					yya[yyac] = new YYARec(266,-96 );yyac++; 
					yya[yyac] = new YYARec(269,-96 );yyac++; 
					yya[yyac] = new YYARec(270,-96 );yyac++; 
					yya[yyac] = new YYARec(271,-96 );yyac++; 
					yya[yyac] = new YYARec(272,-96 );yyac++; 
					yya[yyac] = new YYARec(273,-96 );yyac++; 
					yya[yyac] = new YYARec(275,-96 );yyac++; 
					yya[yyac] = new YYARec(276,-96 );yyac++; 
					yya[yyac] = new YYARec(277,-96 );yyac++; 
					yya[yyac] = new YYARec(276,232);yyac++; 
					yya[yyac] = new YYARec(277,233);yyac++; 
					yya[yyac] = new YYARec(258,-93 );yyac++; 
					yya[yyac] = new YYARec(266,-93 );yyac++; 
					yya[yyac] = new YYARec(269,-93 );yyac++; 
					yya[yyac] = new YYARec(270,-93 );yyac++; 
					yya[yyac] = new YYARec(271,-93 );yyac++; 
					yya[yyac] = new YYARec(272,-93 );yyac++; 
					yya[yyac] = new YYARec(273,-93 );yyac++; 
					yya[yyac] = new YYARec(275,-93 );yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(258,-91 );yyac++; 
					yya[yyac] = new YYARec(266,-91 );yyac++; 
					yya[yyac] = new YYARec(269,-91 );yyac++; 
					yya[yyac] = new YYARec(270,-91 );yyac++; 
					yya[yyac] = new YYARec(271,-91 );yyac++; 
					yya[yyac] = new YYARec(272,-91 );yyac++; 
					yya[yyac] = new YYARec(275,-91 );yyac++; 
					yya[yyac] = new YYARec(272,235);yyac++; 
					yya[yyac] = new YYARec(258,-89 );yyac++; 
					yya[yyac] = new YYARec(266,-89 );yyac++; 
					yya[yyac] = new YYARec(269,-89 );yyac++; 
					yya[yyac] = new YYARec(270,-89 );yyac++; 
					yya[yyac] = new YYARec(271,-89 );yyac++; 
					yya[yyac] = new YYARec(275,-89 );yyac++; 
					yya[yyac] = new YYARec(271,236);yyac++; 
					yya[yyac] = new YYARec(258,-87 );yyac++; 
					yya[yyac] = new YYARec(266,-87 );yyac++; 
					yya[yyac] = new YYARec(269,-87 );yyac++; 
					yya[yyac] = new YYARec(270,-87 );yyac++; 
					yya[yyac] = new YYARec(275,-87 );yyac++; 
					yya[yyac] = new YYARec(270,237);yyac++; 
					yya[yyac] = new YYARec(258,-85 );yyac++; 
					yya[yyac] = new YYARec(266,-85 );yyac++; 
					yya[yyac] = new YYARec(269,-85 );yyac++; 
					yya[yyac] = new YYARec(275,-85 );yyac++; 
					yya[yyac] = new YYARec(267,292);yyac++; 
					yya[yyac] = new YYARec(267,293);yyac++; 
					yya[yyac] = new YYARec(282,56);yyac++; 
					yya[yyac] = new YYARec(283,57);yyac++; 
					yya[yyac] = new YYARec(287,58);yyac++; 
					yya[yyac] = new YYARec(298,252);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,37);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,39);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,42);yyac++; 
					yya[yyac] = new YYARec(320,59);yyac++; 
					yya[yyac] = new YYARec(321,60);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(323,62);yyac++; 
					yya[yyac] = new YYARec(324,63);yyac++; 
					yya[yyac] = new YYARec(258,-54 );yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,139);yyac++; 
					yya[yyac] = new YYARec(311,140);yyac++; 
					yya[yyac] = new YYARec(312,141);yyac++; 
					yya[yyac] = new YYARec(315,142);yyac++; 
					yya[yyac] = new YYARec(317,143);yyac++; 
					yya[yyac] = new YYARec(319,144);yyac++; 
					yya[yyac] = new YYARec(260,296);yyac++; 
					yya[yyac] = new YYARec(261,297);yyac++; 
					yya[yyac] = new YYARec(258,298);yyac++; 
					yya[yyac] = new YYARec(258,299);yyac++; 
					yya[yyac] = new YYARec(258,300);yyac++; 
					yya[yyac] = new YYARec(258,301);yyac++; 
					yya[yyac] = new YYARec(257,120);yyac++; 
					yya[yyac] = new YYARec(259,121);yyac++; 
					yya[yyac] = new YYARec(266,122);yyac++; 
					yya[yyac] = new YYARec(288,123);yyac++; 
					yya[yyac] = new YYARec(294,124);yyac++; 
					yya[yyac] = new YYARec(295,125);yyac++; 
					yya[yyac] = new YYARec(296,126);yyac++; 
					yya[yyac] = new YYARec(298,106);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,34);yyac++; 
					yya[yyac] = new YYARec(305,35);yyac++; 
					yya[yyac] = new YYARec(306,36);yyac++; 
					yya[yyac] = new YYARec(307,127);yyac++; 
					yya[yyac] = new YYARec(309,38);yyac++; 
					yya[yyac] = new YYARec(310,108);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,128);yyac++; 
					yya[yyac] = new YYARec(315,40);yyac++; 
					yya[yyac] = new YYARec(316,109);yyac++; 
					yya[yyac] = new YYARec(317,41);yyac++; 
					yya[yyac] = new YYARec(318,129);yyac++; 
					yya[yyac] = new YYARec(319,110);yyac++; 
					yya[yyac] = new YYARec(322,43);yyac++; 
					yya[yyac] = new YYARec(261,-71 );yyac++; 
					yya[yyac] = new YYARec(257,137);yyac++; 
					yya[yyac] = new YYARec(259,138);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,139);yyac++; 
					yya[yyac] = new YYARec(311,140);yyac++; 
					yya[yyac] = new YYARec(312,141);yyac++; 
					yya[yyac] = new YYARec(315,142);yyac++; 
					yya[yyac] = new YYARec(317,143);yyac++; 
					yya[yyac] = new YYARec(319,144);yyac++; 
					yya[yyac] = new YYARec(261,-52 );yyac++; 
					yya[yyac] = new YYARec(261,304);yyac++; 
					yya[yyac] = new YYARec(261,305);yyac++;

					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,3);yygc++; 
					yyg[yygc] = new YYARec(-30,4);yygc++; 
					yyg[yygc] = new YYARec(-24,5);yygc++; 
					yyg[yygc] = new YYARec(-22,6);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,15);yygc++; 
					yyg[yygc] = new YYARec(-2,16);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,33);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,44);yygc++; 
					yyg[yygc] = new YYARec(-31,45);yygc++; 
					yyg[yygc] = new YYARec(-71,48);yygc++; 
					yyg[yygc] = new YYARec(-65,49);yygc++; 
					yyg[yygc] = new YYARec(-33,50);yygc++; 
					yyg[yygc] = new YYARec(-29,51);yygc++; 
					yyg[yygc] = new YYARec(-28,52);yygc++; 
					yyg[yygc] = new YYARec(-25,53);yygc++; 
					yyg[yygc] = new YYARec(-21,54);yygc++; 
					yyg[yygc] = new YYARec(-12,55);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,64);yygc++; 
					yyg[yygc] = new YYARec(-23,65);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,3);yygc++; 
					yyg[yygc] = new YYARec(-30,4);yygc++; 
					yyg[yygc] = new YYARec(-24,5);yygc++; 
					yyg[yygc] = new YYARec(-22,6);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,66);yygc++; 
					yyg[yygc] = new YYARec(-12,67);yygc++; 
					yyg[yygc] = new YYARec(-12,68);yygc++; 
					yyg[yygc] = new YYARec(-12,69);yygc++; 
					yyg[yygc] = new YYARec(-12,70);yygc++; 
					yyg[yygc] = new YYARec(-21,71);yygc++; 
					yyg[yygc] = new YYARec(-27,72);yygc++; 
					yyg[yygc] = new YYARec(-27,74);yygc++; 
					yyg[yygc] = new YYARec(-27,76);yygc++; 
					yyg[yygc] = new YYARec(-27,80);yygc++; 
					yyg[yygc] = new YYARec(-28,89);yygc++; 
					yyg[yygc] = new YYARec(-21,91);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,64);yygc++; 
					yyg[yygc] = new YYARec(-23,92);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,3);yygc++; 
					yyg[yygc] = new YYARec(-30,4);yygc++; 
					yyg[yygc] = new YYARec(-24,5);yygc++; 
					yyg[yygc] = new YYARec(-22,6);yygc++; 
					yyg[yygc] = new YYARec(-13,93);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,94);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,3);yygc++; 
					yyg[yygc] = new YYARec(-30,4);yygc++; 
					yyg[yygc] = new YYARec(-24,5);yygc++; 
					yyg[yygc] = new YYARec(-22,6);yygc++; 
					yyg[yygc] = new YYARec(-13,95);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,94);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-71,48);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-65,49);yygc++; 
					yyg[yygc] = new YYARec(-48,97);yygc++; 
					yyg[yygc] = new YYARec(-47,98);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-33,50);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-29,100);yygc++; 
					yyg[yygc] = new YYARec(-28,101);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-21,104);yygc++; 
					yyg[yygc] = new YYARec(-20,105);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-48,111);yygc++; 
					yyg[yygc] = new YYARec(-45,112);yygc++; 
					yyg[yygc] = new YYARec(-44,113);yygc++; 
					yyg[yygc] = new YYARec(-43,114);yygc++; 
					yyg[yygc] = new YYARec(-42,115);yygc++; 
					yyg[yygc] = new YYARec(-41,116);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,117);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-16,118);yygc++; 
					yyg[yygc] = new YYARec(-14,119);yygc++; 
					yyg[yygc] = new YYARec(-37,131);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-30,134);yygc++; 
					yyg[yygc] = new YYARec(-19,135);yygc++; 
					yyg[yygc] = new YYARec(-17,136);yygc++; 
					yyg[yygc] = new YYARec(-27,145);yygc++; 
					yyg[yygc] = new YYARec(-68,153);yygc++; 
					yyg[yygc] = new YYARec(-27,159);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-48,111);yygc++; 
					yyg[yygc] = new YYARec(-45,112);yygc++; 
					yyg[yygc] = new YYARec(-44,113);yygc++; 
					yyg[yygc] = new YYARec(-43,114);yygc++; 
					yyg[yygc] = new YYARec(-42,115);yygc++; 
					yyg[yygc] = new YYARec(-41,116);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,117);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-16,160);yygc++; 
					yyg[yygc] = new YYARec(-14,119);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-48,111);yygc++; 
					yyg[yygc] = new YYARec(-45,112);yygc++; 
					yyg[yygc] = new YYARec(-44,113);yygc++; 
					yyg[yygc] = new YYARec(-43,114);yygc++; 
					yyg[yygc] = new YYARec(-42,115);yygc++; 
					yyg[yygc] = new YYARec(-41,116);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,117);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-16,164);yygc++; 
					yyg[yygc] = new YYARec(-14,119);yygc++; 
					yyg[yygc] = new YYARec(-12,165);yygc++; 
					yyg[yygc] = new YYARec(-12,166);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-48,111);yygc++; 
					yyg[yygc] = new YYARec(-45,112);yygc++; 
					yyg[yygc] = new YYARec(-44,113);yygc++; 
					yyg[yygc] = new YYARec(-43,114);yygc++; 
					yyg[yygc] = new YYARec(-42,115);yygc++; 
					yyg[yygc] = new YYARec(-41,116);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,117);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-16,167);yygc++; 
					yyg[yygc] = new YYARec(-14,119);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-58,174);yygc++; 
					yyg[yygc] = new YYARec(-56,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-54,177);yygc++; 
					yyg[yygc] = new YYARec(-53,178);yygc++; 
					yyg[yygc] = new YYARec(-52,179);yygc++; 
					yyg[yygc] = new YYARec(-51,180);yygc++; 
					yyg[yygc] = new YYARec(-50,181);yygc++; 
					yyg[yygc] = new YYARec(-49,182);yygc++; 
					yyg[yygc] = new YYARec(-48,183);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-58,174);yygc++; 
					yyg[yygc] = new YYARec(-56,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-54,177);yygc++; 
					yyg[yygc] = new YYARec(-53,178);yygc++; 
					yyg[yygc] = new YYARec(-52,179);yygc++; 
					yyg[yygc] = new YYARec(-51,180);yygc++; 
					yyg[yygc] = new YYARec(-50,181);yygc++; 
					yyg[yygc] = new YYARec(-49,188);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-58,174);yygc++; 
					yyg[yygc] = new YYARec(-56,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-54,177);yygc++; 
					yyg[yygc] = new YYARec(-53,178);yygc++; 
					yyg[yygc] = new YYARec(-52,179);yygc++; 
					yyg[yygc] = new YYARec(-51,180);yygc++; 
					yyg[yygc] = new YYARec(-50,181);yygc++; 
					yyg[yygc] = new YYARec(-49,190);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-27,191);yygc++; 
					yyg[yygc] = new YYARec(-37,131);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-30,134);yygc++; 
					yyg[yygc] = new YYARec(-19,194);yygc++; 
					yyg[yygc] = new YYARec(-17,136);yygc++; 
					yyg[yygc] = new YYARec(-12,195);yygc++; 
					yyg[yygc] = new YYARec(-12,196);yygc++; 
					yyg[yygc] = new YYARec(-65,197);yygc++; 
					yyg[yygc] = new YYARec(-33,198);yygc++; 
					yyg[yygc] = new YYARec(-32,199);yygc++; 
					yyg[yygc] = new YYARec(-69,201);yygc++; 
					yyg[yygc] = new YYARec(-37,202);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-30,134);yygc++; 
					yyg[yygc] = new YYARec(-69,207);yygc++; 
					yyg[yygc] = new YYARec(-37,208);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-30,134);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-58,174);yygc++; 
					yyg[yygc] = new YYARec(-56,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-54,177);yygc++; 
					yyg[yygc] = new YYARec(-53,178);yygc++; 
					yyg[yygc] = new YYARec(-52,179);yygc++; 
					yyg[yygc] = new YYARec(-51,180);yygc++; 
					yyg[yygc] = new YYARec(-50,181);yygc++; 
					yyg[yygc] = new YYARec(-49,209);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-71,48);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-65,49);yygc++; 
					yyg[yygc] = new YYARec(-48,97);yygc++; 
					yyg[yygc] = new YYARec(-47,98);yygc++; 
					yyg[yygc] = new YYARec(-46,210);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-33,50);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-29,100);yygc++; 
					yyg[yygc] = new YYARec(-28,101);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-21,104);yygc++; 
					yyg[yygc] = new YYARec(-20,211);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-48,111);yygc++; 
					yyg[yygc] = new YYARec(-45,112);yygc++; 
					yyg[yygc] = new YYARec(-44,113);yygc++; 
					yyg[yygc] = new YYARec(-43,114);yygc++; 
					yyg[yygc] = new YYARec(-42,115);yygc++; 
					yyg[yygc] = new YYARec(-41,116);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,117);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-16,212);yygc++; 
					yyg[yygc] = new YYARec(-14,119);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-48,111);yygc++; 
					yyg[yygc] = new YYARec(-45,112);yygc++; 
					yyg[yygc] = new YYARec(-44,113);yygc++; 
					yyg[yygc] = new YYARec(-43,114);yygc++; 
					yyg[yygc] = new YYARec(-42,115);yygc++; 
					yyg[yygc] = new YYARec(-41,116);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,117);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-16,213);yygc++; 
					yyg[yygc] = new YYARec(-14,119);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,218);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-63,219);yygc++; 
					yyg[yygc] = new YYARec(-61,223);yygc++; 
					yyg[yygc] = new YYARec(-59,226);yygc++; 
					yyg[yygc] = new YYARec(-57,231);yygc++; 
					yyg[yygc] = new YYARec(-68,239);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-58,174);yygc++; 
					yyg[yygc] = new YYARec(-56,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-54,177);yygc++; 
					yyg[yygc] = new YYARec(-53,178);yygc++; 
					yyg[yygc] = new YYARec(-52,179);yygc++; 
					yyg[yygc] = new YYARec(-51,180);yygc++; 
					yyg[yygc] = new YYARec(-50,181);yygc++; 
					yyg[yygc] = new YYARec(-49,240);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-48,111);yygc++; 
					yyg[yygc] = new YYARec(-45,112);yygc++; 
					yyg[yygc] = new YYARec(-44,113);yygc++; 
					yyg[yygc] = new YYARec(-43,114);yygc++; 
					yyg[yygc] = new YYARec(-42,115);yygc++; 
					yyg[yygc] = new YYARec(-41,116);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,117);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-16,241);yygc++; 
					yyg[yygc] = new YYARec(-14,119);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-71,48);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-65,49);yygc++; 
					yyg[yygc] = new YYARec(-40,244);yygc++; 
					yyg[yygc] = new YYARec(-39,245);yygc++; 
					yyg[yygc] = new YYARec(-38,246);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,247);yygc++; 
					yyg[yygc] = new YYARec(-33,50);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-29,248);yygc++; 
					yyg[yygc] = new YYARec(-28,249);yygc++; 
					yyg[yygc] = new YYARec(-26,250);yygc++; 
					yyg[yygc] = new YYARec(-21,251);yygc++; 
					yyg[yygc] = new YYARec(-37,131);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-30,134);yygc++; 
					yyg[yygc] = new YYARec(-19,253);yygc++; 
					yyg[yygc] = new YYARec(-17,136);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,3);yygc++; 
					yyg[yygc] = new YYARec(-30,4);yygc++; 
					yyg[yygc] = new YYARec(-24,5);yygc++; 
					yyg[yygc] = new YYARec(-22,6);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,258);yygc++; 
					yyg[yygc] = new YYARec(-27,259);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-48,111);yygc++; 
					yyg[yygc] = new YYARec(-45,112);yygc++; 
					yyg[yygc] = new YYARec(-44,113);yygc++; 
					yyg[yygc] = new YYARec(-43,114);yygc++; 
					yyg[yygc] = new YYARec(-42,115);yygc++; 
					yyg[yygc] = new YYARec(-41,116);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,117);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-16,260);yygc++; 
					yyg[yygc] = new YYARec(-15,261);yygc++; 
					yyg[yygc] = new YYARec(-14,119);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-48,111);yygc++; 
					yyg[yygc] = new YYARec(-45,112);yygc++; 
					yyg[yygc] = new YYARec(-44,113);yygc++; 
					yyg[yygc] = new YYARec(-43,114);yygc++; 
					yyg[yygc] = new YYARec(-42,115);yygc++; 
					yyg[yygc] = new YYARec(-41,116);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,117);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-16,260);yygc++; 
					yyg[yygc] = new YYARec(-15,262);yygc++; 
					yyg[yygc] = new YYARec(-14,119);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-48,111);yygc++; 
					yyg[yygc] = new YYARec(-45,112);yygc++; 
					yyg[yygc] = new YYARec(-44,113);yygc++; 
					yyg[yygc] = new YYARec(-43,114);yygc++; 
					yyg[yygc] = new YYARec(-42,115);yygc++; 
					yyg[yygc] = new YYARec(-41,116);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,117);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-16,263);yygc++; 
					yyg[yygc] = new YYARec(-14,119);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-58,174);yygc++; 
					yyg[yygc] = new YYARec(-56,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-54,177);yygc++; 
					yyg[yygc] = new YYARec(-53,178);yygc++; 
					yyg[yygc] = new YYARec(-52,179);yygc++; 
					yyg[yygc] = new YYARec(-51,180);yygc++; 
					yyg[yygc] = new YYARec(-50,181);yygc++; 
					yyg[yygc] = new YYARec(-49,264);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,265);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,172);yygc++; 
					yyg[yygc] = new YYARec(-60,266);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-58,267);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-58,174);yygc++; 
					yyg[yygc] = new YYARec(-56,268);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-58,174);yygc++; 
					yyg[yygc] = new YYARec(-56,175);yygc++; 
					yyg[yygc] = new YYARec(-55,269);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-58,174);yygc++; 
					yyg[yygc] = new YYARec(-56,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-54,270);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-58,174);yygc++; 
					yyg[yygc] = new YYARec(-56,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-54,177);yygc++; 
					yyg[yygc] = new YYARec(-53,271);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-58,174);yygc++; 
					yyg[yygc] = new YYARec(-56,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-54,177);yygc++; 
					yyg[yygc] = new YYARec(-53,178);yygc++; 
					yyg[yygc] = new YYARec(-52,272);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-58,174);yygc++; 
					yyg[yygc] = new YYARec(-56,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-54,177);yygc++; 
					yyg[yygc] = new YYARec(-53,178);yygc++; 
					yyg[yygc] = new YYARec(-52,179);yygc++; 
					yyg[yygc] = new YYARec(-51,273);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-67,168);yygc++; 
					yyg[yygc] = new YYARec(-66,169);yygc++; 
					yyg[yygc] = new YYARec(-65,170);yygc++; 
					yyg[yygc] = new YYARec(-64,171);yygc++; 
					yyg[yygc] = new YYARec(-62,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-58,174);yygc++; 
					yyg[yygc] = new YYARec(-56,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-54,177);yygc++; 
					yyg[yygc] = new YYARec(-53,178);yygc++; 
					yyg[yygc] = new YYARec(-52,179);yygc++; 
					yyg[yygc] = new YYARec(-51,180);yygc++; 
					yyg[yygc] = new YYARec(-50,181);yygc++; 
					yyg[yygc] = new YYARec(-49,274);yygc++; 
					yyg[yygc] = new YYARec(-48,189);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-48,111);yygc++; 
					yyg[yygc] = new YYARec(-45,112);yygc++; 
					yyg[yygc] = new YYARec(-44,113);yygc++; 
					yyg[yygc] = new YYARec(-43,114);yygc++; 
					yyg[yygc] = new YYARec(-42,115);yygc++; 
					yyg[yygc] = new YYARec(-41,116);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,117);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-16,277);yygc++; 
					yyg[yygc] = new YYARec(-14,119);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-48,111);yygc++; 
					yyg[yygc] = new YYARec(-45,112);yygc++; 
					yyg[yygc] = new YYARec(-44,113);yygc++; 
					yyg[yygc] = new YYARec(-43,114);yygc++; 
					yyg[yygc] = new YYARec(-42,115);yygc++; 
					yyg[yygc] = new YYARec(-41,116);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,117);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-16,278);yygc++; 
					yyg[yygc] = new YYARec(-14,119);yygc++; 
					yyg[yygc] = new YYARec(-27,279);yygc++; 
					yyg[yygc] = new YYARec(-37,131);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-30,134);yygc++; 
					yyg[yygc] = new YYARec(-19,281);yygc++; 
					yyg[yygc] = new YYARec(-18,282);yygc++; 
					yyg[yygc] = new YYARec(-17,136);yygc++; 
					yyg[yygc] = new YYARec(-37,131);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-30,134);yygc++; 
					yyg[yygc] = new YYARec(-19,281);yygc++; 
					yyg[yygc] = new YYARec(-18,283);yygc++; 
					yyg[yygc] = new YYARec(-17,136);yygc++; 
					yyg[yygc] = new YYARec(-65,197);yygc++; 
					yyg[yygc] = new YYARec(-33,198);yygc++; 
					yyg[yygc] = new YYARec(-32,284);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-71,48);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-65,49);yygc++; 
					yyg[yygc] = new YYARec(-48,97);yygc++; 
					yyg[yygc] = new YYARec(-47,98);yygc++; 
					yyg[yygc] = new YYARec(-46,286);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-33,50);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-29,100);yygc++; 
					yyg[yygc] = new YYARec(-28,101);yygc++; 
					yyg[yygc] = new YYARec(-26,102);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-21,104);yygc++; 
					yyg[yygc] = new YYARec(-20,211);yygc++; 
					yyg[yygc] = new YYARec(-63,219);yygc++; 
					yyg[yygc] = new YYARec(-61,223);yygc++; 
					yyg[yygc] = new YYARec(-59,226);yygc++; 
					yyg[yygc] = new YYARec(-57,231);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-71,48);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-65,49);yygc++; 
					yyg[yygc] = new YYARec(-40,244);yygc++; 
					yyg[yygc] = new YYARec(-39,245);yygc++; 
					yyg[yygc] = new YYARec(-38,294);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,247);yygc++; 
					yyg[yygc] = new YYARec(-33,50);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-29,248);yygc++; 
					yyg[yygc] = new YYARec(-28,249);yygc++; 
					yyg[yygc] = new YYARec(-26,250);yygc++; 
					yyg[yygc] = new YYARec(-21,251);yygc++; 
					yyg[yygc] = new YYARec(-37,295);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-30,134);yygc++; 
					yyg[yygc] = new YYARec(-72,30);yygc++; 
					yyg[yygc] = new YYARec(-70,96);yygc++; 
					yyg[yygc] = new YYARec(-66,31);yygc++; 
					yyg[yygc] = new YYARec(-48,111);yygc++; 
					yyg[yygc] = new YYARec(-45,112);yygc++; 
					yyg[yygc] = new YYARec(-44,113);yygc++; 
					yyg[yygc] = new YYARec(-43,114);yygc++; 
					yyg[yygc] = new YYARec(-42,115);yygc++; 
					yyg[yygc] = new YYARec(-41,116);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,99);yygc++; 
					yyg[yygc] = new YYARec(-30,32);yygc++; 
					yyg[yygc] = new YYARec(-26,117);yygc++; 
					yyg[yygc] = new YYARec(-22,103);yygc++; 
					yyg[yygc] = new YYARec(-16,302);yygc++; 
					yyg[yygc] = new YYARec(-14,119);yygc++; 
					yyg[yygc] = new YYARec(-37,131);yygc++; 
					yyg[yygc] = new YYARec(-36,132);yygc++; 
					yyg[yygc] = new YYARec(-35,133);yygc++; 
					yyg[yygc] = new YYARec(-30,134);yygc++; 
					yyg[yygc] = new YYARec(-19,303);yygc++; 
					yyg[yygc] = new YYARec(-17,136);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = 0;  
					yyd[2] = -49;  
					yyd[3] = 0;  
					yyd[4] = 0;  
					yyd[5] = 0;  
					yyd[6] = 0;  
					yyd[7] = -10;  
					yyd[8] = -9;  
					yyd[9] = -8;  
					yyd[10] = -7;  
					yyd[11] = -6;  
					yyd[12] = -5;  
					yyd[13] = -4;  
					yyd[14] = 0;  
					yyd[15] = -1;  
					yyd[16] = 0;  
					yyd[17] = 0;  
					yyd[18] = 0;  
					yyd[19] = 0;  
					yyd[20] = 0;  
					yyd[21] = 0;  
					yyd[22] = -32;  
					yyd[23] = -30;  
					yyd[24] = -43;  
					yyd[25] = -47;  
					yyd[26] = -65;  
					yyd[27] = -29;  
					yyd[28] = -31;  
					yyd[29] = -48;  
					yyd[30] = -186;  
					yyd[31] = -184;  
					yyd[32] = -185;  
					yyd[33] = 0;  
					yyd[34] = -133;  
					yyd[35] = -181;  
					yyd[36] = -183;  
					yyd[37] = -180;  
					yyd[38] = -175;  
					yyd[39] = -131;  
					yyd[40] = -132;  
					yyd[41] = -174;  
					yyd[42] = -179;  
					yyd[43] = -182;  
					yyd[44] = 0;  
					yyd[45] = 0;  
					yyd[46] = -173;  
					yyd[47] = -172;  
					yyd[48] = -162;  
					yyd[49] = 0;  
					yyd[50] = -163;  
					yyd[51] = -37;  
					yyd[52] = -35;  
					yyd[53] = 0;  
					yyd[54] = -38;  
					yyd[55] = -36;  
					yyd[56] = -121;  
					yyd[57] = -122;  
					yyd[58] = -120;  
					yyd[59] = -167;  
					yyd[60] = -169;  
					yyd[61] = -170;  
					yyd[62] = -188;  
					yyd[63] = -189;  
					yyd[64] = 0;  
					yyd[65] = 0;  
					yyd[66] = -2;  
					yyd[67] = 0;  
					yyd[68] = 0;  
					yyd[69] = 0;  
					yyd[70] = 0;  
					yyd[71] = 0;  
					yyd[72] = 0;  
					yyd[73] = -82;  
					yyd[74] = 0;  
					yyd[75] = -46;  
					yyd[76] = 0;  
					yyd[77] = -166;  
					yyd[78] = -168;  
					yyd[79] = -28;  
					yyd[80] = 0;  
					yyd[81] = -27;  
					yyd[82] = 0;  
					yyd[83] = 0;  
					yyd[84] = -24;  
					yyd[85] = 0;  
					yyd[86] = -25;  
					yyd[87] = -26;  
					yyd[88] = 0;  
					yyd[89] = 0;  
					yyd[90] = 0;  
					yyd[91] = 0;  
					yyd[92] = -34;  
					yyd[93] = 0;  
					yyd[94] = 0;  
					yyd[95] = 0;  
					yyd[96] = 0;  
					yyd[97] = -81;  
					yyd[98] = -78;  
					yyd[99] = -143;  
					yyd[100] = -80;  
					yyd[101] = -77;  
					yyd[102] = 0;  
					yyd[103] = -142;  
					yyd[104] = -79;  
					yyd[105] = 0;  
					yyd[106] = -141;  
					yyd[107] = -171;  
					yyd[108] = -178;  
					yyd[109] = -176;  
					yyd[110] = -177;  
					yyd[111] = 0;  
					yyd[112] = 0;  
					yyd[113] = -72;  
					yyd[114] = 0;  
					yyd[115] = 0;  
					yyd[116] = 0;  
					yyd[117] = 0;  
					yyd[118] = 0;  
					yyd[119] = 0;  
					yyd[120] = 0;  
					yyd[121] = 0;  
					yyd[122] = 0;  
					yyd[123] = 0;  
					yyd[124] = 0;  
					yyd[125] = 0;  
					yyd[126] = 0;  
					yyd[127] = 0;  
					yyd[128] = 0;  
					yyd[129] = 0;  
					yyd[130] = -45;  
					yyd[131] = 0;  
					yyd[132] = 0;  
					yyd[133] = -157;  
					yyd[134] = -156;  
					yyd[135] = 0;  
					yyd[136] = 0;  
					yyd[137] = 0;  
					yyd[138] = 0;  
					yyd[139] = -155;  
					yyd[140] = -151;  
					yyd[141] = -150;  
					yyd[142] = -152;  
					yyd[143] = -153;  
					yyd[144] = -154;  
					yyd[145] = 0;  
					yyd[146] = -11;  
					yyd[147] = 0;  
					yyd[148] = -14;  
					yyd[149] = -12;  
					yyd[150] = 0;  
					yyd[151] = 0;  
					yyd[152] = -23;  
					yyd[153] = 0;  
					yyd[154] = -126;  
					yyd[155] = -127;  
					yyd[156] = -128;  
					yyd[157] = -129;  
					yyd[158] = -130;  
					yyd[159] = 0;  
					yyd[160] = -70;  
					yyd[161] = 0;  
					yyd[162] = 0;  
					yyd[163] = -64;  
					yyd[164] = -69;  
					yyd[165] = 0;  
					yyd[166] = 0;  
					yyd[167] = 0;  
					yyd[168] = -107;  
					yyd[169] = 0;  
					yyd[170] = 0;  
					yyd[171] = -103;  
					yyd[172] = -101;  
					yyd[173] = 0;  
					yyd[174] = 0;  
					yyd[175] = 0;  
					yyd[176] = 0;  
					yyd[177] = 0;  
					yyd[178] = 0;  
					yyd[179] = 0;  
					yyd[180] = 0;  
					yyd[181] = 0;  
					yyd[182] = -123;  
					yyd[183] = 0;  
					yyd[184] = 0;  
					yyd[185] = -165;  
					yyd[186] = -164;  
					yyd[187] = 0;  
					yyd[188] = 0;  
					yyd[189] = -108;  
					yyd[190] = 0;  
					yyd[191] = 0;  
					yyd[192] = 0;  
					yyd[193] = -44;  
					yyd[194] = -51;  
					yyd[195] = 0;  
					yyd[196] = 0;  
					yyd[197] = 0;  
					yyd[198] = 0;  
					yyd[199] = 0;  
					yyd[200] = 0;  
					yyd[201] = -138;  
					yyd[202] = -139;  
					yyd[203] = -149;  
					yyd[204] = -147;  
					yyd[205] = -148;  
					yyd[206] = -146;  
					yyd[207] = -137;  
					yyd[208] = -140;  
					yyd[209] = -125;  
					yyd[210] = -73;  
					yyd[211] = 0;  
					yyd[212] = -68;  
					yyd[213] = -67;  
					yyd[214] = 0;  
					yyd[215] = 0;  
					yyd[216] = 0;  
					yyd[217] = 0;  
					yyd[218] = -104;  
					yyd[219] = 0;  
					yyd[220] = -117;  
					yyd[221] = -118;  
					yyd[222] = -119;  
					yyd[223] = 0;  
					yyd[224] = -115;  
					yyd[225] = -116;  
					yyd[226] = 0;  
					yyd[227] = -111;  
					yyd[228] = -112;  
					yyd[229] = -113;  
					yyd[230] = -114;  
					yyd[231] = 0;  
					yyd[232] = -109;  
					yyd[233] = -110;  
					yyd[234] = 0;  
					yyd[235] = 0;  
					yyd[236] = 0;  
					yyd[237] = 0;  
					yyd[238] = 0;  
					yyd[239] = 0;  
					yyd[240] = 0;  
					yyd[241] = 0;  
					yyd[242] = 0;  
					yyd[243] = 0;  
					yyd[244] = -60;  
					yyd[245] = 0;  
					yyd[246] = -53;  
					yyd[247] = -63;  
					yyd[248] = -61;  
					yyd[249] = -59;  
					yyd[250] = 0;  
					yyd[251] = -58;  
					yyd[252] = -57;  
					yyd[253] = -50;  
					yyd[254] = 0;  
					yyd[255] = 0;  
					yyd[256] = 0;  
					yyd[257] = -39;  
					yyd[258] = 0;  
					yyd[259] = 0;  
					yyd[260] = 0;  
					yyd[261] = 0;  
					yyd[262] = 0;  
					yyd[263] = -66;  
					yyd[264] = 0;  
					yyd[265] = -102;  
					yyd[266] = 0;  
					yyd[267] = 0;  
					yyd[268] = 0;  
					yyd[269] = 0;  
					yyd[270] = 0;  
					yyd[271] = 0;  
					yyd[272] = 0;  
					yyd[273] = 0;  
					yyd[274] = -124;  
					yyd[275] = -106;  
					yyd[276] = -134;  
					yyd[277] = 0;  
					yyd[278] = 0;  
					yyd[279] = 0;  
					yyd[280] = 0;  
					yyd[281] = 0;  
					yyd[282] = 0;  
					yyd[283] = 0;  
					yyd[284] = -40;  
					yyd[285] = -13;  
					yyd[286] = -76;  
					yyd[287] = 0;  
					yyd[288] = -18;  
					yyd[289] = -15;  
					yyd[290] = -16;  
					yyd[291] = -105;  
					yyd[292] = -135;  
					yyd[293] = -136;  
					yyd[294] = -55;  
					yyd[295] = -56;  
					yyd[296] = 0;  
					yyd[297] = -22;  
					yyd[298] = -19;  
					yyd[299] = -20;  
					yyd[300] = 0;  
					yyd[301] = 0;  
					yyd[302] = 0;  
					yyd[303] = 0;  
					yyd[304] = -17;  
					yyd[305] = -21; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 15;  
					yyal[2] = 26;  
					yyal[3] = 26;  
					yyal[4] = 37;  
					yyal[5] = 39;  
					yyal[6] = 47;  
					yyal[7] = 58;  
					yyal[8] = 58;  
					yyal[9] = 58;  
					yyal[10] = 58;  
					yyal[11] = 58;  
					yyal[12] = 58;  
					yyal[13] = 58;  
					yyal[14] = 58;  
					yyal[15] = 74;  
					yyal[16] = 74;  
					yyal[17] = 75;  
					yyal[18] = 76;  
					yyal[19] = 77;  
					yyal[20] = 78;  
					yyal[21] = 79;  
					yyal[22] = 80;  
					yyal[23] = 80;  
					yyal[24] = 80;  
					yyal[25] = 80;  
					yyal[26] = 80;  
					yyal[27] = 80;  
					yyal[28] = 80;  
					yyal[29] = 80;  
					yyal[30] = 80;  
					yyal[31] = 80;  
					yyal[32] = 80;  
					yyal[33] = 80;  
					yyal[34] = 82;  
					yyal[35] = 82;  
					yyal[36] = 82;  
					yyal[37] = 82;  
					yyal[38] = 82;  
					yyal[39] = 82;  
					yyal[40] = 82;  
					yyal[41] = 82;  
					yyal[42] = 82;  
					yyal[43] = 82;  
					yyal[44] = 82;  
					yyal[45] = 86;  
					yyal[46] = 88;  
					yyal[47] = 88;  
					yyal[48] = 88;  
					yyal[49] = 88;  
					yyal[50] = 90;  
					yyal[51] = 90;  
					yyal[52] = 90;  
					yyal[53] = 90;  
					yyal[54] = 91;  
					yyal[55] = 91;  
					yyal[56] = 91;  
					yyal[57] = 91;  
					yyal[58] = 91;  
					yyal[59] = 91;  
					yyal[60] = 91;  
					yyal[61] = 91;  
					yyal[62] = 91;  
					yyal[63] = 91;  
					yyal[64] = 91;  
					yyal[65] = 104;  
					yyal[66] = 105;  
					yyal[67] = 105;  
					yyal[68] = 106;  
					yyal[69] = 107;  
					yyal[70] = 109;  
					yyal[71] = 110;  
					yyal[72] = 111;  
					yyal[73] = 112;  
					yyal[74] = 112;  
					yyal[75] = 114;  
					yyal[76] = 114;  
					yyal[77] = 115;  
					yyal[78] = 115;  
					yyal[79] = 115;  
					yyal[80] = 115;  
					yyal[81] = 127;  
					yyal[82] = 127;  
					yyal[83] = 142;  
					yyal[84] = 157;  
					yyal[85] = 157;  
					yyal[86] = 184;  
					yyal[87] = 184;  
					yyal[88] = 184;  
					yyal[89] = 211;  
					yyal[90] = 212;  
					yyal[91] = 223;  
					yyal[92] = 229;  
					yyal[93] = 229;  
					yyal[94] = 230;  
					yyal[95] = 232;  
					yyal[96] = 233;  
					yyal[97] = 284;  
					yyal[98] = 284;  
					yyal[99] = 284;  
					yyal[100] = 284;  
					yyal[101] = 284;  
					yyal[102] = 284;  
					yyal[103] = 335;  
					yyal[104] = 335;  
					yyal[105] = 335;  
					yyal[106] = 336;  
					yyal[107] = 336;  
					yyal[108] = 336;  
					yyal[109] = 336;  
					yyal[110] = 336;  
					yyal[111] = 336;  
					yyal[112] = 341;  
					yyal[113] = 370;  
					yyal[114] = 370;  
					yyal[115] = 399;  
					yyal[116] = 400;  
					yyal[117] = 401;  
					yyal[118] = 408;  
					yyal[119] = 409;  
					yyal[120] = 438;  
					yyal[121] = 439;  
					yyal[122] = 440;  
					yyal[123] = 467;  
					yyal[124] = 492;  
					yyal[125] = 522;  
					yyal[126] = 547;  
					yyal[127] = 572;  
					yyal[128] = 608;  
					yyal[129] = 644;  
					yyal[130] = 680;  
					yyal[131] = 680;  
					yyal[132] = 703;  
					yyal[133] = 704;  
					yyal[134] = 704;  
					yyal[135] = 704;  
					yyal[136] = 705;  
					yyal[137] = 718;  
					yyal[138] = 719;  
					yyal[139] = 720;  
					yyal[140] = 720;  
					yyal[141] = 720;  
					yyal[142] = 720;  
					yyal[143] = 720;  
					yyal[144] = 720;  
					yyal[145] = 720;  
					yyal[146] = 725;  
					yyal[147] = 725;  
					yyal[148] = 726;  
					yyal[149] = 726;  
					yyal[150] = 726;  
					yyal[151] = 738;  
					yyal[152] = 750;  
					yyal[153] = 750;  
					yyal[154] = 775;  
					yyal[155] = 775;  
					yyal[156] = 775;  
					yyal[157] = 775;  
					yyal[158] = 775;  
					yyal[159] = 775;  
					yyal[160] = 802;  
					yyal[161] = 802;  
					yyal[162] = 831;  
					yyal[163] = 860;  
					yyal[164] = 860;  
					yyal[165] = 860;  
					yyal[166] = 861;  
					yyal[167] = 862;  
					yyal[168] = 863;  
					yyal[169] = 863;  
					yyal[170] = 889;  
					yyal[171] = 914;  
					yyal[172] = 914;  
					yyal[173] = 914;  
					yyal[174] = 933;  
					yyal[175] = 949;  
					yyal[176] = 963;  
					yyal[177] = 973;  
					yyal[178] = 981;  
					yyal[179] = 988;  
					yyal[180] = 994;  
					yyal[181] = 999;  
					yyal[182] = 1003;  
					yyal[183] = 1003;  
					yyal[184] = 1025;  
					yyal[185] = 1050;  
					yyal[186] = 1050;  
					yyal[187] = 1050;  
					yyal[188] = 1077;  
					yyal[189] = 1078;  
					yyal[190] = 1078;  
					yyal[191] = 1079;  
					yyal[192] = 1101;  
					yyal[193] = 1114;  
					yyal[194] = 1114;  
					yyal[195] = 1114;  
					yyal[196] = 1115;  
					yyal[197] = 1116;  
					yyal[198] = 1117;  
					yyal[199] = 1119;  
					yyal[200] = 1120;  
					yyal[201] = 1134;  
					yyal[202] = 1134;  
					yyal[203] = 1134;  
					yyal[204] = 1134;  
					yyal[205] = 1134;  
					yyal[206] = 1134;  
					yyal[207] = 1134;  
					yyal[208] = 1134;  
					yyal[209] = 1134;  
					yyal[210] = 1134;  
					yyal[211] = 1134;  
					yyal[212] = 1163;  
					yyal[213] = 1163;  
					yyal[214] = 1163;  
					yyal[215] = 1191;  
					yyal[216] = 1219;  
					yyal[217] = 1248;  
					yyal[218] = 1273;  
					yyal[219] = 1273;  
					yyal[220] = 1298;  
					yyal[221] = 1298;  
					yyal[222] = 1298;  
					yyal[223] = 1298;  
					yyal[224] = 1323;  
					yyal[225] = 1323;  
					yyal[226] = 1323;  
					yyal[227] = 1348;  
					yyal[228] = 1348;  
					yyal[229] = 1348;  
					yyal[230] = 1348;  
					yyal[231] = 1348;  
					yyal[232] = 1373;  
					yyal[233] = 1373;  
					yyal[234] = 1373;  
					yyal[235] = 1398;  
					yyal[236] = 1423;  
					yyal[237] = 1448;  
					yyal[238] = 1473;  
					yyal[239] = 1498;  
					yyal[240] = 1523;  
					yyal[241] = 1524;  
					yyal[242] = 1525;  
					yyal[243] = 1552;  
					yyal[244] = 1579;  
					yyal[245] = 1579;  
					yyal[246] = 1603;  
					yyal[247] = 1603;  
					yyal[248] = 1603;  
					yyal[249] = 1603;  
					yyal[250] = 1603;  
					yyal[251] = 1628;  
					yyal[252] = 1628;  
					yyal[253] = 1628;  
					yyal[254] = 1628;  
					yyal[255] = 1640;  
					yyal[256] = 1652;  
					yyal[257] = 1657;  
					yyal[258] = 1657;  
					yyal[259] = 1658;  
					yyal[260] = 1686;  
					yyal[261] = 1688;  
					yyal[262] = 1689;  
					yyal[263] = 1690;  
					yyal[264] = 1690;  
					yyal[265] = 1691;  
					yyal[266] = 1691;  
					yyal[267] = 1710;  
					yyal[268] = 1726;  
					yyal[269] = 1740;  
					yyal[270] = 1750;  
					yyal[271] = 1758;  
					yyal[272] = 1765;  
					yyal[273] = 1771;  
					yyal[274] = 1776;  
					yyal[275] = 1776;  
					yyal[276] = 1776;  
					yyal[277] = 1776;  
					yyal[278] = 1777;  
					yyal[279] = 1778;  
					yyal[280] = 1801;  
					yyal[281] = 1809;  
					yyal[282] = 1811;  
					yyal[283] = 1812;  
					yyal[284] = 1813;  
					yyal[285] = 1813;  
					yyal[286] = 1813;  
					yyal[287] = 1813;  
					yyal[288] = 1814;  
					yyal[289] = 1814;  
					yyal[290] = 1814;  
					yyal[291] = 1814;  
					yyal[292] = 1814;  
					yyal[293] = 1814;  
					yyal[294] = 1814;  
					yyal[295] = 1814;  
					yyal[296] = 1814;  
					yyal[297] = 1815;  
					yyal[298] = 1815;  
					yyal[299] = 1815;  
					yyal[300] = 1815;  
					yyal[301] = 1842;  
					yyal[302] = 1853;  
					yyal[303] = 1854;  
					yyal[304] = 1855;  
					yyal[305] = 1855; 

					yyah = new int[yynstates];
					yyah[0] = 14;  
					yyah[1] = 25;  
					yyah[2] = 25;  
					yyah[3] = 36;  
					yyah[4] = 38;  
					yyah[5] = 46;  
					yyah[6] = 57;  
					yyah[7] = 57;  
					yyah[8] = 57;  
					yyah[9] = 57;  
					yyah[10] = 57;  
					yyah[11] = 57;  
					yyah[12] = 57;  
					yyah[13] = 57;  
					yyah[14] = 73;  
					yyah[15] = 73;  
					yyah[16] = 74;  
					yyah[17] = 75;  
					yyah[18] = 76;  
					yyah[19] = 77;  
					yyah[20] = 78;  
					yyah[21] = 79;  
					yyah[22] = 79;  
					yyah[23] = 79;  
					yyah[24] = 79;  
					yyah[25] = 79;  
					yyah[26] = 79;  
					yyah[27] = 79;  
					yyah[28] = 79;  
					yyah[29] = 79;  
					yyah[30] = 79;  
					yyah[31] = 79;  
					yyah[32] = 79;  
					yyah[33] = 81;  
					yyah[34] = 81;  
					yyah[35] = 81;  
					yyah[36] = 81;  
					yyah[37] = 81;  
					yyah[38] = 81;  
					yyah[39] = 81;  
					yyah[40] = 81;  
					yyah[41] = 81;  
					yyah[42] = 81;  
					yyah[43] = 81;  
					yyah[44] = 85;  
					yyah[45] = 87;  
					yyah[46] = 87;  
					yyah[47] = 87;  
					yyah[48] = 87;  
					yyah[49] = 89;  
					yyah[50] = 89;  
					yyah[51] = 89;  
					yyah[52] = 89;  
					yyah[53] = 90;  
					yyah[54] = 90;  
					yyah[55] = 90;  
					yyah[56] = 90;  
					yyah[57] = 90;  
					yyah[58] = 90;  
					yyah[59] = 90;  
					yyah[60] = 90;  
					yyah[61] = 90;  
					yyah[62] = 90;  
					yyah[63] = 90;  
					yyah[64] = 103;  
					yyah[65] = 104;  
					yyah[66] = 104;  
					yyah[67] = 105;  
					yyah[68] = 106;  
					yyah[69] = 108;  
					yyah[70] = 109;  
					yyah[71] = 110;  
					yyah[72] = 111;  
					yyah[73] = 111;  
					yyah[74] = 113;  
					yyah[75] = 113;  
					yyah[76] = 114;  
					yyah[77] = 114;  
					yyah[78] = 114;  
					yyah[79] = 114;  
					yyah[80] = 126;  
					yyah[81] = 126;  
					yyah[82] = 141;  
					yyah[83] = 156;  
					yyah[84] = 156;  
					yyah[85] = 183;  
					yyah[86] = 183;  
					yyah[87] = 183;  
					yyah[88] = 210;  
					yyah[89] = 211;  
					yyah[90] = 222;  
					yyah[91] = 228;  
					yyah[92] = 228;  
					yyah[93] = 229;  
					yyah[94] = 231;  
					yyah[95] = 232;  
					yyah[96] = 283;  
					yyah[97] = 283;  
					yyah[98] = 283;  
					yyah[99] = 283;  
					yyah[100] = 283;  
					yyah[101] = 283;  
					yyah[102] = 334;  
					yyah[103] = 334;  
					yyah[104] = 334;  
					yyah[105] = 335;  
					yyah[106] = 335;  
					yyah[107] = 335;  
					yyah[108] = 335;  
					yyah[109] = 335;  
					yyah[110] = 335;  
					yyah[111] = 340;  
					yyah[112] = 369;  
					yyah[113] = 369;  
					yyah[114] = 398;  
					yyah[115] = 399;  
					yyah[116] = 400;  
					yyah[117] = 407;  
					yyah[118] = 408;  
					yyah[119] = 437;  
					yyah[120] = 438;  
					yyah[121] = 439;  
					yyah[122] = 466;  
					yyah[123] = 491;  
					yyah[124] = 521;  
					yyah[125] = 546;  
					yyah[126] = 571;  
					yyah[127] = 607;  
					yyah[128] = 643;  
					yyah[129] = 679;  
					yyah[130] = 679;  
					yyah[131] = 702;  
					yyah[132] = 703;  
					yyah[133] = 703;  
					yyah[134] = 703;  
					yyah[135] = 704;  
					yyah[136] = 717;  
					yyah[137] = 718;  
					yyah[138] = 719;  
					yyah[139] = 719;  
					yyah[140] = 719;  
					yyah[141] = 719;  
					yyah[142] = 719;  
					yyah[143] = 719;  
					yyah[144] = 719;  
					yyah[145] = 724;  
					yyah[146] = 724;  
					yyah[147] = 725;  
					yyah[148] = 725;  
					yyah[149] = 725;  
					yyah[150] = 737;  
					yyah[151] = 749;  
					yyah[152] = 749;  
					yyah[153] = 774;  
					yyah[154] = 774;  
					yyah[155] = 774;  
					yyah[156] = 774;  
					yyah[157] = 774;  
					yyah[158] = 774;  
					yyah[159] = 801;  
					yyah[160] = 801;  
					yyah[161] = 830;  
					yyah[162] = 859;  
					yyah[163] = 859;  
					yyah[164] = 859;  
					yyah[165] = 860;  
					yyah[166] = 861;  
					yyah[167] = 862;  
					yyah[168] = 862;  
					yyah[169] = 888;  
					yyah[170] = 913;  
					yyah[171] = 913;  
					yyah[172] = 913;  
					yyah[173] = 932;  
					yyah[174] = 948;  
					yyah[175] = 962;  
					yyah[176] = 972;  
					yyah[177] = 980;  
					yyah[178] = 987;  
					yyah[179] = 993;  
					yyah[180] = 998;  
					yyah[181] = 1002;  
					yyah[182] = 1002;  
					yyah[183] = 1024;  
					yyah[184] = 1049;  
					yyah[185] = 1049;  
					yyah[186] = 1049;  
					yyah[187] = 1076;  
					yyah[188] = 1077;  
					yyah[189] = 1077;  
					yyah[190] = 1078;  
					yyah[191] = 1100;  
					yyah[192] = 1113;  
					yyah[193] = 1113;  
					yyah[194] = 1113;  
					yyah[195] = 1114;  
					yyah[196] = 1115;  
					yyah[197] = 1116;  
					yyah[198] = 1118;  
					yyah[199] = 1119;  
					yyah[200] = 1133;  
					yyah[201] = 1133;  
					yyah[202] = 1133;  
					yyah[203] = 1133;  
					yyah[204] = 1133;  
					yyah[205] = 1133;  
					yyah[206] = 1133;  
					yyah[207] = 1133;  
					yyah[208] = 1133;  
					yyah[209] = 1133;  
					yyah[210] = 1133;  
					yyah[211] = 1162;  
					yyah[212] = 1162;  
					yyah[213] = 1162;  
					yyah[214] = 1190;  
					yyah[215] = 1218;  
					yyah[216] = 1247;  
					yyah[217] = 1272;  
					yyah[218] = 1272;  
					yyah[219] = 1297;  
					yyah[220] = 1297;  
					yyah[221] = 1297;  
					yyah[222] = 1297;  
					yyah[223] = 1322;  
					yyah[224] = 1322;  
					yyah[225] = 1322;  
					yyah[226] = 1347;  
					yyah[227] = 1347;  
					yyah[228] = 1347;  
					yyah[229] = 1347;  
					yyah[230] = 1347;  
					yyah[231] = 1372;  
					yyah[232] = 1372;  
					yyah[233] = 1372;  
					yyah[234] = 1397;  
					yyah[235] = 1422;  
					yyah[236] = 1447;  
					yyah[237] = 1472;  
					yyah[238] = 1497;  
					yyah[239] = 1522;  
					yyah[240] = 1523;  
					yyah[241] = 1524;  
					yyah[242] = 1551;  
					yyah[243] = 1578;  
					yyah[244] = 1578;  
					yyah[245] = 1602;  
					yyah[246] = 1602;  
					yyah[247] = 1602;  
					yyah[248] = 1602;  
					yyah[249] = 1602;  
					yyah[250] = 1627;  
					yyah[251] = 1627;  
					yyah[252] = 1627;  
					yyah[253] = 1627;  
					yyah[254] = 1639;  
					yyah[255] = 1651;  
					yyah[256] = 1656;  
					yyah[257] = 1656;  
					yyah[258] = 1657;  
					yyah[259] = 1685;  
					yyah[260] = 1687;  
					yyah[261] = 1688;  
					yyah[262] = 1689;  
					yyah[263] = 1689;  
					yyah[264] = 1690;  
					yyah[265] = 1690;  
					yyah[266] = 1709;  
					yyah[267] = 1725;  
					yyah[268] = 1739;  
					yyah[269] = 1749;  
					yyah[270] = 1757;  
					yyah[271] = 1764;  
					yyah[272] = 1770;  
					yyah[273] = 1775;  
					yyah[274] = 1775;  
					yyah[275] = 1775;  
					yyah[276] = 1775;  
					yyah[277] = 1776;  
					yyah[278] = 1777;  
					yyah[279] = 1800;  
					yyah[280] = 1808;  
					yyah[281] = 1810;  
					yyah[282] = 1811;  
					yyah[283] = 1812;  
					yyah[284] = 1812;  
					yyah[285] = 1812;  
					yyah[286] = 1812;  
					yyah[287] = 1813;  
					yyah[288] = 1813;  
					yyah[289] = 1813;  
					yyah[290] = 1813;  
					yyah[291] = 1813;  
					yyah[292] = 1813;  
					yyah[293] = 1813;  
					yyah[294] = 1813;  
					yyah[295] = 1813;  
					yyah[296] = 1814;  
					yyah[297] = 1814;  
					yyah[298] = 1814;  
					yyah[299] = 1814;  
					yyah[300] = 1841;  
					yyah[301] = 1852;  
					yyah[302] = 1853;  
					yyah[303] = 1854;  
					yyah[304] = 1854;  
					yyah[305] = 1854; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 17;  
					yygl[2] = 21;  
					yygl[3] = 21;  
					yygl[4] = 25;  
					yygl[5] = 26;  
					yygl[6] = 34;  
					yygl[7] = 39;  
					yygl[8] = 39;  
					yygl[9] = 39;  
					yygl[10] = 39;  
					yygl[11] = 39;  
					yygl[12] = 39;  
					yygl[13] = 39;  
					yygl[14] = 39;  
					yygl[15] = 54;  
					yygl[16] = 54;  
					yygl[17] = 54;  
					yygl[18] = 55;  
					yygl[19] = 56;  
					yygl[20] = 57;  
					yygl[21] = 58;  
					yygl[22] = 59;  
					yygl[23] = 59;  
					yygl[24] = 59;  
					yygl[25] = 59;  
					yygl[26] = 59;  
					yygl[27] = 59;  
					yygl[28] = 59;  
					yygl[29] = 59;  
					yygl[30] = 59;  
					yygl[31] = 59;  
					yygl[32] = 59;  
					yygl[33] = 59;  
					yygl[34] = 60;  
					yygl[35] = 60;  
					yygl[36] = 60;  
					yygl[37] = 60;  
					yygl[38] = 60;  
					yygl[39] = 60;  
					yygl[40] = 60;  
					yygl[41] = 60;  
					yygl[42] = 60;  
					yygl[43] = 60;  
					yygl[44] = 60;  
					yygl[45] = 61;  
					yygl[46] = 62;  
					yygl[47] = 62;  
					yygl[48] = 62;  
					yygl[49] = 62;  
					yygl[50] = 62;  
					yygl[51] = 62;  
					yygl[52] = 62;  
					yygl[53] = 62;  
					yygl[54] = 62;  
					yygl[55] = 62;  
					yygl[56] = 62;  
					yygl[57] = 62;  
					yygl[58] = 62;  
					yygl[59] = 62;  
					yygl[60] = 62;  
					yygl[61] = 62;  
					yygl[62] = 62;  
					yygl[63] = 62;  
					yygl[64] = 62;  
					yygl[65] = 63;  
					yygl[66] = 63;  
					yygl[67] = 63;  
					yygl[68] = 63;  
					yygl[69] = 63;  
					yygl[70] = 63;  
					yygl[71] = 63;  
					yygl[72] = 63;  
					yygl[73] = 63;  
					yygl[74] = 63;  
					yygl[75] = 64;  
					yygl[76] = 64;  
					yygl[77] = 65;  
					yygl[78] = 65;  
					yygl[79] = 65;  
					yygl[80] = 65;  
					yygl[81] = 70;  
					yygl[82] = 70;  
					yygl[83] = 86;  
					yygl[84] = 102;  
					yygl[85] = 102;  
					yygl[86] = 119;  
					yygl[87] = 119;  
					yygl[88] = 119;  
					yygl[89] = 135;  
					yygl[90] = 135;  
					yygl[91] = 141;  
					yygl[92] = 142;  
					yygl[93] = 142;  
					yygl[94] = 142;  
					yygl[95] = 142;  
					yygl[96] = 142;  
					yygl[97] = 142;  
					yygl[98] = 142;  
					yygl[99] = 142;  
					yygl[100] = 142;  
					yygl[101] = 142;  
					yygl[102] = 142;  
					yygl[103] = 142;  
					yygl[104] = 142;  
					yygl[105] = 142;  
					yygl[106] = 142;  
					yygl[107] = 142;  
					yygl[108] = 142;  
					yygl[109] = 142;  
					yygl[110] = 142;  
					yygl[111] = 142;  
					yygl[112] = 143;  
					yygl[113] = 144;  
					yygl[114] = 144;  
					yygl[115] = 160;  
					yygl[116] = 160;  
					yygl[117] = 160;  
					yygl[118] = 160;  
					yygl[119] = 160;  
					yygl[120] = 176;  
					yygl[121] = 177;  
					yygl[122] = 178;  
					yygl[123] = 194;  
					yygl[124] = 217;  
					yygl[125] = 217;  
					yygl[126] = 240;  
					yygl[127] = 263;  
					yygl[128] = 263;  
					yygl[129] = 263;  
					yygl[130] = 263;  
					yygl[131] = 263;  
					yygl[132] = 264;  
					yygl[133] = 264;  
					yygl[134] = 264;  
					yygl[135] = 264;  
					yygl[136] = 264;  
					yygl[137] = 270;  
					yygl[138] = 271;  
					yygl[139] = 272;  
					yygl[140] = 272;  
					yygl[141] = 272;  
					yygl[142] = 272;  
					yygl[143] = 272;  
					yygl[144] = 272;  
					yygl[145] = 272;  
					yygl[146] = 275;  
					yygl[147] = 275;  
					yygl[148] = 275;  
					yygl[149] = 275;  
					yygl[150] = 275;  
					yygl[151] = 279;  
					yygl[152] = 283;  
					yygl[153] = 283;  
					yygl[154] = 306;  
					yygl[155] = 306;  
					yygl[156] = 306;  
					yygl[157] = 306;  
					yygl[158] = 306;  
					yygl[159] = 306;  
					yygl[160] = 324;  
					yygl[161] = 324;  
					yygl[162] = 340;  
					yygl[163] = 356;  
					yygl[164] = 356;  
					yygl[165] = 356;  
					yygl[166] = 356;  
					yygl[167] = 356;  
					yygl[168] = 356;  
					yygl[169] = 356;  
					yygl[170] = 356;  
					yygl[171] = 369;  
					yygl[172] = 369;  
					yygl[173] = 369;  
					yygl[174] = 370;  
					yygl[175] = 371;  
					yygl[176] = 372;  
					yygl[177] = 373;  
					yygl[178] = 373;  
					yygl[179] = 373;  
					yygl[180] = 373;  
					yygl[181] = 373;  
					yygl[182] = 373;  
					yygl[183] = 373;  
					yygl[184] = 374;  
					yygl[185] = 397;  
					yygl[186] = 397;  
					yygl[187] = 397;  
					yygl[188] = 413;  
					yygl[189] = 413;  
					yygl[190] = 413;  
					yygl[191] = 413;  
					yygl[192] = 428;  
					yygl[193] = 434;  
					yygl[194] = 434;  
					yygl[195] = 434;  
					yygl[196] = 434;  
					yygl[197] = 434;  
					yygl[198] = 434;  
					yygl[199] = 434;  
					yygl[200] = 434;  
					yygl[201] = 449;  
					yygl[202] = 449;  
					yygl[203] = 449;  
					yygl[204] = 449;  
					yygl[205] = 449;  
					yygl[206] = 449;  
					yygl[207] = 449;  
					yygl[208] = 449;  
					yygl[209] = 449;  
					yygl[210] = 449;  
					yygl[211] = 449;  
					yygl[212] = 450;  
					yygl[213] = 450;  
					yygl[214] = 450;  
					yygl[215] = 467;  
					yygl[216] = 484;  
					yygl[217] = 500;  
					yygl[218] = 523;  
					yygl[219] = 523;  
					yygl[220] = 536;  
					yygl[221] = 536;  
					yygl[222] = 536;  
					yygl[223] = 536;  
					yygl[224] = 550;  
					yygl[225] = 550;  
					yygl[226] = 550;  
					yygl[227] = 565;  
					yygl[228] = 565;  
					yygl[229] = 565;  
					yygl[230] = 565;  
					yygl[231] = 565;  
					yygl[232] = 581;  
					yygl[233] = 581;  
					yygl[234] = 581;  
					yygl[235] = 598;  
					yygl[236] = 616;  
					yygl[237] = 635;  
					yygl[238] = 655;  
					yygl[239] = 676;  
					yygl[240] = 699;  
					yygl[241] = 699;  
					yygl[242] = 699;  
					yygl[243] = 715;  
					yygl[244] = 731;  
					yygl[245] = 731;  
					yygl[246] = 732;  
					yygl[247] = 732;  
					yygl[248] = 732;  
					yygl[249] = 732;  
					yygl[250] = 732;  
					yygl[251] = 732;  
					yygl[252] = 732;  
					yygl[253] = 732;  
					yygl[254] = 732;  
					yygl[255] = 739;  
					yygl[256] = 746;  
					yygl[257] = 749;  
					yygl[258] = 749;  
					yygl[259] = 749;  
					yygl[260] = 767;  
					yygl[261] = 767;  
					yygl[262] = 767;  
					yygl[263] = 767;  
					yygl[264] = 767;  
					yygl[265] = 767;  
					yygl[266] = 767;  
					yygl[267] = 768;  
					yygl[268] = 769;  
					yygl[269] = 770;  
					yygl[270] = 771;  
					yygl[271] = 771;  
					yygl[272] = 771;  
					yygl[273] = 771;  
					yygl[274] = 771;  
					yygl[275] = 771;  
					yygl[276] = 771;  
					yygl[277] = 771;  
					yygl[278] = 771;  
					yygl[279] = 771;  
					yygl[280] = 786;  
					yygl[281] = 789;  
					yygl[282] = 789;  
					yygl[283] = 789;  
					yygl[284] = 789;  
					yygl[285] = 789;  
					yygl[286] = 789;  
					yygl[287] = 789;  
					yygl[288] = 789;  
					yygl[289] = 789;  
					yygl[290] = 789;  
					yygl[291] = 789;  
					yygl[292] = 789;  
					yygl[293] = 789;  
					yygl[294] = 789;  
					yygl[295] = 789;  
					yygl[296] = 789;  
					yygl[297] = 789;  
					yygl[298] = 789;  
					yygl[299] = 789;  
					yygl[300] = 789;  
					yygl[301] = 805;  
					yygl[302] = 811;  
					yygl[303] = 811;  
					yygl[304] = 811;  
					yygl[305] = 811; 

					yygh = new int[yynstates];
					yygh[0] = 16;  
					yygh[1] = 20;  
					yygh[2] = 20;  
					yygh[3] = 24;  
					yygh[4] = 25;  
					yygh[5] = 33;  
					yygh[6] = 38;  
					yygh[7] = 38;  
					yygh[8] = 38;  
					yygh[9] = 38;  
					yygh[10] = 38;  
					yygh[11] = 38;  
					yygh[12] = 38;  
					yygh[13] = 38;  
					yygh[14] = 53;  
					yygh[15] = 53;  
					yygh[16] = 53;  
					yygh[17] = 54;  
					yygh[18] = 55;  
					yygh[19] = 56;  
					yygh[20] = 57;  
					yygh[21] = 58;  
					yygh[22] = 58;  
					yygh[23] = 58;  
					yygh[24] = 58;  
					yygh[25] = 58;  
					yygh[26] = 58;  
					yygh[27] = 58;  
					yygh[28] = 58;  
					yygh[29] = 58;  
					yygh[30] = 58;  
					yygh[31] = 58;  
					yygh[32] = 58;  
					yygh[33] = 59;  
					yygh[34] = 59;  
					yygh[35] = 59;  
					yygh[36] = 59;  
					yygh[37] = 59;  
					yygh[38] = 59;  
					yygh[39] = 59;  
					yygh[40] = 59;  
					yygh[41] = 59;  
					yygh[42] = 59;  
					yygh[43] = 59;  
					yygh[44] = 60;  
					yygh[45] = 61;  
					yygh[46] = 61;  
					yygh[47] = 61;  
					yygh[48] = 61;  
					yygh[49] = 61;  
					yygh[50] = 61;  
					yygh[51] = 61;  
					yygh[52] = 61;  
					yygh[53] = 61;  
					yygh[54] = 61;  
					yygh[55] = 61;  
					yygh[56] = 61;  
					yygh[57] = 61;  
					yygh[58] = 61;  
					yygh[59] = 61;  
					yygh[60] = 61;  
					yygh[61] = 61;  
					yygh[62] = 61;  
					yygh[63] = 61;  
					yygh[64] = 62;  
					yygh[65] = 62;  
					yygh[66] = 62;  
					yygh[67] = 62;  
					yygh[68] = 62;  
					yygh[69] = 62;  
					yygh[70] = 62;  
					yygh[71] = 62;  
					yygh[72] = 62;  
					yygh[73] = 62;  
					yygh[74] = 63;  
					yygh[75] = 63;  
					yygh[76] = 64;  
					yygh[77] = 64;  
					yygh[78] = 64;  
					yygh[79] = 64;  
					yygh[80] = 69;  
					yygh[81] = 69;  
					yygh[82] = 85;  
					yygh[83] = 101;  
					yygh[84] = 101;  
					yygh[85] = 118;  
					yygh[86] = 118;  
					yygh[87] = 118;  
					yygh[88] = 134;  
					yygh[89] = 134;  
					yygh[90] = 140;  
					yygh[91] = 141;  
					yygh[92] = 141;  
					yygh[93] = 141;  
					yygh[94] = 141;  
					yygh[95] = 141;  
					yygh[96] = 141;  
					yygh[97] = 141;  
					yygh[98] = 141;  
					yygh[99] = 141;  
					yygh[100] = 141;  
					yygh[101] = 141;  
					yygh[102] = 141;  
					yygh[103] = 141;  
					yygh[104] = 141;  
					yygh[105] = 141;  
					yygh[106] = 141;  
					yygh[107] = 141;  
					yygh[108] = 141;  
					yygh[109] = 141;  
					yygh[110] = 141;  
					yygh[111] = 142;  
					yygh[112] = 143;  
					yygh[113] = 143;  
					yygh[114] = 159;  
					yygh[115] = 159;  
					yygh[116] = 159;  
					yygh[117] = 159;  
					yygh[118] = 159;  
					yygh[119] = 175;  
					yygh[120] = 176;  
					yygh[121] = 177;  
					yygh[122] = 193;  
					yygh[123] = 216;  
					yygh[124] = 216;  
					yygh[125] = 239;  
					yygh[126] = 262;  
					yygh[127] = 262;  
					yygh[128] = 262;  
					yygh[129] = 262;  
					yygh[130] = 262;  
					yygh[131] = 263;  
					yygh[132] = 263;  
					yygh[133] = 263;  
					yygh[134] = 263;  
					yygh[135] = 263;  
					yygh[136] = 269;  
					yygh[137] = 270;  
					yygh[138] = 271;  
					yygh[139] = 271;  
					yygh[140] = 271;  
					yygh[141] = 271;  
					yygh[142] = 271;  
					yygh[143] = 271;  
					yygh[144] = 271;  
					yygh[145] = 274;  
					yygh[146] = 274;  
					yygh[147] = 274;  
					yygh[148] = 274;  
					yygh[149] = 274;  
					yygh[150] = 278;  
					yygh[151] = 282;  
					yygh[152] = 282;  
					yygh[153] = 305;  
					yygh[154] = 305;  
					yygh[155] = 305;  
					yygh[156] = 305;  
					yygh[157] = 305;  
					yygh[158] = 305;  
					yygh[159] = 323;  
					yygh[160] = 323;  
					yygh[161] = 339;  
					yygh[162] = 355;  
					yygh[163] = 355;  
					yygh[164] = 355;  
					yygh[165] = 355;  
					yygh[166] = 355;  
					yygh[167] = 355;  
					yygh[168] = 355;  
					yygh[169] = 355;  
					yygh[170] = 368;  
					yygh[171] = 368;  
					yygh[172] = 368;  
					yygh[173] = 369;  
					yygh[174] = 370;  
					yygh[175] = 371;  
					yygh[176] = 372;  
					yygh[177] = 372;  
					yygh[178] = 372;  
					yygh[179] = 372;  
					yygh[180] = 372;  
					yygh[181] = 372;  
					yygh[182] = 372;  
					yygh[183] = 373;  
					yygh[184] = 396;  
					yygh[185] = 396;  
					yygh[186] = 396;  
					yygh[187] = 412;  
					yygh[188] = 412;  
					yygh[189] = 412;  
					yygh[190] = 412;  
					yygh[191] = 427;  
					yygh[192] = 433;  
					yygh[193] = 433;  
					yygh[194] = 433;  
					yygh[195] = 433;  
					yygh[196] = 433;  
					yygh[197] = 433;  
					yygh[198] = 433;  
					yygh[199] = 433;  
					yygh[200] = 448;  
					yygh[201] = 448;  
					yygh[202] = 448;  
					yygh[203] = 448;  
					yygh[204] = 448;  
					yygh[205] = 448;  
					yygh[206] = 448;  
					yygh[207] = 448;  
					yygh[208] = 448;  
					yygh[209] = 448;  
					yygh[210] = 448;  
					yygh[211] = 449;  
					yygh[212] = 449;  
					yygh[213] = 449;  
					yygh[214] = 466;  
					yygh[215] = 483;  
					yygh[216] = 499;  
					yygh[217] = 522;  
					yygh[218] = 522;  
					yygh[219] = 535;  
					yygh[220] = 535;  
					yygh[221] = 535;  
					yygh[222] = 535;  
					yygh[223] = 549;  
					yygh[224] = 549;  
					yygh[225] = 549;  
					yygh[226] = 564;  
					yygh[227] = 564;  
					yygh[228] = 564;  
					yygh[229] = 564;  
					yygh[230] = 564;  
					yygh[231] = 580;  
					yygh[232] = 580;  
					yygh[233] = 580;  
					yygh[234] = 597;  
					yygh[235] = 615;  
					yygh[236] = 634;  
					yygh[237] = 654;  
					yygh[238] = 675;  
					yygh[239] = 698;  
					yygh[240] = 698;  
					yygh[241] = 698;  
					yygh[242] = 714;  
					yygh[243] = 730;  
					yygh[244] = 730;  
					yygh[245] = 731;  
					yygh[246] = 731;  
					yygh[247] = 731;  
					yygh[248] = 731;  
					yygh[249] = 731;  
					yygh[250] = 731;  
					yygh[251] = 731;  
					yygh[252] = 731;  
					yygh[253] = 731;  
					yygh[254] = 738;  
					yygh[255] = 745;  
					yygh[256] = 748;  
					yygh[257] = 748;  
					yygh[258] = 748;  
					yygh[259] = 766;  
					yygh[260] = 766;  
					yygh[261] = 766;  
					yygh[262] = 766;  
					yygh[263] = 766;  
					yygh[264] = 766;  
					yygh[265] = 766;  
					yygh[266] = 767;  
					yygh[267] = 768;  
					yygh[268] = 769;  
					yygh[269] = 770;  
					yygh[270] = 770;  
					yygh[271] = 770;  
					yygh[272] = 770;  
					yygh[273] = 770;  
					yygh[274] = 770;  
					yygh[275] = 770;  
					yygh[276] = 770;  
					yygh[277] = 770;  
					yygh[278] = 770;  
					yygh[279] = 785;  
					yygh[280] = 788;  
					yygh[281] = 788;  
					yygh[282] = 788;  
					yygh[283] = 788;  
					yygh[284] = 788;  
					yygh[285] = 788;  
					yygh[286] = 788;  
					yygh[287] = 788;  
					yygh[288] = 788;  
					yygh[289] = 788;  
					yygh[290] = 788;  
					yygh[291] = 788;  
					yygh[292] = 788;  
					yygh[293] = 788;  
					yygh[294] = 788;  
					yygh[295] = 788;  
					yygh[296] = 788;  
					yygh[297] = 788;  
					yygh[298] = 788;  
					yygh[299] = 788;  
					yygh[300] = 804;  
					yygh[301] = 810;  
					yygh[302] = 810;  
					yygh[303] = 810;  
					yygh[304] = 810;  
					yygh[305] = 810; 

					yyr[yyrc] = new YYRRec(1,-2);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++;
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

			if (Regex.IsMatch(Rest,"^((?i)IFELSE)")){
				Results.Add (t_IFELSE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)IFELSE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)ENDIF)")){
				Results.Add (t_ENDIF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)ENDIF)").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)(IF_MIDDLE|IF_ANYKEY|IF_EQUALS|IF_PERIOD|IF_BRACKL|IF_BRACKR|IF_START|IF_RIGHT|IF_KLICK|IF_MSTOP|IF_SPACE|IF_PAUSE|IF_COMMA|IF_SEMIC|IF_SLASH|EACH_SEC|MESSAGES|IF_LOAD|IF_LEFT|IF_CTRL|IF_BKSP|IF_PGUP|IF_PGDN|IF_HOME|IF_BKSL|IF_ESC|IF_TAB|IF_ALT|IF_CUU|IF_CUD|IF_CUR|IF_CUL|IF_END|IF_INS|IF_DEL|IF_CAR|IF_CAL|PANELS|LAYERS|(IF_F(1[0-2]|[1-9]))|IF_[0-9A-Z]))")){
				Results.Add (t_event);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(IF_MIDDLE|IF_ANYKEY|IF_EQUALS|IF_PERIOD|IF_BRACKL|IF_BRACKR|IF_START|IF_RIGHT|IF_KLICK|IF_MSTOP|IF_SPACE|IF_PAUSE|IF_COMMA|IF_SEMIC|IF_SLASH|EACH_SEC|MESSAGES|IF_LOAD|IF_LEFT|IF_CTRL|IF_BKSP|IF_PGUP|IF_PGDN|IF_HOME|IF_BKSL|IF_ESC|IF_TAB|IF_ALT|IF_CUU|IF_CUD|IF_CUR|IF_CUL|IF_END|IF_INS|IF_DEL|IF_CAR|IF_CAL|PANELS|LAYERS|(IF_F(1[0-2]|[1-9]))|IF_[0-9A-Z]))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER))")){
				Results.Add (t_global);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))")){
				Results.Add (t_asset);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))")){
				Results.Add (t_object);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACTION|RULES))")){
				Results.Add (t_function);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACTION|RULES))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACOS|COS|TAN|SIGN|ABS|INT|EXP|LOG10|LOG2|LOG))")){
				Results.Add (t_math);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACOS|COS|TAN|SIGN|ABS|INT|EXP|LOG10|LOG2|LOG))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|HERE|PLAY|SEEN|WIRE|FAR|SKY))")){
				Results.Add (t_flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|HERE|PLAY|SEEN|WIRE|FAR|SKY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_OFFS_X|FLOOR_OFFS_Y|CEIL_OFFS_X|CEIL_OFFS_Y|FLOOR_ANGLE|IF_RELEASE|SKILL[1-8]|EACH_CYCLE|CEIL_ANGLE|IF_ARRIVED|TARGET_MAP|MAP_COLOR|FLOOR_TEX|REL_ANGLE|OFFSET_X|OFFSET_Y|SCALE_XY|RADIANCE|IF_TOUCH|IF_KLICK|POSITION|DISTANCE|CEIL_TEX|IF_LEAVE|IF_ARISE|WAYPOINT|TARGET_X|TARGET_Y|REL_DIST|PALFILE|SCALE_X|SCALE_Y|SCYCLES|IF_NEAR|IF_DIVE|PAN_MAP|VSLIDER|HSLIDER|PICTURE|STRINGS|DEFAULT|CYCLES|MIRROR|ALBEDO|SVDIST|ATTACH|LENGTH|SIZE_X|SIZE_Y|IF_FAR|GENIUS|TARGET|HEIGHT|VSPEED|ASPEED|BUTTON|DIGITS|ASPECT|RANGE|FLAGS|SIDES|FRAME|TITLE|DELAY|SDIST|POS_X|POS_Y|TOUCH|CYCLE|BELOW|ANGLE|SPEED|BMAPS|OVLYS|LAYER|RIGHT|SVOL|DIST|SIDE|VBAR|HBAR|MASK|LEFT|TYPE|TOP|VAL|MIN|MAX|X1|Y1|Z1|X2|Y2|Z2|X|Y|Z|INDEX))")){
				Results.Add (t_property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_OFFS_X|FLOOR_OFFS_Y|CEIL_OFFS_X|CEIL_OFFS_Y|FLOOR_ANGLE|IF_RELEASE|SKILL[1-8]|EACH_CYCLE|CEIL_ANGLE|IF_ARRIVED|TARGET_MAP|MAP_COLOR|FLOOR_TEX|REL_ANGLE|OFFSET_X|OFFSET_Y|SCALE_XY|RADIANCE|IF_TOUCH|IF_KLICK|POSITION|DISTANCE|CEIL_TEX|IF_LEAVE|IF_ARISE|WAYPOINT|TARGET_X|TARGET_Y|REL_DIST|PALFILE|SCALE_X|SCALE_Y|SCYCLES|IF_NEAR|IF_DIVE|PAN_MAP|VSLIDER|HSLIDER|PICTURE|STRINGS|DEFAULT|CYCLES|MIRROR|ALBEDO|SVDIST|ATTACH|LENGTH|SIZE_X|SIZE_Y|IF_FAR|GENIUS|TARGET|HEIGHT|VSPEED|ASPEED|BUTTON|DIGITS|ASPECT|RANGE|FLAGS|SIDES|FRAME|TITLE|DELAY|SDIST|POS_X|POS_Y|TOUCH|CYCLE|BELOW|ANGLE|SPEED|BMAPS|OVLYS|LAYER|RIGHT|SVOL|DIST|SIDE|VBAR|HBAR|MASK|LEFT|TYPE|TOP|VAL|MIN|MAX|X1|Y1|Z1|X2|Y2|Z2|X|Y|Z|INDEX))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(PLAY_SOUNDFILE|PLAY_SONG_ONCE|PLAY_FLICFILE|NEXT_MY_THERE|PRINT_STRING|PRINT_VALUE|PLAY_SOUND|STOP_SOUND|SET_STRING|SCREENSHOT|NEXT_THERE|ADD_STRING|RANDOMIZE|PLAY_SONG|PLAY_FLIC|STOP_FLIC|SAVE_INFO|LOAD_INFO|SAVE_DEMO|PLAY_DEMO|STOP_DEMO|EXCLUSIVE|PRINTFILE|SET_SKILL|TO_STRING|IF_NEQUAL|IF_ABOVE|IF_BELOW|IF_EQUAL|FADE_PAL|MIDI_COM|SET_INFO|SET_ALL|PLAY_CD|OUTPORT|SETMIDI|GETMIDI|EXPLODE|NEXT_MY|IF_MIN|IF_MAX|BRANCH|INPORT|FREEZE|LOCATE|ROTATE|ACCEL|WAITT|INKEY|PLACE|SHOOT|SHAKE|SHIFT|LEVEL|ADDT|SUBT|SKIP|GOTO|CALL|WAIT|BEEP|FIND|DROP|PUSH|LIFT|TILT|LOAD|EXIT|SET|ADD|SUB|AND|END|MAP))")){
				Results.Add (t_command);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(PLAY_SOUNDFILE|PLAY_SONG_ONCE|PLAY_FLICFILE|NEXT_MY_THERE|PRINT_STRING|PRINT_VALUE|PLAY_SOUND|STOP_SOUND|SET_STRING|SCREENSHOT|NEXT_THERE|ADD_STRING|RANDOMIZE|PLAY_SONG|PLAY_FLIC|STOP_FLIC|SAVE_INFO|LOAD_INFO|SAVE_DEMO|PLAY_DEMO|STOP_DEMO|EXCLUSIVE|PRINTFILE|SET_SKILL|TO_STRING|IF_NEQUAL|IF_ABOVE|IF_BELOW|IF_EQUAL|FADE_PAL|MIDI_COM|SET_INFO|SET_ALL|PLAY_CD|OUTPORT|SETMIDI|GETMIDI|EXPLODE|NEXT_MY|IF_MIN|IF_MAX|BRANCH|INPORT|FREEZE|LOCATE|ROTATE|ACCEL|WAITT|INKEY|PLACE|SHOOT|SHAKE|SHIFT|LEVEL|ADDT|SUBT|SKIP|GOTO|CALL|WAIT|BEEP|FIND|DROP|PUSH|LIFT|TILT|LOAD|EXIT|SET|ADD|SUB|AND|END|MAP))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))")){
				Results.Add (t_list);
				ResultsV.Add(Regex.Match(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACTOR_IMPACT_VX|ACTOR_IMPACT_VY|ACTOR_IMPACT_VZ|ACTOR_FLOOR_HGT|ACTIVE_OBJTICKS|ACTOR_CEIL_HGT|ACTIVE_TARGETS|CHANNEL_[0-7]|PLAYER_LAST_X|PLAYER_LAST_Y|SCREEN_WIDTH|COLOR_PLAYER|COLOR_ACTORS|COLOR_THINGS|COLOR_BORDER|MOUSE_MOVING|PLAYER_LIGHT|PLAYER_WIDTH|PLAYER_CLIMB|SHOOT_SECTOR|PLAYER_ANGLE|PLAYER_SPEED|ACCELERATION|PLAYER_DEPTH|MOUSE_MIDDLE|FORCE_STRAFE|ACTIVE_NEXUS|MOTION_BLUR|RENDER_MODE|MAP_EDGE_X1|MAP_EDGE_X2|MAP_EDGE_Y1|MAP_EDGE_Y2|COLOR_WALLS|PANEL_LAYER|MOUSE_ANGLE|TOUCH_STATE|PLAYER_SIZE|WALK_PERIOD|WAVE_PERIOD|PSOUND_TONE|PLAYER_VROT|PLAYER_TILT|SHOOT_RANGE|HIT_MINDIST|SHOOT_ANGLE|SKIP_FRAMES|ACTOR_CLIMB|ACTOR_WIDTH|THING_WIDTH|IMPACT_VROT|SLOPE_AHEAD|DELTA_ANGLE|MOUSE_RIGHT|FORCE_AHEAD|SHIFT_SENSE|MOUSE_SENSE|MAP_CENTERX|MAP_CENTERY|CDAUDIO_VOL|SCREEN_HGT|SKY_OFFS_X|SKY_OFFS_Y|THING_DIST|ACTOR_DIST|MAP_OFFS_X|MAP_OFFS_Y|TEXT_LAYER|MOUSE_MODE|TOUCH_MODE|MOUSE_CALM|MOUSE_TIME|TOUCH_DIST|JOYSTICK_X|JOYSTICK_Y|LIGHT_DIST|PSOUND_VOL|PLAYER_ARC|PLAYER_SIN|PLAYER_COS|PLAYER_HGT|SLOPE_SIDE|MOVE_ANGLE|FLIC_FRAME|MOUSE_LEFT|KEY_EQUALS|KEY_PERIOD|KEY_BRACKL|KEY_BRACKR|FORCE_TILE|DEBUG_MODE|BLUR_MODE|MOVE_MODE|MAP_SCALE|MAP_LAYER|SOUND_VOL|MUSIC_VOL|DARK_DIST|WALK_TIME|PLAYER_VX|PLAYER_VY|PLAYER_VZ|SHOOT_FAC|IMPACT_VX|IMPACT_VY|IMPACT_VZ|BOUNCE_VX|BOUNCE_VY|TIME_CORR|KEY_SHIFT|KEY_SPACE|KEY_PAUSE|KEY_MINUS|KEY_ENTER|KEY_COMMA|KEY_SEMIC|KEY_SLASH|FORCE_ROT|KEY_SENSE|JOY_SENSE|LOAD_MODE|SCREEN_X|SCREEN_Y|CLIPPING|MAP_MAXX|MAP_MINX|MAP_MAXY|MAP_MINY|MAP_MODE|MICKEY_X|MICKEY_Y|FRICTION|HIT_DIST|PLAYER_X|PLAYER_Y|PLAYER_Z|REMOTE_0|REMOTE_1|TIME_FAC|CD_TRACK|KEY_CTRL|KEY_BKSP|KEY_PGUP|KEY_PGDN|KEY_HOME|KEY_PLUS|KEY_BKSL|FORCE_UP|MAX_DIST|MAP_ROT|MOUSE_X|MOUSE_Y|CHANNEL|INERTIA|SHOOT_X|SHOOT_Y|SLOPE_X|SLOPE_Y|KEY_ANY|KEY_ESC|KEY_TAB|KEY_ALT|KEY_CUU|KEY_CUD|KEY_CUR|KEY_CUL|KEY_END|KEY_INS|KEY_DEL|KEY_CAR|KEY_CAL|ACTIONS|STR_LEN|ASPECT|HIT_X|HIT_Y|TICKS|STEPS|JOY_4|ERROR|WALK|WAVE|NODE|SECS|(KEY_F(1[0-2]|[1-9]))|KEY[A-Z0-9]))")){
				Results.Add (t_skill);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACTOR_IMPACT_VX|ACTOR_IMPACT_VY|ACTOR_IMPACT_VZ|ACTOR_FLOOR_HGT|ACTIVE_OBJTICKS|ACTOR_CEIL_HGT|ACTIVE_TARGETS|CHANNEL_[0-7]|PLAYER_LAST_X|PLAYER_LAST_Y|SCREEN_WIDTH|COLOR_PLAYER|COLOR_ACTORS|COLOR_THINGS|COLOR_BORDER|MOUSE_MOVING|PLAYER_LIGHT|PLAYER_WIDTH|PLAYER_CLIMB|SHOOT_SECTOR|PLAYER_ANGLE|PLAYER_SPEED|ACCELERATION|PLAYER_DEPTH|MOUSE_MIDDLE|FORCE_STRAFE|ACTIVE_NEXUS|MOTION_BLUR|RENDER_MODE|MAP_EDGE_X1|MAP_EDGE_X2|MAP_EDGE_Y1|MAP_EDGE_Y2|COLOR_WALLS|PANEL_LAYER|MOUSE_ANGLE|TOUCH_STATE|PLAYER_SIZE|WALK_PERIOD|WAVE_PERIOD|PSOUND_TONE|PLAYER_VROT|PLAYER_TILT|SHOOT_RANGE|HIT_MINDIST|SHOOT_ANGLE|SKIP_FRAMES|ACTOR_CLIMB|ACTOR_WIDTH|THING_WIDTH|IMPACT_VROT|SLOPE_AHEAD|DELTA_ANGLE|MOUSE_RIGHT|FORCE_AHEAD|SHIFT_SENSE|MOUSE_SENSE|MAP_CENTERX|MAP_CENTERY|CDAUDIO_VOL|SCREEN_HGT|SKY_OFFS_X|SKY_OFFS_Y|THING_DIST|ACTOR_DIST|MAP_OFFS_X|MAP_OFFS_Y|TEXT_LAYER|MOUSE_MODE|TOUCH_MODE|MOUSE_CALM|MOUSE_TIME|TOUCH_DIST|JOYSTICK_X|JOYSTICK_Y|LIGHT_DIST|PSOUND_VOL|PLAYER_ARC|PLAYER_SIN|PLAYER_COS|PLAYER_HGT|SLOPE_SIDE|MOVE_ANGLE|FLIC_FRAME|MOUSE_LEFT|KEY_EQUALS|KEY_PERIOD|KEY_BRACKL|KEY_BRACKR|FORCE_TILE|DEBUG_MODE|BLUR_MODE|MOVE_MODE|MAP_SCALE|MAP_LAYER|SOUND_VOL|MUSIC_VOL|DARK_DIST|WALK_TIME|PLAYER_VX|PLAYER_VY|PLAYER_VZ|SHOOT_FAC|IMPACT_VX|IMPACT_VY|IMPACT_VZ|BOUNCE_VX|BOUNCE_VY|TIME_CORR|KEY_SHIFT|KEY_SPACE|KEY_PAUSE|KEY_MINUS|KEY_ENTER|KEY_COMMA|KEY_SEMIC|KEY_SLASH|FORCE_ROT|KEY_SENSE|JOY_SENSE|LOAD_MODE|SCREEN_X|SCREEN_Y|CLIPPING|MAP_MAXX|MAP_MINX|MAP_MAXY|MAP_MINY|MAP_MODE|MICKEY_X|MICKEY_Y|FRICTION|HIT_DIST|PLAYER_X|PLAYER_Y|PLAYER_Z|REMOTE_0|REMOTE_1|TIME_FAC|CD_TRACK|KEY_CTRL|KEY_BKSP|KEY_PGUP|KEY_PGDN|KEY_HOME|KEY_PLUS|KEY_BKSL|FORCE_UP|MAX_DIST|MAP_ROT|MOUSE_X|MOUSE_Y|CHANNEL|INERTIA|SHOOT_X|SHOOT_Y|SLOPE_X|SLOPE_Y|KEY_ANY|KEY_ESC|KEY_TAB|KEY_ALT|KEY_CUU|KEY_CUD|KEY_CUR|KEY_CUL|KEY_END|KEY_INS|KEY_DEL|KEY_CAR|KEY_CAL|ACTIONS|STR_LEN|ASPECT|HIT_X|HIT_Y|TICKS|STEPS|JOY_4|ERROR|WALK|WAVE|NODE|SECS|(KEY_F(1[0-2]|[1-9]))|KEY[A-Z0-9]))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE))")){
				Results.Add (t_synonym);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)CLIP_DIST)")){
				Results.Add (t_ambigChar95globalChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)CLIP_DIST)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(EACH_TICK|IF_ENTER|IF_HIT))")){
				Results.Add (t_ambigChar95eventChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(EACH_TICK|IF_ENTER|IF_HIT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(THING|ACTOR))")){
				Results.Add (t_ambigChar95objectChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(THING|ACTOR))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(SIN|ASIN|SQRT))")){
				Results.Add (t_ambigChar95mathChar95command);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(SIN|ASIN|SQRT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)RANDOM)")){
				Results.Add (t_ambigChar95mathChar95skillChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)RANDOM)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)HERE)")){
				Results.Add (t_ambigChar95synonymChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)HERE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT))")){
				Results.Add (t_ambigChar95skillChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)SAVE)")){
				Results.Add (t_ambigChar95commandChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)SAVE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)MSPRITE)")){
				Results.Add (t_ambigChar95synonymChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)MSPRITE)").Value);}

			if (Regex.IsMatch(Rest,"^([0-9]+)")){
				Results.Add (t_integer);
				ResultsV.Add(Regex.Match(Rest,"^([0-9]+)").Value);}

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
