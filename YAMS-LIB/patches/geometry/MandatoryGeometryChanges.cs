using UndertaleModLib;
using UndertaleModLib.Decompiler;

namespace YAMS_LIB.patches.geometry;

public class MandatoryGeometryChanges
{
    public static void Apply(UndertaleData gmData, GlobalDecompileContext decompileContext, SeedObject seedObject)
    {
        // A4 exterior top, always remove the bomb blocks when coming from that entrance
        foreach (string codeName in new[] { "gml_RoomCC_rm_a4h03_6341_Create", "gml_RoomCC_rm_a4h03_6342_Create" })
        {
            gmData.Code.ByName(codeName).ReplaceGMLInCode("oControl.mod_previous_room == 214 && global.spiderball == 0", "global.targetx == 416");
        }

        // Super Missile chamber - make first two crumble blocks shoot blocks
        gmData.Code.ByName("gml_Room_rm_a3a23a_Create").AppendGMLInCode("if (global.softlockPrevention) { with (119465) instance_destroy(); with (119465) instance_destroy(); instance_create(304, 96, oBlockShoot); instance_create(304, 112, oBlockShoot);}");

        // When going down from thoth, make PB blocks disabled
        gmData.Code.ByName("gml_Room_rm_a0h13_Create").PrependGMLInCode("if (global.targety == 16) {global.event[176] = 1; with (oBlockPBombChain) event_user(0); }");

        // When coming from right side in Drill, always make drill event done
        gmData.Code.ByName("gml_Room_rm_a0h17e_Create").PrependGMLInCode("if (global.targety == 160) global.event[172] = 3");
    }
}
