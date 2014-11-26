CustomRichTextBoxWithHighligh
=============================

A custom richtextbox with highlight functionality along with multiple color changes
There is requirement from the client has following complex scenario

There are 2 mode in a application

Read Only mode:

 If "NumberOfReadOnlyCharacter" (e.x first 50 characters)  exist, then those character should be in "Red " color, and those characters should not be editable, other characters should be in "Block" color

https://sites.google.com/site/greateindiaclub/mobil-apps/windows8/_draft_post/ReadOnly.png


Read only mode


If user try to search some text then those text should be highlighted in "Black" color and remaining text should  change into
" Gray" color

https://sites.google.com/site/greateindiaclub/mobil-apps/windows8/_draft_post/SearchMode.png

Search Mode


Edit mode:

In edit mode read only text should be in "Red" color and should not be editable and remaining text should turn to be "Green" color, so that user can append the text at the end.

CaretPosition should not go to read only text and selection is not allowed for read only text


https://sites.google.com/site/greateindiaclub/mobil-apps/windows8/_draft_post/EditMode.png



Solution:

To solve this complex issue, i'm end of with writing my own custom Richtext box

            <UserControls:PORichTextBox  
                RTHeight="200" 
                Text="Welcome to Greate India Club. We are here to deliver quality of software related information from all over the best software professionals working in various MNC companies. You can also share and post your projects and your valuable articles by joining in this group. We are very happy to welcome all the students and professionals to create best community.Thanks and Regards,Boobalan Munusamy. "
                NumberOfReadOnlyCharacter="50"
                IsEditMode="{Binding IsRTBEnabled, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged, ElementName=MainPage }"
                SearchText="{Binding Path=Text, ElementName=TxtSearch}"     
                IsRedColorForReadOnly="True"
                                         />



RTHeight= Usercontrol height
Text= Text should be displayed at RTB
NumberOfReadOnlyCharacter= Number of readonly characters
IsRedColorForReadOnly= true, if readonly characters color is in red. False it those characters is in black color
SearchText= The text should be highlghted in RTB
