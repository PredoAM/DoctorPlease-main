using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterGenerator : MonoBehaviour
{
List<string> firstNames = new List<string>{ "Joao", "Jessica", "Barbara", "Tomas", "Dafne",
											"Jaime", "Sara", "Bruno", "Davi", "Jefferson", "Ana", "Lidia",
											"Micaela", "Ana", "Lourenco", "Olavo", "Sofia", "Luiz",
											"Moises", "Estevao", "Henrique", "Rebeca", "Sebastiao", "Simone",
											"Miguel", "Angelica", "Marcos", "Allan", "Ricardo", "Helena", "Daniel",
											"Fernanda", "Joaquim", "Lorena" };

List<string> surnames = new List<string>{ "Pereira", "Silva", "Lima", "Moraes", "Sousa", "Rodrigues", "Oliveira",
											"Martins", "Almeida", "Barbosa", "Gomes", "Santos", "Coelho", "Carvalho",
											"Souza", "Costa", "Schmidt", "Santos", "Muniz", "Fernandes", "Nascimento", "Maciel",
											"Camargo", "Araujo", "Ribeiro", "Batista", "Barros", "Teixeira", "Muller", "Fischer", "Schneider",
											"Carvalho", "Macedo", "Ferreira", "Borges", "Jaeger", "Ziegler" };

List<string> symptoms = new List<string> { "Dor de cabeca", "Febre", "Tosse", "Fadiga", "Dor muscular", "Calafrios", "Dificuldade para respirar", "Dor no peito",
													"Nausea", "Vomito", "Diarreia", "Perda de apetite", "Erupcao cutanea", "Fadiga", "Suores noturnos", "Congestao nasal",
													"Dor abdominal", "Dores nas articulacoes", "Tontura", "Perda de olfato", "Confusao mental", "Olhos vermelhos", "Falta de ar", "Dor de garganta" };

List<string> sicknes = new List<string> { "Enxaqueca e ->", "Gripe e ->", "Bronquite e ->", "Anemia e ->", "Fibromialgia e ->", "Malaria e ->", "Asma e ->", "Angina e ->",
													"Gastrite e ->", "Vomito agudo e ->", "Gastroenterite e ->", "Hepatite e ->", "Dermatite e ->", "Sindrome da fadiga cronica e ->", "Tuberculose e ->",
													"Sinusite e ->", "Apendicite e ->", "Artrite e ->", "Vertigem e ->", "COVID-19 e ->", "Demencia e ->", "Conjuntivite e ->", "DPOC e ->", "Faringite e ->" };

List<string> NIS = new List<string>{"277056876540008","287120984760002", "296345178320004","314983645870006","327109283760003","339471087520001","341985672430007","357634178900008","369172087540004","374120938760003",  "382765478340006",  "395083761420002","404398572610009",
		"416987342580004",  "429017364570003","436789234570002","445987102830006","457432198620001",  "465789234670007","479013847260008",
		"485172936480003","497603184570002","508793617450004","512073984620001","523098741620006","537461029340002","543897612340009",
		"555671294380006","564839127450003","572098134580001","587612349870004","595871034670002","602198736450006","617409283650008","629874012360004","637198572430002","648792034870005","655401238960003"
};


	List<string> fakeNIS = new List<string> {
    "49760318 :) :D ;) 570002",
    "834726 :P :O XD 192834",
    "293847 ;) :/ :( 2837",
    "120398 :D :) :* 9384",
    "674920 XD ;) :P 3872",
    "902834 :O :/ :) 1234",
    "876234 :/ :P :S 0987",
    "432978 :( :D :) 1265",
    "849320 :| :O :P 0921",
    "283746 :S XD ;( 2384",
    "548392 :* :) :O 8765",
    "749284 <3 :P ;) 4387",
    "123984 :X :) :D 9834",
    "987234 >:( ;) XD 1257",
    "876239 :P :O :) 0982",
    "432908 ^_^ XD :D 4523"
};


	int difficulty = 0;
	public float z = 0;

	public const int baseFakeValue = 6;
	public int value = 0;


	public Letter createdletter;

	public void SetDifficulty(int newDifficulty)
	{
		difficulty = newDifficulty;
	}

	public void Generate(Letter letterComponent)
	{
		string firstName = PickRandomFromList(firstNames) + " ";
		string surname = PickRandomFromList(surnames);
		string sickness = PickRandomFromList(sicknes) + " ";
		string symptom = PickRandomFromList(symptoms);
		string NISS = PickRandomFromList(NIS);

		EDeliveryType type = Random.Range(0f, 1f) >= .65f ? EDeliveryType.FirstClass : EDeliveryType.SecondClass;

		bool isLetterValid = true;

		value = Random.Range(1, baseFakeValue + 1 + difficulty);

		//first couple letters guaranteed to be right
		if (LetterManager.instance.scorePennies < 3 && LetterManager.instance.scorePounds < 1)
			value = 0;

		//generate incorrect letter
		if (value >= baseFakeValue)
		{
			isLetterValid = false;
		
			switch (value)
			{
				case baseFakeValue:
					//no stamp
					type = EDeliveryType.Missing;
					break;
				case baseFakeValue + 1:
					//fake stamp
					type = Random.Range(0f, 1f) >= .5f ? EDeliveryType.FakeFirstClass : EDeliveryType.FakeSecondClass;
					break;
				case baseFakeValue + 2:
					//wrong name
					{
						int roll = Random.Range(0, 2);
						if (roll == 0)
							firstName = "";
						else
							surname = "";
						break;
					}
				case baseFakeValue + 3:
					//wrong Sickness
					{
						int roll = Random.Range(0, 2);
						if (roll == 0)
							sickness = "";
						else
							symptom = "";
						break;
					}
				case baseFakeValue + 4:
					//fake NIS
					NISS = PickRandomFromList(fakeNIS);
					break;
			}

		}

		string address = firstName + surname + "\n" + sickness + symptom + "\n" + NISS;
		letterComponent.Initialise(address, isLetterValid, type, z , value);
		createdletter = letterComponent;
		z += .2f;
		if (z > 8.5)
			z = 0;
	}

	string PickRandomFromList(List<string> list)
	{
		int index = Mathf.RoundToInt(Random.Range(0, list.Count));

		return list[index];
	}
}
