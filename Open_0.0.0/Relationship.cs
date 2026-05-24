

namespace Open_0._0._0
{
     class Relationship
    {
        public string RelationWith;//muss mit actor string nur für testen
        public Desire Desire = new Desire();
        public RelationshipState RelationshipState = RelationshipState.Stranger;

        public override string ToString()
        {
            return $"{RelationWith} ({RelationshipState})";
        }

    }
}