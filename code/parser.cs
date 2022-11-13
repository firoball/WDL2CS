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
                int t_ambigChar95globalChar95synonymChar95property = 319;
                int t_ambigChar95commandChar95property = 320;
                int t_integer = 321;
                int t_fixed = 322;
                int t_identifier = 323;
                int t_file = 324;
                int t_string = 325;
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
         yyval = yyv[yysp-0];
         
       break;
							case   34 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   35 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   41 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   42 : 
         yyval = yyv[yysp-0];
         
       break;
							case   43 : 
         yyval = "";
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   52 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   53 : 
         yyval = "";
         
       break;
							case   54 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   55 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   56 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   57 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   66 : 
         yyval = yyv[yysp-0];
         
       break;
							case   67 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   68 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   69 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   70 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   71 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   72 : 
         yyval = "";
         
       break;
							case   73 : 
         yyval = yyv[yysp-0];
         
       break;
							case   74 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   75 : 
         yyval = yyv[yysp-0];
         
       break;
							case   76 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   77 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = "";
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   91 : 
         yyval = yyv[yysp-0];
         
       break;
							case   92 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   93 : 
         yyval = yyv[yysp-0];
         
       break;
							case   94 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   95 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  104 : 
         yyval = yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case  124 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  125 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  126 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  136 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  137 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case  167 : 
         yyval = yyv[yysp-0];
         
       break;
							case  168 : 
         yyval = yyv[yysp-0];
         
       break;
							case  169 : 
         yyval = yyv[yysp-0];
         
       break;
							case  170 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  171 : 
         yyval = yyv[yysp-0];
         
       break;
							case  172 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
							case  190 : 
         yyval = yyv[yysp-0];
         
       break;
							case  191 : 
         yyval = yyv[yysp-0];
         
       break;
							case  192 : 
         yyval = yyv[yysp-0];
         
       break;
							case  193 : 
         yyval = yyv[yysp-0];
         
       break;
							case  194 : 
         yyval = yyv[yysp-0];
         
       break;
							case  195 : 
         yyval = yyv[yysp-0];
         
       break;
							case  196 : 
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

					int yynacts   = 1952;
					int yyngotos  = 856;
					int yynstates = 313;
					int yynrules  = 196;
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
					yya[yyac] = new YYARec(319,30);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,53);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(322,66);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(325,69);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
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
					yya[yyac] = new YYARec(319,30);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(263,79);yyac++; 
					yya[yyac] = new YYARec(266,-84 );yyac++; 
					yya[yyac] = new YYARec(258,81);yyac++; 
					yya[yyac] = new YYARec(263,79);yyac++; 
					yya[yyac] = new YYARec(266,-84 );yyac++; 
					yya[yyac] = new YYARec(325,-84 );yyac++; 
					yya[yyac] = new YYARec(263,79);yyac++; 
					yya[yyac] = new YYARec(324,-84 );yyac++; 
					yya[yyac] = new YYARec(321,83);yyac++; 
					yya[yyac] = new YYARec(322,84);yyac++; 
					yya[yyac] = new YYARec(258,85);yyac++; 
					yya[yyac] = new YYARec(263,79);yyac++; 
					yya[yyac] = new YYARec(258,-84 );yyac++; 
					yya[yyac] = new YYARec(300,-84 );yyac++; 
					yya[yyac] = new YYARec(301,-84 );yyac++; 
					yya[yyac] = new YYARec(304,-84 );yyac++; 
					yya[yyac] = new YYARec(305,-84 );yyac++; 
					yya[yyac] = new YYARec(306,-84 );yyac++; 
					yya[yyac] = new YYARec(307,-84 );yyac++; 
					yya[yyac] = new YYARec(309,-84 );yyac++; 
					yya[yyac] = new YYARec(314,-84 );yyac++; 
					yya[yyac] = new YYARec(315,-84 );yyac++; 
					yya[yyac] = new YYARec(317,-84 );yyac++; 
					yya[yyac] = new YYARec(318,-84 );yyac++; 
					yya[yyac] = new YYARec(320,-84 );yyac++; 
					yya[yyac] = new YYARec(323,-84 );yyac++; 
					yya[yyac] = new YYARec(258,87);yyac++; 
					yya[yyac] = new YYARec(258,88);yyac++; 
					yya[yyac] = new YYARec(258,89);yyac++; 
					yya[yyac] = new YYARec(258,90);yyac++; 
					yya[yyac] = new YYARec(263,91);yyac++; 
					yya[yyac] = new YYARec(258,92);yyac++; 
					yya[yyac] = new YYARec(258,93);yyac++; 
					yya[yyac] = new YYARec(266,94);yyac++; 
					yya[yyac] = new YYARec(266,96);yyac++; 
					yya[yyac] = new YYARec(325,69);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(258,-34 );yyac++; 
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
					yya[yyac] = new YYARec(319,30);yyac++; 
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
					yya[yyac] = new YYARec(319,30);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(308,113);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(322,66);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(325,69);yyac++; 
					yya[yyac] = new YYARec(257,127);yyac++; 
					yya[yyac] = new YYARec(259,128);yyac++; 
					yya[yyac] = new YYARec(266,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(294,131);yyac++; 
					yya[yyac] = new YYARec(295,132);yyac++; 
					yya[yyac] = new YYARec(296,133);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,134);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,135);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(258,136);yyac++; 
					yya[yyac] = new YYARec(257,143);yyac++; 
					yya[yyac] = new YYARec(259,144);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,145);yyac++; 
					yya[yyac] = new YYARec(311,146);yyac++; 
					yya[yyac] = new YYARec(312,147);yyac++; 
					yya[yyac] = new YYARec(315,148);yyac++; 
					yya[yyac] = new YYARec(317,149);yyac++; 
					yya[yyac] = new YYARec(319,150);yyac++; 
					yya[yyac] = new YYARec(320,151);yyac++; 
					yya[yyac] = new YYARec(267,-53 );yyac++; 
					yya[yyac] = new YYARec(263,79);yyac++; 
					yya[yyac] = new YYARec(258,-84 );yyac++; 
					yya[yyac] = new YYARec(282,-84 );yyac++; 
					yya[yyac] = new YYARec(283,-84 );yyac++; 
					yya[yyac] = new YYARec(287,-84 );yyac++; 
					yya[yyac] = new YYARec(321,-84 );yyac++; 
					yya[yyac] = new YYARec(258,153);yyac++; 
					yya[yyac] = new YYARec(260,154);yyac++; 
					yya[yyac] = new YYARec(261,155);yyac++; 
					yya[yyac] = new YYARec(258,156);yyac++; 
					yya[yyac] = new YYARec(297,157);yyac++; 
					yya[yyac] = new YYARec(258,-146 );yyac++; 
					yya[yyac] = new YYARec(263,-146 );yyac++; 
					yya[yyac] = new YYARec(266,-146 );yyac++; 
					yya[yyac] = new YYARec(269,-146 );yyac++; 
					yya[yyac] = new YYARec(270,-146 );yyac++; 
					yya[yyac] = new YYARec(271,-146 );yyac++; 
					yya[yyac] = new YYARec(272,-146 );yyac++; 
					yya[yyac] = new YYARec(273,-146 );yyac++; 
					yya[yyac] = new YYARec(275,-146 );yyac++; 
					yya[yyac] = new YYARec(276,-146 );yyac++; 
					yya[yyac] = new YYARec(277,-146 );yyac++; 
					yya[yyac] = new YYARec(278,-146 );yyac++; 
					yya[yyac] = new YYARec(279,-146 );yyac++; 
					yya[yyac] = new YYARec(280,-146 );yyac++; 
					yya[yyac] = new YYARec(281,-146 );yyac++; 
					yya[yyac] = new YYARec(282,-146 );yyac++; 
					yya[yyac] = new YYARec(283,-146 );yyac++; 
					yya[yyac] = new YYARec(284,-146 );yyac++; 
					yya[yyac] = new YYARec(285,-146 );yyac++; 
					yya[yyac] = new YYARec(286,-146 );yyac++; 
					yya[yyac] = new YYARec(287,-146 );yyac++; 
					yya[yyac] = new YYARec(289,-146 );yyac++; 
					yya[yyac] = new YYARec(290,-146 );yyac++; 
					yya[yyac] = new YYARec(291,-146 );yyac++; 
					yya[yyac] = new YYARec(292,-146 );yyac++; 
					yya[yyac] = new YYARec(293,-146 );yyac++; 
					yya[yyac] = new YYARec(298,-146 );yyac++; 
					yya[yyac] = new YYARec(299,-146 );yyac++; 
					yya[yyac] = new YYARec(300,-146 );yyac++; 
					yya[yyac] = new YYARec(301,-146 );yyac++; 
					yya[yyac] = new YYARec(302,-146 );yyac++; 
					yya[yyac] = new YYARec(304,-146 );yyac++; 
					yya[yyac] = new YYARec(305,-146 );yyac++; 
					yya[yyac] = new YYARec(306,-146 );yyac++; 
					yya[yyac] = new YYARec(307,-146 );yyac++; 
					yya[yyac] = new YYARec(308,-146 );yyac++; 
					yya[yyac] = new YYARec(309,-146 );yyac++; 
					yya[yyac] = new YYARec(310,-146 );yyac++; 
					yya[yyac] = new YYARec(312,-146 );yyac++; 
					yya[yyac] = new YYARec(313,-146 );yyac++; 
					yya[yyac] = new YYARec(314,-146 );yyac++; 
					yya[yyac] = new YYARec(315,-146 );yyac++; 
					yya[yyac] = new YYARec(316,-146 );yyac++; 
					yya[yyac] = new YYARec(317,-146 );yyac++; 
					yya[yyac] = new YYARec(318,-146 );yyac++; 
					yya[yyac] = new YYARec(319,-146 );yyac++; 
					yya[yyac] = new YYARec(320,-146 );yyac++; 
					yya[yyac] = new YYARec(321,-146 );yyac++; 
					yya[yyac] = new YYARec(322,-146 );yyac++; 
					yya[yyac] = new YYARec(323,-146 );yyac++; 
					yya[yyac] = new YYARec(324,-146 );yyac++; 
					yya[yyac] = new YYARec(325,-146 );yyac++; 
					yya[yyac] = new YYARec(297,158);yyac++; 
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
					yya[yyac] = new YYARec(300,-145 );yyac++; 
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
					yya[yyac] = new YYARec(325,-145 );yyac++; 
					yya[yyac] = new YYARec(258,159);yyac++; 
					yya[yyac] = new YYARec(258,-162 );yyac++; 
					yya[yyac] = new YYARec(263,-162 );yyac++; 
					yya[yyac] = new YYARec(282,-162 );yyac++; 
					yya[yyac] = new YYARec(283,-162 );yyac++; 
					yya[yyac] = new YYARec(287,-162 );yyac++; 
					yya[yyac] = new YYARec(298,-162 );yyac++; 
					yya[yyac] = new YYARec(299,-162 );yyac++; 
					yya[yyac] = new YYARec(300,-162 );yyac++; 
					yya[yyac] = new YYARec(301,-162 );yyac++; 
					yya[yyac] = new YYARec(302,-162 );yyac++; 
					yya[yyac] = new YYARec(304,-162 );yyac++; 
					yya[yyac] = new YYARec(305,-162 );yyac++; 
					yya[yyac] = new YYARec(306,-162 );yyac++; 
					yya[yyac] = new YYARec(307,-162 );yyac++; 
					yya[yyac] = new YYARec(308,-162 );yyac++; 
					yya[yyac] = new YYARec(309,-162 );yyac++; 
					yya[yyac] = new YYARec(310,-162 );yyac++; 
					yya[yyac] = new YYARec(312,-162 );yyac++; 
					yya[yyac] = new YYARec(313,-162 );yyac++; 
					yya[yyac] = new YYARec(314,-162 );yyac++; 
					yya[yyac] = new YYARec(315,-162 );yyac++; 
					yya[yyac] = new YYARec(316,-162 );yyac++; 
					yya[yyac] = new YYARec(317,-162 );yyac++; 
					yya[yyac] = new YYARec(318,-162 );yyac++; 
					yya[yyac] = new YYARec(319,-162 );yyac++; 
					yya[yyac] = new YYARec(320,-162 );yyac++; 
					yya[yyac] = new YYARec(321,-162 );yyac++; 
					yya[yyac] = new YYARec(322,-162 );yyac++; 
					yya[yyac] = new YYARec(323,-162 );yyac++; 
					yya[yyac] = new YYARec(324,-162 );yyac++; 
					yya[yyac] = new YYARec(325,-162 );yyac++; 
					yya[yyac] = new YYARec(268,-187 );yyac++; 
					yya[yyac] = new YYARec(289,-187 );yyac++; 
					yya[yyac] = new YYARec(290,-187 );yyac++; 
					yya[yyac] = new YYARec(291,-187 );yyac++; 
					yya[yyac] = new YYARec(292,-187 );yyac++; 
					yya[yyac] = new YYARec(293,-187 );yyac++; 
					yya[yyac] = new YYARec(297,-187 );yyac++; 
					yya[yyac] = new YYARec(289,161);yyac++; 
					yya[yyac] = new YYARec(290,162);yyac++; 
					yya[yyac] = new YYARec(291,163);yyac++; 
					yya[yyac] = new YYARec(292,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(263,79);yyac++; 
					yya[yyac] = new YYARec(258,-75 );yyac++; 
					yya[yyac] = new YYARec(282,-84 );yyac++; 
					yya[yyac] = new YYARec(283,-84 );yyac++; 
					yya[yyac] = new YYARec(287,-84 );yyac++; 
					yya[yyac] = new YYARec(298,-84 );yyac++; 
					yya[yyac] = new YYARec(299,-84 );yyac++; 
					yya[yyac] = new YYARec(300,-84 );yyac++; 
					yya[yyac] = new YYARec(301,-84 );yyac++; 
					yya[yyac] = new YYARec(302,-84 );yyac++; 
					yya[yyac] = new YYARec(304,-84 );yyac++; 
					yya[yyac] = new YYARec(305,-84 );yyac++; 
					yya[yyac] = new YYARec(306,-84 );yyac++; 
					yya[yyac] = new YYARec(307,-84 );yyac++; 
					yya[yyac] = new YYARec(308,-84 );yyac++; 
					yya[yyac] = new YYARec(309,-84 );yyac++; 
					yya[yyac] = new YYARec(310,-84 );yyac++; 
					yya[yyac] = new YYARec(312,-84 );yyac++; 
					yya[yyac] = new YYARec(313,-84 );yyac++; 
					yya[yyac] = new YYARec(314,-84 );yyac++; 
					yya[yyac] = new YYARec(315,-84 );yyac++; 
					yya[yyac] = new YYARec(316,-84 );yyac++; 
					yya[yyac] = new YYARec(317,-84 );yyac++; 
					yya[yyac] = new YYARec(318,-84 );yyac++; 
					yya[yyac] = new YYARec(319,-84 );yyac++; 
					yya[yyac] = new YYARec(320,-84 );yyac++; 
					yya[yyac] = new YYARec(321,-84 );yyac++; 
					yya[yyac] = new YYARec(322,-84 );yyac++; 
					yya[yyac] = new YYARec(323,-84 );yyac++; 
					yya[yyac] = new YYARec(324,-84 );yyac++; 
					yya[yyac] = new YYARec(325,-84 );yyac++; 
					yya[yyac] = new YYARec(257,127);yyac++; 
					yya[yyac] = new YYARec(259,128);yyac++; 
					yya[yyac] = new YYARec(266,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(294,131);yyac++; 
					yya[yyac] = new YYARec(295,132);yyac++; 
					yya[yyac] = new YYARec(296,133);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,134);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,135);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(260,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(258,168);yyac++; 
					yya[yyac] = new YYARec(268,169);yyac++; 
					yya[yyac] = new YYARec(297,158);yyac++; 
					yya[yyac] = new YYARec(289,-145 );yyac++; 
					yya[yyac] = new YYARec(290,-145 );yyac++; 
					yya[yyac] = new YYARec(291,-145 );yyac++; 
					yya[yyac] = new YYARec(292,-145 );yyac++; 
					yya[yyac] = new YYARec(293,-145 );yyac++; 
					yya[yyac] = new YYARec(268,-194 );yyac++; 
					yya[yyac] = new YYARec(267,170);yyac++; 
					yya[yyac] = new YYARec(257,127);yyac++; 
					yya[yyac] = new YYARec(259,128);yyac++; 
					yya[yyac] = new YYARec(266,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(294,131);yyac++; 
					yya[yyac] = new YYARec(295,132);yyac++; 
					yya[yyac] = new YYARec(296,133);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,134);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,135);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(260,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(257,127);yyac++; 
					yya[yyac] = new YYARec(259,128);yyac++; 
					yya[yyac] = new YYARec(266,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(294,131);yyac++; 
					yya[yyac] = new YYARec(295,132);yyac++; 
					yya[yyac] = new YYARec(296,133);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,134);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,135);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(266,194);yyac++; 
					yya[yyac] = new YYARec(258,-160 );yyac++; 
					yya[yyac] = new YYARec(263,-160 );yyac++; 
					yya[yyac] = new YYARec(282,-160 );yyac++; 
					yya[yyac] = new YYARec(283,-160 );yyac++; 
					yya[yyac] = new YYARec(287,-160 );yyac++; 
					yya[yyac] = new YYARec(298,-160 );yyac++; 
					yya[yyac] = new YYARec(299,-160 );yyac++; 
					yya[yyac] = new YYARec(300,-160 );yyac++; 
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
					yya[yyac] = new YYARec(325,-160 );yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(289,-182 );yyac++; 
					yya[yyac] = new YYARec(290,-182 );yyac++; 
					yya[yyac] = new YYARec(291,-182 );yyac++; 
					yya[yyac] = new YYARec(292,-182 );yyac++; 
					yya[yyac] = new YYARec(293,-182 );yyac++; 
					yya[yyac] = new YYARec(297,-182 );yyac++; 
					yya[yyac] = new YYARec(268,-193 );yyac++; 
					yya[yyac] = new YYARec(268,-132 );yyac++; 
					yya[yyac] = new YYARec(289,-132 );yyac++; 
					yya[yyac] = new YYARec(290,-132 );yyac++; 
					yya[yyac] = new YYARec(291,-132 );yyac++; 
					yya[yyac] = new YYARec(292,-132 );yyac++; 
					yya[yyac] = new YYARec(293,-132 );yyac++; 
					yya[yyac] = new YYARec(297,-132 );yyac++; 
					yya[yyac] = new YYARec(258,-161 );yyac++; 
					yya[yyac] = new YYARec(263,-161 );yyac++; 
					yya[yyac] = new YYARec(282,-161 );yyac++; 
					yya[yyac] = new YYARec(283,-161 );yyac++; 
					yya[yyac] = new YYARec(287,-161 );yyac++; 
					yya[yyac] = new YYARec(298,-161 );yyac++; 
					yya[yyac] = new YYARec(299,-161 );yyac++; 
					yya[yyac] = new YYARec(300,-161 );yyac++; 
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
					yya[yyac] = new YYARec(325,-161 );yyac++; 
					yya[yyac] = new YYARec(263,79);yyac++; 
					yya[yyac] = new YYARec(282,-84 );yyac++; 
					yya[yyac] = new YYARec(283,-84 );yyac++; 
					yya[yyac] = new YYARec(287,-84 );yyac++; 
					yya[yyac] = new YYARec(298,-84 );yyac++; 
					yya[yyac] = new YYARec(300,-84 );yyac++; 
					yya[yyac] = new YYARec(301,-84 );yyac++; 
					yya[yyac] = new YYARec(302,-84 );yyac++; 
					yya[yyac] = new YYARec(303,-84 );yyac++; 
					yya[yyac] = new YYARec(304,-84 );yyac++; 
					yya[yyac] = new YYARec(305,-84 );yyac++; 
					yya[yyac] = new YYARec(306,-84 );yyac++; 
					yya[yyac] = new YYARec(307,-84 );yyac++; 
					yya[yyac] = new YYARec(309,-84 );yyac++; 
					yya[yyac] = new YYARec(313,-84 );yyac++; 
					yya[yyac] = new YYARec(314,-84 );yyac++; 
					yya[yyac] = new YYARec(315,-84 );yyac++; 
					yya[yyac] = new YYARec(317,-84 );yyac++; 
					yya[yyac] = new YYARec(318,-84 );yyac++; 
					yya[yyac] = new YYARec(320,-84 );yyac++; 
					yya[yyac] = new YYARec(321,-84 );yyac++; 
					yya[yyac] = new YYARec(322,-84 );yyac++; 
					yya[yyac] = new YYARec(323,-84 );yyac++; 
					yya[yyac] = new YYARec(324,-84 );yyac++; 
					yya[yyac] = new YYARec(325,-84 );yyac++; 
					yya[yyac] = new YYARec(258,199);yyac++; 
					yya[yyac] = new YYARec(267,200);yyac++; 
					yya[yyac] = new YYARec(257,143);yyac++; 
					yya[yyac] = new YYARec(259,144);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,145);yyac++; 
					yya[yyac] = new YYARec(311,146);yyac++; 
					yya[yyac] = new YYARec(312,147);yyac++; 
					yya[yyac] = new YYARec(315,148);yyac++; 
					yya[yyac] = new YYARec(317,149);yyac++; 
					yya[yyac] = new YYARec(319,150);yyac++; 
					yya[yyac] = new YYARec(320,151);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(267,-53 );yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(258,-43 );yyac++; 
					yya[yyac] = new YYARec(258,207);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(305,210);yyac++; 
					yya[yyac] = new YYARec(306,145);yyac++; 
					yya[yyac] = new YYARec(311,146);yyac++; 
					yya[yyac] = new YYARec(312,147);yyac++; 
					yya[yyac] = new YYARec(313,211);yyac++; 
					yya[yyac] = new YYARec(315,148);yyac++; 
					yya[yyac] = new YYARec(316,212);yyac++; 
					yya[yyac] = new YYARec(317,149);yyac++; 
					yya[yyac] = new YYARec(318,213);yyac++; 
					yya[yyac] = new YYARec(319,150);yyac++; 
					yya[yyac] = new YYARec(320,151);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(305,210);yyac++; 
					yya[yyac] = new YYARec(306,145);yyac++; 
					yya[yyac] = new YYARec(311,146);yyac++; 
					yya[yyac] = new YYARec(312,147);yyac++; 
					yya[yyac] = new YYARec(313,211);yyac++; 
					yya[yyac] = new YYARec(315,148);yyac++; 
					yya[yyac] = new YYARec(316,212);yyac++; 
					yya[yyac] = new YYARec(317,149);yyac++; 
					yya[yyac] = new YYARec(318,213);yyac++; 
					yya[yyac] = new YYARec(319,150);yyac++; 
					yya[yyac] = new YYARec(320,151);yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(308,113);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(322,66);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(325,69);yyac++; 
					yya[yyac] = new YYARec(257,127);yyac++; 
					yya[yyac] = new YYARec(259,128);yyac++; 
					yya[yyac] = new YYARec(266,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(294,131);yyac++; 
					yya[yyac] = new YYARec(295,132);yyac++; 
					yya[yyac] = new YYARec(296,133);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,134);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,135);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(260,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(257,127);yyac++; 
					yya[yyac] = new YYARec(259,128);yyac++; 
					yya[yyac] = new YYARec(266,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(294,131);yyac++; 
					yya[yyac] = new YYARec(295,132);yyac++; 
					yya[yyac] = new YYARec(296,133);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,134);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,135);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(260,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(258,221);yyac++; 
					yya[yyac] = new YYARec(258,222);yyac++; 
					yya[yyac] = new YYARec(267,223);yyac++; 
					yya[yyac] = new YYARec(274,224);yyac++; 
					yya[yyac] = new YYARec(258,-189 );yyac++; 
					yya[yyac] = new YYARec(266,-189 );yyac++; 
					yya[yyac] = new YYARec(269,-189 );yyac++; 
					yya[yyac] = new YYARec(270,-189 );yyac++; 
					yya[yyac] = new YYARec(271,-189 );yyac++; 
					yya[yyac] = new YYARec(272,-189 );yyac++; 
					yya[yyac] = new YYARec(273,-189 );yyac++; 
					yya[yyac] = new YYARec(275,-189 );yyac++; 
					yya[yyac] = new YYARec(276,-189 );yyac++; 
					yya[yyac] = new YYARec(277,-189 );yyac++; 
					yya[yyac] = new YYARec(278,-189 );yyac++; 
					yya[yyac] = new YYARec(279,-189 );yyac++; 
					yya[yyac] = new YYARec(280,-189 );yyac++; 
					yya[yyac] = new YYARec(281,-189 );yyac++; 
					yya[yyac] = new YYARec(282,-189 );yyac++; 
					yya[yyac] = new YYARec(283,-189 );yyac++; 
					yya[yyac] = new YYARec(284,-189 );yyac++; 
					yya[yyac] = new YYARec(285,-189 );yyac++; 
					yya[yyac] = new YYARec(286,-189 );yyac++; 
					yya[yyac] = new YYARec(289,-189 );yyac++; 
					yya[yyac] = new YYARec(290,-189 );yyac++; 
					yya[yyac] = new YYARec(291,-189 );yyac++; 
					yya[yyac] = new YYARec(292,-189 );yyac++; 
					yya[yyac] = new YYARec(293,-189 );yyac++; 
					yya[yyac] = new YYARec(297,-189 );yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(284,227);yyac++; 
					yya[yyac] = new YYARec(285,228);yyac++; 
					yya[yyac] = new YYARec(286,229);yyac++; 
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
					yya[yyac] = new YYARec(282,231);yyac++; 
					yya[yyac] = new YYARec(283,232);yyac++; 
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
					yya[yyac] = new YYARec(278,234);yyac++; 
					yya[yyac] = new YYARec(279,235);yyac++; 
					yya[yyac] = new YYARec(280,236);yyac++; 
					yya[yyac] = new YYARec(281,237);yyac++; 
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
					yya[yyac] = new YYARec(276,239);yyac++; 
					yya[yyac] = new YYARec(277,240);yyac++; 
					yya[yyac] = new YYARec(258,-95 );yyac++; 
					yya[yyac] = new YYARec(266,-95 );yyac++; 
					yya[yyac] = new YYARec(269,-95 );yyac++; 
					yya[yyac] = new YYARec(270,-95 );yyac++; 
					yya[yyac] = new YYARec(271,-95 );yyac++; 
					yya[yyac] = new YYARec(272,-95 );yyac++; 
					yya[yyac] = new YYARec(273,-95 );yyac++; 
					yya[yyac] = new YYARec(275,-95 );yyac++; 
					yya[yyac] = new YYARec(273,241);yyac++; 
					yya[yyac] = new YYARec(258,-93 );yyac++; 
					yya[yyac] = new YYARec(266,-93 );yyac++; 
					yya[yyac] = new YYARec(269,-93 );yyac++; 
					yya[yyac] = new YYARec(270,-93 );yyac++; 
					yya[yyac] = new YYARec(271,-93 );yyac++; 
					yya[yyac] = new YYARec(272,-93 );yyac++; 
					yya[yyac] = new YYARec(275,-93 );yyac++; 
					yya[yyac] = new YYARec(272,242);yyac++; 
					yya[yyac] = new YYARec(258,-91 );yyac++; 
					yya[yyac] = new YYARec(266,-91 );yyac++; 
					yya[yyac] = new YYARec(269,-91 );yyac++; 
					yya[yyac] = new YYARec(270,-91 );yyac++; 
					yya[yyac] = new YYARec(271,-91 );yyac++; 
					yya[yyac] = new YYARec(275,-91 );yyac++; 
					yya[yyac] = new YYARec(271,243);yyac++; 
					yya[yyac] = new YYARec(258,-89 );yyac++; 
					yya[yyac] = new YYARec(266,-89 );yyac++; 
					yya[yyac] = new YYARec(269,-89 );yyac++; 
					yya[yyac] = new YYARec(270,-89 );yyac++; 
					yya[yyac] = new YYARec(275,-89 );yyac++; 
					yya[yyac] = new YYARec(270,244);yyac++; 
					yya[yyac] = new YYARec(258,-87 );yyac++; 
					yya[yyac] = new YYARec(266,-87 );yyac++; 
					yya[yyac] = new YYARec(269,-87 );yyac++; 
					yya[yyac] = new YYARec(275,-87 );yyac++; 
					yya[yyac] = new YYARec(269,245);yyac++; 
					yya[yyac] = new YYARec(258,-85 );yyac++; 
					yya[yyac] = new YYARec(266,-85 );yyac++; 
					yya[yyac] = new YYARec(275,-85 );yyac++; 
					yya[yyac] = new YYARec(289,161);yyac++; 
					yya[yyac] = new YYARec(290,162);yyac++; 
					yya[yyac] = new YYARec(291,163);yyac++; 
					yya[yyac] = new YYARec(292,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(258,-109 );yyac++; 
					yya[yyac] = new YYARec(269,-109 );yyac++; 
					yya[yyac] = new YYARec(270,-109 );yyac++; 
					yya[yyac] = new YYARec(271,-109 );yyac++; 
					yya[yyac] = new YYARec(272,-109 );yyac++; 
					yya[yyac] = new YYARec(273,-109 );yyac++; 
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
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(257,127);yyac++; 
					yya[yyac] = new YYARec(259,128);yyac++; 
					yya[yyac] = new YYARec(266,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(294,131);yyac++; 
					yya[yyac] = new YYARec(295,132);yyac++; 
					yya[yyac] = new YYARec(296,133);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,134);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,135);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(266,249);yyac++; 
					yya[yyac] = new YYARec(266,250);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,259);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(322,66);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(325,69);yyac++; 
					yya[yyac] = new YYARec(257,143);yyac++; 
					yya[yyac] = new YYARec(259,144);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,145);yyac++; 
					yya[yyac] = new YYARec(311,146);yyac++; 
					yya[yyac] = new YYARec(312,147);yyac++; 
					yya[yyac] = new YYARec(315,148);yyac++; 
					yya[yyac] = new YYARec(317,149);yyac++; 
					yya[yyac] = new YYARec(319,150);yyac++; 
					yya[yyac] = new YYARec(320,151);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(267,-53 );yyac++; 
					yya[yyac] = new YYARec(258,261);yyac++; 
					yya[yyac] = new YYARec(258,262);yyac++; 
					yya[yyac] = new YYARec(321,83);yyac++; 
					yya[yyac] = new YYARec(263,263);yyac++; 
					yya[yyac] = new YYARec(258,-42 );yyac++; 
					yya[yyac] = new YYARec(258,264);yyac++; 
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
					yya[yyac] = new YYARec(319,30);yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(263,79);yyac++; 
					yya[yyac] = new YYARec(258,-84 );yyac++; 
					yya[yyac] = new YYARec(282,-84 );yyac++; 
					yya[yyac] = new YYARec(283,-84 );yyac++; 
					yya[yyac] = new YYARec(287,-84 );yyac++; 
					yya[yyac] = new YYARec(298,-84 );yyac++; 
					yya[yyac] = new YYARec(299,-84 );yyac++; 
					yya[yyac] = new YYARec(300,-84 );yyac++; 
					yya[yyac] = new YYARec(301,-84 );yyac++; 
					yya[yyac] = new YYARec(302,-84 );yyac++; 
					yya[yyac] = new YYARec(304,-84 );yyac++; 
					yya[yyac] = new YYARec(305,-84 );yyac++; 
					yya[yyac] = new YYARec(306,-84 );yyac++; 
					yya[yyac] = new YYARec(307,-84 );yyac++; 
					yya[yyac] = new YYARec(308,-84 );yyac++; 
					yya[yyac] = new YYARec(309,-84 );yyac++; 
					yya[yyac] = new YYARec(310,-84 );yyac++; 
					yya[yyac] = new YYARec(312,-84 );yyac++; 
					yya[yyac] = new YYARec(313,-84 );yyac++; 
					yya[yyac] = new YYARec(314,-84 );yyac++; 
					yya[yyac] = new YYARec(315,-84 );yyac++; 
					yya[yyac] = new YYARec(316,-84 );yyac++; 
					yya[yyac] = new YYARec(317,-84 );yyac++; 
					yya[yyac] = new YYARec(318,-84 );yyac++; 
					yya[yyac] = new YYARec(319,-84 );yyac++; 
					yya[yyac] = new YYARec(320,-84 );yyac++; 
					yya[yyac] = new YYARec(321,-84 );yyac++; 
					yya[yyac] = new YYARec(322,-84 );yyac++; 
					yya[yyac] = new YYARec(323,-84 );yyac++; 
					yya[yyac] = new YYARec(324,-84 );yyac++; 
					yya[yyac] = new YYARec(325,-84 );yyac++; 
					yya[yyac] = new YYARec(257,127);yyac++; 
					yya[yyac] = new YYARec(259,128);yyac++; 
					yya[yyac] = new YYARec(266,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(294,131);yyac++; 
					yya[yyac] = new YYARec(295,132);yyac++; 
					yya[yyac] = new YYARec(296,133);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,134);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,135);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(260,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(257,127);yyac++; 
					yya[yyac] = new YYARec(259,128);yyac++; 
					yya[yyac] = new YYARec(266,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(294,131);yyac++; 
					yya[yyac] = new YYARec(295,132);yyac++; 
					yya[yyac] = new YYARec(296,133);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,134);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,135);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(260,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(257,127);yyac++; 
					yya[yyac] = new YYARec(259,128);yyac++; 
					yya[yyac] = new YYARec(266,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(294,131);yyac++; 
					yya[yyac] = new YYARec(295,132);yyac++; 
					yya[yyac] = new YYARec(296,133);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,134);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,135);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(260,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,191);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,192);yyac++; 
					yya[yyac] = new YYARec(322,193);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(275,282);yyac++; 
					yya[yyac] = new YYARec(267,283);yyac++; 
					yya[yyac] = new YYARec(257,127);yyac++; 
					yya[yyac] = new YYARec(259,128);yyac++; 
					yya[yyac] = new YYARec(266,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(294,131);yyac++; 
					yya[yyac] = new YYARec(295,132);yyac++; 
					yya[yyac] = new YYARec(296,133);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,134);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,135);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(257,127);yyac++; 
					yya[yyac] = new YYARec(259,128);yyac++; 
					yya[yyac] = new YYARec(266,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(294,131);yyac++; 
					yya[yyac] = new YYARec(295,132);yyac++; 
					yya[yyac] = new YYARec(296,133);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,134);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,135);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(263,79);yyac++; 
					yya[yyac] = new YYARec(258,-84 );yyac++; 
					yya[yyac] = new YYARec(282,-84 );yyac++; 
					yya[yyac] = new YYARec(283,-84 );yyac++; 
					yya[yyac] = new YYARec(287,-84 );yyac++; 
					yya[yyac] = new YYARec(298,-84 );yyac++; 
					yya[yyac] = new YYARec(300,-84 );yyac++; 
					yya[yyac] = new YYARec(301,-84 );yyac++; 
					yya[yyac] = new YYARec(302,-84 );yyac++; 
					yya[yyac] = new YYARec(303,-84 );yyac++; 
					yya[yyac] = new YYARec(304,-84 );yyac++; 
					yya[yyac] = new YYARec(305,-84 );yyac++; 
					yya[yyac] = new YYARec(306,-84 );yyac++; 
					yya[yyac] = new YYARec(307,-84 );yyac++; 
					yya[yyac] = new YYARec(309,-84 );yyac++; 
					yya[yyac] = new YYARec(313,-84 );yyac++; 
					yya[yyac] = new YYARec(314,-84 );yyac++; 
					yya[yyac] = new YYARec(315,-84 );yyac++; 
					yya[yyac] = new YYARec(317,-84 );yyac++; 
					yya[yyac] = new YYARec(318,-84 );yyac++; 
					yya[yyac] = new YYARec(320,-84 );yyac++; 
					yya[yyac] = new YYARec(321,-84 );yyac++; 
					yya[yyac] = new YYARec(322,-84 );yyac++; 
					yya[yyac] = new YYARec(323,-84 );yyac++; 
					yya[yyac] = new YYARec(324,-84 );yyac++; 
					yya[yyac] = new YYARec(325,-84 );yyac++; 
					yya[yyac] = new YYARec(297,287);yyac++; 
					yya[yyac] = new YYARec(258,-63 );yyac++; 
					yya[yyac] = new YYARec(263,-63 );yyac++; 
					yya[yyac] = new YYARec(282,-63 );yyac++; 
					yya[yyac] = new YYARec(283,-63 );yyac++; 
					yya[yyac] = new YYARec(287,-63 );yyac++; 
					yya[yyac] = new YYARec(298,-63 );yyac++; 
					yya[yyac] = new YYARec(300,-63 );yyac++; 
					yya[yyac] = new YYARec(301,-63 );yyac++; 
					yya[yyac] = new YYARec(302,-63 );yyac++; 
					yya[yyac] = new YYARec(303,-63 );yyac++; 
					yya[yyac] = new YYARec(304,-63 );yyac++; 
					yya[yyac] = new YYARec(305,-63 );yyac++; 
					yya[yyac] = new YYARec(306,-63 );yyac++; 
					yya[yyac] = new YYARec(307,-63 );yyac++; 
					yya[yyac] = new YYARec(309,-63 );yyac++; 
					yya[yyac] = new YYARec(313,-63 );yyac++; 
					yya[yyac] = new YYARec(314,-63 );yyac++; 
					yya[yyac] = new YYARec(315,-63 );yyac++; 
					yya[yyac] = new YYARec(317,-63 );yyac++; 
					yya[yyac] = new YYARec(318,-63 );yyac++; 
					yya[yyac] = new YYARec(320,-63 );yyac++; 
					yya[yyac] = new YYARec(321,-63 );yyac++; 
					yya[yyac] = new YYARec(322,-63 );yyac++; 
					yya[yyac] = new YYARec(323,-63 );yyac++; 
					yya[yyac] = new YYARec(324,-63 );yyac++; 
					yya[yyac] = new YYARec(325,-63 );yyac++; 
					yya[yyac] = new YYARec(257,143);yyac++; 
					yya[yyac] = new YYARec(259,144);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,145);yyac++; 
					yya[yyac] = new YYARec(311,146);yyac++; 
					yya[yyac] = new YYARec(312,147);yyac++; 
					yya[yyac] = new YYARec(315,148);yyac++; 
					yya[yyac] = new YYARec(317,149);yyac++; 
					yya[yyac] = new YYARec(319,150);yyac++; 
					yya[yyac] = new YYARec(320,151);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(257,143);yyac++; 
					yya[yyac] = new YYARec(259,144);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,145);yyac++; 
					yya[yyac] = new YYARec(311,146);yyac++; 
					yya[yyac] = new YYARec(312,147);yyac++; 
					yya[yyac] = new YYARec(315,148);yyac++; 
					yya[yyac] = new YYARec(317,149);yyac++; 
					yya[yyac] = new YYARec(319,150);yyac++; 
					yya[yyac] = new YYARec(320,151);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(258,-43 );yyac++; 
					yya[yyac] = new YYARec(261,292);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(308,113);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(322,66);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(325,69);yyac++; 
					yya[yyac] = new YYARec(258,-76 );yyac++; 
					yya[yyac] = new YYARec(260,294);yyac++; 
					yya[yyac] = new YYARec(261,295);yyac++; 
					yya[yyac] = new YYARec(258,296);yyac++; 
					yya[yyac] = new YYARec(258,297);yyac++; 
					yya[yyac] = new YYARec(275,298);yyac++; 
					yya[yyac] = new YYARec(284,227);yyac++; 
					yya[yyac] = new YYARec(285,228);yyac++; 
					yya[yyac] = new YYARec(286,229);yyac++; 
					yya[yyac] = new YYARec(258,-101 );yyac++; 
					yya[yyac] = new YYARec(266,-101 );yyac++; 
					yya[yyac] = new YYARec(269,-101 );yyac++; 
					yya[yyac] = new YYARec(270,-101 );yyac++; 
					yya[yyac] = new YYARec(271,-101 );yyac++; 
					yya[yyac] = new YYARec(272,-101 );yyac++; 
					yya[yyac] = new YYARec(273,-101 );yyac++; 
					yya[yyac] = new YYARec(275,-101 );yyac++; 
					yya[yyac] = new YYARec(276,-101 );yyac++; 
					yya[yyac] = new YYARec(277,-101 );yyac++; 
					yya[yyac] = new YYARec(278,-101 );yyac++; 
					yya[yyac] = new YYARec(279,-101 );yyac++; 
					yya[yyac] = new YYARec(280,-101 );yyac++; 
					yya[yyac] = new YYARec(281,-101 );yyac++; 
					yya[yyac] = new YYARec(282,-101 );yyac++; 
					yya[yyac] = new YYARec(283,-101 );yyac++; 
					yya[yyac] = new YYARec(282,231);yyac++; 
					yya[yyac] = new YYARec(283,232);yyac++; 
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
					yya[yyac] = new YYARec(278,234);yyac++; 
					yya[yyac] = new YYARec(279,235);yyac++; 
					yya[yyac] = new YYARec(280,236);yyac++; 
					yya[yyac] = new YYARec(281,237);yyac++; 
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
					yya[yyac] = new YYARec(276,239);yyac++; 
					yya[yyac] = new YYARec(277,240);yyac++; 
					yya[yyac] = new YYARec(258,-94 );yyac++; 
					yya[yyac] = new YYARec(266,-94 );yyac++; 
					yya[yyac] = new YYARec(269,-94 );yyac++; 
					yya[yyac] = new YYARec(270,-94 );yyac++; 
					yya[yyac] = new YYARec(271,-94 );yyac++; 
					yya[yyac] = new YYARec(272,-94 );yyac++; 
					yya[yyac] = new YYARec(273,-94 );yyac++; 
					yya[yyac] = new YYARec(275,-94 );yyac++; 
					yya[yyac] = new YYARec(273,241);yyac++; 
					yya[yyac] = new YYARec(258,-92 );yyac++; 
					yya[yyac] = new YYARec(266,-92 );yyac++; 
					yya[yyac] = new YYARec(269,-92 );yyac++; 
					yya[yyac] = new YYARec(270,-92 );yyac++; 
					yya[yyac] = new YYARec(271,-92 );yyac++; 
					yya[yyac] = new YYARec(272,-92 );yyac++; 
					yya[yyac] = new YYARec(275,-92 );yyac++; 
					yya[yyac] = new YYARec(272,242);yyac++; 
					yya[yyac] = new YYARec(258,-90 );yyac++; 
					yya[yyac] = new YYARec(266,-90 );yyac++; 
					yya[yyac] = new YYARec(269,-90 );yyac++; 
					yya[yyac] = new YYARec(270,-90 );yyac++; 
					yya[yyac] = new YYARec(271,-90 );yyac++; 
					yya[yyac] = new YYARec(275,-90 );yyac++; 
					yya[yyac] = new YYARec(271,243);yyac++; 
					yya[yyac] = new YYARec(258,-88 );yyac++; 
					yya[yyac] = new YYARec(266,-88 );yyac++; 
					yya[yyac] = new YYARec(269,-88 );yyac++; 
					yya[yyac] = new YYARec(270,-88 );yyac++; 
					yya[yyac] = new YYARec(275,-88 );yyac++; 
					yya[yyac] = new YYARec(270,244);yyac++; 
					yya[yyac] = new YYARec(258,-86 );yyac++; 
					yya[yyac] = new YYARec(266,-86 );yyac++; 
					yya[yyac] = new YYARec(269,-86 );yyac++; 
					yya[yyac] = new YYARec(275,-86 );yyac++; 
					yya[yyac] = new YYARec(267,299);yyac++; 
					yya[yyac] = new YYARec(267,300);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,259);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(322,66);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(325,69);yyac++; 
					yya[yyac] = new YYARec(258,-55 );yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,145);yyac++; 
					yya[yyac] = new YYARec(311,146);yyac++; 
					yya[yyac] = new YYARec(312,147);yyac++; 
					yya[yyac] = new YYARec(315,148);yyac++; 
					yya[yyac] = new YYARec(317,149);yyac++; 
					yya[yyac] = new YYARec(319,150);yyac++; 
					yya[yyac] = new YYARec(320,151);yyac++; 
					yya[yyac] = new YYARec(260,303);yyac++; 
					yya[yyac] = new YYARec(261,304);yyac++; 
					yya[yyac] = new YYARec(258,305);yyac++; 
					yya[yyac] = new YYARec(258,306);yyac++; 
					yya[yyac] = new YYARec(258,307);yyac++; 
					yya[yyac] = new YYARec(258,308);yyac++; 
					yya[yyac] = new YYARec(257,127);yyac++; 
					yya[yyac] = new YYARec(259,128);yyac++; 
					yya[yyac] = new YYARec(266,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(294,131);yyac++; 
					yya[yyac] = new YYARec(295,132);yyac++; 
					yya[yyac] = new YYARec(296,133);yyac++; 
					yya[yyac] = new YYARec(298,112);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,134);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,135);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,115);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,116);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(257,143);yyac++; 
					yya[yyac] = new YYARec(259,144);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,145);yyac++; 
					yya[yyac] = new YYARec(311,146);yyac++; 
					yya[yyac] = new YYARec(312,147);yyac++; 
					yya[yyac] = new YYARec(315,148);yyac++; 
					yya[yyac] = new YYARec(317,149);yyac++; 
					yya[yyac] = new YYARec(319,150);yyac++; 
					yya[yyac] = new YYARec(320,151);yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(261,311);yyac++; 
					yya[yyac] = new YYARec(261,312);yyac++;

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
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-41,34);yygc++; 
					yyg[yygc] = new YYARec(-35,35);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,37);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,50);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-31,51);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,52);yygc++; 
					yyg[yygc] = new YYARec(-73,54);yygc++; 
					yyg[yygc] = new YYARec(-66,55);yygc++; 
					yyg[yygc] = new YYARec(-33,56);yygc++; 
					yyg[yygc] = new YYARec(-29,57);yygc++; 
					yyg[yygc] = new YYARec(-28,58);yygc++; 
					yyg[yygc] = new YYARec(-25,59);yygc++; 
					yyg[yygc] = new YYARec(-21,60);yygc++; 
					yyg[yygc] = new YYARec(-12,61);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,70);yygc++; 
					yyg[yygc] = new YYARec(-23,71);yygc++; 
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
					yyg[yygc] = new YYARec(-3,72);yygc++; 
					yyg[yygc] = new YYARec(-12,73);yygc++; 
					yyg[yygc] = new YYARec(-12,74);yygc++; 
					yyg[yygc] = new YYARec(-12,75);yygc++; 
					yyg[yygc] = new YYARec(-12,76);yygc++; 
					yyg[yygc] = new YYARec(-21,77);yygc++; 
					yyg[yygc] = new YYARec(-27,78);yygc++; 
					yyg[yygc] = new YYARec(-27,80);yygc++; 
					yyg[yygc] = new YYARec(-27,82);yygc++; 
					yyg[yygc] = new YYARec(-27,86);yygc++; 
					yyg[yygc] = new YYARec(-28,95);yygc++; 
					yyg[yygc] = new YYARec(-21,97);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,70);yygc++; 
					yyg[yygc] = new YYARec(-23,98);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,3);yygc++; 
					yyg[yygc] = new YYARec(-30,4);yygc++; 
					yyg[yygc] = new YYARec(-24,5);yygc++; 
					yyg[yygc] = new YYARec(-22,6);yygc++; 
					yyg[yygc] = new YYARec(-13,99);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,100);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,3);yygc++; 
					yyg[yygc] = new YYARec(-30,4);yygc++; 
					yyg[yygc] = new YYARec(-24,5);yygc++; 
					yyg[yygc] = new YYARec(-22,6);yygc++; 
					yyg[yygc] = new YYARec(-13,101);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,100);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-73,54);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-66,55);yygc++; 
					yyg[yygc] = new YYARec(-49,103);yygc++; 
					yyg[yygc] = new YYARec(-48,104);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-33,56);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-29,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-21,110);yygc++; 
					yyg[yygc] = new YYARec(-20,111);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,117);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-49,118);yygc++; 
					yyg[yygc] = new YYARec(-46,119);yygc++; 
					yyg[yygc] = new YYARec(-45,120);yygc++; 
					yyg[yygc] = new YYARec(-44,121);yygc++; 
					yyg[yygc] = new YYARec(-43,122);yygc++; 
					yyg[yygc] = new YYARec(-42,123);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,124);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-16,125);yygc++; 
					yyg[yygc] = new YYARec(-14,126);yygc++; 
					yyg[yygc] = new YYARec(-37,137);yygc++; 
					yyg[yygc] = new YYARec(-36,138);yygc++; 
					yyg[yygc] = new YYARec(-35,139);yygc++; 
					yyg[yygc] = new YYARec(-30,140);yygc++; 
					yyg[yygc] = new YYARec(-19,141);yygc++; 
					yyg[yygc] = new YYARec(-17,142);yygc++; 
					yyg[yygc] = new YYARec(-27,152);yygc++; 
					yyg[yygc] = new YYARec(-69,160);yygc++; 
					yyg[yygc] = new YYARec(-27,166);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,117);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-49,118);yygc++; 
					yyg[yygc] = new YYARec(-46,119);yygc++; 
					yyg[yygc] = new YYARec(-45,120);yygc++; 
					yyg[yygc] = new YYARec(-44,121);yygc++; 
					yyg[yygc] = new YYARec(-43,122);yygc++; 
					yyg[yygc] = new YYARec(-42,123);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,124);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-16,167);yygc++; 
					yyg[yygc] = new YYARec(-14,126);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,117);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-49,118);yygc++; 
					yyg[yygc] = new YYARec(-46,119);yygc++; 
					yyg[yygc] = new YYARec(-45,120);yygc++; 
					yyg[yygc] = new YYARec(-44,121);yygc++; 
					yyg[yygc] = new YYARec(-43,122);yygc++; 
					yyg[yygc] = new YYARec(-42,123);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,124);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-16,171);yygc++; 
					yyg[yygc] = new YYARec(-14,126);yygc++; 
					yyg[yygc] = new YYARec(-12,172);yygc++; 
					yyg[yygc] = new YYARec(-12,173);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,117);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-49,118);yygc++; 
					yyg[yygc] = new YYARec(-46,119);yygc++; 
					yyg[yygc] = new YYARec(-45,120);yygc++; 
					yyg[yygc] = new YYARec(-44,121);yygc++; 
					yyg[yygc] = new YYARec(-43,122);yygc++; 
					yyg[yygc] = new YYARec(-42,123);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,124);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-16,174);yygc++; 
					yyg[yygc] = new YYARec(-14,126);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,179);yygc++; 
					yyg[yygc] = new YYARec(-61,180);yygc++; 
					yyg[yygc] = new YYARec(-59,181);yygc++; 
					yyg[yygc] = new YYARec(-57,182);yygc++; 
					yyg[yygc] = new YYARec(-56,183);yygc++; 
					yyg[yygc] = new YYARec(-55,184);yygc++; 
					yyg[yygc] = new YYARec(-54,185);yygc++; 
					yyg[yygc] = new YYARec(-53,186);yygc++; 
					yyg[yygc] = new YYARec(-52,187);yygc++; 
					yyg[yygc] = new YYARec(-51,188);yygc++; 
					yyg[yygc] = new YYARec(-50,189);yygc++; 
					yyg[yygc] = new YYARec(-49,190);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,179);yygc++; 
					yyg[yygc] = new YYARec(-61,180);yygc++; 
					yyg[yygc] = new YYARec(-59,181);yygc++; 
					yyg[yygc] = new YYARec(-57,182);yygc++; 
					yyg[yygc] = new YYARec(-56,183);yygc++; 
					yyg[yygc] = new YYARec(-55,184);yygc++; 
					yyg[yygc] = new YYARec(-54,185);yygc++; 
					yyg[yygc] = new YYARec(-53,186);yygc++; 
					yyg[yygc] = new YYARec(-52,187);yygc++; 
					yyg[yygc] = new YYARec(-51,188);yygc++; 
					yyg[yygc] = new YYARec(-50,195);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,179);yygc++; 
					yyg[yygc] = new YYARec(-61,180);yygc++; 
					yyg[yygc] = new YYARec(-59,181);yygc++; 
					yyg[yygc] = new YYARec(-57,182);yygc++; 
					yyg[yygc] = new YYARec(-56,183);yygc++; 
					yyg[yygc] = new YYARec(-55,184);yygc++; 
					yyg[yygc] = new YYARec(-54,185);yygc++; 
					yyg[yygc] = new YYARec(-53,186);yygc++; 
					yyg[yygc] = new YYARec(-52,187);yygc++; 
					yyg[yygc] = new YYARec(-51,188);yygc++; 
					yyg[yygc] = new YYARec(-50,197);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-27,198);yygc++; 
					yyg[yygc] = new YYARec(-37,137);yygc++; 
					yyg[yygc] = new YYARec(-36,138);yygc++; 
					yyg[yygc] = new YYARec(-35,139);yygc++; 
					yyg[yygc] = new YYARec(-30,140);yygc++; 
					yyg[yygc] = new YYARec(-19,201);yygc++; 
					yyg[yygc] = new YYARec(-17,142);yygc++; 
					yyg[yygc] = new YYARec(-12,202);yygc++; 
					yyg[yygc] = new YYARec(-12,203);yygc++; 
					yyg[yygc] = new YYARec(-66,204);yygc++; 
					yyg[yygc] = new YYARec(-33,205);yygc++; 
					yyg[yygc] = new YYARec(-32,206);yygc++; 
					yyg[yygc] = new YYARec(-70,208);yygc++; 
					yyg[yygc] = new YYARec(-37,209);yygc++; 
					yyg[yygc] = new YYARec(-35,139);yygc++; 
					yyg[yygc] = new YYARec(-30,140);yygc++; 
					yyg[yygc] = new YYARec(-70,214);yygc++; 
					yyg[yygc] = new YYARec(-37,215);yygc++; 
					yyg[yygc] = new YYARec(-35,139);yygc++; 
					yyg[yygc] = new YYARec(-30,140);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,179);yygc++; 
					yyg[yygc] = new YYARec(-61,180);yygc++; 
					yyg[yygc] = new YYARec(-59,181);yygc++; 
					yyg[yygc] = new YYARec(-57,182);yygc++; 
					yyg[yygc] = new YYARec(-56,183);yygc++; 
					yyg[yygc] = new YYARec(-55,184);yygc++; 
					yyg[yygc] = new YYARec(-54,185);yygc++; 
					yyg[yygc] = new YYARec(-53,186);yygc++; 
					yyg[yygc] = new YYARec(-52,187);yygc++; 
					yyg[yygc] = new YYARec(-51,188);yygc++; 
					yyg[yygc] = new YYARec(-50,216);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-73,54);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-66,55);yygc++; 
					yyg[yygc] = new YYARec(-49,103);yygc++; 
					yyg[yygc] = new YYARec(-48,104);yygc++; 
					yyg[yygc] = new YYARec(-47,217);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-33,56);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-29,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-21,110);yygc++; 
					yyg[yygc] = new YYARec(-20,218);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,117);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-49,118);yygc++; 
					yyg[yygc] = new YYARec(-46,119);yygc++; 
					yyg[yygc] = new YYARec(-45,120);yygc++; 
					yyg[yygc] = new YYARec(-44,121);yygc++; 
					yyg[yygc] = new YYARec(-43,122);yygc++; 
					yyg[yygc] = new YYARec(-42,123);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,124);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-16,219);yygc++; 
					yyg[yygc] = new YYARec(-14,126);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,117);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-49,118);yygc++; 
					yyg[yygc] = new YYARec(-46,119);yygc++; 
					yyg[yygc] = new YYARec(-45,120);yygc++; 
					yyg[yygc] = new YYARec(-44,121);yygc++; 
					yyg[yygc] = new YYARec(-43,122);yygc++; 
					yyg[yygc] = new YYARec(-42,123);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,124);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-16,220);yygc++; 
					yyg[yygc] = new YYARec(-14,126);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,225);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-64,226);yygc++; 
					yyg[yygc] = new YYARec(-62,230);yygc++; 
					yyg[yygc] = new YYARec(-60,233);yygc++; 
					yyg[yygc] = new YYARec(-58,238);yygc++; 
					yyg[yygc] = new YYARec(-69,246);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,179);yygc++; 
					yyg[yygc] = new YYARec(-61,180);yygc++; 
					yyg[yygc] = new YYARec(-59,181);yygc++; 
					yyg[yygc] = new YYARec(-57,182);yygc++; 
					yyg[yygc] = new YYARec(-56,183);yygc++; 
					yyg[yygc] = new YYARec(-55,184);yygc++; 
					yyg[yygc] = new YYARec(-54,185);yygc++; 
					yyg[yygc] = new YYARec(-53,186);yygc++; 
					yyg[yygc] = new YYARec(-52,187);yygc++; 
					yyg[yygc] = new YYARec(-51,188);yygc++; 
					yyg[yygc] = new YYARec(-50,247);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,117);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-49,118);yygc++; 
					yyg[yygc] = new YYARec(-46,119);yygc++; 
					yyg[yygc] = new YYARec(-45,120);yygc++; 
					yyg[yygc] = new YYARec(-44,121);yygc++; 
					yyg[yygc] = new YYARec(-43,122);yygc++; 
					yyg[yygc] = new YYARec(-42,123);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,124);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-16,248);yygc++; 
					yyg[yygc] = new YYARec(-14,126);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-73,54);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-66,55);yygc++; 
					yyg[yygc] = new YYARec(-40,251);yygc++; 
					yyg[yygc] = new YYARec(-39,252);yygc++; 
					yyg[yygc] = new YYARec(-38,253);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,254);yygc++; 
					yyg[yygc] = new YYARec(-33,56);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-29,255);yygc++; 
					yyg[yygc] = new YYARec(-28,256);yygc++; 
					yyg[yygc] = new YYARec(-26,257);yygc++; 
					yyg[yygc] = new YYARec(-21,258);yygc++; 
					yyg[yygc] = new YYARec(-37,137);yygc++; 
					yyg[yygc] = new YYARec(-36,138);yygc++; 
					yyg[yygc] = new YYARec(-35,139);yygc++; 
					yyg[yygc] = new YYARec(-30,140);yygc++; 
					yyg[yygc] = new YYARec(-19,260);yygc++; 
					yyg[yygc] = new YYARec(-17,142);yygc++; 
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
					yyg[yygc] = new YYARec(-3,265);yygc++; 
					yyg[yygc] = new YYARec(-27,266);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,117);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-49,118);yygc++; 
					yyg[yygc] = new YYARec(-46,119);yygc++; 
					yyg[yygc] = new YYARec(-45,120);yygc++; 
					yyg[yygc] = new YYARec(-44,121);yygc++; 
					yyg[yygc] = new YYARec(-43,122);yygc++; 
					yyg[yygc] = new YYARec(-42,123);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,124);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-16,267);yygc++; 
					yyg[yygc] = new YYARec(-15,268);yygc++; 
					yyg[yygc] = new YYARec(-14,126);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,117);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-49,118);yygc++; 
					yyg[yygc] = new YYARec(-46,119);yygc++; 
					yyg[yygc] = new YYARec(-45,120);yygc++; 
					yyg[yygc] = new YYARec(-44,121);yygc++; 
					yyg[yygc] = new YYARec(-43,122);yygc++; 
					yyg[yygc] = new YYARec(-42,123);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,124);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-16,267);yygc++; 
					yyg[yygc] = new YYARec(-15,269);yygc++; 
					yyg[yygc] = new YYARec(-14,126);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,117);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-49,118);yygc++; 
					yyg[yygc] = new YYARec(-46,119);yygc++; 
					yyg[yygc] = new YYARec(-45,120);yygc++; 
					yyg[yygc] = new YYARec(-44,121);yygc++; 
					yyg[yygc] = new YYARec(-43,122);yygc++; 
					yyg[yygc] = new YYARec(-42,123);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,124);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-16,270);yygc++; 
					yyg[yygc] = new YYARec(-14,126);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,179);yygc++; 
					yyg[yygc] = new YYARec(-61,180);yygc++; 
					yyg[yygc] = new YYARec(-59,181);yygc++; 
					yyg[yygc] = new YYARec(-57,182);yygc++; 
					yyg[yygc] = new YYARec(-56,183);yygc++; 
					yyg[yygc] = new YYARec(-55,184);yygc++; 
					yyg[yygc] = new YYARec(-54,185);yygc++; 
					yyg[yygc] = new YYARec(-53,186);yygc++; 
					yyg[yygc] = new YYARec(-52,187);yygc++; 
					yyg[yygc] = new YYARec(-51,188);yygc++; 
					yyg[yygc] = new YYARec(-50,271);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,272);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,179);yygc++; 
					yyg[yygc] = new YYARec(-61,273);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,179);yygc++; 
					yyg[yygc] = new YYARec(-61,180);yygc++; 
					yyg[yygc] = new YYARec(-59,274);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,179);yygc++; 
					yyg[yygc] = new YYARec(-61,180);yygc++; 
					yyg[yygc] = new YYARec(-59,181);yygc++; 
					yyg[yygc] = new YYARec(-57,275);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,179);yygc++; 
					yyg[yygc] = new YYARec(-61,180);yygc++; 
					yyg[yygc] = new YYARec(-59,181);yygc++; 
					yyg[yygc] = new YYARec(-57,182);yygc++; 
					yyg[yygc] = new YYARec(-56,276);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,179);yygc++; 
					yyg[yygc] = new YYARec(-61,180);yygc++; 
					yyg[yygc] = new YYARec(-59,181);yygc++; 
					yyg[yygc] = new YYARec(-57,182);yygc++; 
					yyg[yygc] = new YYARec(-56,183);yygc++; 
					yyg[yygc] = new YYARec(-55,277);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,179);yygc++; 
					yyg[yygc] = new YYARec(-61,180);yygc++; 
					yyg[yygc] = new YYARec(-59,181);yygc++; 
					yyg[yygc] = new YYARec(-57,182);yygc++; 
					yyg[yygc] = new YYARec(-56,183);yygc++; 
					yyg[yygc] = new YYARec(-55,184);yygc++; 
					yyg[yygc] = new YYARec(-54,278);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,179);yygc++; 
					yyg[yygc] = new YYARec(-61,180);yygc++; 
					yyg[yygc] = new YYARec(-59,181);yygc++; 
					yyg[yygc] = new YYARec(-57,182);yygc++; 
					yyg[yygc] = new YYARec(-56,183);yygc++; 
					yyg[yygc] = new YYARec(-55,184);yygc++; 
					yyg[yygc] = new YYARec(-54,185);yygc++; 
					yyg[yygc] = new YYARec(-53,279);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,179);yygc++; 
					yyg[yygc] = new YYARec(-61,180);yygc++; 
					yyg[yygc] = new YYARec(-59,181);yygc++; 
					yyg[yygc] = new YYARec(-57,182);yygc++; 
					yyg[yygc] = new YYARec(-56,183);yygc++; 
					yyg[yygc] = new YYARec(-55,184);yygc++; 
					yyg[yygc] = new YYARec(-54,185);yygc++; 
					yyg[yygc] = new YYARec(-53,186);yygc++; 
					yyg[yygc] = new YYARec(-52,280);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-68,175);yygc++; 
					yyg[yygc] = new YYARec(-67,176);yygc++; 
					yyg[yygc] = new YYARec(-66,177);yygc++; 
					yyg[yygc] = new YYARec(-65,178);yygc++; 
					yyg[yygc] = new YYARec(-63,179);yygc++; 
					yyg[yygc] = new YYARec(-61,180);yygc++; 
					yyg[yygc] = new YYARec(-59,181);yygc++; 
					yyg[yygc] = new YYARec(-57,182);yygc++; 
					yyg[yygc] = new YYARec(-56,183);yygc++; 
					yyg[yygc] = new YYARec(-55,184);yygc++; 
					yyg[yygc] = new YYARec(-54,185);yygc++; 
					yyg[yygc] = new YYARec(-53,186);yygc++; 
					yyg[yygc] = new YYARec(-52,187);yygc++; 
					yyg[yygc] = new YYARec(-51,188);yygc++; 
					yyg[yygc] = new YYARec(-50,281);yygc++; 
					yyg[yygc] = new YYARec(-49,196);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,117);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-49,118);yygc++; 
					yyg[yygc] = new YYARec(-46,119);yygc++; 
					yyg[yygc] = new YYARec(-45,120);yygc++; 
					yyg[yygc] = new YYARec(-44,121);yygc++; 
					yyg[yygc] = new YYARec(-43,122);yygc++; 
					yyg[yygc] = new YYARec(-42,123);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,124);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-16,284);yygc++; 
					yyg[yygc] = new YYARec(-14,126);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,117);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-49,118);yygc++; 
					yyg[yygc] = new YYARec(-46,119);yygc++; 
					yyg[yygc] = new YYARec(-45,120);yygc++; 
					yyg[yygc] = new YYARec(-44,121);yygc++; 
					yyg[yygc] = new YYARec(-43,122);yygc++; 
					yyg[yygc] = new YYARec(-42,123);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,124);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-16,285);yygc++; 
					yyg[yygc] = new YYARec(-14,126);yygc++; 
					yyg[yygc] = new YYARec(-27,286);yygc++; 
					yyg[yygc] = new YYARec(-37,137);yygc++; 
					yyg[yygc] = new YYARec(-36,138);yygc++; 
					yyg[yygc] = new YYARec(-35,139);yygc++; 
					yyg[yygc] = new YYARec(-30,140);yygc++; 
					yyg[yygc] = new YYARec(-19,288);yygc++; 
					yyg[yygc] = new YYARec(-18,289);yygc++; 
					yyg[yygc] = new YYARec(-17,142);yygc++; 
					yyg[yygc] = new YYARec(-37,137);yygc++; 
					yyg[yygc] = new YYARec(-36,138);yygc++; 
					yyg[yygc] = new YYARec(-35,139);yygc++; 
					yyg[yygc] = new YYARec(-30,140);yygc++; 
					yyg[yygc] = new YYARec(-19,288);yygc++; 
					yyg[yygc] = new YYARec(-18,290);yygc++; 
					yyg[yygc] = new YYARec(-17,142);yygc++; 
					yyg[yygc] = new YYARec(-66,204);yygc++; 
					yyg[yygc] = new YYARec(-33,205);yygc++; 
					yyg[yygc] = new YYARec(-32,291);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-73,54);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-66,55);yygc++; 
					yyg[yygc] = new YYARec(-49,103);yygc++; 
					yyg[yygc] = new YYARec(-48,104);yygc++; 
					yyg[yygc] = new YYARec(-47,293);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-33,56);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-29,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-26,108);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-21,110);yygc++; 
					yyg[yygc] = new YYARec(-20,218);yygc++; 
					yyg[yygc] = new YYARec(-64,226);yygc++; 
					yyg[yygc] = new YYARec(-62,230);yygc++; 
					yyg[yygc] = new YYARec(-60,233);yygc++; 
					yyg[yygc] = new YYARec(-58,238);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-73,54);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-66,55);yygc++; 
					yyg[yygc] = new YYARec(-40,251);yygc++; 
					yyg[yygc] = new YYARec(-39,252);yygc++; 
					yyg[yygc] = new YYARec(-38,301);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,254);yygc++; 
					yyg[yygc] = new YYARec(-33,56);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-29,255);yygc++; 
					yyg[yygc] = new YYARec(-28,256);yygc++; 
					yyg[yygc] = new YYARec(-26,257);yygc++; 
					yyg[yygc] = new YYARec(-21,258);yygc++; 
					yyg[yygc] = new YYARec(-37,302);yygc++; 
					yyg[yygc] = new YYARec(-35,139);yygc++; 
					yyg[yygc] = new YYARec(-30,140);yygc++; 
					yyg[yygc] = new YYARec(-74,31);yygc++; 
					yyg[yygc] = new YYARec(-72,117);yygc++; 
					yyg[yygc] = new YYARec(-71,102);yygc++; 
					yyg[yygc] = new YYARec(-67,33);yygc++; 
					yyg[yygc] = new YYARec(-49,118);yygc++; 
					yyg[yygc] = new YYARec(-46,119);yygc++; 
					yyg[yygc] = new YYARec(-45,120);yygc++; 
					yyg[yygc] = new YYARec(-44,121);yygc++; 
					yyg[yygc] = new YYARec(-43,122);yygc++; 
					yyg[yygc] = new YYARec(-42,123);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,105);yygc++; 
					yyg[yygc] = new YYARec(-30,36);yygc++; 
					yyg[yygc] = new YYARec(-26,124);yygc++; 
					yyg[yygc] = new YYARec(-22,109);yygc++; 
					yyg[yygc] = new YYARec(-16,309);yygc++; 
					yyg[yygc] = new YYARec(-14,126);yygc++; 
					yyg[yygc] = new YYARec(-37,137);yygc++; 
					yyg[yygc] = new YYARec(-36,138);yygc++; 
					yyg[yygc] = new YYARec(-35,139);yygc++; 
					yyg[yygc] = new YYARec(-30,140);yygc++; 
					yyg[yygc] = new YYARec(-19,310);yygc++; 
					yyg[yygc] = new YYARec(-17,142);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = 0;  
					yyd[2] = -50;  
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
					yyd[22] = -33;  
					yyd[23] = -31;  
					yyd[24] = -44;  
					yyd[25] = -48;  
					yyd[26] = -66;  
					yyd[27] = -29;  
					yyd[28] = -32;  
					yyd[29] = -49;  
					yyd[30] = -30;  
					yyd[31] = -190;  
					yyd[32] = -187;  
					yyd[33] = -189;  
					yyd[34] = 0;  
					yyd[35] = -191;  
					yyd[36] = -188;  
					yyd[37] = -192;  
					yyd[38] = -184;  
					yyd[39] = -134;  
					yyd[40] = -183;  
					yyd[41] = -186;  
					yyd[42] = -165;  
					yyd[43] = -179;  
					yyd[44] = -132;  
					yyd[45] = -133;  
					yyd[46] = -178;  
					yyd[47] = -163;  
					yyd[48] = -164;  
					yyd[49] = -185;  
					yyd[50] = 0;  
					yyd[51] = 0;  
					yyd[52] = -177;  
					yyd[53] = -176;  
					yyd[54] = -166;  
					yyd[55] = 0;  
					yyd[56] = -167;  
					yyd[57] = -38;  
					yyd[58] = -36;  
					yyd[59] = 0;  
					yyd[60] = -39;  
					yyd[61] = -37;  
					yyd[62] = -122;  
					yyd[63] = -123;  
					yyd[64] = -121;  
					yyd[65] = -171;  
					yyd[66] = -173;  
					yyd[67] = -174;  
					yyd[68] = -195;  
					yyd[69] = -196;  
					yyd[70] = 0;  
					yyd[71] = 0;  
					yyd[72] = -2;  
					yyd[73] = 0;  
					yyd[74] = 0;  
					yyd[75] = 0;  
					yyd[76] = 0;  
					yyd[77] = 0;  
					yyd[78] = 0;  
					yyd[79] = -83;  
					yyd[80] = 0;  
					yyd[81] = -47;  
					yyd[82] = 0;  
					yyd[83] = -170;  
					yyd[84] = -172;  
					yyd[85] = -28;  
					yyd[86] = 0;  
					yyd[87] = -27;  
					yyd[88] = 0;  
					yyd[89] = 0;  
					yyd[90] = -24;  
					yyd[91] = 0;  
					yyd[92] = -25;  
					yyd[93] = -26;  
					yyd[94] = 0;  
					yyd[95] = 0;  
					yyd[96] = 0;  
					yyd[97] = 0;  
					yyd[98] = -35;  
					yyd[99] = 0;  
					yyd[100] = 0;  
					yyd[101] = 0;  
					yyd[102] = 0;  
					yyd[103] = -82;  
					yyd[104] = -79;  
					yyd[105] = -144;  
					yyd[106] = -81;  
					yyd[107] = -78;  
					yyd[108] = 0;  
					yyd[109] = -143;  
					yyd[110] = -80;  
					yyd[111] = 0;  
					yyd[112] = -142;  
					yyd[113] = -175;  
					yyd[114] = -182;  
					yyd[115] = -181;  
					yyd[116] = -180;  
					yyd[117] = 0;  
					yyd[118] = 0;  
					yyd[119] = 0;  
					yyd[120] = -73;  
					yyd[121] = 0;  
					yyd[122] = 0;  
					yyd[123] = 0;  
					yyd[124] = 0;  
					yyd[125] = 0;  
					yyd[126] = 0;  
					yyd[127] = 0;  
					yyd[128] = 0;  
					yyd[129] = 0;  
					yyd[130] = 0;  
					yyd[131] = 0;  
					yyd[132] = 0;  
					yyd[133] = 0;  
					yyd[134] = 0;  
					yyd[135] = 0;  
					yyd[136] = -46;  
					yyd[137] = 0;  
					yyd[138] = 0;  
					yyd[139] = -159;  
					yyd[140] = -158;  
					yyd[141] = 0;  
					yyd[142] = 0;  
					yyd[143] = 0;  
					yyd[144] = 0;  
					yyd[145] = -157;  
					yyd[146] = -153;  
					yyd[147] = -152;  
					yyd[148] = -155;  
					yyd[149] = -156;  
					yyd[150] = -154;  
					yyd[151] = -151;  
					yyd[152] = 0;  
					yyd[153] = -11;  
					yyd[154] = 0;  
					yyd[155] = -14;  
					yyd[156] = -12;  
					yyd[157] = 0;  
					yyd[158] = 0;  
					yyd[159] = -23;  
					yyd[160] = 0;  
					yyd[161] = -127;  
					yyd[162] = -128;  
					yyd[163] = -129;  
					yyd[164] = -130;  
					yyd[165] = -131;  
					yyd[166] = 0;  
					yyd[167] = -71;  
					yyd[168] = 0;  
					yyd[169] = 0;  
					yyd[170] = -65;  
					yyd[171] = -70;  
					yyd[172] = 0;  
					yyd[173] = 0;  
					yyd[174] = 0;  
					yyd[175] = -108;  
					yyd[176] = 0;  
					yyd[177] = 0;  
					yyd[178] = -104;  
					yyd[179] = -102;  
					yyd[180] = 0;  
					yyd[181] = 0;  
					yyd[182] = 0;  
					yyd[183] = 0;  
					yyd[184] = 0;  
					yyd[185] = 0;  
					yyd[186] = 0;  
					yyd[187] = 0;  
					yyd[188] = 0;  
					yyd[189] = -124;  
					yyd[190] = 0;  
					yyd[191] = 0;  
					yyd[192] = -169;  
					yyd[193] = -168;  
					yyd[194] = 0;  
					yyd[195] = 0;  
					yyd[196] = -109;  
					yyd[197] = 0;  
					yyd[198] = 0;  
					yyd[199] = 0;  
					yyd[200] = -45;  
					yyd[201] = -52;  
					yyd[202] = 0;  
					yyd[203] = 0;  
					yyd[204] = 0;  
					yyd[205] = 0;  
					yyd[206] = 0;  
					yyd[207] = 0;  
					yyd[208] = -139;  
					yyd[209] = -140;  
					yyd[210] = -150;  
					yyd[211] = -148;  
					yyd[212] = -149;  
					yyd[213] = -147;  
					yyd[214] = -138;  
					yyd[215] = -141;  
					yyd[216] = -126;  
					yyd[217] = -74;  
					yyd[218] = 0;  
					yyd[219] = -69;  
					yyd[220] = -68;  
					yyd[221] = 0;  
					yyd[222] = 0;  
					yyd[223] = 0;  
					yyd[224] = 0;  
					yyd[225] = -105;  
					yyd[226] = 0;  
					yyd[227] = -118;  
					yyd[228] = -119;  
					yyd[229] = -120;  
					yyd[230] = 0;  
					yyd[231] = -116;  
					yyd[232] = -117;  
					yyd[233] = 0;  
					yyd[234] = -112;  
					yyd[235] = -113;  
					yyd[236] = -114;  
					yyd[237] = -115;  
					yyd[238] = 0;  
					yyd[239] = -110;  
					yyd[240] = -111;  
					yyd[241] = 0;  
					yyd[242] = 0;  
					yyd[243] = 0;  
					yyd[244] = 0;  
					yyd[245] = 0;  
					yyd[246] = 0;  
					yyd[247] = 0;  
					yyd[248] = 0;  
					yyd[249] = 0;  
					yyd[250] = 0;  
					yyd[251] = -61;  
					yyd[252] = 0;  
					yyd[253] = -54;  
					yyd[254] = -64;  
					yyd[255] = -62;  
					yyd[256] = -60;  
					yyd[257] = 0;  
					yyd[258] = -59;  
					yyd[259] = -58;  
					yyd[260] = -51;  
					yyd[261] = 0;  
					yyd[262] = 0;  
					yyd[263] = 0;  
					yyd[264] = -40;  
					yyd[265] = 0;  
					yyd[266] = 0;  
					yyd[267] = 0;  
					yyd[268] = 0;  
					yyd[269] = 0;  
					yyd[270] = -67;  
					yyd[271] = 0;  
					yyd[272] = -103;  
					yyd[273] = 0;  
					yyd[274] = 0;  
					yyd[275] = 0;  
					yyd[276] = 0;  
					yyd[277] = 0;  
					yyd[278] = 0;  
					yyd[279] = 0;  
					yyd[280] = 0;  
					yyd[281] = -125;  
					yyd[282] = -107;  
					yyd[283] = -135;  
					yyd[284] = 0;  
					yyd[285] = 0;  
					yyd[286] = 0;  
					yyd[287] = 0;  
					yyd[288] = 0;  
					yyd[289] = 0;  
					yyd[290] = 0;  
					yyd[291] = -41;  
					yyd[292] = -13;  
					yyd[293] = -77;  
					yyd[294] = 0;  
					yyd[295] = -18;  
					yyd[296] = -15;  
					yyd[297] = -16;  
					yyd[298] = -106;  
					yyd[299] = -136;  
					yyd[300] = -137;  
					yyd[301] = -56;  
					yyd[302] = -57;  
					yyd[303] = 0;  
					yyd[304] = -22;  
					yyd[305] = -19;  
					yyd[306] = -20;  
					yyd[307] = 0;  
					yyd[308] = 0;  
					yyd[309] = 0;  
					yyd[310] = 0;  
					yyd[311] = -17;  
					yyd[312] = -21; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 16;  
					yyal[2] = 30;  
					yyal[3] = 30;  
					yyal[4] = 43;  
					yyal[5] = 57;  
					yyal[6] = 65;  
					yyal[7] = 78;  
					yyal[8] = 78;  
					yyal[9] = 78;  
					yyal[10] = 78;  
					yyal[11] = 78;  
					yyal[12] = 78;  
					yyal[13] = 78;  
					yyal[14] = 78;  
					yyal[15] = 95;  
					yyal[16] = 95;  
					yyal[17] = 96;  
					yyal[18] = 97;  
					yyal[19] = 98;  
					yyal[20] = 99;  
					yyal[21] = 100;  
					yyal[22] = 101;  
					yyal[23] = 101;  
					yyal[24] = 101;  
					yyal[25] = 101;  
					yyal[26] = 101;  
					yyal[27] = 101;  
					yyal[28] = 101;  
					yyal[29] = 101;  
					yyal[30] = 101;  
					yyal[31] = 101;  
					yyal[32] = 101;  
					yyal[33] = 101;  
					yyal[34] = 101;  
					yyal[35] = 103;  
					yyal[36] = 103;  
					yyal[37] = 103;  
					yyal[38] = 103;  
					yyal[39] = 103;  
					yyal[40] = 103;  
					yyal[41] = 103;  
					yyal[42] = 103;  
					yyal[43] = 103;  
					yyal[44] = 103;  
					yyal[45] = 103;  
					yyal[46] = 103;  
					yyal[47] = 103;  
					yyal[48] = 103;  
					yyal[49] = 103;  
					yyal[50] = 103;  
					yyal[51] = 107;  
					yyal[52] = 109;  
					yyal[53] = 109;  
					yyal[54] = 109;  
					yyal[55] = 109;  
					yyal[56] = 111;  
					yyal[57] = 111;  
					yyal[58] = 111;  
					yyal[59] = 111;  
					yyal[60] = 112;  
					yyal[61] = 112;  
					yyal[62] = 112;  
					yyal[63] = 112;  
					yyal[64] = 112;  
					yyal[65] = 112;  
					yyal[66] = 112;  
					yyal[67] = 112;  
					yyal[68] = 112;  
					yyal[69] = 112;  
					yyal[70] = 112;  
					yyal[71] = 127;  
					yyal[72] = 128;  
					yyal[73] = 128;  
					yyal[74] = 129;  
					yyal[75] = 130;  
					yyal[76] = 132;  
					yyal[77] = 133;  
					yyal[78] = 134;  
					yyal[79] = 135;  
					yyal[80] = 135;  
					yyal[81] = 137;  
					yyal[82] = 137;  
					yyal[83] = 138;  
					yyal[84] = 138;  
					yyal[85] = 138;  
					yyal[86] = 138;  
					yyal[87] = 152;  
					yyal[88] = 152;  
					yyal[89] = 168;  
					yyal[90] = 184;  
					yyal[91] = 184;  
					yyal[92] = 213;  
					yyal[93] = 213;  
					yyal[94] = 213;  
					yyal[95] = 242;  
					yyal[96] = 243;  
					yyal[97] = 255;  
					yyal[98] = 261;  
					yyal[99] = 261;  
					yyal[100] = 262;  
					yyal[101] = 264;  
					yyal[102] = 265;  
					yyal[103] = 318;  
					yyal[104] = 318;  
					yyal[105] = 318;  
					yyal[106] = 318;  
					yyal[107] = 318;  
					yyal[108] = 318;  
					yyal[109] = 371;  
					yyal[110] = 371;  
					yyal[111] = 371;  
					yyal[112] = 372;  
					yyal[113] = 372;  
					yyal[114] = 372;  
					yyal[115] = 372;  
					yyal[116] = 372;  
					yyal[117] = 372;  
					yyal[118] = 410;  
					yyal[119] = 415;  
					yyal[120] = 446;  
					yyal[121] = 446;  
					yyal[122] = 477;  
					yyal[123] = 478;  
					yyal[124] = 479;  
					yyal[125] = 486;  
					yyal[126] = 487;  
					yyal[127] = 518;  
					yyal[128] = 519;  
					yyal[129] = 520;  
					yyal[130] = 549;  
					yyal[131] = 576;  
					yyal[132] = 608;  
					yyal[133] = 635;  
					yyal[134] = 662;  
					yyal[135] = 669;  
					yyal[136] = 707;  
					yyal[137] = 707;  
					yyal[138] = 732;  
					yyal[139] = 733;  
					yyal[140] = 733;  
					yyal[141] = 733;  
					yyal[142] = 734;  
					yyal[143] = 748;  
					yyal[144] = 749;  
					yyal[145] = 750;  
					yyal[146] = 750;  
					yyal[147] = 750;  
					yyal[148] = 750;  
					yyal[149] = 750;  
					yyal[150] = 750;  
					yyal[151] = 750;  
					yyal[152] = 750;  
					yyal[153] = 755;  
					yyal[154] = 755;  
					yyal[155] = 756;  
					yyal[156] = 756;  
					yyal[157] = 756;  
					yyal[158] = 769;  
					yyal[159] = 782;  
					yyal[160] = 782;  
					yyal[161] = 809;  
					yyal[162] = 809;  
					yyal[163] = 809;  
					yyal[164] = 809;  
					yyal[165] = 809;  
					yyal[166] = 809;  
					yyal[167] = 838;  
					yyal[168] = 838;  
					yyal[169] = 869;  
					yyal[170] = 900;  
					yyal[171] = 900;  
					yyal[172] = 900;  
					yyal[173] = 901;  
					yyal[174] = 902;  
					yyal[175] = 903;  
					yyal[176] = 903;  
					yyal[177] = 929;  
					yyal[178] = 956;  
					yyal[179] = 956;  
					yyal[180] = 956;  
					yyal[181] = 975;  
					yyal[182] = 991;  
					yyal[183] = 1005;  
					yyal[184] = 1015;  
					yyal[185] = 1023;  
					yyal[186] = 1030;  
					yyal[187] = 1036;  
					yyal[188] = 1041;  
					yyal[189] = 1045;  
					yyal[190] = 1045;  
					yyal[191] = 1067;  
					yyal[192] = 1094;  
					yyal[193] = 1094;  
					yyal[194] = 1094;  
					yyal[195] = 1123;  
					yyal[196] = 1124;  
					yyal[197] = 1124;  
					yyal[198] = 1125;  
					yyal[199] = 1149;  
					yyal[200] = 1163;  
					yyal[201] = 1163;  
					yyal[202] = 1163;  
					yyal[203] = 1164;  
					yyal[204] = 1165;  
					yyal[205] = 1166;  
					yyal[206] = 1168;  
					yyal[207] = 1169;  
					yyal[208] = 1184;  
					yyal[209] = 1184;  
					yyal[210] = 1184;  
					yyal[211] = 1184;  
					yyal[212] = 1184;  
					yyal[213] = 1184;  
					yyal[214] = 1184;  
					yyal[215] = 1184;  
					yyal[216] = 1184;  
					yyal[217] = 1184;  
					yyal[218] = 1184;  
					yyal[219] = 1215;  
					yyal[220] = 1215;  
					yyal[221] = 1215;  
					yyal[222] = 1245;  
					yyal[223] = 1275;  
					yyal[224] = 1306;  
					yyal[225] = 1333;  
					yyal[226] = 1333;  
					yyal[227] = 1360;  
					yyal[228] = 1360;  
					yyal[229] = 1360;  
					yyal[230] = 1360;  
					yyal[231] = 1387;  
					yyal[232] = 1387;  
					yyal[233] = 1387;  
					yyal[234] = 1414;  
					yyal[235] = 1414;  
					yyal[236] = 1414;  
					yyal[237] = 1414;  
					yyal[238] = 1414;  
					yyal[239] = 1441;  
					yyal[240] = 1441;  
					yyal[241] = 1441;  
					yyal[242] = 1468;  
					yyal[243] = 1495;  
					yyal[244] = 1522;  
					yyal[245] = 1549;  
					yyal[246] = 1576;  
					yyal[247] = 1603;  
					yyal[248] = 1604;  
					yyal[249] = 1605;  
					yyal[250] = 1634;  
					yyal[251] = 1663;  
					yyal[252] = 1663;  
					yyal[253] = 1689;  
					yyal[254] = 1689;  
					yyal[255] = 1689;  
					yyal[256] = 1689;  
					yyal[257] = 1689;  
					yyal[258] = 1716;  
					yyal[259] = 1716;  
					yyal[260] = 1716;  
					yyal[261] = 1716;  
					yyal[262] = 1729;  
					yyal[263] = 1742;  
					yyal[264] = 1747;  
					yyal[265] = 1747;  
					yyal[266] = 1748;  
					yyal[267] = 1778;  
					yyal[268] = 1780;  
					yyal[269] = 1781;  
					yyal[270] = 1782;  
					yyal[271] = 1782;  
					yyal[272] = 1783;  
					yyal[273] = 1783;  
					yyal[274] = 1802;  
					yyal[275] = 1818;  
					yyal[276] = 1832;  
					yyal[277] = 1842;  
					yyal[278] = 1850;  
					yyal[279] = 1857;  
					yyal[280] = 1863;  
					yyal[281] = 1868;  
					yyal[282] = 1868;  
					yyal[283] = 1868;  
					yyal[284] = 1868;  
					yyal[285] = 1869;  
					yyal[286] = 1870;  
					yyal[287] = 1895;  
					yyal[288] = 1904;  
					yyal[289] = 1906;  
					yyal[290] = 1907;  
					yyal[291] = 1908;  
					yyal[292] = 1908;  
					yyal[293] = 1908;  
					yyal[294] = 1908;  
					yyal[295] = 1909;  
					yyal[296] = 1909;  
					yyal[297] = 1909;  
					yyal[298] = 1909;  
					yyal[299] = 1909;  
					yyal[300] = 1909;  
					yyal[301] = 1909;  
					yyal[302] = 1909;  
					yyal[303] = 1909;  
					yyal[304] = 1910;  
					yyal[305] = 1910;  
					yyal[306] = 1910;  
					yyal[307] = 1910;  
					yyal[308] = 1939;  
					yyal[309] = 1951;  
					yyal[310] = 1952;  
					yyal[311] = 1953;  
					yyal[312] = 1953; 

					yyah = new int[yynstates];
					yyah[0] = 15;  
					yyah[1] = 29;  
					yyah[2] = 29;  
					yyah[3] = 42;  
					yyah[4] = 56;  
					yyah[5] = 64;  
					yyah[6] = 77;  
					yyah[7] = 77;  
					yyah[8] = 77;  
					yyah[9] = 77;  
					yyah[10] = 77;  
					yyah[11] = 77;  
					yyah[12] = 77;  
					yyah[13] = 77;  
					yyah[14] = 94;  
					yyah[15] = 94;  
					yyah[16] = 95;  
					yyah[17] = 96;  
					yyah[18] = 97;  
					yyah[19] = 98;  
					yyah[20] = 99;  
					yyah[21] = 100;  
					yyah[22] = 100;  
					yyah[23] = 100;  
					yyah[24] = 100;  
					yyah[25] = 100;  
					yyah[26] = 100;  
					yyah[27] = 100;  
					yyah[28] = 100;  
					yyah[29] = 100;  
					yyah[30] = 100;  
					yyah[31] = 100;  
					yyah[32] = 100;  
					yyah[33] = 100;  
					yyah[34] = 102;  
					yyah[35] = 102;  
					yyah[36] = 102;  
					yyah[37] = 102;  
					yyah[38] = 102;  
					yyah[39] = 102;  
					yyah[40] = 102;  
					yyah[41] = 102;  
					yyah[42] = 102;  
					yyah[43] = 102;  
					yyah[44] = 102;  
					yyah[45] = 102;  
					yyah[46] = 102;  
					yyah[47] = 102;  
					yyah[48] = 102;  
					yyah[49] = 102;  
					yyah[50] = 106;  
					yyah[51] = 108;  
					yyah[52] = 108;  
					yyah[53] = 108;  
					yyah[54] = 108;  
					yyah[55] = 110;  
					yyah[56] = 110;  
					yyah[57] = 110;  
					yyah[58] = 110;  
					yyah[59] = 111;  
					yyah[60] = 111;  
					yyah[61] = 111;  
					yyah[62] = 111;  
					yyah[63] = 111;  
					yyah[64] = 111;  
					yyah[65] = 111;  
					yyah[66] = 111;  
					yyah[67] = 111;  
					yyah[68] = 111;  
					yyah[69] = 111;  
					yyah[70] = 126;  
					yyah[71] = 127;  
					yyah[72] = 127;  
					yyah[73] = 128;  
					yyah[74] = 129;  
					yyah[75] = 131;  
					yyah[76] = 132;  
					yyah[77] = 133;  
					yyah[78] = 134;  
					yyah[79] = 134;  
					yyah[80] = 136;  
					yyah[81] = 136;  
					yyah[82] = 137;  
					yyah[83] = 137;  
					yyah[84] = 137;  
					yyah[85] = 137;  
					yyah[86] = 151;  
					yyah[87] = 151;  
					yyah[88] = 167;  
					yyah[89] = 183;  
					yyah[90] = 183;  
					yyah[91] = 212;  
					yyah[92] = 212;  
					yyah[93] = 212;  
					yyah[94] = 241;  
					yyah[95] = 242;  
					yyah[96] = 254;  
					yyah[97] = 260;  
					yyah[98] = 260;  
					yyah[99] = 261;  
					yyah[100] = 263;  
					yyah[101] = 264;  
					yyah[102] = 317;  
					yyah[103] = 317;  
					yyah[104] = 317;  
					yyah[105] = 317;  
					yyah[106] = 317;  
					yyah[107] = 317;  
					yyah[108] = 370;  
					yyah[109] = 370;  
					yyah[110] = 370;  
					yyah[111] = 371;  
					yyah[112] = 371;  
					yyah[113] = 371;  
					yyah[114] = 371;  
					yyah[115] = 371;  
					yyah[116] = 371;  
					yyah[117] = 409;  
					yyah[118] = 414;  
					yyah[119] = 445;  
					yyah[120] = 445;  
					yyah[121] = 476;  
					yyah[122] = 477;  
					yyah[123] = 478;  
					yyah[124] = 485;  
					yyah[125] = 486;  
					yyah[126] = 517;  
					yyah[127] = 518;  
					yyah[128] = 519;  
					yyah[129] = 548;  
					yyah[130] = 575;  
					yyah[131] = 607;  
					yyah[132] = 634;  
					yyah[133] = 661;  
					yyah[134] = 668;  
					yyah[135] = 706;  
					yyah[136] = 706;  
					yyah[137] = 731;  
					yyah[138] = 732;  
					yyah[139] = 732;  
					yyah[140] = 732;  
					yyah[141] = 733;  
					yyah[142] = 747;  
					yyah[143] = 748;  
					yyah[144] = 749;  
					yyah[145] = 749;  
					yyah[146] = 749;  
					yyah[147] = 749;  
					yyah[148] = 749;  
					yyah[149] = 749;  
					yyah[150] = 749;  
					yyah[151] = 749;  
					yyah[152] = 754;  
					yyah[153] = 754;  
					yyah[154] = 755;  
					yyah[155] = 755;  
					yyah[156] = 755;  
					yyah[157] = 768;  
					yyah[158] = 781;  
					yyah[159] = 781;  
					yyah[160] = 808;  
					yyah[161] = 808;  
					yyah[162] = 808;  
					yyah[163] = 808;  
					yyah[164] = 808;  
					yyah[165] = 808;  
					yyah[166] = 837;  
					yyah[167] = 837;  
					yyah[168] = 868;  
					yyah[169] = 899;  
					yyah[170] = 899;  
					yyah[171] = 899;  
					yyah[172] = 900;  
					yyah[173] = 901;  
					yyah[174] = 902;  
					yyah[175] = 902;  
					yyah[176] = 928;  
					yyah[177] = 955;  
					yyah[178] = 955;  
					yyah[179] = 955;  
					yyah[180] = 974;  
					yyah[181] = 990;  
					yyah[182] = 1004;  
					yyah[183] = 1014;  
					yyah[184] = 1022;  
					yyah[185] = 1029;  
					yyah[186] = 1035;  
					yyah[187] = 1040;  
					yyah[188] = 1044;  
					yyah[189] = 1044;  
					yyah[190] = 1066;  
					yyah[191] = 1093;  
					yyah[192] = 1093;  
					yyah[193] = 1093;  
					yyah[194] = 1122;  
					yyah[195] = 1123;  
					yyah[196] = 1123;  
					yyah[197] = 1124;  
					yyah[198] = 1148;  
					yyah[199] = 1162;  
					yyah[200] = 1162;  
					yyah[201] = 1162;  
					yyah[202] = 1163;  
					yyah[203] = 1164;  
					yyah[204] = 1165;  
					yyah[205] = 1167;  
					yyah[206] = 1168;  
					yyah[207] = 1183;  
					yyah[208] = 1183;  
					yyah[209] = 1183;  
					yyah[210] = 1183;  
					yyah[211] = 1183;  
					yyah[212] = 1183;  
					yyah[213] = 1183;  
					yyah[214] = 1183;  
					yyah[215] = 1183;  
					yyah[216] = 1183;  
					yyah[217] = 1183;  
					yyah[218] = 1214;  
					yyah[219] = 1214;  
					yyah[220] = 1214;  
					yyah[221] = 1244;  
					yyah[222] = 1274;  
					yyah[223] = 1305;  
					yyah[224] = 1332;  
					yyah[225] = 1332;  
					yyah[226] = 1359;  
					yyah[227] = 1359;  
					yyah[228] = 1359;  
					yyah[229] = 1359;  
					yyah[230] = 1386;  
					yyah[231] = 1386;  
					yyah[232] = 1386;  
					yyah[233] = 1413;  
					yyah[234] = 1413;  
					yyah[235] = 1413;  
					yyah[236] = 1413;  
					yyah[237] = 1413;  
					yyah[238] = 1440;  
					yyah[239] = 1440;  
					yyah[240] = 1440;  
					yyah[241] = 1467;  
					yyah[242] = 1494;  
					yyah[243] = 1521;  
					yyah[244] = 1548;  
					yyah[245] = 1575;  
					yyah[246] = 1602;  
					yyah[247] = 1603;  
					yyah[248] = 1604;  
					yyah[249] = 1633;  
					yyah[250] = 1662;  
					yyah[251] = 1662;  
					yyah[252] = 1688;  
					yyah[253] = 1688;  
					yyah[254] = 1688;  
					yyah[255] = 1688;  
					yyah[256] = 1688;  
					yyah[257] = 1715;  
					yyah[258] = 1715;  
					yyah[259] = 1715;  
					yyah[260] = 1715;  
					yyah[261] = 1728;  
					yyah[262] = 1741;  
					yyah[263] = 1746;  
					yyah[264] = 1746;  
					yyah[265] = 1747;  
					yyah[266] = 1777;  
					yyah[267] = 1779;  
					yyah[268] = 1780;  
					yyah[269] = 1781;  
					yyah[270] = 1781;  
					yyah[271] = 1782;  
					yyah[272] = 1782;  
					yyah[273] = 1801;  
					yyah[274] = 1817;  
					yyah[275] = 1831;  
					yyah[276] = 1841;  
					yyah[277] = 1849;  
					yyah[278] = 1856;  
					yyah[279] = 1862;  
					yyah[280] = 1867;  
					yyah[281] = 1867;  
					yyah[282] = 1867;  
					yyah[283] = 1867;  
					yyah[284] = 1868;  
					yyah[285] = 1869;  
					yyah[286] = 1894;  
					yyah[287] = 1903;  
					yyah[288] = 1905;  
					yyah[289] = 1906;  
					yyah[290] = 1907;  
					yyah[291] = 1907;  
					yyah[292] = 1907;  
					yyah[293] = 1907;  
					yyah[294] = 1908;  
					yyah[295] = 1908;  
					yyah[296] = 1908;  
					yyah[297] = 1908;  
					yyah[298] = 1908;  
					yyah[299] = 1908;  
					yyah[300] = 1908;  
					yyah[301] = 1908;  
					yyah[302] = 1908;  
					yyah[303] = 1909;  
					yyah[304] = 1909;  
					yyah[305] = 1909;  
					yyah[306] = 1909;  
					yyah[307] = 1938;  
					yyah[308] = 1950;  
					yyah[309] = 1951;  
					yyah[310] = 1952;  
					yyah[311] = 1952;  
					yyah[312] = 1952; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 17;  
					yygl[2] = 24;  
					yygl[3] = 24;  
					yygl[4] = 29;  
					yygl[5] = 35;  
					yygl[6] = 43;  
					yygl[7] = 49;  
					yygl[8] = 49;  
					yygl[9] = 49;  
					yygl[10] = 49;  
					yygl[11] = 49;  
					yygl[12] = 49;  
					yygl[13] = 49;  
					yygl[14] = 49;  
					yygl[15] = 64;  
					yygl[16] = 64;  
					yygl[17] = 64;  
					yygl[18] = 65;  
					yygl[19] = 66;  
					yygl[20] = 67;  
					yygl[21] = 68;  
					yygl[22] = 69;  
					yygl[23] = 69;  
					yygl[24] = 69;  
					yygl[25] = 69;  
					yygl[26] = 69;  
					yygl[27] = 69;  
					yygl[28] = 69;  
					yygl[29] = 69;  
					yygl[30] = 69;  
					yygl[31] = 69;  
					yygl[32] = 69;  
					yygl[33] = 69;  
					yygl[34] = 69;  
					yygl[35] = 70;  
					yygl[36] = 70;  
					yygl[37] = 70;  
					yygl[38] = 70;  
					yygl[39] = 70;  
					yygl[40] = 70;  
					yygl[41] = 70;  
					yygl[42] = 70;  
					yygl[43] = 70;  
					yygl[44] = 70;  
					yygl[45] = 70;  
					yygl[46] = 70;  
					yygl[47] = 70;  
					yygl[48] = 70;  
					yygl[49] = 70;  
					yygl[50] = 70;  
					yygl[51] = 71;  
					yygl[52] = 72;  
					yygl[53] = 72;  
					yygl[54] = 72;  
					yygl[55] = 72;  
					yygl[56] = 72;  
					yygl[57] = 72;  
					yygl[58] = 72;  
					yygl[59] = 72;  
					yygl[60] = 72;  
					yygl[61] = 72;  
					yygl[62] = 72;  
					yygl[63] = 72;  
					yygl[64] = 72;  
					yygl[65] = 72;  
					yygl[66] = 72;  
					yygl[67] = 72;  
					yygl[68] = 72;  
					yygl[69] = 72;  
					yygl[70] = 72;  
					yygl[71] = 73;  
					yygl[72] = 73;  
					yygl[73] = 73;  
					yygl[74] = 73;  
					yygl[75] = 73;  
					yygl[76] = 73;  
					yygl[77] = 73;  
					yygl[78] = 73;  
					yygl[79] = 73;  
					yygl[80] = 73;  
					yygl[81] = 74;  
					yygl[82] = 74;  
					yygl[83] = 75;  
					yygl[84] = 75;  
					yygl[85] = 75;  
					yygl[86] = 75;  
					yygl[87] = 81;  
					yygl[88] = 81;  
					yygl[89] = 97;  
					yygl[90] = 113;  
					yygl[91] = 113;  
					yygl[92] = 131;  
					yygl[93] = 131;  
					yygl[94] = 131;  
					yygl[95] = 148;  
					yygl[96] = 148;  
					yygl[97] = 154;  
					yygl[98] = 155;  
					yygl[99] = 155;  
					yygl[100] = 155;  
					yygl[101] = 155;  
					yygl[102] = 155;  
					yygl[103] = 155;  
					yygl[104] = 155;  
					yygl[105] = 155;  
					yygl[106] = 155;  
					yygl[107] = 155;  
					yygl[108] = 155;  
					yygl[109] = 155;  
					yygl[110] = 155;  
					yygl[111] = 155;  
					yygl[112] = 155;  
					yygl[113] = 155;  
					yygl[114] = 155;  
					yygl[115] = 155;  
					yygl[116] = 155;  
					yygl[117] = 155;  
					yygl[118] = 155;  
					yygl[119] = 156;  
					yygl[120] = 157;  
					yygl[121] = 157;  
					yygl[122] = 174;  
					yygl[123] = 174;  
					yygl[124] = 174;  
					yygl[125] = 174;  
					yygl[126] = 174;  
					yygl[127] = 191;  
					yygl[128] = 192;  
					yygl[129] = 193;  
					yygl[130] = 210;  
					yygl[131] = 234;  
					yygl[132] = 234;  
					yygl[133] = 258;  
					yygl[134] = 282;  
					yygl[135] = 282;  
					yygl[136] = 282;  
					yygl[137] = 282;  
					yygl[138] = 283;  
					yygl[139] = 283;  
					yygl[140] = 283;  
					yygl[141] = 283;  
					yygl[142] = 283;  
					yygl[143] = 289;  
					yygl[144] = 290;  
					yygl[145] = 291;  
					yygl[146] = 291;  
					yygl[147] = 291;  
					yygl[148] = 291;  
					yygl[149] = 291;  
					yygl[150] = 291;  
					yygl[151] = 291;  
					yygl[152] = 291;  
					yygl[153] = 294;  
					yygl[154] = 294;  
					yygl[155] = 294;  
					yygl[156] = 294;  
					yygl[157] = 294;  
					yygl[158] = 298;  
					yygl[159] = 302;  
					yygl[160] = 302;  
					yygl[161] = 326;  
					yygl[162] = 326;  
					yygl[163] = 326;  
					yygl[164] = 326;  
					yygl[165] = 326;  
					yygl[166] = 326;  
					yygl[167] = 345;  
					yygl[168] = 345;  
					yygl[169] = 362;  
					yygl[170] = 379;  
					yygl[171] = 379;  
					yygl[172] = 379;  
					yygl[173] = 379;  
					yygl[174] = 379;  
					yygl[175] = 379;  
					yygl[176] = 379;  
					yygl[177] = 379;  
					yygl[178] = 393;  
					yygl[179] = 393;  
					yygl[180] = 393;  
					yygl[181] = 394;  
					yygl[182] = 395;  
					yygl[183] = 396;  
					yygl[184] = 397;  
					yygl[185] = 397;  
					yygl[186] = 397;  
					yygl[187] = 397;  
					yygl[188] = 397;  
					yygl[189] = 397;  
					yygl[190] = 397;  
					yygl[191] = 398;  
					yygl[192] = 422;  
					yygl[193] = 422;  
					yygl[194] = 422;  
					yygl[195] = 439;  
					yygl[196] = 439;  
					yygl[197] = 439;  
					yygl[198] = 439;  
					yygl[199] = 455;  
					yygl[200] = 461;  
					yygl[201] = 461;  
					yygl[202] = 461;  
					yygl[203] = 461;  
					yygl[204] = 461;  
					yygl[205] = 461;  
					yygl[206] = 461;  
					yygl[207] = 461;  
					yygl[208] = 476;  
					yygl[209] = 476;  
					yygl[210] = 476;  
					yygl[211] = 476;  
					yygl[212] = 476;  
					yygl[213] = 476;  
					yygl[214] = 476;  
					yygl[215] = 476;  
					yygl[216] = 476;  
					yygl[217] = 476;  
					yygl[218] = 476;  
					yygl[219] = 477;  
					yygl[220] = 477;  
					yygl[221] = 477;  
					yygl[222] = 495;  
					yygl[223] = 513;  
					yygl[224] = 530;  
					yygl[225] = 554;  
					yygl[226] = 554;  
					yygl[227] = 568;  
					yygl[228] = 568;  
					yygl[229] = 568;  
					yygl[230] = 568;  
					yygl[231] = 583;  
					yygl[232] = 583;  
					yygl[233] = 583;  
					yygl[234] = 599;  
					yygl[235] = 599;  
					yygl[236] = 599;  
					yygl[237] = 599;  
					yygl[238] = 599;  
					yygl[239] = 616;  
					yygl[240] = 616;  
					yygl[241] = 616;  
					yygl[242] = 634;  
					yygl[243] = 653;  
					yygl[244] = 673;  
					yygl[245] = 694;  
					yygl[246] = 716;  
					yygl[247] = 740;  
					yygl[248] = 740;  
					yygl[249] = 740;  
					yygl[250] = 757;  
					yygl[251] = 774;  
					yygl[252] = 774;  
					yygl[253] = 775;  
					yygl[254] = 775;  
					yygl[255] = 775;  
					yygl[256] = 775;  
					yygl[257] = 775;  
					yygl[258] = 775;  
					yygl[259] = 775;  
					yygl[260] = 775;  
					yygl[261] = 775;  
					yygl[262] = 782;  
					yygl[263] = 789;  
					yygl[264] = 792;  
					yygl[265] = 792;  
					yygl[266] = 792;  
					yygl[267] = 811;  
					yygl[268] = 811;  
					yygl[269] = 811;  
					yygl[270] = 811;  
					yygl[271] = 811;  
					yygl[272] = 811;  
					yygl[273] = 811;  
					yygl[274] = 812;  
					yygl[275] = 813;  
					yygl[276] = 814;  
					yygl[277] = 815;  
					yygl[278] = 815;  
					yygl[279] = 815;  
					yygl[280] = 815;  
					yygl[281] = 815;  
					yygl[282] = 815;  
					yygl[283] = 815;  
					yygl[284] = 815;  
					yygl[285] = 815;  
					yygl[286] = 815;  
					yygl[287] = 831;  
					yygl[288] = 834;  
					yygl[289] = 834;  
					yygl[290] = 834;  
					yygl[291] = 834;  
					yygl[292] = 834;  
					yygl[293] = 834;  
					yygl[294] = 834;  
					yygl[295] = 834;  
					yygl[296] = 834;  
					yygl[297] = 834;  
					yygl[298] = 834;  
					yygl[299] = 834;  
					yygl[300] = 834;  
					yygl[301] = 834;  
					yygl[302] = 834;  
					yygl[303] = 834;  
					yygl[304] = 834;  
					yygl[305] = 834;  
					yygl[306] = 834;  
					yygl[307] = 834;  
					yygl[308] = 851;  
					yygl[309] = 857;  
					yygl[310] = 857;  
					yygl[311] = 857;  
					yygl[312] = 857; 

					yygh = new int[yynstates];
					yygh[0] = 16;  
					yygh[1] = 23;  
					yygh[2] = 23;  
					yygh[3] = 28;  
					yygh[4] = 34;  
					yygh[5] = 42;  
					yygh[6] = 48;  
					yygh[7] = 48;  
					yygh[8] = 48;  
					yygh[9] = 48;  
					yygh[10] = 48;  
					yygh[11] = 48;  
					yygh[12] = 48;  
					yygh[13] = 48;  
					yygh[14] = 63;  
					yygh[15] = 63;  
					yygh[16] = 63;  
					yygh[17] = 64;  
					yygh[18] = 65;  
					yygh[19] = 66;  
					yygh[20] = 67;  
					yygh[21] = 68;  
					yygh[22] = 68;  
					yygh[23] = 68;  
					yygh[24] = 68;  
					yygh[25] = 68;  
					yygh[26] = 68;  
					yygh[27] = 68;  
					yygh[28] = 68;  
					yygh[29] = 68;  
					yygh[30] = 68;  
					yygh[31] = 68;  
					yygh[32] = 68;  
					yygh[33] = 68;  
					yygh[34] = 69;  
					yygh[35] = 69;  
					yygh[36] = 69;  
					yygh[37] = 69;  
					yygh[38] = 69;  
					yygh[39] = 69;  
					yygh[40] = 69;  
					yygh[41] = 69;  
					yygh[42] = 69;  
					yygh[43] = 69;  
					yygh[44] = 69;  
					yygh[45] = 69;  
					yygh[46] = 69;  
					yygh[47] = 69;  
					yygh[48] = 69;  
					yygh[49] = 69;  
					yygh[50] = 70;  
					yygh[51] = 71;  
					yygh[52] = 71;  
					yygh[53] = 71;  
					yygh[54] = 71;  
					yygh[55] = 71;  
					yygh[56] = 71;  
					yygh[57] = 71;  
					yygh[58] = 71;  
					yygh[59] = 71;  
					yygh[60] = 71;  
					yygh[61] = 71;  
					yygh[62] = 71;  
					yygh[63] = 71;  
					yygh[64] = 71;  
					yygh[65] = 71;  
					yygh[66] = 71;  
					yygh[67] = 71;  
					yygh[68] = 71;  
					yygh[69] = 71;  
					yygh[70] = 72;  
					yygh[71] = 72;  
					yygh[72] = 72;  
					yygh[73] = 72;  
					yygh[74] = 72;  
					yygh[75] = 72;  
					yygh[76] = 72;  
					yygh[77] = 72;  
					yygh[78] = 72;  
					yygh[79] = 72;  
					yygh[80] = 73;  
					yygh[81] = 73;  
					yygh[82] = 74;  
					yygh[83] = 74;  
					yygh[84] = 74;  
					yygh[85] = 74;  
					yygh[86] = 80;  
					yygh[87] = 80;  
					yygh[88] = 96;  
					yygh[89] = 112;  
					yygh[90] = 112;  
					yygh[91] = 130;  
					yygh[92] = 130;  
					yygh[93] = 130;  
					yygh[94] = 147;  
					yygh[95] = 147;  
					yygh[96] = 153;  
					yygh[97] = 154;  
					yygh[98] = 154;  
					yygh[99] = 154;  
					yygh[100] = 154;  
					yygh[101] = 154;  
					yygh[102] = 154;  
					yygh[103] = 154;  
					yygh[104] = 154;  
					yygh[105] = 154;  
					yygh[106] = 154;  
					yygh[107] = 154;  
					yygh[108] = 154;  
					yygh[109] = 154;  
					yygh[110] = 154;  
					yygh[111] = 154;  
					yygh[112] = 154;  
					yygh[113] = 154;  
					yygh[114] = 154;  
					yygh[115] = 154;  
					yygh[116] = 154;  
					yygh[117] = 154;  
					yygh[118] = 155;  
					yygh[119] = 156;  
					yygh[120] = 156;  
					yygh[121] = 173;  
					yygh[122] = 173;  
					yygh[123] = 173;  
					yygh[124] = 173;  
					yygh[125] = 173;  
					yygh[126] = 190;  
					yygh[127] = 191;  
					yygh[128] = 192;  
					yygh[129] = 209;  
					yygh[130] = 233;  
					yygh[131] = 233;  
					yygh[132] = 257;  
					yygh[133] = 281;  
					yygh[134] = 281;  
					yygh[135] = 281;  
					yygh[136] = 281;  
					yygh[137] = 282;  
					yygh[138] = 282;  
					yygh[139] = 282;  
					yygh[140] = 282;  
					yygh[141] = 282;  
					yygh[142] = 288;  
					yygh[143] = 289;  
					yygh[144] = 290;  
					yygh[145] = 290;  
					yygh[146] = 290;  
					yygh[147] = 290;  
					yygh[148] = 290;  
					yygh[149] = 290;  
					yygh[150] = 290;  
					yygh[151] = 290;  
					yygh[152] = 293;  
					yygh[153] = 293;  
					yygh[154] = 293;  
					yygh[155] = 293;  
					yygh[156] = 293;  
					yygh[157] = 297;  
					yygh[158] = 301;  
					yygh[159] = 301;  
					yygh[160] = 325;  
					yygh[161] = 325;  
					yygh[162] = 325;  
					yygh[163] = 325;  
					yygh[164] = 325;  
					yygh[165] = 325;  
					yygh[166] = 344;  
					yygh[167] = 344;  
					yygh[168] = 361;  
					yygh[169] = 378;  
					yygh[170] = 378;  
					yygh[171] = 378;  
					yygh[172] = 378;  
					yygh[173] = 378;  
					yygh[174] = 378;  
					yygh[175] = 378;  
					yygh[176] = 378;  
					yygh[177] = 392;  
					yygh[178] = 392;  
					yygh[179] = 392;  
					yygh[180] = 393;  
					yygh[181] = 394;  
					yygh[182] = 395;  
					yygh[183] = 396;  
					yygh[184] = 396;  
					yygh[185] = 396;  
					yygh[186] = 396;  
					yygh[187] = 396;  
					yygh[188] = 396;  
					yygh[189] = 396;  
					yygh[190] = 397;  
					yygh[191] = 421;  
					yygh[192] = 421;  
					yygh[193] = 421;  
					yygh[194] = 438;  
					yygh[195] = 438;  
					yygh[196] = 438;  
					yygh[197] = 438;  
					yygh[198] = 454;  
					yygh[199] = 460;  
					yygh[200] = 460;  
					yygh[201] = 460;  
					yygh[202] = 460;  
					yygh[203] = 460;  
					yygh[204] = 460;  
					yygh[205] = 460;  
					yygh[206] = 460;  
					yygh[207] = 475;  
					yygh[208] = 475;  
					yygh[209] = 475;  
					yygh[210] = 475;  
					yygh[211] = 475;  
					yygh[212] = 475;  
					yygh[213] = 475;  
					yygh[214] = 475;  
					yygh[215] = 475;  
					yygh[216] = 475;  
					yygh[217] = 475;  
					yygh[218] = 476;  
					yygh[219] = 476;  
					yygh[220] = 476;  
					yygh[221] = 494;  
					yygh[222] = 512;  
					yygh[223] = 529;  
					yygh[224] = 553;  
					yygh[225] = 553;  
					yygh[226] = 567;  
					yygh[227] = 567;  
					yygh[228] = 567;  
					yygh[229] = 567;  
					yygh[230] = 582;  
					yygh[231] = 582;  
					yygh[232] = 582;  
					yygh[233] = 598;  
					yygh[234] = 598;  
					yygh[235] = 598;  
					yygh[236] = 598;  
					yygh[237] = 598;  
					yygh[238] = 615;  
					yygh[239] = 615;  
					yygh[240] = 615;  
					yygh[241] = 633;  
					yygh[242] = 652;  
					yygh[243] = 672;  
					yygh[244] = 693;  
					yygh[245] = 715;  
					yygh[246] = 739;  
					yygh[247] = 739;  
					yygh[248] = 739;  
					yygh[249] = 756;  
					yygh[250] = 773;  
					yygh[251] = 773;  
					yygh[252] = 774;  
					yygh[253] = 774;  
					yygh[254] = 774;  
					yygh[255] = 774;  
					yygh[256] = 774;  
					yygh[257] = 774;  
					yygh[258] = 774;  
					yygh[259] = 774;  
					yygh[260] = 774;  
					yygh[261] = 781;  
					yygh[262] = 788;  
					yygh[263] = 791;  
					yygh[264] = 791;  
					yygh[265] = 791;  
					yygh[266] = 810;  
					yygh[267] = 810;  
					yygh[268] = 810;  
					yygh[269] = 810;  
					yygh[270] = 810;  
					yygh[271] = 810;  
					yygh[272] = 810;  
					yygh[273] = 811;  
					yygh[274] = 812;  
					yygh[275] = 813;  
					yygh[276] = 814;  
					yygh[277] = 814;  
					yygh[278] = 814;  
					yygh[279] = 814;  
					yygh[280] = 814;  
					yygh[281] = 814;  
					yygh[282] = 814;  
					yygh[283] = 814;  
					yygh[284] = 814;  
					yygh[285] = 814;  
					yygh[286] = 830;  
					yygh[287] = 833;  
					yygh[288] = 833;  
					yygh[289] = 833;  
					yygh[290] = 833;  
					yygh[291] = 833;  
					yygh[292] = 833;  
					yygh[293] = 833;  
					yygh[294] = 833;  
					yygh[295] = 833;  
					yygh[296] = 833;  
					yygh[297] = 833;  
					yygh[298] = 833;  
					yygh[299] = 833;  
					yygh[300] = 833;  
					yygh[301] = 833;  
					yygh[302] = 833;  
					yygh[303] = 833;  
					yygh[304] = 833;  
					yygh[305] = 833;  
					yygh[306] = 833;  
					yygh[307] = 850;  
					yygh[308] = 856;  
					yygh[309] = 856;  
					yygh[310] = 856;  
					yygh[311] = 856;  
					yygh[312] = 856; 

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
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
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

			if (Regex.IsMatch(Rest,"^((?i)(IF_MIDDLE|IF_ANYKEY|IF_EQUALS|IF_PERIOD|IF_BRACKL|IF_BRACKR|IF_START|IF_RIGHT|IF_MSTOP|IF_SPACE|IF_PAUSE|IF_COMMA|IF_SEMIC|IF_SLASH|EACH_SEC|MESSAGES|IF_LOAD|IF_LEFT|IF_CTRL|IF_BKSP|IF_PGUP|IF_PGDN|IF_HOME|IF_BKSL|IF_ESC|IF_TAB|IF_ALT|IF_CUU|IF_CUD|IF_CUR|IF_CUL|IF_END|IF_INS|IF_DEL|IF_CAR|IF_CAL|PANELS|LAYERS|IF_SZ|IF_PLUS|IF_APO|IF_MINUS|(IF_F(1[0-2]|[1-9]))|IF_[0-9A-Z]))")){
				Results.Add (t_event);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(IF_MIDDLE|IF_ANYKEY|IF_EQUALS|IF_PERIOD|IF_BRACKL|IF_BRACKR|IF_START|IF_RIGHT|IF_MSTOP|IF_SPACE|IF_PAUSE|IF_COMMA|IF_SEMIC|IF_SLASH|EACH_SEC|MESSAGES|IF_LOAD|IF_LEFT|IF_CTRL|IF_BKSP|IF_PGUP|IF_PGDN|IF_HOME|IF_BKSL|IF_ESC|IF_TAB|IF_ALT|IF_CUU|IF_CUD|IF_CUR|IF_CUL|IF_END|IF_INS|IF_DEL|IF_CAR|IF_CAL|PANELS|LAYERS|IF_SZ|IF_PLUS|IF_APO|IF_MINUS|(IF_F(1[0-2]|[1-9]))|IF_[0-9A-Z]))").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)(ACOS|COS|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))")){
				Results.Add (t_math);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACOS|COS|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))")){
				Results.Add (t_flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(EACH_CYCLE_C|TEXTURE[1-4]|FLOOR_OFFS_X|FLOOR_OFFS_Y|CEIL_OFFS_X|CEIL_OFFS_Y|FLOOR_ANGLE|IF_RELEASE|SKILL[1-8]|EACH_CYCLE|CEIL_ANGLE|IF_ARRIVED|TARGET_MAP|MAP_COLOR|FLOOR_TEX|REL_ANGLE|OFFSET_X|OFFSET_Y|SCALE_XY|RADIANCE|IF_TOUCH|POSITION|DISTANCE|CEIL_TEX|IF_LEAVE|IF_ARISE|WAYPOINT|TARGET_X|TARGET_Y|REL_DIST|PALFILE|SCALE_X|SCALE_Y|SCYCLES|SCYCLE|IF_NEAR|IF_DIVE|PAN_MAP|VSLIDER|HSLIDER|PICTURE|STRINGS|DEFAULT|CYCLES|MIRROR|ALBEDO|SVDIST|ATTACH|LENGTH|SIZE_X|SIZE_Y|IF_FAR|GENIUS|TARGET|THING_HGT|HEIGHT|VSPEED|ASPEED|BUTTON|DIGITS|ASPECT|INDEX|RANGE|FLAGS|SIDES|FRAME|TITLE|DELAY|SDIST|POS_X|POS_Y|TOUCH|CYCLE|BELOW|ANGLE|SPEED|BMAPS|OVLYS|LAYER|RIGHT|SVOL|DIST|SIDE|VBAR|HBAR|MASK|LEFT|TYPE|TOP|VAL|MIN|MAX|X1|Y1|Z1|X2|Y2|Z2|X|Y|Z))")){
				Results.Add (t_property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(EACH_CYCLE_C|TEXTURE[1-4]|FLOOR_OFFS_X|FLOOR_OFFS_Y|CEIL_OFFS_X|CEIL_OFFS_Y|FLOOR_ANGLE|IF_RELEASE|SKILL[1-8]|EACH_CYCLE|CEIL_ANGLE|IF_ARRIVED|TARGET_MAP|MAP_COLOR|FLOOR_TEX|REL_ANGLE|OFFSET_X|OFFSET_Y|SCALE_XY|RADIANCE|IF_TOUCH|POSITION|DISTANCE|CEIL_TEX|IF_LEAVE|IF_ARISE|WAYPOINT|TARGET_X|TARGET_Y|REL_DIST|PALFILE|SCALE_X|SCALE_Y|SCYCLES|SCYCLE|IF_NEAR|IF_DIVE|PAN_MAP|VSLIDER|HSLIDER|PICTURE|STRINGS|DEFAULT|CYCLES|MIRROR|ALBEDO|SVDIST|ATTACH|LENGTH|SIZE_X|SIZE_Y|IF_FAR|GENIUS|TARGET|THING_HGT|HEIGHT|VSPEED|ASPEED|BUTTON|DIGITS|ASPECT|INDEX|RANGE|FLAGS|SIDES|FRAME|TITLE|DELAY|SDIST|POS_X|POS_Y|TOUCH|CYCLE|BELOW|ANGLE|SPEED|BMAPS|OVLYS|LAYER|RIGHT|SVOL|DIST|SIDE|VBAR|HBAR|MASK|LEFT|TYPE|TOP|VAL|MIN|MAX|X1|Y1|Z1|X2|Y2|Z2|X|Y|Z))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(WAIT_TICKS|EXEC_RULES|PLAY_SOUNDFILE|PLAY_SONG_ONCE|PLAY_FLICFILE|NEXT_MY_THERE|PRINT_STRING|PRINT_VALUE|PLAY_SOUND|STOP_SOUND|SET_STRING|SCREENSHOT|NEXT_THERE|ADD_STRING|RANDOMIZE|PLAY_SONG|PLAY_FLIC|STOP_FLIC|SAVE_INFO|LOAD_INFO|SAVE_DEMO|PLAY_DEMO|STOP_DEMO|EXCLUSIVE|PRINTFILE|SET_SKILL|TO_STRING|IF_NEQUAL|IF_ABOVE|IF_BELOW|IF_EQUAL|FADE_PAL|MIDI_COM|SET_INFO|SET_ALL|PLAY_CD|OUTPORT|SETMIDI|GETMIDI|EXPLODE|NEXT_MY|IF_MIN|IF_MAX|BRANCH|INPORT|FREEZE|LOCATE|ROTATE|ACCEL|WAITT|INKEY|PLACE|SHOOT|SHAKE|SHIFT|LEVEL|BREAK|ADDT|SUBT|SKIP|GOTO|CALL|WAIT|BEEP|FIND|DROP|PUSH|LIFT|TILT|LOAD|EXIT|SCAN|SET|ADD|SUB|AND|END|MAP|NOP))")){
				Results.Add (t_command);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(WAIT_TICKS|EXEC_RULES|PLAY_SOUNDFILE|PLAY_SONG_ONCE|PLAY_FLICFILE|NEXT_MY_THERE|PRINT_STRING|PRINT_VALUE|PLAY_SOUND|STOP_SOUND|SET_STRING|SCREENSHOT|NEXT_THERE|ADD_STRING|RANDOMIZE|PLAY_SONG|PLAY_FLIC|STOP_FLIC|SAVE_INFO|LOAD_INFO|SAVE_DEMO|PLAY_DEMO|STOP_DEMO|EXCLUSIVE|PRINTFILE|SET_SKILL|TO_STRING|IF_NEQUAL|IF_ABOVE|IF_BELOW|IF_EQUAL|FADE_PAL|MIDI_COM|SET_INFO|SET_ALL|PLAY_CD|OUTPORT|SETMIDI|GETMIDI|EXPLODE|NEXT_MY|IF_MIN|IF_MAX|BRANCH|INPORT|FREEZE|LOCATE|ROTATE|ACCEL|WAITT|INKEY|PLACE|SHOOT|SHAKE|SHIFT|LEVEL|BREAK|ADDT|SUBT|SKIP|GOTO|CALL|WAIT|BEEP|FIND|DROP|PUSH|LIFT|TILT|LOAD|EXIT|SCAN|SET|ADD|SUB|AND|END|MAP|NOP))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))")){
				Results.Add (t_list);
				ResultsV.Add(Regex.Match(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACTOR_IMPACT_VX|ACTOR_IMPACT_VY|ACTOR_IMPACT_VZ|ACTOR_FLOOR_HGT|ACTIVE_OBJTICKS|ACTOR_CEIL_HGT|ACTIVE_TARGETS|CHANNEL_[0-7]|PLAYER_LAST_X|PLAYER_LAST_Y|SCREEN_WIDTH|COLOR_PLAYER|COLOR_ACTORS|COLOR_THINGS|COLOR_BORDER|MOUSE_MOVING|PLAYER_LIGHT|PLAYER_WIDTH|PLAYER_CLIMB|SHOOT_SECTOR|PLAYER_ANGLE|PLAYER_SPEED|ACCELERATION|PLAYER_DEPTH|MOUSE_MIDDLE|FORCE_STRAFE|ACTIVE_NEXUS|MOTION_BLUR|RENDER_MODE|MAP_EDGE_X1|MAP_EDGE_X2|MAP_EDGE_Y1|MAP_EDGE_Y2|COLOR_WALLS|PANEL_LAYER|MOUSE_ANGLE|TOUCH_STATE|PLAYER_SIZE|WALK_PERIOD|WAVE_PERIOD|PSOUND_TONE|PLAYER_VROT|PLAYER_TILT|SHOOT_RANGE|HIT_MINDIST|SHOOT_ANGLE|SKIP_FRAMES|ACTOR_CLIMB|ACTOR_WIDTH|THING_WIDTH|IMPACT_VROT|SLOPE_AHEAD|DELTA_ANGLE|MOUSE_RIGHT|FORCE_AHEAD|SHIFT_SENSE|MOUSE_SENSE|MAP_CENTERX|MAP_CENTERY|CDAUDIO_VOL|SCREEN_HGT|SKY_OFFS_X|SKY_OFFS_Y|THING_DIST|ACTOR_DIST|MAP_OFFS_X|MAP_OFFS_Y|TEXT_LAYER|MOUSE_MODE|TOUCH_MODE|MOUSE_CALM|MOUSE_TIME|TOUCH_DIST|JOYSTICK_X|JOYSTICK_Y|LIGHT_DIST|PSOUND_VOL|PLAYER_ARC|PLAYER_SIN|PLAYER_COS|PLAYER_HGT|SLOPE_SIDE|MOVE_ANGLE|FLIC_FRAME|MOUSE_LEFT|KEY_EQUALS|KEY_PERIOD|KEY_BRACKL|KEY_BRACKR|FORCE_TILE|DEBUG_MODE|BLUR_MODE|MOVE_MODE|MAP_SCALE|MAP_LAYER|SOUND_VOL|MUSIC_VOL|DARK_DIST|WALK_TIME|PLAYER_VX|PLAYER_VY|PLAYER_VZ|SHOOT_FAC|IMPACT_VX|IMPACT_VY|IMPACT_VZ|BOUNCE_VX|BOUNCE_VY|TIME_CORR|KEY_SHIFT|KEY_SPACE|KEY_PAUSE|KEY_MINUS|KEY_ENTER|KEY_COMMA|KEY_SEMIC|KEY_SLASH|FORCE_ROT|KEY_SENSE|JOY_SENSE|LOAD_MODE|SCREEN_X|SCREEN_Y|CLIPPING|MAP_MAXX|MAP_MINX|MAP_MAXY|MAP_MINY|MAP_MODE|MICKEY_X|MICKEY_Y|FRICTION|HIT_DIST|PLAYER_X|PLAYER_Y|PLAYER_Z|REMOTE_0|REMOTE_1|TIME_FAC|CD_TRACK|KEY_CTRL|KEY_BKSP|KEY_PGUP|KEY_PGDN|KEY_HOME|KEY_PLUS|KEY_BKSL|FORCE_UP|MAX_DIST|MAP_ROT|MOUSE_X|MOUSE_Y|CHANNEL|INERTIA|SHOOT_X|SHOOT_Y|SLOPE_X|SLOPE_Y|KEY_ANY|KEY_ESC|KEY_TAB|KEY_ALT|KEY_CUU|KEY_CUD|KEY_CUR|KEY_CUL|KEY_END|KEY_INS|KEY_DEL|KEY_CAR|KEY_CAL|ACTIONS|STR_LEN|ASPECT|HIT_X|HIT_Y|TICKS|STEPS|JOY_4|ERROR|WALK|WAVE|NODE|SECS|KEY_SZ|KEY_APO|(KEY_F(1[0-2]|[1-9]))|KEY[A-Z0-9]))")){
				Results.Add (t_skill);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACTOR_IMPACT_VX|ACTOR_IMPACT_VY|ACTOR_IMPACT_VZ|ACTOR_FLOOR_HGT|ACTIVE_OBJTICKS|ACTOR_CEIL_HGT|ACTIVE_TARGETS|CHANNEL_[0-7]|PLAYER_LAST_X|PLAYER_LAST_Y|SCREEN_WIDTH|COLOR_PLAYER|COLOR_ACTORS|COLOR_THINGS|COLOR_BORDER|MOUSE_MOVING|PLAYER_LIGHT|PLAYER_WIDTH|PLAYER_CLIMB|SHOOT_SECTOR|PLAYER_ANGLE|PLAYER_SPEED|ACCELERATION|PLAYER_DEPTH|MOUSE_MIDDLE|FORCE_STRAFE|ACTIVE_NEXUS|MOTION_BLUR|RENDER_MODE|MAP_EDGE_X1|MAP_EDGE_X2|MAP_EDGE_Y1|MAP_EDGE_Y2|COLOR_WALLS|PANEL_LAYER|MOUSE_ANGLE|TOUCH_STATE|PLAYER_SIZE|WALK_PERIOD|WAVE_PERIOD|PSOUND_TONE|PLAYER_VROT|PLAYER_TILT|SHOOT_RANGE|HIT_MINDIST|SHOOT_ANGLE|SKIP_FRAMES|ACTOR_CLIMB|ACTOR_WIDTH|THING_WIDTH|IMPACT_VROT|SLOPE_AHEAD|DELTA_ANGLE|MOUSE_RIGHT|FORCE_AHEAD|SHIFT_SENSE|MOUSE_SENSE|MAP_CENTERX|MAP_CENTERY|CDAUDIO_VOL|SCREEN_HGT|SKY_OFFS_X|SKY_OFFS_Y|THING_DIST|ACTOR_DIST|MAP_OFFS_X|MAP_OFFS_Y|TEXT_LAYER|MOUSE_MODE|TOUCH_MODE|MOUSE_CALM|MOUSE_TIME|TOUCH_DIST|JOYSTICK_X|JOYSTICK_Y|LIGHT_DIST|PSOUND_VOL|PLAYER_ARC|PLAYER_SIN|PLAYER_COS|PLAYER_HGT|SLOPE_SIDE|MOVE_ANGLE|FLIC_FRAME|MOUSE_LEFT|KEY_EQUALS|KEY_PERIOD|KEY_BRACKL|KEY_BRACKR|FORCE_TILE|DEBUG_MODE|BLUR_MODE|MOVE_MODE|MAP_SCALE|MAP_LAYER|SOUND_VOL|MUSIC_VOL|DARK_DIST|WALK_TIME|PLAYER_VX|PLAYER_VY|PLAYER_VZ|SHOOT_FAC|IMPACT_VX|IMPACT_VY|IMPACT_VZ|BOUNCE_VX|BOUNCE_VY|TIME_CORR|KEY_SHIFT|KEY_SPACE|KEY_PAUSE|KEY_MINUS|KEY_ENTER|KEY_COMMA|KEY_SEMIC|KEY_SLASH|FORCE_ROT|KEY_SENSE|JOY_SENSE|LOAD_MODE|SCREEN_X|SCREEN_Y|CLIPPING|MAP_MAXX|MAP_MINX|MAP_MAXY|MAP_MINY|MAP_MODE|MICKEY_X|MICKEY_Y|FRICTION|HIT_DIST|PLAYER_X|PLAYER_Y|PLAYER_Z|REMOTE_0|REMOTE_1|TIME_FAC|CD_TRACK|KEY_CTRL|KEY_BKSP|KEY_PGUP|KEY_PGDN|KEY_HOME|KEY_PLUS|KEY_BKSL|FORCE_UP|MAX_DIST|MAP_ROT|MOUSE_X|MOUSE_Y|CHANNEL|INERTIA|SHOOT_X|SHOOT_Y|SLOPE_X|SLOPE_Y|KEY_ANY|KEY_ESC|KEY_TAB|KEY_ALT|KEY_CUU|KEY_CUD|KEY_CUR|KEY_CUL|KEY_END|KEY_INS|KEY_DEL|KEY_CAR|KEY_CAL|ACTIONS|STR_LEN|ASPECT|HIT_X|HIT_Y|TICKS|STEPS|JOY_4|ERROR|WALK|WAVE|NODE|SECS|KEY_SZ|KEY_APO|(KEY_F(1[0-2]|[1-9]))|KEY[A-Z0-9]))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE))")){
				Results.Add (t_synonym);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)CLIP_DIST)")){
				Results.Add (t_ambigChar95globalChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)CLIP_DIST)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(EACH_TICK|IF_ENTER|IF_HIT|IF_KLICK))")){
				Results.Add (t_ambigChar95eventChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(EACH_TICK|IF_ENTER|IF_HIT|IF_KLICK))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(THING|ACTOR))")){
				Results.Add (t_ambigChar95objectChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(THING|ACTOR))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(SIN|ASIN|SQRT|ABS))")){
				Results.Add (t_ambigChar95mathChar95command);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(SIN|ASIN|SQRT|ABS))").Value);}

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
				Results.Add (t_ambigChar95globalChar95synonymChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)MSPRITE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)DO)")){
				Results.Add (t_ambigChar95commandChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)DO)").Value);}

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
