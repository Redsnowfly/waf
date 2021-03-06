﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using System.Waf.Foundation;
using System.Windows.Input;
using Waf.Writer.Applications.Documents;

namespace Waf.Writer.Applications.Services
{
    [Export(typeof(IFileService)), Export]
    internal class FileService : Model, IFileService
    {
        private readonly ObservableCollection<IDocument> documents;
        private readonly ReadOnlyObservableCollection<IDocument> readOnlyDocuments;
        private IDocument activeDocument;

        [ImportingConstructor]
        public FileService()
        {
            documents = new ObservableCollection<IDocument>();
            readOnlyDocuments = new ReadOnlyObservableCollection<IDocument>(documents);
        }

        public ReadOnlyObservableCollection<IDocument> Documents => readOnlyDocuments;

        public IDocument ActiveDocument
        {
            get { return activeDocument; }
            set
            {
                if (activeDocument != value)
                {
                    if (value != null && !documents.Contains(value))
                    {
                        throw new ArgumentException("value is not an item of the Documents collection.");
                    }
                    activeDocument = value;
                    RaisePropertyChanged();
                }
            }
        }

        public RecentFileList RecentFileList { get; set; }

        public ICommand NewCommand { get; set; }

        public ICommand OpenCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public ICommand SaveAsCommand { get; set; }

        public void AddDocument(IDocument document)
        {
            documents.Add(document);
        }

        public void RemoveDocument(IDocument document)
        {
            documents.Remove(document);
        }
    }
}
