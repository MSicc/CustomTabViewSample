using SliMvvm.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomTabViewTest
{
    public class TabViewModel : ObservableObject
    {
        private string _title;
        private string _content;
        private bool _isSelected;

        public TabViewModel(string title)
        {
            this.Title = title;
            this.Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Tempor id eu nisl nunc mi ipsum faucibus vitae aliquet. Turpis egestas integer eget aliquet nibh praesent tristique magna. In fermentum posuere urna nec tincidunt. Vitae congue eu consequat ac felis donec et odio pellentesque. Augue lacus viverra vitae congue. Viverra vitae congue eu consequat. Orci nulla pellentesque dignissim enim sit amet venenatis urna. Et ultrices neque ornare aenean euismod elementum nisi. Id consectetur purus ut faucibus pulvinar. In cursus turpis massa tincidunt. Egestas pretium aenean pharetra magna. Et pharetra pharetra massa massa ultricies mi quis. Nunc sed blandit libero volutpat. Purus viverra accumsan in nisl nisi scelerisque eu ultrices vitae.";
        }

        public string Title { get => _title; set => Set(ref _title, value); }


        public string Content { get => _content; set => Set(ref _content, value); }

        public bool IsSelected { get => _isSelected; set =>Set(ref _isSelected, value); }
    }
}
