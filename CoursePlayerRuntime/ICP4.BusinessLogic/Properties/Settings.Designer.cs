﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5420
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ICP4.BusinessLogic.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://kar-dev-3/vu360/vu3/icp4/LCMS_VUConnectorService.cfc")]
        public string ICP4_BusinessLogic_LCMS_VUConnectorService_LCMS_VUConnectorServiceService {
            get {
                return ((string)(this["ICP4_BusinessLogic_LCMS_VUConnectorService_LCMS_VUConnectorServiceService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://kar-dev-3/vu3/ICP4/ValidationQuestions.cfc")]
        public string ICP4_BusinessLogic_LegacyVUValidationQuestionService_ValidationQuestionsService {
            get {
                return ((string)(this["ICP4_BusinessLogic_LegacyVUValidationQuestionService_ValidationQuestionsService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://10.0.100.152:8080/lms/service/lms-lcms/")]
        public string ICP4_BusinessLogic_LearningSessionComplete_lmsLcmsService {
            get {
                return ((string)(this["ICP4_BusinessLogic_LearningSessionComplete_lmsLcmsService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://10.0.100.179/iCPBrandingService/BrandingService.asmx")]
        public string ICP4_BusinessLogic_ICPBrandingService_BrandingService {
            get {
                return ((string)(this["ICP4_BusinessLogic_ICPBrandingService_BrandingService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://10.0.100.179/iCPAssessmentService/AssessmentService.asmx")]
        public string ICP4_BusinessLogic_ICPAssessmentService_AssessmentService {
            get {
                return ((string)(this["ICP4_BusinessLogic_ICPAssessmentService_AssessmentService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:50061/TrackingService.asmx")]
        public string ICP4_BusinessLogic_ICPTrackingService_TrackingService {
            get {
                return ((string)(this["ICP4_BusinessLogic_ICPTrackingService_TrackingService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://10.0.100.179/ICPCourseService/CourseService.asmx")]
        public string ICP4_BusinessLogic_ICPCourseService_CourseService {
            get {
                return ((string)(this["ICP4_BusinessLogic_ICPCourseService_CourseService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:58412/PlayerUtility.asmx")]
        public string ICP4_BusinessLogic_ICP4PlayerUtility_PlayerUtility {
            get {
                return ((string)(this["ICP4_BusinessLogic_ICP4PlayerUtility_PlayerUtility"]));
            }
        }
    }
}
