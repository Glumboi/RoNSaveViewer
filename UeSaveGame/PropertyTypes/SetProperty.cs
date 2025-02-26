﻿// Copyright 2022 Crystal Ferrai
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UeSaveGame.Util;

namespace UeSaveGame.PropertyTypes
{
	public class SetProperty : UProperty<UProperty[]>
    {
        private StructProperty? mPrototype;

        private int mRemovedCount;

        public FString? ItemType { get; private set; }

        public SetProperty(FString name, FString type)
            : base(name, type)
        {
        }

        public override void Deserialize(BinaryReader reader, long size, bool includeHeader)
        {
            if (includeHeader)
            {
                ItemType = reader.ReadUnrealString();
                reader.ReadByte();
            }

            if (ItemType == null) throw new InvalidOperationException("Cannot read set with unknown item type");

            mRemovedCount = reader.ReadInt32();
            if (mRemovedCount != 0)
            {
                // Sets can store items to be removed as well as items to be added. Have not encountered the removed case yet.
                throw new NotImplementedException();
            }

            int count = reader.ReadInt32();

            UProperty[] data;
            mPrototype = ArraySerializationHelper.Deserialize(reader, count, size - 8, ItemType, includeHeader, out data);
            Value = data;
        }

        public override long Serialize(BinaryWriter writer, bool includeHeader)
        {
            if (Value == null) throw new InvalidOperationException("Instance is not valid for serialization");

            if (includeHeader)
            {
                writer.WriteUnrealString(ItemType);
                writer.Write((byte)0);
            }

            long size = 4;
            writer.Write(mRemovedCount);
            if (mRemovedCount != 0)
            {
                throw new NotImplementedException();
            }

            size += 4;
            writer.Write(Value.Length);

            size += ArraySerializationHelper.Serialize(writer, ItemType, includeHeader, mPrototype, Value);

            return size;
        }

        public override string ToString()
        {
            return Value == null ? base.ToString() : $"{Name} [{nameof(SetProperty)}<{ItemType}>] Count = {Value.Length}";
        }
    }
}
