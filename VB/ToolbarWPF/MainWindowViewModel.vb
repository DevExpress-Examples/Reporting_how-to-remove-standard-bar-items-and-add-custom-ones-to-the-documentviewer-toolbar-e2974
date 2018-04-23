﻿Imports Microsoft.VisualBasic
Imports System.ComponentModel
Imports System.Windows.Input
Imports DevExpress.Xpf.Core.Commands
Imports DevExpress.Xpf.Printing
' ...

Namespace ToolbarWPF
	Friend Class MainWindowViewModel
		Implements INotifyPropertyChanged
		Private ReadOnly createDocumentCommand_Renamed As DelegateCommand(Of Object)
		Private ReadOnly clearDocumentCommand_Renamed As DelegateCommand(Of Object)
		Private previewModel_Renamed As LinkPreviewModel

		Public Property PreviewModel() As IDocumentPreviewModel
			Get
				Return previewModel_Renamed
			End Get
			Private Set(ByVal value As IDocumentPreviewModel)
				If previewModel_Renamed Is value Then
					Return
				End If
				previewModel_Renamed = CType(value, LinkPreviewModel)
				RaisePropertyChanged("PreviewModel")
			End Set
		End Property
		Public ReadOnly Property CreateDocumentCommand() As ICommand
			Get
				Return createDocumentCommand_Renamed
			End Get
		End Property
		Public ReadOnly Property ClearDocumentCommand() As ICommand
			Get
				Return clearDocumentCommand_Renamed
			End Get
		End Property

		Public Sub New()
			createDocumentCommand_Renamed = New DelegateCommand(Of Object)(AddressOf ExecuteCreateDocumentCommand, AddressOf CanExecuteCreateDocumentCommand)
			clearDocumentCommand_Renamed = New DelegateCommand(Of Object)(AddressOf ExecuteClearDocumentCommand, AddressOf CanExecuteClearDocumentCommand)
			PreviewModel = New LinkPreviewModel()
		End Sub

		Private Function CanExecuteCreateDocumentCommand(ByVal parameter As Object) As Boolean
			Return True
		End Function

		Private Sub ExecuteCreateDocumentCommand(ByVal parameter As Object)
			previewModel_Renamed.Link.CreateDocument(True)
			clearDocumentCommand_Renamed.RaiseCanExecuteChanged()
		End Sub

		Private Function CanExecuteClearDocumentCommand(ByVal parameter As Object) As Boolean
			Return Not PreviewModel.IsEmptyDocument
		End Function

		Private Sub ExecuteClearDocumentCommand(ByVal parameter As Object)
			previewModel_Renamed.Link.PrintingSystem.ClearContent()
			clearDocumentCommand_Renamed.RaiseCanExecuteChanged()
		End Sub

		#Region "INotifyPropertyChanged"

		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

		Private Sub RaisePropertyChanged(ByVal propertyName As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
		#End Region
	End Class
End Namespace