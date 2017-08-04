namespace FOMOD.Creator.Localize
{
    using System.Collections.Generic;
    using System.Reflection;
    using Newtonsoft.Json;

    class ApplicationLocalize
    {
        [JsonIgnore]
        public System.Globalization.CultureInfo Culture { get; set; }

        static Dictionary<string, PropertyInfo> properies;
        static ApplicationLocalize()
        {
            properies = new Dictionary<string, PropertyInfo>();
            var pr = typeof(ApplicationLocalize).GetProperties();
            foreach (var item in pr)
            {
                properies.Add(item.Name.ToLower(), item);
            }
        }
        public object Read(string key)
        {
            key = key.ToLower().Replace("-","_");
            if (properies.ContainsKey(key))
            {
                return properies[key].GetValue(this);
            }
            else
            {
                return null;
            }
        }



        [JsonProperty("app-welcome")] public string app_welcome { get; set; } = "Welcome!";
        [JsonProperty("app-open-project")] public string app_open_project { get; set; } = "Open project";
        [JsonProperty("app-save-project")] public string app_save_project { get; set; } = "Save";
        [JsonProperty("app-save-as-project")] public string app_save_as_project { get; set; } = "Save as";
        [JsonProperty("app-new-project")] public string app_new_project { get; set; } = "Create project";
        [JsonProperty("app-close-application")] public string app_close_application { get; set; } = "Close programm";
        [JsonProperty("app-recent")] public string app_recent { get; set; } = "Recent";
        [JsonProperty("app-recent-header")] public string app_recent_header { get; set; } = "FOMOD Creator";
        [JsonProperty("app-main-tab-name")] public string app_main_tab_name { get; set; } = "Welcome!";
        [JsonProperty("tree-plugin-tooltip")] public string tree_plugin_tooltip { get; set; } = "Plagin";
        [JsonProperty("tree-group-tooltip")] public string tree_group_tooltip { get; set; } = "Group";
        [JsonProperty("tree-install-step-tooltip")] public string tree_install_step_tooltip { get; set; } = "Install Step";
        [JsonProperty("tree-project-root-tooltip")] public string tree_project_root_tooltip { get; set; } = "Project Root";
        [JsonProperty("dependencyPattern-button-change-type")] public string dependencyPattern_button_change_type { get; set; } = "Change type";
        [JsonProperty("conditionalFileInstall-button-refresh")] public string conditionalFileInstall_button_refresh { get; set; } = "Refresh patterns numbers";
        [JsonProperty("dependencyPattern-button-refresh")] public string dependencyPattern_button_refresh { get; set; } = "Refresh patterns numbers";
        [JsonProperty("group-info")] public string group_info { get; set; } = "Group Info";
        [JsonProperty("group-name")] public string group_name { get; set; } = "Group name";
        [JsonProperty("group-type")] public string group_type { get; set; } = "Group type";
        [JsonProperty("group-other-settings")] public string group_other_settings { get; set; } = "Other Settings";
        [JsonProperty("group-plugins-order")] public string group_plugins_order { get; set; } = "Plugins Order";
        [JsonProperty("installStep-info")] public string installStep_info { get; set; } = "Install Step Info";
        [JsonProperty("installStep-step-name")] public string installStep_step_name { get; set; } = "Step Name";
        [JsonProperty("installStep-other-settings")] public string installStep_other_settings { get; set; } = "Other Settings";
        [JsonProperty("installStep-groups-order")] public string installStep_groups_order { get; set; } = "Groups Order";
        [JsonProperty("compositeDependency-title")] public string compositeDependency_title { get; set; } = "Composite dependency";
        [JsonProperty("compositeDependency-operator")] public string compositeDependency_operator { get; set; } = "Operator";
        [JsonProperty("compositeDependency-files-dependency")] public string compositeDependency_files_dependency { get; set; } = "Files dependency";
        [JsonProperty("compositeDependency-file-status")] public string compositeDependency_file_status { get; set; } = "Status";
        [JsonProperty("compositeDependency-file-name")] public string compositeDependency_file_name { get; set; } = "File name";
        [JsonProperty("compositeDependency-flag-dependency")] public string compositeDependency_flag_dependency { get; set; } = "Flag dependency";
        [JsonProperty("compositeDependency-flag-value")] public string compositeDependency_flag_value { get; set; } = "Value";
        [JsonProperty("compositeDependency-flag-name")] public string compositeDependency_flag_name { get; set; } = "Flag Name";
        [JsonProperty("plugin-info")] public string plugin_info { get; set; } = "Plugin info";
        [JsonProperty("plugin-name")] public string plugin_name { get; set; } = "Plugin name";
        [JsonProperty("plugin-description")] public string plugin_description { get; set; } = "Description";
        [JsonProperty("plugin-image")] public string plugin_image { get; set; } = "Image";
        [JsonProperty("plugin-files-and-folders")] public string plugin_files_and_folders { get; set; } = "Files and folders";
        [JsonProperty("plugin-condition-flags")] public string plugin_condition_flags { get; set; } = "Condition Flags";
        [JsonProperty("image-path")] public string image_path { get; set; } = "Path";
        [JsonProperty("image-show")] public string image_show { get; set; } = "Show Image";
        [JsonProperty("image-height")] public string image_height { get; set; } = "Height";
        [JsonProperty("image-show-fade")] public string image_show_fade { get; set; } = "Show Fade";
        [JsonProperty("fileList-destination-button-ok")] public string fileList_destination_button_ok { get; set; } = "OK";
        [JsonProperty("fileList-source")] public string fileList_source { get; set; } = "Source";
        [JsonProperty("fileList-destination")] public string fileList_destination { get; set; } = "Destination";
        [JsonProperty("fileList-always-install")] public string fileList_always_install { get; set; } = "Always Install";
        [JsonProperty("fileList-install-if-usable")] public string fileList_install_if_usable { get; set; } = "Install If Usable";
        [JsonProperty("fileList-priority")] public string fileList_priority { get; set; } = "Priority";
        [JsonProperty("conditionFlag-value")] public string conditionFlag_value { get; set; } = "Value";
        [JsonProperty("conditionFlag-name")] public string conditionFlag_name { get; set; } = "Flag Name";
        [JsonProperty("pluginTypeDescriptor-title")] public string pluginTypeDescriptor_title { get; set; } = "Plugin type";
        [JsonProperty("pluginTypeDescriptor-plugin-type")] public string pluginTypeDescriptor_plugin_type { get; set; } = "Plugin type";
        [JsonProperty("pluginTypeDescriptor-dependency-type")] public string pluginTypeDescriptor_dependency_type { get; set; } = "Dependency Type";
        [JsonProperty("pluginTypeDescriptor-default-plugin-type")] public string pluginTypeDescriptor_default_plugin_type { get; set; } = "Default plugin type";
        [JsonProperty("dependencyPattern-plugin-type")] public string dependencyPattern_plugin_type { get; set; } = "Plugin type*";
        [JsonProperty("dependencyPattern-pattern")] public string dependencyPattern_pattern { get; set; } = "Pattern";
        [JsonProperty("dependencyPattern-dependency-type")] public string dependencyPattern_dependency_type { get; set; } = "Dependency Type";
        [JsonProperty("project-mod-info-settings")] public string project_mod_info_settings { get; set; } = "Mod Info Settings";
        [JsonProperty("project-module-name")] public string project_module_name { get; set; } = "Module name";
        [JsonProperty("project-author")] public string project_author { get; set; } = "Author";
        [JsonProperty("project-version")] public string project_version { get; set; } = "Version";
        [JsonProperty("project-website")] public string project_website { get; set; } = "Website";
        [JsonProperty("project-category")] public string project_category { get; set; } = "Category";
        [JsonProperty("project-description")] public string project_description { get; set; } = "Description";
        [JsonProperty("project-module-title")] public string project_module_title { get; set; } = "Module Title Settings";
        [JsonProperty("project-module-title-field")] public string project_module_title_field { get; set; } = "Title";
        [JsonProperty("project-color")] public string project_color { get; set; } = "Color";
        [JsonProperty("project-position")] public string project_position { get; set; } = "Position";
        [JsonProperty("project-other-settings-title")] public string project_other_settings_title { get; set; } = "Other Settings";
        [JsonProperty("project-install-step-order")] public string project_install_step_order { get; set; } = "InstallStep Order";
        [JsonProperty("project-image")] public string project_image { get; set; } = "Image";
        [JsonProperty("project-files-and-folder-title")] public string project_files_and_folder_title { get; set; } = "Files and folders";
        [JsonProperty("conditionalFileInstall-title")] public string conditionalFileInstall_title { get; set; } = "Conditional File Installs";
        [JsonProperty("conditionalFileInstall-pattern")] public string conditionalFileInstall_pattern { get; set; } = "Pattern";
        [JsonProperty("conditionalFileInstall-file-list-title")] public string conditionalFileInstall_file_list_title { get; set; } = "File list";
        [JsonProperty("group-info-tooltip")] public string group_info_tooltip { get; set; } = "with out description";
        [JsonProperty("group-name-tooltip")] public string group_name_tooltip { get; set; } = "with out description";
        [JsonProperty("group-type-tooltip")] public string group_type_tooltip { get; set; } = "with out description";
        [JsonProperty("group-other-settings-tooltip")] public string group_other_settings_tooltip { get; set; } = "with out description";
        [JsonProperty("group-plugins-order-tooltip")] public string group_plugins_order_tooltip { get; set; } = "with out description";
        [JsonProperty("installStep-info-tooltip")] public string installStep_info_tooltip { get; set; } = "with out description";
        [JsonProperty("installStep-step-name-tooltip")] public string installStep_step_name_tooltip { get; set; } = "with out description";
        [JsonProperty("installStep-other-settings-tooltip")] public string installStep_other_settings_tooltip { get; set; } = "with out description";
        [JsonProperty("installStep-groups-order-tooltip")] public string installStep_groups_order_tooltip { get; set; } = "with out description";
        [JsonProperty("compositeDependency-title-tooltip")] public string compositeDependency_title_tooltip { get; set; } = "with out description";
        [JsonProperty("compositeDependency-operator-tooltip")] public string compositeDependency_operator_tooltip { get; set; } = "with out description";
        [JsonProperty("compositeDependency-files-dependency-tooltip")] public string compositeDependency_files_dependency_tooltip { get; set; } = "with out description";
        [JsonProperty("compositeDependency-file-status-tooltip")] public string compositeDependency_file_status_tooltip { get; set; } = "with out description";
        [JsonProperty("compositeDependency-file-name-tooltip")] public string compositeDependency_file_name_tooltip { get; set; } = "with out description";
        [JsonProperty("compositeDependency-flag-dependency-tooltip")] public string compositeDependency_flag_dependency_tooltip { get; set; } = "with out description";
        [JsonProperty("compositeDependency-flag-value-tooltip")] public string compositeDependency_flag_value_tooltip { get; set; } = "with out description";
        [JsonProperty("compositeDependency-flag-name-tooltip")] public string compositeDependency_flag_name_tooltip { get; set; } = "with out description";
        [JsonProperty("plugin-info-tooltip")] public string plugin_info_tooltip { get; set; } = "with out description";
        [JsonProperty("plugin-name-tooltip")] public string plugin_name_tooltip { get; set; } = "with out description";
        [JsonProperty("plugin-description-tooltip")] public string plugin_description_tooltip { get; set; } = "with out description";
        [JsonProperty("plugin-image-tooltip")] public string plugin_image_tooltip { get; set; } = "with out description";
        [JsonProperty("plugin-files-and-folders-tooltip")] public string plugin_files_and_folders_tooltip { get; set; } = "with out description";
        [JsonProperty("plugin-condition-flags-tooltip")] public string plugin_condition_flags_tooltip { get; set; } = "with out description";
        [JsonProperty("image-path-tooltip")] public string image_path_tooltip { get; set; } = "with out description";
        [JsonProperty("image-show-tooltip")] public string image_show_tooltip { get; set; } = "with out description";
        [JsonProperty("image-height-tooltip")] public string image_height_tooltip { get; set; } = "with out description";
        [JsonProperty("image-show-fade-tooltip")] public string image_show_fade_tooltip { get; set; } = "with out description";
        [JsonProperty("fileList-destination-button-ok-tooltip")] public string fileList_destination_button_ok_tooltip { get; set; } = "with out description";
        [JsonProperty("fileList-source-tooltip")] public string fileList_source_tooltip { get; set; } = "with out description";
        [JsonProperty("fileList-destination-tooltip")] public string fileList_destination_tooltip { get; set; } = "with out description";
        [JsonProperty("fileList-always-install-tooltip")] public string fileList_always_install_tooltip { get; set; } = "with out description";
        [JsonProperty("fileList-install-if-usable-tooltip")] public string fileList_install_if_usable_tooltip { get; set; } = "with out description";
        [JsonProperty("fileList-priority-tooltip")] public string fileList_priority_tooltip { get; set; } = "with out description";
        [JsonProperty("conditionFlag-value-tooltip")] public string conditionFlag_value_tooltip { get; set; } = "with out description";
        [JsonProperty("conditionFlag-name-tooltip")] public string conditionFlag_name_tooltip { get; set; } = "with out description";
        [JsonProperty("pluginTypeDescriptor-title-tooltip")] public string pluginTypeDescriptor_title_tooltip { get; set; } = "with out description";
        [JsonProperty("pluginTypeDescriptor-plugin-type-tooltip")] public string pluginTypeDescriptor_plugin_type_tooltip { get; set; } = "with out description";
        [JsonProperty("pluginTypeDescriptor-dependency-type-tooltip")] public string pluginTypeDescriptor_dependency_type_tooltip { get; set; } = "with out description";
        [JsonProperty("pluginTypeDescriptor-default-plugin-type-tooltip")] public string pluginTypeDescriptor_default_plugin_type_tooltip { get; set; } = "with out description";
        [JsonProperty("dependencyPattern-plugin-type-tooltip")] public string dependencyPattern_plugin_type_tooltip { get; set; } = "with out description";
        [JsonProperty("dependencyPattern-pattern-tooltip")] public string dependencyPattern_pattern_tooltip { get; set; } = "with out description";
        [JsonProperty("dependencyPattern-dependency-type-tooltip")] public string dependencyPattern_dependency_type_tooltip { get; set; } = "with out description";
        [JsonProperty("project-mod-info-settings-tooltip")] public string project_mod_info_settings_tooltip { get; set; } = "with out description";
        [JsonProperty("project-module-name-tooltip")] public string project_module_name_tooltip { get; set; } = "with out description";
        [JsonProperty("project-author-tooltip")] public string project_author_tooltip { get; set; } = "with out description";
        [JsonProperty("project-version-tooltip")] public string project_version_tooltip { get; set; } = "with out description";
        [JsonProperty("project-website-tooltip")] public string project_website_tooltip { get; set; } = "with out description";
        [JsonProperty("project-category-tooltip")] public string project_category_tooltip { get; set; } = "with out description";
        [JsonProperty("project-description-tooltip")] public string project_description_tooltip { get; set; } = "with out description";
        [JsonProperty("project-module-title-tooltip")] public string project_module_title_tooltip { get; set; } = "with out description";
        [JsonProperty("project-module-title-field-tooltip")] public string project_module_title_field_tooltip { get; set; } = "with out description";
        [JsonProperty("project-color-tooltip")] public string project_color_tooltip { get; set; } = "with out description";
        [JsonProperty("project-position-tooltip")] public string project_position_tooltip { get; set; } = "with out description";
        [JsonProperty("project-other-settings-title-tooltip")] public string project_other_settings_title_tooltip { get; set; } = "with out description";
        [JsonProperty("project-install-step-order-tooltip")] public string project_install_step_order_tooltip { get; set; } = "with out description";
        [JsonProperty("project-image-tooltip")] public string project_image_tooltip { get; set; } = "with out description";
        [JsonProperty("project-files-and-folder-title-tooltip")] public string project_files_and_folder_title_tooltip { get; set; } = "with out description";
        [JsonProperty("conditionalFileInstall-title-tooltip")] public string conditionalFileInstall_title_tooltip { get; set; } = "with out description";
        [JsonProperty("conditionalFileInstall-pattern-tooltip")] public string conditionalFileInstall_pattern_tooltip { get; set; } = "with out description";
        [JsonProperty("conditionalFileInstall-file-list-title-tooltip")] public string conditionalFileInstall_file_list_title_tooltip { get; set; } = "with out description";
        [JsonProperty("ModuleTitlePosition-Left")] public string ModuleTitlePosition_Left { get; set; } = "Left";
        [JsonProperty("ModuleTitlePosition-Right")] public string ModuleTitlePosition_Right { get; set; } = "Right";
        [JsonProperty("ModuleTitlePosition-RightOfImage")] public string ModuleTitlePosition_RightOfImage { get; set; } = "Right of image";
        [JsonProperty("Boolean-True")] public string Boolean_True { get; set; } = "True";
        [JsonProperty("Boolean-False")] public string Boolean_False { get; set; } = "False";
        [JsonProperty("OrderEnum-Ascending")] public string OrderEnum_Ascending { get; set; } = "Ascending";
        [JsonProperty("OrderEnum-Descending")] public string OrderEnum_Descending { get; set; } = "Descending";
        [JsonProperty("OrderEnum-Explicit")] public string OrderEnum_Explicit { get; set; } = "Explicit";
        [JsonProperty("CompositeDependencyOperator-And")] public string CompositeDependencyOperator_And { get; set; } = "And";
        [JsonProperty("CompositeDependencyOperator-Or")] public string CompositeDependencyOperator_Or { get; set; } = "Or";
        [JsonProperty("FileDependencyState-Missing")] public string FileDependencyState_Missing { get; set; } = "Missing";
        [JsonProperty("FileDependencyState-Inactive")] public string FileDependencyState_Inactive { get; set; } = "Inactive";
        [JsonProperty("FileDependencyState-Active")] public string FileDependencyState_Active { get; set; } = "Active";
        [JsonProperty("GroupType-SelectAtLeastOne")] public string GroupType_SelectAtLeastOne { get; set; } = "Select At Least One";
        [JsonProperty("GroupType-SelectAtMostOne")] public string GroupType_SelectAtMostOne { get; set; } = "Select At Most One";
        [JsonProperty("GroupType-SelectExactlyOne")] public string GroupType_SelectExactlyOne { get; set; } = "Select Exactly One";
        [JsonProperty("GroupType-SelectAll")] public string GroupType_SelectAll { get; set; } = "Select All";
        [JsonProperty("GroupType-SelectAny")] public string GroupType_SelectAny { get; set; } = "Select Any";
        [JsonProperty("ItemsChoiceType-Dependencies")] public string ItemsChoiceType_Dependencies { get; set; } = "Dependencies";
        [JsonProperty("ItemsChoiceType-FileDependency")] public string ItemsChoiceType_FileDependency { get; set; } = "File dependency";
        [JsonProperty("ItemsChoiceType-FlagDependency")] public string ItemsChoiceType_FlagDependency { get; set; } = "Flag dependency";
        [JsonProperty("ItemsChoiceType-FommDependency")] public string ItemsChoiceType_FommDependency { get; set; } = "Fomm dependency";
        [JsonProperty("ItemsChoiceType-GameDependency")] public string ItemsChoiceType_GameDependency { get; set; } = "Game dependency";
        [JsonProperty("ItemsChoiceType1-File")] public string ItemsChoiceType1_File { get; set; } = "File";
        [JsonProperty("ItemsChoiceType1-Folder")] public string ItemsChoiceType1_Folder { get; set; } = "Folder";
        [JsonProperty("PluginTypeEnum-Required")] public string PluginTypeEnum_Required { get; set; } = "Required";
        [JsonProperty("PluginTypeEnum-Optional")] public string PluginTypeEnum_Optional { get; set; } = "Optional";
        [JsonProperty("PluginTypeEnum-Recommended")] public string PluginTypeEnum_Recommended { get; set; } = "Recommended";
        [JsonProperty("PluginTypeEnum-NotUsable")] public string PluginTypeEnum_NotUsable { get; set; } = "Not Usable";
        [JsonProperty("PluginTypeEnum-CouldBeUsable")] public string PluginTypeEnum_CouldBeUsable { get; set; } = "Could Be Usable";
        [JsonProperty("CategoriesEnum-Category1")] public string CategoriesEnum_Category1 { get; set; } = "Default";
        [JsonProperty("CategoriesEnum-Category2")] public string CategoriesEnum_Category2 { get; set; } = "Miscellaneous";
        [JsonProperty("CategoriesEnum-Category3")] public string CategoriesEnum_Category3 { get; set; } = "Ammo";
        [JsonProperty("CategoriesEnum-Category4")] public string CategoriesEnum_Category4 { get; set; } = "Animation";
        [JsonProperty("CategoriesEnum-Category5")] public string CategoriesEnum_Category5 { get; set; } = "Armour";
        [JsonProperty("CategoriesEnum-Category6")] public string CategoriesEnum_Category6 { get; set; } = "BugFixes";
        [JsonProperty("CategoriesEnum-Category7")] public string CategoriesEnum_Category7 { get; set; } = "Buildings";
        [JsonProperty("CategoriesEnum-Category8")] public string CategoriesEnum_Category8 { get; set; } = "CheatsandGoditems";
        [JsonProperty("CategoriesEnum-Category9")] public string CategoriesEnum_Category9 { get; set; } = "Clothing";
        [JsonProperty("CategoriesEnum-Category10")] public string CategoriesEnum_Category10 { get; set; } = "Collectibles,TreasureHunts,andPuzzles";
        [JsonProperty("CategoriesEnum-Category11")] public string CategoriesEnum_Category11 { get; set; } = "Companions";
        [JsonProperty("CategoriesEnum-Category12")] public string CategoriesEnum_Category12 { get; set; } = "Creatures";
        [JsonProperty("CategoriesEnum-Category13")] public string CategoriesEnum_Category13 { get; set; } = "ENBPresets";
        [JsonProperty("CategoriesEnum-Category14")] public string CategoriesEnum_Category14 { get; set; } = "Environment";
        [JsonProperty("CategoriesEnum-Category15")] public string CategoriesEnum_Category15 { get; set; } = "GameplayEffectsandChanges";
        [JsonProperty("CategoriesEnum-Category16")] public string CategoriesEnum_Category16 { get; set; } = "Factions";
        [JsonProperty("CategoriesEnum-Category17")] public string CategoriesEnum_Category17 { get; set; } = "HairandFaceModels";
        [JsonProperty("CategoriesEnum-Category18")] public string CategoriesEnum_Category18 { get; set; } = "ModdersResourcesandTutorials";
        [JsonProperty("CategoriesEnum-Category19")] public string CategoriesEnum_Category19 { get; set; } = "ModelsandTextures";
        [JsonProperty("CategoriesEnum-Category20")] public string CategoriesEnum_Category20 { get; set; } = "NewLands";
        [JsonProperty("CategoriesEnum-Category21")] public string CategoriesEnum_Category21 { get; set; } = "Locations-New";
        [JsonProperty("CategoriesEnum-Category22")] public string CategoriesEnum_Category22 { get; set; } = "NPC";
        [JsonProperty("CategoriesEnum-Category23")] public string CategoriesEnum_Category23 { get; set; } = "NPC-Vendors";
        [JsonProperty("CategoriesEnum-Category24")] public string CategoriesEnum_Category24 { get; set; } = "Overhauls";
        [JsonProperty("CategoriesEnum-Category25")] public string CategoriesEnum_Category25 { get; set; } = "Patches";
        [JsonProperty("CategoriesEnum-Category26")] public string CategoriesEnum_Category26 { get; set; } = "Performances";
        [JsonProperty("CategoriesEnum-Category27")] public string CategoriesEnum_Category27 { get; set; } = "Perks";
        [JsonProperty("CategoriesEnum-Category28")] public string CategoriesEnum_Category28 { get; set; } = "PlayerHomes";
        [JsonProperty("CategoriesEnum-Category29")] public string CategoriesEnum_Category29 { get; set; } = "Poses";
        [JsonProperty("CategoriesEnum-Category30")] public string CategoriesEnum_Category30 { get; set; } = "QuestsandAdventures";
        [JsonProperty("CategoriesEnum-Category31")] public string CategoriesEnum_Category31 { get; set; } = "Radio";
        [JsonProperty("CategoriesEnum-Category32")] public string CategoriesEnum_Category32 { get; set; } = "SavedGames/Characters";
        [JsonProperty("CategoriesEnum-Category33")] public string CategoriesEnum_Category33 { get; set; } = "Audio-SFX";
        [JsonProperty("CategoriesEnum-Category34")] public string CategoriesEnum_Category34 { get; set; } = "Audio-Music";
        [JsonProperty("CategoriesEnum-Category35")] public string CategoriesEnum_Category35 { get; set; } = "Audio-Misc";
        [JsonProperty("CategoriesEnum-Category36")] public string CategoriesEnum_Category36 { get; set; } = "Audio-Voice";
        [JsonProperty("CategoriesEnum-Category37")] public string CategoriesEnum_Category37 { get; set; } = "UserInterface";
        [JsonProperty("CategoriesEnum-Category38")] public string CategoriesEnum_Category38 { get; set; } = "Utilities";
        [JsonProperty("CategoriesEnum-Category39")] public string CategoriesEnum_Category39 { get; set; } = "Vehicles";
        [JsonProperty("CategoriesEnum-Category40")] public string CategoriesEnum_Category40 { get; set; } = "VisualsandGraphics";
        [JsonProperty("CategoriesEnum-Category41")] public string CategoriesEnum_Category41 { get; set; } = "Weapons";
        [JsonProperty("CategoriesEnum-Category42")] public string CategoriesEnum_Category42 { get; set; } = "WeaponsandArmour";
        [JsonProperty("CategoriesEnum-Category43")] public string CategoriesEnum_Category43 { get; set; } = "Items-Food/Drinks/Chems/etc";
        [JsonProperty("CategoriesEnum-Category44")] public string CategoriesEnum_Category44 { get; set; } = "Crafting-Equipment";
        [JsonProperty("CategoriesEnum-Category45")] public string CategoriesEnum_Category45 { get; set; } = "Crafting-Home";
        [JsonProperty("CategoriesEnum-Category46")] public string CategoriesEnum_Category46 { get; set; } = "SkillsandLeveling";
        [JsonProperty("CategoriesEnum-Category47")] public string CategoriesEnum_Category47 { get; set; } = "Locations-Vanilla";
        [JsonProperty("CategoriesEnum-Category48")] public string CategoriesEnum_Category48 { get; set; } = "PlayerSettlement";
        [JsonProperty("CategoriesEnum-Category49")] public string CategoriesEnum_Category49 { get; set; } = "Clothing-Backpacks";
        [JsonProperty("CategoriesEnum-Category50")] public string CategoriesEnum_Category50 { get; set; } = "Crafting-Other";
        [JsonProperty("CategoriesEnum-Category51")] public string CategoriesEnum_Category51 { get; set; } = "Immersion";
        [JsonProperty("CategoriesEnum-Category52")] public string CategoriesEnum_Category52 { get; set; } = "Pip-boy";
        [JsonProperty("CategoriesEnum-Category53")] public string CategoriesEnum_Category53 { get; set; } = "PowerArmour";
        [JsonProperty("CategoriesEnum-Category54")] public string CategoriesEnum_Category54 { get; set; } = "Immersion";
        [JsonProperty("CategoriesEnum-Category55")] public string CategoriesEnum_Category55 { get; set; } = "ReShadePresets";
        [JsonProperty("CategoriesEnum-Category56")] public string CategoriesEnum_Category56 { get; set; } = "Weather";
        [JsonProperty("CategoriesEnum-Category57")] public string CategoriesEnum_Category57 { get; set; } = "Tattoos";

    }
}
