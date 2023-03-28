using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WDL2CS;

namespace VCCCompiler
{
    /// <summary>
    /// Zusammenfassung für MyCompiler.
    /// </summary>
    class MyCompiler
    {
        List<int> tList = new List<int>();
        List<Regex> rList = new List<Regex>();
        MyCompiler()
        {
            tList.Add(t_IFDEF);
            rList.Add(new Regex("^((?i)IFDEF)"));
            tList.Add(t_Char59);
            rList.Add(new Regex("^((,*[\\s\\t\\x00]*)?;)"));
            tList.Add(t_IFNDEF);
            rList.Add(new Regex("^((?i)IFNDEF)"));
            tList.Add(t_IFELSE);
            rList.Add(new Regex("^((?i)IFELSE)"));
            tList.Add(t_ENDIF);
            rList.Add(new Regex("^((?i)ENDIF)"));
            tList.Add(t_DEFINE);
            rList.Add(new Regex("^((?i)DEFINE)"));
            tList.Add(t_Char44);
            rList.Add(new Regex("^(,|:=)"));
            tList.Add(t_UNDEF);
            rList.Add(new Regex("^((?i)UNDEF)"));
            tList.Add(t_INCLUDE);
            rList.Add(new Regex("^((?i)INCLUDE)"));
            tList.Add(t_Char123);
            rList.Add(new Regex("^(\\{)"));
            tList.Add(t_Char125);
            rList.Add(new Regex("^(\\})"));
            tList.Add(t_Char58);
            rList.Add(new Regex("^(:)"));
            tList.Add(t_Char124Char124);
            rList.Add(new Regex("^(\\|\\|)"));
            tList.Add(t_Char38Char38);
            rList.Add(new Regex("^(&&)"));
            tList.Add(t_Char124);
            rList.Add(new Regex("^(\\|)"));
            tList.Add(t_Char94);
            rList.Add(new Regex("^(\\^)"));
            tList.Add(t_Char38);
            rList.Add(new Regex("^(&)"));
            tList.Add(t_Char40);
            rList.Add(new Regex("^(\\()"));
            tList.Add(t_Char41);
            rList.Add(new Regex("^(\\))"));
            tList.Add(t_Char33Char61);
            rList.Add(new Regex("^(!=)"));
            tList.Add(t_Char61Char61);
            rList.Add(new Regex("^(==)"));
            tList.Add(t_Char60);
            rList.Add(new Regex("^(<)"));
            tList.Add(t_Char60Char61);
            rList.Add(new Regex("^(<=)"));
            tList.Add(t_Char62);
            rList.Add(new Regex("^(>)"));
            tList.Add(t_Char62Char61);
            rList.Add(new Regex("^(>=)"));
            tList.Add(t_Char43);
            rList.Add(new Regex("^(\\+)"));
            tList.Add(t_Char45);
            rList.Add(new Regex("^(\\-)"));
            tList.Add(t_Char37);
            rList.Add(new Regex("^(%)"));
            tList.Add(t_Char42);
            rList.Add(new Regex("^(\\*)"));
            tList.Add(t_Char47);
            rList.Add(new Regex("^(/)"));
            tList.Add(t_Char33);
            rList.Add(new Regex("^(!)"));
            tList.Add(t_RULE);
            rList.Add(new Regex("^((?i)RULE)"));
            tList.Add(t_Char42Char61);
            rList.Add(new Regex("^(\\*[\\s\\t\\x00]*=)"));
            tList.Add(t_Char43Char61);
            rList.Add(new Regex("^(\\+[\\s\\t\\x00]*=)"));
            tList.Add(t_Char45Char61);
            rList.Add(new Regex("^(\\-[\\s\\t\\x00]*=)"));
            tList.Add(t_Char47Char61);
            rList.Add(new Regex("^(/[\\s\\t\\x00]*=)"));
            tList.Add(t_Char61);
            rList.Add(new Regex("^(=)"));
            tList.Add(t_ELSE);
            rList.Add(new Regex("^((?i)ELSE)"));
            tList.Add(t_IF);
            rList.Add(new Regex("^((?i)IF)"));
            tList.Add(t_WHILE);
            rList.Add(new Regex("^((?i)WHILE)"));
            tList.Add(t_Char46);
            rList.Add(new Regex("^(\\.)"));
            tList.Add(t_NULL);
            rList.Add(new Regex("^((?i)NULL)"));
            tList.Add(t_event);
            rList.Add(new Regex("^((?i)(EACH_SEC|IF_(ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))"));
            tList.Add(t_global);
            rList.Add(new Regex("^((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER|SAVE_KEYS|REMOTE_KEYS))"));
            tList.Add(t_asset);
            rList.Add(new Regex("^((?i)(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))"));
            tList.Add(t_object);
            rList.Add(new Regex("^((?i)(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))"));
            tList.Add(t_function);
            rList.Add(new Regex("^((?i)(ACTION|RULES))"));
            tList.Add(t_math);
            rList.Add(new Regex("^((?i)(ACOS|COS|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))"));
            tList.Add(t_flag);
            rList.Add(new Regex("^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))"));
            tList.Add(t_property);
            rList.Add(new Regex("^((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_C|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))"));
            tList.Add(t_command);
            rList.Add(new Regex("^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))"));
            tList.Add(t_list);
            rList.Add(new Regex("^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))"));
            tList.Add(t_skill);
            rList.Add(new Regex("^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))"));
            tList.Add(t_synonym);
            rList.Add(new Regex("^((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE))"));
            tList.Add(t_ambigChar95globalChar95property);
            rList.Add(new Regex("^((?i)CLIP_DIST)"));
            tList.Add(t_ambigChar95eventChar95property);
            rList.Add(new Regex("^((?i)(EACH_TICK|IF_ENTER|IF_HIT|IF_KLICK))"));
            tList.Add(t_ambigChar95objectChar95flag);
            rList.Add(new Regex("^((?i)(THING|ACTOR))"));
            tList.Add(t_ambigChar95mathChar95command);
            rList.Add(new Regex("^((?i)(SIN|ASIN|SQRT|ABS))"));
            tList.Add(t_ambigChar95mathChar95skillChar95property);
            rList.Add(new Regex("^((?i)RANDOM)"));
            tList.Add(t_ambigChar95synonymChar95flag);
            rList.Add(new Regex("^((?i)HERE)"));
            tList.Add(t_ambigChar95skillChar95property);
            rList.Add(new Regex("^((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT|NODE))"));
            tList.Add(t_ambigChar95commandChar95flag);
            rList.Add(new Regex("^((?i)SAVE)"));
            tList.Add(t_ambigChar95globalChar95synonymChar95property);
            rList.Add(new Regex("^((?i)MSPRITE)"));
            tList.Add(t_ambigChar95commandChar95property);
            rList.Add(new Regex("^((?i)DO)"));
            tList.Add(t_integer);
            rList.Add(new Regex("^([0-9]+)"));
            tList.Add(t_fixed);
            rList.Add(new Regex("^(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))"));
            tList.Add(t_identifier);
            rList.Add(new Regex("^([A-Za-z0-9_][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)"));
            tList.Add(t_file);
            rList.Add(new Regex("^(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)"));
            tList.Add(t_string);
            rList.Add(new Regex("^(\"(\\\\\"|.|[\\r\\n])*?\")"));
            tList.Add(t_ignore);
            rList.Add(new Regex("^([\\r\\n\\t\\s\\x00]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))"));
        }
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
        int yyfnone = 0;
        int[] yys = new int[4096];
        string[] yyv = new string[4096];

        string yyval = "";

        FileStream OutputStream;
        //StreamWriter Output;
        TextWriter Output;
        bool DeserializeOutput = false;
        string Scriptname = "Script";

        class YYARec
        {
            public int sym;
            public int act;
            public YYARec(int s, int a) { sym = s; act = a; }
        }

        class YYRRec
        {
            public int len;
            public int sym;
            public YYRRec(int l, int s) { sym = s; len = l; }
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
            string Scriptname = "";

            foreach (string s in args)
            {
                if (s.ToLower() == "-t")
                {
                    ShowTokens = true;
                }
                else
                {
                    if (InputFilename == "") InputFilename = s;
                    else
                    if (OutputFilename == "") OutputFilename = s;
                    else
                    if (Scriptname == "") Scriptname = s;
                    else
                    {
                        Console.WriteLine("Too many arguments!");
                        return 1;
                    }
                }
            }
            if (InputFilename == "")
            {
                System.Console.WriteLine("You need to specify input and (optional) outputfile: compiler.exe input.txt [output.txt] [C# Class Identifier]");
                return 1;
            }
            if (Scriptname == "")
            {
                Scriptname = Path.GetFileNameWithoutExtension(InputFilename);
            }

            StreamReader in_s = File.OpenText(InputFilename);
            string inputstream = in_s.ReadToEnd();
            in_s.Close();

            ////////////////////////////////////////////////////////////////
            /// Compiler Code:
            ////////////////////////////////////////////////////////////////
            MyCompiler compiler = new MyCompiler();
            compiler.Output = null;
            compiler.DeserializeOutput = true;
            compiler.Scriptname = Scriptname;
            if (OutputFilename != "")
            {
                File.Delete(OutputFilename);
                compiler.OutputStream = File.OpenWrite(OutputFilename);
                compiler.Output = new StreamWriter(compiler.OutputStream, new System.Text.UTF8Encoding(false));
            }
            else
            {
                compiler.Output = new StreamWriter(Console.OpenStandardOutput(), new System.Text.UTF8Encoding(false));

            }

            //                        if (!compiler.Scanner(inputstream)) return 1;
            if (!compiler.ScannerOpt(inputstream)) return 1;
            if (ShowTokens)
            {
                foreach (AToken t in compiler.TokenList)
                {
                    Console.WriteLine("TokenID: " + t.token + "  =  " + t.val);
                }
            }
            compiler.InitTables();
            if (!compiler.yyparse()) return 1;

            if (compiler.Output != null) compiler.Output.Close();
            return 0;
        }
        public void yyaction(int yyruleno)
        {
            switch (yyruleno)
            {
                ////////////////////////////////////////////////////////////////
                /// YYAction code:
                ////////////////////////////////////////////////////////////////
							case    1 : 
         yyval = yyv[yysp-0];
         if (DeserializeOutput)
         Output.WriteLine(Script.Format(Scriptname, yyval));
         else
         Output.Write(yyval);
         
       break;
							case    2 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case    3 : 
         yyval = "";
         
       break;
							case    4 : 
         yyval = "";
         
       break;
							case    5 : 
         yyval = Sections.AddDummySection(yyv[yysp-0]);
         
       break;
							case    6 : 
         yyval = yyv[yysp-0];
         
       break;
							case    7 : 
         yyval = Sections.AddGlobalSection(yyv[yysp-0]);
         
       break;
							case    8 : 
         yyval = yyv[yysp-0];
         
       break;
							case    9 : 
         yyval = Sections.AddDefineSection(yyv[yysp-0]);
         
       break;
							case   10 : 
         yyval = Sections.AddObjectSection(yyv[yysp-0]);
         
       break;
							case   11 : 
         yyval = Sections.AddActionSection(yyv[yysp-0]);
         
       break;
							case   12 : 
         yyval = Sections.AddAssetSection(yyv[yysp-0]);
         
       break;
							case   13 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   14 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   15 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcElseCondition(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   16 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   17 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcIfCondition(yyv[yysp-1], yyv[yysp-0]);
         
       break;
							case   18 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcIfNotCondition(yyv[yysp-1], yyv[yysp-0]);
         
       break;
							case   19 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcElseCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   20 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   21 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   22 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   23 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcElseCondition(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   24 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   25 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.AddTransform(yyv[yysp-3]);
         
       break;
							case   26 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.AddDefine(yyv[yysp-1]);
         
       break;
							case   27 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.RemoveDefine(yyv[yysp-1]);
         
       break;
							case   28 : 
         yyval = Formatter.FormatPreprocessor(yyv[yysp-0]);
         
       break;
							case   29 : 
         yyval = Formatter.FormatPreprocessor(yyv[yysp-0]);
         
       break;
							case   30 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddStringDefine(yyv[yysp-0]);
         
       break;
							case   31 : 
         //yyval = yyv[yysp-0];
         Defines.AddListDefine(yyv[yysp-0]);
         
       break;
							case   32 : 
         //yyval = yyv[yysp-0];
         Defines.AddFileDefine(yyv[yysp-0]);
         
       break;
							case   33 : 
         //yyval = yyv[yysp-0];
         Defines.AddNumberDefine(yyv[yysp-0]);
         
       break;
							case   34 : 
         //yyval = yyv[yysp-0];
         Defines.AddKeywordDefine(yyv[yysp-0]);
         
       break;
							case   35 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Include.Process(yyv[yysp-1]);
         
       break;
							case   36 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Globals.AddEvent(yyv[yysp-3]);
         
       break;
							case   37 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Globals.AddGlobal(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   38 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   39 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   40 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   41 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
       break;
							case   42 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   43 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   44 : 
         //yyval = yyv[yysp-1];
         yyval = "";
         Globals.AddParameter(yyv[yysp-1]);
         
       break;
							case   45 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Globals.AddParameter(yyv[yysp-2]);
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   50 : 
         //yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Assets.AddAsset(yyv[yysp-6], yyv[yysp-5], yyv[yysp-3]);
         
       break;
							case   51 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Assets.AddParameter(yyv[yysp-2]);
         
       break;
							case   52 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Assets.AddParameter(yyv[yysp-0]);
         
       break;
							case   53 : 
         yyval = "";
         
       break;
							case   54 : 
         yyval = Formatter.FormatObject(yyv[yysp-0]);
         
       break;
							case   55 : 
         //yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddObject(yyv[yysp-5], yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   56 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddStringObject(yyv[yysp-4], yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   57 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddObject(yyv[yysp-2], yyv[yysp-1]);
         
       break;
							case   58 : 
         yyval = yyv[yysp-0];
         
       break;
							case   59 : 
         yyval = Formatter.FormatObject(yyv[yysp-0]);
         
       break;
							case   60 : 
         yyval = Formatter.FormatObject(yyv[yysp-0]);
         
       break;
							case   61 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   62 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   63 : 
         yyval = "";
         
       break;
							case   64 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreateProperty(yyv[yysp-2]);
         
       break;
							case   65 : 
         Objects.AddPropertyValue(yyv[yysp-1]);
         yyval = "";
         
       break;
							case   66 : 
         Objects.AddPropertyValue(yyv[yysp-2]);
         yyval = "";
         
       break;
							case   67 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case   68 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case   69 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case   70 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
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
         yyval = Formatter.FormatPropertyValue(yyv[yysp-0]);
         
       break;
							case   78 : 
         //yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.AddAction(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   79 : 
         yyval = yyv[yysp-0];
         
       break;
							case   80 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   81 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   82 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   83 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   84 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   85 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   86 : 
         yyval = "";
         
       break;
							case   87 : 
         yyval = "";
         
       break;
							case   88 : 
         yyval = yyv[yysp-1];
         
       break;
							case   89 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1];
         yyval = Actions.CreateInstruction(yyv[yysp-3]);
         
       break;
							case   90 : 
         //yyval = yyv[yysp-1];
         yyval = Actions.CreateInstruction(yyv[yysp-1]);
         
       break;
							case   91 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-1]);
         
       break;
							case   92 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-2]);
         
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
         yyval = "";
         
       break;
							case  100 : 
         yyval = yyv[yysp-0];
         
       break;
							case  101 : 
         yyval = yyv[yysp-2] + " || " + yyv[yysp-0];
         
       break;
							case  102 : 
         yyval = yyv[yysp-0];
         
       break;
							case  103 : 
         yyval = yyv[yysp-2] + " && " + yyv[yysp-0];
         
       break;
							case  104 : 
         yyval = yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-2] + " | " + yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-0];
         
       break;
							case  107 : 
         yyval = yyv[yysp-2] + " ^ " + yyv[yysp-0];
         
       break;
							case  108 : 
         yyval = yyv[yysp-0];
         
       break;
							case  109 : 
         yyval = yyv[yysp-2] + " & " + yyv[yysp-0];
         
       break;
							case  110 : 
         yyval = yyv[yysp-0];
         
       break;
							case  111 : 
         yyval = yyv[yysp-0];
         
       break;
							case  112 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  113 : 
         yyval = yyv[yysp-0];
         
       break;
							case  114 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  115 : 
         yyval = yyv[yysp-0];
         
       break;
							case  116 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  117 : 
         yyval = yyv[yysp-0];
         
       break;
							case  118 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  119 : 
         yyval = yyv[yysp-0];
         
       break;
							case  120 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  121 : 
         yyval = Formatter.FormatMath(yyv[yysp-3]) + "(" + yyv[yysp-1] + ")";
         
       break;
							case  122 : 
         yyval = "(" + yyv[yysp-1] + ")";
         
       break;
							case  123 : 
         yyval = yyv[yysp-0];
         
       break;
							case  124 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
       break;
							case  125 : 
         yyval = " != ";
         
       break;
							case  126 : 
         yyval = " == ";
         
       break;
							case  127 : 
         yyval = " < ";
         
       break;
							case  128 : 
         yyval = " <= ";
         
       break;
							case  129 : 
         yyval = " > ";
         
       break;
							case  130 : 
         yyval = " >= ";
         
       break;
							case  131 : 
         yyval = " + ";
         
       break;
							case  132 : 
         yyval = " - ";
         
       break;
							case  133 : 
         yyval = " % ";
         
       break;
							case  134 : 
         yyval = " * ";
         
       break;
							case  135 : 
         yyval = " / ";
         
       break;
							case  136 : 
         yyval = "!";
         
       break;
							case  137 : 
         yyval = "+";
         
       break;
							case  138 : 
         yyval = "-";
         
       break;
							case  139 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(yyv[yysp-0]);
         
       break;
							case  140 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  141 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  142 : 
         yyval = " *= ";
         
       break;
							case  143 : 
         yyval = " += ";
         
       break;
							case  144 : 
         yyval = " -= ";
         
       break;
							case  145 : 
         yyval = " /= ";
         
       break;
							case  146 : 
         yyval = " = ";
         
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
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateElseCondition(yyv[yysp-1]);
         
       break;
							case  151 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  152 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateWhileCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  153 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  154 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  155 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  156 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  157 : 
         yyval = yyv[yysp-0];
         
       break;
							case  158 : 
         yyval = Formatter.FormatObjectId(Formatter.FormatIdentifier(yyv[yysp-0]));
         
       break;
							case  159 : 
         yyval = Formatter.FormatObjectId(yyv[yysp-0]);
         
       break;
							case  160 : 
         yyval = yyv[yysp-0];
         
       break;
							case  161 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  162 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  163 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  164 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  165 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  166 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  167 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  168 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  169 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  170 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  171 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  172 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  173 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  174 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  175 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  176 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  177 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
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
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  182 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  183 : 
         yyval = yyv[yysp-0];
         
       break;
							case  184 : 
         yyval = yyv[yysp-0];
         
       break;
							case  185 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  186 : 
         yyval = yyv[yysp-0];
         
       break;
							case  187 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  188 : 
         yyval = yyv[yysp-0];
         
       break;
							case  189 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  190 : 
         yyval = Formatter.FormatList(yyv[yysp-0]);
         
       break;
							case  191 : 
         yyval = Formatter.FormatAssetId(yyv[yysp-0]);
         
       break;
							case  192 : 
         yyval = yyv[yysp-0];
         
       break;
							case  193 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  194 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  195 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  196 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  197 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  198 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  199 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  200 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  201 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  202 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  203 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  204 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  205 : 
         yyval = yyv[yysp-0];
         
       break;
							case  206 : 
         yyval = Formatter.FormatNull();
         
       break;
							case  207 : 
         //yyval = yyv[yysp-0];
         yyval = Formatter.FormatPatchFunction(yyv[yysp-0]);
         
       break;
							case  208 : 
         //yyval = yyv[yysp-0];
         yyval = Formatter.FormatFunction(yyv[yysp-0]);
         
       break;
							case  209 : 
         yyval = yyv[yysp-0];
         
       break;
							case  210 : 
         yyval = yyv[yysp-0];
         
       break;
							case  211 : 
         yyval = Formatter.FormatFile(yyv[yysp-0]);
         
       break;
							case  212 : 
         yyval = Formatter.FormatString(yyv[yysp-0]);
         
       break;
               default: return;
            }
        }

        public void InitTables()
        {
            ////////////////////////////////////////////////////////////////
            /// Init Table code:
            ////////////////////////////////////////////////////////////////

					int yynacts   = 2139;
					int yyngotos  = 913;
					int yynstates = 331;
					int yynrules  = 212;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(258,19);yyac++; 
					yya[yyac] = new YYARec(259,20);yyac++; 
					yya[yyac] = new YYARec(262,21);yyac++; 
					yya[yyac] = new YYARec(264,22);yyac++; 
					yya[yyac] = new YYARec(265,23);yyac++; 
					yya[yyac] = new YYARec(267,24);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,26);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(311,31);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(319,35);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,57);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(263,59);yyac++; 
					yya[yyac] = new YYARec(282,-99 );yyac++; 
					yya[yyac] = new YYARec(283,-99 );yyac++; 
					yya[yyac] = new YYARec(287,-99 );yyac++; 
					yya[yyac] = new YYARec(321,-99 );yyac++; 
					yya[yyac] = new YYARec(322,-99 );yyac++; 
					yya[yyac] = new YYARec(323,-99 );yyac++; 
					yya[yyac] = new YYARec(324,-99 );yyac++; 
					yya[yyac] = new YYARec(325,-99 );yyac++; 
					yya[yyac] = new YYARec(263,59);yyac++; 
					yya[yyac] = new YYARec(298,-99 );yyac++; 
					yya[yyac] = new YYARec(300,-99 );yyac++; 
					yya[yyac] = new YYARec(301,-99 );yyac++; 
					yya[yyac] = new YYARec(304,-99 );yyac++; 
					yya[yyac] = new YYARec(305,-99 );yyac++; 
					yya[yyac] = new YYARec(306,-99 );yyac++; 
					yya[yyac] = new YYARec(307,-99 );yyac++; 
					yya[yyac] = new YYARec(309,-99 );yyac++; 
					yya[yyac] = new YYARec(314,-99 );yyac++; 
					yya[yyac] = new YYARec(315,-99 );yyac++; 
					yya[yyac] = new YYARec(317,-99 );yyac++; 
					yya[yyac] = new YYARec(318,-99 );yyac++; 
					yya[yyac] = new YYARec(320,-99 );yyac++; 
					yya[yyac] = new YYARec(323,-99 );yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(258,19);yyac++; 
					yya[yyac] = new YYARec(259,20);yyac++; 
					yya[yyac] = new YYARec(262,21);yyac++; 
					yya[yyac] = new YYARec(264,22);yyac++; 
					yya[yyac] = new YYARec(265,23);yyac++; 
					yya[yyac] = new YYARec(267,24);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,26);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(311,31);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(319,35);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(324,70);yyac++; 
					yya[yyac] = new YYARec(263,59);yyac++; 
					yya[yyac] = new YYARec(266,-99 );yyac++; 
					yya[yyac] = new YYARec(258,73);yyac++; 
					yya[yyac] = new YYARec(263,59);yyac++; 
					yya[yyac] = new YYARec(266,-99 );yyac++; 
					yya[yyac] = new YYARec(325,-99 );yyac++; 
					yya[yyac] = new YYARec(263,59);yyac++; 
					yya[yyac] = new YYARec(324,-99 );yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(321,86);yyac++; 
					yya[yyac] = new YYARec(322,87);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(324,70);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(258,91);yyac++; 
					yya[yyac] = new YYARec(258,92);yyac++; 
					yya[yyac] = new YYARec(258,93);yyac++; 
					yya[yyac] = new YYARec(263,94);yyac++; 
					yya[yyac] = new YYARec(258,95);yyac++; 
					yya[yyac] = new YYARec(258,96);yyac++; 
					yya[yyac] = new YYARec(266,97);yyac++; 
					yya[yyac] = new YYARec(266,99);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(324,70);yyac++; 
					yya[yyac] = new YYARec(321,101);yyac++; 
					yya[yyac] = new YYARec(322,102);yyac++; 
					yya[yyac] = new YYARec(258,103);yyac++; 
					yya[yyac] = new YYARec(263,59);yyac++; 
					yya[yyac] = new YYARec(258,-99 );yyac++; 
					yya[yyac] = new YYARec(298,-99 );yyac++; 
					yya[yyac] = new YYARec(300,-99 );yyac++; 
					yya[yyac] = new YYARec(301,-99 );yyac++; 
					yya[yyac] = new YYARec(304,-99 );yyac++; 
					yya[yyac] = new YYARec(305,-99 );yyac++; 
					yya[yyac] = new YYARec(306,-99 );yyac++; 
					yya[yyac] = new YYARec(307,-99 );yyac++; 
					yya[yyac] = new YYARec(309,-99 );yyac++; 
					yya[yyac] = new YYARec(314,-99 );yyac++; 
					yya[yyac] = new YYARec(315,-99 );yyac++; 
					yya[yyac] = new YYARec(317,-99 );yyac++; 
					yya[yyac] = new YYARec(318,-99 );yyac++; 
					yya[yyac] = new YYARec(320,-99 );yyac++; 
					yya[yyac] = new YYARec(323,-99 );yyac++; 
					yya[yyac] = new YYARec(258,105);yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(258,19);yyac++; 
					yya[yyac] = new YYARec(259,20);yyac++; 
					yya[yyac] = new YYARec(262,21);yyac++; 
					yya[yyac] = new YYARec(264,22);yyac++; 
					yya[yyac] = new YYARec(265,23);yyac++; 
					yya[yyac] = new YYARec(267,24);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,26);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(311,31);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(319,35);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(258,19);yyac++; 
					yya[yyac] = new YYARec(259,20);yyac++; 
					yya[yyac] = new YYARec(262,21);yyac++; 
					yya[yyac] = new YYARec(264,22);yyac++; 
					yya[yyac] = new YYARec(265,23);yyac++; 
					yya[yyac] = new YYARec(267,24);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,26);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(311,31);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(319,35);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(308,119);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,86);yyac++; 
					yya[yyac] = new YYARec(322,87);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(324,70);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(257,133);yyac++; 
					yya[yyac] = new YYARec(258,134);yyac++; 
					yya[yyac] = new YYARec(259,135);yyac++; 
					yya[yyac] = new YYARec(266,136);yyac++; 
					yya[yyac] = new YYARec(288,137);yyac++; 
					yya[yyac] = new YYARec(294,138);yyac++; 
					yya[yyac] = new YYARec(295,139);yyac++; 
					yya[yyac] = new YYARec(296,140);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,141);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,142);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(267,-86 );yyac++; 
					yya[yyac] = new YYARec(258,144);yyac++; 
					yya[yyac] = new YYARec(257,152);yyac++; 
					yya[yyac] = new YYARec(259,153);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,154);yyac++; 
					yya[yyac] = new YYARec(311,155);yyac++; 
					yya[yyac] = new YYARec(312,156);yyac++; 
					yya[yyac] = new YYARec(315,157);yyac++; 
					yya[yyac] = new YYARec(317,158);yyac++; 
					yya[yyac] = new YYARec(319,159);yyac++; 
					yya[yyac] = new YYARec(320,160);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(267,-63 );yyac++; 
					yya[yyac] = new YYARec(263,59);yyac++; 
					yya[yyac] = new YYARec(258,-99 );yyac++; 
					yya[yyac] = new YYARec(282,-99 );yyac++; 
					yya[yyac] = new YYARec(283,-99 );yyac++; 
					yya[yyac] = new YYARec(287,-99 );yyac++; 
					yya[yyac] = new YYARec(321,-99 );yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(258,-44 );yyac++; 
					yya[yyac] = new YYARec(258,163);yyac++; 
					yya[yyac] = new YYARec(260,164);yyac++; 
					yya[yyac] = new YYARec(261,165);yyac++; 
					yya[yyac] = new YYARec(258,166);yyac++; 
					yya[yyac] = new YYARec(297,167);yyac++; 
					yya[yyac] = new YYARec(258,-160 );yyac++; 
					yya[yyac] = new YYARec(263,-160 );yyac++; 
					yya[yyac] = new YYARec(266,-160 );yyac++; 
					yya[yyac] = new YYARec(269,-160 );yyac++; 
					yya[yyac] = new YYARec(270,-160 );yyac++; 
					yya[yyac] = new YYARec(271,-160 );yyac++; 
					yya[yyac] = new YYARec(272,-160 );yyac++; 
					yya[yyac] = new YYARec(273,-160 );yyac++; 
					yya[yyac] = new YYARec(275,-160 );yyac++; 
					yya[yyac] = new YYARec(276,-160 );yyac++; 
					yya[yyac] = new YYARec(277,-160 );yyac++; 
					yya[yyac] = new YYARec(278,-160 );yyac++; 
					yya[yyac] = new YYARec(279,-160 );yyac++; 
					yya[yyac] = new YYARec(280,-160 );yyac++; 
					yya[yyac] = new YYARec(281,-160 );yyac++; 
					yya[yyac] = new YYARec(282,-160 );yyac++; 
					yya[yyac] = new YYARec(283,-160 );yyac++; 
					yya[yyac] = new YYARec(284,-160 );yyac++; 
					yya[yyac] = new YYARec(285,-160 );yyac++; 
					yya[yyac] = new YYARec(286,-160 );yyac++; 
					yya[yyac] = new YYARec(287,-160 );yyac++; 
					yya[yyac] = new YYARec(289,-160 );yyac++; 
					yya[yyac] = new YYARec(290,-160 );yyac++; 
					yya[yyac] = new YYARec(291,-160 );yyac++; 
					yya[yyac] = new YYARec(292,-160 );yyac++; 
					yya[yyac] = new YYARec(293,-160 );yyac++; 
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
					yya[yyac] = new YYARec(297,168);yyac++; 
					yya[yyac] = new YYARec(258,-159 );yyac++; 
					yya[yyac] = new YYARec(263,-159 );yyac++; 
					yya[yyac] = new YYARec(266,-159 );yyac++; 
					yya[yyac] = new YYARec(269,-159 );yyac++; 
					yya[yyac] = new YYARec(270,-159 );yyac++; 
					yya[yyac] = new YYARec(271,-159 );yyac++; 
					yya[yyac] = new YYARec(272,-159 );yyac++; 
					yya[yyac] = new YYARec(273,-159 );yyac++; 
					yya[yyac] = new YYARec(275,-159 );yyac++; 
					yya[yyac] = new YYARec(276,-159 );yyac++; 
					yya[yyac] = new YYARec(277,-159 );yyac++; 
					yya[yyac] = new YYARec(278,-159 );yyac++; 
					yya[yyac] = new YYARec(279,-159 );yyac++; 
					yya[yyac] = new YYARec(280,-159 );yyac++; 
					yya[yyac] = new YYARec(281,-159 );yyac++; 
					yya[yyac] = new YYARec(282,-159 );yyac++; 
					yya[yyac] = new YYARec(283,-159 );yyac++; 
					yya[yyac] = new YYARec(284,-159 );yyac++; 
					yya[yyac] = new YYARec(285,-159 );yyac++; 
					yya[yyac] = new YYARec(286,-159 );yyac++; 
					yya[yyac] = new YYARec(287,-159 );yyac++; 
					yya[yyac] = new YYARec(289,-159 );yyac++; 
					yya[yyac] = new YYARec(290,-159 );yyac++; 
					yya[yyac] = new YYARec(291,-159 );yyac++; 
					yya[yyac] = new YYARec(292,-159 );yyac++; 
					yya[yyac] = new YYARec(293,-159 );yyac++; 
					yya[yyac] = new YYARec(298,-159 );yyac++; 
					yya[yyac] = new YYARec(299,-159 );yyac++; 
					yya[yyac] = new YYARec(300,-159 );yyac++; 
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
					yya[yyac] = new YYARec(325,-159 );yyac++; 
					yya[yyac] = new YYARec(258,169);yyac++; 
					yya[yyac] = new YYARec(258,-177 );yyac++; 
					yya[yyac] = new YYARec(263,-177 );yyac++; 
					yya[yyac] = new YYARec(282,-177 );yyac++; 
					yya[yyac] = new YYARec(283,-177 );yyac++; 
					yya[yyac] = new YYARec(287,-177 );yyac++; 
					yya[yyac] = new YYARec(298,-177 );yyac++; 
					yya[yyac] = new YYARec(299,-177 );yyac++; 
					yya[yyac] = new YYARec(300,-177 );yyac++; 
					yya[yyac] = new YYARec(301,-177 );yyac++; 
					yya[yyac] = new YYARec(302,-177 );yyac++; 
					yya[yyac] = new YYARec(304,-177 );yyac++; 
					yya[yyac] = new YYARec(305,-177 );yyac++; 
					yya[yyac] = new YYARec(306,-177 );yyac++; 
					yya[yyac] = new YYARec(307,-177 );yyac++; 
					yya[yyac] = new YYARec(308,-177 );yyac++; 
					yya[yyac] = new YYARec(309,-177 );yyac++; 
					yya[yyac] = new YYARec(310,-177 );yyac++; 
					yya[yyac] = new YYARec(312,-177 );yyac++; 
					yya[yyac] = new YYARec(313,-177 );yyac++; 
					yya[yyac] = new YYARec(314,-177 );yyac++; 
					yya[yyac] = new YYARec(315,-177 );yyac++; 
					yya[yyac] = new YYARec(316,-177 );yyac++; 
					yya[yyac] = new YYARec(317,-177 );yyac++; 
					yya[yyac] = new YYARec(318,-177 );yyac++; 
					yya[yyac] = new YYARec(319,-177 );yyac++; 
					yya[yyac] = new YYARec(320,-177 );yyac++; 
					yya[yyac] = new YYARec(321,-177 );yyac++; 
					yya[yyac] = new YYARec(322,-177 );yyac++; 
					yya[yyac] = new YYARec(323,-177 );yyac++; 
					yya[yyac] = new YYARec(324,-177 );yyac++; 
					yya[yyac] = new YYARec(325,-177 );yyac++; 
					yya[yyac] = new YYARec(268,-202 );yyac++; 
					yya[yyac] = new YYARec(289,-202 );yyac++; 
					yya[yyac] = new YYARec(290,-202 );yyac++; 
					yya[yyac] = new YYARec(291,-202 );yyac++; 
					yya[yyac] = new YYARec(292,-202 );yyac++; 
					yya[yyac] = new YYARec(293,-202 );yyac++; 
					yya[yyac] = new YYARec(297,-202 );yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(263,59);yyac++; 
					yya[yyac] = new YYARec(282,-99 );yyac++; 
					yya[yyac] = new YYARec(283,-99 );yyac++; 
					yya[yyac] = new YYARec(287,-99 );yyac++; 
					yya[yyac] = new YYARec(298,-99 );yyac++; 
					yya[yyac] = new YYARec(299,-99 );yyac++; 
					yya[yyac] = new YYARec(300,-99 );yyac++; 
					yya[yyac] = new YYARec(301,-99 );yyac++; 
					yya[yyac] = new YYARec(302,-99 );yyac++; 
					yya[yyac] = new YYARec(304,-99 );yyac++; 
					yya[yyac] = new YYARec(305,-99 );yyac++; 
					yya[yyac] = new YYARec(306,-99 );yyac++; 
					yya[yyac] = new YYARec(307,-99 );yyac++; 
					yya[yyac] = new YYARec(308,-99 );yyac++; 
					yya[yyac] = new YYARec(309,-99 );yyac++; 
					yya[yyac] = new YYARec(310,-99 );yyac++; 
					yya[yyac] = new YYARec(312,-99 );yyac++; 
					yya[yyac] = new YYARec(313,-99 );yyac++; 
					yya[yyac] = new YYARec(314,-99 );yyac++; 
					yya[yyac] = new YYARec(315,-99 );yyac++; 
					yya[yyac] = new YYARec(316,-99 );yyac++; 
					yya[yyac] = new YYARec(317,-99 );yyac++; 
					yya[yyac] = new YYARec(318,-99 );yyac++; 
					yya[yyac] = new YYARec(319,-99 );yyac++; 
					yya[yyac] = new YYARec(320,-99 );yyac++; 
					yya[yyac] = new YYARec(321,-99 );yyac++; 
					yya[yyac] = new YYARec(322,-99 );yyac++; 
					yya[yyac] = new YYARec(323,-99 );yyac++; 
					yya[yyac] = new YYARec(324,-99 );yyac++; 
					yya[yyac] = new YYARec(325,-99 );yyac++; 
					yya[yyac] = new YYARec(258,172);yyac++; 
					yya[yyac] = new YYARec(257,133);yyac++; 
					yya[yyac] = new YYARec(258,134);yyac++; 
					yya[yyac] = new YYARec(259,135);yyac++; 
					yya[yyac] = new YYARec(266,136);yyac++; 
					yya[yyac] = new YYARec(288,137);yyac++; 
					yya[yyac] = new YYARec(294,138);yyac++; 
					yya[yyac] = new YYARec(295,139);yyac++; 
					yya[yyac] = new YYARec(296,140);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,141);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,142);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(260,-86 );yyac++; 
					yya[yyac] = new YYARec(261,-86 );yyac++; 
					yya[yyac] = new YYARec(267,-86 );yyac++; 
					yya[yyac] = new YYARec(257,133);yyac++; 
					yya[yyac] = new YYARec(258,134);yyac++; 
					yya[yyac] = new YYARec(259,135);yyac++; 
					yya[yyac] = new YYARec(266,136);yyac++; 
					yya[yyac] = new YYARec(288,137);yyac++; 
					yya[yyac] = new YYARec(294,138);yyac++; 
					yya[yyac] = new YYARec(295,139);yyac++; 
					yya[yyac] = new YYARec(296,140);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,141);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,142);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(260,-86 );yyac++; 
					yya[yyac] = new YYARec(261,-86 );yyac++; 
					yya[yyac] = new YYARec(267,-86 );yyac++; 
					yya[yyac] = new YYARec(268,175);yyac++; 
					yya[yyac] = new YYARec(297,168);yyac++; 
					yya[yyac] = new YYARec(289,-159 );yyac++; 
					yya[yyac] = new YYARec(290,-159 );yyac++; 
					yya[yyac] = new YYARec(291,-159 );yyac++; 
					yya[yyac] = new YYARec(292,-159 );yyac++; 
					yya[yyac] = new YYARec(293,-159 );yyac++; 
					yya[yyac] = new YYARec(268,-210 );yyac++; 
					yya[yyac] = new YYARec(289,177);yyac++; 
					yya[yyac] = new YYARec(290,178);yyac++; 
					yya[yyac] = new YYARec(291,179);yyac++; 
					yya[yyac] = new YYARec(292,180);yyac++; 
					yya[yyac] = new YYARec(293,181);yyac++; 
					yya[yyac] = new YYARec(267,182);yyac++; 
					yya[yyac] = new YYARec(257,133);yyac++; 
					yya[yyac] = new YYARec(258,134);yyac++; 
					yya[yyac] = new YYARec(259,135);yyac++; 
					yya[yyac] = new YYARec(266,136);yyac++; 
					yya[yyac] = new YYARec(288,137);yyac++; 
					yya[yyac] = new YYARec(294,138);yyac++; 
					yya[yyac] = new YYARec(295,139);yyac++; 
					yya[yyac] = new YYARec(296,140);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,141);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,142);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(260,-86 );yyac++; 
					yya[yyac] = new YYARec(261,-86 );yyac++; 
					yya[yyac] = new YYARec(267,-86 );yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(257,133);yyac++; 
					yya[yyac] = new YYARec(258,134);yyac++; 
					yya[yyac] = new YYARec(259,135);yyac++; 
					yya[yyac] = new YYARec(266,136);yyac++; 
					yya[yyac] = new YYARec(288,137);yyac++; 
					yya[yyac] = new YYARec(294,138);yyac++; 
					yya[yyac] = new YYARec(295,139);yyac++; 
					yya[yyac] = new YYARec(296,140);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,141);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,142);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(267,-86 );yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(266,206);yyac++; 
					yya[yyac] = new YYARec(258,-175 );yyac++; 
					yya[yyac] = new YYARec(263,-175 );yyac++; 
					yya[yyac] = new YYARec(282,-175 );yyac++; 
					yya[yyac] = new YYARec(283,-175 );yyac++; 
					yya[yyac] = new YYARec(287,-175 );yyac++; 
					yya[yyac] = new YYARec(298,-175 );yyac++; 
					yya[yyac] = new YYARec(299,-175 );yyac++; 
					yya[yyac] = new YYARec(300,-175 );yyac++; 
					yya[yyac] = new YYARec(301,-175 );yyac++; 
					yya[yyac] = new YYARec(302,-175 );yyac++; 
					yya[yyac] = new YYARec(304,-175 );yyac++; 
					yya[yyac] = new YYARec(305,-175 );yyac++; 
					yya[yyac] = new YYARec(306,-175 );yyac++; 
					yya[yyac] = new YYARec(307,-175 );yyac++; 
					yya[yyac] = new YYARec(308,-175 );yyac++; 
					yya[yyac] = new YYARec(309,-175 );yyac++; 
					yya[yyac] = new YYARec(310,-175 );yyac++; 
					yya[yyac] = new YYARec(312,-175 );yyac++; 
					yya[yyac] = new YYARec(313,-175 );yyac++; 
					yya[yyac] = new YYARec(314,-175 );yyac++; 
					yya[yyac] = new YYARec(315,-175 );yyac++; 
					yya[yyac] = new YYARec(316,-175 );yyac++; 
					yya[yyac] = new YYARec(317,-175 );yyac++; 
					yya[yyac] = new YYARec(318,-175 );yyac++; 
					yya[yyac] = new YYARec(319,-175 );yyac++; 
					yya[yyac] = new YYARec(320,-175 );yyac++; 
					yya[yyac] = new YYARec(321,-175 );yyac++; 
					yya[yyac] = new YYARec(322,-175 );yyac++; 
					yya[yyac] = new YYARec(323,-175 );yyac++; 
					yya[yyac] = new YYARec(324,-175 );yyac++; 
					yya[yyac] = new YYARec(325,-175 );yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(289,-197 );yyac++; 
					yya[yyac] = new YYARec(290,-197 );yyac++; 
					yya[yyac] = new YYARec(291,-197 );yyac++; 
					yya[yyac] = new YYARec(292,-197 );yyac++; 
					yya[yyac] = new YYARec(293,-197 );yyac++; 
					yya[yyac] = new YYARec(297,-197 );yyac++; 
					yya[yyac] = new YYARec(268,-209 );yyac++; 
					yya[yyac] = new YYARec(268,-147 );yyac++; 
					yya[yyac] = new YYARec(289,-147 );yyac++; 
					yya[yyac] = new YYARec(290,-147 );yyac++; 
					yya[yyac] = new YYARec(291,-147 );yyac++; 
					yya[yyac] = new YYARec(292,-147 );yyac++; 
					yya[yyac] = new YYARec(293,-147 );yyac++; 
					yya[yyac] = new YYARec(297,-147 );yyac++; 
					yya[yyac] = new YYARec(258,-176 );yyac++; 
					yya[yyac] = new YYARec(263,-176 );yyac++; 
					yya[yyac] = new YYARec(282,-176 );yyac++; 
					yya[yyac] = new YYARec(283,-176 );yyac++; 
					yya[yyac] = new YYARec(287,-176 );yyac++; 
					yya[yyac] = new YYARec(298,-176 );yyac++; 
					yya[yyac] = new YYARec(299,-176 );yyac++; 
					yya[yyac] = new YYARec(300,-176 );yyac++; 
					yya[yyac] = new YYARec(301,-176 );yyac++; 
					yya[yyac] = new YYARec(302,-176 );yyac++; 
					yya[yyac] = new YYARec(304,-176 );yyac++; 
					yya[yyac] = new YYARec(305,-176 );yyac++; 
					yya[yyac] = new YYARec(306,-176 );yyac++; 
					yya[yyac] = new YYARec(307,-176 );yyac++; 
					yya[yyac] = new YYARec(308,-176 );yyac++; 
					yya[yyac] = new YYARec(309,-176 );yyac++; 
					yya[yyac] = new YYARec(310,-176 );yyac++; 
					yya[yyac] = new YYARec(312,-176 );yyac++; 
					yya[yyac] = new YYARec(313,-176 );yyac++; 
					yya[yyac] = new YYARec(314,-176 );yyac++; 
					yya[yyac] = new YYARec(315,-176 );yyac++; 
					yya[yyac] = new YYARec(316,-176 );yyac++; 
					yya[yyac] = new YYARec(317,-176 );yyac++; 
					yya[yyac] = new YYARec(318,-176 );yyac++; 
					yya[yyac] = new YYARec(319,-176 );yyac++; 
					yya[yyac] = new YYARec(320,-176 );yyac++; 
					yya[yyac] = new YYARec(321,-176 );yyac++; 
					yya[yyac] = new YYARec(322,-176 );yyac++; 
					yya[yyac] = new YYARec(323,-176 );yyac++; 
					yya[yyac] = new YYARec(324,-176 );yyac++; 
					yya[yyac] = new YYARec(325,-176 );yyac++; 
					yya[yyac] = new YYARec(258,210);yyac++; 
					yya[yyac] = new YYARec(268,-200 );yyac++; 
					yya[yyac] = new YYARec(289,-200 );yyac++; 
					yya[yyac] = new YYARec(290,-200 );yyac++; 
					yya[yyac] = new YYARec(291,-200 );yyac++; 
					yya[yyac] = new YYARec(292,-200 );yyac++; 
					yya[yyac] = new YYARec(293,-200 );yyac++; 
					yya[yyac] = new YYARec(297,-200 );yyac++; 
					yya[yyac] = new YYARec(263,59);yyac++; 
					yya[yyac] = new YYARec(282,-99 );yyac++; 
					yya[yyac] = new YYARec(283,-99 );yyac++; 
					yya[yyac] = new YYARec(287,-99 );yyac++; 
					yya[yyac] = new YYARec(298,-99 );yyac++; 
					yya[yyac] = new YYARec(300,-99 );yyac++; 
					yya[yyac] = new YYARec(301,-99 );yyac++; 
					yya[yyac] = new YYARec(302,-99 );yyac++; 
					yya[yyac] = new YYARec(303,-99 );yyac++; 
					yya[yyac] = new YYARec(304,-99 );yyac++; 
					yya[yyac] = new YYARec(305,-99 );yyac++; 
					yya[yyac] = new YYARec(306,-99 );yyac++; 
					yya[yyac] = new YYARec(307,-99 );yyac++; 
					yya[yyac] = new YYARec(309,-99 );yyac++; 
					yya[yyac] = new YYARec(310,-99 );yyac++; 
					yya[yyac] = new YYARec(313,-99 );yyac++; 
					yya[yyac] = new YYARec(314,-99 );yyac++; 
					yya[yyac] = new YYARec(315,-99 );yyac++; 
					yya[yyac] = new YYARec(316,-99 );yyac++; 
					yya[yyac] = new YYARec(317,-99 );yyac++; 
					yya[yyac] = new YYARec(318,-99 );yyac++; 
					yya[yyac] = new YYARec(319,-99 );yyac++; 
					yya[yyac] = new YYARec(320,-99 );yyac++; 
					yya[yyac] = new YYARec(321,-99 );yyac++; 
					yya[yyac] = new YYARec(322,-99 );yyac++; 
					yya[yyac] = new YYARec(323,-99 );yyac++; 
					yya[yyac] = new YYARec(324,-99 );yyac++; 
					yya[yyac] = new YYARec(325,-99 );yyac++; 
					yya[yyac] = new YYARec(258,212);yyac++; 
					yya[yyac] = new YYARec(267,213);yyac++; 
					yya[yyac] = new YYARec(257,152);yyac++; 
					yya[yyac] = new YYARec(259,153);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,154);yyac++; 
					yya[yyac] = new YYARec(311,155);yyac++; 
					yya[yyac] = new YYARec(312,156);yyac++; 
					yya[yyac] = new YYARec(315,157);yyac++; 
					yya[yyac] = new YYARec(317,158);yyac++; 
					yya[yyac] = new YYARec(319,159);yyac++; 
					yya[yyac] = new YYARec(320,160);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(260,-63 );yyac++; 
					yya[yyac] = new YYARec(261,-63 );yyac++; 
					yya[yyac] = new YYARec(267,-63 );yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(321,86);yyac++; 
					yya[yyac] = new YYARec(258,-53 );yyac++; 
					yya[yyac] = new YYARec(258,220);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(305,223);yyac++; 
					yya[yyac] = new YYARec(306,154);yyac++; 
					yya[yyac] = new YYARec(311,155);yyac++; 
					yya[yyac] = new YYARec(312,156);yyac++; 
					yya[yyac] = new YYARec(313,224);yyac++; 
					yya[yyac] = new YYARec(315,157);yyac++; 
					yya[yyac] = new YYARec(316,225);yyac++; 
					yya[yyac] = new YYARec(317,158);yyac++; 
					yya[yyac] = new YYARec(318,226);yyac++; 
					yya[yyac] = new YYARec(319,159);yyac++; 
					yya[yyac] = new YYARec(320,160);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(305,223);yyac++; 
					yya[yyac] = new YYARec(306,154);yyac++; 
					yya[yyac] = new YYARec(311,155);yyac++; 
					yya[yyac] = new YYARec(312,156);yyac++; 
					yya[yyac] = new YYARec(313,224);yyac++; 
					yya[yyac] = new YYARec(315,157);yyac++; 
					yya[yyac] = new YYARec(316,225);yyac++; 
					yya[yyac] = new YYARec(317,158);yyac++; 
					yya[yyac] = new YYARec(318,226);yyac++; 
					yya[yyac] = new YYARec(319,159);yyac++; 
					yya[yyac] = new YYARec(320,160);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(308,119);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,86);yyac++; 
					yya[yyac] = new YYARec(322,87);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(324,70);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(257,133);yyac++; 
					yya[yyac] = new YYARec(258,134);yyac++; 
					yya[yyac] = new YYARec(259,135);yyac++; 
					yya[yyac] = new YYARec(266,136);yyac++; 
					yya[yyac] = new YYARec(288,137);yyac++; 
					yya[yyac] = new YYARec(294,138);yyac++; 
					yya[yyac] = new YYARec(295,139);yyac++; 
					yya[yyac] = new YYARec(296,140);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,141);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,142);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(260,-86 );yyac++; 
					yya[yyac] = new YYARec(261,-86 );yyac++; 
					yya[yyac] = new YYARec(267,-86 );yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(257,133);yyac++; 
					yya[yyac] = new YYARec(258,134);yyac++; 
					yya[yyac] = new YYARec(259,135);yyac++; 
					yya[yyac] = new YYARec(266,136);yyac++; 
					yya[yyac] = new YYARec(288,137);yyac++; 
					yya[yyac] = new YYARec(294,138);yyac++; 
					yya[yyac] = new YYARec(295,139);yyac++; 
					yya[yyac] = new YYARec(296,140);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,141);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,142);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(260,-86 );yyac++; 
					yya[yyac] = new YYARec(261,-86 );yyac++; 
					yya[yyac] = new YYARec(257,133);yyac++; 
					yya[yyac] = new YYARec(258,134);yyac++; 
					yya[yyac] = new YYARec(259,135);yyac++; 
					yya[yyac] = new YYARec(266,136);yyac++; 
					yya[yyac] = new YYARec(288,137);yyac++; 
					yya[yyac] = new YYARec(294,138);yyac++; 
					yya[yyac] = new YYARec(295,139);yyac++; 
					yya[yyac] = new YYARec(296,140);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,141);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,142);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(260,-86 );yyac++; 
					yya[yyac] = new YYARec(261,-86 );yyac++; 
					yya[yyac] = new YYARec(267,241);yyac++; 
					yya[yyac] = new YYARec(274,242);yyac++; 
					yya[yyac] = new YYARec(258,-204 );yyac++; 
					yya[yyac] = new YYARec(266,-204 );yyac++; 
					yya[yyac] = new YYARec(269,-204 );yyac++; 
					yya[yyac] = new YYARec(270,-204 );yyac++; 
					yya[yyac] = new YYARec(271,-204 );yyac++; 
					yya[yyac] = new YYARec(272,-204 );yyac++; 
					yya[yyac] = new YYARec(273,-204 );yyac++; 
					yya[yyac] = new YYARec(275,-204 );yyac++; 
					yya[yyac] = new YYARec(276,-204 );yyac++; 
					yya[yyac] = new YYARec(277,-204 );yyac++; 
					yya[yyac] = new YYARec(278,-204 );yyac++; 
					yya[yyac] = new YYARec(279,-204 );yyac++; 
					yya[yyac] = new YYARec(280,-204 );yyac++; 
					yya[yyac] = new YYARec(281,-204 );yyac++; 
					yya[yyac] = new YYARec(282,-204 );yyac++; 
					yya[yyac] = new YYARec(283,-204 );yyac++; 
					yya[yyac] = new YYARec(284,-204 );yyac++; 
					yya[yyac] = new YYARec(285,-204 );yyac++; 
					yya[yyac] = new YYARec(286,-204 );yyac++; 
					yya[yyac] = new YYARec(289,-204 );yyac++; 
					yya[yyac] = new YYARec(290,-204 );yyac++; 
					yya[yyac] = new YYARec(291,-204 );yyac++; 
					yya[yyac] = new YYARec(292,-204 );yyac++; 
					yya[yyac] = new YYARec(293,-204 );yyac++; 
					yya[yyac] = new YYARec(297,-204 );yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(284,245);yyac++; 
					yya[yyac] = new YYARec(285,246);yyac++; 
					yya[yyac] = new YYARec(286,247);yyac++; 
					yya[yyac] = new YYARec(258,-115 );yyac++; 
					yya[yyac] = new YYARec(266,-115 );yyac++; 
					yya[yyac] = new YYARec(269,-115 );yyac++; 
					yya[yyac] = new YYARec(270,-115 );yyac++; 
					yya[yyac] = new YYARec(271,-115 );yyac++; 
					yya[yyac] = new YYARec(272,-115 );yyac++; 
					yya[yyac] = new YYARec(273,-115 );yyac++; 
					yya[yyac] = new YYARec(275,-115 );yyac++; 
					yya[yyac] = new YYARec(276,-115 );yyac++; 
					yya[yyac] = new YYARec(277,-115 );yyac++; 
					yya[yyac] = new YYARec(278,-115 );yyac++; 
					yya[yyac] = new YYARec(279,-115 );yyac++; 
					yya[yyac] = new YYARec(280,-115 );yyac++; 
					yya[yyac] = new YYARec(281,-115 );yyac++; 
					yya[yyac] = new YYARec(282,-115 );yyac++; 
					yya[yyac] = new YYARec(283,-115 );yyac++; 
					yya[yyac] = new YYARec(282,249);yyac++; 
					yya[yyac] = new YYARec(283,250);yyac++; 
					yya[yyac] = new YYARec(258,-113 );yyac++; 
					yya[yyac] = new YYARec(266,-113 );yyac++; 
					yya[yyac] = new YYARec(269,-113 );yyac++; 
					yya[yyac] = new YYARec(270,-113 );yyac++; 
					yya[yyac] = new YYARec(271,-113 );yyac++; 
					yya[yyac] = new YYARec(272,-113 );yyac++; 
					yya[yyac] = new YYARec(273,-113 );yyac++; 
					yya[yyac] = new YYARec(275,-113 );yyac++; 
					yya[yyac] = new YYARec(276,-113 );yyac++; 
					yya[yyac] = new YYARec(277,-113 );yyac++; 
					yya[yyac] = new YYARec(278,-113 );yyac++; 
					yya[yyac] = new YYARec(279,-113 );yyac++; 
					yya[yyac] = new YYARec(280,-113 );yyac++; 
					yya[yyac] = new YYARec(281,-113 );yyac++; 
					yya[yyac] = new YYARec(278,252);yyac++; 
					yya[yyac] = new YYARec(279,253);yyac++; 
					yya[yyac] = new YYARec(280,254);yyac++; 
					yya[yyac] = new YYARec(281,255);yyac++; 
					yya[yyac] = new YYARec(258,-111 );yyac++; 
					yya[yyac] = new YYARec(266,-111 );yyac++; 
					yya[yyac] = new YYARec(269,-111 );yyac++; 
					yya[yyac] = new YYARec(270,-111 );yyac++; 
					yya[yyac] = new YYARec(271,-111 );yyac++; 
					yya[yyac] = new YYARec(272,-111 );yyac++; 
					yya[yyac] = new YYARec(273,-111 );yyac++; 
					yya[yyac] = new YYARec(275,-111 );yyac++; 
					yya[yyac] = new YYARec(276,-111 );yyac++; 
					yya[yyac] = new YYARec(277,-111 );yyac++; 
					yya[yyac] = new YYARec(276,257);yyac++; 
					yya[yyac] = new YYARec(277,258);yyac++; 
					yya[yyac] = new YYARec(258,-110 );yyac++; 
					yya[yyac] = new YYARec(266,-110 );yyac++; 
					yya[yyac] = new YYARec(269,-110 );yyac++; 
					yya[yyac] = new YYARec(270,-110 );yyac++; 
					yya[yyac] = new YYARec(271,-110 );yyac++; 
					yya[yyac] = new YYARec(272,-110 );yyac++; 
					yya[yyac] = new YYARec(273,-110 );yyac++; 
					yya[yyac] = new YYARec(275,-110 );yyac++; 
					yya[yyac] = new YYARec(273,259);yyac++; 
					yya[yyac] = new YYARec(258,-108 );yyac++; 
					yya[yyac] = new YYARec(266,-108 );yyac++; 
					yya[yyac] = new YYARec(269,-108 );yyac++; 
					yya[yyac] = new YYARec(270,-108 );yyac++; 
					yya[yyac] = new YYARec(271,-108 );yyac++; 
					yya[yyac] = new YYARec(272,-108 );yyac++; 
					yya[yyac] = new YYARec(275,-108 );yyac++; 
					yya[yyac] = new YYARec(272,260);yyac++; 
					yya[yyac] = new YYARec(258,-106 );yyac++; 
					yya[yyac] = new YYARec(266,-106 );yyac++; 
					yya[yyac] = new YYARec(269,-106 );yyac++; 
					yya[yyac] = new YYARec(270,-106 );yyac++; 
					yya[yyac] = new YYARec(271,-106 );yyac++; 
					yya[yyac] = new YYARec(275,-106 );yyac++; 
					yya[yyac] = new YYARec(271,261);yyac++; 
					yya[yyac] = new YYARec(258,-104 );yyac++; 
					yya[yyac] = new YYARec(266,-104 );yyac++; 
					yya[yyac] = new YYARec(269,-104 );yyac++; 
					yya[yyac] = new YYARec(270,-104 );yyac++; 
					yya[yyac] = new YYARec(275,-104 );yyac++; 
					yya[yyac] = new YYARec(270,262);yyac++; 
					yya[yyac] = new YYARec(258,-102 );yyac++; 
					yya[yyac] = new YYARec(266,-102 );yyac++; 
					yya[yyac] = new YYARec(269,-102 );yyac++; 
					yya[yyac] = new YYARec(275,-102 );yyac++; 
					yya[yyac] = new YYARec(269,263);yyac++; 
					yya[yyac] = new YYARec(258,-100 );yyac++; 
					yya[yyac] = new YYARec(266,-100 );yyac++; 
					yya[yyac] = new YYARec(275,-100 );yyac++; 
					yya[yyac] = new YYARec(289,177);yyac++; 
					yya[yyac] = new YYARec(290,178);yyac++; 
					yya[yyac] = new YYARec(291,179);yyac++; 
					yya[yyac] = new YYARec(292,180);yyac++; 
					yya[yyac] = new YYARec(293,181);yyac++; 
					yya[yyac] = new YYARec(258,-124 );yyac++; 
					yya[yyac] = new YYARec(269,-124 );yyac++; 
					yya[yyac] = new YYARec(270,-124 );yyac++; 
					yya[yyac] = new YYARec(271,-124 );yyac++; 
					yya[yyac] = new YYARec(272,-124 );yyac++; 
					yya[yyac] = new YYARec(273,-124 );yyac++; 
					yya[yyac] = new YYARec(276,-124 );yyac++; 
					yya[yyac] = new YYARec(277,-124 );yyac++; 
					yya[yyac] = new YYARec(278,-124 );yyac++; 
					yya[yyac] = new YYARec(279,-124 );yyac++; 
					yya[yyac] = new YYARec(280,-124 );yyac++; 
					yya[yyac] = new YYARec(281,-124 );yyac++; 
					yya[yyac] = new YYARec(282,-124 );yyac++; 
					yya[yyac] = new YYARec(283,-124 );yyac++; 
					yya[yyac] = new YYARec(284,-124 );yyac++; 
					yya[yyac] = new YYARec(285,-124 );yyac++; 
					yya[yyac] = new YYARec(286,-124 );yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(257,133);yyac++; 
					yya[yyac] = new YYARec(258,134);yyac++; 
					yya[yyac] = new YYARec(259,135);yyac++; 
					yya[yyac] = new YYARec(266,136);yyac++; 
					yya[yyac] = new YYARec(288,137);yyac++; 
					yya[yyac] = new YYARec(294,138);yyac++; 
					yya[yyac] = new YYARec(295,139);yyac++; 
					yya[yyac] = new YYARec(296,140);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,141);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,142);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(267,-86 );yyac++; 
					yya[yyac] = new YYARec(266,267);yyac++; 
					yya[yyac] = new YYARec(266,268);yyac++; 
					yya[yyac] = new YYARec(257,133);yyac++; 
					yya[yyac] = new YYARec(258,134);yyac++; 
					yya[yyac] = new YYARec(259,135);yyac++; 
					yya[yyac] = new YYARec(266,136);yyac++; 
					yya[yyac] = new YYARec(288,137);yyac++; 
					yya[yyac] = new YYARec(294,138);yyac++; 
					yya[yyac] = new YYARec(295,139);yyac++; 
					yya[yyac] = new YYARec(296,140);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,141);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,142);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(260,-86 );yyac++; 
					yya[yyac] = new YYARec(261,-86 );yyac++; 
					yya[yyac] = new YYARec(267,-86 );yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,86);yyac++; 
					yya[yyac] = new YYARec(322,87);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(324,70);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(257,152);yyac++; 
					yya[yyac] = new YYARec(259,153);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,154);yyac++; 
					yya[yyac] = new YYARec(311,155);yyac++; 
					yya[yyac] = new YYARec(312,156);yyac++; 
					yya[yyac] = new YYARec(315,157);yyac++; 
					yya[yyac] = new YYARec(317,158);yyac++; 
					yya[yyac] = new YYARec(319,159);yyac++; 
					yya[yyac] = new YYARec(320,160);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(260,-63 );yyac++; 
					yya[yyac] = new YYARec(261,-63 );yyac++; 
					yya[yyac] = new YYARec(267,-63 );yyac++; 
					yya[yyac] = new YYARec(258,280);yyac++; 
					yya[yyac] = new YYARec(258,281);yyac++; 
					yya[yyac] = new YYARec(321,101);yyac++; 
					yya[yyac] = new YYARec(263,282);yyac++; 
					yya[yyac] = new YYARec(258,-52 );yyac++; 
					yya[yyac] = new YYARec(258,283);yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(258,19);yyac++; 
					yya[yyac] = new YYARec(259,20);yyac++; 
					yya[yyac] = new YYARec(262,21);yyac++; 
					yya[yyac] = new YYARec(264,22);yyac++; 
					yya[yyac] = new YYARec(265,23);yyac++; 
					yya[yyac] = new YYARec(267,24);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,26);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(311,31);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(319,35);yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(263,59);yyac++; 
					yya[yyac] = new YYARec(258,-99 );yyac++; 
					yya[yyac] = new YYARec(282,-99 );yyac++; 
					yya[yyac] = new YYARec(283,-99 );yyac++; 
					yya[yyac] = new YYARec(287,-99 );yyac++; 
					yya[yyac] = new YYARec(298,-99 );yyac++; 
					yya[yyac] = new YYARec(299,-99 );yyac++; 
					yya[yyac] = new YYARec(300,-99 );yyac++; 
					yya[yyac] = new YYARec(301,-99 );yyac++; 
					yya[yyac] = new YYARec(302,-99 );yyac++; 
					yya[yyac] = new YYARec(304,-99 );yyac++; 
					yya[yyac] = new YYARec(305,-99 );yyac++; 
					yya[yyac] = new YYARec(306,-99 );yyac++; 
					yya[yyac] = new YYARec(307,-99 );yyac++; 
					yya[yyac] = new YYARec(308,-99 );yyac++; 
					yya[yyac] = new YYARec(309,-99 );yyac++; 
					yya[yyac] = new YYARec(310,-99 );yyac++; 
					yya[yyac] = new YYARec(312,-99 );yyac++; 
					yya[yyac] = new YYARec(313,-99 );yyac++; 
					yya[yyac] = new YYARec(314,-99 );yyac++; 
					yya[yyac] = new YYARec(315,-99 );yyac++; 
					yya[yyac] = new YYARec(316,-99 );yyac++; 
					yya[yyac] = new YYARec(317,-99 );yyac++; 
					yya[yyac] = new YYARec(318,-99 );yyac++; 
					yya[yyac] = new YYARec(319,-99 );yyac++; 
					yya[yyac] = new YYARec(320,-99 );yyac++; 
					yya[yyac] = new YYARec(321,-99 );yyac++; 
					yya[yyac] = new YYARec(322,-99 );yyac++; 
					yya[yyac] = new YYARec(323,-99 );yyac++; 
					yya[yyac] = new YYARec(324,-99 );yyac++; 
					yya[yyac] = new YYARec(325,-99 );yyac++; 
					yya[yyac] = new YYARec(258,286);yyac++; 
					yya[yyac] = new YYARec(260,287);yyac++; 
					yya[yyac] = new YYARec(261,288);yyac++; 
					yya[yyac] = new YYARec(257,133);yyac++; 
					yya[yyac] = new YYARec(258,134);yyac++; 
					yya[yyac] = new YYARec(259,135);yyac++; 
					yya[yyac] = new YYARec(266,136);yyac++; 
					yya[yyac] = new YYARec(288,137);yyac++; 
					yya[yyac] = new YYARec(294,138);yyac++; 
					yya[yyac] = new YYARec(295,139);yyac++; 
					yya[yyac] = new YYARec(296,140);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,141);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,142);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(260,-86 );yyac++; 
					yya[yyac] = new YYARec(261,-86 );yyac++; 
					yya[yyac] = new YYARec(267,-86 );yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(274,203);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,204);yyac++; 
					yya[yyac] = new YYARec(322,205);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(275,301);yyac++; 
					yya[yyac] = new YYARec(267,302);yyac++; 
					yya[yyac] = new YYARec(257,133);yyac++; 
					yya[yyac] = new YYARec(258,134);yyac++; 
					yya[yyac] = new YYARec(259,135);yyac++; 
					yya[yyac] = new YYARec(266,136);yyac++; 
					yya[yyac] = new YYARec(288,137);yyac++; 
					yya[yyac] = new YYARec(294,138);yyac++; 
					yya[yyac] = new YYARec(295,139);yyac++; 
					yya[yyac] = new YYARec(296,140);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,141);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,142);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(267,-86 );yyac++; 
					yya[yyac] = new YYARec(257,133);yyac++; 
					yya[yyac] = new YYARec(258,134);yyac++; 
					yya[yyac] = new YYARec(259,135);yyac++; 
					yya[yyac] = new YYARec(266,136);yyac++; 
					yya[yyac] = new YYARec(288,137);yyac++; 
					yya[yyac] = new YYARec(294,138);yyac++; 
					yya[yyac] = new YYARec(295,139);yyac++; 
					yya[yyac] = new YYARec(296,140);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,141);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,142);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(267,-86 );yyac++; 
					yya[yyac] = new YYARec(297,305);yyac++; 
					yya[yyac] = new YYARec(258,-76 );yyac++; 
					yya[yyac] = new YYARec(263,-76 );yyac++; 
					yya[yyac] = new YYARec(282,-76 );yyac++; 
					yya[yyac] = new YYARec(283,-76 );yyac++; 
					yya[yyac] = new YYARec(287,-76 );yyac++; 
					yya[yyac] = new YYARec(298,-76 );yyac++; 
					yya[yyac] = new YYARec(300,-76 );yyac++; 
					yya[yyac] = new YYARec(301,-76 );yyac++; 
					yya[yyac] = new YYARec(302,-76 );yyac++; 
					yya[yyac] = new YYARec(303,-76 );yyac++; 
					yya[yyac] = new YYARec(304,-76 );yyac++; 
					yya[yyac] = new YYARec(305,-76 );yyac++; 
					yya[yyac] = new YYARec(306,-76 );yyac++; 
					yya[yyac] = new YYARec(307,-76 );yyac++; 
					yya[yyac] = new YYARec(309,-76 );yyac++; 
					yya[yyac] = new YYARec(310,-76 );yyac++; 
					yya[yyac] = new YYARec(313,-76 );yyac++; 
					yya[yyac] = new YYARec(314,-76 );yyac++; 
					yya[yyac] = new YYARec(315,-76 );yyac++; 
					yya[yyac] = new YYARec(316,-76 );yyac++; 
					yya[yyac] = new YYARec(317,-76 );yyac++; 
					yya[yyac] = new YYARec(318,-76 );yyac++; 
					yya[yyac] = new YYARec(319,-76 );yyac++; 
					yya[yyac] = new YYARec(320,-76 );yyac++; 
					yya[yyac] = new YYARec(321,-76 );yyac++; 
					yya[yyac] = new YYARec(322,-76 );yyac++; 
					yya[yyac] = new YYARec(323,-76 );yyac++; 
					yya[yyac] = new YYARec(324,-76 );yyac++; 
					yya[yyac] = new YYARec(325,-76 );yyac++; 
					yya[yyac] = new YYARec(263,59);yyac++; 
					yya[yyac] = new YYARec(258,-99 );yyac++; 
					yya[yyac] = new YYARec(282,-99 );yyac++; 
					yya[yyac] = new YYARec(283,-99 );yyac++; 
					yya[yyac] = new YYARec(287,-99 );yyac++; 
					yya[yyac] = new YYARec(298,-99 );yyac++; 
					yya[yyac] = new YYARec(300,-99 );yyac++; 
					yya[yyac] = new YYARec(301,-99 );yyac++; 
					yya[yyac] = new YYARec(302,-99 );yyac++; 
					yya[yyac] = new YYARec(303,-99 );yyac++; 
					yya[yyac] = new YYARec(304,-99 );yyac++; 
					yya[yyac] = new YYARec(305,-99 );yyac++; 
					yya[yyac] = new YYARec(306,-99 );yyac++; 
					yya[yyac] = new YYARec(307,-99 );yyac++; 
					yya[yyac] = new YYARec(309,-99 );yyac++; 
					yya[yyac] = new YYARec(310,-99 );yyac++; 
					yya[yyac] = new YYARec(313,-99 );yyac++; 
					yya[yyac] = new YYARec(314,-99 );yyac++; 
					yya[yyac] = new YYARec(315,-99 );yyac++; 
					yya[yyac] = new YYARec(316,-99 );yyac++; 
					yya[yyac] = new YYARec(317,-99 );yyac++; 
					yya[yyac] = new YYARec(318,-99 );yyac++; 
					yya[yyac] = new YYARec(319,-99 );yyac++; 
					yya[yyac] = new YYARec(320,-99 );yyac++; 
					yya[yyac] = new YYARec(321,-99 );yyac++; 
					yya[yyac] = new YYARec(322,-99 );yyac++; 
					yya[yyac] = new YYARec(323,-99 );yyac++; 
					yya[yyac] = new YYARec(324,-99 );yyac++; 
					yya[yyac] = new YYARec(325,-99 );yyac++; 
					yya[yyac] = new YYARec(297,307);yyac++; 
					yya[yyac] = new YYARec(258,-77 );yyac++; 
					yya[yyac] = new YYARec(263,-77 );yyac++; 
					yya[yyac] = new YYARec(282,-77 );yyac++; 
					yya[yyac] = new YYARec(283,-77 );yyac++; 
					yya[yyac] = new YYARec(287,-77 );yyac++; 
					yya[yyac] = new YYARec(298,-77 );yyac++; 
					yya[yyac] = new YYARec(300,-77 );yyac++; 
					yya[yyac] = new YYARec(301,-77 );yyac++; 
					yya[yyac] = new YYARec(302,-77 );yyac++; 
					yya[yyac] = new YYARec(303,-77 );yyac++; 
					yya[yyac] = new YYARec(304,-77 );yyac++; 
					yya[yyac] = new YYARec(305,-77 );yyac++; 
					yya[yyac] = new YYARec(306,-77 );yyac++; 
					yya[yyac] = new YYARec(307,-77 );yyac++; 
					yya[yyac] = new YYARec(309,-77 );yyac++; 
					yya[yyac] = new YYARec(310,-77 );yyac++; 
					yya[yyac] = new YYARec(313,-77 );yyac++; 
					yya[yyac] = new YYARec(314,-77 );yyac++; 
					yya[yyac] = new YYARec(315,-77 );yyac++; 
					yya[yyac] = new YYARec(316,-77 );yyac++; 
					yya[yyac] = new YYARec(317,-77 );yyac++; 
					yya[yyac] = new YYARec(318,-77 );yyac++; 
					yya[yyac] = new YYARec(319,-77 );yyac++; 
					yya[yyac] = new YYARec(320,-77 );yyac++; 
					yya[yyac] = new YYARec(321,-77 );yyac++; 
					yya[yyac] = new YYARec(322,-77 );yyac++; 
					yya[yyac] = new YYARec(323,-77 );yyac++; 
					yya[yyac] = new YYARec(324,-77 );yyac++; 
					yya[yyac] = new YYARec(325,-77 );yyac++; 
					yya[yyac] = new YYARec(257,152);yyac++; 
					yya[yyac] = new YYARec(259,153);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,154);yyac++; 
					yya[yyac] = new YYARec(311,155);yyac++; 
					yya[yyac] = new YYARec(312,156);yyac++; 
					yya[yyac] = new YYARec(315,157);yyac++; 
					yya[yyac] = new YYARec(317,158);yyac++; 
					yya[yyac] = new YYARec(319,159);yyac++; 
					yya[yyac] = new YYARec(320,160);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(260,-63 );yyac++; 
					yya[yyac] = new YYARec(261,-63 );yyac++; 
					yya[yyac] = new YYARec(257,152);yyac++; 
					yya[yyac] = new YYARec(259,153);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,154);yyac++; 
					yya[yyac] = new YYARec(311,155);yyac++; 
					yya[yyac] = new YYARec(312,156);yyac++; 
					yya[yyac] = new YYARec(315,157);yyac++; 
					yya[yyac] = new YYARec(317,158);yyac++; 
					yya[yyac] = new YYARec(319,159);yyac++; 
					yya[yyac] = new YYARec(320,160);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(260,-63 );yyac++; 
					yya[yyac] = new YYARec(261,-63 );yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(321,86);yyac++; 
					yya[yyac] = new YYARec(258,-53 );yyac++; 
					yya[yyac] = new YYARec(261,312);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(308,119);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,86);yyac++; 
					yya[yyac] = new YYARec(322,87);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(324,70);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(258,-91 );yyac++; 
					yya[yyac] = new YYARec(257,133);yyac++; 
					yya[yyac] = new YYARec(258,134);yyac++; 
					yya[yyac] = new YYARec(259,135);yyac++; 
					yya[yyac] = new YYARec(266,136);yyac++; 
					yya[yyac] = new YYARec(288,137);yyac++; 
					yya[yyac] = new YYARec(294,138);yyac++; 
					yya[yyac] = new YYARec(295,139);yyac++; 
					yya[yyac] = new YYARec(296,140);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,141);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,142);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(261,-86 );yyac++; 
					yya[yyac] = new YYARec(275,315);yyac++; 
					yya[yyac] = new YYARec(284,245);yyac++; 
					yya[yyac] = new YYARec(285,246);yyac++; 
					yya[yyac] = new YYARec(286,247);yyac++; 
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
					yya[yyac] = new YYARec(282,249);yyac++; 
					yya[yyac] = new YYARec(283,250);yyac++; 
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
					yya[yyac] = new YYARec(278,252);yyac++; 
					yya[yyac] = new YYARec(279,253);yyac++; 
					yya[yyac] = new YYARec(280,254);yyac++; 
					yya[yyac] = new YYARec(281,255);yyac++; 
					yya[yyac] = new YYARec(258,-112 );yyac++; 
					yya[yyac] = new YYARec(266,-112 );yyac++; 
					yya[yyac] = new YYARec(269,-112 );yyac++; 
					yya[yyac] = new YYARec(270,-112 );yyac++; 
					yya[yyac] = new YYARec(271,-112 );yyac++; 
					yya[yyac] = new YYARec(272,-112 );yyac++; 
					yya[yyac] = new YYARec(273,-112 );yyac++; 
					yya[yyac] = new YYARec(275,-112 );yyac++; 
					yya[yyac] = new YYARec(276,-112 );yyac++; 
					yya[yyac] = new YYARec(277,-112 );yyac++; 
					yya[yyac] = new YYARec(276,257);yyac++; 
					yya[yyac] = new YYARec(277,258);yyac++; 
					yya[yyac] = new YYARec(258,-109 );yyac++; 
					yya[yyac] = new YYARec(266,-109 );yyac++; 
					yya[yyac] = new YYARec(269,-109 );yyac++; 
					yya[yyac] = new YYARec(270,-109 );yyac++; 
					yya[yyac] = new YYARec(271,-109 );yyac++; 
					yya[yyac] = new YYARec(272,-109 );yyac++; 
					yya[yyac] = new YYARec(273,-109 );yyac++; 
					yya[yyac] = new YYARec(275,-109 );yyac++; 
					yya[yyac] = new YYARec(273,259);yyac++; 
					yya[yyac] = new YYARec(258,-107 );yyac++; 
					yya[yyac] = new YYARec(266,-107 );yyac++; 
					yya[yyac] = new YYARec(269,-107 );yyac++; 
					yya[yyac] = new YYARec(270,-107 );yyac++; 
					yya[yyac] = new YYARec(271,-107 );yyac++; 
					yya[yyac] = new YYARec(272,-107 );yyac++; 
					yya[yyac] = new YYARec(275,-107 );yyac++; 
					yya[yyac] = new YYARec(272,260);yyac++; 
					yya[yyac] = new YYARec(258,-105 );yyac++; 
					yya[yyac] = new YYARec(266,-105 );yyac++; 
					yya[yyac] = new YYARec(269,-105 );yyac++; 
					yya[yyac] = new YYARec(270,-105 );yyac++; 
					yya[yyac] = new YYARec(271,-105 );yyac++; 
					yya[yyac] = new YYARec(275,-105 );yyac++; 
					yya[yyac] = new YYARec(271,261);yyac++; 
					yya[yyac] = new YYARec(258,-103 );yyac++; 
					yya[yyac] = new YYARec(266,-103 );yyac++; 
					yya[yyac] = new YYARec(269,-103 );yyac++; 
					yya[yyac] = new YYARec(270,-103 );yyac++; 
					yya[yyac] = new YYARec(275,-103 );yyac++; 
					yya[yyac] = new YYARec(270,262);yyac++; 
					yya[yyac] = new YYARec(258,-101 );yyac++; 
					yya[yyac] = new YYARec(266,-101 );yyac++; 
					yya[yyac] = new YYARec(269,-101 );yyac++; 
					yya[yyac] = new YYARec(275,-101 );yyac++; 
					yya[yyac] = new YYARec(267,316);yyac++; 
					yya[yyac] = new YYARec(267,317);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(305,223);yyac++; 
					yya[yyac] = new YYARec(306,154);yyac++; 
					yya[yyac] = new YYARec(311,155);yyac++; 
					yya[yyac] = new YYARec(312,156);yyac++; 
					yya[yyac] = new YYARec(313,224);yyac++; 
					yya[yyac] = new YYARec(315,157);yyac++; 
					yya[yyac] = new YYARec(316,225);yyac++; 
					yya[yyac] = new YYARec(317,158);yyac++; 
					yya[yyac] = new YYARec(318,226);yyac++; 
					yya[yyac] = new YYARec(319,159);yyac++; 
					yya[yyac] = new YYARec(320,160);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(282,83);yyac++; 
					yya[yyac] = new YYARec(283,84);yyac++; 
					yya[yyac] = new YYARec(287,85);yyac++; 
					yya[yyac] = new YYARec(298,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(304,45);yyac++; 
					yya[yyac] = new YYARec(305,46);yyac++; 
					yya[yyac] = new YYARec(306,47);yyac++; 
					yya[yyac] = new YYARec(307,48);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(310,120);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(314,49);yyac++; 
					yya[yyac] = new YYARec(315,50);yyac++; 
					yya[yyac] = new YYARec(316,121);yyac++; 
					yya[yyac] = new YYARec(317,34);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,52);yyac++; 
					yya[yyac] = new YYARec(321,86);yyac++; 
					yya[yyac] = new YYARec(322,87);yyac++; 
					yya[yyac] = new YYARec(323,53);yyac++; 
					yya[yyac] = new YYARec(324,70);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(258,-65 );yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(305,223);yyac++; 
					yya[yyac] = new YYARec(306,154);yyac++; 
					yya[yyac] = new YYARec(311,155);yyac++; 
					yya[yyac] = new YYARec(312,156);yyac++; 
					yya[yyac] = new YYARec(313,224);yyac++; 
					yya[yyac] = new YYARec(315,157);yyac++; 
					yya[yyac] = new YYARec(316,225);yyac++; 
					yya[yyac] = new YYARec(317,158);yyac++; 
					yya[yyac] = new YYARec(318,226);yyac++; 
					yya[yyac] = new YYARec(319,159);yyac++; 
					yya[yyac] = new YYARec(320,160);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(260,323);yyac++; 
					yya[yyac] = new YYARec(261,324);yyac++; 
					yya[yyac] = new YYARec(258,325);yyac++; 
					yya[yyac] = new YYARec(258,326);yyac++; 
					yya[yyac] = new YYARec(261,327);yyac++; 
					yya[yyac] = new YYARec(258,328);yyac++; 
					yya[yyac] = new YYARec(257,152);yyac++; 
					yya[yyac] = new YYARec(259,153);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,154);yyac++; 
					yya[yyac] = new YYARec(311,155);yyac++; 
					yya[yyac] = new YYARec(312,156);yyac++; 
					yya[yyac] = new YYARec(315,157);yyac++; 
					yya[yyac] = new YYARec(317,158);yyac++; 
					yya[yyac] = new YYARec(319,159);yyac++; 
					yya[yyac] = new YYARec(320,160);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(261,-63 );yyac++; 
					yya[yyac] = new YYARec(261,330);yyac++;

					yyg[yygc] = new YYARec(-46,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,16);yygc++; 
					yyg[yygc] = new YYARec(-2,17);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-47,38);yygc++; 
					yyg[yygc] = new YYARec(-39,39);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,41);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,54);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-35,55);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,56);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-29,58);yygc++; 
					yyg[yygc] = new YYARec(-29,60);yygc++; 
					yyg[yygc] = new YYARec(-46,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,61);yygc++; 
					yyg[yygc] = new YYARec(-22,62);yygc++; 
					yyg[yygc] = new YYARec(-21,63);yygc++; 
					yyg[yygc] = new YYARec(-12,64);yygc++; 
					yyg[yygc] = new YYARec(-22,62);yygc++; 
					yyg[yygc] = new YYARec(-21,63);yygc++; 
					yyg[yygc] = new YYARec(-12,66);yygc++; 
					yyg[yygc] = new YYARec(-22,62);yygc++; 
					yyg[yygc] = new YYARec(-21,63);yygc++; 
					yyg[yygc] = new YYARec(-12,67);yygc++; 
					yyg[yygc] = new YYARec(-22,62);yygc++; 
					yyg[yygc] = new YYARec(-21,63);yygc++; 
					yyg[yygc] = new YYARec(-12,68);yygc++; 
					yyg[yygc] = new YYARec(-25,69);yygc++; 
					yyg[yygc] = new YYARec(-29,71);yygc++; 
					yyg[yygc] = new YYARec(-29,72);yygc++; 
					yyg[yygc] = new YYARec(-29,74);yygc++; 
					yyg[yygc] = new YYARec(-76,75);yygc++; 
					yyg[yygc] = new YYARec(-71,76);yygc++; 
					yyg[yygc] = new YYARec(-37,77);yygc++; 
					yyg[yygc] = new YYARec(-32,78);yygc++; 
					yyg[yygc] = new YYARec(-26,79);yygc++; 
					yyg[yygc] = new YYARec(-25,80);yygc++; 
					yyg[yygc] = new YYARec(-23,81);yygc++; 
					yyg[yygc] = new YYARec(-22,82);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,89);yygc++; 
					yyg[yygc] = new YYARec(-30,90);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-23,98);yygc++; 
					yyg[yygc] = new YYARec(-25,100);yygc++; 
					yyg[yygc] = new YYARec(-29,104);yygc++; 
					yyg[yygc] = new YYARec(-46,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-13,106);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,107);yygc++; 
					yyg[yygc] = new YYARec(-46,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-13,108);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,107);yygc++; 
					yyg[yygc] = new YYARec(-76,75);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-71,76);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-37,77);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,113);yygc++; 
					yyg[yygc] = new YYARec(-26,114);yygc++; 
					yyg[yygc] = new YYARec(-25,115);yygc++; 
					yyg[yygc] = new YYARec(-24,116);yygc++; 
					yyg[yygc] = new YYARec(-23,117);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-20,118);yygc++; 
					yyg[yygc] = new YYARec(-75,123);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-52,124);yygc++; 
					yyg[yygc] = new YYARec(-51,125);yygc++; 
					yyg[yygc] = new YYARec(-50,126);yygc++; 
					yyg[yygc] = new YYARec(-49,127);yygc++; 
					yyg[yygc] = new YYARec(-48,128);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,129);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,130);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-16,131);yygc++; 
					yyg[yygc] = new YYARec(-14,132);yygc++; 
					yyg[yygc] = new YYARec(-41,145);yygc++; 
					yyg[yygc] = new YYARec(-40,146);yygc++; 
					yyg[yygc] = new YYARec(-39,147);yygc++; 
					yyg[yygc] = new YYARec(-34,148);yygc++; 
					yyg[yygc] = new YYARec(-22,149);yygc++; 
					yyg[yygc] = new YYARec(-19,150);yygc++; 
					yyg[yygc] = new YYARec(-17,151);yygc++; 
					yyg[yygc] = new YYARec(-29,161);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,89);yygc++; 
					yyg[yygc] = new YYARec(-30,162);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-29,170);yygc++; 
					yyg[yygc] = new YYARec(-75,123);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-52,124);yygc++; 
					yyg[yygc] = new YYARec(-51,125);yygc++; 
					yyg[yygc] = new YYARec(-50,126);yygc++; 
					yyg[yygc] = new YYARec(-49,127);yygc++; 
					yyg[yygc] = new YYARec(-48,128);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,129);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,130);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-16,173);yygc++; 
					yyg[yygc] = new YYARec(-14,132);yygc++; 
					yyg[yygc] = new YYARec(-75,123);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-52,124);yygc++; 
					yyg[yygc] = new YYARec(-51,125);yygc++; 
					yyg[yygc] = new YYARec(-50,126);yygc++; 
					yyg[yygc] = new YYARec(-49,127);yygc++; 
					yyg[yygc] = new YYARec(-48,128);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,129);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,130);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-16,174);yygc++; 
					yyg[yygc] = new YYARec(-14,132);yygc++; 
					yyg[yygc] = new YYARec(-74,176);yygc++; 
					yyg[yygc] = new YYARec(-75,123);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-52,124);yygc++; 
					yyg[yygc] = new YYARec(-51,125);yygc++; 
					yyg[yygc] = new YYARec(-50,126);yygc++; 
					yyg[yygc] = new YYARec(-49,127);yygc++; 
					yyg[yygc] = new YYARec(-48,128);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,129);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,130);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-16,183);yygc++; 
					yyg[yygc] = new YYARec(-14,132);yygc++; 
					yyg[yygc] = new YYARec(-22,62);yygc++; 
					yyg[yygc] = new YYARec(-21,63);yygc++; 
					yyg[yygc] = new YYARec(-12,184);yygc++; 
					yyg[yygc] = new YYARec(-22,62);yygc++; 
					yyg[yygc] = new YYARec(-21,63);yygc++; 
					yyg[yygc] = new YYARec(-12,185);yygc++; 
					yyg[yygc] = new YYARec(-75,123);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-52,124);yygc++; 
					yyg[yygc] = new YYARec(-51,125);yygc++; 
					yyg[yygc] = new YYARec(-50,126);yygc++; 
					yyg[yygc] = new YYARec(-49,127);yygc++; 
					yyg[yygc] = new YYARec(-48,128);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,129);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,130);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-16,186);yygc++; 
					yyg[yygc] = new YYARec(-14,132);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,191);yygc++; 
					yyg[yygc] = new YYARec(-66,192);yygc++; 
					yyg[yygc] = new YYARec(-64,193);yygc++; 
					yyg[yygc] = new YYARec(-62,194);yygc++; 
					yyg[yygc] = new YYARec(-61,195);yygc++; 
					yyg[yygc] = new YYARec(-60,196);yygc++; 
					yyg[yygc] = new YYARec(-59,197);yygc++; 
					yyg[yygc] = new YYARec(-58,198);yygc++; 
					yyg[yygc] = new YYARec(-57,199);yygc++; 
					yyg[yygc] = new YYARec(-56,200);yygc++; 
					yyg[yygc] = new YYARec(-55,201);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,202);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,191);yygc++; 
					yyg[yygc] = new YYARec(-66,192);yygc++; 
					yyg[yygc] = new YYARec(-64,193);yygc++; 
					yyg[yygc] = new YYARec(-62,194);yygc++; 
					yyg[yygc] = new YYARec(-61,195);yygc++; 
					yyg[yygc] = new YYARec(-60,196);yygc++; 
					yyg[yygc] = new YYARec(-59,197);yygc++; 
					yyg[yygc] = new YYARec(-58,198);yygc++; 
					yyg[yygc] = new YYARec(-57,199);yygc++; 
					yyg[yygc] = new YYARec(-56,200);yygc++; 
					yyg[yygc] = new YYARec(-55,207);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,191);yygc++; 
					yyg[yygc] = new YYARec(-66,192);yygc++; 
					yyg[yygc] = new YYARec(-64,193);yygc++; 
					yyg[yygc] = new YYARec(-62,194);yygc++; 
					yyg[yygc] = new YYARec(-61,195);yygc++; 
					yyg[yygc] = new YYARec(-60,196);yygc++; 
					yyg[yygc] = new YYARec(-59,197);yygc++; 
					yyg[yygc] = new YYARec(-58,198);yygc++; 
					yyg[yygc] = new YYARec(-57,199);yygc++; 
					yyg[yygc] = new YYARec(-56,200);yygc++; 
					yyg[yygc] = new YYARec(-55,209);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-29,211);yygc++; 
					yyg[yygc] = new YYARec(-41,145);yygc++; 
					yyg[yygc] = new YYARec(-40,146);yygc++; 
					yyg[yygc] = new YYARec(-39,147);yygc++; 
					yyg[yygc] = new YYARec(-34,148);yygc++; 
					yyg[yygc] = new YYARec(-22,149);yygc++; 
					yyg[yygc] = new YYARec(-19,214);yygc++; 
					yyg[yygc] = new YYARec(-17,151);yygc++; 
					yyg[yygc] = new YYARec(-22,62);yygc++; 
					yyg[yygc] = new YYARec(-21,63);yygc++; 
					yyg[yygc] = new YYARec(-12,215);yygc++; 
					yyg[yygc] = new YYARec(-22,62);yygc++; 
					yyg[yygc] = new YYARec(-21,63);yygc++; 
					yyg[yygc] = new YYARec(-12,216);yygc++; 
					yyg[yygc] = new YYARec(-71,217);yygc++; 
					yyg[yygc] = new YYARec(-37,218);yygc++; 
					yyg[yygc] = new YYARec(-36,219);yygc++; 
					yyg[yygc] = new YYARec(-44,221);yygc++; 
					yyg[yygc] = new YYARec(-41,222);yygc++; 
					yyg[yygc] = new YYARec(-39,147);yygc++; 
					yyg[yygc] = new YYARec(-34,148);yygc++; 
					yyg[yygc] = new YYARec(-22,149);yygc++; 
					yyg[yygc] = new YYARec(-44,227);yygc++; 
					yyg[yygc] = new YYARec(-41,228);yygc++; 
					yyg[yygc] = new YYARec(-39,147);yygc++; 
					yyg[yygc] = new YYARec(-34,148);yygc++; 
					yyg[yygc] = new YYARec(-22,149);yygc++; 
					yyg[yygc] = new YYARec(-76,75);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-71,76);yygc++; 
					yyg[yygc] = new YYARec(-54,229);yygc++; 
					yyg[yygc] = new YYARec(-53,230);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-37,77);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,231);yygc++; 
					yyg[yygc] = new YYARec(-26,232);yygc++; 
					yyg[yygc] = new YYARec(-25,233);yygc++; 
					yyg[yygc] = new YYARec(-24,234);yygc++; 
					yyg[yygc] = new YYARec(-23,235);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,123);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-52,124);yygc++; 
					yyg[yygc] = new YYARec(-51,125);yygc++; 
					yyg[yygc] = new YYARec(-50,126);yygc++; 
					yyg[yygc] = new YYARec(-49,127);yygc++; 
					yyg[yygc] = new YYARec(-48,128);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,129);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,130);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-16,236);yygc++; 
					yyg[yygc] = new YYARec(-14,132);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,191);yygc++; 
					yyg[yygc] = new YYARec(-66,192);yygc++; 
					yyg[yygc] = new YYARec(-64,193);yygc++; 
					yyg[yygc] = new YYARec(-62,194);yygc++; 
					yyg[yygc] = new YYARec(-61,195);yygc++; 
					yyg[yygc] = new YYARec(-60,196);yygc++; 
					yyg[yygc] = new YYARec(-59,197);yygc++; 
					yyg[yygc] = new YYARec(-58,198);yygc++; 
					yyg[yygc] = new YYARec(-57,199);yygc++; 
					yyg[yygc] = new YYARec(-56,200);yygc++; 
					yyg[yygc] = new YYARec(-55,237);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,123);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-52,124);yygc++; 
					yyg[yygc] = new YYARec(-51,125);yygc++; 
					yyg[yygc] = new YYARec(-50,126);yygc++; 
					yyg[yygc] = new YYARec(-49,127);yygc++; 
					yyg[yygc] = new YYARec(-48,128);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,129);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,130);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-16,238);yygc++; 
					yyg[yygc] = new YYARec(-15,239);yygc++; 
					yyg[yygc] = new YYARec(-14,132);yygc++; 
					yyg[yygc] = new YYARec(-75,123);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-52,124);yygc++; 
					yyg[yygc] = new YYARec(-51,125);yygc++; 
					yyg[yygc] = new YYARec(-50,126);yygc++; 
					yyg[yygc] = new YYARec(-49,127);yygc++; 
					yyg[yygc] = new YYARec(-48,128);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,129);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,130);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-16,238);yygc++; 
					yyg[yygc] = new YYARec(-15,240);yygc++; 
					yyg[yygc] = new YYARec(-14,132);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,243);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-69,244);yygc++; 
					yyg[yygc] = new YYARec(-67,248);yygc++; 
					yyg[yygc] = new YYARec(-65,251);yygc++; 
					yyg[yygc] = new YYARec(-63,256);yygc++; 
					yyg[yygc] = new YYARec(-74,264);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,191);yygc++; 
					yyg[yygc] = new YYARec(-66,192);yygc++; 
					yyg[yygc] = new YYARec(-64,193);yygc++; 
					yyg[yygc] = new YYARec(-62,194);yygc++; 
					yyg[yygc] = new YYARec(-61,195);yygc++; 
					yyg[yygc] = new YYARec(-60,196);yygc++; 
					yyg[yygc] = new YYARec(-59,197);yygc++; 
					yyg[yygc] = new YYARec(-58,198);yygc++; 
					yyg[yygc] = new YYARec(-57,199);yygc++; 
					yyg[yygc] = new YYARec(-56,200);yygc++; 
					yyg[yygc] = new YYARec(-55,265);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,123);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-52,124);yygc++; 
					yyg[yygc] = new YYARec(-51,125);yygc++; 
					yyg[yygc] = new YYARec(-50,126);yygc++; 
					yyg[yygc] = new YYARec(-49,127);yygc++; 
					yyg[yygc] = new YYARec(-48,128);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,129);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,130);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-16,266);yygc++; 
					yyg[yygc] = new YYARec(-14,132);yygc++; 
					yyg[yygc] = new YYARec(-75,123);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-52,124);yygc++; 
					yyg[yygc] = new YYARec(-51,125);yygc++; 
					yyg[yygc] = new YYARec(-50,126);yygc++; 
					yyg[yygc] = new YYARec(-49,127);yygc++; 
					yyg[yygc] = new YYARec(-48,128);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,129);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,130);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-16,269);yygc++; 
					yyg[yygc] = new YYARec(-14,132);yygc++; 
					yyg[yygc] = new YYARec(-76,75);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-71,76);yygc++; 
					yyg[yygc] = new YYARec(-46,270);yygc++; 
					yyg[yygc] = new YYARec(-45,271);yygc++; 
					yyg[yygc] = new YYARec(-43,272);yygc++; 
					yyg[yygc] = new YYARec(-42,273);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,274);yygc++; 
					yyg[yygc] = new YYARec(-37,77);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,275);yygc++; 
					yyg[yygc] = new YYARec(-26,276);yygc++; 
					yyg[yygc] = new YYARec(-25,277);yygc++; 
					yyg[yygc] = new YYARec(-23,278);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-41,145);yygc++; 
					yyg[yygc] = new YYARec(-40,146);yygc++; 
					yyg[yygc] = new YYARec(-39,147);yygc++; 
					yyg[yygc] = new YYARec(-34,148);yygc++; 
					yyg[yygc] = new YYARec(-22,149);yygc++; 
					yyg[yygc] = new YYARec(-19,279);yygc++; 
					yyg[yygc] = new YYARec(-17,151);yygc++; 
					yyg[yygc] = new YYARec(-46,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,284);yygc++; 
					yyg[yygc] = new YYARec(-29,285);yygc++; 
					yyg[yygc] = new YYARec(-75,123);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-52,124);yygc++; 
					yyg[yygc] = new YYARec(-51,125);yygc++; 
					yyg[yygc] = new YYARec(-50,126);yygc++; 
					yyg[yygc] = new YYARec(-49,127);yygc++; 
					yyg[yygc] = new YYARec(-48,128);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,129);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,130);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-16,289);yygc++; 
					yyg[yygc] = new YYARec(-14,132);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,191);yygc++; 
					yyg[yygc] = new YYARec(-66,192);yygc++; 
					yyg[yygc] = new YYARec(-64,193);yygc++; 
					yyg[yygc] = new YYARec(-62,194);yygc++; 
					yyg[yygc] = new YYARec(-61,195);yygc++; 
					yyg[yygc] = new YYARec(-60,196);yygc++; 
					yyg[yygc] = new YYARec(-59,197);yygc++; 
					yyg[yygc] = new YYARec(-58,198);yygc++; 
					yyg[yygc] = new YYARec(-57,199);yygc++; 
					yyg[yygc] = new YYARec(-56,200);yygc++; 
					yyg[yygc] = new YYARec(-55,290);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,291);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,191);yygc++; 
					yyg[yygc] = new YYARec(-66,292);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,191);yygc++; 
					yyg[yygc] = new YYARec(-66,192);yygc++; 
					yyg[yygc] = new YYARec(-64,293);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,191);yygc++; 
					yyg[yygc] = new YYARec(-66,192);yygc++; 
					yyg[yygc] = new YYARec(-64,193);yygc++; 
					yyg[yygc] = new YYARec(-62,294);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,191);yygc++; 
					yyg[yygc] = new YYARec(-66,192);yygc++; 
					yyg[yygc] = new YYARec(-64,193);yygc++; 
					yyg[yygc] = new YYARec(-62,194);yygc++; 
					yyg[yygc] = new YYARec(-61,295);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,191);yygc++; 
					yyg[yygc] = new YYARec(-66,192);yygc++; 
					yyg[yygc] = new YYARec(-64,193);yygc++; 
					yyg[yygc] = new YYARec(-62,194);yygc++; 
					yyg[yygc] = new YYARec(-61,195);yygc++; 
					yyg[yygc] = new YYARec(-60,296);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,191);yygc++; 
					yyg[yygc] = new YYARec(-66,192);yygc++; 
					yyg[yygc] = new YYARec(-64,193);yygc++; 
					yyg[yygc] = new YYARec(-62,194);yygc++; 
					yyg[yygc] = new YYARec(-61,195);yygc++; 
					yyg[yygc] = new YYARec(-60,196);yygc++; 
					yyg[yygc] = new YYARec(-59,297);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,191);yygc++; 
					yyg[yygc] = new YYARec(-66,192);yygc++; 
					yyg[yygc] = new YYARec(-64,193);yygc++; 
					yyg[yygc] = new YYARec(-62,194);yygc++; 
					yyg[yygc] = new YYARec(-61,195);yygc++; 
					yyg[yygc] = new YYARec(-60,196);yygc++; 
					yyg[yygc] = new YYARec(-59,197);yygc++; 
					yyg[yygc] = new YYARec(-58,298);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,191);yygc++; 
					yyg[yygc] = new YYARec(-66,192);yygc++; 
					yyg[yygc] = new YYARec(-64,193);yygc++; 
					yyg[yygc] = new YYARec(-62,194);yygc++; 
					yyg[yygc] = new YYARec(-61,195);yygc++; 
					yyg[yygc] = new YYARec(-60,196);yygc++; 
					yyg[yygc] = new YYARec(-59,197);yygc++; 
					yyg[yygc] = new YYARec(-58,198);yygc++; 
					yyg[yygc] = new YYARec(-57,299);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-73,187);yygc++; 
					yyg[yygc] = new YYARec(-72,188);yygc++; 
					yyg[yygc] = new YYARec(-71,189);yygc++; 
					yyg[yygc] = new YYARec(-70,190);yygc++; 
					yyg[yygc] = new YYARec(-68,191);yygc++; 
					yyg[yygc] = new YYARec(-66,192);yygc++; 
					yyg[yygc] = new YYARec(-64,193);yygc++; 
					yyg[yygc] = new YYARec(-62,194);yygc++; 
					yyg[yygc] = new YYARec(-61,195);yygc++; 
					yyg[yygc] = new YYARec(-60,196);yygc++; 
					yyg[yygc] = new YYARec(-59,197);yygc++; 
					yyg[yygc] = new YYARec(-58,198);yygc++; 
					yyg[yygc] = new YYARec(-57,199);yygc++; 
					yyg[yygc] = new YYARec(-56,200);yygc++; 
					yyg[yygc] = new YYARec(-55,300);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,208);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,123);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-52,124);yygc++; 
					yyg[yygc] = new YYARec(-51,125);yygc++; 
					yyg[yygc] = new YYARec(-50,126);yygc++; 
					yyg[yygc] = new YYARec(-49,127);yygc++; 
					yyg[yygc] = new YYARec(-48,128);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,129);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,130);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-16,303);yygc++; 
					yyg[yygc] = new YYARec(-14,132);yygc++; 
					yyg[yygc] = new YYARec(-75,123);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-52,124);yygc++; 
					yyg[yygc] = new YYARec(-51,125);yygc++; 
					yyg[yygc] = new YYARec(-50,126);yygc++; 
					yyg[yygc] = new YYARec(-49,127);yygc++; 
					yyg[yygc] = new YYARec(-48,128);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,129);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,130);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-16,304);yygc++; 
					yyg[yygc] = new YYARec(-14,132);yygc++; 
					yyg[yygc] = new YYARec(-29,306);yygc++; 
					yyg[yygc] = new YYARec(-41,145);yygc++; 
					yyg[yygc] = new YYARec(-40,146);yygc++; 
					yyg[yygc] = new YYARec(-39,147);yygc++; 
					yyg[yygc] = new YYARec(-34,148);yygc++; 
					yyg[yygc] = new YYARec(-22,149);yygc++; 
					yyg[yygc] = new YYARec(-19,308);yygc++; 
					yyg[yygc] = new YYARec(-18,309);yygc++; 
					yyg[yygc] = new YYARec(-17,151);yygc++; 
					yyg[yygc] = new YYARec(-41,145);yygc++; 
					yyg[yygc] = new YYARec(-40,146);yygc++; 
					yyg[yygc] = new YYARec(-39,147);yygc++; 
					yyg[yygc] = new YYARec(-34,148);yygc++; 
					yyg[yygc] = new YYARec(-22,149);yygc++; 
					yyg[yygc] = new YYARec(-19,308);yygc++; 
					yyg[yygc] = new YYARec(-18,310);yygc++; 
					yyg[yygc] = new YYARec(-17,151);yygc++; 
					yyg[yygc] = new YYARec(-71,217);yygc++; 
					yyg[yygc] = new YYARec(-37,218);yygc++; 
					yyg[yygc] = new YYARec(-36,311);yygc++; 
					yyg[yygc] = new YYARec(-76,75);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-71,76);yygc++; 
					yyg[yygc] = new YYARec(-54,229);yygc++; 
					yyg[yygc] = new YYARec(-53,313);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-37,77);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,231);yygc++; 
					yyg[yygc] = new YYARec(-26,232);yygc++; 
					yyg[yygc] = new YYARec(-25,233);yygc++; 
					yyg[yygc] = new YYARec(-24,234);yygc++; 
					yyg[yygc] = new YYARec(-23,235);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-75,123);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-52,124);yygc++; 
					yyg[yygc] = new YYARec(-51,125);yygc++; 
					yyg[yygc] = new YYARec(-50,126);yygc++; 
					yyg[yygc] = new YYARec(-49,127);yygc++; 
					yyg[yygc] = new YYARec(-48,128);yygc++; 
					yyg[yygc] = new YYARec(-45,109);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,110);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,129);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-27,130);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-16,314);yygc++; 
					yyg[yygc] = new YYARec(-14,132);yygc++; 
					yyg[yygc] = new YYARec(-69,244);yygc++; 
					yyg[yygc] = new YYARec(-67,248);yygc++; 
					yyg[yygc] = new YYARec(-65,251);yygc++; 
					yyg[yygc] = new YYARec(-63,256);yygc++; 
					yyg[yygc] = new YYARec(-44,318);yygc++; 
					yyg[yygc] = new YYARec(-41,319);yygc++; 
					yyg[yygc] = new YYARec(-39,147);yygc++; 
					yyg[yygc] = new YYARec(-34,148);yygc++; 
					yyg[yygc] = new YYARec(-22,149);yygc++; 
					yyg[yygc] = new YYARec(-76,75);yygc++; 
					yyg[yygc] = new YYARec(-75,36);yygc++; 
					yyg[yygc] = new YYARec(-72,37);yygc++; 
					yyg[yygc] = new YYARec(-71,76);yygc++; 
					yyg[yygc] = new YYARec(-46,270);yygc++; 
					yyg[yygc] = new YYARec(-45,271);yygc++; 
					yyg[yygc] = new YYARec(-43,272);yygc++; 
					yyg[yygc] = new YYARec(-42,320);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,274);yygc++; 
					yyg[yygc] = new YYARec(-37,77);yygc++; 
					yyg[yygc] = new YYARec(-34,40);yygc++; 
					yyg[yygc] = new YYARec(-33,275);yygc++; 
					yyg[yygc] = new YYARec(-26,276);yygc++; 
					yyg[yygc] = new YYARec(-25,277);yygc++; 
					yyg[yygc] = new YYARec(-23,278);yygc++; 
					yyg[yygc] = new YYARec(-21,42);yygc++; 
					yyg[yygc] = new YYARec(-44,321);yygc++; 
					yyg[yygc] = new YYARec(-41,322);yygc++; 
					yyg[yygc] = new YYARec(-39,147);yygc++; 
					yyg[yygc] = new YYARec(-34,148);yygc++; 
					yyg[yygc] = new YYARec(-22,149);yygc++; 
					yyg[yygc] = new YYARec(-41,145);yygc++; 
					yyg[yygc] = new YYARec(-40,146);yygc++; 
					yyg[yygc] = new YYARec(-39,147);yygc++; 
					yyg[yygc] = new YYARec(-34,148);yygc++; 
					yyg[yygc] = new YYARec(-22,149);yygc++; 
					yyg[yygc] = new YYARec(-19,329);yygc++; 
					yyg[yygc] = new YYARec(-17,151);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = 0;  
					yyd[2] = -60;  
					yyd[3] = 0;  
					yyd[4] = 0;  
					yyd[5] = 0;  
					yyd[6] = 0;  
					yyd[7] = -41;  
					yyd[8] = -12;  
					yyd[9] = -11;  
					yyd[10] = -10;  
					yyd[11] = -9;  
					yyd[12] = -8;  
					yyd[13] = -7;  
					yyd[14] = -6;  
					yyd[15] = 0;  
					yyd[16] = -1;  
					yyd[17] = 0;  
					yyd[18] = 0;  
					yyd[19] = -4;  
					yyd[20] = 0;  
					yyd[21] = 0;  
					yyd[22] = 0;  
					yyd[23] = 0;  
					yyd[24] = -5;  
					yyd[25] = -43;  
					yyd[26] = -40;  
					yyd[27] = -54;  
					yyd[28] = -58;  
					yyd[29] = -79;  
					yyd[30] = -194;  
					yyd[31] = -38;  
					yyd[32] = -42;  
					yyd[33] = -59;  
					yyd[34] = -193;  
					yyd[35] = -39;  
					yyd[36] = -202;  
					yyd[37] = -204;  
					yyd[38] = 0;  
					yyd[39] = -207;  
					yyd[40] = -203;  
					yyd[41] = -208;  
					yyd[42] = -205;  
					yyd[43] = -206;  
					yyd[44] = -199;  
					yyd[45] = -149;  
					yyd[46] = -198;  
					yyd[47] = -201;  
					yyd[48] = -180;  
					yyd[49] = -147;  
					yyd[50] = -148;  
					yyd[51] = -178;  
					yyd[52] = -179;  
					yyd[53] = -200;  
					yyd[54] = 0;  
					yyd[55] = 0;  
					yyd[56] = -192;  
					yyd[57] = -191;  
					yyd[58] = 0;  
					yyd[59] = -98;  
					yyd[60] = 0;  
					yyd[61] = -2;  
					yyd[62] = -29;  
					yyd[63] = -28;  
					yyd[64] = 0;  
					yyd[65] = -189;  
					yyd[66] = 0;  
					yyd[67] = 0;  
					yyd[68] = 0;  
					yyd[69] = 0;  
					yyd[70] = -211;  
					yyd[71] = 0;  
					yyd[72] = 0;  
					yyd[73] = -57;  
					yyd[74] = 0;  
					yyd[75] = -181;  
					yyd[76] = 0;  
					yyd[77] = -182;  
					yyd[78] = 0;  
					yyd[79] = -48;  
					yyd[80] = -49;  
					yyd[81] = -46;  
					yyd[82] = -47;  
					yyd[83] = -137;  
					yyd[84] = -138;  
					yyd[85] = -136;  
					yyd[86] = -186;  
					yyd[87] = -188;  
					yyd[88] = -212;  
					yyd[89] = 0;  
					yyd[90] = 0;  
					yyd[91] = 0;  
					yyd[92] = 0;  
					yyd[93] = -26;  
					yyd[94] = 0;  
					yyd[95] = -27;  
					yyd[96] = -35;  
					yyd[97] = 0;  
					yyd[98] = 0;  
					yyd[99] = 0;  
					yyd[100] = 0;  
					yyd[101] = -185;  
					yyd[102] = -187;  
					yyd[103] = -37;  
					yyd[104] = 0;  
					yyd[105] = -36;  
					yyd[106] = 0;  
					yyd[107] = 0;  
					yyd[108] = 0;  
					yyd[109] = 0;  
					yyd[110] = -158;  
					yyd[111] = 0;  
					yyd[112] = -157;  
					yyd[113] = -34;  
					yyd[114] = -33;  
					yyd[115] = -32;  
					yyd[116] = -31;  
					yyd[117] = -30;  
					yyd[118] = 0;  
					yyd[119] = -190;  
					yyd[120] = -197;  
					yyd[121] = -196;  
					yyd[122] = -195;  
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
					yyd[134] = -87;  
					yyd[135] = 0;  
					yyd[136] = 0;  
					yyd[137] = 0;  
					yyd[138] = 0;  
					yyd[139] = 0;  
					yyd[140] = 0;  
					yyd[141] = 0;  
					yyd[142] = 0;  
					yyd[143] = 0;  
					yyd[144] = -56;  
					yyd[145] = 0;  
					yyd[146] = 0;  
					yyd[147] = -173;  
					yyd[148] = -174;  
					yyd[149] = -172;  
					yyd[150] = 0;  
					yyd[151] = 0;  
					yyd[152] = 0;  
					yyd[153] = 0;  
					yyd[154] = -171;  
					yyd[155] = -167;  
					yyd[156] = -166;  
					yyd[157] = -169;  
					yyd[158] = -170;  
					yyd[159] = -168;  
					yyd[160] = -165;  
					yyd[161] = 0;  
					yyd[162] = -45;  
					yyd[163] = -13;  
					yyd[164] = 0;  
					yyd[165] = -16;  
					yyd[166] = -14;  
					yyd[167] = 0;  
					yyd[168] = 0;  
					yyd[169] = -25;  
					yyd[170] = 0;  
					yyd[171] = -90;  
					yyd[172] = -88;  
					yyd[173] = -85;  
					yyd[174] = -83;  
					yyd[175] = 0;  
					yyd[176] = 0;  
					yyd[177] = -142;  
					yyd[178] = -143;  
					yyd[179] = -144;  
					yyd[180] = -145;  
					yyd[181] = -146;  
					yyd[182] = -78;  
					yyd[183] = -84;  
					yyd[184] = 0;  
					yyd[185] = 0;  
					yyd[186] = 0;  
					yyd[187] = -123;  
					yyd[188] = 0;  
					yyd[189] = 0;  
					yyd[190] = -119;  
					yyd[191] = -117;  
					yyd[192] = 0;  
					yyd[193] = 0;  
					yyd[194] = 0;  
					yyd[195] = 0;  
					yyd[196] = 0;  
					yyd[197] = 0;  
					yyd[198] = 0;  
					yyd[199] = 0;  
					yyd[200] = 0;  
					yyd[201] = -139;  
					yyd[202] = 0;  
					yyd[203] = 0;  
					yyd[204] = -184;  
					yyd[205] = -183;  
					yyd[206] = 0;  
					yyd[207] = 0;  
					yyd[208] = -124;  
					yyd[209] = 0;  
					yyd[210] = 0;  
					yyd[211] = 0;  
					yyd[212] = 0;  
					yyd[213] = -55;  
					yyd[214] = -62;  
					yyd[215] = 0;  
					yyd[216] = 0;  
					yyd[217] = 0;  
					yyd[218] = 0;  
					yyd[219] = 0;  
					yyd[220] = 0;  
					yyd[221] = -154;  
					yyd[222] = -155;  
					yyd[223] = -164;  
					yyd[224] = -162;  
					yyd[225] = -163;  
					yyd[226] = -161;  
					yyd[227] = -153;  
					yyd[228] = -156;  
					yyd[229] = 0;  
					yyd[230] = 0;  
					yyd[231] = -97;  
					yyd[232] = -96;  
					yyd[233] = -95;  
					yyd[234] = -94;  
					yyd[235] = -93;  
					yyd[236] = -82;  
					yyd[237] = -141;  
					yyd[238] = 0;  
					yyd[239] = -17;  
					yyd[240] = -18;  
					yyd[241] = 0;  
					yyd[242] = 0;  
					yyd[243] = -120;  
					yyd[244] = 0;  
					yyd[245] = -133;  
					yyd[246] = -134;  
					yyd[247] = -135;  
					yyd[248] = 0;  
					yyd[249] = -131;  
					yyd[250] = -132;  
					yyd[251] = 0;  
					yyd[252] = -127;  
					yyd[253] = -128;  
					yyd[254] = -129;  
					yyd[255] = -130;  
					yyd[256] = 0;  
					yyd[257] = -125;  
					yyd[258] = -126;  
					yyd[259] = 0;  
					yyd[260] = 0;  
					yyd[261] = 0;  
					yyd[262] = 0;  
					yyd[263] = 0;  
					yyd[264] = 0;  
					yyd[265] = 0;  
					yyd[266] = 0;  
					yyd[267] = 0;  
					yyd[268] = 0;  
					yyd[269] = -80;  
					yyd[270] = -73;  
					yyd[271] = 0;  
					yyd[272] = 0;  
					yyd[273] = -64;  
					yyd[274] = -74;  
					yyd[275] = 0;  
					yyd[276] = -75;  
					yyd[277] = -72;  
					yyd[278] = -71;  
					yyd[279] = -61;  
					yyd[280] = 0;  
					yyd[281] = 0;  
					yyd[282] = 0;  
					yyd[283] = -50;  
					yyd[284] = 0;  
					yyd[285] = 0;  
					yyd[286] = -89;  
					yyd[287] = 0;  
					yyd[288] = -20;  
					yyd[289] = -81;  
					yyd[290] = 0;  
					yyd[291] = -118;  
					yyd[292] = 0;  
					yyd[293] = 0;  
					yyd[294] = 0;  
					yyd[295] = 0;  
					yyd[296] = 0;  
					yyd[297] = 0;  
					yyd[298] = 0;  
					yyd[299] = 0;  
					yyd[300] = -140;  
					yyd[301] = -122;  
					yyd[302] = -150;  
					yyd[303] = 0;  
					yyd[304] = 0;  
					yyd[305] = 0;  
					yyd[306] = 0;  
					yyd[307] = 0;  
					yyd[308] = 0;  
					yyd[309] = 0;  
					yyd[310] = 0;  
					yyd[311] = -51;  
					yyd[312] = -15;  
					yyd[313] = -92;  
					yyd[314] = 0;  
					yyd[315] = -121;  
					yyd[316] = -151;  
					yyd[317] = -152;  
					yyd[318] = -69;  
					yyd[319] = -68;  
					yyd[320] = -66;  
					yyd[321] = -67;  
					yyd[322] = -70;  
					yyd[323] = 0;  
					yyd[324] = -24;  
					yyd[325] = -21;  
					yyd[326] = -22;  
					yyd[327] = -19;  
					yyd[328] = 0;  
					yyd[329] = 0;  
					yyd[330] = -23; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 20;  
					yyal[2] = 35;  
					yyal[3] = 35;  
					yyal[4] = 49;  
					yyal[5] = 64;  
					yyal[6] = 73;  
					yyal[7] = 88;  
					yyal[8] = 88;  
					yyal[9] = 88;  
					yyal[10] = 88;  
					yyal[11] = 88;  
					yyal[12] = 88;  
					yyal[13] = 88;  
					yyal[14] = 88;  
					yyal[15] = 88;  
					yyal[16] = 109;  
					yyal[17] = 109;  
					yyal[18] = 110;  
					yyal[19] = 113;  
					yyal[20] = 113;  
					yyal[21] = 116;  
					yyal[22] = 119;  
					yyal[23] = 122;  
					yyal[24] = 123;  
					yyal[25] = 123;  
					yyal[26] = 123;  
					yyal[27] = 123;  
					yyal[28] = 123;  
					yyal[29] = 123;  
					yyal[30] = 123;  
					yyal[31] = 123;  
					yyal[32] = 123;  
					yyal[33] = 123;  
					yyal[34] = 123;  
					yyal[35] = 123;  
					yyal[36] = 123;  
					yyal[37] = 123;  
					yyal[38] = 123;  
					yyal[39] = 125;  
					yyal[40] = 125;  
					yyal[41] = 125;  
					yyal[42] = 125;  
					yyal[43] = 125;  
					yyal[44] = 125;  
					yyal[45] = 125;  
					yyal[46] = 125;  
					yyal[47] = 125;  
					yyal[48] = 125;  
					yyal[49] = 125;  
					yyal[50] = 125;  
					yyal[51] = 125;  
					yyal[52] = 125;  
					yyal[53] = 125;  
					yyal[54] = 125;  
					yyal[55] = 129;  
					yyal[56] = 131;  
					yyal[57] = 131;  
					yyal[58] = 131;  
					yyal[59] = 139;  
					yyal[60] = 139;  
					yyal[61] = 153;  
					yyal[62] = 153;  
					yyal[63] = 153;  
					yyal[64] = 153;  
					yyal[65] = 154;  
					yyal[66] = 154;  
					yyal[67] = 155;  
					yyal[68] = 157;  
					yyal[69] = 158;  
					yyal[70] = 159;  
					yyal[71] = 159;  
					yyal[72] = 160;  
					yyal[73] = 162;  
					yyal[74] = 162;  
					yyal[75] = 163;  
					yyal[76] = 163;  
					yyal[77] = 165;  
					yyal[78] = 165;  
					yyal[79] = 166;  
					yyal[80] = 166;  
					yyal[81] = 166;  
					yyal[82] = 166;  
					yyal[83] = 166;  
					yyal[84] = 166;  
					yyal[85] = 166;  
					yyal[86] = 166;  
					yyal[87] = 166;  
					yyal[88] = 166;  
					yyal[89] = 166;  
					yyal[90] = 182;  
					yyal[91] = 183;  
					yyal[92] = 203;  
					yyal[93] = 223;  
					yyal[94] = 223;  
					yyal[95] = 252;  
					yyal[96] = 252;  
					yyal[97] = 252;  
					yyal[98] = 282;  
					yyal[99] = 283;  
					yyal[100] = 296;  
					yyal[101] = 302;  
					yyal[102] = 302;  
					yyal[103] = 302;  
					yyal[104] = 302;  
					yyal[105] = 317;  
					yyal[106] = 317;  
					yyal[107] = 318;  
					yyal[108] = 320;  
					yyal[109] = 321;  
					yyal[110] = 374;  
					yyal[111] = 374;  
					yyal[112] = 427;  
					yyal[113] = 427;  
					yyal[114] = 427;  
					yyal[115] = 427;  
					yyal[116] = 427;  
					yyal[117] = 427;  
					yyal[118] = 427;  
					yyal[119] = 428;  
					yyal[120] = 428;  
					yyal[121] = 428;  
					yyal[122] = 428;  
					yyal[123] = 428;  
					yyal[124] = 466;  
					yyal[125] = 497;  
					yyal[126] = 498;  
					yyal[127] = 530;  
					yyal[128] = 562;  
					yyal[129] = 563;  
					yyal[130] = 570;  
					yyal[131] = 575;  
					yyal[132] = 576;  
					yyal[133] = 608;  
					yyal[134] = 611;  
					yyal[135] = 611;  
					yyal[136] = 614;  
					yyal[137] = 644;  
					yyal[138] = 671;  
					yyal[139] = 703;  
					yyal[140] = 730;  
					yyal[141] = 757;  
					yyal[142] = 764;  
					yyal[143] = 802;  
					yyal[144] = 810;  
					yyal[145] = 810;  
					yyal[146] = 838;  
					yyal[147] = 839;  
					yyal[148] = 839;  
					yyal[149] = 839;  
					yyal[150] = 839;  
					yyal[151] = 840;  
					yyal[152] = 855;  
					yyal[153] = 858;  
					yyal[154] = 861;  
					yyal[155] = 861;  
					yyal[156] = 861;  
					yyal[157] = 861;  
					yyal[158] = 861;  
					yyal[159] = 861;  
					yyal[160] = 861;  
					yyal[161] = 861;  
					yyal[162] = 866;  
					yyal[163] = 866;  
					yyal[164] = 866;  
					yyal[165] = 867;  
					yyal[166] = 867;  
					yyal[167] = 867;  
					yyal[168] = 881;  
					yyal[169] = 895;  
					yyal[170] = 895;  
					yyal[171] = 924;  
					yyal[172] = 924;  
					yyal[173] = 924;  
					yyal[174] = 924;  
					yyal[175] = 924;  
					yyal[176] = 956;  
					yyal[177] = 983;  
					yyal[178] = 983;  
					yyal[179] = 983;  
					yyal[180] = 983;  
					yyal[181] = 983;  
					yyal[182] = 983;  
					yyal[183] = 983;  
					yyal[184] = 983;  
					yyal[185] = 1014;  
					yyal[186] = 1045;  
					yyal[187] = 1046;  
					yyal[188] = 1046;  
					yyal[189] = 1072;  
					yyal[190] = 1099;  
					yyal[191] = 1099;  
					yyal[192] = 1099;  
					yyal[193] = 1118;  
					yyal[194] = 1134;  
					yyal[195] = 1148;  
					yyal[196] = 1158;  
					yyal[197] = 1166;  
					yyal[198] = 1173;  
					yyal[199] = 1179;  
					yyal[200] = 1184;  
					yyal[201] = 1188;  
					yyal[202] = 1188;  
					yyal[203] = 1210;  
					yyal[204] = 1237;  
					yyal[205] = 1237;  
					yyal[206] = 1237;  
					yyal[207] = 1267;  
					yyal[208] = 1268;  
					yyal[209] = 1268;  
					yyal[210] = 1269;  
					yyal[211] = 1301;  
					yyal[212] = 1328;  
					yyal[213] = 1343;  
					yyal[214] = 1343;  
					yyal[215] = 1343;  
					yyal[216] = 1344;  
					yyal[217] = 1345;  
					yyal[218] = 1346;  
					yyal[219] = 1348;  
					yyal[220] = 1349;  
					yyal[221] = 1368;  
					yyal[222] = 1368;  
					yyal[223] = 1368;  
					yyal[224] = 1368;  
					yyal[225] = 1368;  
					yyal[226] = 1368;  
					yyal[227] = 1368;  
					yyal[228] = 1368;  
					yyal[229] = 1368;  
					yyal[230] = 1399;  
					yyal[231] = 1400;  
					yyal[232] = 1400;  
					yyal[233] = 1400;  
					yyal[234] = 1400;  
					yyal[235] = 1400;  
					yyal[236] = 1400;  
					yyal[237] = 1400;  
					yyal[238] = 1400;  
					yyal[239] = 1402;  
					yyal[240] = 1402;  
					yyal[241] = 1402;  
					yyal[242] = 1434;  
					yyal[243] = 1461;  
					yyal[244] = 1461;  
					yyal[245] = 1488;  
					yyal[246] = 1488;  
					yyal[247] = 1488;  
					yyal[248] = 1488;  
					yyal[249] = 1515;  
					yyal[250] = 1515;  
					yyal[251] = 1515;  
					yyal[252] = 1542;  
					yyal[253] = 1542;  
					yyal[254] = 1542;  
					yyal[255] = 1542;  
					yyal[256] = 1542;  
					yyal[257] = 1569;  
					yyal[258] = 1569;  
					yyal[259] = 1569;  
					yyal[260] = 1596;  
					yyal[261] = 1623;  
					yyal[262] = 1650;  
					yyal[263] = 1677;  
					yyal[264] = 1704;  
					yyal[265] = 1731;  
					yyal[266] = 1732;  
					yyal[267] = 1733;  
					yyal[268] = 1763;  
					yyal[269] = 1793;  
					yyal[270] = 1793;  
					yyal[271] = 1793;  
					yyal[272] = 1823;  
					yyal[273] = 1852;  
					yyal[274] = 1852;  
					yyal[275] = 1852;  
					yyal[276] = 1882;  
					yyal[277] = 1882;  
					yyal[278] = 1882;  
					yyal[279] = 1882;  
					yyal[280] = 1882;  
					yyal[281] = 1896;  
					yyal[282] = 1910;  
					yyal[283] = 1915;  
					yyal[284] = 1915;  
					yyal[285] = 1916;  
					yyal[286] = 1946;  
					yyal[287] = 1946;  
					yyal[288] = 1976;  
					yyal[289] = 1976;  
					yyal[290] = 1976;  
					yyal[291] = 1977;  
					yyal[292] = 1977;  
					yyal[293] = 1996;  
					yyal[294] = 2012;  
					yyal[295] = 2026;  
					yyal[296] = 2036;  
					yyal[297] = 2044;  
					yyal[298] = 2051;  
					yyal[299] = 2057;  
					yyal[300] = 2062;  
					yyal[301] = 2062;  
					yyal[302] = 2062;  
					yyal[303] = 2062;  
					yyal[304] = 2063;  
					yyal[305] = 2064;  
					yyal[306] = 2078;  
					yyal[307] = 2106;  
					yyal[308] = 2120;  
					yyal[309] = 2122;  
					yyal[310] = 2123;  
					yyal[311] = 2124;  
					yyal[312] = 2124;  
					yyal[313] = 2124;  
					yyal[314] = 2124;  
					yyal[315] = 2125;  
					yyal[316] = 2125;  
					yyal[317] = 2125;  
					yyal[318] = 2125;  
					yyal[319] = 2125;  
					yyal[320] = 2125;  
					yyal[321] = 2125;  
					yyal[322] = 2125;  
					yyal[323] = 2125;  
					yyal[324] = 2126;  
					yyal[325] = 2126;  
					yyal[326] = 2126;  
					yyal[327] = 2126;  
					yyal[328] = 2126;  
					yyal[329] = 2139;  
					yyal[330] = 2140; 

					yyah = new int[yynstates];
					yyah[0] = 19;  
					yyah[1] = 34;  
					yyah[2] = 34;  
					yyah[3] = 48;  
					yyah[4] = 63;  
					yyah[5] = 72;  
					yyah[6] = 87;  
					yyah[7] = 87;  
					yyah[8] = 87;  
					yyah[9] = 87;  
					yyah[10] = 87;  
					yyah[11] = 87;  
					yyah[12] = 87;  
					yyah[13] = 87;  
					yyah[14] = 87;  
					yyah[15] = 108;  
					yyah[16] = 108;  
					yyah[17] = 109;  
					yyah[18] = 112;  
					yyah[19] = 112;  
					yyah[20] = 115;  
					yyah[21] = 118;  
					yyah[22] = 121;  
					yyah[23] = 122;  
					yyah[24] = 122;  
					yyah[25] = 122;  
					yyah[26] = 122;  
					yyah[27] = 122;  
					yyah[28] = 122;  
					yyah[29] = 122;  
					yyah[30] = 122;  
					yyah[31] = 122;  
					yyah[32] = 122;  
					yyah[33] = 122;  
					yyah[34] = 122;  
					yyah[35] = 122;  
					yyah[36] = 122;  
					yyah[37] = 122;  
					yyah[38] = 124;  
					yyah[39] = 124;  
					yyah[40] = 124;  
					yyah[41] = 124;  
					yyah[42] = 124;  
					yyah[43] = 124;  
					yyah[44] = 124;  
					yyah[45] = 124;  
					yyah[46] = 124;  
					yyah[47] = 124;  
					yyah[48] = 124;  
					yyah[49] = 124;  
					yyah[50] = 124;  
					yyah[51] = 124;  
					yyah[52] = 124;  
					yyah[53] = 124;  
					yyah[54] = 128;  
					yyah[55] = 130;  
					yyah[56] = 130;  
					yyah[57] = 130;  
					yyah[58] = 138;  
					yyah[59] = 138;  
					yyah[60] = 152;  
					yyah[61] = 152;  
					yyah[62] = 152;  
					yyah[63] = 152;  
					yyah[64] = 153;  
					yyah[65] = 153;  
					yyah[66] = 154;  
					yyah[67] = 156;  
					yyah[68] = 157;  
					yyah[69] = 158;  
					yyah[70] = 158;  
					yyah[71] = 159;  
					yyah[72] = 161;  
					yyah[73] = 161;  
					yyah[74] = 162;  
					yyah[75] = 162;  
					yyah[76] = 164;  
					yyah[77] = 164;  
					yyah[78] = 165;  
					yyah[79] = 165;  
					yyah[80] = 165;  
					yyah[81] = 165;  
					yyah[82] = 165;  
					yyah[83] = 165;  
					yyah[84] = 165;  
					yyah[85] = 165;  
					yyah[86] = 165;  
					yyah[87] = 165;  
					yyah[88] = 165;  
					yyah[89] = 181;  
					yyah[90] = 182;  
					yyah[91] = 202;  
					yyah[92] = 222;  
					yyah[93] = 222;  
					yyah[94] = 251;  
					yyah[95] = 251;  
					yyah[96] = 251;  
					yyah[97] = 281;  
					yyah[98] = 282;  
					yyah[99] = 295;  
					yyah[100] = 301;  
					yyah[101] = 301;  
					yyah[102] = 301;  
					yyah[103] = 301;  
					yyah[104] = 316;  
					yyah[105] = 316;  
					yyah[106] = 317;  
					yyah[107] = 319;  
					yyah[108] = 320;  
					yyah[109] = 373;  
					yyah[110] = 373;  
					yyah[111] = 426;  
					yyah[112] = 426;  
					yyah[113] = 426;  
					yyah[114] = 426;  
					yyah[115] = 426;  
					yyah[116] = 426;  
					yyah[117] = 426;  
					yyah[118] = 427;  
					yyah[119] = 427;  
					yyah[120] = 427;  
					yyah[121] = 427;  
					yyah[122] = 427;  
					yyah[123] = 465;  
					yyah[124] = 496;  
					yyah[125] = 497;  
					yyah[126] = 529;  
					yyah[127] = 561;  
					yyah[128] = 562;  
					yyah[129] = 569;  
					yyah[130] = 574;  
					yyah[131] = 575;  
					yyah[132] = 607;  
					yyah[133] = 610;  
					yyah[134] = 610;  
					yyah[135] = 613;  
					yyah[136] = 643;  
					yyah[137] = 670;  
					yyah[138] = 702;  
					yyah[139] = 729;  
					yyah[140] = 756;  
					yyah[141] = 763;  
					yyah[142] = 801;  
					yyah[143] = 809;  
					yyah[144] = 809;  
					yyah[145] = 837;  
					yyah[146] = 838;  
					yyah[147] = 838;  
					yyah[148] = 838;  
					yyah[149] = 838;  
					yyah[150] = 839;  
					yyah[151] = 854;  
					yyah[152] = 857;  
					yyah[153] = 860;  
					yyah[154] = 860;  
					yyah[155] = 860;  
					yyah[156] = 860;  
					yyah[157] = 860;  
					yyah[158] = 860;  
					yyah[159] = 860;  
					yyah[160] = 860;  
					yyah[161] = 865;  
					yyah[162] = 865;  
					yyah[163] = 865;  
					yyah[164] = 866;  
					yyah[165] = 866;  
					yyah[166] = 866;  
					yyah[167] = 880;  
					yyah[168] = 894;  
					yyah[169] = 894;  
					yyah[170] = 923;  
					yyah[171] = 923;  
					yyah[172] = 923;  
					yyah[173] = 923;  
					yyah[174] = 923;  
					yyah[175] = 955;  
					yyah[176] = 982;  
					yyah[177] = 982;  
					yyah[178] = 982;  
					yyah[179] = 982;  
					yyah[180] = 982;  
					yyah[181] = 982;  
					yyah[182] = 982;  
					yyah[183] = 982;  
					yyah[184] = 1013;  
					yyah[185] = 1044;  
					yyah[186] = 1045;  
					yyah[187] = 1045;  
					yyah[188] = 1071;  
					yyah[189] = 1098;  
					yyah[190] = 1098;  
					yyah[191] = 1098;  
					yyah[192] = 1117;  
					yyah[193] = 1133;  
					yyah[194] = 1147;  
					yyah[195] = 1157;  
					yyah[196] = 1165;  
					yyah[197] = 1172;  
					yyah[198] = 1178;  
					yyah[199] = 1183;  
					yyah[200] = 1187;  
					yyah[201] = 1187;  
					yyah[202] = 1209;  
					yyah[203] = 1236;  
					yyah[204] = 1236;  
					yyah[205] = 1236;  
					yyah[206] = 1266;  
					yyah[207] = 1267;  
					yyah[208] = 1267;  
					yyah[209] = 1268;  
					yyah[210] = 1300;  
					yyah[211] = 1327;  
					yyah[212] = 1342;  
					yyah[213] = 1342;  
					yyah[214] = 1342;  
					yyah[215] = 1343;  
					yyah[216] = 1344;  
					yyah[217] = 1345;  
					yyah[218] = 1347;  
					yyah[219] = 1348;  
					yyah[220] = 1367;  
					yyah[221] = 1367;  
					yyah[222] = 1367;  
					yyah[223] = 1367;  
					yyah[224] = 1367;  
					yyah[225] = 1367;  
					yyah[226] = 1367;  
					yyah[227] = 1367;  
					yyah[228] = 1367;  
					yyah[229] = 1398;  
					yyah[230] = 1399;  
					yyah[231] = 1399;  
					yyah[232] = 1399;  
					yyah[233] = 1399;  
					yyah[234] = 1399;  
					yyah[235] = 1399;  
					yyah[236] = 1399;  
					yyah[237] = 1399;  
					yyah[238] = 1401;  
					yyah[239] = 1401;  
					yyah[240] = 1401;  
					yyah[241] = 1433;  
					yyah[242] = 1460;  
					yyah[243] = 1460;  
					yyah[244] = 1487;  
					yyah[245] = 1487;  
					yyah[246] = 1487;  
					yyah[247] = 1487;  
					yyah[248] = 1514;  
					yyah[249] = 1514;  
					yyah[250] = 1514;  
					yyah[251] = 1541;  
					yyah[252] = 1541;  
					yyah[253] = 1541;  
					yyah[254] = 1541;  
					yyah[255] = 1541;  
					yyah[256] = 1568;  
					yyah[257] = 1568;  
					yyah[258] = 1568;  
					yyah[259] = 1595;  
					yyah[260] = 1622;  
					yyah[261] = 1649;  
					yyah[262] = 1676;  
					yyah[263] = 1703;  
					yyah[264] = 1730;  
					yyah[265] = 1731;  
					yyah[266] = 1732;  
					yyah[267] = 1762;  
					yyah[268] = 1792;  
					yyah[269] = 1792;  
					yyah[270] = 1792;  
					yyah[271] = 1822;  
					yyah[272] = 1851;  
					yyah[273] = 1851;  
					yyah[274] = 1851;  
					yyah[275] = 1881;  
					yyah[276] = 1881;  
					yyah[277] = 1881;  
					yyah[278] = 1881;  
					yyah[279] = 1881;  
					yyah[280] = 1895;  
					yyah[281] = 1909;  
					yyah[282] = 1914;  
					yyah[283] = 1914;  
					yyah[284] = 1915;  
					yyah[285] = 1945;  
					yyah[286] = 1945;  
					yyah[287] = 1975;  
					yyah[288] = 1975;  
					yyah[289] = 1975;  
					yyah[290] = 1976;  
					yyah[291] = 1976;  
					yyah[292] = 1995;  
					yyah[293] = 2011;  
					yyah[294] = 2025;  
					yyah[295] = 2035;  
					yyah[296] = 2043;  
					yyah[297] = 2050;  
					yyah[298] = 2056;  
					yyah[299] = 2061;  
					yyah[300] = 2061;  
					yyah[301] = 2061;  
					yyah[302] = 2061;  
					yyah[303] = 2062;  
					yyah[304] = 2063;  
					yyah[305] = 2077;  
					yyah[306] = 2105;  
					yyah[307] = 2119;  
					yyah[308] = 2121;  
					yyah[309] = 2122;  
					yyah[310] = 2123;  
					yyah[311] = 2123;  
					yyah[312] = 2123;  
					yyah[313] = 2123;  
					yyah[314] = 2124;  
					yyah[315] = 2124;  
					yyah[316] = 2124;  
					yyah[317] = 2124;  
					yyah[318] = 2124;  
					yyah[319] = 2124;  
					yyah[320] = 2124;  
					yyah[321] = 2124;  
					yyah[322] = 2124;  
					yyah[323] = 2125;  
					yyah[324] = 2125;  
					yyah[325] = 2125;  
					yyah[326] = 2125;  
					yyah[327] = 2125;  
					yyah[328] = 2138;  
					yyah[329] = 2139;  
					yyah[330] = 2139; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 18;  
					yygl[2] = 25;  
					yygl[3] = 25;  
					yygl[4] = 30;  
					yygl[5] = 36;  
					yygl[6] = 37;  
					yygl[7] = 38;  
					yygl[8] = 38;  
					yygl[9] = 38;  
					yygl[10] = 38;  
					yygl[11] = 38;  
					yygl[12] = 38;  
					yygl[13] = 38;  
					yygl[14] = 38;  
					yygl[15] = 38;  
					yygl[16] = 54;  
					yygl[17] = 54;  
					yygl[18] = 54;  
					yygl[19] = 57;  
					yygl[20] = 57;  
					yygl[21] = 60;  
					yygl[22] = 63;  
					yygl[23] = 66;  
					yygl[24] = 67;  
					yygl[25] = 67;  
					yygl[26] = 67;  
					yygl[27] = 67;  
					yygl[28] = 67;  
					yygl[29] = 67;  
					yygl[30] = 67;  
					yygl[31] = 67;  
					yygl[32] = 67;  
					yygl[33] = 67;  
					yygl[34] = 67;  
					yygl[35] = 67;  
					yygl[36] = 67;  
					yygl[37] = 67;  
					yygl[38] = 67;  
					yygl[39] = 68;  
					yygl[40] = 68;  
					yygl[41] = 68;  
					yygl[42] = 68;  
					yygl[43] = 68;  
					yygl[44] = 68;  
					yygl[45] = 68;  
					yygl[46] = 68;  
					yygl[47] = 68;  
					yygl[48] = 68;  
					yygl[49] = 68;  
					yygl[50] = 68;  
					yygl[51] = 68;  
					yygl[52] = 68;  
					yygl[53] = 68;  
					yygl[54] = 68;  
					yygl[55] = 69;  
					yygl[56] = 70;  
					yygl[57] = 70;  
					yygl[58] = 70;  
					yygl[59] = 78;  
					yygl[60] = 78;  
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
					yygl[73] = 85;  
					yygl[74] = 85;  
					yygl[75] = 86;  
					yygl[76] = 86;  
					yygl[77] = 86;  
					yygl[78] = 86;  
					yygl[79] = 86;  
					yygl[80] = 86;  
					yygl[81] = 86;  
					yygl[82] = 86;  
					yygl[83] = 86;  
					yygl[84] = 86;  
					yygl[85] = 86;  
					yygl[86] = 86;  
					yygl[87] = 86;  
					yygl[88] = 86;  
					yygl[89] = 86;  
					yygl[90] = 87;  
					yygl[91] = 87;  
					yygl[92] = 104;  
					yygl[93] = 121;  
					yygl[94] = 121;  
					yygl[95] = 139;  
					yygl[96] = 139;  
					yygl[97] = 139;  
					yygl[98] = 156;  
					yygl[99] = 156;  
					yygl[100] = 163;  
					yygl[101] = 164;  
					yygl[102] = 164;  
					yygl[103] = 164;  
					yygl[104] = 164;  
					yygl[105] = 170;  
					yygl[106] = 170;  
					yygl[107] = 170;  
					yygl[108] = 170;  
					yygl[109] = 170;  
					yygl[110] = 170;  
					yygl[111] = 170;  
					yygl[112] = 170;  
					yygl[113] = 170;  
					yygl[114] = 170;  
					yygl[115] = 170;  
					yygl[116] = 170;  
					yygl[117] = 170;  
					yygl[118] = 170;  
					yygl[119] = 170;  
					yygl[120] = 170;  
					yygl[121] = 170;  
					yygl[122] = 170;  
					yygl[123] = 170;  
					yygl[124] = 170;  
					yygl[125] = 171;  
					yygl[126] = 171;  
					yygl[127] = 188;  
					yygl[128] = 205;  
					yygl[129] = 205;  
					yygl[130] = 205;  
					yygl[131] = 206;  
					yygl[132] = 206;  
					yygl[133] = 223;  
					yygl[134] = 226;  
					yygl[135] = 226;  
					yygl[136] = 229;  
					yygl[137] = 246;  
					yygl[138] = 270;  
					yygl[139] = 270;  
					yygl[140] = 294;  
					yygl[141] = 318;  
					yygl[142] = 318;  
					yygl[143] = 318;  
					yygl[144] = 318;  
					yygl[145] = 318;  
					yygl[146] = 319;  
					yygl[147] = 319;  
					yygl[148] = 319;  
					yygl[149] = 319;  
					yygl[150] = 319;  
					yygl[151] = 319;  
					yygl[152] = 326;  
					yygl[153] = 329;  
					yygl[154] = 332;  
					yygl[155] = 332;  
					yygl[156] = 332;  
					yygl[157] = 332;  
					yygl[158] = 332;  
					yygl[159] = 332;  
					yygl[160] = 332;  
					yygl[161] = 332;  
					yygl[162] = 335;  
					yygl[163] = 335;  
					yygl[164] = 335;  
					yygl[165] = 335;  
					yygl[166] = 335;  
					yygl[167] = 335;  
					yygl[168] = 340;  
					yygl[169] = 345;  
					yygl[170] = 345;  
					yygl[171] = 364;  
					yygl[172] = 364;  
					yygl[173] = 364;  
					yygl[174] = 364;  
					yygl[175] = 364;  
					yygl[176] = 381;  
					yygl[177] = 405;  
					yygl[178] = 405;  
					yygl[179] = 405;  
					yygl[180] = 405;  
					yygl[181] = 405;  
					yygl[182] = 405;  
					yygl[183] = 405;  
					yygl[184] = 405;  
					yygl[185] = 423;  
					yygl[186] = 441;  
					yygl[187] = 441;  
					yygl[188] = 441;  
					yygl[189] = 441;  
					yygl[190] = 455;  
					yygl[191] = 455;  
					yygl[192] = 455;  
					yygl[193] = 456;  
					yygl[194] = 457;  
					yygl[195] = 458;  
					yygl[196] = 459;  
					yygl[197] = 459;  
					yygl[198] = 459;  
					yygl[199] = 459;  
					yygl[200] = 459;  
					yygl[201] = 459;  
					yygl[202] = 459;  
					yygl[203] = 460;  
					yygl[204] = 484;  
					yygl[205] = 484;  
					yygl[206] = 484;  
					yygl[207] = 501;  
					yygl[208] = 501;  
					yygl[209] = 501;  
					yygl[210] = 501;  
					yygl[211] = 518;  
					yygl[212] = 535;  
					yygl[213] = 542;  
					yygl[214] = 542;  
					yygl[215] = 542;  
					yygl[216] = 542;  
					yygl[217] = 542;  
					yygl[218] = 542;  
					yygl[219] = 542;  
					yygl[220] = 542;  
					yygl[221] = 558;  
					yygl[222] = 558;  
					yygl[223] = 558;  
					yygl[224] = 558;  
					yygl[225] = 558;  
					yygl[226] = 558;  
					yygl[227] = 558;  
					yygl[228] = 558;  
					yygl[229] = 558;  
					yygl[230] = 559;  
					yygl[231] = 559;  
					yygl[232] = 559;  
					yygl[233] = 559;  
					yygl[234] = 559;  
					yygl[235] = 559;  
					yygl[236] = 559;  
					yygl[237] = 559;  
					yygl[238] = 559;  
					yygl[239] = 559;  
					yygl[240] = 559;  
					yygl[241] = 559;  
					yygl[242] = 576;  
					yygl[243] = 600;  
					yygl[244] = 600;  
					yygl[245] = 614;  
					yygl[246] = 614;  
					yygl[247] = 614;  
					yygl[248] = 614;  
					yygl[249] = 629;  
					yygl[250] = 629;  
					yygl[251] = 629;  
					yygl[252] = 645;  
					yygl[253] = 645;  
					yygl[254] = 645;  
					yygl[255] = 645;  
					yygl[256] = 645;  
					yygl[257] = 662;  
					yygl[258] = 662;  
					yygl[259] = 662;  
					yygl[260] = 680;  
					yygl[261] = 699;  
					yygl[262] = 719;  
					yygl[263] = 740;  
					yygl[264] = 762;  
					yygl[265] = 786;  
					yygl[266] = 786;  
					yygl[267] = 786;  
					yygl[268] = 803;  
					yygl[269] = 820;  
					yygl[270] = 820;  
					yygl[271] = 820;  
					yygl[272] = 820;  
					yygl[273] = 821;  
					yygl[274] = 821;  
					yygl[275] = 821;  
					yygl[276] = 821;  
					yygl[277] = 821;  
					yygl[278] = 821;  
					yygl[279] = 821;  
					yygl[280] = 821;  
					yygl[281] = 829;  
					yygl[282] = 837;  
					yygl[283] = 840;  
					yygl[284] = 840;  
					yygl[285] = 840;  
					yygl[286] = 859;  
					yygl[287] = 859;  
					yygl[288] = 876;  
					yygl[289] = 876;  
					yygl[290] = 876;  
					yygl[291] = 876;  
					yygl[292] = 876;  
					yygl[293] = 877;  
					yygl[294] = 878;  
					yygl[295] = 879;  
					yygl[296] = 880;  
					yygl[297] = 880;  
					yygl[298] = 880;  
					yygl[299] = 880;  
					yygl[300] = 880;  
					yygl[301] = 880;  
					yygl[302] = 880;  
					yygl[303] = 880;  
					yygl[304] = 880;  
					yygl[305] = 880;  
					yygl[306] = 885;  
					yygl[307] = 902;  
					yygl[308] = 907;  
					yygl[309] = 907;  
					yygl[310] = 907;  
					yygl[311] = 907;  
					yygl[312] = 907;  
					yygl[313] = 907;  
					yygl[314] = 907;  
					yygl[315] = 907;  
					yygl[316] = 907;  
					yygl[317] = 907;  
					yygl[318] = 907;  
					yygl[319] = 907;  
					yygl[320] = 907;  
					yygl[321] = 907;  
					yygl[322] = 907;  
					yygl[323] = 907;  
					yygl[324] = 907;  
					yygl[325] = 907;  
					yygl[326] = 907;  
					yygl[327] = 907;  
					yygl[328] = 907;  
					yygl[329] = 914;  
					yygl[330] = 914; 

					yygh = new int[yynstates];
					yygh[0] = 17;  
					yygh[1] = 24;  
					yygh[2] = 24;  
					yygh[3] = 29;  
					yygh[4] = 35;  
					yygh[5] = 36;  
					yygh[6] = 37;  
					yygh[7] = 37;  
					yygh[8] = 37;  
					yygh[9] = 37;  
					yygh[10] = 37;  
					yygh[11] = 37;  
					yygh[12] = 37;  
					yygh[13] = 37;  
					yygh[14] = 37;  
					yygh[15] = 53;  
					yygh[16] = 53;  
					yygh[17] = 53;  
					yygh[18] = 56;  
					yygh[19] = 56;  
					yygh[20] = 59;  
					yygh[21] = 62;  
					yygh[22] = 65;  
					yygh[23] = 66;  
					yygh[24] = 66;  
					yygh[25] = 66;  
					yygh[26] = 66;  
					yygh[27] = 66;  
					yygh[28] = 66;  
					yygh[29] = 66;  
					yygh[30] = 66;  
					yygh[31] = 66;  
					yygh[32] = 66;  
					yygh[33] = 66;  
					yygh[34] = 66;  
					yygh[35] = 66;  
					yygh[36] = 66;  
					yygh[37] = 66;  
					yygh[38] = 67;  
					yygh[39] = 67;  
					yygh[40] = 67;  
					yygh[41] = 67;  
					yygh[42] = 67;  
					yygh[43] = 67;  
					yygh[44] = 67;  
					yygh[45] = 67;  
					yygh[46] = 67;  
					yygh[47] = 67;  
					yygh[48] = 67;  
					yygh[49] = 67;  
					yygh[50] = 67;  
					yygh[51] = 67;  
					yygh[52] = 67;  
					yygh[53] = 67;  
					yygh[54] = 68;  
					yygh[55] = 69;  
					yygh[56] = 69;  
					yygh[57] = 69;  
					yygh[58] = 77;  
					yygh[59] = 77;  
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
					yygh[72] = 84;  
					yygh[73] = 84;  
					yygh[74] = 85;  
					yygh[75] = 85;  
					yygh[76] = 85;  
					yygh[77] = 85;  
					yygh[78] = 85;  
					yygh[79] = 85;  
					yygh[80] = 85;  
					yygh[81] = 85;  
					yygh[82] = 85;  
					yygh[83] = 85;  
					yygh[84] = 85;  
					yygh[85] = 85;  
					yygh[86] = 85;  
					yygh[87] = 85;  
					yygh[88] = 85;  
					yygh[89] = 86;  
					yygh[90] = 86;  
					yygh[91] = 103;  
					yygh[92] = 120;  
					yygh[93] = 120;  
					yygh[94] = 138;  
					yygh[95] = 138;  
					yygh[96] = 138;  
					yygh[97] = 155;  
					yygh[98] = 155;  
					yygh[99] = 162;  
					yygh[100] = 163;  
					yygh[101] = 163;  
					yygh[102] = 163;  
					yygh[103] = 163;  
					yygh[104] = 169;  
					yygh[105] = 169;  
					yygh[106] = 169;  
					yygh[107] = 169;  
					yygh[108] = 169;  
					yygh[109] = 169;  
					yygh[110] = 169;  
					yygh[111] = 169;  
					yygh[112] = 169;  
					yygh[113] = 169;  
					yygh[114] = 169;  
					yygh[115] = 169;  
					yygh[116] = 169;  
					yygh[117] = 169;  
					yygh[118] = 169;  
					yygh[119] = 169;  
					yygh[120] = 169;  
					yygh[121] = 169;  
					yygh[122] = 169;  
					yygh[123] = 169;  
					yygh[124] = 170;  
					yygh[125] = 170;  
					yygh[126] = 187;  
					yygh[127] = 204;  
					yygh[128] = 204;  
					yygh[129] = 204;  
					yygh[130] = 205;  
					yygh[131] = 205;  
					yygh[132] = 222;  
					yygh[133] = 225;  
					yygh[134] = 225;  
					yygh[135] = 228;  
					yygh[136] = 245;  
					yygh[137] = 269;  
					yygh[138] = 269;  
					yygh[139] = 293;  
					yygh[140] = 317;  
					yygh[141] = 317;  
					yygh[142] = 317;  
					yygh[143] = 317;  
					yygh[144] = 317;  
					yygh[145] = 318;  
					yygh[146] = 318;  
					yygh[147] = 318;  
					yygh[148] = 318;  
					yygh[149] = 318;  
					yygh[150] = 318;  
					yygh[151] = 325;  
					yygh[152] = 328;  
					yygh[153] = 331;  
					yygh[154] = 331;  
					yygh[155] = 331;  
					yygh[156] = 331;  
					yygh[157] = 331;  
					yygh[158] = 331;  
					yygh[159] = 331;  
					yygh[160] = 331;  
					yygh[161] = 334;  
					yygh[162] = 334;  
					yygh[163] = 334;  
					yygh[164] = 334;  
					yygh[165] = 334;  
					yygh[166] = 334;  
					yygh[167] = 339;  
					yygh[168] = 344;  
					yygh[169] = 344;  
					yygh[170] = 363;  
					yygh[171] = 363;  
					yygh[172] = 363;  
					yygh[173] = 363;  
					yygh[174] = 363;  
					yygh[175] = 380;  
					yygh[176] = 404;  
					yygh[177] = 404;  
					yygh[178] = 404;  
					yygh[179] = 404;  
					yygh[180] = 404;  
					yygh[181] = 404;  
					yygh[182] = 404;  
					yygh[183] = 404;  
					yygh[184] = 422;  
					yygh[185] = 440;  
					yygh[186] = 440;  
					yygh[187] = 440;  
					yygh[188] = 440;  
					yygh[189] = 454;  
					yygh[190] = 454;  
					yygh[191] = 454;  
					yygh[192] = 455;  
					yygh[193] = 456;  
					yygh[194] = 457;  
					yygh[195] = 458;  
					yygh[196] = 458;  
					yygh[197] = 458;  
					yygh[198] = 458;  
					yygh[199] = 458;  
					yygh[200] = 458;  
					yygh[201] = 458;  
					yygh[202] = 459;  
					yygh[203] = 483;  
					yygh[204] = 483;  
					yygh[205] = 483;  
					yygh[206] = 500;  
					yygh[207] = 500;  
					yygh[208] = 500;  
					yygh[209] = 500;  
					yygh[210] = 517;  
					yygh[211] = 534;  
					yygh[212] = 541;  
					yygh[213] = 541;  
					yygh[214] = 541;  
					yygh[215] = 541;  
					yygh[216] = 541;  
					yygh[217] = 541;  
					yygh[218] = 541;  
					yygh[219] = 541;  
					yygh[220] = 557;  
					yygh[221] = 557;  
					yygh[222] = 557;  
					yygh[223] = 557;  
					yygh[224] = 557;  
					yygh[225] = 557;  
					yygh[226] = 557;  
					yygh[227] = 557;  
					yygh[228] = 557;  
					yygh[229] = 558;  
					yygh[230] = 558;  
					yygh[231] = 558;  
					yygh[232] = 558;  
					yygh[233] = 558;  
					yygh[234] = 558;  
					yygh[235] = 558;  
					yygh[236] = 558;  
					yygh[237] = 558;  
					yygh[238] = 558;  
					yygh[239] = 558;  
					yygh[240] = 558;  
					yygh[241] = 575;  
					yygh[242] = 599;  
					yygh[243] = 599;  
					yygh[244] = 613;  
					yygh[245] = 613;  
					yygh[246] = 613;  
					yygh[247] = 613;  
					yygh[248] = 628;  
					yygh[249] = 628;  
					yygh[250] = 628;  
					yygh[251] = 644;  
					yygh[252] = 644;  
					yygh[253] = 644;  
					yygh[254] = 644;  
					yygh[255] = 644;  
					yygh[256] = 661;  
					yygh[257] = 661;  
					yygh[258] = 661;  
					yygh[259] = 679;  
					yygh[260] = 698;  
					yygh[261] = 718;  
					yygh[262] = 739;  
					yygh[263] = 761;  
					yygh[264] = 785;  
					yygh[265] = 785;  
					yygh[266] = 785;  
					yygh[267] = 802;  
					yygh[268] = 819;  
					yygh[269] = 819;  
					yygh[270] = 819;  
					yygh[271] = 819;  
					yygh[272] = 820;  
					yygh[273] = 820;  
					yygh[274] = 820;  
					yygh[275] = 820;  
					yygh[276] = 820;  
					yygh[277] = 820;  
					yygh[278] = 820;  
					yygh[279] = 820;  
					yygh[280] = 828;  
					yygh[281] = 836;  
					yygh[282] = 839;  
					yygh[283] = 839;  
					yygh[284] = 839;  
					yygh[285] = 858;  
					yygh[286] = 858;  
					yygh[287] = 875;  
					yygh[288] = 875;  
					yygh[289] = 875;  
					yygh[290] = 875;  
					yygh[291] = 875;  
					yygh[292] = 876;  
					yygh[293] = 877;  
					yygh[294] = 878;  
					yygh[295] = 879;  
					yygh[296] = 879;  
					yygh[297] = 879;  
					yygh[298] = 879;  
					yygh[299] = 879;  
					yygh[300] = 879;  
					yygh[301] = 879;  
					yygh[302] = 879;  
					yygh[303] = 879;  
					yygh[304] = 879;  
					yygh[305] = 884;  
					yygh[306] = 901;  
					yygh[307] = 906;  
					yygh[308] = 906;  
					yygh[309] = 906;  
					yygh[310] = 906;  
					yygh[311] = 906;  
					yygh[312] = 906;  
					yygh[313] = 906;  
					yygh[314] = 906;  
					yygh[315] = 906;  
					yygh[316] = 906;  
					yygh[317] = 906;  
					yygh[318] = 906;  
					yygh[319] = 906;  
					yygh[320] = 906;  
					yygh[321] = 906;  
					yygh[322] = 906;  
					yygh[323] = 906;  
					yygh[324] = 906;  
					yygh[325] = 906;  
					yygh[326] = 906;  
					yygh[327] = 906;  
					yygh[328] = 913;  
					yygh[329] = 913;  
					yygh[330] = 913; 

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
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++;
        }

        public bool yyact(int state, int sym, ref int act)
        {
            int k = yyal[state];
            while (k <= yyah[state] && yya[k].sym != sym) k++;
            if (k > yyah[state]) return false;
            act = yya[k].act;
            return true;
        }
        public bool yygoto(int state, int sym, ref int nstate)
        {
            int k = yygl[state];
            while (k <= yygh[state] && yyg[k].sym != sym) k++;
            if (k > yygh[state]) return false;
            nstate = yyg[k].act;
            return true;
        }

        public void yyerror(string s)
        {
            System.Console.Write(s);
        }

        int yylexpos = -1;
        string yylval = "";

        public int yylex()
        {
            yylexpos++;
            if (yylexpos >= TokenList.Count)
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

        public bool yyparse()
        {

            parse:

            yysp++;
            if (yysp >= yymaxdepth)
            {
                yyerror("yyparse stack overflow");
                goto abort;
            }

            yys[yysp] = yystate;
            yyv[yysp] = yyval;

            next:

            if (yyd[yystate] == 0 && yychar == -1)
            {
                yychar = yylex();
                if (yychar < 0) yychar = 0;
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
                while (yysp > 0 && !(yyact(yys[yysp], 255, ref yyn) && yyn > 0)) yysp--;

                if (yysp == 0) goto abort;
                yystate = yyn;
                goto parse;
            }
            else
            {
                if (yychar == 0) goto abort;
                yychar = -1; goto next;
            }

            shift:

            yystate = yyn;
            yychar = -1;
            yyval = yylval;
            if (yyerrflag > 0) yyerrflag--;
            goto parse;

            reduce:

            yyflag = yyfnone;
            yyaction(-yyn);
            yysp -= yyr[-yyn].len;

            if (yygoto(yys[yysp], yyr[-yyn].sym, ref yyn)) yystate = yyn;

            switch (yyflag)
            {
                case 1: goto accept;
                case 2: goto abort;
                case 3: goto errlab;
            }

            goto parse;

            accept:

            return true;

            abort:

            return false;
        }
        ////////////////////////////////////////////////////////////////
        /// Scanner - Optimized
        ////////////////////////////////////////////////////////////////

        public bool ScannerOpt(string Input)
        {
            if (Input.Length == 0) return true;
            TokenList = new ArrayList();
            while (1 == 1)
            {
                AToken lasttoken = FindTokenOpt(Input);
                if (lasttoken.token == 0) break;
                if (lasttoken.token != t_ignore) TokenList.Add(lasttoken);
                if (Input.Length > lasttoken.val.Length)
                    Input = Input.Substring(lasttoken.val.Length);
                else return true;
            }
            System.Console.WriteLine(Input);
            System.Console.WriteLine();
            System.Console.WriteLine("No matching token found!");
            return false;
        }
        public AToken FindTokenOpt(string Rest)
        {
            ArrayList Results = new ArrayList();
            ArrayList ResultsV = new ArrayList();
            Match m;
            try
            {

                for (int idx = 0; idx < tList.Count; idx++)
                {
                    m = rList[idx].Match(Rest);
                    if (m.Success)
                    {
                        Results.Add(tList[idx]);
                        ResultsV.Add(m.Value);
                    }
                }

            }
            catch { }
            int maxlength = 0;
            int besttoken = 0;
            AToken ret = new AToken();
            ret.token = besttoken;
            for (int i = 0; i < Results.Count; i++)
            {
                if (ResultsV[i].ToString().Length > maxlength)
                {
                    maxlength = ResultsV[i].ToString().Length;
                    besttoken = (int)Results[i];
                    ret.token = besttoken;
                    if (besttoken != 0)
                        ret.val = ResultsV[i].ToString();
                }
            }
            return ret;
        }
        public static string SubScanner(string file)
        {
            StreamReader in_s = File.OpenText(file);
            string inputstream = in_s.ReadToEnd();
            in_s.Close();

            MyCompiler compiler = new MyCompiler();
            StringBuilder sb = new StringBuilder();
            compiler.Output = new StringWriter(sb);
            if (!compiler.ScannerOpt(inputstream))
                return string.Empty;
            compiler.InitTables();
            if (!compiler.yyparse())
                return string.Empty;

            if (compiler.Output != null)
                compiler.Output.Close();
            return sb.ToString();
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

			if (Regex.IsMatch(Rest,"^((,*[\\s\\t\\x00]*)?;)")){
				Results.Add (t_Char59);
				ResultsV.Add(Regex.Match(Rest,"^((,*[\\s\\t\\x00]*)?;)").Value);}

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

			if (Regex.IsMatch(Rest,"^(,|:=)")){
				Results.Add (t_Char44);
				ResultsV.Add(Regex.Match(Rest,"^(,|:=)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)UNDEF)")){
				Results.Add (t_UNDEF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)UNDEF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)INCLUDE)")){
				Results.Add (t_INCLUDE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)INCLUDE)").Value);}

			if (Regex.IsMatch(Rest,"^(\\{)")){
				Results.Add (t_Char123);
				ResultsV.Add(Regex.Match(Rest,"^(\\{)").Value);}

			if (Regex.IsMatch(Rest,"^(\\})")){
				Results.Add (t_Char125);
				ResultsV.Add(Regex.Match(Rest,"^(\\})").Value);}

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

			if (Regex.IsMatch(Rest,"^(\\*[\\s\\t\\x00]*=)")){
				Results.Add (t_Char42Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\*[\\s\\t\\x00]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\+[\\s\\t\\x00]*=)")){
				Results.Add (t_Char43Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\+[\\s\\t\\x00]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\-[\\s\\t\\x00]*=)")){
				Results.Add (t_Char45Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\-[\\s\\t\\x00]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(/[\\s\\t\\x00]*=)")){
				Results.Add (t_Char47Char61);
				ResultsV.Add(Regex.Match(Rest,"^(/[\\s\\t\\x00]*=)").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)(EACH_SEC|IF_(ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))")){
				Results.Add (t_event);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(EACH_SEC|IF_(ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER|SAVE_KEYS|REMOTE_KEYS))")){
				Results.Add (t_global);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER|SAVE_KEYS|REMOTE_KEYS))").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))")){
				Results.Add (t_flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_C|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))")){
				Results.Add (t_property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_C|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))")){
				Results.Add (t_command);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))")){
				Results.Add (t_list);
				ResultsV.Add(Regex.Match(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))")){
				Results.Add (t_skill);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT|NODE))")){
				Results.Add (t_ambigChar95skillChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT|NODE))").Value);}

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

			if (Regex.IsMatch(Rest,"^([A-Za-z0-9_][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)")){
				Results.Add (t_identifier);
				ResultsV.Add(Regex.Match(Rest,"^([A-Za-z0-9_][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)").Value);}

			if (Regex.IsMatch(Rest,"^(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)")){
				Results.Add (t_file);
				ResultsV.Add(Regex.Match(Rest,"^(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)").Value);}

			if (Regex.IsMatch(Rest,"^(\"(\\\\\"|.|[\\r\\n])*?\")")){
				Results.Add (t_string);
				ResultsV.Add(Regex.Match(Rest,"^(\"(\\\\\"|.|[\\r\\n])*?\")").Value);}

			if (Regex.IsMatch(Rest,"^([\\r\\n\\t\\s\\x00]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))")){
				Results.Add (t_ignore);
				ResultsV.Add(Regex.Match(Rest,"^([\\r\\n\\t\\s\\x00]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))").Value);}

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
