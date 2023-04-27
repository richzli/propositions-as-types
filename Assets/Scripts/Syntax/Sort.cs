using System;
using System.Collections;
using System.Collections.Generic;

namespace Syntax {

    public class Sort : Term
    {
        private int type;
        private Sort() {}
        private Sort(int type) {
            this.type = type;
        }

        private static Sort? set_ = null;
        private static Sort? prop_ = null;
        private static Dictionary<int, Sort> type_ = new Dictionary<int, Sort>();

        public static Sort SET {
            get {
                if (set_ == null) {
                    set_ = new Sort(-1);
                }
                return set_;
            }
        }
        public static Sort PROP {
            get {
                if (prop_ == null) {
                    prop_ = new Sort(-2);
                }
                return prop_;
            }
        }
        public static Sort TYPE(int i) {
            if (i < 0) {
                throw new ArgumentException();
            }
            if (!type_.ContainsKey(i)) {
                type_.Add(i, new Sort(i));
            }
            return type_[i];
        }

        public override string ToString()
        {
            if (type == -2) {
                return "Prop";
            } else if (type == -1) {
                return "Set";
            } else {
                return $"Type{type}";
            }
        }

        public override bool Is(object t)
        {
            return this == t;
        }

        public override bool Filled()
        {
            return true;
        }

        public bool IsType() {
            return type >= 0;
        }

        public int Level() {
            return IsType() ? type : -1;
        }
    }

}