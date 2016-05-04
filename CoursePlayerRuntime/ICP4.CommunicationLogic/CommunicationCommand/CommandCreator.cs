using System;
using System.Collections.Generic;
using System.Text;



namespace ICP4.CommunicationLogic.CommunicationCommand
{
    public static class CommandCreator
    {
        /*
        public static CommunicationCommand.BAL.ShowTableofContent.ShowTableofContent CreateTOC(List<ModuleEntity> modules)
        {
        

            CommunicationCommand.BAL.ShowTableofContent.ShowTableofContent tableofContent = new CommunicationCommand.BAL.ShowTableofContent.ShowTableofContent();
            CommunicationCommand.BAL.ShowTableofContent.TableContentLesson tableofContentEntry = new CommunicationCommand.BAL.ShowTableofContent.TableContentLesson();
            List<CommunicationCommand.BAL.ShowTableofContent.TableContentLesson> lessons = new List<CommunicationCommand.BAL.ShowTableofContent.TableContentLesson>();
            try
            {
                foreach(ModuleEntity module in modules)
                {
                    foreach(LessonEntity lesson in module.LessonEntityList)
                    {
                        tableofContentEntry = new CommunicationCommand.BAL.ShowTableofContent.TableContentLesson();
                        tableofContentEntry.LessonID = lesson.LessonID;
                        tableofContentEntry.LessonName = lesson.Title; 
                        lessons.Add(tableofContentEntry); 

                    }
                }

                tableofContent.CommandName = CommunicationCommand.BAL.CommandNames.ShowTableofContent;  
                tableofContent.Lessons = lessons;
            }
            catch(Exception ex)
            {
                tableofContent = null;
                //ExceptionHandler.HandleException(ex);
            }
            return tableofContent;
        }

        public static CommunicationCommand.BAL.ShowCourseInfo.ShowCourseInfo GetCourseInfo(string courseTitle)
        {
            CommunicationCommand.BAL.ShowCourseInfo.ShowCourseInfo showCourseInfo = new CommunicationCommand.BAL.ShowCourseInfo.ShowCourseInfo();
            CommunicationCommand.BAL.ShowCourseInfo.CourseInfo courseInfo  = new CommunicationCommand.BAL.ShowCourseInfo.CourseInfo();

            courseInfo.CourseName = courseTitle;
            //courseInfo.CourseTimeUnit = "Hours";
            //courseInfo.TotalCourseTime = 99;
            //courseInfo.TotalNoOfAssessmentQuestions = CourseEntity.AssessmentList.Count;   
  

            showCourseInfo.CommandName = CommunicationCommand.BAL.CommandNames.ShowCourseInfo;    
            showCourseInfo.CourseInfo = courseInfo;

            return showCourseInfo;


        }

        public static CommunicationCommand.BAL.ShowSupplementalMaterial.ShowSupplementalMaterial GetSupplimentalMaterial(List<SupplementalMaterialEntity> supplimentalMaterialList)
        {
            CommunicationCommand.BAL.ShowSupplementalMaterial.ShowSupplementalMaterial showSupplimentalMaterial = new CommunicationCommand.BAL.ShowSupplementalMaterial.ShowSupplementalMaterial();
            CommunicationCommand.BAL.ShowSupplementalMaterial.SupplementalMaterial supplimentalMaterial = null;
            List<CommunicationCommand.BAL.ShowSupplementalMaterial.SupplementalMaterial> supplimentalMaterials = new List<CommunicationCommand.BAL.ShowSupplementalMaterial.SupplementalMaterial>() ;

     
            foreach(SupplementalMaterialEntity supplimentalEntity in supplimentalMaterialList)
            {
                supplimentalMaterial = new CommunicationCommand.BAL.ShowSupplementalMaterial.SupplementalMaterial();
                supplimentalMaterial.SupplementalMaterialID = supplimentalEntity.SupplementalMaterialID;
                supplimentalMaterial.SupplementalMaterialTitle = supplimentalEntity.Title; 
                supplimentalMaterials.Add(supplimentalMaterial); 
            }
            showSupplimentalMaterial.CommandName = CommunicationCommand.BAL.CommandNames.ShowSupplementalMaterial;    
            showSupplimentalMaterial.SupplimentalMaterials = supplimentalMaterials;

            return showSupplimentalMaterial; 

            
        }

        public static CommunicationCommand.BAL.ShowGlossary.ShowGlossary GetGlossary(List<GlossaryEntity> glossaryList)
        {
            CommunicationCommand.BAL.ShowGlossary.ShowGlossary showGlossary = new CommunicationCommand.BAL.ShowGlossary.ShowGlossary();
            CommunicationCommand.BAL.ShowGlossary.Glossary glossary = null;
            List<CommunicationCommand.BAL.ShowGlossary.Glossary> glossaries = new List<CommunicationCommand.BAL.ShowGlossary.Glossary>() ;

            foreach(GlossaryEntity glossaryEntity in glossaryList)
            {
                glossary = new CommunicationCommand.BAL.ShowGlossary.Glossary();
                glossary.GlossaryID = glossaryEntity.GlossaryID; 
                glossary.Term = glossaryEntity.Term; 
                glossaries.Add(glossary); 
            }
            showGlossary.CommandName = CommunicationCommand.BAL.CommandNames.ShowGlossary;    
            showGlossary.Glossaries = glossaries;

            return showGlossary;
        }

        public static CommunicationCommand.BAL.ShowErrorMessage.ShowErrorMessage GetErrorMessage(string errorMessegeString)
        {
        
            CommunicationCommand.BAL.ShowErrorMessage.ShowErrorMessage showErrorMessage = new CommunicationCommand.BAL.ShowErrorMessage.ShowErrorMessage();
            CommunicationCommand.BAL.ShowErrorMessage.ErrorMessage errorMessage = new CommunicationCommand.BAL.ShowErrorMessage.ErrorMessage();

            errorMessage.ErrorMessageText = errorMessegeString;

            showErrorMessage.CommandName = CommunicationCommand.BAL.CommandNames.ShowErrorMessage;    
            showErrorMessage.ErrorMessage  = errorMessage;


            return showErrorMessage;
        }

        public static CommunicationCommand.BAL.ShowSlide.ShowSlide GetNextBackSlide(string itemId,int parentItemId,int lessonNo,ItemType itemType,string slidePath)
        {
        
            CommunicationCommand.BAL.ShowSlide.ShowSlide showSlide  = new CommunicationCommand.BAL.ShowSlide.ShowSlide();
            CommunicationCommand.BAL.ShowSlide.SlideMediaAsset slideMediaAsset = new CommunicationCommand.BAL.ShowSlide.SlideMediaAsset();
            List<CommunicationCommand.BAL.ShowSlide.SlideMediaAsset> mediaAssets = new List<CommunicationCommand.BAL.ShowSlide.SlideMediaAsset>();

            slideMediaAsset.MediaAssetID = itemId;
            slideMediaAsset.LessonID = parentItemId;
            slideMediaAsset.LessonNo = lessonNo;
            slideMediaAsset.MediaAssetURL = slidePath;

            mediaAssets.Add (slideMediaAsset);

            showSlide.CommandName = CommunicationCommand.BAL.CommandNames.ShowSlide ;  
            showSlide.MediaAssets = mediaAssets;


            return showSlide;
        }

        public static CommunicationCommand.BAL.ShowSlide.ShowSlide GetBookMarkedSlide(Sequence sequenceItem,CommunicationCommand.BAL.AddBookMark.ClientBookMark clientBookMark)
        {
        
            CommunicationCommand.BAL.ShowSlide.ShowSlide showSlide  = new CommunicationCommand.BAL.ShowSlide.ShowSlide();
            CommunicationCommand.BAL.ShowSlide.SlideMediaAsset slideMediaAsset = new CommunicationCommand.BAL.ShowSlide.SlideMediaAsset();
            List<CommunicationCommand.BAL.ShowSlide.SlideMediaAsset> mediaAssets = new List<CommunicationCommand.BAL.ShowSlide.SlideMediaAsset>();

            slideMediaAsset.MediaAssetID = sequenceItem.ItemId;
            slideMediaAsset.LessonID = sequenceItem.ParentItemId;
            slideMediaAsset.LessonNo = sequenceItem.LessonNo;
            slideMediaAsset.MediaAssetURL = sequenceItem.SlidePath ;
            slideMediaAsset.IsMovieEnded = clientBookMark.IsMovieEnded;
            slideMediaAsset.LastScene = clientBookMark.LastScene;
            slideMediaAsset.NextButtonState = clientBookMark.NextButtonState;
            slideMediaAsset.SceneNo = clientBookMark.SceneNo;

            mediaAssets.Add (slideMediaAsset);

            showSlide.CommandName = CommunicationCommand.BAL.CommandNames.ShowSlide ;  
            showSlide.MediaAssets = mediaAssets;


            return showSlide;
        }

        public static CommunicationCommand.BAL.ShowSMEBioInfo.ShowSMEBioInfo GetSmeBioInfo(string smeBioText,string smeSlide)
        {

            CommunicationCommand.BAL.ShowSMEBioInfo.ShowSMEBioInfo showSMEBioInfo = new   CommunicationCommand.BAL.ShowSMEBioInfo.ShowSMEBioInfo();
            CommunicationCommand.BAL.ShowSMEBioInfo.SMEBioInfo smeBioInfo = new CommunicationCommand.BAL.ShowSMEBioInfo.SMEBioInfo();
            showSMEBioInfo.CommandName = CommunicationCommand.BAL.CommandNames.ShowSmeBioInfo;  
            
            smeBioInfo.SmeBioText = smeBioText;
            smeBioInfo.SmeBioSlide = smeSlide;

            
            showSMEBioInfo.SMEBioInfo = smeBioInfo;

            return showSMEBioInfo;

        
        }

        public static CommunicationCommand.BAL.ShowCourseObjective.ShowCourseObjective GetCourseObjectiveInfo(string courseObjectiveText,string courseObjectiveSlide)
        {

            CommunicationCommand.BAL.ShowCourseObjective.ShowCourseObjective showCourseObjective = new CommunicationCommand.BAL.ShowCourseObjective.ShowCourseObjective();
            CommunicationCommand.BAL.ShowCourseObjective.CourseObjectiveInfo courseObjectiveInfo = new CommunicationCommand.BAL.ShowCourseObjective.CourseObjectiveInfo();
            
            showCourseObjective.CommandName = CommunicationCommand.BAL.CommandNames.ShowCourseObjective;
            
            courseObjectiveInfo.CourseObjectiveText = courseObjectiveText;
            courseObjectiveInfo.CourseObjectiveSlide = courseObjectiveSlide;

            
            showCourseObjective.CourseObjectiveInfo = courseObjectiveInfo;

            return showCourseObjective;

        
        }

        public static CommunicationCommand.BAL.CourseIdleTimeInfo.CourseIdleTimeInfo GetCourseIdleTimerInfo(int courseIdleTimeInMinutes)
        {

            CommunicationCommand.BAL.CourseIdleTimeInfo.CourseIdleTimeInfo courseIdleTimeInfo = new CommunicationCommand.BAL.CourseIdleTimeInfo.CourseIdleTimeInfo();
            CommunicationCommand.BAL.CourseIdleTimeInfo.IdleTime idleTime = new CommunicationCommand.BAL.CourseIdleTimeInfo.IdleTime();
            
            courseIdleTimeInfo.CommandName  = CommunicationCommand.BAL.CommandNames.CourseIdleTimeInfo;
            
            idleTime.CourseIdleTimeInMinutes = courseIdleTimeInMinutes;
            

            
            courseIdleTimeInfo.IdleTime  = idleTime;

            return courseIdleTimeInfo;

        
        }

        public static CommunicationCommand.BAL.CourseRunTimeInfo.CourseRunTimeInfo GetCourseRunTimerInfo(int courseRunTimeInMinutes)
        {

            CommunicationCommand.BAL.CourseRunTimeInfo.CourseRunTimeInfo courseRunTimeInfo = new CommunicationCommand.BAL.CourseRunTimeInfo.CourseRunTimeInfo();
            CommunicationCommand.BAL.CourseRunTimeInfo.CourseTime courseTime = new CommunicationCommand.BAL.CourseRunTimeInfo.CourseTime();
            
            courseRunTimeInfo.CommandName  = CommunicationCommand.BAL.CommandNames.CourseRunTimeInfo;
            
            courseTime.CourseRunTimeInMinutes = courseRunTimeInMinutes;

            courseRunTimeInfo.CourseTime  = courseTime;

            return courseRunTimeInfo;

        
        }

        public static CommunicationCommand.BAL.CourseExpireTimeInfo.CourseExpireTimeInfo GetCourseExpireTimerInfo(int courseExpireTimeInMinutes)
        {

            CommunicationCommand.BAL.CourseExpireTimeInfo.CourseExpireTimeInfo courseExpireTimeInfo = new CommunicationCommand.BAL.CourseExpireTimeInfo.CourseExpireTimeInfo();
            CommunicationCommand.BAL.CourseExpireTimeInfo.ExpireTime expireTime = new CommunicationCommand.BAL.CourseExpireTimeInfo.ExpireTime();
            
            courseExpireTimeInfo.CommandName  = CommunicationCommand.BAL.CommandNames.CourseExpireTimeInfo;
            
            expireTime.CourseExpireTimeInMinutes = courseExpireTimeInMinutes;

            courseExpireTimeInfo.ExpireTime  = expireTime;

            return courseExpireTimeInfo;

        
        }

        public static CommunicationCommand.BAL.ShowAnimatedMessage.ShowAnimatedMessage GetAnimatedMessage(string text)
        {

            CommunicationCommand.BAL.ShowAnimatedMessage.ShowAnimatedMessage showAnimatedMessage = new CommunicationCommand.BAL.ShowAnimatedMessage.ShowAnimatedMessage();
            CommunicationCommand.BAL.ShowAnimatedMessage.AnimatedMessage animatedMessage = new CommunicationCommand.BAL.ShowAnimatedMessage.AnimatedMessage();
            
            showAnimatedMessage.CommandName  = CommunicationCommand.BAL.CommandNames.ShowAnimatedMessage; 
            
            animatedMessage.AnimatedMessageURL = "";
            animatedMessage.ShowCancelButton = false;
            animatedMessage.SupportedTextMessage = text;

            showAnimatedMessage.AnimatedMessage  = animatedMessage;

            return showAnimatedMessage;

        
        }

        public static CommunicationCommand.BAL.ShowSupplementalMaterialInDetail.ShowSupplementalMaterialInDetail GetSupplementalMaterialInDetail(SupplementalMaterialEntity supplimentalEntity)
        {
            CommunicationCommand.BAL.ShowSupplementalMaterialInDetail.ShowSupplementalMaterialInDetail showSupplimentalMaterialInDetail = new CommunicationCommand.BAL.ShowSupplementalMaterialInDetail.ShowSupplementalMaterialInDetail();
            CommunicationCommand.BAL.ShowSupplementalMaterialInDetail.SupplementalMaterialDetail supplimentalMaterialDetail = new CommunicationCommand.BAL.ShowSupplementalMaterialInDetail.SupplementalMaterialDetail();
            
            CommunicationCommand.BAL.ShowGraphic.MediaGraphic mediaGraphic = null;
            

            showSupplimentalMaterialInDetail.CommandName = CommunicationCommand.BAL.CommandNames.ShowSupplementalMaterialInDetail;
            

                        supplimentalMaterialDetail.SupplementalMaterialID = supplimentalEntity.SupplementalMaterialID;
                        supplimentalMaterialDetail.SupplementalMaterialText  = supplimentalEntity.Description;
                        supplimentalMaterialDetail.SupplementalMaterialTitle  = supplimentalEntity.Title ;
                        supplimentalMaterialDetail.MediaGraphics = new  List<CommunicationCommand.BAL.ShowGraphic.MediaGraphic>();
                        foreach(ContentFileEntity mediaAsset in supplimentalEntity.ContentFiles)
                        {
                            mediaGraphic = new CommunicationCommand.BAL.ShowGraphic.MediaGraphic();    
                            mediaGraphic.GraphicID = supplimentalEntity.ID; 
                            mediaGraphic.GraphicTitle = supplimentalEntity.Description;
                            mediaGraphic.GraphicType = mediaAsset.Type;
                            mediaGraphic.GraphicURL = mediaAsset.Path; 
                            supplimentalMaterialDetail.MediaGraphics.Add(mediaGraphic); 
                        }
                    
                    

            showSupplimentalMaterialInDetail.SupplimentalMaterialDetail = supplimentalMaterialDetail;
            return showSupplimentalMaterialInDetail;
            
        }

        public static CommunicationCommand.BAL.ShowGlossaryInDetail.ShowGlossaryInDetail GetGlossaryInDetail(int glossaryID,string definition)
        {
            CommunicationCommand.BAL.ShowGlossaryInDetail.ShowGlossaryInDetail showGlossaryInDetail = new CommunicationCommand.BAL.ShowGlossaryInDetail.ShowGlossaryInDetail();
            CommunicationCommand.BAL.ShowGlossaryInDetail.GlossaryDetail glossaryDetail = new CommunicationCommand.BAL.ShowGlossaryInDetail.GlossaryDetail();
            
      
            showGlossaryInDetail.CommandName = CommunicationCommand.BAL.CommandNames.ShowGlossaryInDetail;
            
            glossaryDetail.GlossaryID  = glossaryID;
            glossaryDetail.GlossaryDefinition  = definition;

            showGlossaryInDetail.GlossaryDetail = glossaryDetail;
            return showGlossaryInDetail;
            
        }

        public static CommunicationCommand.BAL.ShowBookMark.ShowBookMark GetShowBookMark(List<CommunicationCommand.BAL.ShowBookMark.BookMark> bookMarkList)
        {
            CommunicationCommand.BAL.ShowBookMark.ShowBookMark showBookMark = new CommunicationCommand.BAL.ShowBookMark.ShowBookMark ();
            
      
            showBookMark.CommandName = CommunicationCommand.BAL.CommandNames.ShowBookMark;
            
            showBookMark.BookMarks = bookMarkList;

            
            return showBookMark;
            
        }
        */
    }
}
