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
            rList.Add(new Regex("\\G((?i)IFDEF)"));
            tList.Add(t_Char59);
            rList.Add(new Regex("\\G((,*[\\s\\t\\x00]*)?;)"));
            tList.Add(t_IFNDEF);
            rList.Add(new Regex("\\G((?i)IFNDEF)"));
            tList.Add(t_IFELSE);
            rList.Add(new Regex("\\G((?i)IFELSE)"));
            tList.Add(t_ENDIF);
            rList.Add(new Regex("\\G((?i)ENDIF)"));
            tList.Add(t_DEFINE);
            rList.Add(new Regex("\\G((?i)DEFINE)"));
            tList.Add(t_Char44);
            rList.Add(new Regex("\\G((:=|(,+[\\s\\t\\x00]*)*))"));
            tList.Add(t_UNDEF);
            rList.Add(new Regex("\\G((?i)UNDEF)"));
            tList.Add(t_INCLUDE);
            rList.Add(new Regex("\\G((?i)INCLUDE)"));
            tList.Add(t_Char123);
            rList.Add(new Regex("\\G(\\{)"));
            tList.Add(t_Char125);
            rList.Add(new Regex("\\G(\\})"));
            tList.Add(t_Char58);
            rList.Add(new Regex("\\G(:)"));
            tList.Add(t_Char124Char124);
            rList.Add(new Regex("\\G(\\|\\|)"));
            tList.Add(t_Char38Char38);
            rList.Add(new Regex("\\G(&&)"));
            tList.Add(t_Char124);
            rList.Add(new Regex("\\G(\\|)"));
            tList.Add(t_Char94);
            rList.Add(new Regex("\\G(\\^)"));
            tList.Add(t_Char38);
            rList.Add(new Regex("\\G(&)"));
            tList.Add(t_Char40);
            rList.Add(new Regex("\\G(\\()"));
            tList.Add(t_Char41);
            rList.Add(new Regex("\\G(\\))"));
            tList.Add(t_Char33Char61);
            rList.Add(new Regex("\\G(!=)"));
            tList.Add(t_Char61Char61);
            rList.Add(new Regex("\\G(==)"));
            tList.Add(t_Char60);
            rList.Add(new Regex("\\G(<)"));
            tList.Add(t_Char60Char61);
            rList.Add(new Regex("\\G(<=)"));
            tList.Add(t_Char62);
            rList.Add(new Regex("\\G(>)"));
            tList.Add(t_Char62Char61);
            rList.Add(new Regex("\\G(>=)"));
            tList.Add(t_Char43);
            rList.Add(new Regex("\\G(\\+)"));
            tList.Add(t_Char45);
            rList.Add(new Regex("\\G(\\-)"));
            tList.Add(t_Char37);
            rList.Add(new Regex("\\G(%)"));
            tList.Add(t_Char42);
            rList.Add(new Regex("\\G(\\*)"));
            tList.Add(t_Char47);
            rList.Add(new Regex("\\G(/)"));
            tList.Add(t_Char33);
            rList.Add(new Regex("\\G(!)"));
            tList.Add(t_RULE);
            rList.Add(new Regex("\\G((?i)RULE)"));
            tList.Add(t_Char42Char61);
            rList.Add(new Regex("\\G(\\*[\\s\\t\\x00]*=)"));
            tList.Add(t_Char43Char61);
            rList.Add(new Regex("\\G(\\+[\\s\\t\\x00]*=)"));
            tList.Add(t_Char45Char61);
            rList.Add(new Regex("\\G(\\-[\\s\\t\\x00]*=)"));
            tList.Add(t_Char47Char61);
            rList.Add(new Regex("\\G(/[\\s\\t\\x00]*=)"));
            tList.Add(t_Char61);
            rList.Add(new Regex("\\G(=)"));
            tList.Add(t_ELSE);
            rList.Add(new Regex("\\G((?i)ELSE)"));
            tList.Add(t_IF);
            rList.Add(new Regex("\\G((?i)IF)"));
            tList.Add(t_WHILE);
            rList.Add(new Regex("\\G((?i)WHILE)"));
            tList.Add(t_Char46);
            rList.Add(new Regex("\\G(\\.)"));
            tList.Add(t_NULL);
            rList.Add(new Regex("\\G((?i)NULL)"));
            tList.Add(t_event);
            rList.Add(new Regex("\\G((?i)(EACH_SEC|IF_(AE|ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|OE|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|UE|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))"));
            tList.Add(t_global);
            rList.Add(new Regex("\\G((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER|SAVE_KEYS|REMOTE_KEYS))"));
            tList.Add(t_asset);
            rList.Add(new Regex("\\G((?i)(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))"));
            tList.Add(t_object);
            rList.Add(new Regex("\\G((?i)(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))"));
            tList.Add(t_function);
            rList.Add(new Regex("\\G((?i)(ACTION|RULES))"));
            tList.Add(t_math);
            rList.Add(new Regex("\\G((?i)(ACOS|COS|ATAN|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))"));
            tList.Add(t_flag);
            rList.Add(new Regex("\\G((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))"));
            tList.Add(t_property);
            rList.Add(new Regex("\\G((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_(C|F)|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FOOT_HGT|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SCALE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))"));
            tList.Add(t_command);
            rList.Add(new Regex("\\G((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))"));
            tList.Add(t_list);
            rList.Add(new Regex("\\G((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))"));
            tList.Add(t_skill);
            rList.Add(new Regex("\\G((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|APPEND_MODE|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|RANGE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))"));
            tList.Add(t_synonym);
            rList.Add(new Regex("\\G((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE|REGION[1-8]))"));
            tList.Add(t_ambigChar95globalChar95property);
            rList.Add(new Regex("\\G((?i)CLIP_DIST)"));
            tList.Add(t_ambigChar95eventChar95property);
            rList.Add(new Regex("\\G((?i)(EACH_TICK|IF_ENTER|IF_HIT|IF_KLICK))"));
            tList.Add(t_ambigChar95objectChar95flag);
            rList.Add(new Regex("\\G((?i)(THING|ACTOR))"));
            tList.Add(t_ambigChar95mathChar95command);
            rList.Add(new Regex("\\G((?i)(SIN|ASIN|SQRT|ABS))"));
            tList.Add(t_ambigChar95mathChar95skillChar95property);
            rList.Add(new Regex("\\G((?i)RANDOM)"));
            tList.Add(t_ambigChar95synonymChar95flag);
            rList.Add(new Regex("\\G((?i)HERE)"));
            tList.Add(t_ambigChar95skillChar95property);
            rList.Add(new Regex("\\G((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT|NODE))"));
            tList.Add(t_ambigChar95commandChar95flag);
            rList.Add(new Regex("\\G((?i)SAVE)"));
            tList.Add(t_ambigChar95globalChar95synonymChar95property);
            rList.Add(new Regex("\\G((?i)MSPRITE)"));
            tList.Add(t_ambigChar95commandChar95property);
            rList.Add(new Regex("\\G((?i)DO)"));
            tList.Add(t_ambigChar95skillChar95flag);
            rList.Add(new Regex("\\G((?i)(FLAG[1-8]))"));
            tList.Add(t_integer);
            rList.Add(new Regex("\\G([0-9]+)"));
            tList.Add(t_fixed);
            rList.Add(new Regex("\\G(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))"));
            tList.Add(t_identifier);
            rList.Add(new Regex("\\G([A-Za-z0-9_][A-Za-z0-9_\\?]*(\\.[1-9][0-9]?)?)"));
            tList.Add(t_file);
            rList.Add(new Regex("\\G(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)"));
            tList.Add(t_string);
            rList.Add(new Regex("\\G(\"(\\\\\"|.|[\\r\\n])*?\")"));
            tList.Add(t_ignore);
            rList.Add(new Regex("\\G([\\r\\n\\t\\s\\x00]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))"));
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
        int yymaxdepth = 20000;
        int yyflag = 0;
        int yyfnone = 0;
        int[] yys = new int[20000];
        string[] yyv = new string[20000];

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
                int t_ambigChar95skillChar95flag = 321;
                int t_integer = 322;
                int t_fixed = 323;
                int t_identifier = 324;
                int t_file = 325;
                int t_string = 326;
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
         yyval = ""; //bogus keyword at EOF - just discard
         
       break;
							case    4 : 
         yyval = "";
         
       break;
							case    5 : 
         yyval = "";
         
       break;
							case    6 : 
         yyval = Sections.AddDummySection(yyv[yysp-0]);
         
       break;
							case    7 : 
         yyval = Sections.AddGlobalSection(yyv[yysp-0]);
         
       break;
							case    8 : 
         yyval = yyv[yysp-0];
         
       break;
							case    9 : 
         yyval = yyv[yysp-0];
         
       break;
							case   10 : 
         yyval = Sections.AddActionSection(yyv[yysp-0]);
         
       break;
							case   11 : 
         yyval = Sections.AddObjectSection(yyv[yysp-0]);
         
       break;
							case   12 : 
         yyval = Sections.AddDefineSection(yyv[yysp-0]);
         
       break;
							case   13 : 
         yyval = Sections.AddAssetSection(yyv[yysp-0]);
         
       break;
							case   14 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   15 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   16 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcElseCondition(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   17 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   18 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcIfCondition(yyv[yysp-1], yyv[yysp-0]);
         
       break;
							case   19 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcIfNotCondition(yyv[yysp-1], yyv[yysp-0]);
         
       break;
							case   20 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcElseCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   21 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   22 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   23 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   24 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcElseCondition(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   25 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   26 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.AddTransform(yyv[yysp-3]);
         
       break;
							case   27 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.AddDefine(yyv[yysp-1]);
         
       break;
							case   28 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.RemoveDefine(yyv[yysp-1]);
         
       break;
							case   29 : 
         yyval = Formatter.FormatPreprocessor(yyv[yysp-0]);
         
       break;
							case   30 : 
         yyval = Formatter.FormatPreprocessor(yyv[yysp-0]);
         
       break;
							case   31 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddStringDefine(yyv[yysp-0]);
         
       break;
							case   32 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddListDefine(yyv[yysp-0]);
         
       break;
							case   33 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddFileDefine(yyv[yysp-0]);
         
       break;
							case   34 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddNumberDefine(yyv[yysp-0]);
         
       break;
							case   35 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddKeywordDefine(yyv[yysp-0]);
         
       break;
							case   36 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Include.Process(yyv[yysp-1]);
         
       break;
							case   37 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Globals.AddEvent(yyv[yysp-3]);
         
       break;
							case   38 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Globals.AddGlobal(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   39 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   40 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   41 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   42 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
       break;
							case   43 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   44 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   45 : 
         //yyval = yyv[yysp-1];
         yyval = "";
         Globals.AddParameter(yyv[yysp-1]);
         
       break;
							case   46 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Globals.AddParameter(yyv[yysp-2]);
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   51 : 
         //yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Assets.AddAsset(yyv[yysp-6], yyv[yysp-5], yyv[yysp-3]);
         
       break;
							case   52 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Assets.AddParameter(yyv[yysp-2]);
         
       break;
							case   53 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Assets.AddParameter(yyv[yysp-0]);
         
       break;
							case   54 : 
         yyval = "";
         
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
         //yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddObject(yyv[yysp-5], yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   59 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddStringObject(yyv[yysp-4], yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   60 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddObject(yyv[yysp-2], yyv[yysp-1]);
         
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
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   65 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   66 : 
         yyval = "";
         
       break;
							case   67 : 
         yyval = Objects.CreateProperty(yyv[yysp-3]);
         
       break;
							case   68 : 
         yyval = "";
         
       break;
							case   69 : 
         Objects.AddPropertyValue(yyv[yysp-1]);
         yyval = "";
         
       break;
							case   70 : 
         Objects.AddPropertyValue(yyv[yysp-2]);
         yyval = "";
         
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
         //yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.AddAction(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   76 : 
         yyval = yyv[yysp-0];
         
       break;
							case   77 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   78 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   79 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   80 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   81 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   82 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   83 : 
         yyval = "";
         
       break;
							case   84 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1];
         yyval = Actions.CreateInstruction(yyv[yysp-3]);
         
       break;
							case   85 : 
         //Capture and discard bogus code
         yyval = Actions.CreateInvalidInstruction(yyv[yysp-3]);
         
       break;
							case   86 : 
         yyval = yyv[yysp-1];
         
       break;
							case   87 : 
         //yyval = yyv[yysp-1];
         yyval = Actions.CreateInstruction(yyv[yysp-1]);
         
       break;
							case   88 : 
         yyval = "";
         
       break;
							case   89 : 
         yyval = yyv[yysp-0];
         
       break;
							case   90 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-1]);
         
       break;
							case   91 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-2]);
         
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
         yyval = "";
         
       break;
							case   99 : 
         yyval = yyv[yysp-0];
         
       break;
							case  100 : 
         yyval = yyv[yysp-2] + " || " + yyv[yysp-0];
         
       break;
							case  101 : 
         yyval = yyv[yysp-0];
         
       break;
							case  102 : 
         yyval = yyv[yysp-2] + " && " + yyv[yysp-0];
         
       break;
							case  103 : 
         yyval = yyv[yysp-0];
         
       break;
							case  104 : 
         yyval = yyv[yysp-2] + " | " + yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-2] + " ^ " + yyv[yysp-0];
         
       break;
							case  107 : 
         yyval = yyv[yysp-0];
         
       break;
							case  108 : 
         yyval = yyv[yysp-2] + " & " + yyv[yysp-0];
         
       break;
							case  109 : 
         yyval = yyv[yysp-0];
         
       break;
							case  110 : 
         yyval = yyv[yysp-0];
         
       break;
							case  111 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  112 : 
         yyval = yyv[yysp-0];
         
       break;
							case  113 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  114 : 
         yyval = yyv[yysp-0];
         
       break;
							case  115 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  116 : 
         yyval = yyv[yysp-0];
         
       break;
							case  117 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  118 : 
         yyval = yyv[yysp-0];
         
       break;
							case  119 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  120 : 
         yyval = Formatter.FormatMath(yyv[yysp-3]) + "(" + yyv[yysp-1] + ")";
         
       break;
							case  121 : 
         yyval = "(" + yyv[yysp-1] + ")";
         
       break;
							case  122 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-1] + yyv[yysp-0]); //fixes things like "Skill 6"
         
       break;
							case  123 : 
         yyval = yyv[yysp-0];
         
       break;
							case  124 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
       break;
							case  125 : 
         yyval = yyv[yysp-0];
         
       break;
							case  126 : 
         yyval = " != ";
         
       break;
							case  127 : 
         yyval = " == ";
         
       break;
							case  128 : 
         yyval = " < ";
         
       break;
							case  129 : 
         yyval = " <= ";
         
       break;
							case  130 : 
         yyval = " > ";
         
       break;
							case  131 : 
         yyval = " >= ";
         
       break;
							case  132 : 
         yyval = " + ";
         
       break;
							case  133 : 
         yyval = " - ";
         
       break;
							case  134 : 
         yyval = " % ";
         
       break;
							case  135 : 
         yyval = " * ";
         
       break;
							case  136 : 
         yyval = " / ";
         
       break;
							case  137 : 
         yyval = "!";
         
       break;
							case  138 : 
         yyval = "+";
         
       break;
							case  139 : 
         yyval = "-";
         
       break;
							case  140 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(yyv[yysp-0]);
         
       break;
							case  141 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  142 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  143 : 
         yyval = " *= ";
         
       break;
							case  144 : 
         yyval = " += ";
         
       break;
							case  145 : 
         yyval = " -= ";
         
       break;
							case  146 : 
         yyval = " /= ";
         
       break;
							case  147 : 
         yyval = " = ";
         
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
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateElseCondition(yyv[yysp-1]);
         
       break;
							case  152 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  153 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateWhileCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  154 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  155 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  156 : 
         yyval = yyv[yysp-0];
         
       break;
							case  157 : 
         yyval = yyv[yysp-0];
         
       break;
							case  158 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  159 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  160 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
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
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
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
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  179 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  180 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  181 : 
         yyval = yyv[yysp-0];
         
       break;
							case  182 : 
         yyval = yyv[yysp-0];
         
       break;
							case  183 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  188 : 
         yyval = Formatter.FormatList(yyv[yysp-0]);
         
       break;
							case  189 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  190 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  191 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  192 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  193 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  194 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case  195 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  196 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  197 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  198 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  199 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  200 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case  201 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  202 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  203 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  204 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  205 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  206 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  207 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  208 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  209 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  210 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  211 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  212 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  213 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  214 : 
         yyval = Formatter.FormatNull();
         
       break;
							case  215 : 
         yyval = yyv[yysp-0];
         
       break;
							case  216 : 
         yyval = yyv[yysp-0];
         
       break;
							case  217 : 
         yyval = yyv[yysp-0];
         
       break;
							case  218 : 
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case  219 : 
         yyval = yyv[yysp-0];
         
       break;
							case  220 : 
         yyval = yyv[yysp-0];
         
       break;
							case  221 : 
         yyval = yyv[yysp-0];
         
       break;
							case  222 : 
         yyval = yyv[yysp-0];
         
       break;
							case  223 : 
         yyval = yyv[yysp-0];
         
       break;
							case  224 : 
         yyval = yyv[yysp-0]; //TODO: FormatIdentifier?
         
       break;
							case  225 : 
         yyval = Formatter.FormatFile(yyv[yysp-0]);
         
       break;
							case  226 : 
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

					int yynacts   = 2884;
					int yyngotos  = 825;
					int yynstates = 353;
					int yynrules  = 226;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,23);yyac++; 
					yya[yyac] = new YYARec(258,24);yyac++; 
					yya[yyac] = new YYARec(259,25);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(265,28);yyac++; 
					yya[yyac] = new YYARec(267,29);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,32);yyac++; 
					yya[yyac] = new YYARec(301,33);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,40);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(312,43);yyac++; 
					yya[yyac] = new YYARec(313,44);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,48);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,50);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(0,-4 );yyac++; 
					yya[yyac] = new YYARec(297,54);yyac++; 
					yya[yyac] = new YYARec(0,-156 );yyac++; 
					yya[yyac] = new YYARec(258,-156 );yyac++; 
					yya[yyac] = new YYARec(260,-156 );yyac++; 
					yya[yyac] = new YYARec(261,-156 );yyac++; 
					yya[yyac] = new YYARec(263,-156 );yyac++; 
					yya[yyac] = new YYARec(266,-156 );yyac++; 
					yya[yyac] = new YYARec(269,-156 );yyac++; 
					yya[yyac] = new YYARec(270,-156 );yyac++; 
					yya[yyac] = new YYARec(271,-156 );yyac++; 
					yya[yyac] = new YYARec(272,-156 );yyac++; 
					yya[yyac] = new YYARec(273,-156 );yyac++; 
					yya[yyac] = new YYARec(275,-156 );yyac++; 
					yya[yyac] = new YYARec(276,-156 );yyac++; 
					yya[yyac] = new YYARec(277,-156 );yyac++; 
					yya[yyac] = new YYARec(278,-156 );yyac++; 
					yya[yyac] = new YYARec(279,-156 );yyac++; 
					yya[yyac] = new YYARec(280,-156 );yyac++; 
					yya[yyac] = new YYARec(281,-156 );yyac++; 
					yya[yyac] = new YYARec(282,-156 );yyac++; 
					yya[yyac] = new YYARec(283,-156 );yyac++; 
					yya[yyac] = new YYARec(284,-156 );yyac++; 
					yya[yyac] = new YYARec(285,-156 );yyac++; 
					yya[yyac] = new YYARec(286,-156 );yyac++; 
					yya[yyac] = new YYARec(287,-156 );yyac++; 
					yya[yyac] = new YYARec(289,-156 );yyac++; 
					yya[yyac] = new YYARec(290,-156 );yyac++; 
					yya[yyac] = new YYARec(291,-156 );yyac++; 
					yya[yyac] = new YYARec(292,-156 );yyac++; 
					yya[yyac] = new YYARec(293,-156 );yyac++; 
					yya[yyac] = new YYARec(298,-156 );yyac++; 
					yya[yyac] = new YYARec(299,-156 );yyac++; 
					yya[yyac] = new YYARec(300,-156 );yyac++; 
					yya[yyac] = new YYARec(301,-156 );yyac++; 
					yya[yyac] = new YYARec(302,-156 );yyac++; 
					yya[yyac] = new YYARec(303,-156 );yyac++; 
					yya[yyac] = new YYARec(304,-156 );yyac++; 
					yya[yyac] = new YYARec(305,-156 );yyac++; 
					yya[yyac] = new YYARec(306,-156 );yyac++; 
					yya[yyac] = new YYARec(307,-156 );yyac++; 
					yya[yyac] = new YYARec(308,-156 );yyac++; 
					yya[yyac] = new YYARec(309,-156 );yyac++; 
					yya[yyac] = new YYARec(310,-156 );yyac++; 
					yya[yyac] = new YYARec(311,-156 );yyac++; 
					yya[yyac] = new YYARec(312,-156 );yyac++; 
					yya[yyac] = new YYARec(313,-156 );yyac++; 
					yya[yyac] = new YYARec(314,-156 );yyac++; 
					yya[yyac] = new YYARec(315,-156 );yyac++; 
					yya[yyac] = new YYARec(316,-156 );yyac++; 
					yya[yyac] = new YYARec(317,-156 );yyac++; 
					yya[yyac] = new YYARec(318,-156 );yyac++; 
					yya[yyac] = new YYARec(319,-156 );yyac++; 
					yya[yyac] = new YYARec(320,-156 );yyac++; 
					yya[yyac] = new YYARec(321,-156 );yyac++; 
					yya[yyac] = new YYARec(322,-156 );yyac++; 
					yya[yyac] = new YYARec(323,-156 );yyac++; 
					yya[yyac] = new YYARec(324,-156 );yyac++; 
					yya[yyac] = new YYARec(325,-156 );yyac++; 
					yya[yyac] = new YYARec(326,-156 );yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,70);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,70);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,75);yyac++; 
					yya[yyac] = new YYARec(324,70);yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(282,-98 );yyac++; 
					yya[yyac] = new YYARec(283,-98 );yyac++; 
					yya[yyac] = new YYARec(287,-98 );yyac++; 
					yya[yyac] = new YYARec(322,-98 );yyac++; 
					yya[yyac] = new YYARec(323,-98 );yyac++; 
					yya[yyac] = new YYARec(324,-98 );yyac++; 
					yya[yyac] = new YYARec(325,-98 );yyac++; 
					yya[yyac] = new YYARec(326,-98 );yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(298,-98 );yyac++; 
					yya[yyac] = new YYARec(299,-98 );yyac++; 
					yya[yyac] = new YYARec(300,-98 );yyac++; 
					yya[yyac] = new YYARec(301,-98 );yyac++; 
					yya[yyac] = new YYARec(302,-98 );yyac++; 
					yya[yyac] = new YYARec(303,-98 );yyac++; 
					yya[yyac] = new YYARec(304,-98 );yyac++; 
					yya[yyac] = new YYARec(305,-98 );yyac++; 
					yya[yyac] = new YYARec(306,-98 );yyac++; 
					yya[yyac] = new YYARec(307,-98 );yyac++; 
					yya[yyac] = new YYARec(309,-98 );yyac++; 
					yya[yyac] = new YYARec(310,-98 );yyac++; 
					yya[yyac] = new YYARec(311,-98 );yyac++; 
					yya[yyac] = new YYARec(312,-98 );yyac++; 
					yya[yyac] = new YYARec(313,-98 );yyac++; 
					yya[yyac] = new YYARec(314,-98 );yyac++; 
					yya[yyac] = new YYARec(315,-98 );yyac++; 
					yya[yyac] = new YYARec(316,-98 );yyac++; 
					yya[yyac] = new YYARec(317,-98 );yyac++; 
					yya[yyac] = new YYARec(318,-98 );yyac++; 
					yya[yyac] = new YYARec(319,-98 );yyac++; 
					yya[yyac] = new YYARec(320,-98 );yyac++; 
					yya[yyac] = new YYARec(321,-98 );yyac++; 
					yya[yyac] = new YYARec(324,-98 );yyac++; 
					yya[yyac] = new YYARec(257,23);yyac++; 
					yya[yyac] = new YYARec(258,24);yyac++; 
					yya[yyac] = new YYARec(259,25);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(265,28);yyac++; 
					yya[yyac] = new YYARec(267,29);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,32);yyac++; 
					yya[yyac] = new YYARec(301,33);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,40);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(312,43);yyac++; 
					yya[yyac] = new YYARec(313,44);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,48);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,50);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(0,-4 );yyac++; 
					yya[yyac] = new YYARec(260,-4 );yyac++; 
					yya[yyac] = new YYARec(261,-4 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(309,83);yyac++; 
					yya[yyac] = new YYARec(315,84);yyac++; 
					yya[yyac] = new YYARec(317,85);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(309,83);yyac++; 
					yya[yyac] = new YYARec(315,84);yyac++; 
					yya[yyac] = new YYARec(317,85);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(309,83);yyac++; 
					yya[yyac] = new YYARec(315,84);yyac++; 
					yya[yyac] = new YYARec(317,85);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(309,83);yyac++; 
					yya[yyac] = new YYARec(315,84);yyac++; 
					yya[yyac] = new YYARec(317,85);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,90);yyac++; 
					yya[yyac] = new YYARec(263,-44 );yyac++; 
					yya[yyac] = new YYARec(298,-44 );yyac++; 
					yya[yyac] = new YYARec(299,-44 );yyac++; 
					yya[yyac] = new YYARec(300,-44 );yyac++; 
					yya[yyac] = new YYARec(301,-44 );yyac++; 
					yya[yyac] = new YYARec(302,-44 );yyac++; 
					yya[yyac] = new YYARec(303,-44 );yyac++; 
					yya[yyac] = new YYARec(304,-44 );yyac++; 
					yya[yyac] = new YYARec(305,-44 );yyac++; 
					yya[yyac] = new YYARec(306,-44 );yyac++; 
					yya[yyac] = new YYARec(307,-44 );yyac++; 
					yya[yyac] = new YYARec(309,-44 );yyac++; 
					yya[yyac] = new YYARec(310,-44 );yyac++; 
					yya[yyac] = new YYARec(311,-44 );yyac++; 
					yya[yyac] = new YYARec(312,-44 );yyac++; 
					yya[yyac] = new YYARec(313,-44 );yyac++; 
					yya[yyac] = new YYARec(314,-44 );yyac++; 
					yya[yyac] = new YYARec(315,-44 );yyac++; 
					yya[yyac] = new YYARec(316,-44 );yyac++; 
					yya[yyac] = new YYARec(317,-44 );yyac++; 
					yya[yyac] = new YYARec(318,-44 );yyac++; 
					yya[yyac] = new YYARec(319,-44 );yyac++; 
					yya[yyac] = new YYARec(320,-44 );yyac++; 
					yya[yyac] = new YYARec(321,-44 );yyac++; 
					yya[yyac] = new YYARec(324,-44 );yyac++; 
					yya[yyac] = new YYARec(0,-200 );yyac++; 
					yya[yyac] = new YYARec(260,-200 );yyac++; 
					yya[yyac] = new YYARec(261,-200 );yyac++; 
					yya[yyac] = new YYARec(297,-200 );yyac++; 
					yya[yyac] = new YYARec(263,-41 );yyac++; 
					yya[yyac] = new YYARec(282,-41 );yyac++; 
					yya[yyac] = new YYARec(283,-41 );yyac++; 
					yya[yyac] = new YYARec(287,-41 );yyac++; 
					yya[yyac] = new YYARec(322,-41 );yyac++; 
					yya[yyac] = new YYARec(323,-41 );yyac++; 
					yya[yyac] = new YYARec(324,-41 );yyac++; 
					yya[yyac] = new YYARec(325,-41 );yyac++; 
					yya[yyac] = new YYARec(326,-41 );yyac++; 
					yya[yyac] = new YYARec(0,-203 );yyac++; 
					yya[yyac] = new YYARec(260,-203 );yyac++; 
					yya[yyac] = new YYARec(261,-203 );yyac++; 
					yya[yyac] = new YYARec(297,-203 );yyac++; 
					yya[yyac] = new YYARec(298,-57 );yyac++; 
					yya[yyac] = new YYARec(299,-57 );yyac++; 
					yya[yyac] = new YYARec(300,-57 );yyac++; 
					yya[yyac] = new YYARec(301,-57 );yyac++; 
					yya[yyac] = new YYARec(302,-57 );yyac++; 
					yya[yyac] = new YYARec(303,-57 );yyac++; 
					yya[yyac] = new YYARec(304,-57 );yyac++; 
					yya[yyac] = new YYARec(305,-57 );yyac++; 
					yya[yyac] = new YYARec(306,-57 );yyac++; 
					yya[yyac] = new YYARec(307,-57 );yyac++; 
					yya[yyac] = new YYARec(309,-57 );yyac++; 
					yya[yyac] = new YYARec(310,-57 );yyac++; 
					yya[yyac] = new YYARec(311,-57 );yyac++; 
					yya[yyac] = new YYARec(312,-57 );yyac++; 
					yya[yyac] = new YYARec(313,-57 );yyac++; 
					yya[yyac] = new YYARec(314,-57 );yyac++; 
					yya[yyac] = new YYARec(315,-57 );yyac++; 
					yya[yyac] = new YYARec(316,-57 );yyac++; 
					yya[yyac] = new YYARec(317,-57 );yyac++; 
					yya[yyac] = new YYARec(318,-57 );yyac++; 
					yya[yyac] = new YYARec(319,-57 );yyac++; 
					yya[yyac] = new YYARec(320,-57 );yyac++; 
					yya[yyac] = new YYARec(321,-57 );yyac++; 
					yya[yyac] = new YYARec(322,-57 );yyac++; 
					yya[yyac] = new YYARec(324,-57 );yyac++; 
					yya[yyac] = new YYARec(0,-198 );yyac++; 
					yya[yyac] = new YYARec(260,-198 );yyac++; 
					yya[yyac] = new YYARec(261,-198 );yyac++; 
					yya[yyac] = new YYARec(297,-198 );yyac++; 
					yya[yyac] = new YYARec(298,-61 );yyac++; 
					yya[yyac] = new YYARec(299,-61 );yyac++; 
					yya[yyac] = new YYARec(300,-61 );yyac++; 
					yya[yyac] = new YYARec(301,-61 );yyac++; 
					yya[yyac] = new YYARec(302,-61 );yyac++; 
					yya[yyac] = new YYARec(303,-61 );yyac++; 
					yya[yyac] = new YYARec(304,-61 );yyac++; 
					yya[yyac] = new YYARec(305,-61 );yyac++; 
					yya[yyac] = new YYARec(306,-61 );yyac++; 
					yya[yyac] = new YYARec(307,-61 );yyac++; 
					yya[yyac] = new YYARec(309,-61 );yyac++; 
					yya[yyac] = new YYARec(310,-61 );yyac++; 
					yya[yyac] = new YYARec(311,-61 );yyac++; 
					yya[yyac] = new YYARec(312,-61 );yyac++; 
					yya[yyac] = new YYARec(313,-61 );yyac++; 
					yya[yyac] = new YYARec(314,-61 );yyac++; 
					yya[yyac] = new YYARec(315,-61 );yyac++; 
					yya[yyac] = new YYARec(316,-61 );yyac++; 
					yya[yyac] = new YYARec(317,-61 );yyac++; 
					yya[yyac] = new YYARec(318,-61 );yyac++; 
					yya[yyac] = new YYARec(319,-61 );yyac++; 
					yya[yyac] = new YYARec(320,-61 );yyac++; 
					yya[yyac] = new YYARec(321,-61 );yyac++; 
					yya[yyac] = new YYARec(324,-61 );yyac++; 
					yya[yyac] = new YYARec(0,-205 );yyac++; 
					yya[yyac] = new YYARec(260,-205 );yyac++; 
					yya[yyac] = new YYARec(261,-205 );yyac++; 
					yya[yyac] = new YYARec(297,-205 );yyac++; 
					yya[yyac] = new YYARec(298,-76 );yyac++; 
					yya[yyac] = new YYARec(299,-76 );yyac++; 
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
					yya[yyac] = new YYARec(311,-76 );yyac++; 
					yya[yyac] = new YYARec(312,-76 );yyac++; 
					yya[yyac] = new YYARec(313,-76 );yyac++; 
					yya[yyac] = new YYARec(314,-76 );yyac++; 
					yya[yyac] = new YYARec(315,-76 );yyac++; 
					yya[yyac] = new YYARec(316,-76 );yyac++; 
					yya[yyac] = new YYARec(317,-76 );yyac++; 
					yya[yyac] = new YYARec(318,-76 );yyac++; 
					yya[yyac] = new YYARec(319,-76 );yyac++; 
					yya[yyac] = new YYARec(320,-76 );yyac++; 
					yya[yyac] = new YYARec(321,-76 );yyac++; 
					yya[yyac] = new YYARec(324,-76 );yyac++; 
					yya[yyac] = new YYARec(0,-202 );yyac++; 
					yya[yyac] = new YYARec(260,-202 );yyac++; 
					yya[yyac] = new YYARec(261,-202 );yyac++; 
					yya[yyac] = new YYARec(297,-202 );yyac++; 
					yya[yyac] = new YYARec(263,-191 );yyac++; 
					yya[yyac] = new YYARec(282,-191 );yyac++; 
					yya[yyac] = new YYARec(283,-191 );yyac++; 
					yya[yyac] = new YYARec(287,-191 );yyac++; 
					yya[yyac] = new YYARec(322,-191 );yyac++; 
					yya[yyac] = new YYARec(323,-191 );yyac++; 
					yya[yyac] = new YYARec(324,-191 );yyac++; 
					yya[yyac] = new YYARec(325,-191 );yyac++; 
					yya[yyac] = new YYARec(326,-191 );yyac++; 
					yya[yyac] = new YYARec(0,-212 );yyac++; 
					yya[yyac] = new YYARec(260,-212 );yyac++; 
					yya[yyac] = new YYARec(261,-212 );yyac++; 
					yya[yyac] = new YYARec(297,-212 );yyac++; 
					yya[yyac] = new YYARec(263,-39 );yyac++; 
					yya[yyac] = new YYARec(282,-39 );yyac++; 
					yya[yyac] = new YYARec(283,-39 );yyac++; 
					yya[yyac] = new YYARec(287,-39 );yyac++; 
					yya[yyac] = new YYARec(322,-39 );yyac++; 
					yya[yyac] = new YYARec(323,-39 );yyac++; 
					yya[yyac] = new YYARec(324,-39 );yyac++; 
					yya[yyac] = new YYARec(325,-39 );yyac++; 
					yya[yyac] = new YYARec(326,-39 );yyac++; 
					yya[yyac] = new YYARec(0,-195 );yyac++; 
					yya[yyac] = new YYARec(260,-195 );yyac++; 
					yya[yyac] = new YYARec(261,-195 );yyac++; 
					yya[yyac] = new YYARec(297,-195 );yyac++; 
					yya[yyac] = new YYARec(263,-43 );yyac++; 
					yya[yyac] = new YYARec(298,-43 );yyac++; 
					yya[yyac] = new YYARec(299,-43 );yyac++; 
					yya[yyac] = new YYARec(300,-43 );yyac++; 
					yya[yyac] = new YYARec(301,-43 );yyac++; 
					yya[yyac] = new YYARec(302,-43 );yyac++; 
					yya[yyac] = new YYARec(303,-43 );yyac++; 
					yya[yyac] = new YYARec(304,-43 );yyac++; 
					yya[yyac] = new YYARec(305,-43 );yyac++; 
					yya[yyac] = new YYARec(306,-43 );yyac++; 
					yya[yyac] = new YYARec(307,-43 );yyac++; 
					yya[yyac] = new YYARec(309,-43 );yyac++; 
					yya[yyac] = new YYARec(310,-43 );yyac++; 
					yya[yyac] = new YYARec(311,-43 );yyac++; 
					yya[yyac] = new YYARec(312,-43 );yyac++; 
					yya[yyac] = new YYARec(313,-43 );yyac++; 
					yya[yyac] = new YYARec(314,-43 );yyac++; 
					yya[yyac] = new YYARec(315,-43 );yyac++; 
					yya[yyac] = new YYARec(316,-43 );yyac++; 
					yya[yyac] = new YYARec(317,-43 );yyac++; 
					yya[yyac] = new YYARec(318,-43 );yyac++; 
					yya[yyac] = new YYARec(319,-43 );yyac++; 
					yya[yyac] = new YYARec(320,-43 );yyac++; 
					yya[yyac] = new YYARec(321,-43 );yyac++; 
					yya[yyac] = new YYARec(324,-43 );yyac++; 
					yya[yyac] = new YYARec(0,-194 );yyac++; 
					yya[yyac] = new YYARec(260,-194 );yyac++; 
					yya[yyac] = new YYARec(261,-194 );yyac++; 
					yya[yyac] = new YYARec(297,-194 );yyac++; 
					yya[yyac] = new YYARec(298,-62 );yyac++; 
					yya[yyac] = new YYARec(299,-62 );yyac++; 
					yya[yyac] = new YYARec(300,-62 );yyac++; 
					yya[yyac] = new YYARec(301,-62 );yyac++; 
					yya[yyac] = new YYARec(302,-62 );yyac++; 
					yya[yyac] = new YYARec(303,-62 );yyac++; 
					yya[yyac] = new YYARec(304,-62 );yyac++; 
					yya[yyac] = new YYARec(305,-62 );yyac++; 
					yya[yyac] = new YYARec(306,-62 );yyac++; 
					yya[yyac] = new YYARec(307,-62 );yyac++; 
					yya[yyac] = new YYARec(309,-62 );yyac++; 
					yya[yyac] = new YYARec(310,-62 );yyac++; 
					yya[yyac] = new YYARec(311,-62 );yyac++; 
					yya[yyac] = new YYARec(312,-62 );yyac++; 
					yya[yyac] = new YYARec(313,-62 );yyac++; 
					yya[yyac] = new YYARec(314,-62 );yyac++; 
					yya[yyac] = new YYARec(315,-62 );yyac++; 
					yya[yyac] = new YYARec(316,-62 );yyac++; 
					yya[yyac] = new YYARec(317,-62 );yyac++; 
					yya[yyac] = new YYARec(318,-62 );yyac++; 
					yya[yyac] = new YYARec(319,-62 );yyac++; 
					yya[yyac] = new YYARec(320,-62 );yyac++; 
					yya[yyac] = new YYARec(321,-62 );yyac++; 
					yya[yyac] = new YYARec(324,-62 );yyac++; 
					yya[yyac] = new YYARec(0,-197 );yyac++; 
					yya[yyac] = new YYARec(260,-197 );yyac++; 
					yya[yyac] = new YYARec(261,-197 );yyac++; 
					yya[yyac] = new YYARec(297,-197 );yyac++; 
					yya[yyac] = new YYARec(263,-189 );yyac++; 
					yya[yyac] = new YYARec(282,-189 );yyac++; 
					yya[yyac] = new YYARec(283,-189 );yyac++; 
					yya[yyac] = new YYARec(287,-189 );yyac++; 
					yya[yyac] = new YYARec(322,-189 );yyac++; 
					yya[yyac] = new YYARec(323,-189 );yyac++; 
					yya[yyac] = new YYARec(324,-189 );yyac++; 
					yya[yyac] = new YYARec(325,-189 );yyac++; 
					yya[yyac] = new YYARec(326,-189 );yyac++; 
					yya[yyac] = new YYARec(0,-208 );yyac++; 
					yya[yyac] = new YYARec(260,-208 );yyac++; 
					yya[yyac] = new YYARec(261,-208 );yyac++; 
					yya[yyac] = new YYARec(297,-208 );yyac++; 
					yya[yyac] = new YYARec(263,-190 );yyac++; 
					yya[yyac] = new YYARec(282,-190 );yyac++; 
					yya[yyac] = new YYARec(283,-190 );yyac++; 
					yya[yyac] = new YYARec(287,-190 );yyac++; 
					yya[yyac] = new YYARec(322,-190 );yyac++; 
					yya[yyac] = new YYARec(323,-190 );yyac++; 
					yya[yyac] = new YYARec(324,-190 );yyac++; 
					yya[yyac] = new YYARec(325,-190 );yyac++; 
					yya[yyac] = new YYARec(326,-190 );yyac++; 
					yya[yyac] = new YYARec(0,-210 );yyac++; 
					yya[yyac] = new YYARec(260,-210 );yyac++; 
					yya[yyac] = new YYARec(261,-210 );yyac++; 
					yya[yyac] = new YYARec(297,-210 );yyac++; 
					yya[yyac] = new YYARec(263,-40 );yyac++; 
					yya[yyac] = new YYARec(282,-40 );yyac++; 
					yya[yyac] = new YYARec(283,-40 );yyac++; 
					yya[yyac] = new YYARec(287,-40 );yyac++; 
					yya[yyac] = new YYARec(322,-40 );yyac++; 
					yya[yyac] = new YYARec(323,-40 );yyac++; 
					yya[yyac] = new YYARec(324,-40 );yyac++; 
					yya[yyac] = new YYARec(325,-40 );yyac++; 
					yya[yyac] = new YYARec(326,-40 );yyac++; 
					yya[yyac] = new YYARec(0,-207 );yyac++; 
					yya[yyac] = new YYARec(260,-207 );yyac++; 
					yya[yyac] = new YYARec(261,-207 );yyac++; 
					yya[yyac] = new YYARec(297,-207 );yyac++; 
					yya[yyac] = new YYARec(301,96);yyac++; 
					yya[yyac] = new YYARec(302,97);yyac++; 
					yya[yyac] = new YYARec(305,98);yyac++; 
					yya[yyac] = new YYARec(306,99);yyac++; 
					yya[yyac] = new YYARec(311,100);yyac++; 
					yya[yyac] = new YYARec(312,101);yyac++; 
					yya[yyac] = new YYARec(313,102);yyac++; 
					yya[yyac] = new YYARec(315,103);yyac++; 
					yya[yyac] = new YYARec(316,104);yyac++; 
					yya[yyac] = new YYARec(317,105);yyac++; 
					yya[yyac] = new YYARec(318,106);yyac++; 
					yya[yyac] = new YYARec(319,107);yyac++; 
					yya[yyac] = new YYARec(320,108);yyac++; 
					yya[yyac] = new YYARec(321,109);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(266,-98 );yyac++; 
					yya[yyac] = new YYARec(283,111);yyac++; 
					yya[yyac] = new YYARec(258,-187 );yyac++; 
					yya[yyac] = new YYARec(263,-187 );yyac++; 
					yya[yyac] = new YYARec(266,-187 );yyac++; 
					yya[yyac] = new YYARec(298,-187 );yyac++; 
					yya[yyac] = new YYARec(299,-187 );yyac++; 
					yya[yyac] = new YYARec(300,-187 );yyac++; 
					yya[yyac] = new YYARec(301,-187 );yyac++; 
					yya[yyac] = new YYARec(302,-187 );yyac++; 
					yya[yyac] = new YYARec(303,-187 );yyac++; 
					yya[yyac] = new YYARec(304,-187 );yyac++; 
					yya[yyac] = new YYARec(305,-187 );yyac++; 
					yya[yyac] = new YYARec(306,-187 );yyac++; 
					yya[yyac] = new YYARec(307,-187 );yyac++; 
					yya[yyac] = new YYARec(309,-187 );yyac++; 
					yya[yyac] = new YYARec(310,-187 );yyac++; 
					yya[yyac] = new YYARec(311,-187 );yyac++; 
					yya[yyac] = new YYARec(312,-187 );yyac++; 
					yya[yyac] = new YYARec(313,-187 );yyac++; 
					yya[yyac] = new YYARec(314,-187 );yyac++; 
					yya[yyac] = new YYARec(315,-187 );yyac++; 
					yya[yyac] = new YYARec(316,-187 );yyac++; 
					yya[yyac] = new YYARec(317,-187 );yyac++; 
					yya[yyac] = new YYARec(318,-187 );yyac++; 
					yya[yyac] = new YYARec(319,-187 );yyac++; 
					yya[yyac] = new YYARec(320,-187 );yyac++; 
					yya[yyac] = new YYARec(321,-187 );yyac++; 
					yya[yyac] = new YYARec(324,-187 );yyac++; 
					yya[yyac] = new YYARec(325,-187 );yyac++; 
					yya[yyac] = new YYARec(326,-187 );yyac++; 
					yya[yyac] = new YYARec(258,113);yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(266,-98 );yyac++; 
					yya[yyac] = new YYARec(326,-98 );yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(325,-98 );yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(322,126);yyac++; 
					yya[yyac] = new YYARec(323,127);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,90);yyac++; 
					yya[yyac] = new YYARec(326,128);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,70);yyac++; 
					yya[yyac] = new YYARec(258,131);yyac++; 
					yya[yyac] = new YYARec(258,132);yyac++; 
					yya[yyac] = new YYARec(258,133);yyac++; 
					yya[yyac] = new YYARec(263,134);yyac++; 
					yya[yyac] = new YYARec(258,135);yyac++; 
					yya[yyac] = new YYARec(258,136);yyac++; 
					yya[yyac] = new YYARec(266,137);yyac++; 
					yya[yyac] = new YYARec(324,138);yyac++; 
					yya[yyac] = new YYARec(266,140);yyac++; 
					yya[yyac] = new YYARec(326,128);yyac++; 
					yya[yyac] = new YYARec(325,90);yyac++; 
					yya[yyac] = new YYARec(322,142);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(258,144);yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(258,-98 );yyac++; 
					yya[yyac] = new YYARec(298,-98 );yyac++; 
					yya[yyac] = new YYARec(299,-98 );yyac++; 
					yya[yyac] = new YYARec(300,-98 );yyac++; 
					yya[yyac] = new YYARec(301,-98 );yyac++; 
					yya[yyac] = new YYARec(302,-98 );yyac++; 
					yya[yyac] = new YYARec(303,-98 );yyac++; 
					yya[yyac] = new YYARec(304,-98 );yyac++; 
					yya[yyac] = new YYARec(305,-98 );yyac++; 
					yya[yyac] = new YYARec(306,-98 );yyac++; 
					yya[yyac] = new YYARec(307,-98 );yyac++; 
					yya[yyac] = new YYARec(309,-98 );yyac++; 
					yya[yyac] = new YYARec(310,-98 );yyac++; 
					yya[yyac] = new YYARec(311,-98 );yyac++; 
					yya[yyac] = new YYARec(312,-98 );yyac++; 
					yya[yyac] = new YYARec(313,-98 );yyac++; 
					yya[yyac] = new YYARec(314,-98 );yyac++; 
					yya[yyac] = new YYARec(315,-98 );yyac++; 
					yya[yyac] = new YYARec(316,-98 );yyac++; 
					yya[yyac] = new YYARec(317,-98 );yyac++; 
					yya[yyac] = new YYARec(318,-98 );yyac++; 
					yya[yyac] = new YYARec(319,-98 );yyac++; 
					yya[yyac] = new YYARec(320,-98 );yyac++; 
					yya[yyac] = new YYARec(321,-98 );yyac++; 
					yya[yyac] = new YYARec(324,-98 );yyac++; 
					yya[yyac] = new YYARec(258,146);yyac++; 
					yya[yyac] = new YYARec(257,23);yyac++; 
					yya[yyac] = new YYARec(258,24);yyac++; 
					yya[yyac] = new YYARec(259,25);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(265,28);yyac++; 
					yya[yyac] = new YYARec(267,29);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,32);yyac++; 
					yya[yyac] = new YYARec(301,33);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,40);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(312,43);yyac++; 
					yya[yyac] = new YYARec(313,44);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,48);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,50);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(260,-4 );yyac++; 
					yya[yyac] = new YYARec(261,-4 );yyac++; 
					yya[yyac] = new YYARec(257,23);yyac++; 
					yya[yyac] = new YYARec(258,24);yyac++; 
					yya[yyac] = new YYARec(259,25);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(265,28);yyac++; 
					yya[yyac] = new YYARec(267,29);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,32);yyac++; 
					yya[yyac] = new YYARec(301,33);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,40);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(312,43);yyac++; 
					yya[yyac] = new YYARec(313,44);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,48);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,50);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(260,-4 );yyac++; 
					yya[yyac] = new YYARec(261,-4 );yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,126);yyac++; 
					yya[yyac] = new YYARec(323,127);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,90);yyac++; 
					yya[yyac] = new YYARec(326,128);yyac++; 
					yya[yyac] = new YYARec(257,170);yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(259,172);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,173);yyac++; 
					yya[yyac] = new YYARec(288,174);yyac++; 
					yya[yyac] = new YYARec(294,175);yyac++; 
					yya[yyac] = new YYARec(295,176);yyac++; 
					yya[yyac] = new YYARec(296,177);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,178);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,179);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,180);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,181);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,182);yyac++; 
					yya[yyac] = new YYARec(267,-83 );yyac++; 
					yya[yyac] = new YYARec(258,183);yyac++; 
					yya[yyac] = new YYARec(257,188);yyac++; 
					yya[yyac] = new YYARec(258,189);yyac++; 
					yya[yyac] = new YYARec(259,190);yyac++; 
					yya[yyac] = new YYARec(301,96);yyac++; 
					yya[yyac] = new YYARec(302,97);yyac++; 
					yya[yyac] = new YYARec(306,99);yyac++; 
					yya[yyac] = new YYARec(311,100);yyac++; 
					yya[yyac] = new YYARec(312,101);yyac++; 
					yya[yyac] = new YYARec(315,103);yyac++; 
					yya[yyac] = new YYARec(317,105);yyac++; 
					yya[yyac] = new YYARec(319,107);yyac++; 
					yya[yyac] = new YYARec(320,108);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(267,-66 );yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(258,-98 );yyac++; 
					yya[yyac] = new YYARec(282,-98 );yyac++; 
					yya[yyac] = new YYARec(283,-98 );yyac++; 
					yya[yyac] = new YYARec(287,-98 );yyac++; 
					yya[yyac] = new YYARec(322,-98 );yyac++; 
					yya[yyac] = new YYARec(324,-98 );yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,70);yyac++; 
					yya[yyac] = new YYARec(258,-45 );yyac++; 
					yya[yyac] = new YYARec(258,193);yyac++; 
					yya[yyac] = new YYARec(260,194);yyac++; 
					yya[yyac] = new YYARec(261,195);yyac++; 
					yya[yyac] = new YYARec(258,196);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,142);yyac++; 
					yya[yyac] = new YYARec(323,143);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(258,198);yyac++; 
					yya[yyac] = new YYARec(297,54);yyac++; 
					yya[yyac] = new YYARec(289,-156 );yyac++; 
					yya[yyac] = new YYARec(290,-156 );yyac++; 
					yya[yyac] = new YYARec(291,-156 );yyac++; 
					yya[yyac] = new YYARec(292,-156 );yyac++; 
					yya[yyac] = new YYARec(293,-156 );yyac++; 
					yya[yyac] = new YYARec(268,-224 );yyac++; 
					yya[yyac] = new YYARec(258,199);yyac++; 
					yya[yyac] = new YYARec(258,201);yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(282,-98 );yyac++; 
					yya[yyac] = new YYARec(283,-98 );yyac++; 
					yya[yyac] = new YYARec(287,-98 );yyac++; 
					yya[yyac] = new YYARec(298,-98 );yyac++; 
					yya[yyac] = new YYARec(299,-98 );yyac++; 
					yya[yyac] = new YYARec(300,-98 );yyac++; 
					yya[yyac] = new YYARec(301,-98 );yyac++; 
					yya[yyac] = new YYARec(302,-98 );yyac++; 
					yya[yyac] = new YYARec(303,-98 );yyac++; 
					yya[yyac] = new YYARec(304,-98 );yyac++; 
					yya[yyac] = new YYARec(305,-98 );yyac++; 
					yya[yyac] = new YYARec(306,-98 );yyac++; 
					yya[yyac] = new YYARec(307,-98 );yyac++; 
					yya[yyac] = new YYARec(308,-98 );yyac++; 
					yya[yyac] = new YYARec(309,-98 );yyac++; 
					yya[yyac] = new YYARec(310,-98 );yyac++; 
					yya[yyac] = new YYARec(311,-98 );yyac++; 
					yya[yyac] = new YYARec(312,-98 );yyac++; 
					yya[yyac] = new YYARec(313,-98 );yyac++; 
					yya[yyac] = new YYARec(314,-98 );yyac++; 
					yya[yyac] = new YYARec(315,-98 );yyac++; 
					yya[yyac] = new YYARec(316,-98 );yyac++; 
					yya[yyac] = new YYARec(317,-98 );yyac++; 
					yya[yyac] = new YYARec(318,-98 );yyac++; 
					yya[yyac] = new YYARec(319,-98 );yyac++; 
					yya[yyac] = new YYARec(320,-98 );yyac++; 
					yya[yyac] = new YYARec(321,-98 );yyac++; 
					yya[yyac] = new YYARec(322,-98 );yyac++; 
					yya[yyac] = new YYARec(323,-98 );yyac++; 
					yya[yyac] = new YYARec(324,-98 );yyac++; 
					yya[yyac] = new YYARec(325,-98 );yyac++; 
					yya[yyac] = new YYARec(326,-98 );yyac++; 
					yya[yyac] = new YYARec(257,170);yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(259,172);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,173);yyac++; 
					yya[yyac] = new YYARec(288,174);yyac++; 
					yya[yyac] = new YYARec(294,175);yyac++; 
					yya[yyac] = new YYARec(295,176);yyac++; 
					yya[yyac] = new YYARec(296,177);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,178);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,179);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,180);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,181);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,182);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(267,-83 );yyac++; 
					yya[yyac] = new YYARec(257,170);yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(259,172);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,173);yyac++; 
					yya[yyac] = new YYARec(288,174);yyac++; 
					yya[yyac] = new YYARec(294,175);yyac++; 
					yya[yyac] = new YYARec(295,176);yyac++; 
					yya[yyac] = new YYARec(296,177);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,178);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,179);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,180);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,181);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,182);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(267,-83 );yyac++; 
					yya[yyac] = new YYARec(268,204);yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(282,-98 );yyac++; 
					yya[yyac] = new YYARec(283,-98 );yyac++; 
					yya[yyac] = new YYARec(287,-98 );yyac++; 
					yya[yyac] = new YYARec(298,-98 );yyac++; 
					yya[yyac] = new YYARec(299,-98 );yyac++; 
					yya[yyac] = new YYARec(300,-98 );yyac++; 
					yya[yyac] = new YYARec(301,-98 );yyac++; 
					yya[yyac] = new YYARec(302,-98 );yyac++; 
					yya[yyac] = new YYARec(303,-98 );yyac++; 
					yya[yyac] = new YYARec(304,-98 );yyac++; 
					yya[yyac] = new YYARec(305,-98 );yyac++; 
					yya[yyac] = new YYARec(306,-98 );yyac++; 
					yya[yyac] = new YYARec(307,-98 );yyac++; 
					yya[yyac] = new YYARec(308,-98 );yyac++; 
					yya[yyac] = new YYARec(309,-98 );yyac++; 
					yya[yyac] = new YYARec(310,-98 );yyac++; 
					yya[yyac] = new YYARec(311,-98 );yyac++; 
					yya[yyac] = new YYARec(312,-98 );yyac++; 
					yya[yyac] = new YYARec(313,-98 );yyac++; 
					yya[yyac] = new YYARec(314,-98 );yyac++; 
					yya[yyac] = new YYARec(315,-98 );yyac++; 
					yya[yyac] = new YYARec(316,-98 );yyac++; 
					yya[yyac] = new YYARec(317,-98 );yyac++; 
					yya[yyac] = new YYARec(318,-98 );yyac++; 
					yya[yyac] = new YYARec(319,-98 );yyac++; 
					yya[yyac] = new YYARec(320,-98 );yyac++; 
					yya[yyac] = new YYARec(321,-98 );yyac++; 
					yya[yyac] = new YYARec(322,-98 );yyac++; 
					yya[yyac] = new YYARec(323,-98 );yyac++; 
					yya[yyac] = new YYARec(324,-98 );yyac++; 
					yya[yyac] = new YYARec(325,-98 );yyac++; 
					yya[yyac] = new YYARec(326,-98 );yyac++; 
					yya[yyac] = new YYARec(268,-215 );yyac++; 
					yya[yyac] = new YYARec(289,-215 );yyac++; 
					yya[yyac] = new YYARec(290,-215 );yyac++; 
					yya[yyac] = new YYARec(291,-215 );yyac++; 
					yya[yyac] = new YYARec(292,-215 );yyac++; 
					yya[yyac] = new YYARec(293,-215 );yyac++; 
					yya[yyac] = new YYARec(297,-215 );yyac++; 
					yya[yyac] = new YYARec(267,206);yyac++; 
					yya[yyac] = new YYARec(257,170);yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(259,172);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,173);yyac++; 
					yya[yyac] = new YYARec(288,174);yyac++; 
					yya[yyac] = new YYARec(294,175);yyac++; 
					yya[yyac] = new YYARec(295,176);yyac++; 
					yya[yyac] = new YYARec(296,177);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,178);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,179);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,180);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,181);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,182);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(267,-83 );yyac++; 
					yya[yyac] = new YYARec(289,209);yyac++; 
					yya[yyac] = new YYARec(290,210);yyac++; 
					yya[yyac] = new YYARec(291,211);yyac++; 
					yya[yyac] = new YYARec(292,212);yyac++; 
					yya[yyac] = new YYARec(293,213);yyac++; 
					yya[yyac] = new YYARec(309,83);yyac++; 
					yya[yyac] = new YYARec(315,84);yyac++; 
					yya[yyac] = new YYARec(317,85);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(309,83);yyac++; 
					yya[yyac] = new YYARec(315,84);yyac++; 
					yya[yyac] = new YYARec(317,85);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(257,170);yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(259,172);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,173);yyac++; 
					yya[yyac] = new YYARec(288,174);yyac++; 
					yya[yyac] = new YYARec(294,175);yyac++; 
					yya[yyac] = new YYARec(295,176);yyac++; 
					yya[yyac] = new YYARec(296,177);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,178);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,179);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,180);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,181);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,182);yyac++; 
					yya[yyac] = new YYARec(267,-83 );yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(266,240);yyac++; 
					yya[yyac] = new YYARec(258,-174 );yyac++; 
					yya[yyac] = new YYARec(263,-174 );yyac++; 
					yya[yyac] = new YYARec(282,-174 );yyac++; 
					yya[yyac] = new YYARec(283,-174 );yyac++; 
					yya[yyac] = new YYARec(287,-174 );yyac++; 
					yya[yyac] = new YYARec(298,-174 );yyac++; 
					yya[yyac] = new YYARec(299,-174 );yyac++; 
					yya[yyac] = new YYARec(300,-174 );yyac++; 
					yya[yyac] = new YYARec(301,-174 );yyac++; 
					yya[yyac] = new YYARec(302,-174 );yyac++; 
					yya[yyac] = new YYARec(303,-174 );yyac++; 
					yya[yyac] = new YYARec(304,-174 );yyac++; 
					yya[yyac] = new YYARec(305,-174 );yyac++; 
					yya[yyac] = new YYARec(306,-174 );yyac++; 
					yya[yyac] = new YYARec(307,-174 );yyac++; 
					yya[yyac] = new YYARec(308,-174 );yyac++; 
					yya[yyac] = new YYARec(309,-174 );yyac++; 
					yya[yyac] = new YYARec(310,-174 );yyac++; 
					yya[yyac] = new YYARec(311,-174 );yyac++; 
					yya[yyac] = new YYARec(312,-174 );yyac++; 
					yya[yyac] = new YYARec(313,-174 );yyac++; 
					yya[yyac] = new YYARec(314,-174 );yyac++; 
					yya[yyac] = new YYARec(315,-174 );yyac++; 
					yya[yyac] = new YYARec(316,-174 );yyac++; 
					yya[yyac] = new YYARec(317,-174 );yyac++; 
					yya[yyac] = new YYARec(318,-174 );yyac++; 
					yya[yyac] = new YYARec(319,-174 );yyac++; 
					yya[yyac] = new YYARec(320,-174 );yyac++; 
					yya[yyac] = new YYARec(321,-174 );yyac++; 
					yya[yyac] = new YYARec(322,-174 );yyac++; 
					yya[yyac] = new YYARec(323,-174 );yyac++; 
					yya[yyac] = new YYARec(324,-174 );yyac++; 
					yya[yyac] = new YYARec(325,-174 );yyac++; 
					yya[yyac] = new YYARec(326,-174 );yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(258,-178 );yyac++; 
					yya[yyac] = new YYARec(263,-178 );yyac++; 
					yya[yyac] = new YYARec(282,-178 );yyac++; 
					yya[yyac] = new YYARec(283,-178 );yyac++; 
					yya[yyac] = new YYARec(287,-178 );yyac++; 
					yya[yyac] = new YYARec(298,-178 );yyac++; 
					yya[yyac] = new YYARec(299,-178 );yyac++; 
					yya[yyac] = new YYARec(300,-178 );yyac++; 
					yya[yyac] = new YYARec(301,-178 );yyac++; 
					yya[yyac] = new YYARec(302,-178 );yyac++; 
					yya[yyac] = new YYARec(303,-178 );yyac++; 
					yya[yyac] = new YYARec(304,-178 );yyac++; 
					yya[yyac] = new YYARec(305,-178 );yyac++; 
					yya[yyac] = new YYARec(306,-178 );yyac++; 
					yya[yyac] = new YYARec(307,-178 );yyac++; 
					yya[yyac] = new YYARec(308,-178 );yyac++; 
					yya[yyac] = new YYARec(309,-178 );yyac++; 
					yya[yyac] = new YYARec(310,-178 );yyac++; 
					yya[yyac] = new YYARec(311,-178 );yyac++; 
					yya[yyac] = new YYARec(312,-178 );yyac++; 
					yya[yyac] = new YYARec(313,-178 );yyac++; 
					yya[yyac] = new YYARec(314,-178 );yyac++; 
					yya[yyac] = new YYARec(315,-178 );yyac++; 
					yya[yyac] = new YYARec(316,-178 );yyac++; 
					yya[yyac] = new YYARec(317,-178 );yyac++; 
					yya[yyac] = new YYARec(318,-178 );yyac++; 
					yya[yyac] = new YYARec(319,-178 );yyac++; 
					yya[yyac] = new YYARec(320,-178 );yyac++; 
					yya[yyac] = new YYARec(321,-178 );yyac++; 
					yya[yyac] = new YYARec(322,-178 );yyac++; 
					yya[yyac] = new YYARec(323,-178 );yyac++; 
					yya[yyac] = new YYARec(324,-178 );yyac++; 
					yya[yyac] = new YYARec(325,-178 );yyac++; 
					yya[yyac] = new YYARec(326,-178 );yyac++; 
					yya[yyac] = new YYARec(268,-199 );yyac++; 
					yya[yyac] = new YYARec(289,-199 );yyac++; 
					yya[yyac] = new YYARec(290,-199 );yyac++; 
					yya[yyac] = new YYARec(291,-199 );yyac++; 
					yya[yyac] = new YYARec(292,-199 );yyac++; 
					yya[yyac] = new YYARec(293,-199 );yyac++; 
					yya[yyac] = new YYARec(297,-199 );yyac++; 
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
					yya[yyac] = new YYARec(303,-177 );yyac++; 
					yya[yyac] = new YYARec(304,-177 );yyac++; 
					yya[yyac] = new YYARec(305,-177 );yyac++; 
					yya[yyac] = new YYARec(306,-177 );yyac++; 
					yya[yyac] = new YYARec(307,-177 );yyac++; 
					yya[yyac] = new YYARec(308,-177 );yyac++; 
					yya[yyac] = new YYARec(309,-177 );yyac++; 
					yya[yyac] = new YYARec(310,-177 );yyac++; 
					yya[yyac] = new YYARec(311,-177 );yyac++; 
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
					yya[yyac] = new YYARec(326,-177 );yyac++; 
					yya[yyac] = new YYARec(268,-196 );yyac++; 
					yya[yyac] = new YYARec(289,-196 );yyac++; 
					yya[yyac] = new YYARec(290,-196 );yyac++; 
					yya[yyac] = new YYARec(291,-196 );yyac++; 
					yya[yyac] = new YYARec(292,-196 );yyac++; 
					yya[yyac] = new YYARec(293,-196 );yyac++; 
					yya[yyac] = new YYARec(297,-196 );yyac++; 
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
					yya[yyac] = new YYARec(303,-175 );yyac++; 
					yya[yyac] = new YYARec(304,-175 );yyac++; 
					yya[yyac] = new YYARec(305,-175 );yyac++; 
					yya[yyac] = new YYARec(306,-175 );yyac++; 
					yya[yyac] = new YYARec(307,-175 );yyac++; 
					yya[yyac] = new YYARec(308,-175 );yyac++; 
					yya[yyac] = new YYARec(309,-175 );yyac++; 
					yya[yyac] = new YYARec(310,-175 );yyac++; 
					yya[yyac] = new YYARec(311,-175 );yyac++; 
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
					yya[yyac] = new YYARec(326,-175 );yyac++; 
					yya[yyac] = new YYARec(268,-192 );yyac++; 
					yya[yyac] = new YYARec(289,-192 );yyac++; 
					yya[yyac] = new YYARec(290,-192 );yyac++; 
					yya[yyac] = new YYARec(291,-192 );yyac++; 
					yya[yyac] = new YYARec(292,-192 );yyac++; 
					yya[yyac] = new YYARec(293,-192 );yyac++; 
					yya[yyac] = new YYARec(297,-192 );yyac++; 
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
					yya[yyac] = new YYARec(303,-176 );yyac++; 
					yya[yyac] = new YYARec(304,-176 );yyac++; 
					yya[yyac] = new YYARec(305,-176 );yyac++; 
					yya[yyac] = new YYARec(306,-176 );yyac++; 
					yya[yyac] = new YYARec(307,-176 );yyac++; 
					yya[yyac] = new YYARec(308,-176 );yyac++; 
					yya[yyac] = new YYARec(309,-176 );yyac++; 
					yya[yyac] = new YYARec(310,-176 );yyac++; 
					yya[yyac] = new YYARec(311,-176 );yyac++; 
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
					yya[yyac] = new YYARec(326,-176 );yyac++; 
					yya[yyac] = new YYARec(268,-193 );yyac++; 
					yya[yyac] = new YYARec(289,-193 );yyac++; 
					yya[yyac] = new YYARec(290,-193 );yyac++; 
					yya[yyac] = new YYARec(291,-193 );yyac++; 
					yya[yyac] = new YYARec(292,-193 );yyac++; 
					yya[yyac] = new YYARec(293,-193 );yyac++; 
					yya[yyac] = new YYARec(297,-193 );yyac++; 
					yya[yyac] = new YYARec(258,244);yyac++; 
					yya[yyac] = new YYARec(263,-187 );yyac++; 
					yya[yyac] = new YYARec(268,-187 );yyac++; 
					yya[yyac] = new YYARec(282,-187 );yyac++; 
					yya[yyac] = new YYARec(283,-187 );yyac++; 
					yya[yyac] = new YYARec(287,-187 );yyac++; 
					yya[yyac] = new YYARec(289,-187 );yyac++; 
					yya[yyac] = new YYARec(290,-187 );yyac++; 
					yya[yyac] = new YYARec(291,-187 );yyac++; 
					yya[yyac] = new YYARec(292,-187 );yyac++; 
					yya[yyac] = new YYARec(293,-187 );yyac++; 
					yya[yyac] = new YYARec(297,-187 );yyac++; 
					yya[yyac] = new YYARec(298,-187 );yyac++; 
					yya[yyac] = new YYARec(299,-187 );yyac++; 
					yya[yyac] = new YYARec(300,-187 );yyac++; 
					yya[yyac] = new YYARec(301,-187 );yyac++; 
					yya[yyac] = new YYARec(302,-187 );yyac++; 
					yya[yyac] = new YYARec(303,-187 );yyac++; 
					yya[yyac] = new YYARec(304,-187 );yyac++; 
					yya[yyac] = new YYARec(305,-187 );yyac++; 
					yya[yyac] = new YYARec(306,-187 );yyac++; 
					yya[yyac] = new YYARec(307,-187 );yyac++; 
					yya[yyac] = new YYARec(308,-187 );yyac++; 
					yya[yyac] = new YYARec(309,-187 );yyac++; 
					yya[yyac] = new YYARec(310,-187 );yyac++; 
					yya[yyac] = new YYARec(311,-187 );yyac++; 
					yya[yyac] = new YYARec(312,-187 );yyac++; 
					yya[yyac] = new YYARec(313,-187 );yyac++; 
					yya[yyac] = new YYARec(314,-187 );yyac++; 
					yya[yyac] = new YYARec(315,-187 );yyac++; 
					yya[yyac] = new YYARec(316,-187 );yyac++; 
					yya[yyac] = new YYARec(317,-187 );yyac++; 
					yya[yyac] = new YYARec(318,-187 );yyac++; 
					yya[yyac] = new YYARec(319,-187 );yyac++; 
					yya[yyac] = new YYARec(320,-187 );yyac++; 
					yya[yyac] = new YYARec(321,-187 );yyac++; 
					yya[yyac] = new YYARec(322,-187 );yyac++; 
					yya[yyac] = new YYARec(323,-187 );yyac++; 
					yya[yyac] = new YYARec(324,-187 );yyac++; 
					yya[yyac] = new YYARec(325,-187 );yyac++; 
					yya[yyac] = new YYARec(326,-187 );yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(282,-98 );yyac++; 
					yya[yyac] = new YYARec(283,-98 );yyac++; 
					yya[yyac] = new YYARec(287,-98 );yyac++; 
					yya[yyac] = new YYARec(298,-98 );yyac++; 
					yya[yyac] = new YYARec(299,-98 );yyac++; 
					yya[yyac] = new YYARec(300,-98 );yyac++; 
					yya[yyac] = new YYARec(301,-98 );yyac++; 
					yya[yyac] = new YYARec(302,-98 );yyac++; 
					yya[yyac] = new YYARec(303,-98 );yyac++; 
					yya[yyac] = new YYARec(304,-98 );yyac++; 
					yya[yyac] = new YYARec(305,-98 );yyac++; 
					yya[yyac] = new YYARec(306,-98 );yyac++; 
					yya[yyac] = new YYARec(307,-98 );yyac++; 
					yya[yyac] = new YYARec(309,-98 );yyac++; 
					yya[yyac] = new YYARec(310,-98 );yyac++; 
					yya[yyac] = new YYARec(311,-98 );yyac++; 
					yya[yyac] = new YYARec(312,-98 );yyac++; 
					yya[yyac] = new YYARec(313,-98 );yyac++; 
					yya[yyac] = new YYARec(314,-98 );yyac++; 
					yya[yyac] = new YYARec(315,-98 );yyac++; 
					yya[yyac] = new YYARec(316,-98 );yyac++; 
					yya[yyac] = new YYARec(317,-98 );yyac++; 
					yya[yyac] = new YYARec(318,-98 );yyac++; 
					yya[yyac] = new YYARec(319,-98 );yyac++; 
					yya[yyac] = new YYARec(320,-98 );yyac++; 
					yya[yyac] = new YYARec(321,-98 );yyac++; 
					yya[yyac] = new YYARec(322,-98 );yyac++; 
					yya[yyac] = new YYARec(323,-98 );yyac++; 
					yya[yyac] = new YYARec(324,-98 );yyac++; 
					yya[yyac] = new YYARec(325,-98 );yyac++; 
					yya[yyac] = new YYARec(326,-98 );yyac++; 
					yya[yyac] = new YYARec(257,188);yyac++; 
					yya[yyac] = new YYARec(258,189);yyac++; 
					yya[yyac] = new YYARec(259,190);yyac++; 
					yya[yyac] = new YYARec(301,96);yyac++; 
					yya[yyac] = new YYARec(302,97);yyac++; 
					yya[yyac] = new YYARec(306,99);yyac++; 
					yya[yyac] = new YYARec(311,100);yyac++; 
					yya[yyac] = new YYARec(312,101);yyac++; 
					yya[yyac] = new YYARec(315,103);yyac++; 
					yya[yyac] = new YYARec(317,105);yyac++; 
					yya[yyac] = new YYARec(319,107);yyac++; 
					yya[yyac] = new YYARec(320,108);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(260,-66 );yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(267,-66 );yyac++; 
					yya[yyac] = new YYARec(267,247);yyac++; 
					yya[yyac] = new YYARec(257,188);yyac++; 
					yya[yyac] = new YYARec(258,189);yyac++; 
					yya[yyac] = new YYARec(259,190);yyac++; 
					yya[yyac] = new YYARec(301,96);yyac++; 
					yya[yyac] = new YYARec(302,97);yyac++; 
					yya[yyac] = new YYARec(306,99);yyac++; 
					yya[yyac] = new YYARec(311,100);yyac++; 
					yya[yyac] = new YYARec(312,101);yyac++; 
					yya[yyac] = new YYARec(315,103);yyac++; 
					yya[yyac] = new YYARec(317,105);yyac++; 
					yya[yyac] = new YYARec(319,107);yyac++; 
					yya[yyac] = new YYARec(320,108);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(260,-66 );yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(267,-66 );yyac++; 
					yya[yyac] = new YYARec(309,83);yyac++; 
					yya[yyac] = new YYARec(315,84);yyac++; 
					yya[yyac] = new YYARec(317,85);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(309,83);yyac++; 
					yya[yyac] = new YYARec(315,84);yyac++; 
					yya[yyac] = new YYARec(317,85);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(322,126);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(258,-54 );yyac++; 
					yya[yyac] = new YYARec(258,256);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,126);yyac++; 
					yya[yyac] = new YYARec(323,127);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,90);yyac++; 
					yya[yyac] = new YYARec(326,128);yyac++; 
					yya[yyac] = new YYARec(257,170);yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(259,172);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,173);yyac++; 
					yya[yyac] = new YYARec(288,174);yyac++; 
					yya[yyac] = new YYARec(294,175);yyac++; 
					yya[yyac] = new YYARec(295,176);yyac++; 
					yya[yyac] = new YYARec(296,177);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,178);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,179);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,180);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,181);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,182);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(267,-83 );yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,126);yyac++; 
					yya[yyac] = new YYARec(323,127);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,90);yyac++; 
					yya[yyac] = new YYARec(326,128);yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(257,170);yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(259,172);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,173);yyac++; 
					yya[yyac] = new YYARec(288,174);yyac++; 
					yya[yyac] = new YYARec(294,175);yyac++; 
					yya[yyac] = new YYARec(295,176);yyac++; 
					yya[yyac] = new YYARec(296,177);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,178);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,179);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,180);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,181);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,182);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(257,170);yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(259,172);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,173);yyac++; 
					yya[yyac] = new YYARec(288,174);yyac++; 
					yya[yyac] = new YYARec(294,175);yyac++; 
					yya[yyac] = new YYARec(295,176);yyac++; 
					yya[yyac] = new YYARec(296,177);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,178);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,179);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,180);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,181);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,182);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(267,270);yyac++; 
					yya[yyac] = new YYARec(274,271);yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(284,274);yyac++; 
					yya[yyac] = new YYARec(285,275);yyac++; 
					yya[yyac] = new YYARec(286,276);yyac++; 
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
					yya[yyac] = new YYARec(282,278);yyac++; 
					yya[yyac] = new YYARec(283,279);yyac++; 
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
					yya[yyac] = new YYARec(278,-112 );yyac++; 
					yya[yyac] = new YYARec(279,-112 );yyac++; 
					yya[yyac] = new YYARec(280,-112 );yyac++; 
					yya[yyac] = new YYARec(281,-112 );yyac++; 
					yya[yyac] = new YYARec(278,281);yyac++; 
					yya[yyac] = new YYARec(279,282);yyac++; 
					yya[yyac] = new YYARec(280,283);yyac++; 
					yya[yyac] = new YYARec(281,284);yyac++; 
					yya[yyac] = new YYARec(258,-110 );yyac++; 
					yya[yyac] = new YYARec(266,-110 );yyac++; 
					yya[yyac] = new YYARec(269,-110 );yyac++; 
					yya[yyac] = new YYARec(270,-110 );yyac++; 
					yya[yyac] = new YYARec(271,-110 );yyac++; 
					yya[yyac] = new YYARec(272,-110 );yyac++; 
					yya[yyac] = new YYARec(273,-110 );yyac++; 
					yya[yyac] = new YYARec(275,-110 );yyac++; 
					yya[yyac] = new YYARec(276,-110 );yyac++; 
					yya[yyac] = new YYARec(277,-110 );yyac++; 
					yya[yyac] = new YYARec(276,286);yyac++; 
					yya[yyac] = new YYARec(277,287);yyac++; 
					yya[yyac] = new YYARec(258,-109 );yyac++; 
					yya[yyac] = new YYARec(266,-109 );yyac++; 
					yya[yyac] = new YYARec(269,-109 );yyac++; 
					yya[yyac] = new YYARec(270,-109 );yyac++; 
					yya[yyac] = new YYARec(271,-109 );yyac++; 
					yya[yyac] = new YYARec(272,-109 );yyac++; 
					yya[yyac] = new YYARec(273,-109 );yyac++; 
					yya[yyac] = new YYARec(275,-109 );yyac++; 
					yya[yyac] = new YYARec(273,288);yyac++; 
					yya[yyac] = new YYARec(258,-107 );yyac++; 
					yya[yyac] = new YYARec(266,-107 );yyac++; 
					yya[yyac] = new YYARec(269,-107 );yyac++; 
					yya[yyac] = new YYARec(270,-107 );yyac++; 
					yya[yyac] = new YYARec(271,-107 );yyac++; 
					yya[yyac] = new YYARec(272,-107 );yyac++; 
					yya[yyac] = new YYARec(275,-107 );yyac++; 
					yya[yyac] = new YYARec(272,289);yyac++; 
					yya[yyac] = new YYARec(258,-105 );yyac++; 
					yya[yyac] = new YYARec(266,-105 );yyac++; 
					yya[yyac] = new YYARec(269,-105 );yyac++; 
					yya[yyac] = new YYARec(270,-105 );yyac++; 
					yya[yyac] = new YYARec(271,-105 );yyac++; 
					yya[yyac] = new YYARec(275,-105 );yyac++; 
					yya[yyac] = new YYARec(271,290);yyac++; 
					yya[yyac] = new YYARec(258,-103 );yyac++; 
					yya[yyac] = new YYARec(266,-103 );yyac++; 
					yya[yyac] = new YYARec(269,-103 );yyac++; 
					yya[yyac] = new YYARec(270,-103 );yyac++; 
					yya[yyac] = new YYARec(275,-103 );yyac++; 
					yya[yyac] = new YYARec(270,291);yyac++; 
					yya[yyac] = new YYARec(258,-101 );yyac++; 
					yya[yyac] = new YYARec(266,-101 );yyac++; 
					yya[yyac] = new YYARec(269,-101 );yyac++; 
					yya[yyac] = new YYARec(275,-101 );yyac++; 
					yya[yyac] = new YYARec(269,292);yyac++; 
					yya[yyac] = new YYARec(258,-99 );yyac++; 
					yya[yyac] = new YYARec(266,-99 );yyac++; 
					yya[yyac] = new YYARec(275,-99 );yyac++; 
					yya[yyac] = new YYARec(289,209);yyac++; 
					yya[yyac] = new YYARec(290,210);yyac++; 
					yya[yyac] = new YYARec(291,211);yyac++; 
					yya[yyac] = new YYARec(292,212);yyac++; 
					yya[yyac] = new YYARec(293,213);yyac++; 
					yya[yyac] = new YYARec(322,294);yyac++; 
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
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,-150 );yyac++; 
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
					yya[yyac] = new YYARec(322,-204 );yyac++; 
					yya[yyac] = new YYARec(274,-148 );yyac++; 
					yya[yyac] = new YYARec(258,-196 );yyac++; 
					yya[yyac] = new YYARec(266,-196 );yyac++; 
					yya[yyac] = new YYARec(269,-196 );yyac++; 
					yya[yyac] = new YYARec(270,-196 );yyac++; 
					yya[yyac] = new YYARec(271,-196 );yyac++; 
					yya[yyac] = new YYARec(272,-196 );yyac++; 
					yya[yyac] = new YYARec(273,-196 );yyac++; 
					yya[yyac] = new YYARec(275,-196 );yyac++; 
					yya[yyac] = new YYARec(276,-196 );yyac++; 
					yya[yyac] = new YYARec(277,-196 );yyac++; 
					yya[yyac] = new YYARec(278,-196 );yyac++; 
					yya[yyac] = new YYARec(279,-196 );yyac++; 
					yya[yyac] = new YYARec(280,-196 );yyac++; 
					yya[yyac] = new YYARec(281,-196 );yyac++; 
					yya[yyac] = new YYARec(282,-196 );yyac++; 
					yya[yyac] = new YYARec(283,-196 );yyac++; 
					yya[yyac] = new YYARec(284,-196 );yyac++; 
					yya[yyac] = new YYARec(285,-196 );yyac++; 
					yya[yyac] = new YYARec(286,-196 );yyac++; 
					yya[yyac] = new YYARec(289,-196 );yyac++; 
					yya[yyac] = new YYARec(290,-196 );yyac++; 
					yya[yyac] = new YYARec(291,-196 );yyac++; 
					yya[yyac] = new YYARec(292,-196 );yyac++; 
					yya[yyac] = new YYARec(293,-196 );yyac++; 
					yya[yyac] = new YYARec(297,-196 );yyac++; 
					yya[yyac] = new YYARec(322,-196 );yyac++; 
					yya[yyac] = new YYARec(274,-149 );yyac++; 
					yya[yyac] = new YYARec(258,-208 );yyac++; 
					yya[yyac] = new YYARec(266,-208 );yyac++; 
					yya[yyac] = new YYARec(269,-208 );yyac++; 
					yya[yyac] = new YYARec(270,-208 );yyac++; 
					yya[yyac] = new YYARec(271,-208 );yyac++; 
					yya[yyac] = new YYARec(272,-208 );yyac++; 
					yya[yyac] = new YYARec(273,-208 );yyac++; 
					yya[yyac] = new YYARec(275,-208 );yyac++; 
					yya[yyac] = new YYARec(276,-208 );yyac++; 
					yya[yyac] = new YYARec(277,-208 );yyac++; 
					yya[yyac] = new YYARec(278,-208 );yyac++; 
					yya[yyac] = new YYARec(279,-208 );yyac++; 
					yya[yyac] = new YYARec(280,-208 );yyac++; 
					yya[yyac] = new YYARec(281,-208 );yyac++; 
					yya[yyac] = new YYARec(282,-208 );yyac++; 
					yya[yyac] = new YYARec(283,-208 );yyac++; 
					yya[yyac] = new YYARec(284,-208 );yyac++; 
					yya[yyac] = new YYARec(285,-208 );yyac++; 
					yya[yyac] = new YYARec(286,-208 );yyac++; 
					yya[yyac] = new YYARec(289,-208 );yyac++; 
					yya[yyac] = new YYARec(290,-208 );yyac++; 
					yya[yyac] = new YYARec(291,-208 );yyac++; 
					yya[yyac] = new YYARec(292,-208 );yyac++; 
					yya[yyac] = new YYARec(293,-208 );yyac++; 
					yya[yyac] = new YYARec(297,-208 );yyac++; 
					yya[yyac] = new YYARec(322,-208 );yyac++; 
					yya[yyac] = new YYARec(257,170);yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(259,172);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,173);yyac++; 
					yya[yyac] = new YYARec(288,174);yyac++; 
					yya[yyac] = new YYARec(294,175);yyac++; 
					yya[yyac] = new YYARec(295,176);yyac++; 
					yya[yyac] = new YYARec(296,177);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,178);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,179);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,180);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,181);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,182);yyac++; 
					yya[yyac] = new YYARec(267,-83 );yyac++; 
					yya[yyac] = new YYARec(266,297);yyac++; 
					yya[yyac] = new YYARec(322,294);yyac++; 
					yya[yyac] = new YYARec(258,-124 );yyac++; 
					yya[yyac] = new YYARec(266,-124 );yyac++; 
					yya[yyac] = new YYARec(269,-124 );yyac++; 
					yya[yyac] = new YYARec(270,-124 );yyac++; 
					yya[yyac] = new YYARec(271,-124 );yyac++; 
					yya[yyac] = new YYARec(272,-124 );yyac++; 
					yya[yyac] = new YYARec(273,-124 );yyac++; 
					yya[yyac] = new YYARec(275,-124 );yyac++; 
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
					yya[yyac] = new YYARec(266,298);yyac++; 
					yya[yyac] = new YYARec(257,170);yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(259,172);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,173);yyac++; 
					yya[yyac] = new YYARec(288,174);yyac++; 
					yya[yyac] = new YYARec(294,175);yyac++; 
					yya[yyac] = new YYARec(295,176);yyac++; 
					yya[yyac] = new YYARec(296,177);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,178);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,179);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,180);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,181);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,182);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(267,-83 );yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,126);yyac++; 
					yya[yyac] = new YYARec(323,127);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,90);yyac++; 
					yya[yyac] = new YYARec(326,128);yyac++; 
					yya[yyac] = new YYARec(258,306);yyac++; 
					yya[yyac] = new YYARec(258,307);yyac++; 
					yya[yyac] = new YYARec(322,142);yyac++; 
					yya[yyac] = new YYARec(263,308);yyac++; 
					yya[yyac] = new YYARec(258,-53 );yyac++; 
					yya[yyac] = new YYARec(258,309);yyac++; 
					yya[yyac] = new YYARec(257,23);yyac++; 
					yya[yyac] = new YYARec(258,24);yyac++; 
					yya[yyac] = new YYARec(259,25);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(265,28);yyac++; 
					yya[yyac] = new YYARec(267,29);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,32);yyac++; 
					yya[yyac] = new YYARec(301,33);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,40);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(312,43);yyac++; 
					yya[yyac] = new YYARec(313,44);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,48);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,50);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(261,-4 );yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(258,-98 );yyac++; 
					yya[yyac] = new YYARec(282,-98 );yyac++; 
					yya[yyac] = new YYARec(283,-98 );yyac++; 
					yya[yyac] = new YYARec(287,-98 );yyac++; 
					yya[yyac] = new YYARec(298,-98 );yyac++; 
					yya[yyac] = new YYARec(299,-98 );yyac++; 
					yya[yyac] = new YYARec(300,-98 );yyac++; 
					yya[yyac] = new YYARec(301,-98 );yyac++; 
					yya[yyac] = new YYARec(302,-98 );yyac++; 
					yya[yyac] = new YYARec(303,-98 );yyac++; 
					yya[yyac] = new YYARec(304,-98 );yyac++; 
					yya[yyac] = new YYARec(305,-98 );yyac++; 
					yya[yyac] = new YYARec(306,-98 );yyac++; 
					yya[yyac] = new YYARec(307,-98 );yyac++; 
					yya[yyac] = new YYARec(308,-98 );yyac++; 
					yya[yyac] = new YYARec(309,-98 );yyac++; 
					yya[yyac] = new YYARec(310,-98 );yyac++; 
					yya[yyac] = new YYARec(311,-98 );yyac++; 
					yya[yyac] = new YYARec(312,-98 );yyac++; 
					yya[yyac] = new YYARec(313,-98 );yyac++; 
					yya[yyac] = new YYARec(314,-98 );yyac++; 
					yya[yyac] = new YYARec(315,-98 );yyac++; 
					yya[yyac] = new YYARec(316,-98 );yyac++; 
					yya[yyac] = new YYARec(317,-98 );yyac++; 
					yya[yyac] = new YYARec(318,-98 );yyac++; 
					yya[yyac] = new YYARec(319,-98 );yyac++; 
					yya[yyac] = new YYARec(320,-98 );yyac++; 
					yya[yyac] = new YYARec(321,-98 );yyac++; 
					yya[yyac] = new YYARec(322,-98 );yyac++; 
					yya[yyac] = new YYARec(323,-98 );yyac++; 
					yya[yyac] = new YYARec(324,-98 );yyac++; 
					yya[yyac] = new YYARec(325,-98 );yyac++; 
					yya[yyac] = new YYARec(326,-98 );yyac++; 
					yya[yyac] = new YYARec(258,312);yyac++; 
					yya[yyac] = new YYARec(258,313);yyac++; 
					yya[yyac] = new YYARec(260,314);yyac++; 
					yya[yyac] = new YYARec(261,315);yyac++; 
					yya[yyac] = new YYARec(257,170);yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(259,172);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,173);yyac++; 
					yya[yyac] = new YYARec(288,174);yyac++; 
					yya[yyac] = new YYARec(294,175);yyac++; 
					yya[yyac] = new YYARec(295,176);yyac++; 
					yya[yyac] = new YYARec(296,177);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,178);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,179);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,180);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,181);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,182);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(267,-83 );yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,235);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,236);yyac++; 
					yya[yyac] = new YYARec(315,237);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,238);yyac++; 
					yya[yyac] = new YYARec(323,239);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(275,328);yyac++; 
					yya[yyac] = new YYARec(267,329);yyac++; 
					yya[yyac] = new YYARec(257,170);yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(259,172);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,173);yyac++; 
					yya[yyac] = new YYARec(288,174);yyac++; 
					yya[yyac] = new YYARec(294,175);yyac++; 
					yya[yyac] = new YYARec(295,176);yyac++; 
					yya[yyac] = new YYARec(296,177);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,178);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,179);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,180);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,181);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,182);yyac++; 
					yya[yyac] = new YYARec(267,-83 );yyac++; 
					yya[yyac] = new YYARec(257,170);yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(259,172);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,173);yyac++; 
					yya[yyac] = new YYARec(288,174);yyac++; 
					yya[yyac] = new YYARec(294,175);yyac++; 
					yya[yyac] = new YYARec(295,176);yyac++; 
					yya[yyac] = new YYARec(296,177);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,178);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,179);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,180);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,181);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,182);yyac++; 
					yya[yyac] = new YYARec(267,-83 );yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(258,-98 );yyac++; 
					yya[yyac] = new YYARec(282,-98 );yyac++; 
					yya[yyac] = new YYARec(283,-98 );yyac++; 
					yya[yyac] = new YYARec(287,-98 );yyac++; 
					yya[yyac] = new YYARec(298,-98 );yyac++; 
					yya[yyac] = new YYARec(299,-98 );yyac++; 
					yya[yyac] = new YYARec(300,-98 );yyac++; 
					yya[yyac] = new YYARec(301,-98 );yyac++; 
					yya[yyac] = new YYARec(302,-98 );yyac++; 
					yya[yyac] = new YYARec(303,-98 );yyac++; 
					yya[yyac] = new YYARec(304,-98 );yyac++; 
					yya[yyac] = new YYARec(305,-98 );yyac++; 
					yya[yyac] = new YYARec(306,-98 );yyac++; 
					yya[yyac] = new YYARec(307,-98 );yyac++; 
					yya[yyac] = new YYARec(309,-98 );yyac++; 
					yya[yyac] = new YYARec(310,-98 );yyac++; 
					yya[yyac] = new YYARec(311,-98 );yyac++; 
					yya[yyac] = new YYARec(312,-98 );yyac++; 
					yya[yyac] = new YYARec(313,-98 );yyac++; 
					yya[yyac] = new YYARec(314,-98 );yyac++; 
					yya[yyac] = new YYARec(315,-98 );yyac++; 
					yya[yyac] = new YYARec(316,-98 );yyac++; 
					yya[yyac] = new YYARec(317,-98 );yyac++; 
					yya[yyac] = new YYARec(318,-98 );yyac++; 
					yya[yyac] = new YYARec(319,-98 );yyac++; 
					yya[yyac] = new YYARec(320,-98 );yyac++; 
					yya[yyac] = new YYARec(321,-98 );yyac++; 
					yya[yyac] = new YYARec(322,-98 );yyac++; 
					yya[yyac] = new YYARec(323,-98 );yyac++; 
					yya[yyac] = new YYARec(324,-98 );yyac++; 
					yya[yyac] = new YYARec(325,-98 );yyac++; 
					yya[yyac] = new YYARec(326,-98 );yyac++; 
					yya[yyac] = new YYARec(258,333);yyac++; 
					yya[yyac] = new YYARec(257,188);yyac++; 
					yya[yyac] = new YYARec(258,189);yyac++; 
					yya[yyac] = new YYARec(259,190);yyac++; 
					yya[yyac] = new YYARec(301,96);yyac++; 
					yya[yyac] = new YYARec(302,97);yyac++; 
					yya[yyac] = new YYARec(306,99);yyac++; 
					yya[yyac] = new YYARec(311,100);yyac++; 
					yya[yyac] = new YYARec(312,101);yyac++; 
					yya[yyac] = new YYARec(315,103);yyac++; 
					yya[yyac] = new YYARec(317,105);yyac++; 
					yya[yyac] = new YYARec(319,107);yyac++; 
					yya[yyac] = new YYARec(320,108);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(260,-66 );yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(257,188);yyac++; 
					yya[yyac] = new YYARec(258,189);yyac++; 
					yya[yyac] = new YYARec(259,190);yyac++; 
					yya[yyac] = new YYARec(301,96);yyac++; 
					yya[yyac] = new YYARec(302,97);yyac++; 
					yya[yyac] = new YYARec(306,99);yyac++; 
					yya[yyac] = new YYARec(311,100);yyac++; 
					yya[yyac] = new YYARec(312,101);yyac++; 
					yya[yyac] = new YYARec(315,103);yyac++; 
					yya[yyac] = new YYARec(317,105);yyac++; 
					yya[yyac] = new YYARec(319,107);yyac++; 
					yya[yyac] = new YYARec(320,108);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(260,-66 );yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(322,126);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(258,-54 );yyac++; 
					yya[yyac] = new YYARec(261,338);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,158);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,126);yyac++; 
					yya[yyac] = new YYARec(323,127);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,90);yyac++; 
					yya[yyac] = new YYARec(326,128);yyac++; 
					yya[yyac] = new YYARec(258,-90 );yyac++; 
					yya[yyac] = new YYARec(257,170);yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(259,172);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,173);yyac++; 
					yya[yyac] = new YYARec(288,174);yyac++; 
					yya[yyac] = new YYARec(294,175);yyac++; 
					yya[yyac] = new YYARec(295,176);yyac++; 
					yya[yyac] = new YYARec(296,177);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,178);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,179);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,180);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,181);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,182);yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(275,341);yyac++; 
					yya[yyac] = new YYARec(284,274);yyac++; 
					yya[yyac] = new YYARec(285,275);yyac++; 
					yya[yyac] = new YYARec(286,276);yyac++; 
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
					yya[yyac] = new YYARec(282,278);yyac++; 
					yya[yyac] = new YYARec(283,279);yyac++; 
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
					yya[yyac] = new YYARec(278,281);yyac++; 
					yya[yyac] = new YYARec(279,282);yyac++; 
					yya[yyac] = new YYARec(280,283);yyac++; 
					yya[yyac] = new YYARec(281,284);yyac++; 
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
					yya[yyac] = new YYARec(276,286);yyac++; 
					yya[yyac] = new YYARec(277,287);yyac++; 
					yya[yyac] = new YYARec(258,-108 );yyac++; 
					yya[yyac] = new YYARec(266,-108 );yyac++; 
					yya[yyac] = new YYARec(269,-108 );yyac++; 
					yya[yyac] = new YYARec(270,-108 );yyac++; 
					yya[yyac] = new YYARec(271,-108 );yyac++; 
					yya[yyac] = new YYARec(272,-108 );yyac++; 
					yya[yyac] = new YYARec(273,-108 );yyac++; 
					yya[yyac] = new YYARec(275,-108 );yyac++; 
					yya[yyac] = new YYARec(273,288);yyac++; 
					yya[yyac] = new YYARec(258,-106 );yyac++; 
					yya[yyac] = new YYARec(266,-106 );yyac++; 
					yya[yyac] = new YYARec(269,-106 );yyac++; 
					yya[yyac] = new YYARec(270,-106 );yyac++; 
					yya[yyac] = new YYARec(271,-106 );yyac++; 
					yya[yyac] = new YYARec(272,-106 );yyac++; 
					yya[yyac] = new YYARec(275,-106 );yyac++; 
					yya[yyac] = new YYARec(272,289);yyac++; 
					yya[yyac] = new YYARec(258,-104 );yyac++; 
					yya[yyac] = new YYARec(266,-104 );yyac++; 
					yya[yyac] = new YYARec(269,-104 );yyac++; 
					yya[yyac] = new YYARec(270,-104 );yyac++; 
					yya[yyac] = new YYARec(271,-104 );yyac++; 
					yya[yyac] = new YYARec(275,-104 );yyac++; 
					yya[yyac] = new YYARec(271,290);yyac++; 
					yya[yyac] = new YYARec(258,-102 );yyac++; 
					yya[yyac] = new YYARec(266,-102 );yyac++; 
					yya[yyac] = new YYARec(269,-102 );yyac++; 
					yya[yyac] = new YYARec(270,-102 );yyac++; 
					yya[yyac] = new YYARec(275,-102 );yyac++; 
					yya[yyac] = new YYARec(270,291);yyac++; 
					yya[yyac] = new YYARec(258,-100 );yyac++; 
					yya[yyac] = new YYARec(266,-100 );yyac++; 
					yya[yyac] = new YYARec(269,-100 );yyac++; 
					yya[yyac] = new YYARec(275,-100 );yyac++; 
					yya[yyac] = new YYARec(267,342);yyac++; 
					yya[yyac] = new YYARec(267,343);yyac++; 
					yya[yyac] = new YYARec(282,123);yyac++; 
					yya[yyac] = new YYARec(283,124);yyac++; 
					yya[yyac] = new YYARec(287,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,58);yyac++; 
					yya[yyac] = new YYARec(300,59);yyac++; 
					yya[yyac] = new YYARec(301,60);yyac++; 
					yya[yyac] = new YYARec(302,61);yyac++; 
					yya[yyac] = new YYARec(303,62);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,64);yyac++; 
					yya[yyac] = new YYARec(312,65);yyac++; 
					yya[yyac] = new YYARec(313,66);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,67);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,68);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,69);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,126);yyac++; 
					yya[yyac] = new YYARec(323,127);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,90);yyac++; 
					yya[yyac] = new YYARec(326,128);yyac++; 
					yya[yyac] = new YYARec(258,-69 );yyac++; 
					yya[yyac] = new YYARec(260,345);yyac++; 
					yya[yyac] = new YYARec(261,346);yyac++; 
					yya[yyac] = new YYARec(258,347);yyac++; 
					yya[yyac] = new YYARec(258,348);yyac++; 
					yya[yyac] = new YYARec(261,349);yyac++; 
					yya[yyac] = new YYARec(258,350);yyac++; 
					yya[yyac] = new YYARec(257,188);yyac++; 
					yya[yyac] = new YYARec(258,189);yyac++; 
					yya[yyac] = new YYARec(259,190);yyac++; 
					yya[yyac] = new YYARec(301,96);yyac++; 
					yya[yyac] = new YYARec(302,97);yyac++; 
					yya[yyac] = new YYARec(306,99);yyac++; 
					yya[yyac] = new YYARec(311,100);yyac++; 
					yya[yyac] = new YYARec(312,101);yyac++; 
					yya[yyac] = new YYARec(315,103);yyac++; 
					yya[yyac] = new YYARec(317,105);yyac++; 
					yya[yyac] = new YYARec(319,107);yyac++; 
					yya[yyac] = new YYARec(320,108);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(261,352);yyac++;

					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-46,4);yygc++; 
					yyg[yygc] = new YYARec(-41,5);yygc++; 
					yyg[yygc] = new YYARec(-40,6);yygc++; 
					yyg[yygc] = new YYARec(-35,7);yygc++; 
					yyg[yygc] = new YYARec(-32,8);yygc++; 
					yyg[yygc] = new YYARec(-29,9);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-22,11);yygc++; 
					yyg[yygc] = new YYARec(-12,12);yygc++; 
					yyg[yygc] = new YYARec(-11,13);yygc++; 
					yyg[yygc] = new YYARec(-10,14);yygc++; 
					yyg[yygc] = new YYARec(-9,15);yygc++; 
					yyg[yygc] = new YYARec(-8,16);yygc++; 
					yyg[yygc] = new YYARec(-7,17);yygc++; 
					yyg[yygc] = new YYARec(-6,18);yygc++; 
					yyg[yygc] = new YYARec(-5,19);yygc++; 
					yyg[yygc] = new YYARec(-4,20);yygc++; 
					yyg[yygc] = new YYARec(-3,21);yygc++; 
					yyg[yygc] = new YYARec(-2,22);yygc++; 
					yyg[yygc] = new YYARec(-80,55);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,56);yygc++; 
					yyg[yygc] = new YYARec(-47,57);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-80,71);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,56);yygc++; 
					yyg[yygc] = new YYARec(-34,72);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-80,73);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,56);yygc++; 
					yyg[yygc] = new YYARec(-36,74);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-30,76);yygc++; 
					yyg[yygc] = new YYARec(-30,78);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-46,4);yygc++; 
					yyg[yygc] = new YYARec(-41,5);yygc++; 
					yyg[yygc] = new YYARec(-40,6);yygc++; 
					yyg[yygc] = new YYARec(-35,7);yygc++; 
					yyg[yygc] = new YYARec(-32,8);yygc++; 
					yyg[yygc] = new YYARec(-29,9);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-22,11);yygc++; 
					yyg[yygc] = new YYARec(-12,12);yygc++; 
					yyg[yygc] = new YYARec(-11,13);yygc++; 
					yyg[yygc] = new YYARec(-10,14);yygc++; 
					yyg[yygc] = new YYARec(-9,15);yygc++; 
					yyg[yygc] = new YYARec(-8,16);yygc++; 
					yyg[yygc] = new YYARec(-7,17);yygc++; 
					yyg[yygc] = new YYARec(-6,18);yygc++; 
					yyg[yygc] = new YYARec(-5,19);yygc++; 
					yyg[yygc] = new YYARec(-4,20);yygc++; 
					yyg[yygc] = new YYARec(-3,79);yygc++; 
					yyg[yygc] = new YYARec(-23,80);yygc++; 
					yyg[yygc] = new YYARec(-22,81);yygc++; 
					yyg[yygc] = new YYARec(-13,82);yygc++; 
					yyg[yygc] = new YYARec(-23,80);yygc++; 
					yyg[yygc] = new YYARec(-22,81);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-23,80);yygc++; 
					yyg[yygc] = new YYARec(-22,81);yygc++; 
					yyg[yygc] = new YYARec(-13,87);yygc++; 
					yyg[yygc] = new YYARec(-23,80);yygc++; 
					yyg[yygc] = new YYARec(-22,81);yygc++; 
					yyg[yygc] = new YYARec(-13,88);yygc++; 
					yyg[yygc] = new YYARec(-26,89);yygc++; 
					yyg[yygc] = new YYARec(-76,91);yygc++; 
					yyg[yygc] = new YYARec(-43,92);yygc++; 
					yyg[yygc] = new YYARec(-41,93);yygc++; 
					yyg[yygc] = new YYARec(-35,94);yygc++; 
					yyg[yygc] = new YYARec(-23,95);yygc++; 
					yyg[yygc] = new YYARec(-30,110);yygc++; 
					yyg[yygc] = new YYARec(-30,112);yygc++; 
					yyg[yygc] = new YYARec(-30,114);yygc++; 
					yyg[yygc] = new YYARec(-77,115);yygc++; 
					yyg[yygc] = new YYARec(-71,116);yygc++; 
					yyg[yygc] = new YYARec(-39,117);yygc++; 
					yyg[yygc] = new YYARec(-33,118);yygc++; 
					yyg[yygc] = new YYARec(-27,119);yygc++; 
					yyg[yygc] = new YYARec(-26,120);yygc++; 
					yyg[yygc] = new YYARec(-24,121);yygc++; 
					yyg[yygc] = new YYARec(-23,122);yygc++; 
					yyg[yygc] = new YYARec(-80,71);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,56);yygc++; 
					yyg[yygc] = new YYARec(-34,129);yygc++; 
					yyg[yygc] = new YYARec(-31,130);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-24,139);yygc++; 
					yyg[yygc] = new YYARec(-26,141);yygc++; 
					yyg[yygc] = new YYARec(-30,145);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-46,4);yygc++; 
					yyg[yygc] = new YYARec(-41,5);yygc++; 
					yyg[yygc] = new YYARec(-40,6);yygc++; 
					yyg[yygc] = new YYARec(-35,7);yygc++; 
					yyg[yygc] = new YYARec(-32,8);yygc++; 
					yyg[yygc] = new YYARec(-29,9);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-22,11);yygc++; 
					yyg[yygc] = new YYARec(-14,147);yygc++; 
					yyg[yygc] = new YYARec(-12,12);yygc++; 
					yyg[yygc] = new YYARec(-11,13);yygc++; 
					yyg[yygc] = new YYARec(-10,14);yygc++; 
					yyg[yygc] = new YYARec(-9,15);yygc++; 
					yyg[yygc] = new YYARec(-8,16);yygc++; 
					yyg[yygc] = new YYARec(-7,17);yygc++; 
					yyg[yygc] = new YYARec(-6,18);yygc++; 
					yyg[yygc] = new YYARec(-5,19);yygc++; 
					yyg[yygc] = new YYARec(-4,20);yygc++; 
					yyg[yygc] = new YYARec(-3,148);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-46,4);yygc++; 
					yyg[yygc] = new YYARec(-41,5);yygc++; 
					yyg[yygc] = new YYARec(-40,6);yygc++; 
					yyg[yygc] = new YYARec(-35,7);yygc++; 
					yyg[yygc] = new YYARec(-32,8);yygc++; 
					yyg[yygc] = new YYARec(-29,9);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-22,11);yygc++; 
					yyg[yygc] = new YYARec(-14,149);yygc++; 
					yyg[yygc] = new YYARec(-12,12);yygc++; 
					yyg[yygc] = new YYARec(-11,13);yygc++; 
					yyg[yygc] = new YYARec(-10,14);yygc++; 
					yyg[yygc] = new YYARec(-9,15);yygc++; 
					yyg[yygc] = new YYARec(-8,16);yygc++; 
					yyg[yygc] = new YYARec(-7,17);yygc++; 
					yyg[yygc] = new YYARec(-6,18);yygc++; 
					yyg[yygc] = new YYARec(-5,19);yygc++; 
					yyg[yygc] = new YYARec(-4,20);yygc++; 
					yyg[yygc] = new YYARec(-3,148);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-77,115);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-71,150);yygc++; 
					yyg[yygc] = new YYARec(-39,117);yygc++; 
					yyg[yygc] = new YYARec(-28,151);yygc++; 
					yyg[yygc] = new YYARec(-27,152);yygc++; 
					yyg[yygc] = new YYARec(-26,153);yygc++; 
					yyg[yygc] = new YYARec(-25,154);yygc++; 
					yyg[yygc] = new YYARec(-24,155);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-21,156);yygc++; 
					yyg[yygc] = new YYARec(-5,157);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,159);yygc++; 
					yyg[yygc] = new YYARec(-53,160);yygc++; 
					yyg[yygc] = new YYARec(-51,161);yygc++; 
					yyg[yygc] = new YYARec(-50,162);yygc++; 
					yyg[yygc] = new YYARec(-49,163);yygc++; 
					yyg[yygc] = new YYARec(-48,164);yygc++; 
					yyg[yygc] = new YYARec(-23,165);yygc++; 
					yyg[yygc] = new YYARec(-17,166);yygc++; 
					yyg[yygc] = new YYARec(-15,167);yygc++; 
					yyg[yygc] = new YYARec(-11,168);yygc++; 
					yyg[yygc] = new YYARec(-5,169);yygc++; 
					yyg[yygc] = new YYARec(-43,184);yygc++; 
					yyg[yygc] = new YYARec(-42,185);yygc++; 
					yyg[yygc] = new YYARec(-41,93);yygc++; 
					yyg[yygc] = new YYARec(-35,94);yygc++; 
					yyg[yygc] = new YYARec(-23,95);yygc++; 
					yyg[yygc] = new YYARec(-20,186);yygc++; 
					yyg[yygc] = new YYARec(-18,187);yygc++; 
					yyg[yygc] = new YYARec(-30,191);yygc++; 
					yyg[yygc] = new YYARec(-80,71);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,56);yygc++; 
					yyg[yygc] = new YYARec(-34,129);yygc++; 
					yyg[yygc] = new YYARec(-31,192);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,197);yygc++; 
					yyg[yygc] = new YYARec(-30,200);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,159);yygc++; 
					yyg[yygc] = new YYARec(-53,160);yygc++; 
					yyg[yygc] = new YYARec(-51,161);yygc++; 
					yyg[yygc] = new YYARec(-50,162);yygc++; 
					yyg[yygc] = new YYARec(-49,163);yygc++; 
					yyg[yygc] = new YYARec(-48,164);yygc++; 
					yyg[yygc] = new YYARec(-23,165);yygc++; 
					yyg[yygc] = new YYARec(-17,202);yygc++; 
					yyg[yygc] = new YYARec(-15,167);yygc++; 
					yyg[yygc] = new YYARec(-11,168);yygc++; 
					yyg[yygc] = new YYARec(-5,169);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,159);yygc++; 
					yyg[yygc] = new YYARec(-53,160);yygc++; 
					yyg[yygc] = new YYARec(-51,161);yygc++; 
					yyg[yygc] = new YYARec(-50,162);yygc++; 
					yyg[yygc] = new YYARec(-49,163);yygc++; 
					yyg[yygc] = new YYARec(-48,164);yygc++; 
					yyg[yygc] = new YYARec(-23,165);yygc++; 
					yyg[yygc] = new YYARec(-17,203);yygc++; 
					yyg[yygc] = new YYARec(-15,167);yygc++; 
					yyg[yygc] = new YYARec(-11,168);yygc++; 
					yyg[yygc] = new YYARec(-5,169);yygc++; 
					yyg[yygc] = new YYARec(-30,205);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,159);yygc++; 
					yyg[yygc] = new YYARec(-53,160);yygc++; 
					yyg[yygc] = new YYARec(-51,161);yygc++; 
					yyg[yygc] = new YYARec(-50,162);yygc++; 
					yyg[yygc] = new YYARec(-49,163);yygc++; 
					yyg[yygc] = new YYARec(-48,164);yygc++; 
					yyg[yygc] = new YYARec(-23,165);yygc++; 
					yyg[yygc] = new YYARec(-17,207);yygc++; 
					yyg[yygc] = new YYARec(-15,167);yygc++; 
					yyg[yygc] = new YYARec(-11,168);yygc++; 
					yyg[yygc] = new YYARec(-5,169);yygc++; 
					yyg[yygc] = new YYARec(-74,208);yygc++; 
					yyg[yygc] = new YYARec(-23,80);yygc++; 
					yyg[yygc] = new YYARec(-22,81);yygc++; 
					yyg[yygc] = new YYARec(-13,214);yygc++; 
					yyg[yygc] = new YYARec(-23,80);yygc++; 
					yyg[yygc] = new YYARec(-22,81);yygc++; 
					yyg[yygc] = new YYARec(-13,215);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,159);yygc++; 
					yyg[yygc] = new YYARec(-53,160);yygc++; 
					yyg[yygc] = new YYARec(-51,161);yygc++; 
					yyg[yygc] = new YYARec(-50,162);yygc++; 
					yyg[yygc] = new YYARec(-49,163);yygc++; 
					yyg[yygc] = new YYARec(-48,164);yygc++; 
					yyg[yygc] = new YYARec(-23,165);yygc++; 
					yyg[yygc] = new YYARec(-17,216);yygc++; 
					yyg[yygc] = new YYARec(-15,167);yygc++; 
					yyg[yygc] = new YYARec(-11,168);yygc++; 
					yyg[yygc] = new YYARec(-5,169);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,221);yygc++; 
					yyg[yygc] = new YYARec(-66,222);yygc++; 
					yyg[yygc] = new YYARec(-64,223);yygc++; 
					yyg[yygc] = new YYARec(-62,224);yygc++; 
					yyg[yygc] = new YYARec(-61,225);yygc++; 
					yyg[yygc] = new YYARec(-60,226);yygc++; 
					yyg[yygc] = new YYARec(-59,227);yygc++; 
					yyg[yygc] = new YYARec(-58,228);yygc++; 
					yyg[yygc] = new YYARec(-57,229);yygc++; 
					yyg[yygc] = new YYARec(-56,230);yygc++; 
					yyg[yygc] = new YYARec(-55,231);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,233);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,221);yygc++; 
					yyg[yygc] = new YYARec(-66,222);yygc++; 
					yyg[yygc] = new YYARec(-64,223);yygc++; 
					yyg[yygc] = new YYARec(-62,224);yygc++; 
					yyg[yygc] = new YYARec(-61,225);yygc++; 
					yyg[yygc] = new YYARec(-60,226);yygc++; 
					yyg[yygc] = new YYARec(-59,227);yygc++; 
					yyg[yygc] = new YYARec(-58,228);yygc++; 
					yyg[yygc] = new YYARec(-57,229);yygc++; 
					yyg[yygc] = new YYARec(-56,230);yygc++; 
					yyg[yygc] = new YYARec(-55,241);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,221);yygc++; 
					yyg[yygc] = new YYARec(-66,222);yygc++; 
					yyg[yygc] = new YYARec(-64,223);yygc++; 
					yyg[yygc] = new YYARec(-62,224);yygc++; 
					yyg[yygc] = new YYARec(-61,225);yygc++; 
					yyg[yygc] = new YYARec(-60,226);yygc++; 
					yyg[yygc] = new YYARec(-59,227);yygc++; 
					yyg[yygc] = new YYARec(-58,228);yygc++; 
					yyg[yygc] = new YYARec(-57,229);yygc++; 
					yyg[yygc] = new YYARec(-56,230);yygc++; 
					yyg[yygc] = new YYARec(-55,243);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-30,245);yygc++; 
					yyg[yygc] = new YYARec(-43,184);yygc++; 
					yyg[yygc] = new YYARec(-42,185);yygc++; 
					yyg[yygc] = new YYARec(-41,93);yygc++; 
					yyg[yygc] = new YYARec(-35,94);yygc++; 
					yyg[yygc] = new YYARec(-23,95);yygc++; 
					yyg[yygc] = new YYARec(-20,246);yygc++; 
					yyg[yygc] = new YYARec(-18,187);yygc++; 
					yyg[yygc] = new YYARec(-43,184);yygc++; 
					yyg[yygc] = new YYARec(-42,185);yygc++; 
					yyg[yygc] = new YYARec(-41,93);yygc++; 
					yyg[yygc] = new YYARec(-35,94);yygc++; 
					yyg[yygc] = new YYARec(-23,95);yygc++; 
					yyg[yygc] = new YYARec(-20,248);yygc++; 
					yyg[yygc] = new YYARec(-18,187);yygc++; 
					yyg[yygc] = new YYARec(-23,80);yygc++; 
					yyg[yygc] = new YYARec(-22,81);yygc++; 
					yyg[yygc] = new YYARec(-13,249);yygc++; 
					yyg[yygc] = new YYARec(-23,80);yygc++; 
					yyg[yygc] = new YYARec(-22,81);yygc++; 
					yyg[yygc] = new YYARec(-13,250);yygc++; 
					yyg[yygc] = new YYARec(-71,251);yygc++; 
					yyg[yygc] = new YYARec(-39,252);yygc++; 
					yyg[yygc] = new YYARec(-38,253);yygc++; 
					yyg[yygc] = new YYARec(-37,254);yygc++; 
					yyg[yygc] = new YYARec(-23,255);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-77,115);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-71,150);yygc++; 
					yyg[yygc] = new YYARec(-54,257);yygc++; 
					yyg[yygc] = new YYARec(-52,258);yygc++; 
					yyg[yygc] = new YYARec(-39,117);yygc++; 
					yyg[yygc] = new YYARec(-28,259);yygc++; 
					yyg[yygc] = new YYARec(-27,260);yygc++; 
					yyg[yygc] = new YYARec(-26,261);yygc++; 
					yyg[yygc] = new YYARec(-25,262);yygc++; 
					yyg[yygc] = new YYARec(-24,263);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,157);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,159);yygc++; 
					yyg[yygc] = new YYARec(-53,160);yygc++; 
					yyg[yygc] = new YYARec(-51,161);yygc++; 
					yyg[yygc] = new YYARec(-50,162);yygc++; 
					yyg[yygc] = new YYARec(-49,163);yygc++; 
					yyg[yygc] = new YYARec(-48,164);yygc++; 
					yyg[yygc] = new YYARec(-23,165);yygc++; 
					yyg[yygc] = new YYARec(-17,264);yygc++; 
					yyg[yygc] = new YYARec(-15,167);yygc++; 
					yyg[yygc] = new YYARec(-11,168);yygc++; 
					yyg[yygc] = new YYARec(-5,169);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-77,115);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-71,150);yygc++; 
					yyg[yygc] = new YYARec(-54,257);yygc++; 
					yyg[yygc] = new YYARec(-52,265);yygc++; 
					yyg[yygc] = new YYARec(-39,117);yygc++; 
					yyg[yygc] = new YYARec(-28,259);yygc++; 
					yyg[yygc] = new YYARec(-27,260);yygc++; 
					yyg[yygc] = new YYARec(-26,261);yygc++; 
					yyg[yygc] = new YYARec(-25,262);yygc++; 
					yyg[yygc] = new YYARec(-24,263);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,157);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,221);yygc++; 
					yyg[yygc] = new YYARec(-66,222);yygc++; 
					yyg[yygc] = new YYARec(-64,223);yygc++; 
					yyg[yygc] = new YYARec(-62,224);yygc++; 
					yyg[yygc] = new YYARec(-61,225);yygc++; 
					yyg[yygc] = new YYARec(-60,226);yygc++; 
					yyg[yygc] = new YYARec(-59,227);yygc++; 
					yyg[yygc] = new YYARec(-58,228);yygc++; 
					yyg[yygc] = new YYARec(-57,229);yygc++; 
					yyg[yygc] = new YYARec(-56,230);yygc++; 
					yyg[yygc] = new YYARec(-55,266);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,159);yygc++; 
					yyg[yygc] = new YYARec(-53,160);yygc++; 
					yyg[yygc] = new YYARec(-51,161);yygc++; 
					yyg[yygc] = new YYARec(-50,162);yygc++; 
					yyg[yygc] = new YYARec(-49,163);yygc++; 
					yyg[yygc] = new YYARec(-48,164);yygc++; 
					yyg[yygc] = new YYARec(-23,165);yygc++; 
					yyg[yygc] = new YYARec(-17,267);yygc++; 
					yyg[yygc] = new YYARec(-16,268);yygc++; 
					yyg[yygc] = new YYARec(-15,167);yygc++; 
					yyg[yygc] = new YYARec(-11,168);yygc++; 
					yyg[yygc] = new YYARec(-5,169);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,159);yygc++; 
					yyg[yygc] = new YYARec(-53,160);yygc++; 
					yyg[yygc] = new YYARec(-51,161);yygc++; 
					yyg[yygc] = new YYARec(-50,162);yygc++; 
					yyg[yygc] = new YYARec(-49,163);yygc++; 
					yyg[yygc] = new YYARec(-48,164);yygc++; 
					yyg[yygc] = new YYARec(-23,165);yygc++; 
					yyg[yygc] = new YYARec(-17,267);yygc++; 
					yyg[yygc] = new YYARec(-16,269);yygc++; 
					yyg[yygc] = new YYARec(-15,167);yygc++; 
					yyg[yygc] = new YYARec(-11,168);yygc++; 
					yyg[yygc] = new YYARec(-5,169);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,272);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-69,273);yygc++; 
					yyg[yygc] = new YYARec(-67,277);yygc++; 
					yyg[yygc] = new YYARec(-65,280);yygc++; 
					yyg[yygc] = new YYARec(-63,285);yygc++; 
					yyg[yygc] = new YYARec(-74,293);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,221);yygc++; 
					yyg[yygc] = new YYARec(-66,222);yygc++; 
					yyg[yygc] = new YYARec(-64,223);yygc++; 
					yyg[yygc] = new YYARec(-62,224);yygc++; 
					yyg[yygc] = new YYARec(-61,225);yygc++; 
					yyg[yygc] = new YYARec(-60,226);yygc++; 
					yyg[yygc] = new YYARec(-59,227);yygc++; 
					yyg[yygc] = new YYARec(-58,228);yygc++; 
					yyg[yygc] = new YYARec(-57,229);yygc++; 
					yyg[yygc] = new YYARec(-56,230);yygc++; 
					yyg[yygc] = new YYARec(-55,295);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,159);yygc++; 
					yyg[yygc] = new YYARec(-53,160);yygc++; 
					yyg[yygc] = new YYARec(-51,161);yygc++; 
					yyg[yygc] = new YYARec(-50,162);yygc++; 
					yyg[yygc] = new YYARec(-49,163);yygc++; 
					yyg[yygc] = new YYARec(-48,164);yygc++; 
					yyg[yygc] = new YYARec(-23,165);yygc++; 
					yyg[yygc] = new YYARec(-17,296);yygc++; 
					yyg[yygc] = new YYARec(-15,167);yygc++; 
					yyg[yygc] = new YYARec(-11,168);yygc++; 
					yyg[yygc] = new YYARec(-5,169);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,159);yygc++; 
					yyg[yygc] = new YYARec(-53,160);yygc++; 
					yyg[yygc] = new YYARec(-51,161);yygc++; 
					yyg[yygc] = new YYARec(-50,162);yygc++; 
					yyg[yygc] = new YYARec(-49,163);yygc++; 
					yyg[yygc] = new YYARec(-48,164);yygc++; 
					yyg[yygc] = new YYARec(-23,165);yygc++; 
					yyg[yygc] = new YYARec(-17,299);yygc++; 
					yyg[yygc] = new YYARec(-15,167);yygc++; 
					yyg[yygc] = new YYARec(-11,168);yygc++; 
					yyg[yygc] = new YYARec(-5,169);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-77,115);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-71,150);yygc++; 
					yyg[yygc] = new YYARec(-45,300);yygc++; 
					yyg[yygc] = new YYARec(-44,301);yygc++; 
					yyg[yygc] = new YYARec(-39,117);yygc++; 
					yyg[yygc] = new YYARec(-28,302);yygc++; 
					yyg[yygc] = new YYARec(-27,303);yygc++; 
					yyg[yygc] = new YYARec(-26,304);yygc++; 
					yyg[yygc] = new YYARec(-24,305);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,157);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-46,4);yygc++; 
					yyg[yygc] = new YYARec(-41,5);yygc++; 
					yyg[yygc] = new YYARec(-40,6);yygc++; 
					yyg[yygc] = new YYARec(-35,7);yygc++; 
					yyg[yygc] = new YYARec(-32,8);yygc++; 
					yyg[yygc] = new YYARec(-29,9);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-22,11);yygc++; 
					yyg[yygc] = new YYARec(-12,12);yygc++; 
					yyg[yygc] = new YYARec(-11,13);yygc++; 
					yyg[yygc] = new YYARec(-10,14);yygc++; 
					yyg[yygc] = new YYARec(-9,15);yygc++; 
					yyg[yygc] = new YYARec(-8,16);yygc++; 
					yyg[yygc] = new YYARec(-7,17);yygc++; 
					yyg[yygc] = new YYARec(-6,18);yygc++; 
					yyg[yygc] = new YYARec(-5,19);yygc++; 
					yyg[yygc] = new YYARec(-4,20);yygc++; 
					yyg[yygc] = new YYARec(-3,310);yygc++; 
					yyg[yygc] = new YYARec(-30,311);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,159);yygc++; 
					yyg[yygc] = new YYARec(-53,160);yygc++; 
					yyg[yygc] = new YYARec(-51,161);yygc++; 
					yyg[yygc] = new YYARec(-50,162);yygc++; 
					yyg[yygc] = new YYARec(-49,163);yygc++; 
					yyg[yygc] = new YYARec(-48,164);yygc++; 
					yyg[yygc] = new YYARec(-23,165);yygc++; 
					yyg[yygc] = new YYARec(-17,316);yygc++; 
					yyg[yygc] = new YYARec(-15,167);yygc++; 
					yyg[yygc] = new YYARec(-11,168);yygc++; 
					yyg[yygc] = new YYARec(-5,169);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,221);yygc++; 
					yyg[yygc] = new YYARec(-66,222);yygc++; 
					yyg[yygc] = new YYARec(-64,223);yygc++; 
					yyg[yygc] = new YYARec(-62,224);yygc++; 
					yyg[yygc] = new YYARec(-61,225);yygc++; 
					yyg[yygc] = new YYARec(-60,226);yygc++; 
					yyg[yygc] = new YYARec(-59,227);yygc++; 
					yyg[yygc] = new YYARec(-58,228);yygc++; 
					yyg[yygc] = new YYARec(-57,229);yygc++; 
					yyg[yygc] = new YYARec(-56,230);yygc++; 
					yyg[yygc] = new YYARec(-55,317);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,318);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,221);yygc++; 
					yyg[yygc] = new YYARec(-66,319);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,221);yygc++; 
					yyg[yygc] = new YYARec(-66,222);yygc++; 
					yyg[yygc] = new YYARec(-64,320);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,221);yygc++; 
					yyg[yygc] = new YYARec(-66,222);yygc++; 
					yyg[yygc] = new YYARec(-64,223);yygc++; 
					yyg[yygc] = new YYARec(-62,321);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,221);yygc++; 
					yyg[yygc] = new YYARec(-66,222);yygc++; 
					yyg[yygc] = new YYARec(-64,223);yygc++; 
					yyg[yygc] = new YYARec(-62,224);yygc++; 
					yyg[yygc] = new YYARec(-61,322);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,221);yygc++; 
					yyg[yygc] = new YYARec(-66,222);yygc++; 
					yyg[yygc] = new YYARec(-64,223);yygc++; 
					yyg[yygc] = new YYARec(-62,224);yygc++; 
					yyg[yygc] = new YYARec(-61,225);yygc++; 
					yyg[yygc] = new YYARec(-60,323);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,221);yygc++; 
					yyg[yygc] = new YYARec(-66,222);yygc++; 
					yyg[yygc] = new YYARec(-64,223);yygc++; 
					yyg[yygc] = new YYARec(-62,224);yygc++; 
					yyg[yygc] = new YYARec(-61,225);yygc++; 
					yyg[yygc] = new YYARec(-60,226);yygc++; 
					yyg[yygc] = new YYARec(-59,324);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,221);yygc++; 
					yyg[yygc] = new YYARec(-66,222);yygc++; 
					yyg[yygc] = new YYARec(-64,223);yygc++; 
					yyg[yygc] = new YYARec(-62,224);yygc++; 
					yyg[yygc] = new YYARec(-61,225);yygc++; 
					yyg[yygc] = new YYARec(-60,226);yygc++; 
					yyg[yygc] = new YYARec(-59,227);yygc++; 
					yyg[yygc] = new YYARec(-58,325);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,221);yygc++; 
					yyg[yygc] = new YYARec(-66,222);yygc++; 
					yyg[yygc] = new YYARec(-64,223);yygc++; 
					yyg[yygc] = new YYARec(-62,224);yygc++; 
					yyg[yygc] = new YYARec(-61,225);yygc++; 
					yyg[yygc] = new YYARec(-60,226);yygc++; 
					yyg[yygc] = new YYARec(-59,227);yygc++; 
					yyg[yygc] = new YYARec(-58,228);yygc++; 
					yyg[yygc] = new YYARec(-57,326);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,217);yygc++; 
					yyg[yygc] = new YYARec(-72,218);yygc++; 
					yyg[yygc] = new YYARec(-71,219);yygc++; 
					yyg[yygc] = new YYARec(-70,220);yygc++; 
					yyg[yygc] = new YYARec(-68,221);yygc++; 
					yyg[yygc] = new YYARec(-66,222);yygc++; 
					yyg[yygc] = new YYARec(-64,223);yygc++; 
					yyg[yygc] = new YYARec(-62,224);yygc++; 
					yyg[yygc] = new YYARec(-61,225);yygc++; 
					yyg[yygc] = new YYARec(-60,226);yygc++; 
					yyg[yygc] = new YYARec(-59,227);yygc++; 
					yyg[yygc] = new YYARec(-58,228);yygc++; 
					yyg[yygc] = new YYARec(-57,229);yygc++; 
					yyg[yygc] = new YYARec(-56,230);yygc++; 
					yyg[yygc] = new YYARec(-55,327);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,159);yygc++; 
					yyg[yygc] = new YYARec(-53,160);yygc++; 
					yyg[yygc] = new YYARec(-51,161);yygc++; 
					yyg[yygc] = new YYARec(-50,162);yygc++; 
					yyg[yygc] = new YYARec(-49,163);yygc++; 
					yyg[yygc] = new YYARec(-48,164);yygc++; 
					yyg[yygc] = new YYARec(-23,165);yygc++; 
					yyg[yygc] = new YYARec(-17,330);yygc++; 
					yyg[yygc] = new YYARec(-15,167);yygc++; 
					yyg[yygc] = new YYARec(-11,168);yygc++; 
					yyg[yygc] = new YYARec(-5,169);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,159);yygc++; 
					yyg[yygc] = new YYARec(-53,160);yygc++; 
					yyg[yygc] = new YYARec(-51,161);yygc++; 
					yyg[yygc] = new YYARec(-50,162);yygc++; 
					yyg[yygc] = new YYARec(-49,163);yygc++; 
					yyg[yygc] = new YYARec(-48,164);yygc++; 
					yyg[yygc] = new YYARec(-23,165);yygc++; 
					yyg[yygc] = new YYARec(-17,331);yygc++; 
					yyg[yygc] = new YYARec(-15,167);yygc++; 
					yyg[yygc] = new YYARec(-11,168);yygc++; 
					yyg[yygc] = new YYARec(-5,169);yygc++; 
					yyg[yygc] = new YYARec(-30,332);yygc++; 
					yyg[yygc] = new YYARec(-43,184);yygc++; 
					yyg[yygc] = new YYARec(-42,185);yygc++; 
					yyg[yygc] = new YYARec(-41,93);yygc++; 
					yyg[yygc] = new YYARec(-35,94);yygc++; 
					yyg[yygc] = new YYARec(-23,95);yygc++; 
					yyg[yygc] = new YYARec(-20,334);yygc++; 
					yyg[yygc] = new YYARec(-19,335);yygc++; 
					yyg[yygc] = new YYARec(-18,187);yygc++; 
					yyg[yygc] = new YYARec(-43,184);yygc++; 
					yyg[yygc] = new YYARec(-42,185);yygc++; 
					yyg[yygc] = new YYARec(-41,93);yygc++; 
					yyg[yygc] = new YYARec(-35,94);yygc++; 
					yyg[yygc] = new YYARec(-23,95);yygc++; 
					yyg[yygc] = new YYARec(-20,334);yygc++; 
					yyg[yygc] = new YYARec(-19,336);yygc++; 
					yyg[yygc] = new YYARec(-18,187);yygc++; 
					yyg[yygc] = new YYARec(-71,251);yygc++; 
					yyg[yygc] = new YYARec(-39,252);yygc++; 
					yyg[yygc] = new YYARec(-38,253);yygc++; 
					yyg[yygc] = new YYARec(-37,337);yygc++; 
					yyg[yygc] = new YYARec(-23,255);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-77,115);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-71,150);yygc++; 
					yyg[yygc] = new YYARec(-54,257);yygc++; 
					yyg[yygc] = new YYARec(-52,339);yygc++; 
					yyg[yygc] = new YYARec(-39,117);yygc++; 
					yyg[yygc] = new YYARec(-28,259);yygc++; 
					yyg[yygc] = new YYARec(-27,260);yygc++; 
					yyg[yygc] = new YYARec(-26,261);yygc++; 
					yyg[yygc] = new YYARec(-25,262);yygc++; 
					yyg[yygc] = new YYARec(-24,263);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,157);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,159);yygc++; 
					yyg[yygc] = new YYARec(-53,160);yygc++; 
					yyg[yygc] = new YYARec(-51,161);yygc++; 
					yyg[yygc] = new YYARec(-50,162);yygc++; 
					yyg[yygc] = new YYARec(-49,163);yygc++; 
					yyg[yygc] = new YYARec(-48,164);yygc++; 
					yyg[yygc] = new YYARec(-23,165);yygc++; 
					yyg[yygc] = new YYARec(-17,340);yygc++; 
					yyg[yygc] = new YYARec(-15,167);yygc++; 
					yyg[yygc] = new YYARec(-11,168);yygc++; 
					yyg[yygc] = new YYARec(-5,169);yygc++; 
					yyg[yygc] = new YYARec(-69,273);yygc++; 
					yyg[yygc] = new YYARec(-67,277);yygc++; 
					yyg[yygc] = new YYARec(-65,280);yygc++; 
					yyg[yygc] = new YYARec(-63,285);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-77,115);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-71,150);yygc++; 
					yyg[yygc] = new YYARec(-45,300);yygc++; 
					yyg[yygc] = new YYARec(-44,344);yygc++; 
					yyg[yygc] = new YYARec(-39,117);yygc++; 
					yyg[yygc] = new YYARec(-28,302);yygc++; 
					yyg[yygc] = new YYARec(-27,303);yygc++; 
					yyg[yygc] = new YYARec(-26,304);yygc++; 
					yyg[yygc] = new YYARec(-24,305);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,157);yygc++; 
					yyg[yygc] = new YYARec(-43,184);yygc++; 
					yyg[yygc] = new YYARec(-42,185);yygc++; 
					yyg[yygc] = new YYARec(-41,93);yygc++; 
					yyg[yygc] = new YYARec(-35,94);yygc++; 
					yyg[yygc] = new YYARec(-23,95);yygc++; 
					yyg[yygc] = new YYARec(-20,351);yygc++; 
					yyg[yygc] = new YYARec(-18,187);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = -217;  
					yyd[2] = -216;  
					yyd[3] = 0;  
					yyd[4] = 0;  
					yyd[5] = -63;  
					yyd[6] = 0;  
					yyd[7] = 0;  
					yyd[8] = 0;  
					yyd[9] = 0;  
					yyd[10] = -215;  
					yyd[11] = -42;  
					yyd[12] = -13;  
					yyd[13] = -12;  
					yyd[14] = -11;  
					yyd[15] = -10;  
					yyd[16] = -9;  
					yyd[17] = -8;  
					yyd[18] = -7;  
					yyd[19] = -3;  
					yyd[20] = 0;  
					yyd[21] = -1;  
					yyd[22] = 0;  
					yyd[23] = 0;  
					yyd[24] = -5;  
					yyd[25] = 0;  
					yyd[26] = 0;  
					yyd[27] = 0;  
					yyd[28] = 0;  
					yyd[29] = -6;  
					yyd[30] = -214;  
					yyd[31] = 0;  
					yyd[32] = 0;  
					yyd[33] = 0;  
					yyd[34] = 0;  
					yyd[35] = 0;  
					yyd[36] = -204;  
					yyd[37] = -201;  
					yyd[38] = -206;  
					yyd[39] = -199;  
					yyd[40] = 0;  
					yyd[41] = -213;  
					yyd[42] = 0;  
					yyd[43] = 0;  
					yyd[44] = 0;  
					yyd[45] = -196;  
					yyd[46] = 0;  
					yyd[47] = -211;  
					yyd[48] = 0;  
					yyd[49] = -192;  
					yyd[50] = 0;  
					yyd[51] = -193;  
					yyd[52] = -209;  
					yyd[53] = -187;  
					yyd[54] = 0;  
					yyd[55] = -221;  
					yyd[56] = -219;  
					yyd[57] = 0;  
					yyd[58] = -200;  
					yyd[59] = -203;  
					yyd[60] = -198;  
					yyd[61] = -205;  
					yyd[62] = -202;  
					yyd[63] = -212;  
					yyd[64] = -195;  
					yyd[65] = -194;  
					yyd[66] = -197;  
					yyd[67] = -208;  
					yyd[68] = -210;  
					yyd[69] = -207;  
					yyd[70] = 0;  
					yyd[71] = -220;  
					yyd[72] = 0;  
					yyd[73] = -223;  
					yyd[74] = 0;  
					yyd[75] = -222;  
					yyd[76] = 0;  
					yyd[77] = -97;  
					yyd[78] = 0;  
					yyd[79] = -2;  
					yyd[80] = -30;  
					yyd[81] = -29;  
					yyd[82] = 0;  
					yyd[83] = -191;  
					yyd[84] = -189;  
					yyd[85] = -190;  
					yyd[86] = 0;  
					yyd[87] = 0;  
					yyd[88] = 0;  
					yyd[89] = 0;  
					yyd[90] = -225;  
					yyd[91] = -154;  
					yyd[92] = -155;  
					yyd[93] = -172;  
					yyd[94] = -173;  
					yyd[95] = -171;  
					yyd[96] = -57;  
					yyd[97] = -61;  
					yyd[98] = -163;  
					yyd[99] = -170;  
					yyd[100] = -166;  
					yyd[101] = -165;  
					yyd[102] = -160;  
					yyd[103] = -168;  
					yyd[104] = -162;  
					yyd[105] = -169;  
					yyd[106] = -159;  
					yyd[107] = -167;  
					yyd[108] = -164;  
					yyd[109] = -161;  
					yyd[110] = 0;  
					yyd[111] = 0;  
					yyd[112] = 0;  
					yyd[113] = -60;  
					yyd[114] = 0;  
					yyd[115] = -179;  
					yyd[116] = 0;  
					yyd[117] = -180;  
					yyd[118] = 0;  
					yyd[119] = -49;  
					yyd[120] = -50;  
					yyd[121] = -47;  
					yyd[122] = -48;  
					yyd[123] = -138;  
					yyd[124] = -139;  
					yyd[125] = -137;  
					yyd[126] = -184;  
					yyd[127] = -186;  
					yyd[128] = -226;  
					yyd[129] = 0;  
					yyd[130] = 0;  
					yyd[131] = 0;  
					yyd[132] = 0;  
					yyd[133] = -27;  
					yyd[134] = 0;  
					yyd[135] = -28;  
					yyd[136] = -36;  
					yyd[137] = 0;  
					yyd[138] = -218;  
					yyd[139] = 0;  
					yyd[140] = 0;  
					yyd[141] = 0;  
					yyd[142] = -183;  
					yyd[143] = -185;  
					yyd[144] = -38;  
					yyd[145] = 0;  
					yyd[146] = -37;  
					yyd[147] = 0;  
					yyd[148] = 0;  
					yyd[149] = 0;  
					yyd[150] = 0;  
					yyd[151] = -35;  
					yyd[152] = -34;  
					yyd[153] = -33;  
					yyd[154] = -32;  
					yyd[155] = -31;  
					yyd[156] = 0;  
					yyd[157] = -157;  
					yyd[158] = -188;  
					yyd[159] = 0;  
					yyd[160] = 0;  
					yyd[161] = 0;  
					yyd[162] = 0;  
					yyd[163] = 0;  
					yyd[164] = 0;  
					yyd[165] = 0;  
					yyd[166] = 0;  
					yyd[167] = 0;  
					yyd[168] = -89;  
					yyd[169] = 0;  
					yyd[170] = 0;  
					yyd[171] = -88;  
					yyd[172] = 0;  
					yyd[173] = 0;  
					yyd[174] = 0;  
					yyd[175] = 0;  
					yyd[176] = 0;  
					yyd[177] = 0;  
					yyd[178] = 0;  
					yyd[179] = 0;  
					yyd[180] = 0;  
					yyd[181] = 0;  
					yyd[182] = 0;  
					yyd[183] = -59;  
					yyd[184] = 0;  
					yyd[185] = 0;  
					yyd[186] = 0;  
					yyd[187] = 0;  
					yyd[188] = 0;  
					yyd[189] = -68;  
					yyd[190] = 0;  
					yyd[191] = 0;  
					yyd[192] = -46;  
					yyd[193] = -14;  
					yyd[194] = 0;  
					yyd[195] = -17;  
					yyd[196] = -15;  
					yyd[197] = -158;  
					yyd[198] = -26;  
					yyd[199] = -86;  
					yyd[200] = 0;  
					yyd[201] = -87;  
					yyd[202] = -82;  
					yyd[203] = -80;  
					yyd[204] = 0;  
					yyd[205] = 0;  
					yyd[206] = -75;  
					yyd[207] = -81;  
					yyd[208] = 0;  
					yyd[209] = -143;  
					yyd[210] = -144;  
					yyd[211] = -145;  
					yyd[212] = -146;  
					yyd[213] = -147;  
					yyd[214] = 0;  
					yyd[215] = 0;  
					yyd[216] = 0;  
					yyd[217] = -125;  
					yyd[218] = 0;  
					yyd[219] = 0;  
					yyd[220] = -118;  
					yyd[221] = -116;  
					yyd[222] = 0;  
					yyd[223] = 0;  
					yyd[224] = 0;  
					yyd[225] = 0;  
					yyd[226] = 0;  
					yyd[227] = 0;  
					yyd[228] = 0;  
					yyd[229] = 0;  
					yyd[230] = 0;  
					yyd[231] = -140;  
					yyd[232] = -123;  
					yyd[233] = 0;  
					yyd[234] = 0;  
					yyd[235] = 0;  
					yyd[236] = 0;  
					yyd[237] = 0;  
					yyd[238] = -182;  
					yyd[239] = -181;  
					yyd[240] = 0;  
					yyd[241] = 0;  
					yyd[242] = 0;  
					yyd[243] = 0;  
					yyd[244] = 0;  
					yyd[245] = 0;  
					yyd[246] = -64;  
					yyd[247] = -58;  
					yyd[248] = -65;  
					yyd[249] = 0;  
					yyd[250] = 0;  
					yyd[251] = 0;  
					yyd[252] = -56;  
					yyd[253] = 0;  
					yyd[254] = 0;  
					yyd[255] = -55;  
					yyd[256] = 0;  
					yyd[257] = 0;  
					yyd[258] = 0;  
					yyd[259] = -92;  
					yyd[260] = -95;  
					yyd[261] = -96;  
					yyd[262] = -93;  
					yyd[263] = -94;  
					yyd[264] = -79;  
					yyd[265] = 0;  
					yyd[266] = -142;  
					yyd[267] = 0;  
					yyd[268] = -18;  
					yyd[269] = -19;  
					yyd[270] = 0;  
					yyd[271] = 0;  
					yyd[272] = -119;  
					yyd[273] = 0;  
					yyd[274] = -134;  
					yyd[275] = -135;  
					yyd[276] = -136;  
					yyd[277] = 0;  
					yyd[278] = -132;  
					yyd[279] = -133;  
					yyd[280] = 0;  
					yyd[281] = -128;  
					yyd[282] = -129;  
					yyd[283] = -130;  
					yyd[284] = -131;  
					yyd[285] = 0;  
					yyd[286] = -126;  
					yyd[287] = -127;  
					yyd[288] = 0;  
					yyd[289] = 0;  
					yyd[290] = 0;  
					yyd[291] = 0;  
					yyd[292] = 0;  
					yyd[293] = 0;  
					yyd[294] = -122;  
					yyd[295] = 0;  
					yyd[296] = 0;  
					yyd[297] = 0;  
					yyd[298] = 0;  
					yyd[299] = -77;  
					yyd[300] = 0;  
					yyd[301] = 0;  
					yyd[302] = -72;  
					yyd[303] = -73;  
					yyd[304] = -74;  
					yyd[305] = -71;  
					yyd[306] = 0;  
					yyd[307] = 0;  
					yyd[308] = 0;  
					yyd[309] = -51;  
					yyd[310] = 0;  
					yyd[311] = 0;  
					yyd[312] = -84;  
					yyd[313] = -85;  
					yyd[314] = 0;  
					yyd[315] = -21;  
					yyd[316] = -78;  
					yyd[317] = 0;  
					yyd[318] = -117;  
					yyd[319] = 0;  
					yyd[320] = 0;  
					yyd[321] = 0;  
					yyd[322] = 0;  
					yyd[323] = 0;  
					yyd[324] = 0;  
					yyd[325] = 0;  
					yyd[326] = 0;  
					yyd[327] = -141;  
					yyd[328] = -121;  
					yyd[329] = -151;  
					yyd[330] = 0;  
					yyd[331] = 0;  
					yyd[332] = 0;  
					yyd[333] = -67;  
					yyd[334] = 0;  
					yyd[335] = 0;  
					yyd[336] = 0;  
					yyd[337] = -52;  
					yyd[338] = -16;  
					yyd[339] = -91;  
					yyd[340] = 0;  
					yyd[341] = -120;  
					yyd[342] = -152;  
					yyd[343] = -153;  
					yyd[344] = -70;  
					yyd[345] = 0;  
					yyd[346] = -25;  
					yyd[347] = -22;  
					yyd[348] = -23;  
					yyd[349] = -20;  
					yyd[350] = 0;  
					yyd[351] = 0;  
					yyd[352] = -24; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 33;  
					yyal[2] = 33;  
					yyal[3] = 33;  
					yyal[4] = 92;  
					yyal[5] = 116;  
					yyal[6] = 116;  
					yyal[7] = 140;  
					yyal[8] = 165;  
					yyal[9] = 174;  
					yyal[10] = 199;  
					yyal[11] = 199;  
					yyal[12] = 199;  
					yyal[13] = 199;  
					yyal[14] = 199;  
					yyal[15] = 199;  
					yyal[16] = 199;  
					yyal[17] = 199;  
					yyal[18] = 199;  
					yyal[19] = 199;  
					yyal[20] = 199;  
					yyal[21] = 233;  
					yyal[22] = 233;  
					yyal[23] = 234;  
					yyal[24] = 238;  
					yyal[25] = 238;  
					yyal[26] = 242;  
					yyal[27] = 246;  
					yyal[28] = 250;  
					yyal[29] = 251;  
					yyal[30] = 251;  
					yyal[31] = 251;  
					yyal[32] = 280;  
					yyal[33] = 293;  
					yyal[34] = 322;  
					yyal[35] = 350;  
					yyal[36] = 378;  
					yyal[37] = 378;  
					yyal[38] = 378;  
					yyal[39] = 378;  
					yyal[40] = 378;  
					yyal[41] = 391;  
					yyal[42] = 391;  
					yyal[43] = 404;  
					yyal[44] = 433;  
					yyal[45] = 461;  
					yyal[46] = 461;  
					yyal[47] = 474;  
					yyal[48] = 474;  
					yyal[49] = 487;  
					yyal[50] = 487;  
					yyal[51] = 500;  
					yyal[52] = 500;  
					yyal[53] = 500;  
					yyal[54] = 500;  
					yyal[55] = 515;  
					yyal[56] = 515;  
					yyal[57] = 515;  
					yyal[58] = 517;  
					yyal[59] = 517;  
					yyal[60] = 517;  
					yyal[61] = 517;  
					yyal[62] = 517;  
					yyal[63] = 517;  
					yyal[64] = 517;  
					yyal[65] = 517;  
					yyal[66] = 517;  
					yyal[67] = 517;  
					yyal[68] = 517;  
					yyal[69] = 517;  
					yyal[70] = 517;  
					yyal[71] = 547;  
					yyal[72] = 547;  
					yyal[73] = 551;  
					yyal[74] = 551;  
					yyal[75] = 553;  
					yyal[76] = 553;  
					yyal[77] = 561;  
					yyal[78] = 561;  
					yyal[79] = 585;  
					yyal[80] = 585;  
					yyal[81] = 585;  
					yyal[82] = 585;  
					yyal[83] = 586;  
					yyal[84] = 586;  
					yyal[85] = 586;  
					yyal[86] = 586;  
					yyal[87] = 587;  
					yyal[88] = 589;  
					yyal[89] = 590;  
					yyal[90] = 591;  
					yyal[91] = 591;  
					yyal[92] = 591;  
					yyal[93] = 591;  
					yyal[94] = 591;  
					yyal[95] = 591;  
					yyal[96] = 591;  
					yyal[97] = 591;  
					yyal[98] = 591;  
					yyal[99] = 591;  
					yyal[100] = 591;  
					yyal[101] = 591;  
					yyal[102] = 591;  
					yyal[103] = 591;  
					yyal[104] = 591;  
					yyal[105] = 591;  
					yyal[106] = 591;  
					yyal[107] = 591;  
					yyal[108] = 591;  
					yyal[109] = 591;  
					yyal[110] = 591;  
					yyal[111] = 592;  
					yyal[112] = 593;  
					yyal[113] = 595;  
					yyal[114] = 595;  
					yyal[115] = 596;  
					yyal[116] = 596;  
					yyal[117] = 598;  
					yyal[118] = 598;  
					yyal[119] = 599;  
					yyal[120] = 599;  
					yyal[121] = 599;  
					yyal[122] = 599;  
					yyal[123] = 599;  
					yyal[124] = 599;  
					yyal[125] = 599;  
					yyal[126] = 599;  
					yyal[127] = 599;  
					yyal[128] = 599;  
					yyal[129] = 599;  
					yyal[130] = 625;  
					yyal[131] = 626;  
					yyal[132] = 659;  
					yyal[133] = 692;  
					yyal[134] = 692;  
					yyal[135] = 724;  
					yyal[136] = 724;  
					yyal[137] = 724;  
					yyal[138] = 759;  
					yyal[139] = 759;  
					yyal[140] = 760;  
					yyal[141] = 774;  
					yyal[142] = 781;  
					yyal[143] = 781;  
					yyal[144] = 781;  
					yyal[145] = 781;  
					yyal[146] = 806;  
					yyal[147] = 806;  
					yyal[148] = 807;  
					yyal[149] = 809;  
					yyal[150] = 810;  
					yyal[151] = 836;  
					yyal[152] = 836;  
					yyal[153] = 836;  
					yyal[154] = 836;  
					yyal[155] = 836;  
					yyal[156] = 836;  
					yyal[157] = 837;  
					yyal[158] = 837;  
					yyal[159] = 837;  
					yyal[160] = 844;  
					yyal[161] = 845;  
					yyal[162] = 879;  
					yyal[163] = 916;  
					yyal[164] = 953;  
					yyal[165] = 954;  
					yyal[166] = 994;  
					yyal[167] = 995;  
					yyal[168] = 1032;  
					yyal[169] = 1032;  
					yyal[170] = 1037;  
					yyal[171] = 1041;  
					yyal[172] = 1041;  
					yyal[173] = 1045;  
					yyal[174] = 1080;  
					yyal[175] = 1111;  
					yyal[176] = 1146;  
					yyal[177] = 1177;  
					yyal[178] = 1208;  
					yyal[179] = 1249;  
					yyal[180] = 1290;  
					yyal[181] = 1331;  
					yyal[182] = 1372;  
					yyal[183] = 1413;  
					yyal[184] = 1413;  
					yyal[185] = 1445;  
					yyal[186] = 1461;  
					yyal[187] = 1462;  
					yyal[188] = 1478;  
					yyal[189] = 1482;  
					yyal[190] = 1482;  
					yyal[191] = 1486;  
					yyal[192] = 1492;  
					yyal[193] = 1492;  
					yyal[194] = 1492;  
					yyal[195] = 1493;  
					yyal[196] = 1493;  
					yyal[197] = 1493;  
					yyal[198] = 1493;  
					yyal[199] = 1493;  
					yyal[200] = 1493;  
					yyal[201] = 1525;  
					yyal[202] = 1525;  
					yyal[203] = 1525;  
					yyal[204] = 1525;  
					yyal[205] = 1562;  
					yyal[206] = 1594;  
					yyal[207] = 1594;  
					yyal[208] = 1594;  
					yyal[209] = 1625;  
					yyal[210] = 1625;  
					yyal[211] = 1625;  
					yyal[212] = 1625;  
					yyal[213] = 1625;  
					yyal[214] = 1625;  
					yyal[215] = 1661;  
					yyal[216] = 1697;  
					yyal[217] = 1698;  
					yyal[218] = 1698;  
					yyal[219] = 1699;  
					yyal[220] = 1730;  
					yyal[221] = 1730;  
					yyal[222] = 1730;  
					yyal[223] = 1749;  
					yyal[224] = 1765;  
					yyal[225] = 1779;  
					yyal[226] = 1789;  
					yyal[227] = 1797;  
					yyal[228] = 1804;  
					yyal[229] = 1810;  
					yyal[230] = 1815;  
					yyal[231] = 1819;  
					yyal[232] = 1819;  
					yyal[233] = 1819;  
					yyal[234] = 1842;  
					yyal[235] = 1873;  
					yyal[236] = 1900;  
					yyal[237] = 1927;  
					yyal[238] = 1954;  
					yyal[239] = 1954;  
					yyal[240] = 1954;  
					yyal[241] = 1989;  
					yyal[242] = 1990;  
					yyal[243] = 2010;  
					yyal[244] = 2011;  
					yyal[245] = 2048;  
					yyal[246] = 2079;  
					yyal[247] = 2079;  
					yyal[248] = 2079;  
					yyal[249] = 2079;  
					yyal[250] = 2080;  
					yyal[251] = 2081;  
					yyal[252] = 2082;  
					yyal[253] = 2082;  
					yyal[254] = 2084;  
					yyal[255] = 2085;  
					yyal[256] = 2085;  
					yyal[257] = 2117;  
					yyal[258] = 2151;  
					yyal[259] = 2152;  
					yyal[260] = 2152;  
					yyal[261] = 2152;  
					yyal[262] = 2152;  
					yyal[263] = 2152;  
					yyal[264] = 2152;  
					yyal[265] = 2152;  
					yyal[266] = 2153;  
					yyal[267] = 2153;  
					yyal[268] = 2155;  
					yyal[269] = 2155;  
					yyal[270] = 2155;  
					yyal[271] = 2192;  
					yyal[272] = 2223;  
					yyal[273] = 2223;  
					yyal[274] = 2254;  
					yyal[275] = 2254;  
					yyal[276] = 2254;  
					yyal[277] = 2254;  
					yyal[278] = 2285;  
					yyal[279] = 2285;  
					yyal[280] = 2285;  
					yyal[281] = 2316;  
					yyal[282] = 2316;  
					yyal[283] = 2316;  
					yyal[284] = 2316;  
					yyal[285] = 2316;  
					yyal[286] = 2347;  
					yyal[287] = 2347;  
					yyal[288] = 2347;  
					yyal[289] = 2378;  
					yyal[290] = 2409;  
					yyal[291] = 2440;  
					yyal[292] = 2471;  
					yyal[293] = 2502;  
					yyal[294] = 2533;  
					yyal[295] = 2533;  
					yyal[296] = 2534;  
					yyal[297] = 2535;  
					yyal[298] = 2570;  
					yyal[299] = 2605;  
					yyal[300] = 2605;  
					yyal[301] = 2638;  
					yyal[302] = 2639;  
					yyal[303] = 2639;  
					yyal[304] = 2639;  
					yyal[305] = 2639;  
					yyal[306] = 2639;  
					yyal[307] = 2654;  
					yyal[308] = 2669;  
					yyal[309] = 2675;  
					yyal[310] = 2675;  
					yyal[311] = 2676;  
					yyal[312] = 2709;  
					yyal[313] = 2709;  
					yyal[314] = 2709;  
					yyal[315] = 2744;  
					yyal[316] = 2744;  
					yyal[317] = 2744;  
					yyal[318] = 2745;  
					yyal[319] = 2745;  
					yyal[320] = 2764;  
					yyal[321] = 2780;  
					yyal[322] = 2794;  
					yyal[323] = 2804;  
					yyal[324] = 2812;  
					yyal[325] = 2819;  
					yyal[326] = 2825;  
					yyal[327] = 2830;  
					yyal[328] = 2830;  
					yyal[329] = 2830;  
					yyal[330] = 2830;  
					yyal[331] = 2831;  
					yyal[332] = 2832;  
					yyal[333] = 2864;  
					yyal[334] = 2864;  
					yyal[335] = 2866;  
					yyal[336] = 2867;  
					yyal[337] = 2868;  
					yyal[338] = 2868;  
					yyal[339] = 2868;  
					yyal[340] = 2868;  
					yyal[341] = 2869;  
					yyal[342] = 2869;  
					yyal[343] = 2869;  
					yyal[344] = 2869;  
					yyal[345] = 2869;  
					yyal[346] = 2870;  
					yyal[347] = 2870;  
					yyal[348] = 2870;  
					yyal[349] = 2870;  
					yyal[350] = 2870;  
					yyal[351] = 2884;  
					yyal[352] = 2885; 

					yyah = new int[yynstates];
					yyah[0] = 32;  
					yyah[1] = 32;  
					yyah[2] = 32;  
					yyah[3] = 91;  
					yyah[4] = 115;  
					yyah[5] = 115;  
					yyah[6] = 139;  
					yyah[7] = 164;  
					yyah[8] = 173;  
					yyah[9] = 198;  
					yyah[10] = 198;  
					yyah[11] = 198;  
					yyah[12] = 198;  
					yyah[13] = 198;  
					yyah[14] = 198;  
					yyah[15] = 198;  
					yyah[16] = 198;  
					yyah[17] = 198;  
					yyah[18] = 198;  
					yyah[19] = 198;  
					yyah[20] = 232;  
					yyah[21] = 232;  
					yyah[22] = 233;  
					yyah[23] = 237;  
					yyah[24] = 237;  
					yyah[25] = 241;  
					yyah[26] = 245;  
					yyah[27] = 249;  
					yyah[28] = 250;  
					yyah[29] = 250;  
					yyah[30] = 250;  
					yyah[31] = 279;  
					yyah[32] = 292;  
					yyah[33] = 321;  
					yyah[34] = 349;  
					yyah[35] = 377;  
					yyah[36] = 377;  
					yyah[37] = 377;  
					yyah[38] = 377;  
					yyah[39] = 377;  
					yyah[40] = 390;  
					yyah[41] = 390;  
					yyah[42] = 403;  
					yyah[43] = 432;  
					yyah[44] = 460;  
					yyah[45] = 460;  
					yyah[46] = 473;  
					yyah[47] = 473;  
					yyah[48] = 486;  
					yyah[49] = 486;  
					yyah[50] = 499;  
					yyah[51] = 499;  
					yyah[52] = 499;  
					yyah[53] = 499;  
					yyah[54] = 514;  
					yyah[55] = 514;  
					yyah[56] = 514;  
					yyah[57] = 516;  
					yyah[58] = 516;  
					yyah[59] = 516;  
					yyah[60] = 516;  
					yyah[61] = 516;  
					yyah[62] = 516;  
					yyah[63] = 516;  
					yyah[64] = 516;  
					yyah[65] = 516;  
					yyah[66] = 516;  
					yyah[67] = 516;  
					yyah[68] = 516;  
					yyah[69] = 516;  
					yyah[70] = 546;  
					yyah[71] = 546;  
					yyah[72] = 550;  
					yyah[73] = 550;  
					yyah[74] = 552;  
					yyah[75] = 552;  
					yyah[76] = 560;  
					yyah[77] = 560;  
					yyah[78] = 584;  
					yyah[79] = 584;  
					yyah[80] = 584;  
					yyah[81] = 584;  
					yyah[82] = 585;  
					yyah[83] = 585;  
					yyah[84] = 585;  
					yyah[85] = 585;  
					yyah[86] = 586;  
					yyah[87] = 588;  
					yyah[88] = 589;  
					yyah[89] = 590;  
					yyah[90] = 590;  
					yyah[91] = 590;  
					yyah[92] = 590;  
					yyah[93] = 590;  
					yyah[94] = 590;  
					yyah[95] = 590;  
					yyah[96] = 590;  
					yyah[97] = 590;  
					yyah[98] = 590;  
					yyah[99] = 590;  
					yyah[100] = 590;  
					yyah[101] = 590;  
					yyah[102] = 590;  
					yyah[103] = 590;  
					yyah[104] = 590;  
					yyah[105] = 590;  
					yyah[106] = 590;  
					yyah[107] = 590;  
					yyah[108] = 590;  
					yyah[109] = 590;  
					yyah[110] = 591;  
					yyah[111] = 592;  
					yyah[112] = 594;  
					yyah[113] = 594;  
					yyah[114] = 595;  
					yyah[115] = 595;  
					yyah[116] = 597;  
					yyah[117] = 597;  
					yyah[118] = 598;  
					yyah[119] = 598;  
					yyah[120] = 598;  
					yyah[121] = 598;  
					yyah[122] = 598;  
					yyah[123] = 598;  
					yyah[124] = 598;  
					yyah[125] = 598;  
					yyah[126] = 598;  
					yyah[127] = 598;  
					yyah[128] = 598;  
					yyah[129] = 624;  
					yyah[130] = 625;  
					yyah[131] = 658;  
					yyah[132] = 691;  
					yyah[133] = 691;  
					yyah[134] = 723;  
					yyah[135] = 723;  
					yyah[136] = 723;  
					yyah[137] = 758;  
					yyah[138] = 758;  
					yyah[139] = 759;  
					yyah[140] = 773;  
					yyah[141] = 780;  
					yyah[142] = 780;  
					yyah[143] = 780;  
					yyah[144] = 780;  
					yyah[145] = 805;  
					yyah[146] = 805;  
					yyah[147] = 806;  
					yyah[148] = 808;  
					yyah[149] = 809;  
					yyah[150] = 835;  
					yyah[151] = 835;  
					yyah[152] = 835;  
					yyah[153] = 835;  
					yyah[154] = 835;  
					yyah[155] = 835;  
					yyah[156] = 836;  
					yyah[157] = 836;  
					yyah[158] = 836;  
					yyah[159] = 843;  
					yyah[160] = 844;  
					yyah[161] = 878;  
					yyah[162] = 915;  
					yyah[163] = 952;  
					yyah[164] = 953;  
					yyah[165] = 993;  
					yyah[166] = 994;  
					yyah[167] = 1031;  
					yyah[168] = 1031;  
					yyah[169] = 1036;  
					yyah[170] = 1040;  
					yyah[171] = 1040;  
					yyah[172] = 1044;  
					yyah[173] = 1079;  
					yyah[174] = 1110;  
					yyah[175] = 1145;  
					yyah[176] = 1176;  
					yyah[177] = 1207;  
					yyah[178] = 1248;  
					yyah[179] = 1289;  
					yyah[180] = 1330;  
					yyah[181] = 1371;  
					yyah[182] = 1412;  
					yyah[183] = 1412;  
					yyah[184] = 1444;  
					yyah[185] = 1460;  
					yyah[186] = 1461;  
					yyah[187] = 1477;  
					yyah[188] = 1481;  
					yyah[189] = 1481;  
					yyah[190] = 1485;  
					yyah[191] = 1491;  
					yyah[192] = 1491;  
					yyah[193] = 1491;  
					yyah[194] = 1492;  
					yyah[195] = 1492;  
					yyah[196] = 1492;  
					yyah[197] = 1492;  
					yyah[198] = 1492;  
					yyah[199] = 1492;  
					yyah[200] = 1524;  
					yyah[201] = 1524;  
					yyah[202] = 1524;  
					yyah[203] = 1524;  
					yyah[204] = 1561;  
					yyah[205] = 1593;  
					yyah[206] = 1593;  
					yyah[207] = 1593;  
					yyah[208] = 1624;  
					yyah[209] = 1624;  
					yyah[210] = 1624;  
					yyah[211] = 1624;  
					yyah[212] = 1624;  
					yyah[213] = 1624;  
					yyah[214] = 1660;  
					yyah[215] = 1696;  
					yyah[216] = 1697;  
					yyah[217] = 1697;  
					yyah[218] = 1698;  
					yyah[219] = 1729;  
					yyah[220] = 1729;  
					yyah[221] = 1729;  
					yyah[222] = 1748;  
					yyah[223] = 1764;  
					yyah[224] = 1778;  
					yyah[225] = 1788;  
					yyah[226] = 1796;  
					yyah[227] = 1803;  
					yyah[228] = 1809;  
					yyah[229] = 1814;  
					yyah[230] = 1818;  
					yyah[231] = 1818;  
					yyah[232] = 1818;  
					yyah[233] = 1841;  
					yyah[234] = 1872;  
					yyah[235] = 1899;  
					yyah[236] = 1926;  
					yyah[237] = 1953;  
					yyah[238] = 1953;  
					yyah[239] = 1953;  
					yyah[240] = 1988;  
					yyah[241] = 1989;  
					yyah[242] = 2009;  
					yyah[243] = 2010;  
					yyah[244] = 2047;  
					yyah[245] = 2078;  
					yyah[246] = 2078;  
					yyah[247] = 2078;  
					yyah[248] = 2078;  
					yyah[249] = 2079;  
					yyah[250] = 2080;  
					yyah[251] = 2081;  
					yyah[252] = 2081;  
					yyah[253] = 2083;  
					yyah[254] = 2084;  
					yyah[255] = 2084;  
					yyah[256] = 2116;  
					yyah[257] = 2150;  
					yyah[258] = 2151;  
					yyah[259] = 2151;  
					yyah[260] = 2151;  
					yyah[261] = 2151;  
					yyah[262] = 2151;  
					yyah[263] = 2151;  
					yyah[264] = 2151;  
					yyah[265] = 2152;  
					yyah[266] = 2152;  
					yyah[267] = 2154;  
					yyah[268] = 2154;  
					yyah[269] = 2154;  
					yyah[270] = 2191;  
					yyah[271] = 2222;  
					yyah[272] = 2222;  
					yyah[273] = 2253;  
					yyah[274] = 2253;  
					yyah[275] = 2253;  
					yyah[276] = 2253;  
					yyah[277] = 2284;  
					yyah[278] = 2284;  
					yyah[279] = 2284;  
					yyah[280] = 2315;  
					yyah[281] = 2315;  
					yyah[282] = 2315;  
					yyah[283] = 2315;  
					yyah[284] = 2315;  
					yyah[285] = 2346;  
					yyah[286] = 2346;  
					yyah[287] = 2346;  
					yyah[288] = 2377;  
					yyah[289] = 2408;  
					yyah[290] = 2439;  
					yyah[291] = 2470;  
					yyah[292] = 2501;  
					yyah[293] = 2532;  
					yyah[294] = 2532;  
					yyah[295] = 2533;  
					yyah[296] = 2534;  
					yyah[297] = 2569;  
					yyah[298] = 2604;  
					yyah[299] = 2604;  
					yyah[300] = 2637;  
					yyah[301] = 2638;  
					yyah[302] = 2638;  
					yyah[303] = 2638;  
					yyah[304] = 2638;  
					yyah[305] = 2638;  
					yyah[306] = 2653;  
					yyah[307] = 2668;  
					yyah[308] = 2674;  
					yyah[309] = 2674;  
					yyah[310] = 2675;  
					yyah[311] = 2708;  
					yyah[312] = 2708;  
					yyah[313] = 2708;  
					yyah[314] = 2743;  
					yyah[315] = 2743;  
					yyah[316] = 2743;  
					yyah[317] = 2744;  
					yyah[318] = 2744;  
					yyah[319] = 2763;  
					yyah[320] = 2779;  
					yyah[321] = 2793;  
					yyah[322] = 2803;  
					yyah[323] = 2811;  
					yyah[324] = 2818;  
					yyah[325] = 2824;  
					yyah[326] = 2829;  
					yyah[327] = 2829;  
					yyah[328] = 2829;  
					yyah[329] = 2829;  
					yyah[330] = 2830;  
					yyah[331] = 2831;  
					yyah[332] = 2863;  
					yyah[333] = 2863;  
					yyah[334] = 2865;  
					yyah[335] = 2866;  
					yyah[336] = 2867;  
					yyah[337] = 2867;  
					yyah[338] = 2867;  
					yyah[339] = 2867;  
					yyah[340] = 2868;  
					yyah[341] = 2868;  
					yyah[342] = 2868;  
					yyah[343] = 2868;  
					yyah[344] = 2868;  
					yyah[345] = 2869;  
					yyah[346] = 2869;  
					yyah[347] = 2869;  
					yyah[348] = 2869;  
					yyah[349] = 2869;  
					yyah[350] = 2883;  
					yyah[351] = 2884;  
					yyah[352] = 2884; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 23;  
					yygl[2] = 23;  
					yygl[3] = 23;  
					yygl[4] = 23;  
					yygl[5] = 29;  
					yygl[6] = 29;  
					yygl[7] = 35;  
					yygl[8] = 41;  
					yygl[9] = 42;  
					yygl[10] = 43;  
					yygl[11] = 43;  
					yygl[12] = 43;  
					yygl[13] = 43;  
					yygl[14] = 43;  
					yygl[15] = 43;  
					yygl[16] = 43;  
					yygl[17] = 43;  
					yygl[18] = 43;  
					yygl[19] = 43;  
					yygl[20] = 43;  
					yygl[21] = 64;  
					yygl[22] = 64;  
					yygl[23] = 64;  
					yygl[24] = 67;  
					yygl[25] = 67;  
					yygl[26] = 70;  
					yygl[27] = 73;  
					yygl[28] = 76;  
					yygl[29] = 77;  
					yygl[30] = 77;  
					yygl[31] = 77;  
					yygl[32] = 77;  
					yygl[33] = 77;  
					yygl[34] = 77;  
					yygl[35] = 77;  
					yygl[36] = 77;  
					yygl[37] = 77;  
					yygl[38] = 77;  
					yygl[39] = 77;  
					yygl[40] = 77;  
					yygl[41] = 77;  
					yygl[42] = 77;  
					yygl[43] = 77;  
					yygl[44] = 77;  
					yygl[45] = 77;  
					yygl[46] = 77;  
					yygl[47] = 77;  
					yygl[48] = 77;  
					yygl[49] = 77;  
					yygl[50] = 77;  
					yygl[51] = 77;  
					yygl[52] = 77;  
					yygl[53] = 77;  
					yygl[54] = 77;  
					yygl[55] = 82;  
					yygl[56] = 82;  
					yygl[57] = 82;  
					yygl[58] = 83;  
					yygl[59] = 83;  
					yygl[60] = 83;  
					yygl[61] = 83;  
					yygl[62] = 83;  
					yygl[63] = 83;  
					yygl[64] = 83;  
					yygl[65] = 83;  
					yygl[66] = 83;  
					yygl[67] = 83;  
					yygl[68] = 83;  
					yygl[69] = 83;  
					yygl[70] = 83;  
					yygl[71] = 83;  
					yygl[72] = 83;  
					yygl[73] = 84;  
					yygl[74] = 84;  
					yygl[75] = 85;  
					yygl[76] = 85;  
					yygl[77] = 93;  
					yygl[78] = 93;  
					yygl[79] = 100;  
					yygl[80] = 100;  
					yygl[81] = 100;  
					yygl[82] = 100;  
					yygl[83] = 100;  
					yygl[84] = 100;  
					yygl[85] = 100;  
					yygl[86] = 100;  
					yygl[87] = 100;  
					yygl[88] = 100;  
					yygl[89] = 100;  
					yygl[90] = 100;  
					yygl[91] = 100;  
					yygl[92] = 100;  
					yygl[93] = 100;  
					yygl[94] = 100;  
					yygl[95] = 100;  
					yygl[96] = 100;  
					yygl[97] = 100;  
					yygl[98] = 100;  
					yygl[99] = 100;  
					yygl[100] = 100;  
					yygl[101] = 100;  
					yygl[102] = 100;  
					yygl[103] = 100;  
					yygl[104] = 100;  
					yygl[105] = 100;  
					yygl[106] = 100;  
					yygl[107] = 100;  
					yygl[108] = 100;  
					yygl[109] = 100;  
					yygl[110] = 100;  
					yygl[111] = 100;  
					yygl[112] = 100;  
					yygl[113] = 101;  
					yygl[114] = 101;  
					yygl[115] = 102;  
					yygl[116] = 102;  
					yygl[117] = 102;  
					yygl[118] = 102;  
					yygl[119] = 102;  
					yygl[120] = 102;  
					yygl[121] = 102;  
					yygl[122] = 102;  
					yygl[123] = 102;  
					yygl[124] = 102;  
					yygl[125] = 102;  
					yygl[126] = 102;  
					yygl[127] = 102;  
					yygl[128] = 102;  
					yygl[129] = 102;  
					yygl[130] = 103;  
					yygl[131] = 103;  
					yygl[132] = 125;  
					yygl[133] = 147;  
					yygl[134] = 147;  
					yygl[135] = 161;  
					yygl[136] = 161;  
					yygl[137] = 161;  
					yygl[138] = 174;  
					yygl[139] = 174;  
					yygl[140] = 174;  
					yygl[141] = 181;  
					yygl[142] = 182;  
					yygl[143] = 182;  
					yygl[144] = 182;  
					yygl[145] = 182;  
					yygl[146] = 189;  
					yygl[147] = 189;  
					yygl[148] = 189;  
					yygl[149] = 189;  
					yygl[150] = 189;  
					yygl[151] = 194;  
					yygl[152] = 194;  
					yygl[153] = 194;  
					yygl[154] = 194;  
					yygl[155] = 194;  
					yygl[156] = 194;  
					yygl[157] = 194;  
					yygl[158] = 194;  
					yygl[159] = 194;  
					yygl[160] = 194;  
					yygl[161] = 194;  
					yygl[162] = 195;  
					yygl[163] = 208;  
					yygl[164] = 221;  
					yygl[165] = 221;  
					yygl[166] = 222;  
					yygl[167] = 222;  
					yygl[168] = 235;  
					yygl[169] = 235;  
					yygl[170] = 236;  
					yygl[171] = 239;  
					yygl[172] = 239;  
					yygl[173] = 242;  
					yygl[174] = 255;  
					yygl[175] = 276;  
					yygl[176] = 276;  
					yygl[177] = 297;  
					yygl[178] = 318;  
					yygl[179] = 318;  
					yygl[180] = 318;  
					yygl[181] = 318;  
					yygl[182] = 318;  
					yygl[183] = 318;  
					yygl[184] = 318;  
					yygl[185] = 319;  
					yygl[186] = 326;  
					yygl[187] = 326;  
					yygl[188] = 333;  
					yygl[189] = 336;  
					yygl[190] = 336;  
					yygl[191] = 339;  
					yygl[192] = 344;  
					yygl[193] = 344;  
					yygl[194] = 344;  
					yygl[195] = 344;  
					yygl[196] = 344;  
					yygl[197] = 344;  
					yygl[198] = 344;  
					yygl[199] = 344;  
					yygl[200] = 344;  
					yygl[201] = 359;  
					yygl[202] = 359;  
					yygl[203] = 359;  
					yygl[204] = 359;  
					yygl[205] = 372;  
					yygl[206] = 387;  
					yygl[207] = 387;  
					yygl[208] = 387;  
					yygl[209] = 408;  
					yygl[210] = 408;  
					yygl[211] = 408;  
					yygl[212] = 408;  
					yygl[213] = 408;  
					yygl[214] = 408;  
					yygl[215] = 422;  
					yygl[216] = 436;  
					yygl[217] = 436;  
					yygl[218] = 436;  
					yygl[219] = 436;  
					yygl[220] = 447;  
					yygl[221] = 447;  
					yygl[222] = 447;  
					yygl[223] = 448;  
					yygl[224] = 449;  
					yygl[225] = 450;  
					yygl[226] = 451;  
					yygl[227] = 451;  
					yygl[228] = 451;  
					yygl[229] = 451;  
					yygl[230] = 451;  
					yygl[231] = 451;  
					yygl[232] = 451;  
					yygl[233] = 451;  
					yygl[234] = 452;  
					yygl[235] = 473;  
					yygl[236] = 473;  
					yygl[237] = 473;  
					yygl[238] = 473;  
					yygl[239] = 473;  
					yygl[240] = 473;  
					yygl[241] = 486;  
					yygl[242] = 486;  
					yygl[243] = 486;  
					yygl[244] = 486;  
					yygl[245] = 499;  
					yygl[246] = 513;  
					yygl[247] = 513;  
					yygl[248] = 513;  
					yygl[249] = 513;  
					yygl[250] = 513;  
					yygl[251] = 513;  
					yygl[252] = 513;  
					yygl[253] = 513;  
					yygl[254] = 513;  
					yygl[255] = 513;  
					yygl[256] = 513;  
					yygl[257] = 534;  
					yygl[258] = 535;  
					yygl[259] = 535;  
					yygl[260] = 535;  
					yygl[261] = 535;  
					yygl[262] = 535;  
					yygl[263] = 535;  
					yygl[264] = 535;  
					yygl[265] = 535;  
					yygl[266] = 535;  
					yygl[267] = 535;  
					yygl[268] = 535;  
					yygl[269] = 535;  
					yygl[270] = 535;  
					yygl[271] = 548;  
					yygl[272] = 569;  
					yygl[273] = 569;  
					yygl[274] = 580;  
					yygl[275] = 580;  
					yygl[276] = 580;  
					yygl[277] = 580;  
					yygl[278] = 592;  
					yygl[279] = 592;  
					yygl[280] = 592;  
					yygl[281] = 605;  
					yygl[282] = 605;  
					yygl[283] = 605;  
					yygl[284] = 605;  
					yygl[285] = 605;  
					yygl[286] = 619;  
					yygl[287] = 619;  
					yygl[288] = 619;  
					yygl[289] = 634;  
					yygl[290] = 650;  
					yygl[291] = 667;  
					yygl[292] = 685;  
					yygl[293] = 704;  
					yygl[294] = 725;  
					yygl[295] = 725;  
					yygl[296] = 725;  
					yygl[297] = 725;  
					yygl[298] = 738;  
					yygl[299] = 751;  
					yygl[300] = 751;  
					yygl[301] = 752;  
					yygl[302] = 752;  
					yygl[303] = 752;  
					yygl[304] = 752;  
					yygl[305] = 752;  
					yygl[306] = 752;  
					yygl[307] = 760;  
					yygl[308] = 768;  
					yygl[309] = 773;  
					yygl[310] = 773;  
					yygl[311] = 773;  
					yygl[312] = 788;  
					yygl[313] = 788;  
					yygl[314] = 788;  
					yygl[315] = 801;  
					yygl[316] = 801;  
					yygl[317] = 801;  
					yygl[318] = 801;  
					yygl[319] = 801;  
					yygl[320] = 802;  
					yygl[321] = 803;  
					yygl[322] = 804;  
					yygl[323] = 805;  
					yygl[324] = 805;  
					yygl[325] = 805;  
					yygl[326] = 805;  
					yygl[327] = 805;  
					yygl[328] = 805;  
					yygl[329] = 805;  
					yygl[330] = 805;  
					yygl[331] = 805;  
					yygl[332] = 805;  
					yygl[333] = 819;  
					yygl[334] = 819;  
					yygl[335] = 819;  
					yygl[336] = 819;  
					yygl[337] = 819;  
					yygl[338] = 819;  
					yygl[339] = 819;  
					yygl[340] = 819;  
					yygl[341] = 819;  
					yygl[342] = 819;  
					yygl[343] = 819;  
					yygl[344] = 819;  
					yygl[345] = 819;  
					yygl[346] = 819;  
					yygl[347] = 819;  
					yygl[348] = 819;  
					yygl[349] = 819;  
					yygl[350] = 819;  
					yygl[351] = 826;  
					yygl[352] = 826; 

					yygh = new int[yynstates];
					yygh[0] = 22;  
					yygh[1] = 22;  
					yygh[2] = 22;  
					yygh[3] = 22;  
					yygh[4] = 28;  
					yygh[5] = 28;  
					yygh[6] = 34;  
					yygh[7] = 40;  
					yygh[8] = 41;  
					yygh[9] = 42;  
					yygh[10] = 42;  
					yygh[11] = 42;  
					yygh[12] = 42;  
					yygh[13] = 42;  
					yygh[14] = 42;  
					yygh[15] = 42;  
					yygh[16] = 42;  
					yygh[17] = 42;  
					yygh[18] = 42;  
					yygh[19] = 42;  
					yygh[20] = 63;  
					yygh[21] = 63;  
					yygh[22] = 63;  
					yygh[23] = 66;  
					yygh[24] = 66;  
					yygh[25] = 69;  
					yygh[26] = 72;  
					yygh[27] = 75;  
					yygh[28] = 76;  
					yygh[29] = 76;  
					yygh[30] = 76;  
					yygh[31] = 76;  
					yygh[32] = 76;  
					yygh[33] = 76;  
					yygh[34] = 76;  
					yygh[35] = 76;  
					yygh[36] = 76;  
					yygh[37] = 76;  
					yygh[38] = 76;  
					yygh[39] = 76;  
					yygh[40] = 76;  
					yygh[41] = 76;  
					yygh[42] = 76;  
					yygh[43] = 76;  
					yygh[44] = 76;  
					yygh[45] = 76;  
					yygh[46] = 76;  
					yygh[47] = 76;  
					yygh[48] = 76;  
					yygh[49] = 76;  
					yygh[50] = 76;  
					yygh[51] = 76;  
					yygh[52] = 76;  
					yygh[53] = 76;  
					yygh[54] = 81;  
					yygh[55] = 81;  
					yygh[56] = 81;  
					yygh[57] = 82;  
					yygh[58] = 82;  
					yygh[59] = 82;  
					yygh[60] = 82;  
					yygh[61] = 82;  
					yygh[62] = 82;  
					yygh[63] = 82;  
					yygh[64] = 82;  
					yygh[65] = 82;  
					yygh[66] = 82;  
					yygh[67] = 82;  
					yygh[68] = 82;  
					yygh[69] = 82;  
					yygh[70] = 82;  
					yygh[71] = 82;  
					yygh[72] = 83;  
					yygh[73] = 83;  
					yygh[74] = 84;  
					yygh[75] = 84;  
					yygh[76] = 92;  
					yygh[77] = 92;  
					yygh[78] = 99;  
					yygh[79] = 99;  
					yygh[80] = 99;  
					yygh[81] = 99;  
					yygh[82] = 99;  
					yygh[83] = 99;  
					yygh[84] = 99;  
					yygh[85] = 99;  
					yygh[86] = 99;  
					yygh[87] = 99;  
					yygh[88] = 99;  
					yygh[89] = 99;  
					yygh[90] = 99;  
					yygh[91] = 99;  
					yygh[92] = 99;  
					yygh[93] = 99;  
					yygh[94] = 99;  
					yygh[95] = 99;  
					yygh[96] = 99;  
					yygh[97] = 99;  
					yygh[98] = 99;  
					yygh[99] = 99;  
					yygh[100] = 99;  
					yygh[101] = 99;  
					yygh[102] = 99;  
					yygh[103] = 99;  
					yygh[104] = 99;  
					yygh[105] = 99;  
					yygh[106] = 99;  
					yygh[107] = 99;  
					yygh[108] = 99;  
					yygh[109] = 99;  
					yygh[110] = 99;  
					yygh[111] = 99;  
					yygh[112] = 100;  
					yygh[113] = 100;  
					yygh[114] = 101;  
					yygh[115] = 101;  
					yygh[116] = 101;  
					yygh[117] = 101;  
					yygh[118] = 101;  
					yygh[119] = 101;  
					yygh[120] = 101;  
					yygh[121] = 101;  
					yygh[122] = 101;  
					yygh[123] = 101;  
					yygh[124] = 101;  
					yygh[125] = 101;  
					yygh[126] = 101;  
					yygh[127] = 101;  
					yygh[128] = 101;  
					yygh[129] = 102;  
					yygh[130] = 102;  
					yygh[131] = 124;  
					yygh[132] = 146;  
					yygh[133] = 146;  
					yygh[134] = 160;  
					yygh[135] = 160;  
					yygh[136] = 160;  
					yygh[137] = 173;  
					yygh[138] = 173;  
					yygh[139] = 173;  
					yygh[140] = 180;  
					yygh[141] = 181;  
					yygh[142] = 181;  
					yygh[143] = 181;  
					yygh[144] = 181;  
					yygh[145] = 188;  
					yygh[146] = 188;  
					yygh[147] = 188;  
					yygh[148] = 188;  
					yygh[149] = 188;  
					yygh[150] = 193;  
					yygh[151] = 193;  
					yygh[152] = 193;  
					yygh[153] = 193;  
					yygh[154] = 193;  
					yygh[155] = 193;  
					yygh[156] = 193;  
					yygh[157] = 193;  
					yygh[158] = 193;  
					yygh[159] = 193;  
					yygh[160] = 193;  
					yygh[161] = 194;  
					yygh[162] = 207;  
					yygh[163] = 220;  
					yygh[164] = 220;  
					yygh[165] = 221;  
					yygh[166] = 221;  
					yygh[167] = 234;  
					yygh[168] = 234;  
					yygh[169] = 235;  
					yygh[170] = 238;  
					yygh[171] = 238;  
					yygh[172] = 241;  
					yygh[173] = 254;  
					yygh[174] = 275;  
					yygh[175] = 275;  
					yygh[176] = 296;  
					yygh[177] = 317;  
					yygh[178] = 317;  
					yygh[179] = 317;  
					yygh[180] = 317;  
					yygh[181] = 317;  
					yygh[182] = 317;  
					yygh[183] = 317;  
					yygh[184] = 318;  
					yygh[185] = 325;  
					yygh[186] = 325;  
					yygh[187] = 332;  
					yygh[188] = 335;  
					yygh[189] = 335;  
					yygh[190] = 338;  
					yygh[191] = 343;  
					yygh[192] = 343;  
					yygh[193] = 343;  
					yygh[194] = 343;  
					yygh[195] = 343;  
					yygh[196] = 343;  
					yygh[197] = 343;  
					yygh[198] = 343;  
					yygh[199] = 343;  
					yygh[200] = 358;  
					yygh[201] = 358;  
					yygh[202] = 358;  
					yygh[203] = 358;  
					yygh[204] = 371;  
					yygh[205] = 386;  
					yygh[206] = 386;  
					yygh[207] = 386;  
					yygh[208] = 407;  
					yygh[209] = 407;  
					yygh[210] = 407;  
					yygh[211] = 407;  
					yygh[212] = 407;  
					yygh[213] = 407;  
					yygh[214] = 421;  
					yygh[215] = 435;  
					yygh[216] = 435;  
					yygh[217] = 435;  
					yygh[218] = 435;  
					yygh[219] = 446;  
					yygh[220] = 446;  
					yygh[221] = 446;  
					yygh[222] = 447;  
					yygh[223] = 448;  
					yygh[224] = 449;  
					yygh[225] = 450;  
					yygh[226] = 450;  
					yygh[227] = 450;  
					yygh[228] = 450;  
					yygh[229] = 450;  
					yygh[230] = 450;  
					yygh[231] = 450;  
					yygh[232] = 450;  
					yygh[233] = 451;  
					yygh[234] = 472;  
					yygh[235] = 472;  
					yygh[236] = 472;  
					yygh[237] = 472;  
					yygh[238] = 472;  
					yygh[239] = 472;  
					yygh[240] = 485;  
					yygh[241] = 485;  
					yygh[242] = 485;  
					yygh[243] = 485;  
					yygh[244] = 498;  
					yygh[245] = 512;  
					yygh[246] = 512;  
					yygh[247] = 512;  
					yygh[248] = 512;  
					yygh[249] = 512;  
					yygh[250] = 512;  
					yygh[251] = 512;  
					yygh[252] = 512;  
					yygh[253] = 512;  
					yygh[254] = 512;  
					yygh[255] = 512;  
					yygh[256] = 533;  
					yygh[257] = 534;  
					yygh[258] = 534;  
					yygh[259] = 534;  
					yygh[260] = 534;  
					yygh[261] = 534;  
					yygh[262] = 534;  
					yygh[263] = 534;  
					yygh[264] = 534;  
					yygh[265] = 534;  
					yygh[266] = 534;  
					yygh[267] = 534;  
					yygh[268] = 534;  
					yygh[269] = 534;  
					yygh[270] = 547;  
					yygh[271] = 568;  
					yygh[272] = 568;  
					yygh[273] = 579;  
					yygh[274] = 579;  
					yygh[275] = 579;  
					yygh[276] = 579;  
					yygh[277] = 591;  
					yygh[278] = 591;  
					yygh[279] = 591;  
					yygh[280] = 604;  
					yygh[281] = 604;  
					yygh[282] = 604;  
					yygh[283] = 604;  
					yygh[284] = 604;  
					yygh[285] = 618;  
					yygh[286] = 618;  
					yygh[287] = 618;  
					yygh[288] = 633;  
					yygh[289] = 649;  
					yygh[290] = 666;  
					yygh[291] = 684;  
					yygh[292] = 703;  
					yygh[293] = 724;  
					yygh[294] = 724;  
					yygh[295] = 724;  
					yygh[296] = 724;  
					yygh[297] = 737;  
					yygh[298] = 750;  
					yygh[299] = 750;  
					yygh[300] = 751;  
					yygh[301] = 751;  
					yygh[302] = 751;  
					yygh[303] = 751;  
					yygh[304] = 751;  
					yygh[305] = 751;  
					yygh[306] = 759;  
					yygh[307] = 767;  
					yygh[308] = 772;  
					yygh[309] = 772;  
					yygh[310] = 772;  
					yygh[311] = 787;  
					yygh[312] = 787;  
					yygh[313] = 787;  
					yygh[314] = 800;  
					yygh[315] = 800;  
					yygh[316] = 800;  
					yygh[317] = 800;  
					yygh[318] = 800;  
					yygh[319] = 801;  
					yygh[320] = 802;  
					yygh[321] = 803;  
					yygh[322] = 804;  
					yygh[323] = 804;  
					yygh[324] = 804;  
					yygh[325] = 804;  
					yygh[326] = 804;  
					yygh[327] = 804;  
					yygh[328] = 804;  
					yygh[329] = 804;  
					yygh[330] = 804;  
					yygh[331] = 804;  
					yygh[332] = 818;  
					yygh[333] = 818;  
					yygh[334] = 818;  
					yygh[335] = 818;  
					yygh[336] = 818;  
					yygh[337] = 818;  
					yygh[338] = 818;  
					yygh[339] = 818;  
					yygh[340] = 818;  
					yygh[341] = 818;  
					yygh[342] = 818;  
					yygh[343] = 818;  
					yygh[344] = 818;  
					yygh[345] = 818;  
					yygh[346] = 818;  
					yygh[347] = 818;  
					yygh[348] = 818;  
					yygh[349] = 818;  
					yygh[350] = 825;  
					yygh[351] = 825;  
					yygh[352] = 825; 

					yyr[yyrc] = new YYRRec(1,-2);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-3);yyrc++; 
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
					yyr[yyrc] = new YYRRec(5,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-30);yyrc++; 
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
					yyr[yyrc] = new YYRRec(2,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
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
					yyr[yyrc] = new YYRRec(2,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-80);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-80);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++;
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
            int l = yyr[-yyn].len;
            for (int z = yysp; z > yysp - l; z--)
                yyv[z] = null;

            yysp -= l;
//            yysp -= yyr[-yyn].len;

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
            int pos = 0;
            while (1 == 1)
            {
                AToken lasttoken = FindTokenOpt(Input, pos);
                if (lasttoken.token == 0) break;
                if (lasttoken.token != t_ignore) TokenList.Add(lasttoken);
                pos += lasttoken.val.Length;
                if (Input.Length <= pos)
                    return true;
            }
            System.Console.WriteLine(Input);
            System.Console.WriteLine();
            System.Console.WriteLine("No matching token found!");
            return false;
        }
        public AToken FindTokenOpt(string Rest, int startpos)
        {
            Match m;
            int maxlength = 0;
            int besttoken = 0;
            AToken ret = new AToken();
            try
            {

                for (int idx = 0; idx < tList.Count; idx++)
                {
                    m = rList[idx].Match(Rest, startpos);
                    if (m.Success)
                    {
                        if (m.Value.Length > maxlength)
                        {
                            maxlength = m.Value.Length;
                            besttoken = tList[idx];
                            ret.token = besttoken;
                            if (besttoken != 0)
                                ret.val = m.Value;
                        }
                    }
                }

            }
            catch { }
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

			if (Regex.IsMatch(Rest,"^((:=|(,+[\\s\\t\\x00]*)*))")){
				Results.Add (t_Char44);
				ResultsV.Add(Regex.Match(Rest,"^((:=|(,+[\\s\\t\\x00]*)*))").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)(EACH_SEC|IF_(AE|ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|OE|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|UE|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))")){
				Results.Add (t_event);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(EACH_SEC|IF_(AE|ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|OE|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|UE|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)(ACOS|COS|ATAN|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))")){
				Results.Add (t_math);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACOS|COS|ATAN|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))")){
				Results.Add (t_flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_(C|F)|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FOOT_HGT|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SCALE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))")){
				Results.Add (t_property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_(C|F)|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FOOT_HGT|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SCALE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))")){
				Results.Add (t_command);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))")){
				Results.Add (t_list);
				ResultsV.Add(Regex.Match(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|APPEND_MODE|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|RANGE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))")){
				Results.Add (t_skill);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|APPEND_MODE|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|RANGE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE|REGION[1-8]))")){
				Results.Add (t_synonym);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE|REGION[1-8]))").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)(FLAG[1-8]))")){
				Results.Add (t_ambigChar95skillChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLAG[1-8]))").Value);}

			if (Regex.IsMatch(Rest,"^([0-9]+)")){
				Results.Add (t_integer);
				ResultsV.Add(Regex.Match(Rest,"^([0-9]+)").Value);}

			if (Regex.IsMatch(Rest,"^(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))")){
				Results.Add (t_fixed);
				ResultsV.Add(Regex.Match(Rest,"^(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))").Value);}

			if (Regex.IsMatch(Rest,"^([A-Za-z0-9_][A-Za-z0-9_\\?]*(\\.[1-9][0-9]?)?)")){
				Results.Add (t_identifier);
				ResultsV.Add(Regex.Match(Rest,"^([A-Za-z0-9_][A-Za-z0-9_\\?]*(\\.[1-9][0-9]?)?)").Value);}

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
